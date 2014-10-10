using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public interface IPackageRepository
    {
        void AddPackage(Package package);
        int DeletePackage(Guid packageid);
        void UpdatePackage(Package package);
        List<Package> getAllPackage();
        bool checkPackageExists(string packagename);
        Package getPackageDetails(string packagename);
    }
}
