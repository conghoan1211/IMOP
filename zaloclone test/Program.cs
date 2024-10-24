﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using zaloclone_test.Configurations;
using zaloclone_test.Models;
using zaloclone_test.MyHub;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor(); // Đăng ký IHttpContextAccessor

// add config manager appsettings
builder.Services.ConfigureServices(builder.Configuration);
ConfigManager.CreateManager(builder.Configuration);

// Add session 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian chờ phiên
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Chắc chắn cookie có mặt
});
builder.Services.AddSignalR();
// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = ConfigManager.gI().Issuer,
        ValidAudience = ConfigManager.gI().Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigManager.gI().SecretKey))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Lấy token từ cookie
            var token = context.Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithRedirects("/login");


app.MapGet("/", async (HttpContext context) =>
{
    var authenticateResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

    // Redirect to login if the user is not authenticated
    if (!authenticateResult.Succeeded || !context.User.Identity?.IsAuthenticated == true)
    {
        return Results.Redirect("/login");
    }
    return Results.Redirect("/post");
});

// Map SignalR hubs
app.MapHub<PostHub>("/postHub");
app.MapHub<ChatHub>("/chatHub");

app.MapRazorPages();

await app.RunAsync();
