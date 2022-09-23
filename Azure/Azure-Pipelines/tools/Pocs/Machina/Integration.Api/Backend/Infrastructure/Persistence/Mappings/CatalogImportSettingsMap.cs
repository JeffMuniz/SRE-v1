using AutoMapper;
using Integration.Api.Backend.Infrastructure.Persistence.Entities;

namespace Integration.Api.Backend.Infrastructure.Persistence.Mappings
{
    public class CatalogImportSettingsMap : Profile
    {
        public CatalogImportSettingsMap()
        {
            CreateMap<Domain.Entities.CatalogImportSettings, CatalogImportSettings>()
                .ReverseMap();
        }
    }
}
