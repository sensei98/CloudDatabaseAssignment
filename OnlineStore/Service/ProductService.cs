using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using OnlineStore.DAL;
using OnlineStore.Interface;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Threading.Tasks;
using HttpMultipartParser;
using OnlineStore.DTO;

namespace OnlineStore.Service
{
    public class ProductService : IProductService
    {
        private readonly OnlineStoreContext _context;
        public ProductService(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task AddProduct(CreateProductDTO product)
        {
            Product newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = $"€{product.Price}",
                Description = product.Description,
                Stock = product.Stock,
                ImageURL = product.ImageURL

            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSingleProductById(Guid id)
        {
            _context.Products.Remove(new Product { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetSingleProductById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateProductById(Guid id, Product product)
        {
            product.Id = id;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task<string> AddImageToProduct(FilePart image)
        {
            if (CloudStorageAccount.TryParse(Environment.GetEnvironmentVariable("AzureStorageAccount"), out CloudStorageAccount storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("productimages");

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(image.FileName);
                await cloudBlockBlob.UploadFromStreamAsync(image.Data);
                return cloudBlockBlob.Uri.AbsoluteUri;
            }
            else
            {
                throw new Exception("error");
                return null;
            }
        }
    }
}
