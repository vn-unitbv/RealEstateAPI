using Core.Services;
using DataAccess.Repositories;
using DataLayer;

namespace Project.Settings
{
    public static class Dependencies
    {

        public static void Inject(WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddControllers();
            applicationBuilder.Services.AddSwaggerGen();

            applicationBuilder.Services.AddDbContext<AppDbContext>();
            applicationBuilder.Services.AddScoped<AppDbContext>();

            AddRepositories(applicationBuilder.Services);
            AddServices(applicationBuilder.Services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<AuthorizationService>();
            services.AddTransient<UserService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<UserRepository>();
            services.AddTransient<AnnouncementRepository>();
            services.AddTransient<CommentRepository>();
            services.AddTransient<UnitOfWork>();
        }
    }
}
