using System.Collections.Generic;

namespace zkcf.Parallel
{
    class StudentInfo
    {
        public string Name;
        string Source;
        public long Id;
        public short IdLength;
        public long Zw;
        public short ZwLength;
        public List<short> ZwCollection;

        internal static StudentInfo Create(string source, string name, long id, short idlen, long zw = 0, short zwlen = 0)
        {
            StudentInfo ti = new StudentInfo();
            ti.Source = source;
            ti.Name = name;
            ti.Id = id;
            ti.IdLength = idlen;
            ti.Zw = zw;
            ti.ZwLength = zwlen;
            if (zwlen != 0)
            {
                ti.ZwCollection = new List<short>();
                for (short i = 1; i <= zwlen; i++)
                {
                    for (byte j = 1; j <= 30; j++)
                    {
                        ti.ZwCollection.Add((short)(i * 100 + j));
                    }
                }
            }
            return ti;
        }
        
        public void Calculate(TestInfo entry, List<short> idRange)
        {
            foreach (short i in idRange)
            {
                long stuId = this.Id + i;
                if (!entry.NeedZw)
                {
                    entry.Download(stuId);
                }
                else
                {
                    for (int p = 0; p < this.ZwCollection.Count; p++)
                    {
                        short zw = this.ZwCollection[p];
                        long stuZw = this.Zw + zw;
                        if (entry.Download(stuId, stuZw))
                        {
                            this.ZwCollection.Remove(zw);
                            break;
                        }
                    }
                }
            }
        }
    }
}
