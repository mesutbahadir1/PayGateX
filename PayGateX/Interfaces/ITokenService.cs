using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}