using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebExplorer.Models;
using DL = DirectoryLibary;

namespace WebExplorer.Controllers
{
    public class DirectoryController : ApiController
    {
        public IHttpActionResult GetDirectories([FromUri] string path)
        {
            if (!System.IO.Directory.Exists(path))
                return BadRequest("Not exist path");
            var dir = new DirectoryInfo(path);
            string parentPath;
            try
            {
                parentPath=dir.Parent.FullName.ToString();
            }
            catch
            {
                parentPath = "";
            }
            return Ok(new DataDirectory
            {
                Parent = parentPath,
                CurrentPath = path,
                DirectoryModels = DL.WorkWithDirectories.GetListSubdirectoriesAndFiles(path)
            });
        }

        public IHttpActionResult GetStatistic([FromUri] string path)
        {
            if (!Directory.Exists(path))
                return BadRequest("Not exist path");
            if (path != null)
                return Ok(DL.WorkWithDirectories.GetStatistics(path));
            return StatusCode(HttpStatusCode.BadRequest);
        }

        public IHttpActionResult GetDrives()
        {
            return Ok(new DataDirectory
            {
                Parent = "none",
                CurrentPath = "All Disk",
                DirectoryModels = DL.WorkWithDirectories.GetAllDrives()
            });
        }
    }
}
