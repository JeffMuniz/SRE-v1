using AutoMapper;
using CSharpFunctionalExtensions;
using Humanizer;
using System;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Enrichment.macnaima.Api.Backend.Application.Usecases.Shared.Mappings
{
    public class ErrorMap : Profile
    {
        public ErrorMap()
        {
            CreateMap<IError<string>, SharedUsecases.Models.Error>()
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(source => source
                        .Error
                        .Truncate(3, "", Truncator.FixedNumberOfWords, TruncateFrom.Right)
                        .NormalizeCompare())
                )
                .ForMember(
                    dest => dest.Message,
                    opt => opt.MapFrom(source => source.Error)
                )
                .ReverseMap();
        }
    }
}
