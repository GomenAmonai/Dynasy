using System.Collections.Generic;
using System.Threading.Tasks;
using Dynasy.Models;

namespace Dynasy.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(int id, Review review);
        Task DeleteReviewAsync(int id);
    }
}