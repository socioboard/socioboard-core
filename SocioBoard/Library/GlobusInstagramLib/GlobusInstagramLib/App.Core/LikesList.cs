using System;
using GlobusInstagramLib.App.Core;

namespace GlobusInstagramLib.Authentication
{
    [Serializable]
    public class LikesList : InstagramBaseObject
    {
        public int count;
        public User[] data;
    }
}