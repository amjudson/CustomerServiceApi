using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CustomerServiceApi.Data;
using CustomerServiceApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

// USE for Email (reset password and confirm email)
// builder.Services.AddTransient<IEmailSender, EmailSender>();

// USE for access denied callback
builder.Services.ConfigureApplicationCookie(o =>
{
	o.AccessDeniedPath = "/NoAccess";
});

builder.Services.Configure<IdentityOptions>(o =>
{
	o.Password.RequiredLength = 6;
	o.Password.RequireNonAlphanumeric = false;
	o.Password.RequireLowercase = false;
	o.Password.RequireUppercase = false;
	o.Password.RequireDigit = false;
	o.Lockout.MaxFailedAccessAttempts = 3;
	o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
	o.SignIn.RequireConfirmedEmail = false;
});

var key = builder.Configuration["AppSettings:Secret"];
builder.Services.AddAuthentication(u =>
{
	u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(u =>
{
	u.RequireHttpsMetadata = false;
	u.SaveToken = true;
	u.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
		ValidateIssuer = false,
		ValidateAudience = false,
	};
});

builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
	o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
	{
		Description =
			"JWT Authorization header using the Bearer scheme. \n\n " +
			"Enter 'Bearer' [space] and then your token in the text input below.\n\n" +
			"Example: \"Bearer 12345abcdef\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Scheme = JwtBearerDefaults.AuthenticationScheme
	});
	o.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer",
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header,
			},
			new List<string>()
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(o =>
	o.AllowAnyHeader()
		.AllowAnyMethod()
		.AllowAnyOrigin()
		.WithExposedHeaders("*"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
