using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zkcf.Parallel
{
    struct ThreadData
    {
        public List<short> IdRange;
        public TestInfo TestType;
        public StudentInfo StudentPool;
        public byte ThreadId;
    }

}
