using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Repository
{
    public class Repository<T> where T: IIdentifiable
    {
        private List<T> _items = new List<T>();
        private readonly string _filePath;
        private readonly JsonStorage _storage;

        public Repository(string filepath) { 
            _filePath = filepath;
            _storage = new JsonStorage();
        }
        

        public async Task AddItem(T item)
        {
            _items = await _storage.GetAllAsync<T>(_filePath);
            _items.Add(item);
            await _storage.SaveAllAsync(_items, _filePath);
            Console.WriteLine("Added Successfully");
        }

        public async Task RemoveItem(T item)
        {
            _items = await _storage.GetAllAsync<T>(_filePath);
            _items.RemoveAll(el => el.Id == item.Id);
            await _storage.SaveAllAsync(_items, _filePath);
            Console.WriteLine("Removed Successfully");
        }

        public async Task<List<T>> GetAll()
        {
            _items = await _storage.GetAllAsync<T>(_filePath);
            return _items;
        }

        public async Task<T> FindById(string Id)
        {
            _items = await _storage.GetAllAsync<T>(_filePath);
            return _items.FirstOrDefault(item => item.Id == Id) ?? throw new KeyNotFoundException($"Item with ID {Id} was not found.");
        }
        public async Task UpdateItem(T item)
        {
            _items = await _storage.GetAllAsync<T>(_filePath);
            var index = _items.FindIndex(el => el.Id == item.Id);
            if (index != -1)
            {
                _items[index] = item;
                await _storage.SaveAllAsync(_items, _filePath);
            }
        }
    }
}
