using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Rediscachedemo.Controllers
{
    public class MemoryDistributedController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public MemoryDistributedController(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }
        public IActionResult Index()
        {
            string cacheKey = "time";
            string msg = string.Empty;
            var existingTime = _distributedCache.GetString(cacheKey);
            if(!string.IsNullOrEmpty(existingTime))
            {
                msg = $"Time fetched from distributed memory cache :{existingTime}";
            }
            else
            {
                existingTime = DateTime.Now.ToString();
                var option = new DistributedCacheEntryOptions()
                { 
                    AbsoluteExpirationRelativeToNow = (DateTime.Now.AddMinutes(1) - DateTime.Now)
                };
                _distributedCache.SetString(cacheKey, existingTime,option);
                msg = "Time set in distributed memory cache";
            }

            ViewBag.msg = msg;
            return View();
        }
    }
}
