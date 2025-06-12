using System.Collections.ObjectModel;
using System.ComponentModel;
//using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WpfApp.Helpers;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                RaisePropertyChangeed(nameof(Students));
            }
        }

        public void RaisePropertyChangeed(string propertyName)
        {
            if (null != this.PropertyChanged)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<Student> students;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Get the current button.
            Button cmd = (Button)e.OriginalSource;

            // Create an instance of the window named
            // by the current button.
            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            Window win = (Window)assembly.CreateInstance(type.Namespace + "." + cmd.Content);

            // Show the window.
            win.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Students = new ObservableCollection<Student>
            {
                new Student { Name = "张三", Age = 20 },
                new Student { Name = "李四", Age = 22 },
                new Student { Name = "王五", Age = 24 },
            };
            //studentListBox.ItemsSource = students;
        }

        private void buttonVictore_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DependencyObject level1 = VisualTreeHelper.GetParent(btn);
            DependencyObject level2 = VisualTreeHelper.GetParent(level1);
            DependencyObject level3 = VisualTreeHelper.GetParent(level2);
            MessageBox.Show(level1.GetType().ToString());
            e.Handled = false;
        }

        delegate void DrawArrowFun(
            DrawingContext context,
            Point basePoint,
            Vector arrowUpOrLeft,
            Vector arrowDownOrRight
        );

        delegate void DrawDimensionFun(
            DrawingContext context,
            Point dimensionStart,
            Point dimensionEnd,
            Vector direction,
            Point textStart,
            Point textEnd
        );

        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicCreateClick(object sender, RoutedEventArgs e)
        {
            #region 旧方法画矩形
            ////笔画样式
            //BrushConverter brushConverter = new BrushConverter();
            //Pen arrowPen = new Pen(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#108B96")), 5);
            //Pen dimensionPen = new Pen((Brush)brushConverter.ConvertFromString("#108B96"), 2);
            //Pen holePen = new Pen((Brush)brushConverter.ConvertFromString("#108B96"), 1);

            //// 指定矩形的宽和高,标注字体样式
            //double width = 200;
            //double height = 100;
            //FormattedText widthText = new FormattedText(
            //    width.ToString(),
            //    System.Globalization.CultureInfo.InvariantCulture,
            //    FlowDirection.LeftToRight,
            //    new Typeface("Arial"),
            //    12, (Brush)brushConverter.ConvertFromString("#108B96"));

            //FormattedText heightText = new FormattedText(
            //    height.ToString(),
            //    System.Globalization.CultureInfo.InvariantCulture,
            //    FlowDirection.LeftToRight,
            //    new Typeface("Arial"),
            //    12, (Brush)brushConverter.ConvertFromString("#108B96"));

            //// 创建一个 DrawingVisual 对象，用于绘制图形
            //DrawingVisual visual = new DrawingVisual();

            //using (DrawingContext dc = visual.RenderOpen())
            //{
            //    dc.DrawRectangle(Brushes.White, null, new Rect(0, 0, width + 3, height + 3));

            //    // 绘制矩形
            //    dc.DrawRectangle(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D6C560")), null,
            //        new Rect(1, 1, width + 1, height + 1));

            //    //双箭头-横线
            //    Point startArrowPoint = new Point(width / 3, height / 2);
            //    Point endArrowPoint = new Point(width * 2 / 3, height / 2);

            //    arrowPen.StartLineCap = PenLineCap.Round;
            //    arrowPen.EndLineCap = PenLineCap.Round;
            //    dc.DrawLine(arrowPen, startArrowPoint, endArrowPoint);
            //    //
            //    DrawArrowFun DrawArrowHead =
            //        (DrawingContext context, Point basePoint, Vector arrowUpOrLeft, Vector arrowDownOrRight) =>
            //        {
            //            Point pointup = new Point(basePoint.X + arrowUpOrLeft.X, basePoint.Y + arrowUpOrLeft.Y);
            //            Point pointdown = new Point(basePoint.X + arrowDownOrRight.X, basePoint.Y + arrowDownOrRight.Y);
            //            context.DrawLine(arrowPen, basePoint, pointup);
            //            context.DrawLine(arrowPen, basePoint, pointdown);
            //        };

            //    DrawArrowHead(dc, startArrowPoint, new Vector(20, 15), new Vector(20, -15));

            //    DrawArrowHead(dc, endArrowPoint, new Vector(-20, -15), new Vector(-20, 15));


            //    // 添加尺寸标注


            //    var widthTextOffset = 5;
            //    var heightTextOffset = 5;
            //    var widthTextStartPoint = new Point(width / 2 - widthText.Width / 2, height + widthTextOffset);
            //    var widthTextEndPoint = new Point(width / 2 + widthText.Width / 2, height + widthTextOffset);
            //    var heightTextStartPoint = new Point(width + heightTextOffset, height / 2 - heightText.Height / 2);
            //    var heightTextEndPoint = new Point(width + heightTextOffset, height / 2 + heightText.Height / 2);

            //    // 绘制宽度标注
            //    dc.DrawText(widthText, widthTextStartPoint);
            //    // 绘制高度标注
            //    dc.DrawText(heightText, heightTextStartPoint);


            //    //宽度标注
            //    DrawDimensionFun DrawWidthDimension = (DrawingContext context, Point dimensionStart, Point dimensionEnd,
            //        Vector direcion, Point textStart, Point textEnd) =>
            //    {
            //        //尺寸标注开始竖线
            //        Point dimensionStartLineUp = Point.Add(dimensionStart, new Vector(0, -8));

            //        Point dimensionStartLineDown = Point.Add(dimensionStart, new Vector(0, 8));
            //        context.DrawLine(dimensionPen, dimensionStartLineUp, dimensionStartLineDown);

            //        //context.DrawLine(dimensionPen, dimensionStart, dimensionStartLineDown);
            //        //尺寸标注开始箭头
            //        Point startArrowUp = new Point(dimensionStart.X + direcion.X, dimensionStart.Y + direcion.Y);
            //        Point startArrowDown = new Point(dimensionStart.X + direcion.X, dimensionStart.Y - direcion.Y);
            //        context.DrawLine(dimensionPen, dimensionStart, startArrowUp);
            //        context.DrawLine(dimensionPen, dimensionStart, startArrowDown);
            //        //标注开始箭头位置到尺寸的横线
            //        context.DrawLine(dimensionPen, dimensionStart, textStart);
            //        ////尺寸到标注结束箭头位置
            //        context.DrawLine(dimensionPen, textEnd, dimensionEnd);
            //        ////尺寸标注结束箭头
            //        Point endArrowUp = new Point(dimensionEnd.X - direcion.X, dimensionEnd.Y + direcion.Y);
            //        Point endArrowDown = new Point(dimensionEnd.X - direcion.X, dimensionEnd.Y - direcion.Y);
            //        context.DrawLine(dimensionPen, dimensionEnd, endArrowUp);
            //        context.DrawLine(dimensionPen, dimensionEnd, endArrowDown);

            //        //尺寸标注结束竖线
            //        Point dimensionEndLineUp = Point.Add(dimensionEnd, new Vector(0, -8));

            //        Point dimensionEndLineDown = Point.Add(dimensionEnd, new Vector(0, 8));
            //        context.DrawLine(dimensionPen, dimensionEndLineDown, dimensionEndLineUp);

            //        //context.DrawLine(dimensionPen, dimensionEnd, dimensionEndLineDown);
            //    };
            //    var widthTextDimensionStart =
            //        new Point(width / 2 - widthText.Width / 2 - 5, height + widthTextOffset * 2);
            //    var widthTextDimensionEnd =
            //        new Point(width / 2 + widthText.Width / 2 + 5, height + widthTextOffset * 2);

            //    DrawWidthDimension(dc, new Point(2, height + widthTextOffset * 2),
            //        new Point(width + 1, height + widthTextOffset * 2), new Vector(10, 5),
            //        widthTextDimensionStart, widthTextDimensionEnd);

            //    //高度标注
            //    var heightTextDimensionStart =
            //        new Point(width + heightTextOffset * 2, height / 2 - heightText.Height / 2 - 5);
            //    var heightTextDimensionEnd =
            //        new Point(width + heightTextOffset * 2, height / 2 + heightText.Height / 2 + 5);

            //    DrawDimensionFun DrawHeightDimension = (DrawingContext context, Point dimensionStart,
            //        Point dimensionEnd,
            //        Vector direcion, Point textStart, Point textEnd) =>
            //    {
            //        //尺寸标注开始竖线
            //        Point dimensionStartLineUp = Point.Add(dimensionStart, new Vector(-8, 0));

            //        Point dimensionStartLineDown = Point.Add(dimensionStart, new Vector(8, 0));
            //        context.DrawLine(dimensionPen, dimensionStart, dimensionStartLineUp);

            //        context.DrawLine(dimensionPen, dimensionStart, dimensionStartLineDown);
            //        //尺寸标注开始箭头
            //        Point startArrowUp = new Point(dimensionStart.X + direcion.X, dimensionStart.Y + direcion.Y);
            //        Point startArrowDown = new Point(dimensionStart.X - direcion.X, dimensionStart.Y + direcion.Y);
            //        context.DrawLine(dimensionPen, dimensionStart, startArrowUp);
            //        context.DrawLine(dimensionPen, dimensionStart, startArrowDown);
            //        //标注开始箭头位置到尺寸的横线
            //        context.DrawLine(dimensionPen, dimensionStart, textStart);
            //        ////尺寸到标注结束箭头位置
            //        context.DrawLine(dimensionPen, textEnd, dimensionEnd);
            //        ////尺寸标注结束箭头
            //        Point endArrowUp = new Point(dimensionEnd.X + direcion.X, dimensionEnd.Y - direcion.Y);
            //        Point endArrowDown = new Point(dimensionEnd.X - direcion.X, dimensionEnd.Y - direcion.Y);
            //        context.DrawLine(dimensionPen, dimensionEnd, endArrowUp);
            //        context.DrawLine(dimensionPen, dimensionEnd, endArrowDown);

            //        //尺寸标注结束竖线
            //        Point dimensionEndLineUp = Point.Add(dimensionEnd, new Vector(-8, 0));

            //        Point dimensionEndLineDown = Point.Add(dimensionEnd, new Vector(8, 0));
            //        context.DrawLine(dimensionPen, dimensionEnd, dimensionEndLineUp);

            //        context.DrawLine(dimensionPen, dimensionEnd, dimensionEndLineDown);
            //    };

            //    DrawHeightDimension(dc, new Point(width + heightTextOffset * 2, 2),
            //        new Point(width + heightTextOffset * 2, height + 1), new Vector(5, 10),
            //        heightTextDimensionStart, heightTextDimensionEnd);

            //    //孔
            //    dc.DrawEllipse(Brushes.White, holePen, new Point(width / 2, height / 2), 10, 10);
            //}

            //// 将生成的绘制内容转化为位图
            //RenderTargetBitmap bitmap =
            //    new RenderTargetBitmap((int)(width * 5), (int)(height * 5), 300d, 300d, PixelFormats.Pbgra32);

            //bitmap.Render(visual);

            //// 保存 PNG 图片
            //string directoryPath = "./Pic";
            //if (!Directory.Exists(directoryPath))
            //{
            //    Directory.CreateDirectory(directoryPath);
            //}

            //string filePath = Path.Combine(directoryPath, "Rectangle.png");
            //using (FileStream fs = new FileStream(filePath, FileMode.Create))
            //{
            //    BitmapEncoder encoder = new PngBitmapEncoder();
            //    encoder.Frames.Add(BitmapFrame.Create(bitmap));
            //    encoder.Save(fs);
            //}

            //// 显示在 Image 控件上
            //this.UserImage.Source = bitmap;
            #endregion

            RectangleShapeModel rectangleShapeModel = new RectangleShapeModel()
            {
                PicName = "K",
                Width = 120,
                Length = 150,
                Rounds = new List<Round>() { 
                new(){ X=100,Y=100,Radius=20} },
            };

            var bitmap = PanelPicHelper.GeneratePanelPic(rectangleShapeModel);
            //// 显示在 Image 控件上
            this.UserImage.Source = bitmap;
        }


        /// <summary>
        /// 使用Thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadClick(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 使用Task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskClick(object sender, RoutedEventArgs e)
        {

        }


        #region 多语言
        private string language;
        private List<ResourceDictionary> languages = new()
        {
            new ResourceDictionary()
            {
                Source = new Uri("/LanguagesLibrary;component/Languages/en-US.xaml",UriKind.RelativeOrAbsolute)
            }
            ,
                new ResourceDictionary()
            {
                Source = new Uri("/LanguagesLibrary;component/Languages/zh-CN.xaml",UriKind.RelativeOrAbsolute)
            }
        };
   
        private void SwitchClick(object sender, RoutedEventArgs e)
        {
            if (language == "zh_CN")
            {
                this.Resources = languages[0];
                language = "en_US";
            }
            else
            {
                this.Resources = languages[1];
                language = "zh_CN";
            }
        }
        #endregion





        private void BtnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is ButtonClick!");

        }

        private void BtnInListBoxDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("This is BtnInListBoxDoubleClick!");
        }

        private void ListboxDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject originalSource = e.OriginalSource as DependencyObject;

            var btn = FindControlHelper.FindChild<Button>(originalSource) as Button;

            //var listboxitem = FindControlHelper.FindParent<ListBoxItem>(originalSource) as ListBoxItem;

            //if (listboxitem == null)
            //    return;
            //var datacontext = listboxitem.DataContext as Student;
            if (btn == null)
                return;
           
            var mouseEventArgs = new MouseButtonEventArgs
                 (Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left)
            {
                RoutedEvent = Button.MouseDoubleClickEvent,
                Source = btn
            };
            btn.RaiseEvent(mouseEventArgs);
        }

        private void buttonhaveRadio_Click(object sender, RoutedEventArgs e)
        {
            //if (e.OriginalSource is RadioButton)
            //{
            //    return;
            //}
            MessageBox.Show("buttonhaveRadio_Click");
        }



        private void RadioInButton_Click(object sender, MouseButtonEventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            e.Handled = true;
            radiobutton.IsChecked = !radiobutton.IsChecked;
        }

        private void buttonhaveRadio_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class NumericUpDown
    {
        public string Name { get; set; } = "afd";
        public int Age { get; set; } = 2;
    }
}
