using Microsoft.AspNetCore.Authorization;
using WebAppUnderTheHood.AuthorizationRequirements;
using WebAppUnderTheHood.AuthorizationRequirements.HRManagerRequirements;

namespace WebAppUnderTheHood
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            builder.Services.AddAuthentication().AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
                options.LoginPath = "/Account/login";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBelongToHRDepartment", policy => policy.RequireClaim("Department", "HR"));
                options.AddPolicy("MustBeAdmin", policy => policy.RequireClaim("Role", "Admin"));
                options.AddPolicy("MustBeHRManager", policy => policy
                        .RequireClaim("Department", "HR")
                        .RequireClaim("Role", "Admin")
                        .Requirements.Add(new HRManagerProbationRequirement(3)));
            });

            builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
