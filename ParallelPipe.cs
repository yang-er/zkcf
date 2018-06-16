using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using zkcf.Statistics;

namespace zkcf.Parallel
{
    class ParallelPipe
    {
        static List<Thread> hThreads;
        static TestInfo ti;
        static string test;
        static StudentInfo si;
        static string stu;

        static void ParallelCalculate(object obj)
        {
            ThreadData param = (ThreadData)obj;
            Console.WriteLine("{0}时，#{1}线程进入！", DateTime.Now.ToString(), param.ThreadId);
            param.StudentPool.Calculate(param.TestType, param.IdRange);
            Console.WriteLine("{0}时，#{1}线程退出！", DateTime.Now.ToString(), param.ThreadId);
        }

        static void EnrollGet()
        {

            foreach (string var in Directory.GetFiles(Program.CommonFolder + "\\scores", (si.Id / 10000).ToString() + "*", SearchOption.TopDirectoryOnly))
            {
                Scores current = StatisticsPipe.JsonComplete<Scores>(var);
                ti.Download(long.Parse(current.id), long.Parse(current.zw));
            }

            Console.WriteLine("下载完成，按任意键退出。");
            Console.ReadKey();
        }

        static void ScoresGet()
        {

            List<short> stuId = new List<short>();
            for (short i = 1; i <= si.IdLength; i++)
            {
                stuId.Add(i);
            }

            foreach (string var in Directory.GetFiles(Program.CommonFolder + "\\" + test, (si.Id / 10000).ToString() + "*", SearchOption.TopDirectoryOnly))
            {
                Scores current = StatisticsPipe.JsonComplete<Scores>(var);
                stuId.Remove((short)(long.Parse(current.id) - si.Id));
                if(ti.NeedZw) si.ZwCollection.Remove((short)(long.Parse(current.zw) - si.Zw));
            }

            Console.Write("输入测试所用线程数：");
            byte ThreadNumber = Convert.ToByte(Console.ReadLine());
            hThreads = new List<Thread>(ThreadNumber);
            short Count = (short)(stuId.Count / ThreadNumber);

            for (int i = 0; i < ThreadNumber; i++)
                hThreads.Add(new Thread(new ParameterizedThreadStart(ParallelCalculate)));

            Console.WriteLine("退出请关闭窗口！");

            for (byte i = 0; i < ThreadNumber; i++)
            {
                ThreadData td = new ThreadData();
                td.IdRange = stuId.GetRange(i * Count, Count + (i == ThreadNumber ? stuId.Count % ThreadNumber : 0));
                td.StudentPool = si;
                td.TestType = ti;
                td.ThreadId = i;
                hThreads[i].Start(td);
            }

            while (true) Console.ReadKey();

        }

        internal static void ParallelStart()
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
                return;
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
                return;
            }

            if (test == "enroll")
                EnrollGet();
            else
                ScoresGet();

        }

    }

}
