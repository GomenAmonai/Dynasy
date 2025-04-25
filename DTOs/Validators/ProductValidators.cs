public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название продукта обязательно")
            .MaximumLength(100).WithMessage("Название продукта не может быть длиннее 100 символов");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Описание не может быть длиннее 1000 символов");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Цена должна быть больше 0");

        RuleFor(x => x.SellerId)
            .NotEmpty().WithMessage("ID продавца обязателен");
    }
}

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название продукта обязательно")
            .MaximumLength(100).WithMessage("Название продукта не может быть длиннее 100 символов");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Описание не может быть длиннее 1000 символов");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Цена должна быть больше 0");
    }
}