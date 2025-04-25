using Dynasy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynasy.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetReviewsForProductAsync(int productId);
        Task<Review> AddReviewAsync(int userId, int productId, string content, int rating);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}