using System.Drawing;
using TrainingLoad.Data;
using ZoneFiveSoftware.Common.Visuals;

namespace TrainingLoad.UI.View
{
    class ActivityTreeRenderer : TreeList.DefaultRowDataRenderer
    {
        public ActivityTreeRenderer(TreeList tree)
            : base(tree)
        {

        }

        protected override FontStyle GetCellFontStyle(object element, TreeList.Column column)
        {
            FontStyle style = base.GetCellFontStyle(element, column);

            TrimpActivity activity = element as TrimpActivity;
            if (activity != null)
            {
                if (activity.Score == 0 || activity.Score == float.NaN)
                {
                    style = FontStyle.Italic;
                }
            }

            return style;
        }

        protected override void DrawCell(Graphics graphics, TreeList.DrawItemState rowState, object element, TreeList.Column column, Rectangle cellRect)
        {
            TrimpActivity activity = element as TrimpActivity;

            if (activity != null)
            {
                // Gets custom formatted text
                string text = activity.GetFormattedText(column.Id);

                // Draw String if custom text defined
                if (text != null)
                {
                    graphics.DrawString(text,
                                    base.Font(GetCellFontStyle(element, column)),
                                    new SolidBrush(PluginMain.GetApplication().VisualTheme.ControlText),
                                    cellRect);

                    return;
                }
            }

            // Default handler
            base.DrawCell(graphics, rowState, element, column, cellRect);
        }
    }
}
