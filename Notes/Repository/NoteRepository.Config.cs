using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Threax.ReflectedServices;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Notes.Repository;

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