using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Models;
namespace ECommerce.Api.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Db.Product,  Models.Product>();
            CreateMap<Models.Product, Db.Product>();
        }
    }
}
