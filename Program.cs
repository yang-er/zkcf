using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using zkcf.Parallel;
using zkcf.Statistics;

namespace zkcf
{
    class Program
    {
        internal static XmlNode main;
        internal static Dictionary<string, TestInfo> TestPool = new Dictionary<string, TestInfo>();
        internal static Dictionary<string, StudentInfo> StudentPool = new Dictionary<string, StudentInfo>();
        internal static string CommonFolder;

        static void InitializeComponent()
        {
            Console.Title = "中考查分";
            XmlDocument zkcf = new XmlDocument();
            zkcf.Load(".\\zkcf.xml");
            Console.WriteLine("配置文件已加载。");
            main = zkcf.FirstChild;

            CommonFolder = main.ChildNodes[0].ChildNodes[0].Attributes.GetNamedItem("value").InnerText;

            foreach (XmlNode si in main.ChildNodes[1])
                StudentPool.Add(si.Attributes.GetNamedItem("Source").InnerText, StudentInfo.Create(si.Attributes.GetNamedItem("Source").InnerText, si.Attributes.GetNamedItem("Name").InnerText, Int64.Parse(si.Attributes.GetNamedItem("Id").InnerText), Int16.Parse(si.Attributes.GetNamedItem("IdLength").InnerText), Int64.Parse(si.Attributes.GetNamedItem("Zw").InnerText), Int16.Parse(si.Attributes.GetNamedItem("ZwLength").InnerText)));
            Console.WriteLine("学生池已加载。");

            foreach (XmlNode ti in main.ChildNodes[2])
                TestPool.Add(ti.Attributes.GetNamedItem("Source").InnerText, TestInfo.Create(ti.Attributes.GetNamedItem("Source").InnerText, ti.Attributes.GetNamedItem("Name").InnerText, ti.Attributes.GetNamedItem("Url").InnerText, ti.Attributes.GetNamedItem("Pattern").InnerText, ti.Attributes.GetNamedItem("NeedZw").InnerText == "1"));
            Console.WriteLine("考试池已加载。");

            List<string> Folders = new List<string>();

            foreach (string p in TestPool.Keys)
            {
                Folders.Add(CommonFolder + "\\" + p);
            }

            foreach (string f in Folders)
            {
                if (!Directory.Exists(f))
                {
                    Directory.CreateDirectory(f);
                    Console.WriteLine("目录" + f + "已创建。");
                }
            }
            Console.WriteLine("文件夹池已加载。");

            Console.WriteLine();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            InitializeComponent();

            Console.WriteLine("欢迎使用中考查分软件。");
            Console.WriteLine("本程序自动读取当前目录下的zkcf.xml，请勿不懂就修改。");
            Console.WriteLine("本程序分为A——数据下载模块和B——数据整合模块。");
            Console.WriteLine();
            Console.WriteLine("可用考试类型：");
            foreach (KeyValuePair<string, TestInfo> ti in TestPool)
                Console.WriteLine("{0}：{1}", ti.Key, ti.Value.Name);
            Console.WriteLine("可用学生区域：");
            foreach (KeyValuePair<string, StudentInfo> si in StudentPool)
                Console.WriteLine("{0}：{1}", si.Key, si.Value.Name);
            Console.WriteLine();
            Console.Write("您的操作类型为（A/B）");
            string type = Console.ReadLine().Trim().ToLower();
            if (type == "a")
                ParallelPipe.ParallelStart();
            else if (type == "b")
                StatisticsPipe.ManageFiles();
        }
        
    }
}
