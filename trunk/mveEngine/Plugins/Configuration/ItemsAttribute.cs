using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mveEngine
{
    [global::System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ItemsAttribute : Attribute
    {
        private string _items;

        public string Items { get { return _items; } }

        public ItemsAttribute(string Items)
        {
            this._items = Items;
        }
    }
}