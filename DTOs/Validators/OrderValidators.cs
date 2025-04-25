public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("ID пользователя обязателен");

        RuleFor(x => x.ProductIds)
            .NotEmpty().WithMessage("Список продуктов не может быть пустым")
            .Must(x => x != null && x.Count > 0).WithMessage("Должен быть выбран хотя бы один продукт");
    }
}

public class UpdateOrderDtoValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderDtoValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Неверный статус заказа");
    }
}