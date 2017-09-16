using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Resources;

namespace Virtion.IM.View
{
    public class NotifyTray
    {
        private NotifyIcon notifyIcon;
        private Icon blank;
        private Icon striped;
        private bool blink = false;
        private System.Timers.Timer blinkTiemr;
        private TrayMenu trayMenu;

        public NotifyTray()
        {
            this.notifyIcon = new NotifyIcon();
            this.trayMenu = new TrayMenu(this);
            this.notifyIcon.MouseClick += notifyIcon_MouseClick;
        }


        public void SetMenu(String name, EventHandler e)
        {
            MenuItem item = new MenuItem(name);
            item.Click += e;
            if (notifyIcon.ContextMenu == null)
            {
                MenuItem[] childen = new MenuItem[] { item };
                notifyIcon.ContextMenu = new ContextMenu(childen);
            }
            else
            {
                notifyIcon.ContextMenu.MenuItems.Add(item);
            }
        }

        public void InitialTray()
        {
            this.striped = Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.notifyIcon.Icon = this.striped;

            Uri uri = new Uri("/Resource/blank.ico", UriKind.Relative);
            StreamResourceInfo info = System.Windows.Application.GetResourceStream(uri);
            Stream s = info.Stream;

            this.blank = new Icon(s);
            //new System.Drawing.Icon();
            this.notifyIcon.Visible = true;

            this.blinkTiemr = new System.Timers.Timer(500);//设置时间
            blinkTiemr.Elapsed += new System.Timers.ElapsedEventHandler(blinkTiemr_Tick);
            blinkTiemr.AutoReset = true;
        }

        public void Dispose()
        {
            this.notifyIcon.Visible = false;
        }

        public void StartTwinkleIcon()
        {
            blinkTiemr.Enabled = true;
        }

        public void StopTwinkleIcon()
        {
            blinkTiemr.Enabled = false;
        }

        private void blinkTiemr_Tick(object sender, EventArgs e)
        {
            if (!blink)
            {
                this.notifyIcon.Icon = striped;
            }
            else
            {
                this.notifyIcon.Icon = blank;
            }
            blink = !blink;
        }

        void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //if (MainWindow.friendListWindow != null)
            //{
            //    MainWindow.friendListWindow.Activate();
            //}
        }

    }

}
