using AutoMapper;
using MiniOrderManagement.Models;
using MiniOrderManagement.DTOs;

namespace MiniOrderManagement.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // 1. Product Mappings
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>(); // Map từ DTO tạo mới sang Entity

            // 2. Customer Mappings 
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerDto, Customer>(); 

            // 3. Order Mappings
            // Map Order -> OrderDto
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.OrderDetails));

            // Map OrderDetail -> OrderDetailDto
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.PriceAtOrder));
        }
    }
}