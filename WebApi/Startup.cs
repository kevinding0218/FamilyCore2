using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Persistent;
using WebApi.Persistent.Meal;
using WebApi.Persistent.Meal.EntreeHelperRepo;
using WebApi.Persistent.Meal.EntreePhotoRepo;
using WebApi.Persistent.Order.CurrentOrder;
using WebApi.Persistent.User;
using WebApi.Persistent.Utility;
using WebApi.Resource.Meal.PhotoResource;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVegetableRepository, VegetableRepository>();
            services.AddScoped<IEntreeDetailRepository, EntreeDetailRepository>();
            services.AddScoped<IStapleFoodRepository, StapleFoodRepository>();
            services.AddScoped<IEntreeRepository, EntreeRepository>();
            services.AddScoped<IEntreeHelperRepository, EntreeHelperRepository>();
            services.AddScoped<ICurrentOrderRepository, CurrentOrderRepository>();
            services.AddScoped<IEntreePhotoRepository, EntreePhotoRepository>();
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper();

            services.AddDbContext<FcDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddMvc();

            // ********************
            // Setup CORS
            // ********************
            var corsBuilder = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.WithOrigins("http://localhost:4200"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseStaticFiles();

            // ********************
            // USE CORS - might not be required.
            // ********************
            app.UseCors("SiteCorsPolicy");
        }
    }
}
