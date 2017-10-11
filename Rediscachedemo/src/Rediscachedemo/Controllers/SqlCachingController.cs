using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Rediscachedemo.Controllers
{
    public class SqlCachingController : Controller
    {
        private readonly IDistributedCache _memoryCache;
        public SqlCachingController(IDistributedCache distributedCache)
        {
            _memoryCache = distributedCache;
        }

        
        public IActionResult Index()
        {
            var Time = DateTime.Now.ToLocalTime().ToString();
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddYears(1)
            };
            _memoryCache.Set("Time", Encoding.UTF8.GetBytes(Time), cacheOptions);
            return View();
        }

        public IActionResult GetCacheData()
        {
            string Time = string.Empty;
            Time = Encoding.UTF8.GetString(_memoryCache.Get("Time"));
            ViewBag.data = Time;
            return View();
        }

        public bool RemoveCacheData()
        {
            _memoryCache.Remove("Time");
            return true;
        }
    }
}
