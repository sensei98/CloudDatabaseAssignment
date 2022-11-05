using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpMultipartParser;
using OnlineStore.DTO;

namespace OnlineStore.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetSingleProductById(Guid id);
        Task AddProduct(CreateProductDTO product);
        Task UpdateProductById(Guid id, Product product);
        Task DeleteSingleProductById(Guid id);
        Task<string> AddImageToProduct(FilePart image);
    }
}
