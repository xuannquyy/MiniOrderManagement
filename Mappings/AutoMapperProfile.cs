using AutoMapper;
using MiniOrderManagement.Models;
using MiniOrderManagement.DTOs;

namespace MiniOrderManagement.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerCreateDto, Customer>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.OrderDetails));
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
