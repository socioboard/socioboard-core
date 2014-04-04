using System;

namespace GlobusInstagramLib.Authentication
{
    [Serializable]
    public class Location : InstagramBaseObject
    {
        public string id;
        public double latitude;
        public double longitude;
        public string name;

    }
}
