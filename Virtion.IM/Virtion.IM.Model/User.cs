using System;

namespace Virtion.IM.View
{

    public enum UserSex 
    {
        Man,
        Feman,
        Unkonw
    }

    public class User
    {
        public String Username { get; set; }
        public String NickName { get; set; }
        public String Sex { get; set; }
        public String Telphone { get;set;}

        public User()
        { 
        }

        public User(String userName)
        {
            this.Username = userName;
        }


    }
}
