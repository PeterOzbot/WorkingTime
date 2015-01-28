using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorkingTime.Library.Controls {
    /// <summary>
    /// Panel which stretches its children to all fit in available space. Children are all the same size.
    /// </summary>
    public class ProportionallyStretchingPanel : Panel {
        protected override Size MeasureOverride(Size availableSize) {
            double proportionalWidth = availableSize.Width / InternalChildren.Count;
            Size realSize = new Size(proportionalWidth, availableSize.Height);

            foreach (UIElement child in InternalChildren) {
                child.Measure(realSize);
            }

            return realSize;
        }

        protected override Size ArrangeOverride(Size availableSize) {

            double x = 0.0;
            double proportionalWidth = availableSize.Width / InternalChildren.Count;
            foreach (UIElement child in InternalChildren) {
                child.Arrange(
                    new Rect(
                        new Point(x, 0.0),
                        new Point(x + proportionalWidth, availableSize.Height)));
                x += proportionalWidth;
            }
            return availableSize;
        }
    }
}
