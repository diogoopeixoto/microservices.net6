using AutoMapper;
using GeekShooping.ProductApi.Data.ValueObjects;
using GeekShooping.ProductApi.Model;
using GeekShooping.ProductApi.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace GeekShooping.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlContext _sqlContext;
        private IMapper _mapper;

        public ProductRepository(SqlContext sqlContext, IMapper mapper)
        {
            _sqlContext = sqlContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _sqlContext.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
           Product product = 
                await _sqlContext.Products.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Create(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _sqlContext.Products.Add(product);
            await _sqlContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _sqlContext.Products.Update(product);
            await _sqlContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> DeleteById(long id)
        {
            try
            {
                Product product =
                await _sqlContext.Products.Where(x => x.Id == id)
                .FirstOrDefaultAsync();

                if (product == null) return false;
                _sqlContext.Products.Remove(product);
                await _sqlContext.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
