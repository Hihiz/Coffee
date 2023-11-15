using AutoMapper;
using Coffee.Application.Products.Commands.CreateProduct;
using Coffee.Application.Products.Commands.UpdateProduct;
using Coffee.Application.Products.Queries.GetProductDetail;
using Coffee.Application.Products.Queries.GetProductList;
using Coffee.Domain.Entities;

namespace Coffee.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailDto>().ReverseMap();
            CreateMap<Product, ProductListDto>().ReverseMap();

            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
        }
    }
}
