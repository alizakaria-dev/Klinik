using BLC.Appointment;
using BLC.Doctor;
using BLC.Feature;
using BLC.FeatureItem;
using BLC.File;
using BLC.Info;
using BLC.Role;
using BLC.Service;
using BLC.Testimonial;
using BLC.User;
using DALC.Appointment;
using DALC.Context;
using DALC.Doctor;
using DALC.Feature;
using DALC.FeatureItem;
using DALC.File;
using DALC.Info;
using DALC.Role;
using DALC.Service;
using DALC.Testimonial;
using DALC.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Shared.Jwt;
using Shared.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ServiceManager>();
builder.Services.AddScoped<IServiceRepository,ServiceRepository>();

builder.Services.AddScoped<DoctorManager>();
builder.Services.AddScoped<IDoctorRepository,DoctorRepository>();

builder.Services.AddScoped<FeatureManager>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();

builder.Services.AddScoped<FeatureItemManager>();
builder.Services.AddScoped<IFeatureItemRepository, FeatureItemRepository>();

builder.Services.AddScoped<FileManager>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

builder.Services.AddScoped<RoleManager>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<InfoManager>();
builder.Services.AddScoped<IInfoRespository, InfoRepository>();

builder.Services.AddScoped<AppointmentManager>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

builder.Services.AddScoped<TestimonialManager>();
builder.Services.AddScoped<ITestimonialRepository, TestimonialRepository>();

builder.Services.AddScoped<Jwt>();

builder.Services.AddMvc().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("JwtConfig:Audience"),
        ValidIssuer = builder.Configuration.GetValue<string>("JwtConfig:Issuer"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtConfig:SecretKey")))
    };
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "./Files/")),
    RequestPath = "/api/Files"
});


app.MapControllers();

app.Run();
