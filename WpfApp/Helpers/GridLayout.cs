using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Helpers
{
    public static class GridLayout
    {
        internal struct GridDefinitionInfo
        {
            public GridLength GridLength;
            public string SharedSizeGruop;
        }

        #region Helper methods
        internal static GridLength parseGridLength(string text)
        {
            text = text.Trim();
            if (text.ToLower() == "auto")
                return GridLength.Auto;
            if (text.Contains("*"))
            {
                var startCount = text.ToCharArray().Count(c => c == '*');
                var pureNum = text.Replace("*", "");
                var ratio = string.IsNullOrWhiteSpace(pureNum) ? 1 : double.Parse(pureNum);
                return new GridLength(startCount * ratio, GridUnitType.Star);
            }
            var pixelsCount = double.Parse(text);
            return new GridLength(pixelsCount, GridUnitType.Pixel);
        }

        internal static GridDefinitionInfo ParseGridDefinition(string text)
        {
            text = text.Trim();
            var res = new GridDefinitionInfo();
            if (text.StartsWith("[") && text.EndsWith("]"))
            {
                res.SharedSizeGruop = text.Substring(1, text.Length - 2);
                res.GridLength = GridLength.Auto;
            }
            else
            {
                res.GridLength = parseGridLength(text);
            }
            return res;
        }

        internal static IEnumerable<GridDefinitionInfo> Parse(string text)
        {
            if (text.Contains("#"))
            {
                var parts = text.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
                var count = int.Parse(parts[1].Trim());
                return Enumerable.Repeat(ParseGridDefinition(parts[0]), count);
            }
            else
            {
                return new[] { ParseGridDefinition(text) };
            }
        }

        static string? CalculateSharedSize(string sharedSizeGroup, string sharedSizeGruopPrefix)
        {
            if (sharedSizeGroup != null && sharedSizeGruopPrefix != null)
            {
                return sharedSizeGruopPrefix + sharedSizeGroup;
            }
            return sharedSizeGroup;
        }
        #endregion

        #region ColsLayout



        public static string GetColums(DependencyObject obj)
        {
            return (string)obj.GetValue(ColumsProperty);
        }

        public static void SetColums(DependencyObject obj, string value)
        {
            obj.SetValue(ColumsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Colums.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumsProperty =
            DependencyProperty.RegisterAttached(
                "Colums",
                typeof(string),
                typeof(GridLayout),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.AffectsArrange
                        | FrameworkPropertyMetadataOptions.AffectsMeasure,
                    Cols_PropertyChangeCallback
                )
            );

        private static void Cols_PropertyChangeCallback(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var grid = d as Grid;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;
            if (grid == null || oldValue == null || newValue == null)
            {
                return;
            }
            var prefix = GetSharedSizeGroupPrefix(grid);

            if (oldValue != newValue)
            {
                grid.ColumnDefinitions.Clear();
                newValue
                    .Split(new[] {';'},StringSplitOptions.RemoveEmptyEntries)
                    .SelectMany(Parse)
                    .Select(gridDefInfo =>new ColumnDefinition
                    {
                        Width = gridDefInfo.GridLength,
                        SharedSizeGroup = CalculateSharedSize(gridDefInfo.SharedSizeGruop,
                        prefix)
                    })
                    .ToList().ForEach(grid.ColumnDefinitions.Add );
                //var res1 = newValue
                //    .Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                //    .ToList();

                //var res2 = res1.SelectMany(Parse).ToList();

                //var res3 = res2.Select(gridDefinfo => new ColumnDefinition
                //    {
                //        Width = gridDefinfo.GridLength,
                //        SharedSizeGroup = CalculateSharedSize(gridDefinfo.SharedSizeGruop, prefix),
                //    })
                //    .ToList();

                //res3.ForEach(grid.ColumnDefinitions.Add);
            }
        }

        #endregion

        #region RowsLayout


        public static string GetRows(DependencyObject obj)
        {
            return (string)obj.GetValue(RowsProperty);
        }

        public static void SetRows(DependencyObject obj, string value)
        {
            obj.SetValue(RowsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached(
                "Rows",
                typeof(string),
                typeof(GridLayout),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.AffectsArrange
                        | FrameworkPropertyMetadataOptions.AffectsMeasure,
                    Rows_PropertyChangeCallback
                )
            );

        private static void Rows_PropertyChangeCallback(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var grid = d as Grid;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;
            if (grid == null || oldValue == null || newValue == null)
            {
                return;
            }

            var prefix = GetSharedSizeGroupPrefix(grid);

            if (oldValue != newValue)
            {
                grid.RowDefinitions.Clear();
                newValue
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .SelectMany(Parse)
                    .Select(gridDefInfo => new RowDefinition
                    {
                        Height = gridDefInfo.GridLength,
                        SharedSizeGroup = CalculateSharedSize(gridDefInfo.SharedSizeGruop, prefix),
                    })
                    .ToList()
                    .ForEach(grid.RowDefinitions.Add);
            }
        }

        #endregion

        #region Cell


        public static string GetCell(DependencyObject obj)
        {
            return (string)obj.GetValue(CellProperty);
        }

        public static void SetCell(DependencyObject obj, string value)
        {
            obj.SetValue(CellProperty, value);
        }

        // Using a DependencyProperty as the backing store for Cell.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CellProperty =
            DependencyProperty.RegisterAttached(
                "Cell",
                typeof(string),
                typeof(GridLayout),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.AffectsArrange
                        | FrameworkPropertyMetadataOptions.AffectsMeasure,
                    Cell_PropertyChangedCallback
                )
            );

        private static void Cell_PropertyChangedCallback(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var element = d as UIElement;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;
            if (element == null | oldValue == null | newValue == null)
                return;

            if (oldValue != newValue)
            {
                var rowAndCol = newValue.Split(new[] { ' ', ';' });

                if (!string.IsNullOrEmpty(rowAndCol[0]))
                {
                    var row = int.Parse(rowAndCol[0]);
                    Grid.SetRow(element, row);
                }
                if (!string.IsNullOrEmpty(rowAndCol[1]))
                {
                    var col = int.Parse(rowAndCol[1]);
                    Grid.SetColumn(element, col);
                }
            }
        }

        #endregion

        #region SharedSizeGroupPrefix


        public static string GetSharedSizeGroupPrefix(DependencyObject obj)
        {
            return (string)obj.GetValue(SharedSizeGroupPrefixProperty);
        }

        public static void SetSharedSizeGroupPrefix(DependencyObject obj, string value)
        {
            obj.SetValue(SharedSizeGroupPrefixProperty, value);
        }

        // Using a DependencyProperty as the backing store for SharedSizeGroupPrefix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SharedSizeGroupPrefixProperty =
            DependencyProperty.RegisterAttached(
                "SharedSizeGroupPrefix",
                typeof(string),
                typeof(GridLayout),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.AffectsArrange
                        | FrameworkPropertyMetadataOptions.AffectsMeasure
                )
            );

        #endregion
    }
}
