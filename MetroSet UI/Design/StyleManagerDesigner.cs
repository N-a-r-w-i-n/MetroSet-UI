using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using MetroSet_UI.Tasks;

namespace MetroSet_UI.Design
{
    class StyleManagerDesigner : ComponentDesigner
    {
        private DesignerActionListCollection actionListCollection;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionListCollection == null)
                {
                    actionListCollection = new DesignerActionListCollection();
                    actionListCollection.Add(new StyleManagerTask(Component));
                }

                return actionListCollection;
            }
        }

    }

}
