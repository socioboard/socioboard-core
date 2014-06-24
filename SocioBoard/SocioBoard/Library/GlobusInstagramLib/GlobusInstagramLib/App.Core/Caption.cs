using System;
using GlobusInstagramLib.App.Core;

namespace GlobusInstagramLib.Authentication
{
    [Serializable]
    public class Caption : InstagramBaseObject
    {
        public double created_time;
        public string text;
        public string id;
        public User from;
    }
}