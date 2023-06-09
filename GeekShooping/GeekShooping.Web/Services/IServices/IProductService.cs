﻿using GeekShooping.Web.Models;

namespace GeekShooping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAllProducts();
        Task<ProductViewModel> FindProductById(long id);
        Task<ProductViewModel> CreateProduct(ProductViewModel model);
        Task<ProductViewModel> UpdateProduct(ProductViewModel model);
        Task<bool> DeleteProductById(long id);
    }
}
