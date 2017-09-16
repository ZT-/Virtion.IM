using System;

namespace Virtion.IM.View
{

    class ImLoginListener : Listener
    {
        public void onLogin(StatusCode code, User user)
        {
            switch (code)
            {
                case StatusCode.CodeOK:
                    Console.WriteLine("登录成功" + user);
                    break;
                case StatusCode.CodeReloginOK:
                    Console.WriteLine("重登录成功" + user);
                    break;
                case StatusCode.CodeVerifyFailed:
                    Console.WriteLine("密码或用户名错误");
                    return;
                default:
                    Console.WriteLine("登录未知异常！");
                    return;
            }
            Console.WriteLine(code);

            MainWindow.currentUser = user;

            MainWindow.friendListWindow = new Windows.FriendListWindow();
            MainWindow.friendListWindow.Show();
            MainWindow.loginWindows.Hide();

        }
        public void onLogout(StatusCode code)
        {
            Console.WriteLine("onLogout:" + code);
        }
        public void onReconnecting(StatusCode code, User currentUser)
        {
        }
    }

}
