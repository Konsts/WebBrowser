using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DirectoryLibary.Model
{
    public enum ItemType
    {
        Directory,
        File,
        Disk
    }
    public class DirectoryModel
    {
        public string Path { get; set; }
        public ItemType Type { get; set; }
    }
}
