using DefectComp.Models;
using DefectComp.Models.StatusesTable;
using DefectComp.Models.EnterpriseTable;
using DefectComp.Models.CompensationTypesTable;
using DefectComp.Models.SCsTable;
using DefectComp.Models.DepartmentsTable;
using DefectComp.Models.GoodsTable;
using DefectComp.Models.OrdersTable;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DefectComp.Models.Users;
using DefectComp.Models.NotesTable;
using DefectComp.Models.CommonLogTable;
using Microsoft.Extensions.Hosting;

namespace DefectComp
{
    public class Startup
    {
        public IConfigurationRoot Configuration;

        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration["Data:DefectComp:ConnectionString"]));
            services.AddDbContext<AppIdentityDBContext>(options => options.UseSqlServer(Configuration["Data:DefectComp:ConnectionString"]));
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.User.AllowedUserNameCharacters = "ёйцукенгшщзхъфывапролджэячсмитьбюЁЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮQWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            }
                ).AddEntityFrameworkStores<AppIdentityDBContext>().AddDefaultTokenProviders();
            services.AddTransient<IGoodsRepository, EFGoodsRepository>();
            services.AddTransient<IStatusesRepository, EFStatusesRepository>();
            services.AddTransient<IEnterpriseRepository, EFEnterpriseRepository>();
            services.AddTransient<ICompensationTypeRepository, EFCompensationTypeRepository>();
            services.AddTransient<ISCRepository, EFSCRepository>();
            services.AddTransient<IDepartmentRepository, EFDepartmentRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddTransient<INoteRepository, EFNoteRepository>();
            services.AddTransient<ICommonLogRepository, EFCommonLogRepository>();
            services.AddMvc(options => options.EnableEndpointRouting = false); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "Error", template: "Error", defaults: new { Controller="Error", action = "Error"});
                routes.MapRoute(name: null, template: "", defaults: new { Controller = "Home", action = "Index" });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            }
                      );
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}








