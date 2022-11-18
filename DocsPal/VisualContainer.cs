using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DocWriter
{
    public class VisualContainer : FrameworkElement
    {
        private readonly VisualCollection children;

        public VisualContainer()
        {
            children = new VisualCollection(this);
        }

        public void AddVisual(Visual v)
        {
            children.Add(v);
        }

        protected override Visual GetVisualChild(int index)
        {
            return children[index];
        }

        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }
    }   
}
