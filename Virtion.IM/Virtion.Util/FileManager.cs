using System;
using System.IO;
using System.Text;
using System.Windows;

namespace Virtion.IM.View
{
    class FileManager
    {
        public static String ReadFile(String path)
        {

            if (File.Exists(path) == false)
            {
                MessageBox.Show("File is not found! " + path);
                return null;
            }

            StreamReader sr = new StreamReader(path, Encoding.Default);
            StringBuilder stringBuilder = new StringBuilder();
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                //Console.WriteLine(line.ToString());
                stringBuilder.Append(line);
            }

            return stringBuilder.ToString();
        }

        public static void WriteFile(String path, String data)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(data);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

    }
}

