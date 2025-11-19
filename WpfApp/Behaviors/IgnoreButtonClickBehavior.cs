using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Behaviors
{
    public class IgnoreButtonClickBehavior:Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += IgnoreClick;
        }

        private void IgnoreClick(object sender, RoutedEventArgs e)
        {
            e.Handled = false;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= IgnoreClick;
        }
    }
}
