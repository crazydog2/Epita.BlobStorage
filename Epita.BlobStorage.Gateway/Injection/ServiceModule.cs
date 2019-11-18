using Epita.BlobStorage.Services;
using Epita.BlobStorage.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Epita.BlobStorage.Gateway.Injection
{
    public class ServiceModule : IInjectionModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IOrchestratorService, OrchestratorService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IPhotoService, PhotoService>();
            services.AddSingleton<IAlbumService, AlbumService>();
        }
    }
}