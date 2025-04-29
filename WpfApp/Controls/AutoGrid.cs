using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Controls
{
    public class AutoGrid:Grid
    {
        #region ColumnDefinitionWidth
        /// <summary>
        /// Row.Width
        /// 　負数：固定高(50pixel)
        /// 　 0　：可変(1*)
        /// 　正数：指定高
        /// </summary>
        public static readonly DependencyProperty ColumnDefinitionWidthProperty =
                                    DependencyProperty.Register("ColumnDefinitionWidth",
                                        typeof(int),
                                        typeof(AutoGrid),
                                        new PropertyMetadata(0));
        public int ColumnDefinitionWidth
        {
            get { return (int)GetValue(ColumnDefinitionWidthProperty); }
            set { SetValue(ColumnDefinitionWidthProperty, value); }
        }
        #endregion

        #region ColumnCount
        public static readonly DependencyProperty GridColumnCountProperty =
                                    DependencyProperty.Register("GridColumnCount",
                                        typeof(int),
                                        typeof(AutoGrid),
                                        new PropertyMetadata(0, new PropertyChangedCallback(OnGridColumnCountChanged)));
        public int GridColumnCount
        {
            get { return (int)GetValue(GridColumnCountProperty); }
            set { SetValue(GridColumnCountProperty, value); }
        }

        #endregion

        #region RowDefinitionHeight
        /// <summary>
        /// Row.Height
        /// 　負数：固定高(14pixel)
        /// 　 0　：可変(1*)
        /// 　正数：指定高
        /// </summary>
        public static readonly DependencyProperty RowDefinitionHeightProperty =
                                    DependencyProperty.Register("RowDefinitionHeight",
                                        typeof(int),
                                        typeof(AutoGrid),
                                        new PropertyMetadata(-1));
        public int RowDefinitionHeight
        {
            get { return (int)GetValue(RowDefinitionHeightProperty); }
            set { SetValue(RowDefinitionHeightProperty, value); }
        }
        #endregion

        #region RowCount 
        public static readonly DependencyProperty GridRowCountProperty =
                                    DependencyProperty.Register("GridRowCount",
                                        typeof(int),
                                        typeof(AutoGrid),
                                        new PropertyMetadata(1, new PropertyChangedCallback(OnGridRowCountChanged)));
        public int GridRowCount
        {
            get { return (int)GetValue(GridRowCountProperty); }
            set { SetValue(GridRowCountProperty, value); }
        }

        #endregion

        /// <summary>
        /// GridRowCountで指定された数のRowDefinitionsを作成
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnGridRowCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)d;

            //grid.RowDefinitions.Clear();
            var count = grid.RowDefinitions.Count;
       
            int heightProperty = (int)d.GetValue(RowDefinitionHeightProperty);

            var increment = (int)e.NewValue - count;
            if (increment > 0)
                for (int i = 0; i < increment; i++)
                {

                    RowDefinition rowDefinition = new RowDefinition();
                    if (heightProperty < 0)
                    {
                        rowDefinition.Height = new GridLength(0, GridUnitType.Auto);
                    }
                    else if (heightProperty > 0)
                    {
                        rowDefinition.Height = new GridLength(heightProperty, GridUnitType.Pixel);
                    }
                    else if (heightProperty == 0)
                    {
                        rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                    }
                    grid.RowDefinitions.Add(rowDefinition);
                }
            else if(increment < 0 && count > 0)
            {
                increment = -increment;
                grid.RowDefinitions.RemoveRange(count - increment, increment);
            }
        }

        /// <summary>
        /// GridColumnCountで指定された数のColumnDefinitionsを作成
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnGridColumnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)d;

            //grid.ColumnDefinitions.Clear();
            var count = grid.ColumnDefinitions.Count;

            int widthProperty = (int)d.GetValue(ColumnDefinitionWidthProperty);

            var increment = (int)e.NewValue - count;
            if (increment > 0)
                for (int i = 0; i < increment; i++)
            {

                ColumnDefinition columnDefinition = new ColumnDefinition();
                if (widthProperty < 0)
                {
                    columnDefinition.Width = new GridLength(0, GridUnitType.Auto);
                }
                else if (widthProperty > 0)
                {
                    columnDefinition.Width = new GridLength(widthProperty, GridUnitType.Pixel);
                }
                else if (widthProperty == 0)
                {
                    columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                }
                grid.ColumnDefinitions.Add(columnDefinition);
            }
            else if (increment < 0 && count > 0)
            {
                increment = -increment;
                grid.ColumnDefinitions.RemoveRange(count - increment, increment);
            }
        }

    }
}
