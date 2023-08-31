using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using WebApi.Constants;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Authorisatie
builder.Services.AddAuthorization(opts =>
{
    // bouwen van policies
    opts.AddPolicy(Policies.MustHaveStudentsId, policy =>
    {
        policy.RequireClaim("StudentId");
    });

    opts.AddPolicy(Policies.Title, policy =>
    {
        policy.RequireClaim("Title", "Master");
    });

    opts.AddPolicy(Policies.MajorStudent, policy =>
    {
        policy.RequireClaim("MajorStudent");
        policy.RequireUserName("whmuijs");
    });

    opts.AddPolicy(Policies.YearOfClass, policy =>
    {
        policy.RequireClaim("YearOfClass");
    });

    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        //Valideren de tokenwaarden met de waarden
        ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                builder.Configuration.GetValue<string>("Authentication:SecretKey")!))
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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

