
# Dev.AuthService

**Dev.AuthService** is a lightweight and reusable authentication service library for **ASP.NET Core Web API** developers. It allows quick and secure integration of user **Registration**, **Login**, and **JWT Token-based Authentication** with **Role-Based Authorization**.

---

## 🔧 Features

- ✅ User Registration & Login with JWT
- ✅ Role-Based Authorization Support
- ✅ Plug & Play Setup with `AddDevAuthService()`
- ✅ Clean Interface (IAuthService)
- ✅ Built with .NET 8

---

## 📦 Installation

Install the package via NuGet:

```bash
dotnet add package Dev.AuthService
Or search Dev.AuthService in the NuGet Package Manager inside Visual Studio.


🚀 Quick Start Guide

1. 🔧 Program.cs Setup
	In Program.cs, register the service:

		builder.Services.AddDevAuthService(builder.Configuration);


2. ⚙️ Add Configuration in appsettings.json

	"JwtSettings": 
	{
 	 "Key": "MyUltraSecretKeyThatIsOver32Chars!!",
 	 "Issuer": "your-app",
 	 "Audience": "your-app-users",
 	 "ExpiryMinutes": 60
	}
⚠️ Ensure the key length is at least 32 characters (for HS256 JWT algorithm).


3. 🧪 Use in Controller
	Create an Auth Controller and inject the service:


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return Ok(result);
    }
}

📘 Available Models
	RegisterRequest.cs

	public class RegisterRequest
	{
 	 public string Email { get; set; }
  	 public string Password { get; set; }
   	 public string Role { get; set; }
   	 public string UserName { get; set; }
	}

	LoginRequest

	public class LoginRequest
	{
  	  public string Email { get; set; }
   	 public string Password { get; set; }
	}
✅ Authentication Middleware Setup
	You don’t need to configure this manually if using AddDevAuthService()
	But for advanced cases, here’s what’s inside:

	
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });


🧠 FAQ
❓ Why do I get IDX10720: key size must be > 256 bits?
The JWT secret key in your appsettings.json must be at least 32 characters long. Shorter keys are rejected for security.

❓ What roles can I use?
Any string-based role (e.g., "Admin", "User", "Agent") is valid. You define and manage the role logic on your side.

📁 Sample App Repo
View a sample usage here:
🔗 GitHub - https://github.com/mohankumarcodes/Dev-AuthService-Packages

📃 License
MIT License © mohankumarcodes

📦 Publishing to NuGet (for maintainers)
Include this in your .csproj to pack the README:


<ItemGroup>
  <None Include="README.md" Pack="true" PackagePath="" />
</ItemGroup>

Then publish:


dotnet pack -c Release
dotnet nuget push bin/Release/Dev.AuthService.1.0.1.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json


---

Let me know if you'd like me to:
- Generate a GitHub `sample` Web API project using this package ✅
- Create a logo/badge for your NuGet listing 📛
- Help create versioned changelogs or release notes 🗂️






