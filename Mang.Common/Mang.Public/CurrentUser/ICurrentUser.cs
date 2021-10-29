using System.Collections.Generic;
using System.Security.Claims;

namespace Mang.Public.CurrentUser
{
    public interface ICurrentUser
    {
        int Id { get; }

        string Name { get; }

        string Phone { get; }

        string Avatar { get; }

        bool? IsAdmin { get; }

        List<int> Roles { get; }

        Claim FindClaim(string claimType);

        List<Claim> FindClaims(string claimType);

        List<Claim> GetAllClaims();

        bool HasRole(int roleId);
    }
}