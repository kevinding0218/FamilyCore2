using AutoMapper;
using DomainLibrary.Member;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using WebApi.EmailSettings;
using WebApi.Persistent;
using WebApi.Persistent.Meal;
using WebApi.Persistent.Meal.EntreeHelperRepo;
using WebApi.Persistent.Meal.EntreePhotoRepo;
using WebApi.Persistent.Member;
using WebApi.Persistent.Member.JWTAuth;
using WebApi.Persistent.Menu;
using WebApi.Persistent.Order.CurrentOrder;
using WebApi.Persistent.User;
using WebApi.Persistent.Utility;
using WebApi.Resource.Meal.PhotoResource;
using WebApi.Resource.Member.Jwt;

namespace WebApi
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureRepositoryService(IServiceCollection services)
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
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IJwtRepository, JwtRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void ConfigureJwtAuthService(IServiceCollection services)
        {
            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                // Validate the JWT Issuer (iss) claim  
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                // Validate the JWT Audience (aud) claim  
                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                // The signing key must match! 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                // Validate the token expiry  
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            // InvalidOperationException: Scheme already exists: Bearer 
            // Fix: Only include one service.AddAuthentication
            // 1. Add Auth0 Authentication Services
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = "https://familycore.auth0.com/";
            //    options.Audience = "https://api.family-core.com";
            //});

            // Add Custom Authentication Middleware Service
            // introduced JWT authentication to the request pipeline, 
            // specified the validation parameters to dictate how we want received tokens validated and finally, 
            // created an authorization policy to guard our API controllers and actions which we'll apply
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            // use a claims-based authorization check to give the role access to certain controllers and actions so that only users possessing the role claim may access those resources.
            // build and register a policy called ApiUser which checks for the presence of the Rol claim with a value of ApiAccess.
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(WebApi.Persistent.Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, WebApi.Persistent.Helpers.Constants.Strings.JwtClaims.ApiAccess));
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<WebApi.EmailSettings.EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailSender, AuthMessageSender>();
            ConfigureRepositoryService(services);

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
            corsBuilder.WithOrigins("http://localhost:4200", "https://familycoredevsite.azurewebsites.net"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });


            ConfigureJwtAuthService(services);

            // add identity
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<FcDbContext>().AddDefaultTokenProviders();

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 2. Enable authentication middleware
            app.UseAuthentication();

            app.UseMvc();
            app.UseStaticFiles();

            // ********************
            // USE CORS - might not be required.
            // ********************
            app.UseCors("SiteCorsPolicy");
        }
    }
}
