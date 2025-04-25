using Dynasy.Data;
using Dynasy.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynasy.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(AppDbContext context, IMapper mapper, ILogger<ReviewService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            try
            {
                var reviews = await _context.Reviews
                    .Include(r => r.Product)
                    .Include(r => r.User)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка отзывов");
                throw new ApplicationException("Не удалось получить список отзывов", ex);
            }
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
                throw new NotFoundException($"Отзыв с ID {id} не найден");

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            try
            {
                var review = _mapper.Map<Review>(createReviewDto);
                review.CreatedAt = DateTime.UtcNow;

                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                return _mapper.Map<ReviewDto>(review);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании отзыва");
                throw new ApplicationException("Не удалось создать отзыв", ex);
            }
        }

        public async Task<ReviewDto> UpdateReviewAsync(int id, UpdateReviewDto updateReviewDto)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                throw new NotFoundException($"Отзыв с ID {id} не найден");

            _mapper.Map(updateReviewDto, review);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                throw new NotFoundException($"Отзыв с ID {id} не найден");

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}