using AutoMapper;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Services.API.ViewModels;

namespace NS.SalesProduct.Services.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(productVm =>
                new Product(productVm.Id, productVm.Name, productVm.CostPrice, productVm.CostPrice))
                .ReverseMap();

            CreateMap<CustomerViewModel, Customer>()
                .ConstructUsing(customerVm =>
                new Customer(customerVm.Id, customerVm.Name, customerVm.Cpf, customerVm.BirthDate))
                .ReverseMap();

            CreateMap<SaleItemViewModel, SaleItem>()
                .ConstructUsing(saleItemVm =>
                new SaleItem(saleItemVm.Id, saleItemVm.ProductId, saleItemVm.SaleId, saleItemVm.Amount))
                .ReverseMap();

            CreateMap<SaleViewModel, Sale>()
                .ConstructUsing(saleVm =>
                new Sale(saleVm.Id, saleVm.CustomerId, saleVm.TotalPrice, saleVm.Discount, saleVm.PaymentMethod))
                .ReverseMap();
        }
    }
}
