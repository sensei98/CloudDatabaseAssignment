using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using OnlineStore.Model;
using System;

namespace OnlineStore.DAL
{
    public class OnlineStoreContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Review>? Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToContainer("Users").HasPartitionKey(u=> u.Id);
            modelBuilder.Entity<Order>().ToContainer("Orders").HasPartitionKey(o => o.Id).HasMany(p => p.Products);
            modelBuilder.Entity<Product>().ToContainer("Products").HasPartitionKey(p => p.Id);
            modelBuilder.Entity<Review>().ToContainer("Reviews").HasPartitionKey(r => r.Id);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseCosmos(
                Environment.GetEnvironmentVariable("DB_HOST"),
                Environment.GetEnvironmentVariable("DB_CONNECTION"),
                databaseName: Environment.GetEnvironmentVariable("DATABASE"));

            CreateImagesBlobContainer();
           
        }
        private static async void CreateImagesBlobContainer()
        {
            if (CloudStorageAccount.TryParse(Environment.GetEnvironmentVariable("AzureStorageAccount"), out CloudStorageAccount storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("productimages");
                if (!await cloudBlobContainer.ExistsAsync())
                {
                    await cloudBlobContainer.CreateAsync();

                    BlobContainerPermissions permissions = new()
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                }
            }
            else
            {
                throw new Exception("error");
            }
        }
    }
}
