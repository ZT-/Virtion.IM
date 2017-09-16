using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Virtion.IM.View.Virtion.Util.Data.MySql;

namespace Virtion.IM.View
{
    public delegate void CommamdCallBack();

    public class ImManager
    {
        private MysqlHelper mysqlHelper;
        private CommandExecutor cmdExecutor;
        private ImLoginListener loginListener;
        private ImNotifyListener notifyListener;
        private ImChatListener chatListener;
        private ImUserListener userListener;

        public ImManager()
        {
            this.mysqlHelper = new MysqlHelper(new ConnectParam()
            {
                Server = "127.0.0.1",
                UserID = "root",
                Password = "root",
                Database = "chat_system",
            });
            this.cmdExecutor = new CommandExecutor();

            this.loginListener = new ImLoginListener();
            this.notifyListener = new ImNotifyListener();
            this.chatListener = new ImChatListener();
            this.userListener = new ImUserListener();
        }

        public void CheckListeners()
        {
            this.CheckChildListener(this.userListener);
            this.CheckChildListener(this.loginListener);
            this.CheckChildListener(this.notifyListener);
            this.CheckChildListener(this.chatListener);
        }

        private void CheckChildListener(Listener listener)
        {
            if (listener.listenerCallBack != null)
            {
                listener.listenerCallBack();
                listener.listenerCallBack = null;
            }
        }

        public void RegisterListener(Listener listener)
        {
            if (listener.GetType() == typeof(ImLoginListener))
            {
                this.loginListener = listener as ImLoginListener;
            }
            else if (listener.GetType() == typeof(ImNotifyListener))
            {
                this.notifyListener = listener as ImNotifyListener;
            }
            else if (listener.GetType() == typeof(ImNotifyListener))
            {
                this.chatListener = listener as ImChatListener;
            }
        }

        public Boolean Login(String user, String password)
        {
            this.cmdExecutor.Push(() =>
            {
                Table userTable = mysqlHelper.OpenTable("users");
                Condition condition = new Condition();
                condition["username"] = user;
                condition["password"] = password;
                int n = userTable.Where(condition).Count();
                if (n <= 0)
                {
                    this.loginListener.listenerCallBack += () =>
                    {
                        this.loginListener.onLogin(StatusCode.CodeLoginFailed, new User(user));
                    };
                }
                else
                {
                    Condition data = new Condition();
                    data["is_online"] = 1;
                    userTable.Where(condition).Update(data);

                    User currentUser = this.GetUserInfoForDatabase(user);

                    this.loginListener.listenerCallBack += () =>
                    {
                        this.loginListener.onLogin(StatusCode.CodeOK, currentUser);
                    };
                }
            });
            return true;
        }

        public Boolean RegisterUser(User user, String password)
        {
            this.cmdExecutor.Push(() =>
            {
                Table userTable = this.mysqlHelper.OpenTable("users");
                Condition condition = new Condition();
                condition["username"] = user.Username;
                condition["nickname"] = user.NickName;
                condition["password"] = password;
                condition["sex"] = user.Sex;
                condition["telphone"] = user.Telphone;
                condition["is_online"] = 0;
                userTable.Insert(condition);
            });
            return true;
        }

        public Boolean GreateGroup(String groupName, String description)
        {
            this.cmdExecutor.Push(() =>
            {
                Table groupTable = this.mysqlHelper.OpenTable("groups");
                Condition condition = new Condition();
                condition["group_name"] = groupName;
                condition["owner"] = MainWindow.currentUser.Username;
                condition["description"] = description;
                groupTable.Insert(condition);

                this.notifyListener.listenerCallBack += () =>
                {
                    MessageBox.Show("创建成功");
                };

            });
            return true;
        }

