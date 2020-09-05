using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Entities;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
        Task<User> FindByPhoneNumber(string PhoneNumber, CancellationToken cancellationToken);
        Task<User> GetByUserName(string username, CancellationToken cancellationToken);
        Task<User> FindByEmailOrPhoneAsync(string emailOrPhone, CancellationToken cancellationToken);

        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);

    }
}