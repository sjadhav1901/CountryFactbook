using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Core.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // DataModels => Models
            CreateMap<Contracts.DataModels.User, Contracts.Models.User>();
        }
    }
}
