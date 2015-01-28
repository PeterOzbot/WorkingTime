using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorkingTime.Library.Controls {
    /// <summary>
    /// Panel which holds only seven items or less and gives each item 1/7 of available space.
    /// </summary>
    public class SevenItemPanel : Panel {
        protected override Size MeasureOverride(Size availableSize) {
            foreach (UIElement child in InternalChildren) {
                child.Measure(availableSize);
            }

            return new Size(availableSize.Width / InternalChildren.Count, availableSize.Height);
        }

        protected override Size ArrangeOverride(Size availableSize) {
            double itemHeight = availableSize.Height / 7;

            double y = 0.0;
            foreach (UIElement child in InternalChildren) {
                child.Arrange(
                    new Rect(
                        new Point(0.0, y),
                        new Point(availableSize.Width, y + itemHeight)));
                y += itemHeight;
            }

            return availableSize;
        }
    }
}
