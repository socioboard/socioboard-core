using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Socioboard.Domain
{
    interface IUserPackageRelation
    {
        List<UserPackageRelation> getAllUserPackageRelation();
        void AddUserPackageRelation(UserPackageRelation userPackageRelation);
        int DeleteUserPackageRelation(UserPackageRelation userPackageRelation);
        int UpdateUserPackageRelation(User userPackageRelation);
    }
}
