using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Rediscachedemo.Controllers
{
    public class MemoryCacheController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string cacheKey = "time";
            string msg = string.Empty;
            DateTime existingTime;
            if(_memoryCache.TryGetValue(cacheKey, out existingTime))
            {
                msg = $"Time fetched from memory cache :{existingTime.ToString()}";
            }
            else
            {
                existingTime = DateTime.Now;
                var option = new MemoryCacheEntryOptions();
                option.AbsoluteExpirationRelativeToNow = (DateTime.Now.AddMinutes(1) - DateTime.Now);
                _memoryCache.Set(cacheKey, existingTime, option);
                msg = "Time saved in cache ";
            }

            ViewBag.msg = msg;
            return View();
        }
    }
}
