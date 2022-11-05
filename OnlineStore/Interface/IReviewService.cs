using OnlineStore.DTO;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Interface
{
    public interface IReviewService
    {
        Task<List<Review>> GetReviews();
        Task<Review> GetSingleReviewById(Guid id);
        Task AddReview(CreateReviewDTO review);
        Task UpdateReviewById(Guid id, Review review);
        Task DeleteSingleReviewById(Guid id);
        Task<Review> GetReviewsByProductId(Guid id);
    }
}
