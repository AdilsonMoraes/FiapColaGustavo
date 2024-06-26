
using AutoMapper;
using FiapSmartCityDomain.Authentication;
using FiapSmartCityInfrastructure;
using FiapSmartCityIoC;
using FiapSmartCityServices.Authentication.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var allowedOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
builder.Services.AddAuthentication(options =>
{
    //Set default Authentication Schema as Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.Zero // remove delay of token when expire
            };
    });
//Jwt configuration ends here


DependencyResolver.RegisterServices(builder.Services);
DependencyResolver.RegisterRepositories(builder.Services);

var connectionString = builder.Configuration.GetConnectionString("SqlConnectionString") ?? string.Empty;
DependencyResolver.RegisterDB(builder.Services, connectionString);

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<SmartCitiesContext>();

var mappingConfiguration = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AuthenticationMappingConfigurator(mc));

});
var mapper = mappingConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowedOrigins, b => { b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowedOrigins);

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetRequiredService<SmartCitiesContext>())
    {
        var user = context.Users.FirstOrDefault(u => u.UserName == "Administrador");
        if (user == null)
        {
            user = new User()
            {
                Email = "admin@admin.com.br",
                UserName = "Administrador",
                FullName = "Administrador",
                DisplayName = "Administrador",
                PhoneNumber = "1234567890",
                Active = true
            };
            var addResult = scope.ServiceProvider.GetRequiredService<UserManager<User>>().CreateAsync(user, "Adm@2024").Result;
        }



        //gerenciamento de tráfeco
        user = context.Users.FirstOrDefault(u => u.UserName == "TrafficManagement");
        if (user == null)
        {
            user = new User()
            {
                Email = "TrafficManagement@TrafficManagement.com.br",
                UserName = "TrafficManagement",
                FullName = "TrafficManagement",
                DisplayName = "TrafficManagement",
                PhoneNumber = "1234567890",
                Active = true
            };
            var addResult = scope.ServiceProvider.GetRequiredService<UserManager<User>>().CreateAsync(user, "tm@2024").Result;
        }

        //gestão de Residuos
        user = context.Users.FirstOrDefault(u => u.UserName == "WasteManagement");
        if (user == null)
        {
            user = new User()
            {
                Email = "WasteManagement@WasteManagement.com.br",
                UserName = "WasteManagement",
                FullName = "WasteManagement",
                DisplayName = "WasteManagement",
                PhoneNumber = "1234567890",
                Active = true
            };
            var addResult = scope.ServiceProvider.GetRequiredService<UserManager<User>>().CreateAsync(user, "wm@2024").Result;
        }

        //Monitoramento Ambiental
        user = context.Users.FirstOrDefault(u => u.UserName == "EnvironmentalMonitoring");
        if (user == null)
        {
            user = new User()
            {
                Email = "EnvironmentalMonitoring@EnvironmentalMonitoring.com.br",
                UserName = "EnvironmentalMonitoring",
                FullName = "EnvironmentalMonitoring",
                DisplayName = "EnvironmentalMonitoring",
                PhoneNumber = "1234567890",
                Active = true
            };
            var addResult = scope.ServiceProvider.GetRequiredService<UserManager<User>>().CreateAsync(user, "em@2024").Result;
        }


        //Segurança Publica
        user = context.Users.FirstOrDefault(u => u.UserName == "PublicSecurity");
        if (user == null)
        {
            user = new User()
            {
                Email = "PublicSecurity@PublicSecurity.com.br",
                UserName = "PublicSecurity",
                FullName = "PublicSecurity",
                DisplayName = "PublicSecurity",
                PhoneNumber = "1234567890",
                Active = true
            };
            var addResult = scope.ServiceProvider.GetRequiredService<UserManager<User>>().CreateAsync(user, "pc@2024").Result;
        }

        var roles = new List<string>() { "Administrador", "TrafficManagement", "WasteManagement", "EnvironmentalMonitoring", "PublicSecurity" };
        roles.ForEach(role =>
        {
            var roleEntity = context.Roles.FirstOrDefault(r => r.Name == role);
            if (roleEntity == null)
            {
                context.Roles.Add(new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role
                });
            }
        });
        context.SaveChanges();


        var rolesUser = context.Roles.FirstOrDefault(u => u.Name == "Administrador");
        var userRole = context.Users.FirstOrDefault(u => u.UserName == "Administrador");
        var existsRole = context.UserRoles.FirstOrDefault(u => u.RoleId == rolesUser.Id);
        if (existsRole == null)
        {
            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                RoleId = rolesUser.Id,
                UserId = userRole.Id
            });

        }

        rolesUser = context.Roles.FirstOrDefault(u => u.Name == "TrafficManagement");
        userRole = context.Users.FirstOrDefault(u => u.UserName == "TrafficManagement");
        existsRole = context.UserRoles.FirstOrDefault(u => u.RoleId == rolesUser.Id);
        if (existsRole == null)
        {
            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                RoleId = rolesUser.Id,
                UserId = userRole.Id
            });
        }
        context.SaveChanges();


    }
}
app.Run();