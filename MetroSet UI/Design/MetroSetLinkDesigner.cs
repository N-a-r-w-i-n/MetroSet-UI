using MetroSet_UI.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms.Design;

namespace MetroSet_UI.Design
{
    class MetroSetLinkDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionListCollection;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionListCollection == null)
                {
                    actionListCollection = new DesignerActionListCollection();
                    actionListCollection.Add(new MetroSetLinkActionList(Component));
                }

                return actionListCollection;
            }
        }
    }
}
