using DataAccessObj;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Service.Interfaces;
using Service.Services;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add JWT authentication

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});

// Add services to the container.
builder.Services.AddDbContext<GRACEFULLFLORISTContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddDbContext<GRACEFULLFLORISTContext>(options => options.UseQueryTrackingBehavior(Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking));
builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllersWithViews().AddNewtonsoftJson(
    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add Scope here
builder.Services.AddScoped(typeof(GRACEFULLFLORISTContext));
//User
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
//Feedback
builder.Services.AddScoped<FeedbackRepository>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
//Order
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
//ODetail
builder.Services.AddScoped<OrderDetailRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
//Product
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
//Promotion
builder.Services.AddScoped<PromotionRepository>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
//Transaction
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
//Shipping
builder.Services.AddScoped<ShippingPriceRepository>();
builder.Services.AddScoped<IShippingPriceService, ShippingPriceService>();
//Attribute
builder.Services.AddScoped<AttributeRepository>();
builder.Services.AddScoped<IAttributeService, AttributeService>();



builder.Services.AddSwaggerGen(option =>
{
    /*
    option.SwaggerDoc("GracefullFlorist", new OpenApiInfo() { Title = "GracefullFlorist", Version = "v1" });
    //setup comment in swagger UI
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentFileFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);

    option.IncludeXmlComments(xmlCommentFileFullPath);
    */
    //set up jwt token authorize

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


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //c => c.SwaggerEndpoint("/swagger/GRACEFULL_FLORIST/swagger.json", "GracefullFloristAPI v1")
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
