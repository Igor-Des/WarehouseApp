using System.Collections.Generic;

namespace WarehouseApp.Services
{
    public interface ICached<T>
    {
        public IEnumerable<T> GetList();
        public void AddList(string key);
        public IEnumerable<T> GetList(string key);
    }
}
