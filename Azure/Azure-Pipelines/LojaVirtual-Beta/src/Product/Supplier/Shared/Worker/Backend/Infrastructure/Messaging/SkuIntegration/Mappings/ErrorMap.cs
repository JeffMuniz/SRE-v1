using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Product.Change.Worker.Consumers.Mappings
{
    public class ErrorMap : Profile
    {
        public ErrorMap()
        {
            CreateMap<MessagingContracts.Shared.Messages.NotFound, Supplier.Shared.Worker.Backend.Domain.ValueObjects.ErrorType>()
                .ForCtorParam(
                    "id",
                    opt => opt.MapFrom(source => Supplier.Shared.Worker.Backend.Domain.ValueObjects.ErrorType.NotFound.Id)
                )
                .ForCtorParam(
                    "error",
                    opt => opt.MapFrom(source => source.Message ?? Supplier.Shared.Worker.Backend.Domain.ValueObjects.ErrorType.NotFound.Error)
                )
                .ReverseMap();

            CreateMap<MessagingContracts.Shared.Messages.UnexpectedError, Supplier.Shared.Worker.Backend.Domain.ValueObjects.ErrorType>()
                .ForCtorParam(
                    "id",
                    opt => opt.MapFrom(source => Supplier.Shared.Worker.Backend.Domain.ValueObjects.ErrorType.Unexpected.Id)
                )
                .ForCtorParam(
                    "error",
                    opt => opt.MapFrom(source => source.Message ?? Supplier.Shared.Worker.Backend.Domain.ValueObjects.ErrorType.Unexpected.Error)
                )
                .ReverseMap();
        }
    }
}
