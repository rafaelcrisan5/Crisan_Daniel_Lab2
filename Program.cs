using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Crisan_Daniel_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    
    options.Conventions.AuthorizeFolder("/Books");
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/Details");

   
    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");

   
    options.Conventions.AuthorizeFolder("/Publishers", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categories", "AdminPolicy");
   
});


builder.Services.AddDbContext<Crisan_Daniel_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Crisan_Daniel_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Crisan_Daniel_Lab2Context' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Crisan_Daniel_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Crisan_Daniel_Lab2Context' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
