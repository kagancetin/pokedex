using BeastBattle.Server.Data;
using BeastBattle.Server.Services;
using BeastBattle.Server.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BeastBattle.Server.Graphs;
using HotChocolate.Types.Pagination;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//builder.Services.AddTransient<BeastBattleDbContext>();
builder.Services.AddDbContextPool<BeastBattleDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault"));
}
);


builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<BeastBattleDbContext>()
    .SetPagingOptions(new PagingOptions
    {
        MaxPageSize = 100000
    })
    .AddFiltering<CustomFilteringConvention>()
    .AddSorting()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBeastService, BeastService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IAbilityService, AbilityService>();
builder.Services.AddScoped<IExecuteGraph, ExecuteGraph>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.MapGraphQL();

app.MapFallbackToFile("index.html");

app.Run();


