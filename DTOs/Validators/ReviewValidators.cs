public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Содержание отзыва обязательно")
            .MaximumLength(1000).WithMessage("Максимальная длина отзыва 1000 символов");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Рейтинг должен быть от 1 до 5");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("ID пользователя обязателен");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ID продукта обязателен");
    }
}