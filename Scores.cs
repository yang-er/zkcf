using System.Runtime.Serialization;

namespace zkcf.Statistics
{

    [DataContract]
    class Scores
    {
        [DataMember]
        public string id;
        [DataMember]
        public string zw;
        [DataMember]
        public string name;
        [DataMember]
        public decimal cn;
        [DataMember]
        public decimal mt;
        [DataMember]
        public decimal en;
        [DataMember]
        public decimal pl;
        [DataMember]
        public decimal hs;
        [DataMember]
        public decimal ps;
        [DataMember]
        public decimal cm;
        [DataMember]
        public decimal sp;
        [DataMember]
        public decimal ep;
        [DataMember]
        public decimal ca;
        [DataMember]
        public decimal to;
        [DataMember] 
        public short rn;

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", id, zw, name.Trim(), cn, mt, en, pl, hs, ps, cm, sp, ep, ca, to, rn);
        }

        public static string ToSummary()
        {
            return "准考证号,座位号,姓名,语文,数学,英语,政治,历史,物理,化学,体育,实验,政策加分,总分,排名";
        }
    }

}
