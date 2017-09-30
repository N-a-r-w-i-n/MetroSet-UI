using System;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace MetroSet_UI.Child
{
    internal class MetroSetTabPageCollectionEditor : CollectionEditor
    {
        public MetroSetTabPageCollectionEditor(Type type)
            : base(type)
        { }

        protected override Type[] CreateNewItemTypes()
        {
            return new[] { typeof(TabPage), typeof(MetroSetTabPage) };
        }
    }
}