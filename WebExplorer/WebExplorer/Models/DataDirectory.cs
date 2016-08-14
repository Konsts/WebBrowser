using System.Collections.Generic;
using  DirectoryLibary.Model;

namespace WebExplorer.Models
{
    public class DataDirectory
    {
        public string Parent { get; set; }
        public string CurrentPath { get; set; }
        public List<DirectoryModel> DirectoryModels { get; set; }
    }
}