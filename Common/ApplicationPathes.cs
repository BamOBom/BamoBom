using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class ApplicationPathes
    {
        #region Setting

        #region Locations
        public static class Location
        {
            public const string VirtualFolder = @"/Image/Setting/Locations/{0}";
        }
        public static class LocationExel
        {
            public const string VirtualFolder = @"Image/Setting/Locations/Exel/";
        }
        #endregion

        #region Application
        public static class Application
        {
            public const string VirtualFolder = @"/Images/Application/{0}";
            public const string VirtualUploadFolder = @"Images/Application/";
            public const string VirtualDeleteFile = @"/Images/Application/";

        }
        #endregion

        #endregion

        #region Category
        public static class Category
        {
            public const string VirtualFolder = @"/Image/Category/{0}";
            public const string VirtualUploadFolder = @"Image/Category/";
        }
        public static class CategoryExel
        {
            public const string VirtualFolder = @"Image/Category/Exel/";
        }
        #endregion

        #region UploadCenter
        public static class UploadCenter
        {
            public const string VirtualFolder = @"/Images/UploadCenter/{0}";
            public const string VirtualUploadFolder = @"Images/UploadCenter/";
            public const string VirtualDeleteFile = @"/Images/UploadCenter/";

        }
        #endregion

        #region Blog
        public static class Blog
        {
            public const string VirtualFolder = @"/Images/Blog/{0}";
            public const string VirtualUploadFolder = @"Images/Blog/";
            public const string VirtualDeleteFile = @"/Images/Blog/";

        }
        #endregion

        #region User
        public static class User
        {
            public const string VirtualFolder = @"/Image/User/{0}";
            public const string VirtualUploadFolder = @"Images/User/";
            public const string VirtualShowFolder = @"Images\User\";
            public const string VirtualDeleteFile = @"/Images/User/";
        }
        #endregion
      
    }
}
