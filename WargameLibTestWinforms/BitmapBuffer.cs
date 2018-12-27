using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WargameLibTestWinforms
{
    public static class BitmapBuffer
    {
        static Dictionary<string, Bitmap> _itemsDict = new Dictionary<string, Bitmap>();
        static Queue<string> _items = new Queue<string>();

        public static void Clear()
        {
            _items.Clear();
            foreach (var pair in _itemsDict)
            {
                pair.Value.Dispose();
            }
            _itemsDict.Clear();
        }

        public static bool Contains(string name)
        {
            return _itemsDict.ContainsKey(name);
        }

        public static void Push(string name, Bitmap bitmap)
        {
            if (_itemsDict.ContainsKey(name)) return;
            while (_items.Count > 300)
            {
                Debug.WriteLine("Bitmap Buffer full!");
                var n = _items.Dequeue();
                _itemsDict[n].Dispose();
                _itemsDict.Remove(n);
            }
            _items.Enqueue(name);
            _itemsDict[name] = bitmap;
        }

        public static Bitmap Get(string name)
        {
            if (_itemsDict.TryGetValue(name, out var bitmap))
                return bitmap;
            return null;
        }

    }
}
