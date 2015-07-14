using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Api.Socioboard.Helper.UserManager
{
    public class UserStore :
     IUserStore<User, Guid>,
     IUserPasswordStore<User, Guid>,
     IUserRoleStore<User, Guid>
    {
        public UserStore()
        {
           
        }
        public void Dispose()
        {
            this.database.Dispose();
        }

        public Task CreateAsync(User user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user) 
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user) 
        {
            throw new NotImplementedException();
        }

        public  Task<User> FindByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public  Task<User> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
        public User FindAsync(string userName, string password)
        {
            return new User();
        }


        // Summary:
        //     Adds a user to a role
        //
        // Parameters:
        //   user:
        //
        //   roleName:
        public Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Returns the roles for this user
        //
        // Parameters:
        //   user:
        public Task<IList<string>> GetRolesAsync(User user)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Returns true if a user is in the role
        //
        // Parameters:
        //   user:
        //
        //   roleName:
        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Removes the role for the user
        //
        // Parameters:
        //   user:
        //
        //   roleName:
        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }
       

    }
}