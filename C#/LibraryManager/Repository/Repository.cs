using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.Repository;

namespace LibraryManager.Repository
{
    public class Repository<T> where T: IIdentifiable
    {
        private List<T> _items = new List<T>();

        public void AddItem(T item)
        {
            _items.Add(item);
        }

        public void RemoveItem(T item)
        {
            _items.Remove(item);
        }

        public List<T> GetAll()
        {
            return _items;
        }

        public T FindById(string Id)
        {
            return _items.FirstOrDefault(item => item.Id == Id) ?? throw new KeyNotFoundException($"Item with ID {Id} was not found.");
        }
    }
}
