using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppDictionary.MyControl
{
  public  class MyButton : System.Windows.Controls.Button
    {
        public CornerRadius MyButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(MyButtonCornerRadiusProperty); }
            set { SetValue(MyButtonCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyButtonCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyButtonCornerRadiusProperty =
            DependencyProperty.Register("MyButtonCornerRadius", typeof(CornerRadius), typeof(MyButton), new PropertyMetadata(new CornerRadius(5)));
    }
}
