using System;

namespace AspNetCore
{
    // JSON Processing
    using System.Text.Json;
    public class JsonExample {
        private readonly JsonDocument _doc;
        public void ProcessJson() {
            string jsonString = "{\"name\":\"John\",\"age\":30}";
            _doc = JsonDocument.Parse(jsonString);
            var root = _doc.RootElement;
            string name = root.GetProperty("name").GetString();
        }
    }

    // HTTP Client Factory
    using Microsoft.Extensions.Http;
    public class HttpClientFactoryExample {
        private readonly IHttpClientFactory _clientFactory;
        public async Task MakeRequest(IHttpClientFactory clientFactory) {
            _clientFactory = clientFactory;
            var client = _clientFactory.CreateClient("named-client");
            var response = await client.GetAsync("https://api.example.com");
        }
    }

    // Middleware
    using Microsoft.AspNetCore.Builder;
    public class MiddlewareExample {
        public void Configure(IApplicationBuilder app) {
            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Before Request");
                await next();
                await context.Response.WriteAsync("After Request");
            });
        }
    }

    // Routing
    using Microsoft.AspNetCore.Routing;
    public class RoutingExample {
        public void ConfigureRoutes(IEndpointRouteBuilder endpoints) {
            endpoints.MapGet("/hello/{name}", async context => {
                var name = context.Request.RouteValues["name"];
                await context.Response.WriteAsync($"Hello {name}!");
            });
        }
    }

    // SignalR
    using Microsoft.AspNetCore.SignalR;
    public class SignalRExample : Hub {
        public async Task SendMessage(string user, string message) {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }

    // Identity
    using Microsoft.AspNetCore.Identity;
    public class IdentityExample {
        private readonly UserManager<IdentityUser> _userManager;
        public async Task CreateUser(UserManager<IdentityUser> userManager) {
            var user = new IdentityUser { UserName = "test@test.com" };
            await userManager.CreateAsync(user, "Password123!");
        }
    }

    // CORS
    using Microsoft.AspNetCore.Cors;
    public class CorsExample {
        public void ConfigureCors(IServiceCollection services) {
            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder => {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }
    }

    // Rate Limiting
    using Microsoft.AspNetCore.RateLimiting;
    public class RateLimitingExample {
        public void ConfigureRateLimiting(IServiceCollection services) {
            services.AddRateLimiter(options => {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                    RateLimitPartition.GetFixedWindowLimiter("global", _ => 
                        new FixedWindowRateLimiterOptions { PermitLimit = 100, Window = TimeSpan.FromMinutes(1) }));
            });
        }
    }

    // Health Checks
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    public class HealthCheckExample : IHealthCheck {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken token) {
            return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
        }
    }

    // Data Protection
    using Microsoft.AspNetCore.DataProtection;
    public class DataProtectionExample {
        private readonly IDataProtector _protector;
        public void ProtectData(IDataProtectionProvider provider) {
            _protector = provider.CreateProtector("purpose");
            var protected_payload = _protector.Protect("secret-value");
            var unprotected_payload = _protector.Unprotect(protected_payload);
        }
    }

    // Session
    using Microsoft.AspNetCore.Session;
    public class SessionExample {
        public void ConfigureSession(IServiceCollection services) {
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
    }

    // WebSockets
    using Microsoft.AspNetCore.WebSockets;
    public class WebSocketExample {
        public void ConfigureWebSocket(IApplicationBuilder app) {
            app.UseWebSockets();
            app.Use(async (context, next) => {
                if (context.WebSockets.IsWebSocketRequest)
                    await HandleWebSocket(context);
                else
                    await next();
            });
        }
    }

    // Output Caching
    using Microsoft.AspNetCore.OutputCaching;
    public class OutputCacheExample {
        public void ConfigureOutputCache(IServiceCollection services) {
            services.AddOutputCache(options => {
                options.AddBasePolicy(builder => 
                    builder.Expire(TimeSpan.FromMinutes(10)));
            });
        }
    }

    // Response Compression
    using Microsoft.AspNetCore.ResponseCompression;
    public class CompressionExample {
        public void ConfigureCompression(IServiceCollection services) {
            services.AddResponseCompression(options => {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
        }
    }

    // Authentication
    using Microsoft.AspNetCore.Authentication;
    public class AuthenticationExample {
        public void ConfigureAuth(IServiceCollection services) {
            services.AddAuthentication()
                .AddJwtBearer(options => {
                    options.Authority = "https://auth.example.com";
                    options.Audience = "api";
                });
        }
    }

    // Authorization
    using Microsoft.AspNetCore.Authorization;
    public class AuthorizationExample {
        public void ConfigureAuthorization(IServiceCollection services) {
            services.AddAuthorization(options => {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
            });
        }
    }

    // Endpoint Routing
    using Microsoft.AspNetCore.Routing;
    public class EndpointRoutingExample {
        public void ConfigureEndpoints(IEndpointRouteBuilder endpoints) {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
            endpoints.MapHub<ChatHub>("/chathub");
        }
    }

    // Minimal APIs
    using Microsoft.AspNetCore.Builder;
    public class MinimalApiExample {
        public void ConfigureApi(WebApplication app) {
            app.MapGet("/api/hello", () => "Hello, World!")
               .WithName("GetHello")
               .WithOpenApi();
        }
    }
}
}