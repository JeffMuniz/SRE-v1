using AutoMapper;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Change.Worker.Backend.Application.Usecases.Shared.Mappings
{
    public class SupplierSkuMap : Profile
    {
        public SupplierSkuMap()
        {
            CreateMap<SharedUsecases.Models.SupplierSku, Domain.Entities.SupplierSku>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<SharedUsecases.Models.Category, Domain.Entities.Category>()
                .ReverseMap();

            CreateMap<SharedUsecases.Models.Subcategory, Domain.Entities.Subcategory>()
                .ReverseMap();

            CreateMap<SharedUsecases.Models.Brand, Domain.Entities.Brand>()
                .ReverseMap();
        }
    }
}
