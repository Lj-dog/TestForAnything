using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfAppBehavior_AP_DP.Controls
{
    public class CornerButton : Button
    {
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register(
                "ButtonCornerRadius",
                typeof(CornerRadius),
                typeof(CornerButton),
                new PropertyMetadata(
                    new CornerRadius(),
                    propertyChangedCallback: ButtonRaidusChangedCallback,
                    coerceValueCallback: ButtonRaidusCoerceValueCallback
                ),
                validateValueCallback: ButtonRaidusValiationCallback
            );

        private static object ButtonRaidusCoerceValueCallback(DependencyObject d, object baseValue)
        {
            var cornerRadius = (CornerRadius)baseValue;
            var triggervalue = 5;
            if (
                cornerRadius.BottomLeft < triggervalue
                && cornerRadius.BottomRight < triggervalue
                && cornerRadius.TopRight < triggervalue
                && cornerRadius.TopLeft < triggervalue
            )
            {
                return new CornerRadius(0);
            }
            return baseValue;
        }

        private static void ButtonRaidusChangedCallback(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var cornerButton = (CornerButton)d;
            var cornerRadius = (CornerRadius)e.NewValue;
            var triggervalue = 10;
            if (
                cornerRadius.BottomLeft < triggervalue
                && cornerRadius.BottomRight < triggervalue
                && cornerRadius.TopRight < triggervalue
                && cornerRadius.TopLeft < triggervalue
            )
            {
                cornerButton.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                cornerButton.Background = new SolidColorBrush(Colors.Purple);
            }
        }

        private static bool ButtonRaidusValiationCallback(object value)
        {
            var cornerRadius = (CornerRadius)value;
            var maxValue = 15;
            if (
                cornerRadius.BottomLeft < maxValue
                && cornerRadius.BottomRight < maxValue
                && cornerRadius.TopRight < maxValue
                && cornerRadius.TopLeft < maxValue
            )
                return true;
            else
                return false;
        }

        static CornerButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CornerButton),
                new FrameworkPropertyMetadata(typeof(CornerButton))
            );
        }
    }
}
