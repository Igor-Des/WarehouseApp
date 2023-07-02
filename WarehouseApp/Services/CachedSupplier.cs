using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using WarehouseApp.Models;
using WarehouseApp.Data;
using System.Linq;

namespace WarehouseApp.Services
{
    public class CachedSupplier : ICached<Supplier>
    {
        private readonly WarehouseContext _context;
        private readonly IMemoryCache _memoryCache;
        public CachedSupplier(WarehouseContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public void AddList(string key)
        {
            IEnumerable<Supplier> suppliers = _context.Suppliers.ToList();
            if (suppliers != null)
            {
                _memoryCache.Set(key, suppliers, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(258)
                });
            }
        }

        public IEnumerable<Supplier> GetList(string key)
        {
            IEnumerable<Supplier> suppliers;
            if (!_memoryCache.TryGetValue(key, out suppliers))
            {
                suppliers = _context.Suppliers.ToList();
                if (suppliers != null)
                {
                    _memoryCache.Set(key, suppliers, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(258)));
                }
            }
            return suppliers;
        }

        public IEnumerable<Supplier> GetList()
        {
            {
                return _context.Suppliers.ToList();
            }
        }
    }
}
