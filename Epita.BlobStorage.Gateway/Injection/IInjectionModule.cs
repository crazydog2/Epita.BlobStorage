using Microsoft.Extensions.DependencyInjection;

namespace Epita.BlobStorage.Gateway.Injection
{
    public interface IInjectionModule
    {
        void Register(IServiceCollection services);
    }
}