using System;

namespace GlobusInstagramLib.Authentication
{
    [Serializable]
    public class ImagesList : InstagramBaseObject
    {
        public ImageLink low_resolution;
        public ImageLink thumbnail;
        public ImageLink standard_resolution;
    }
}