using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BomoBana
{
    public class RoutingStatic
    {
        private readonly IWebHostEnvironment _env;

        public RoutingStatic(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string RouteFile(string route)
        {
            var path = Path.Combine(_env.ContentRootPath, route);
            return path;
        }
    }

}
