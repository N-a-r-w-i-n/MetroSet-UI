using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Windows.Forms.Design;
using MetroSet_UI.Tasks;

namespace MetroSet_UI.Design
{
    class MetroSetLabelDesigner : ControlDesigner
    {

        private DesignerActionListCollection actionListCollection;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionListCollection == null)
                {
                    actionListCollection = new DesignerActionListCollection();
                    actionListCollection.Add(new MetroSetLabelActionList(Component));
                }

                return actionListCollection;
            }
        }

    }
}
