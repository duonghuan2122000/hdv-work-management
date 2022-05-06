using HDV.BTL.Auth.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        Task<User> GetAsync(string email);
    }
}
