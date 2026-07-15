using BLL.Interfaces;
using BLL.Services;
using DAL.Data;
using DAL.Implementations;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace RestaurantAndHotel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // CORS Local
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp", policy =>
//    {
//        policy.WithOrigins("http://localhost:4200")
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

// CORS Production
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("https://restaurant-frontend-p3l2.onrender.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

            // Local Database
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(
            //        builder.Configuration.GetConnectionString("DefaultConnection")
            //    ));

            //Server Connection
            builder.Services.AddDbContext<AppDbContext>(options =>
                      options.UseNpgsql(
                 builder.Configuration.GetConnectionString("DefaultConnection")));

            // JWT Configuration
            var jwtSettings = builder.Configuration.GetSection("Jwt");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
                        )
                    };
                });

            builder.Services.AddAuthorization();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Restaurant Management API",
                    Version = "v1"
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Enter: Bearer {your JWT token}",

                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        jwtSecurityScheme,
                        Array.Empty<string>()
                    }
                });
            });

            // Repositories
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemsRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IReservationTableRepository, ReservationTableRepository>();
            builder.Services.AddScoped<IRestaurantTableRepository, RestaurantTableRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IStockItemRepository, StockItemRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<DashboardRepository>();

            // Services
            builder.Services.AddScoped<IMenuItemService, MenuItemServices>();
            builder.Services.AddScoped<CustomerServices>();
            builder.Services.AddScoped<CategoryServices>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped<OrderItemServices>();
            builder.Services.AddScoped<IPaymentService, PaymentServices>();
            builder.Services.AddScoped<ReservationTableServices>();
            builder.Services.AddScoped<RestaurantTableServices>();
            builder.Services.AddScoped<RoleServices>();
            builder.Services.AddScoped<StockItemServices>();
            builder.Services.AddScoped<SupplierServices>();
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddScoped<DashboardServices>();

            var app = builder.Build();

            // Swagger Middleware
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors("AllowAngularApp");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
