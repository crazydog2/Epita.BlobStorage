using Epita.BlobStorage.Logic;
using Epita.BlobStorage.Logic.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Epita.BlobStorage.Gateway.Injection
{
    public class LogicModule : IInjectionModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IUserLogic, UserLogic>();
            services.AddSingleton<IPhotoLogic, PhotoLogic>();
            services.AddSingleton<IAlbumLogic, AlbumLogic>();
        }
    }
}