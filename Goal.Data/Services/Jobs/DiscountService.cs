namespace Goal.Data.Services.Jobs
{
    public class DiscountService
    {
        private readonly AppDbContext _context;

        public DiscountService(AppDbContext context)
        {
            _context = context;
        }
        public async Task UpdateDiscounts()
        {
            var today = DateTime.Now;

            var toactive = _context.Discount.Where(e => !e.IsActive && e.StartDate <= today && e.EndDate > today);
            foreach (var discount in toactive)
            {
                discount.IsActive = true;
            }
            var toDeactive = _context.Discount.Where(e => e.IsActive && e.EndDate <= today || e.StartDate > today);
            foreach (var discount in toDeactive)
            {
                discount.IsActive = false;
            }
            await _context.SaveChangesAsync();
        }
    }
}
