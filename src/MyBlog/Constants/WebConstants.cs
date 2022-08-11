using System;

namespace MyBlog.Constants
{
    public class WebConstants
    {
        public const string AdminRoleName = "Admin";
        public const string DefaultAdmin_Username = "adminUser";
        public const string DefaultAdmin_Email = "admin@myblog.com";
        public const string DefaultAdmin_Password = "RandomPass1!";
        public static readonly string DefaultAdmin_SecurityStamp = Guid.NewGuid().ToString();
    }
}