        public Boolean GetFriendList(User user)
        {
            this.cmdExecutor.Push(() =>
            {
                Table friendTable = mysqlHelper.OpenTable("friend");
                String condition = "username='" + user.Username + "'";

                DataTable dt = friendTable.Where(condition).Select();

                if (dt.Rows.Count <= 0)
                {
                    this.userListener.listenerCallBack += () =>
                    {
                        this.userListener.onGetFriendList(StatusCode.CodeUserNotFound, null);
                    };
                }
                else
                {
                    List<User> userList = new List<User>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var friend = this.GetUserInfoForDatabase(dt.Rows[i]["friend"].ToString());
                        if (friend != null)
                        {
                            userList.Add(friend);
                        }
                    }

                    this.userListener.listenerCallBack += () =>
                    {
                        this.userListener.onGetFriendList(StatusCode.CodeOK, userList);
                    };
                }
            });
            return true;
        }

        public Boolean GetGroupList(User user)
        {
            this.cmdExecutor.Push(() =>
            {
                Table addGroupTable = this.mysqlHelper.OpenTable("add_group");
                String condition = "username='" + user.Username + "'";

                DataTable dt = addGroupTable.Where(condition).Select();

                if (dt.Rows.Count <= 0)
                {
                    this.userListener.listenerCallBack += () =>
                    {
                        this.userListener.onGetGroupList(StatusCode.CodeUserNotFound, null);
                    };
                }
                else
                {
                    List<Group> groupList = new List<Group>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var group = this.GetGroupInfoForDatabase(dt.Rows[i]["groupid"].ToString());
                        if (group != null)
                        {
                            groupList.Add(group);
                        }
                    }

                    this.userListener.listenerCallBack += () =>
                    {
                        this.userListener.onGetGroupList(StatusCode.CodeOK, groupList);
                    };
                }
            });
            return true;
        }

        private User GetUserInfoForDatabase(String userName)
        {
            Table userTable = mysqlHelper.OpenTable("users");
            Condition condition = new Condition();
            condition["username"] = userName;
            DataTable dataTable = userTable.Where(condition).Select("username", "nickname", "sex", "telphone");

            if (dataTable.Rows.Count <= 0)
            {
                return null;
            }
            return new User()
            {
                Username = condition["username"].ToString(),
                NickName = dataTable.Rows[0]["nickname"].ToString()
            };
        }

        private User GetUserInfoForDatabaseByView(String userName)
        {
            Table userTable = mysqlHelper.OpenTable("user_info");
            Condition condition = new Condition();
            condition["username"] = userName;
            DataTable dataTable = userTable.Where(condition).Select("username", "nickname", "sex", "telphone");

            if (dataTable.Rows.Count <= 0)
            {
                return null;
            }
            return new User()
            {
                Username = condition["username"].ToString(),
                NickName = dataTable.Rows[0]["nickname"].ToString()
            };
        }

        private Group GetGroupInfoForDatabase(String groupName)
        {
            Table userTable = mysqlHelper.OpenTable("groups");
            Condition condition = new Condition();
            condition["groupid"] = groupName;
            DataTable dataTable = userTable.Where(condition)
                .Select("groupid", "group_name", "owrer", "time");

            if (dataTable.Rows.Count <= 0)
            {
                return null;
            }
            return new Group()
            {
                GroupID = condition["groupid"].ToString(),
                GroupName = dataTable.Rows[0]["group_name"].ToString(),
                Owrer = dataTable.Rows[0]["owrer"].ToString()
            };
        }

        public Boolean GetGroupMenber(Group group)
        {
            this.cmdExecutor.Push(() =>
            {
                Table addGroupTable = this.mysqlHelper.OpenTable("add_group");
                Condition condition = new Condition();
                condition["groupid"] = group.GroupID;
                DataTable dt = addGroupTable.Where(condition).Select();

                List<User> userList = new List<User>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var friend = this.GetUserInfoForDatabase(dt.Rows[i]["username"].ToString());
                    if (friend != null)
                    {
                        userList.Add(friend);
                    }
                }

                this.userListener.listenerCallBack += () =>
                {
                    this.userListener.onGetGroupMemberList(StatusCode.CodeOK, group, userList);
                };
            });
            return true;
        }

        public Boolean SearchFriend(String userName)
        {
            this.cmdExecutor.Push(() =>
            {
                User friend = this.GetUserInfoForDatabase(userName);
                if (friend != null)
                {
                    this.userListener.listenerCallBack += () =>
                    {
                        this.userListener.onSearchUserDetail(StatusCode.CodeOK, friend);
                    };
                }
                else
                {
                    this.userListener.listenerCallBack += () =>
                    {
                        this.userListener.onSearchUserDetail(StatusCode.CodeUserNotExist, null);
                    };
                }
            });
            return true;
        }

        public Boolean SearchGroup(String groupName)
        {
            this.cmdExecutor.Push(() =>
            {
                Group group = this.GetGroupInfoForDatabase(groupName);
                if (group != null)
                {
                    this.userListener.listenerCallBack += () =>
                    {
                        this.userListener.onSearchGroupDetail(StatusCode.CodeOK, group);
                    };
                }
                else
                {
                    this.userListener.listenerCallBack += () =>
                    {
                        this.userListener.onSearchGroupDetail(StatusCode.CodeUserNotExist, null);
                    };
                }
            });
            return true;
        }

        public Boolean AddFriend(User user)
        {
            this.cmdExecutor.Push(() =>
            {
                Table friendTable = this.mysqlHelper.OpenTable("friend");
                Condition condition = new Condition();
                condition["username"] = MainWindow.currentUser.Username;
                condition["friend"] = user.Username;
                friendTable.Insert(condition);

                this.notifyListener.listenerCallBack += () =>
                {
                    this.notifyListener.onFriendChanged(true, user);
                };

            });
            return true;
        }

        public Boolean AddFriendByProcedure(User user)
        {
            this.cmdExecutor.Push(() =>
            {
                Table friendTable = this.mysqlHelper.OpenTable("friend");

                String sql = "call add_friend('"+MainWindow.currentUser.Username
                    +"','"+user.Username +"')";
                friendTable.Execute(sql);

                this.notifyListener.listenerCallBack += () =>
                {
                    this.notifyListener.onFriendChanged(true, user);
                };

            });
            return true;
        }

        public Boolean AddGroup(Group group)
        {
            this.cmdExecutor.Push(() =>
            {
                Table addGroupTable = this.mysqlHelper.OpenTable("add_group");
                Condition condition = new Condition();
                condition["username"] = MainWindow.currentUser.Username;
                condition["groupid"] = group.GroupID;
                addGroupTable.Insert(condition);

                this.notifyListener.listenerCallBack += () =>
                {
                    this.notifyListener.onGroupChanged(true, group);
                };
            });
            return true;
        }

        public Boolean DeleteFriend(User user)
        {
            this.cmdExecutor.Push(() =>
            {
                Table friendTable = this.mysqlHelper.OpenTable("friend");
                Condition condition = new Condition();
                condition["username"] = MainWindow.currentUser.Username;
                condition["friend"] = user.Username;
                friendTable.Where(condition).Delete();

                this.notifyListener.listenerCallBack += () =>
                {
                    this.notifyListener.onFriendChanged(false, user);
                };

            });
            return true;
        }

        public Boolean DeleteGroup(Group group)
        {
            this.cmdExecutor.Push(() =>
            {
                Table addGroupTable = this.mysqlHelper.OpenTable("add_group");
                Condition condition = new Condition();
                condition["username"] = MainWindow.currentUser.Username;
                condition["groupid"] = group.GroupID;
                addGroupTable.Where(condition).Delete();

                this.notifyListener.listenerCallBack += () =>
                {
                    this.notifyListener.onGroupChanged(false, group);
                };

            });
            return true;
        }

        public Boolean SendMessage(User receiver, String text)
        {
            return this.SendMessage(MainWindow.currentUser, receiver, text);
        }

        public Boolean SendMessage(User sender, User receiver, String text)
        {
            this.cmdExecutor.Push(() =>
            {
                Table messageTable = mysqlHelper.OpenTable("message");
                Condition data = new Condition();
                data["sender"] = sender.Username;
                data["receiver"] = receiver.Username;
                data["text"] = text;
                data["is_read"] = 0;

                messageTable.Insert(data);
            });
            return true;
        }

        public Message SendMessage(User sender, User receiver, String text, String userData)
        {
            return new Message();
        }

        public Boolean SendGroupMessage(Group receiver, String text)
        {
            this.cmdExecutor.Push(() =>
            {
                Table messageTable = mysqlHelper.OpenTable("message");
                Condition data = new Condition();
                data["sender"] = MainWindow.currentUser.Username;
                data["receiver"] = receiver.GroupID;
                data["text"] = text;
                data["is_read"] = 0;

                messageTable.Insert(data);
            });
            return true;
        }

        private Boolean MarkMessage(String sender, String time)
        {
            Table messageTable = this.mysqlHelper.OpenTable("message");
            Condition condition = new Condition();
            condition["sender"] = sender;
            condition["time"] = time;

            Condition data = new Condition();
            data["is_read"] = 1;
            messageTable.Where(condition).Update(data);

            return true;
        }

        public Boolean GetMessage(User reciever)
        {
            this.cmdExecutor.Push(() =>
            {
                Table messageTable = this.mysqlHelper.OpenTable("message");
                Condition condition = new Condition();
                condition["receiver"] = MainWindow.currentUser.Username;
                condition["is_read"] = 0;
                DataTable dt = messageTable.Where(condition).Select();

                if (dt.Rows.Count == 0)
                {
                    return;
                }

                this.chatListener.listenerCallBack += () =>
                {
                    List<Message> messageList = new List<Message>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.MarkMessage(
                            dt.Rows[i]["sender"].ToString(),
                            dt.Rows[i]["time"].ToString()
                            );

                        Message message = new Message()
                        {
                            Text = dt.Rows[i]["text"].ToString(),
                            Time = dt.Rows[i]["time"].ToString()
                        };
                        messageList.Add(message);
                    }

                    if (dt.Rows.Count == 1)
                    {
                        this.chatListener.onReceiveMessage(messageList[0], dt.Rows[0]["sender"].ToString());
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        this.chatListener.onGetMessageList(messageList, dt.Rows[0]["sender"].ToString());
                    }
                };

            });
            return true;
        }

        public Boolean GetGroupMessage(Group reciever, string time)
        {
            this.cmdExecutor.Push(() =>
            {
                Table messageTable = this.mysqlHelper.OpenTable("message");

                DataTable dt;
                if (time != null)
                {
                    string condition = " receiver=" + reciever.GroupID
                        + " and  is_read = 0  and message.time >  '" + time+"'";
                    dt = messageTable.Where(condition).Select();
                }
                else
                {
                    Condition condition = new Condition();
                    condition["receiver"] = reciever.GroupID;
                    condition["is_read"] = 0;
                    dt = messageTable.Where(condition).Select();
                }

                if (dt.Rows.Count == 0)
                {
                    return;
                }

                this.chatListener.listenerCallBack += () =>
                {
                    List<Message> messageList = new List<Message>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Message message = new Message()
                        {
                            Text = dt.Rows[i]["text"].ToString(),
                            Time = dt.Rows[i]["time"].ToString()
                        };
                        messageList.Add(message);
                    }

                    if (dt.Rows.Count == 1)
                    {
                        this.chatListener.onReceiveMessage(messageList[0], dt.Rows[0]["receiver"].ToString());
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        this.chatListener.onGetMessageList(messageList, dt.Rows[0]["receiver"].ToString());
                    }
                };

            });
            return true;
        }


        public Boolean LogOut(User user)
        {
            this.cmdExecutor.Push(() =>
            {
                Table userTable = mysqlHelper.OpenTable("users");

                Condition condition = new Condition();
                condition["username"] = user.Username;

                Condition data = new Condition();
                data["is_online"] = 0;

                userTable.Where(condition).Update(data);
            });
            return true;
        }

        public void Exit()
        {
            System.Environment.Exit(0);
        }

    }
}
