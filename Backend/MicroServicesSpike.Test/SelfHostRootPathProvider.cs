using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace MicroServicesSpike.Test
{
    public class SelfHostRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return StaticConfiguration.IsRunningDebug
                ? Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", ".."))
                : AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}

