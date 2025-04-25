using System.Collections.Generic;
using System.Threading.Tasks;
using Dynasy.Models;

public interface IReviewService
{
    Task<Review> AddReviewAsync(Review review);
    Task<IEnumerable<Review>> GetReviewsForProductAsync(int productId);
    Task DeleteReviewAsync(int reviewId);
}