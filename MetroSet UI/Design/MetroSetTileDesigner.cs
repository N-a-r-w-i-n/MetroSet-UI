using MetroSet_UI.Tasks;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace MetroSet_UI.Design
{
    internal class MetroSetTileDesigner : ControlDesigner
    {

        private readonly string[] _PropertiesToRemove =
        {
             "BackgroundImageLayout", "ForeColor",
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
                    actionListCollection.Add(new MetroSetTileActionList(Component));
                }

                return actionListCollection;
            }
        }
    }
}