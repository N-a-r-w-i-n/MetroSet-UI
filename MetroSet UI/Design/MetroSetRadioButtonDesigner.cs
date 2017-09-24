using MetroSet_UI.Tasks;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace MetroSet_UI.Design
{
    internal class MetroSetRadioButtonDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionListCollection;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionListCollection == null)
                {
                    actionListCollection = new DesignerActionListCollection();
                    actionListCollection.Add(new MetroSetRadioButtonActionList(Component));
                }

                return actionListCollection;
            }
        }
    }
}