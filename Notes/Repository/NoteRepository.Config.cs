using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Threax.ReflectedServices;

namespace Notes.Repository.Config
{
    public partial class NoteRepositoryConfig : IServiceSetup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            OnConfigureServices(services);

            services.TryAddScoped<INoteRepository, NoteRepository>();
        }

        partial void OnConfigureServices(IServiceCollection services);
    }
}