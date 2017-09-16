using System;
using System.Diagnostics;

namespace Virtion.IM.View.Virtion.Util
{
    public class VDebug
    {

        public static void Error(object value)
        {
#if (DEBUG)
            Console.Out.WriteLine("===============ERROR===================");
            Console.Out.Write((new StackTrace()).GetFrame(1).GetMethod().ToString() + " :");
            Console.Out.WriteLine(value);
            Console.Out.WriteLine("======================================");
#else
            
#endif
        }

        public static void Track(object value)
        {
#if (DEBUG)
            Console.Out.Write((new StackTrace()).GetFrame(1).GetMethod().ToString() + " :");
            Console.Out.WriteLine(value);
#else
            
#endif
        }

        static void Printf(object value,__arglist)
        {
            Console.Out.Write((new StackTrace()).GetFrame(0).GetMethod());
            Console.Out.WriteLine(value);
        }

    }
}
