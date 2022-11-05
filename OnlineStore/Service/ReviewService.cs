using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL;
using OnlineStore.DTO;
using OnlineStore.Interface;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service
{
    public class ReviewService : IReviewService
    {
        private readonly OnlineStoreContext _context;
        public ReviewService(OnlineStoreContext context)
        {
            _context = context;
        }
        public async Task AddReview(CreateReviewDTO review)
        {
            Review newReview = new Review()
            {
                Message = review.Message,
                Name = review.Name,
                Id = Guid.NewGuid(),
                ProductId = review.ProductId,
                CreatedAt = DateTime.UtcNow,
                Rating = review.Rating,
            };
            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSingleReviewById(Guid id)
        {
            _context.Reviews.Remove(new Review { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewsByProductId(Guid id)
        {
            var productId = await _context.Products.SingleAsync(i => i.Id == id);
            return await _context.Reviews.FindAsync(productId.Id);
        }

        public async Task<Review> GetSingleReviewById(Guid id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task UpdateReviewById(Guid id, Review review)
        {
            review.Id = id;
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
