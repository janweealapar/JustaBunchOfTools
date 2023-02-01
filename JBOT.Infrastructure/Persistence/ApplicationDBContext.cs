using JBOT.Application.Common.Interfaces;
using JBOT.Application.Helpers;
using JBOT.Domain.Entities;
using JBOT.Domain.Entities.Common;
using JBOT.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        #region Constructor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        #endregion

        #region DbSets
        public DbSet<UnitTest> UnitTests { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Operator> Operators { get; set; }
        #endregion

        #region Override
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Error: The type Category must be a reference type in order to use it as parameter TEntity in the 
            // genric type or method ModelBuilder.Entity<TEntity>()
            modelBuilder.Entity<Status>().HasData(EnumHelper.EnumToModel<Status, StatusEnums>());
            modelBuilder.Entity<Operator>().HasData(EnumHelper.EnumToModel<Operator, OperatorEnums>());
        }
        #endregion

        #region CRUD Methods
        public void Add<U>(BaseEntity<U> entity, string user = null)
        {
            entity.RecordUser = user;
            entity.DateRecorded = DateTime.Now;
            base.Add(entity);
        }

        public void BulkInsert<U>(IEnumerable<BaseEntity<U>> collection, string user = null)
        {
            foreach (var entity in collection)
            {
                entity.RecordUser = user;
                entity.DateRecorded = DateTime.Now;
            }
            base.AddRange(collection);
        }    

        public void Update<U>(BaseEntity<U> entity, string user = null)
        {
            entity.ModifiedBy = user;
            entity.DateModified = DateTime.Now;

            if (this.Entry(entity).State == EntityState.Detached)
            {
                base.Update(entity);
            }
        }

        public void BulkUpdate<U>(IEnumerable<BaseEntity<U>> collection, string user = null)
        {
            foreach (var entity in collection)
            {
                entity.ModifiedBy = user;
                entity.DateModified = DateTime.Now;
            }

            if (this.Entry(collection).State == EntityState.Detached)
            {
                base.UpdateRange(collection);
            }
        }

        public void Delete<U>(BaseEntity<U> entity)
        {
            base.Remove(entity);
        }

        public async Task<T> FindAsync<T>(int id) where T: class
        {
            return await base.FindAsync<T>(id);
        }

        public Task<int> SaveChangeAsync()
        {
            return base.SaveChangesAsync();
        }
        #endregion
    }
}
