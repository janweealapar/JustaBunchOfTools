using JBOT.Application.Common.Interfaces;
using JBOT.Domain.Entities;
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
        #endregion

        #region Methods
        public Task<int> SaveChangeAsync()
        {
            return base.SaveChangesAsync();
        }
        #endregion
    }
}
