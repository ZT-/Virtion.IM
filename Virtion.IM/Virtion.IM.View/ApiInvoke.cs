using System.Runtime.InteropServices;

namespace Virtion.IM.View
{
    public struct POINT
    {
        public int X;
        public int Y;
        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    public class ApiInvoke
    {
        /// <summary>   
        /// 获取鼠标的坐标   
        /// </summary>   
        /// <param name="pt">传址参数，坐标point类型</param>
        /// <returns>获取成功返回真</returns>   
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        /// <summary>   
        /// 设置鼠标的坐标   
        /// </summary>   
        /// <param name="x">横坐标</param>   
        /// <param name="y">纵坐标</param>   
        [DllImport("User32")]
        public static extern void SetCursorPos(int x, int y);

    }
}
