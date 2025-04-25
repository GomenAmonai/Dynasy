public class LoginUserQuery : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Некорректный формат email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен");
    }
}

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginUserQueryHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher<User> passwordHasher,
        IJwtGenerator jwtGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
        if (user == null)
            throw new ValidationException("Неверный email или пароль");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new ValidationException("Неверный email или пароль");

        return _jwtGenerator.CreateToken(user);
    }
}