public interface IJwtGenerator
{
    string CreateToken(User user);
}