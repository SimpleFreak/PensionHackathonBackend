using PensionHackathonBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionHackathonBackend.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<Guid> Create(User user);

        Task<Guid> Delete(Guid id);

        Task<List<User>> Get();

        Task<User> GetByLogin(string login);

        Task<Guid> Update(Guid id, string login, string password, string role);
    }
}