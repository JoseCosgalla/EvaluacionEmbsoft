using AutoMapper;
using System;

namespace AeropuertoApp.Web.Mapper
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UseCases.Mapper.UseCasesProfile());
                cfg.AddProfile(new WebProfile());
            });

            return config;
        }
    }
}
