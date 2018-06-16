using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using zkcf.Parallel;

namespace zkcf.Statistics
{
    class StatisticsPipe
    {

        static StringBuilder sb = new StringBuilder();
        static DataContractJsonSerializer json;
        static TestInfo ti;
        static string test;
        static StudentInfo si;
        static string stu;

        internal static T JsonComplete<T>(string fileName)
        {
            if (json == null) json = new DataContractJsonSerializer(typeof(T));
            Stream sr = File.OpenRead(fileName);
            T obj = (T)json.ReadObject(sr);
            sr.Close();
            return obj;
        }

        static void JsonToString<T>(string fileName)
        {
            T obj = JsonComplete<T>(fileName);
            sb.AppendLine(obj.ToString());
        }

        static bool LoadPool()
        {

            Console.Write("输入考试类型（{0}）：", string.Join("，", Program.TestPool.Keys));
            test = Console.ReadLine();
            if (Program.TestPool.ContainsKey(test))
            {
                ti = Program.TestPool[test];
            }
            else
            {
                Console.WriteLine("考试加载失败。按任意键退出。");
                Console.ReadKey();
                return false;
            }

            Console.Write("输入考生区段（{0}）：", string.Join("，", Program.StudentPool.Keys));
            stu = Console.ReadLine();
            if (Program.StudentPool.ContainsKey(stu))
            {
                si = Program.StudentPool[stu];
            }
            else
            {
                Console.WriteLine("考生区加载失败。按任意键退出。");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        internal static void ManageFiles()
        {
            if (!LoadPool()) return;

            if (sb.Length == 0)
            {
                if (test == "nature") sb.AppendLine(Nature.ToSummary());
                if (test == "sports") sb.AppendLine(Sports.ToSummary());
                if (test == "scores") sb.AppendLine(Scores.ToSummary());
                if (test == "localhost") sb.AppendLine(Scores.ToSummary());
                if (test == "enroll") sb.AppendLine(Enroll.ToSummary());
            }

            foreach (string var in Directory.GetFiles(Program.CommonFolder + "\\" + test, (si.Id / 10000).ToString() + "*", SearchOption.TopDirectoryOnly))
            {
                if (test == "nature") JsonToString<Nature>(var);
                if (test == "sports") JsonToString<Sports>(var);
                if (test == "scores") JsonToString<Scores>(var);
                if (test == "localhost") JsonToString<Scores>(var);
                if (test == "enroll") JsonToString<Enroll>(var);
            }

            Console.WriteLine("已处理完成。正在写入……");
            File.WriteAllText(Program.CommonFolder + "\\" + test + "_" + stu + ".csv", sb.ToString());
            Console.WriteLine("写入完成。");
            Console.WriteLine("请使用Notepad++翻译为GB2312编码。");
            Console.WriteLine("按任意键退出。");
            Console.ReadKey();
        }
    }

}
