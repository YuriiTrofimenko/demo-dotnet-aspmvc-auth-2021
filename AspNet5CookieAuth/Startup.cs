using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AspNet5CookieAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static readonly string GOOGLE_OPEN_ID_AUTH_SCHEME = "GoogleOpenID";
        private static readonly string OKTA_OPEN_ID_AUTH_SCHEME = "OktaOpenID";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    // options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    // options.DefaultChallengeScheme = GOOGLE_OPEN_ID_AUTH_SCHEME;
                    // options.DefaultChallengeScheme = OKTA_OPEN_ID_AUTH_SCHEME;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                }
            ).AddCookie(
                options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/denied";
                    options.Events = new CookieAuthenticationEvents()
                    {
                        OnSigningIn = async context =>
                        {
                            var scheme =
                                context.Properties.Items.FirstOrDefault(pair => pair.Key == ".AuthScheme");
                            var claim = new Claim(scheme.Key, scheme.Value);
                            var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
                            claimsIdentity?.AddClaim(claim);
                        }
                    };
                }
            ).AddOpenIdConnect(GOOGLE_OPEN_ID_AUTH_SCHEME, options =>
                {
                    options.Authority = "https://accounts.google.com";
                    options.ClientId = "1047106611754-b6mtjf8njk5ttbri83lshkn88gpbsisf.apps.googleusercontent.com";
                    options.ClientSecret = "RrdDUGev8tSyzs9UPIeE646t";
                    options.CallbackPath = "/auth";
                }
            ).AddOpenIdConnect(OKTA_OPEN_ID_AUTH_SCHEME, options =>
            {
                options.Authority = "https://dev-12251583.okta.com/oauth2/default";
                options.ClientId = "0oa1i21bkolUSiwoy5d7";
                options.ClientSecret = "YftbgfYlOKinITWCfYD9gbnZbMZizNbh1UCv4lmN";
                options.CallbackPath = "/okta-auth";
                options.SignedOutCallbackPath = "/okta-signout";
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("offline_access");
            });
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}