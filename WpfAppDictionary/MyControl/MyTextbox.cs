using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppDictionary.MyControl
{
    internal class MyTextbox:TextBox
    {





        public CornerRadius MyTextBoxCornerRadius
        {
            get { return (CornerRadius)GetValue(MyTextBoxCornerRadiusProperty); }
            set { SetValue(MyTextBoxCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyTextBoxCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyTextBoxCornerRadiusProperty =
            DependencyProperty.Register("MyTextBoxCornerRadius", typeof(CornerRadius), typeof(MyTextbox));




     

    }
}
