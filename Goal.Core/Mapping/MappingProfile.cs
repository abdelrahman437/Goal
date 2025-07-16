using AutoMapper;
using Goal.Core.Models;
using Goal.Core.DTO;

namespace Goal.API.Controllers.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ReverseMap()
                .ForMember(dest => dest.DiscountId, opt => opt.AllowNull())
                .ForMember(dest => dest.ModifiedAt, opt => opt.AllowNull())
                .ForMember(dest => dest.DateDeleted, opt => opt.AllowNull())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CartItem, CartItemDTO>().ReverseMap()
                 .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, UpdateProductDTO>()
                .ReverseMap()
                .ForMember(dest => dest.DiscountId, opt => opt.AllowNull())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, AddProductDTO>()
                .ReverseMap()
                .ForMember(dest => dest.DiscountId, opt => opt.AllowNull())
                .ForMember(dest => dest.ModifiedAt, opt => opt.AllowNull())
                .ForMember(dest => dest.DateDeleted, opt => opt.AllowNull())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Discount,DiscountDTO>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Stock, StockDTO>()
                .ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Customer, RegisterDTO>().ReverseMap()
                .ForPath(dest => dest.CustomerAddress.address, opt => opt.MapFrom(src => src.address))
                .ForPath(dest => dest.CustomerAddress.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


        }
    }
}
