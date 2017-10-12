using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Rediscachedemo.Controllers
{
    public class RedisAzureController : Controller
    {
        private readonly IDistributedCache _cache;

        public RedisAzureController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            string value = _cache.GetString("time");
            if (string.IsNullOrEmpty(value))
            {
                value = DateTime.Now.ToString();
                var option = new DistributedCacheEntryOptions();
                option.SetSlidingExpiration(TimeSpan.FromMinutes(1));
                _cache.SetString("time", value);
            }
            ViewData["CachedTime"] = value;
            ViewData["CurrentTime"] = DateTime.Now.ToString();
            return View();
        }
    }
}
