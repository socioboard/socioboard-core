using System;
using GlobusInstagramLib.Authentication;

namespace GlobusInstagramLib.App.Core
{
    [Serializable]
    public class Pagination : InstagramBaseObject {
        public string next_url;
        public string next_max_id;
        public string next_max_like_id;
    }
}