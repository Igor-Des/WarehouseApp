using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using WarehouseApp.Data;
using WarehouseApp.Models;
using System.Linq;

namespace WarehouseApp.Services
{
    public class CachedComponent : ICached<Component>
    {
        private readonly WarehouseContext _context;
        private readonly IMemoryCache _memoryCache;
        public CachedComponent(WarehouseContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public void AddList(string key)
        {
            IEnumerable<Component> components = _context.Components.ToList();
            if (components != null)
            {
                _memoryCache.Set(key, components, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                });
            }
        }

        public IEnumerable<Component> GetList(string key)
        {
            IEnumerable<Component> components;
            if (!_memoryCache.TryGetValue(key, out components))
            {
                components = _context.Components.ToList();
                if (components != null)
                {
                    _memoryCache.Set(key, components, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60)));
                }
            }
            return components;
        }

        public IEnumerable<Component> GetList()
        {
            {
                return _context.Components.ToList();
            }
        }
    }
}
