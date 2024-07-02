using AdminDashboard.Models;
using Arizona.Core.Entities;
using AutoMapper;

namespace AdminDashboard.Helpers
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {

            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
