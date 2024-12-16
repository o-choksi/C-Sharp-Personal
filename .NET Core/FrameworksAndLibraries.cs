using System;

namespace DotNetCore
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

    // HTTP Client
    using System.Net.Http;
    public class HttpExample {
        private readonly HttpClient _client;
        public async void MakeRequest() {
            _client = new HttpClient();
            var response = await _client.GetAsync("https://api.example.com");
            string content = await response.Content.ReadAsStringAsync();
        }
    }

    // Dependency Injection
    using Microsoft.Extensions.DependencyInjection;
    public class DependencyExample {
        private readonly IServiceCollection _services;
        public void ConfigureServices() {
            _services = new ServiceCollection();
            _services.AddSingleton<IMyService, MyService>();
            var provider = _services.BuildServiceProvider();
        }
    }

    // Logging
    using Microsoft.Extensions.Logging;
    public class LoggingExample {
        private readonly ILogger _logger;
        public void WriteLog() {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<LoggingExample>();
            _logger.LogInformation("Application started");
        }
    }

    // Configuration
    using Microsoft.Extensions.Configuration;
    public class ConfigurationExample {
        private readonly IConfiguration _config;
        public void LoadConfig() {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }

    // Memory Cache
    using Microsoft.Extensions.Caching.Memory;
    public class CacheExample {
        private readonly IMemoryCache _cache;
        public void UseCache() {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _cache.Set("key", "value", TimeSpan.FromMinutes(30));
            string value = _cache.Get<string>("key");
        }
    }

    // Host Building
    using Microsoft.Extensions.Hosting;
    public class HostExample {
        private readonly IHost _host;
        public void BuildHost() {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => {
                    services.AddHostedService<MyBackgroundService>();
                })
                .Build();
        }
    }

    // Options Pattern
    using Microsoft.Extensions.Options;
    public class OptionsExample {
        private readonly IOptions<MyOptions> _options;
        public void ConfigureOptions(IServiceCollection services) {
            services.Configure<MyOptions>(options => {
                options.Setting1 = "value1";
                options.Setting2 = "value2";
            });
        }
    }

    // File Providers
    using Microsoft.Extensions.FileProviders;
    public class FileProviderExample {
        private readonly IFileProvider _provider;
        public void UseFileProvider() {
            _provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            IFileInfo fileInfo = _provider.GetFileInfo("myfile.txt");
            var contents = File.ReadAllText(fileInfo.PhysicalPath);
        }
    }

    // Object Pooling
    using Microsoft.Extensions.ObjectPool;
    public class PoolingExample {
        private readonly ObjectPool<StringBuilder> _pool;
        public void UsePool() {
            var provider = new DefaultObjectPoolProvider();
            _pool = provider.Create(new StringBuilderPooledObjectPolicy());
            var sb = _pool.Get();
            try {
                sb.Append("Hello World");
            }
            finally {
                _pool.Return(sb);
            }
        }
    }
}
}