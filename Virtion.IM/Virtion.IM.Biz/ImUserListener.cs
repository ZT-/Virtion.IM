using System;
using System.Collections.Generic;
using Virtion.IM.View.Windows;

namespace Virtion.IM.View
{
    class ImUserListener : Listener
    {
        public void onSearchUserDetail(StatusCode code, User user)
        {
            if (code == StatusCode.CodeOK)
            {
                MainWindow.searchFriendWindow.ShowResult(true, user);
            }
            else
            {
                MainWindow.searchFriendWindow.ShowResult(false, null);
            }
        }

        public void onSearchGroupDetail(StatusCode code, Group group)
        {
            if (code == StatusCode.CodeOK)
            {
                MainWindow.searchFriendWindow.ShowResult(true, group);
            }
            else
            {
                MainWindow.searchFriendWindow.ShowResult(false, null);
            }
        }

        public void onGetUserDetail(StatusCode code, User user)
        {


        }

        public void onGetGroupMemberList(StatusCode code,Group group, List<User> userList)
        {
           var window = MainWindow.chatWindowMap[group.GroupID] as GroupChatWindow;
           window.SetMenberList(userList);

        }

        /**
         * 修改用户回调
         * @param code 状态码 参见 {@link StatusCode}
         * @param user 被修改的用户对象
         */
        public void onModifyUserInfo(StatusCode code, User user)
        {
        }

        /**
         * 搜索用户列表
         * @param code 状态码 参见 {@link StatusCode}
         * @param mList 结果集合
         * @param pagerIndex 页码
         */
        public void onSearchUserList(StatusCode code, List<User> mList,
             List<User> curPageList, int pagerIndex)
        {
            Console.WriteLine(curPageList);
        }

        /**
         * 添加好友回调
         * @param code 状态码 参见 {@link StatusCode}
         * @param user 被添加的好友
         */
        public void onAddFriend(StatusCode code, User user)
        {
            Console.WriteLine(user);
        }

        /**
         * 获取好友列表
         * @param code 状态码 参见 {@link StatusCode}
         * @param mList 好友列表
         */
        public void onGetFriendList(StatusCode code, List<User> mList)
        {
            if (code == StatusCode.CodeOK)
            {
                Console.WriteLine("成功获取好友列表");
                MainWindow.friendListWindow.SetFriendList(mList);
            }
            else
            {
                Console.WriteLine("error onGetFriendList" + code);
            }

        }

        public void onGetGroupList(StatusCode code, List<Group> mList)
        {
            if (code == StatusCode.CodeOK)
            {
                Console.WriteLine("成功获取群列表");
                MainWindow.friendListWindow.SetGroupList(mList);
            }
            else
            {
                Console.WriteLine("error onGetGroupList" + code);
            }

        }


        /**
         * 添加黑名单回调
         * @param code 状态码 参见 {@link StatusCode}
         * @param user 被添加黑名单用户
         */
        public void onAddBlocked(StatusCode code, User user)
        {

        }

        /**
         * 删除好友回调
         * @param code 状态码 参见 {@link StatusCode}
         * @param user 被删除的好友
         */
        public void onRemoveFriend(StatusCode code, User user)
        {
        }

        /**
         * 删除的黑名单回调
         * @param code 状态码 参见 {@link StatusCode}
         * @param user 被删除的黑名单用户
         */

        public void onRemoveBlocked(StatusCode code, User user)
        {

        }

        /**
         * 获取黑名单列表
         * @param code 状态码 参见 {@link StatusCode}
         * @param mList 黑名单列表
         */
        public void onGetBlockedList(StatusCode code, List<User> mList)
        {
        }

        /**
         * 登陆后返回登录用户信息
         * @param code 状态码 参见 {@link StatusCode}
         * @param user 登陆用户信息
         */
        public void onGetProfile(StatusCode code, User user)
        {
            user.ToString();
        }
    }


}
