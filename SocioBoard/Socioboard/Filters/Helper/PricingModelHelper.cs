using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socioboard.Helper
{
    public class PricingModelHelper
    {
        public List<DataBindingHelper.StructNameValuePair> _lstStructNameValuePair = new List<DataBindingHelper.StructNameValuePair>();

        public PricingModelHelper(string priceHead, string h3, string p, string ulLiStrong, List<DataBindingHelper.StructNameValuePair> lstHelperStructNameValuePair)
        {
            DataBindingHelper.StructNameValuePair structNameValuePair_priceHead = new DataBindingHelper.StructNameValuePair() { name = "price-head", value = priceHead };
            _lstStructNameValuePair.Add(structNameValuePair_priceHead);

            DataBindingHelper.StructNameValuePair structNameValuePair_h3 = new DataBindingHelper.StructNameValuePair() { name = "h3", value = h3 };
            _lstStructNameValuePair.Add(structNameValuePair_h3);

            DataBindingHelper.StructNameValuePair structNameValuePair_p = new DataBindingHelper.StructNameValuePair() { name = "p", value = p };
            _lstStructNameValuePair.Add(structNameValuePair_p);

            DataBindingHelper.StructNameValuePair structNameValuePairpriceHead_ulLiStrong = new DataBindingHelper.StructNameValuePair() { name = "ulLiStrong", value = ulLiStrong };
            _lstStructNameValuePair.Add(structNameValuePairpriceHead_ulLiStrong);

            if (lstHelperStructNameValuePair!=null)
            {
                foreach (var item in lstHelperStructNameValuePair)
                {
                    _lstStructNameValuePair.Add(item);
                } 
            }
        }
    }
}