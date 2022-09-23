using AutoMapper;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Product.Saga.Worker.Backend.Infrastructure.Persistence.Mappings
{
    public class SkuSagaHistoryMap : Profile
    {
        public SkuSagaHistoryMap()
        {
            CreateMap<Saga.States.SkuState, Entities.SkuSagaHistory>()
                .ForMember(
                    dest => dest.Data,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.MapFrom(source => DateTime.Now)
                );

            CreateMap<Saga.States.SkuState, IDictionary<string, object>>()
                .ConstructUsing((source, resulutionContext) =>
                {
                    var settings = new Newtonsoft.Json.JsonSerializerSettings
                    {
                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                        Converters =  new Newtonsoft.Json.JsonConverter[] {
                            new Newtonsoft.Json.Converters.ExpandoObjectConverter(),
                            new Newtonsoft.Json.Converters.StringEnumConverter()
                        }
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(
                        source, settings);
                    var dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(
                        json, settings);

                    return dictionary;
                });
        }
    }
}
