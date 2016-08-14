using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryLibary.Model
{
    public class Statistic
    {
        public int Less10Mb { get; set; }
        public int More10MbAndLess50Mb { get; set; }
        public int More100Mb { get; set; }
        public int Errors { get; set; }
    }
}
