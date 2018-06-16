using System.Runtime.Serialization;

namespace zkcf.Statistics
{

    [DataContract]
    class Sports
    {
        [DataMember]
        public string id;
        [DataMember]
        public string name;
        [DataMember]
        public string sex;

        [DataMember]
        public string item1;
        [DataMember]
        public string item2;
        [DataMember]
        public string item3;
        [DataMember]
        public string item4;
        [DataMember]
        public string item5;
        [DataMember]
        public string item6;

        [DataMember]
        public string item1score;
        [DataMember]
        public string item2score;
        [DataMember]
        public string item3score;
        [DataMember]
        public string item4score;
        [DataMember]
        public string item5score;
        [DataMember]
        public string item6score;

        [DataMember]
        public string result1;
        [DataMember]
        public string result2;
        [DataMember]
        public string result3;
        [DataMember]
        public string result4;
        [DataMember]
        public string result5;
        [DataMember]
        public string result6;

        [DataMember]
        public string total;
        [DataMember]
        public string school;

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", id, name, sex, item1score, item2score, item3score, item4score, item5score, item6score, result1, result2, result3, result4, result5, result6, total, school);
        }

        public static string ToSummary()
        {
            return "准考证号,性别,姓名,项目1,项目2,项目3,项目4,项目5,项目6,分数1,分数2,分数3,分数4,分数5,分数6,总分,学校";
        }
    }

}
