using JBOT.Application.Common.Interfaces;
using JBOT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.Infrastructure.Services
{
    public class ValidationContextFactory : IValidationContextFactory
    {
        public IValidateDBContext CreateInstanceByServer(string server)
        {
            string connsString = $"Data Source={server};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadOnly;MultiSubnetFailover=False";
            var option = new DbContextOptionsBuilder<ValidateDBContext>()
                         .UseSqlServer(connsString, b => b.MigrationsAssembly(typeof(ValidateDBContext).Assembly.FullName))
                         .Options;
            return new ValidateDBContext(option);
        }
    }
}
