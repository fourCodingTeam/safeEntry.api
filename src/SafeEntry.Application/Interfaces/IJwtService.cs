namespace SafeEntry.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email);
        DateTime GetExpiration();
    }
}
