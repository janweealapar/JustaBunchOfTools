using JBOT.Application.Common.Interfaces;
using JBOT.Application.Helpers;
using JBOT.Domain.Entities;
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



        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Error: The type Category must be a reference type in order to use it as parameter TEntity in the 
            // genric type or method ModelBuilder.Entity<TEntity>()

            modelBuilder.Entity<Status>().HasData(EnumHelper.EnumToModel<Status,StatusEnums>());
            modelBuilder.Entity<Operator>().HasData(EnumHelper.EnumToModel<Operator, OperatorEnums>());
        }

        public Task<int> SaveChangeAsync()
        {
            return base.SaveChangesAsync();
        }
        #endregion
    }
}
