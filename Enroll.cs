using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace zkcf.Statistics
{

    [DataContract]
    class Enroll
    {
        [DataMember]
        public string id;
        [DataMember]
        public string zw;
        [DataMember]
        public string name;
        [DataMember]
        public string gender;
        [DataMember]
        public string learned;
        [DataMember]
        public string score;
        [DataMember]
        public string rank;

        [DataMember]
        public string wish1;
        [DataMember]
        public string wish2;
        [DataMember]
        public string wish3;
        [DataMember]
        public string wish4;
        [DataMember]
        public string wish5;

        [DataMember]
        public string school;
        [DataMember]
        public string enroll;
        [DataMember]
        public string logger;
        
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", id, zw, name, gender, learned, score, rank, wish1, wish2, wish3, wish4, wish5, school, enroll, logger);
        }

        public static string ToSummary()
        {
            return "准考证号,座位号,姓名,性别,届别,成绩,等第,志愿一,志愿二,志愿三,志愿四,志愿五,毕业学校,录取结果,录取日志";
        }

    }

}
