using FA.JustBlog.Core;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationUser = FA.JustBlog.Core.Models.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<JustBlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<JustBlogContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Account/Login"; // Redirect to login if not authenticated
    options.AccessDeniedPath = "/Admin/Account/Error"; // Redirect if access is denied
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireBlogOwnerRole", policy => policy.RequireRole("Blog Owner"));
    options.AddPolicy("RequireContributorOrHigher", policy => policy.RequireRole("Contributor", "Blog Owner"));
});


// Register the HttpClient
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICategoryRepositories , CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedRolesAndUsersAsync(userManager, roleManager);
}

async Task SeedRolesAndUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    string[] roleNames = { "User", "Contributor", "Blog Owner" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var users = new[]
    {
        new ApplicationUser { UserName = "user1", Email = "user1@example.com", Age = 25, AboutMe = "Regular user" },
        new ApplicationUser { UserName = "contributor1", Email = "contributor1@example.com", Age = 30, AboutMe = "Contributor user" },
        new ApplicationUser { UserName = "owner1", Email = "owner1@example.com", Age = 35, AboutMe = "Blog owner" },
        new ApplicationUser { UserName = "user2", Email = "user2@example.com", Age = 26, AboutMe = "Another regular user" },
        new ApplicationUser { UserName = "contributor2", Email = "contributor2@example.com", Age = 31, AboutMe = "Another contributor user" },
        new ApplicationUser { UserName = "owner2", Email = "owner2@example.com", Age = 36, AboutMe = "Another blog owner" },
        new ApplicationUser { UserName = "user3", Email = "user3@example.com", Age = 27, AboutMe = "Yet another regular user" },
        new ApplicationUser { UserName = "contributor3", Email = "contributor3@example.com", Age = 32, AboutMe = "Yet another contributor user" },
        new ApplicationUser { UserName = "owner3", Email = "owner3@example.com", Age = 37, AboutMe = "Yet another blog owner" }
    };

    var passwords = new[] { "User123!", "Contributor123!", "Owner123!", "User123!", "Contributor123!", "Owner123!", "User123!", "Contributor123!", "Owner123!" };
    var roles = new[] { "User", "Contributor", "Blog Owner", "User", "Contributor", "Blog Owner", "User", "Contributor", "Blog Owner" };

    for (int i = 0; i < users.Length; i++)
    {
        var user = users[i];
        var role = roles[i];
        var password = passwords[i];

        if (await userManager.FindByEmailAsync(user.Email) == null)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Post}/{action=Index}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
