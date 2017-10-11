using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Rediscachedemo.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public HomeController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        [HttpGet]
        public  IActionResult Index()
        {
            string msg = string.Empty;
            var cacheKey = "storedTime";
            var existingTime = _distributedCache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(existingTime))
            {
                msg = $"Fetched from cache :{existingTime}";
            }
            else
            {
                existingTime = DateTime.UtcNow.ToString();
                _distributedCache.SetString(cacheKey, existingTime);
                msg = $"Stored data in cache :{existingTime}";
            }
            ViewBag.msg = msg;
            return View();
        }
    }
}
