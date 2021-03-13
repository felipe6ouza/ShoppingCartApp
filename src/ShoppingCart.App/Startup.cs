﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Data.Context;
using System;
using ShoppingCart.App.Configurations;
using Microsoft.Extensions.Hosting;
using ShoppingCart.App.Data;
using ShoppingCart.App.Areas.Identity.Data;
using ShoppingCart.App.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingCart.App
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ShoppingCartDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
          
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("IdentityContextConnection"), b => b.MigrationsAssembly("ShoppingCart.App"))                    );

                services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<IdentityContext>();


                services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.ResolveDependencies();

            services.AddAutoMapper(typeof(Startup));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Criar", policy => policy.Requirements.Add(new PermissaoNecessaria("Criar")));
                options.AddPolicy("Excluir", policy => policy.Requirements.Add(new PermissaoNecessaria("Excluir")));
                options.AddPolicy("Editar", policy => policy.Requirements.Add(new PermissaoNecessaria("Editar")));
                options.AddPolicy("Ler", policy => policy.Requirements.Add(new PermissaoNecessaria("Ler")));
            });

            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();


            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Produtos}/{action=Catalogo}");
                endpoints.MapRazorPages();
            });
        }
    }
}
