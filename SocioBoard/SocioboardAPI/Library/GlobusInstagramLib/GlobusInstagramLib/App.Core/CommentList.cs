using System;

namespace GlobusInstagramLib.Authentication
{
    [Serializable]
    public class CommentList : InstagramBaseObject
    {
        public int count;
        public Comment[] data;
    }
}