using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Domain.Socioboard.Domain
{
    public interface IDiscoverySearchRepository
    {

        void addNewSearchResult(DiscoverySearch dis);
        void updateNewSearchResult(DiscoverySearch dis);
        void deleteSearchResult(DiscoverySearch dis);
        List<DiscoverySearch> getResultsFromKeyword(string keyword);
        bool isKeywordPresent(string keyword,string messageid);
        List<string> getAllSearchKeywords(Guid Userid);
       

    }
}