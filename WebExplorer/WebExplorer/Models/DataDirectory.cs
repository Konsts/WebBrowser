using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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