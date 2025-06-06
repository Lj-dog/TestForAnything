using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace WpfApp.Helpers
{
    internal class FindControlHelper
    {
        public static T? FindParent<T>(DependencyObject dependencyObject, string? name = null)
            where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            while (parent != null)
            {
                if (parent is T && (((T)parent).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)parent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        public static T? FindChild<T>(DependencyObject dependencyObject, string? name = null)
            where T : FrameworkElement
        {
            DependencyObject? child = null;
            T? grandChild = null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                child = VisualTreeHelper.GetChild(dependencyObject, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = FindChild<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }
        
            return null;
        }
    }
}
