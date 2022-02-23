using gawektree_back.Persistence.Interfaces;
using gawektree_back.Persistence.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace gawektree_back.Persistence.EF
{
    public static class PersistenceWithEFRegistration
    {
        public static IServiceCollection AddTreePersistenceEFServices(this IServiceCollection services, IConfiguration configuration)
        {
          
            services.AddDbContext<TreeContext>(options =>
               options.UseSqlServer(configuration.
                GetConnectionString("TreeConnectionMSSQLConnectionString")));

           // services.AddDbContext<TreeContext>(options =>
           //    options.UseSqlServer("Server = 192.168.5.3,1430; Database = tree; User Id = sa; Password = 1qaz@WSX; "));



            services.AddScoped<IRecordRepository, RecordRepository>();
           
            return services;
        }
    }
}
