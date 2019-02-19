using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AxinomZip.Api.BasicAuthProvider;
using AxinomZip.Data.Infastructure;
using AxinomZip.Data.Repository;
using AxinomZip.Services.Interface;
using AxinomZip.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AxinomZip.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AxinomZipContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AxinomConnectionString")));
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ZipFileRepository>();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            var authConfig = new AuthConfig(Configuration);

            app.UseHttpsRedirection();
            app.UseMiddleware<BasicAuthMiddleWare>("axinom", authConfig);
            app.UseMvc();
        }
    }
}
