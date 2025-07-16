using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Goal.Data.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
                return result;

            foreach(var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is null || entry.State != EntityState.Deleted || !(entry.Entity is ISoftDeleteable entity))
                    continue;
                entry.State = EntityState.Modified;
                entity.Delete();
            }



            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is null || entry.State != EntityState.Deleted || !(entry.Entity is ISoftDeleteable entity))
                    continue;
                entry.State = EntityState.Modified;
                entity.Delete();
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
