using JBOT.Application.Common.Interfaces;
using JBOT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace JBOT.Infrastructure.Services
{
    public class ValidationContextFactory : IValidationContextFactory
    {
        private readonly IConfiguration _configuration;
        public ValidationContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IValidateDBContext CreateInstanceByServer(string server)
        {
            string connsString = _configuration.GetConnectionString("ValidateDatabase").Replace("{server}",server);
            var option = new DbContextOptionsBuilder<ValidateDBContext>()
                         .UseSqlServer(connsString, b => b.MigrationsAssembly(typeof(ValidateDBContext).Assembly.FullName))
                         .Options;
            return new ValidateDBContext(option);
        }
    }
}
