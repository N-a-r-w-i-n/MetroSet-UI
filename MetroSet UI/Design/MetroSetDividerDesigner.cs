using MetroSet_UI.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms.Design;

namespace MetroSet_UI.Design
{
    class MetroSetDividerDesigner : ControlDesigner
    {

        private readonly string[] _PropertiesToRemove =
{
            "BackgroundImage", "BackgroundImageLayout",
            "RightToLeft","ImeMode"
        };


        protected override void PostFilterProperties(System.Collections.IDictionary properties)
        {
            foreach (var property in _PropertiesToRemove)
            {
                properties.Remove(property);
            }
            base.PostFilterProperties(properties);
        }


        private DesignerActionListCollection actionListCollection;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionListCollection == null)
                {
                    actionListCollection = new DesignerActionListCollection();
                    actionListCollection.Add(new MetroSetDividerActionList(Component));
                }

                return actionListCollection;
            }
        }
    }
}
