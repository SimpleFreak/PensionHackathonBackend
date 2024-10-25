namespace PensionHackathonBackend.Infrastructure.Abstraction
{
    public interface IPasswordHasher
    {
        string Generate(string password);

        bool Verify(string hashedPassword, string providedPassword);
    }
}