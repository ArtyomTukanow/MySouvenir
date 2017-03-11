using System;
using System.Collections.Generic;
using Items;

namespace Managers
{
    public class TestManager
    {
        public static TestManager instance = new TestManager();

        private TestManager()
        {
        }

        private List<string> _tags = new List<string>();

        public void AddTag(string tag)
        {
            if(_tags == null)
                _tags = new List<string>();
            if(!_tags.Contains(tag))
                _tags.Add(tag);
        }

        public List<string> GetTags()
        {
            return _tags;
        }

        public void LoadProducts(Action<Products> callback)
        {
            ApplicationManager.ProductManager.ProductsLoad(callback, _tags.ToArray());
        }
    }
}