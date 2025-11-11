using Fintech.Models;

namespace Fintech.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}