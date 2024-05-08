using Application.Command.Order;
using Application.Command.People;
using Application.Command.Product;
using Application.Query.Order;
using Application.Query.People;
using Application.Query.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<People, PeopleDto>();
            CreateMap<CreatePeopleCommand,People>();

            CreateMap<Products, ProductDto>();
            CreateMap<CreateProductCommand, Products>();

            CreateMap<Orders, OrderDto>();
            CreateMap<CreateOrderCommand, Orders>();
        }
    }
}
