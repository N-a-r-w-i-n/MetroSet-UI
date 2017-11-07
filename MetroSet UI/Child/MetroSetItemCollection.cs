/**
* MetroSet UI - MetroSet UI Framewrok
* 
* The MIT License (MIT)
* Copyright (c) 2017 Narwin, https://github.com/N-a-r-w-i-n
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy of 
* this software and associated documentation files (the "Software"), to deal in the 
* Software without restriction, including without limitation the rights to use, copy, 
* modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
* and to permit persons to whom the Software is furnished to do so, subject to the 
* following conditions:
* 
* The above copyright notice and this permission notice shall be included in 
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
* INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
* PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
* HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
* CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
* OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace MetroSet_UI.Child
{
    public class MetroSetItemCollection : Collection<object>
    {

        /// <summary>
        /// An event for to determine whether and item or items added or removed.
        /// </summary>
        public event EventHandler ItemUpdated;
        public delegate void EventHandler(object sender, EventArgs e);

        /// <summary>
        /// Adds an array of items to the list of items for a MetroSetListBox.
        /// </summary>
        /// <param name="items">An IEnumerable of objects to add to the list.</param>
        public void AddRange(IEnumerable<object> items)
        {
            foreach (object item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Adds an item to the list of items for a MetroSetListBox.
        /// </summary>
        /// <param name="item">An object representing the item to add to the collection.</param>
        protected new void Add(object item)
        {
            base.Add(item);
            ItemUpdated(this, null);
        }

        /// <summary>
        /// Inserts an item into the list box at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index location where the item is inserted.</param>
        /// <param name="item">An object representing the item to insert.</param>
        protected override void InsertItem(int index, object item)
        {
            base.InsertItem(index, item);
            ItemUpdated(this, null);
        }

        /// <summary>
        /// Removes the specified object from the collection.
        /// </summary>
        /// <param name="value">An object representing the item to remove from the collection.</param>
        protected override void RemoveItem(int value)
        {
            base.RemoveItem(value);
            ItemUpdated(this, null);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        protected new void Clear()
        {
            base.Clear();
            ItemUpdated(this, null);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();
            ItemUpdated(this, null);
        }
    }
}
