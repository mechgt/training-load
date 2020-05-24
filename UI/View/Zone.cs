using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Chart;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace TrainingLoad.UI.View
{
    class Zone : GraphObjList
    {
        internal Zone(GraphPane pane, float value, string name, Color color, float height)
        {
            LineObj line = new AxisLineObj(pane.YAxis, value, Color.FromArgb(140, color));
            line.Line.Width = 2;
            line.Line.Style = DashStyle.Dash;
            line.Tag = "ZoneL" + name;
            line.IsVisible = true;
            this.Add(line);

            TextObj text = new AxisTextObj(pane.YAxis, value, name);
            text.Location.Height = height;
            text.Location.AlignV = AlignV.Top;
            text.Tag = "ZoneT" + name;
            text.IsVisible = true;
            this.Add(text);

            GradientFillObj fill = new GradientFillObj(0, value, 1, height, CoordType.XChartFractionYScale);
            fill.MaximumSize = new Size(0, 50);
            fill.Fill.Type = FillType.Brush;
            fill.Fill.Color = Color.Transparent;
            fill.Fill.SecondaryValueGradientColor = Color.FromArgb(40, color);
            fill.Tag = "ZoneF" + name;
            fill.IsVisible = true;
            this.Add(fill);
        }

        internal bool IsVisible
        {
            get
            {
                foreach (GraphObj item in this)
                {
                    if (!this.IsVisible)
                        return false;
                }

                return true;
            }
            set
            {
                foreach (GraphObj item in this)
                    item.IsVisible = value;
            }
        }
    }
}
