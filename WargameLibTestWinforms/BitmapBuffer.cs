using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WargameLibTestWinforms
{
    public class BitmapBuffer
    {
        Dictionary<string, Bitmap> _itemsDict = new Dictionary<string, Bitmap>();
        Queue<string> _items = new Queue<string>();

        int _capacity;

        public BitmapBuffer(int capacity)
        {
            if (capacity < 1) throw new ArgumentException("capacity must at least be 1");
            _capacity = capacity;
        }

        public void Clear()
        {
            _items.Clear();
            foreach (var pair in _itemsDict)
            {
                pair.Value.Dispose();
            }
            _itemsDict.Clear();
        }

        public bool Contains(string name)
        {
            return _itemsDict.ContainsKey(name);
        }

        public void Push(string name, Bitmap bitmap)
        {
            if (_itemsDict.ContainsKey(name)) return;
            while (_items.Count > _capacity)
            {
                Debug.WriteLine("Bitmap Buffer full!");
                var n = _items.Dequeue();
                _itemsDict[n].Dispose();
                _itemsDict.Remove(n);
            }
            _items.Enqueue(name);
            _itemsDict[name] = bitmap;
        }

        public Bitmap Get(string name)
        {
            if (_itemsDict.TryGetValue(name, out var bitmap))
                return bitmap;
            return null;
        }

    }
}
