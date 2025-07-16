using Goal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Goal.Data.Interceptors
{
    public class DiscountPriceInterceptors : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (eventData.Context is null)
                return new ValueTask<InterceptionResult<int>>(result);
            var context = eventData.Context;
            foreach (var entry in eventData.Context.ChangeTracker.Entries<Product>())
            {
                bool IsPriceModified = entry.Property(p => p.Price).IsModified || entry.State == EntityState.Added;
                bool IsDicountIdModified = entry.Property(p => p.DiscountId).IsModified;

                if (IsPriceModified || IsDicountIdModified)
                    UpdateDiscountedPrice(context, entry.Entity);

            }


            foreach (var entry in eventData.Context.ChangeTracker.Entries<Discount>())
            {
                bool stateOfDeleted = entry.State == EntityState.Deleted;
                bool IsStateModified = entry.Property(p => p.IsActive).IsModified;
                bool IsValueModified = entry.Property(p => p.Value).IsModified;
                if (IsStateModified || IsValueModified || stateOfDeleted)
                {
                    UpdateDiscountedPrice(context, entry.Entity, stateOfDeleted);
                }
            }


            return new ValueTask<InterceptionResult<int>>(result);
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData, InterceptionResult<int> result)
        {
            if(eventData.Context is null)
                return base.SavingChanges(eventData, result);
            var context = eventData.Context;
            foreach (var entry in eventData.Context.ChangeTracker.Entries<Product>())
            {
                bool IsPriceModified = entry.Property(p => p.Price).IsModified || entry.State == EntityState.Added;
                bool IsDicountIdModified = entry.Property(p => p.DiscountId).IsModified;

                if(IsPriceModified || IsDicountIdModified)
                    UpdateDiscountedPrice(context, entry.Entity);
                
            }


            foreach (var entry in eventData.Context.ChangeTracker.Entries<Discount>())
            {
                bool stateOfDeleted = entry.State == EntityState.Deleted;
                bool IsStateModified = entry.Property(p => p.IsActive).IsModified;
                bool IsValueModified = entry.Property(p => p.Value).IsModified;
                if (IsStateModified || IsValueModified || stateOfDeleted)
                {
                     UpdateDiscountedPrice(context, entry.Entity,stateOfDeleted);
                }
            }


                return base.SavingChanges(eventData, result);
            
        }
        private void UpdateDiscountedPrice(DbContext context, Product product)
        {
            if (product.DiscountId > 0)
            {
                if (product.Discount == null)
                    context.Entry(product).Reference(p => p.Discount).Load();

                if (product.Discount != null && product.Discount.IsActive && !product.Discount.IsDeleted)
                    product.DiscountedPrice = product.Price - (product.Price * product.Discount.Value / 100);
                else
                    product.DiscountedPrice = product.Price;
            }
        }
        private void UpdateDiscountedPrice(DbContext context, Discount discount, bool stateOfDiscount)
        {
            context.Entry(discount).Collection(d => d.Products).Load();
            if(discount.Products != null)
                foreach(var p in discount.Products)
                {
                    if (((discount.EndDate > DateTime.Now && discount.StartDate <= DateTime.Now) || discount.IsActive) && !discount.IsDeleted && !stateOfDiscount)
                        p.DiscountedPrice = p.Price - (p.Price * discount.Value / 100);
                    else
                        p.DiscountedPrice = p.Price;
                }
        }


        
    }
}
