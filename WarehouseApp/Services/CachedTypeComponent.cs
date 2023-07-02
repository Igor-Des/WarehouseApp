using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using WarehouseApp.Data;
using WarehouseApp.Models;
using System.Linq;

namespace WarehouseApp.Services
{
    public class CachedTypeComponent : ICached<TypeComponent>
    {
        private readonly WarehouseContext _context;
        private readonly IMemoryCache _memoryCache;
        public CachedTypeComponent(WarehouseContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public void AddList(string key)
        {
            IEnumerable<TypeComponent> typeComponents = _context.TypeComponents.ToList();
            if (typeComponents != null)
            {
                _memoryCache.Set(key, typeComponents, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                });
            }
        }

        public IEnumerable<TypeComponent> GetList(string key)
        {
            IEnumerable<TypeComponent> typeComponents;
            if (!_memoryCache.TryGetValue(key, out typeComponents))
            {
                typeComponents = _context.TypeComponents.ToList();
                if (typeComponents != null)
                {
                    _memoryCache.Set(key, typeComponents, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60)));
                }
            }
            return typeComponents;
        }

        public IEnumerable<TypeComponent> GetList()
        {
            {
                return _context.TypeComponents.ToList();
            }
        }
    }
}
