using System;

namespace MyBlog.Constants
{
    public class WebConstants
    {
        public const string AdminRoleName = "Admin";
        public const string DefaultAdmin_Username = "adminUser";
        public const string DefaultAdmin_Email = "admin@myblog.com";
        public const string DefaultAdmin_Password = "RandomPass1!";
        public const string RegularRoleName = "Regular";
        public static readonly string DefaultAdmin_SecurityStamp = Guid.NewGuid().ToString();
        public static readonly string Regular_SecurityStamp = Guid.NewGuid().ToString();

        // Client Errors
        public const string UserWithUsernameAlreadyExists = "A user with that Username already exists! Try choosing another one.";
        public const string PasswordAndComfirmDoNotMatch = "Password and Comfirm do not match!";
        public const string LoginPasswordNotCorrect = "The password you entered is not correct!";
        public const string UserWithUsernameDoesNotExist = "User with that username does not exist. Please register before logging in.";
    }
}
