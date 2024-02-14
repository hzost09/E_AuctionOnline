using ApplicationLayer;
using InfrastructureLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAppService(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.MaxDepth = 1000;
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
//builder.Services.AddMvc().AddJsonOptions(options => {
//    options.JsonSerializerOptions.MaxDepth = 64; // or however deep you need
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//jwt token
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    // không cần https để trao đổi metadata
    x.RequireHttpsMetadata = false;
    //save token vào AuthenticationProperties
    x.SaveToken = true;
    //định dạng chính xác việc xét jwt 
    x.TokenValidationParameters = new TokenValidationParameters
    {      
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my21charSuperSecretKeyForMy21charSuperSecretKey")),      
        ValidateAudience = false,    
        ValidateIssuer = false,   
        ClockSkew = TimeSpan.Zero,
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
