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
        public event EventHandler ItemUpdated;
        public delegate void EventHandler(object sender, EventArgs e);

        public void AddRange(IEnumerable<object> items)
        {
            foreach (object item in items)
            {
                Add(item);
            }
        }

        protected new void Add(object item)
        {
            base.Add(item);
            ItemUpdated(this, null);
        }

        protected override void InsertItem(int index, object item)
        {
            base.InsertItem(index, item);
            ItemUpdated(this, null);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            ItemUpdated(this, null);
        }

        protected new void Clear()
        {
            base.Clear();
            ItemUpdated(this, null);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            ItemUpdated(this, null);
        }
    }
}
