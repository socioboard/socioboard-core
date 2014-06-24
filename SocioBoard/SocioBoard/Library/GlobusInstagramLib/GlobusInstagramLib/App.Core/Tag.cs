using System;
using GlobusInstagramLib.Authentication;

namespace GlobusInstagramLib.App.Core
{
    [Serializable]
    public class Tag : InstagramBaseObject
    {
        public string name;
        public int media_count;
    }
}