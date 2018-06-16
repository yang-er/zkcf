using System.Runtime.Serialization;

namespace zkcf.Statistics
{

    [DataContract]
    class Nature
    {
        [DataMember]
        public string id;
        [DataMember]
        public string stuAD;
        [DataMember]
        public string stuKH;
        [DataMember]
        public string stuName;
        [DataMember]
        public string sex;
        [DataMember]
        public string score;
        [DataMember]
        public string question;
        [DataMember]
        public string queue;
        [DataMember]
        public string school;

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", id, stuAD, stuKH, stuName, sex, score, question, queue, school);
        }

        public static string ToSummary()
        {
            return "序号,准考号,考号,姓名,性别,分数,实验内容,场次,学校";
        }

    }

}
