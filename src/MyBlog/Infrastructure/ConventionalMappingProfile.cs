using AutoMapper;
using MyBlog.Common.Mapping;
using System;
using System.Linq;

namespace MyBlog.Infrastructure
{
    public class ConventionalMappingProfile : Profile
    {
        public ConventionalMappingProfile()
        {
            var mapFromType = typeof(IMapFrom<>);
            var mapToType = typeof(IMapTo<>);
            var explicitMapType = typeof(IMapExplicitly);

            var modelRegistrations = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.StartsWith("MyBlog."))
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Type = t,
                    MapFrom = GetMappingModel(t, mapFromType),
                    MapTo = GetMappingModel(t, mapToType),
                    Explicit = (IMapExplicitly)(t.GetInterface(explicitMapType.Name) != null ?
                    Activator.CreateInstance(t) : null)
                });

            foreach (var modelRegistration in modelRegistrations)
            {
                if (modelRegistration.MapFrom != null)
                {
                    this.CreateMap(modelRegistration.MapFrom, modelRegistration.Type);
                }

                if (modelRegistration.MapTo != null)
                {
                    this.CreateMap(modelRegistration.Type, modelRegistration.MapTo);
                }

                modelRegistration.Explicit?.RegisterMappings(this);
            }
        }

        private Type GetMappingModel(Type type, Type mappingInterface)
            => type
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == mappingInterface)
            ?.GetGenericArguments()
            .First();
    }
}
