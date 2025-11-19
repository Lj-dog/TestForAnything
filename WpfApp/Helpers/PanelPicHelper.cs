using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp.Helpers
{
    public class RectangleShapeModel
    {
        public string PicName { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }

        public List<Round> Rounds { get; set; } = [];
    }

    public class Round
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
    }

    public static class PanelPicHelper
    {
        private static double ratio = 1;
        private static double dpi = 1;

        #region Pens
        private static readonly Pen holePen =
            new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#108B96")), 1);

        private static Pen arrowPen =
            new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#108B96")), 5)
            {
                StartLineCap = PenLineCap.Round,
                EndLineCap = PenLineCap.Round,
            };

        private static Pen dimensionPen =
            new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#108B96")), 2);
        #endregion

        public static RenderTargetBitmap GeneratePanelPic(RectangleShapeModel panel)
        {
            try
            {
                DrawingVisual visual = new DrawingVisual();
                double area = panel.Width * panel.Length;
                dpi = -(1 / 10000000.0) * (area - 100000) + 1;
                ratio = 100000.0 / area;
                ratio = System.Math.Sqrt(ratio);
                double picLength = ratio * panel.Length;
                double picWidth = ratio * panel.Width;
                //Dimension Text
                FormattedText widthTextFormat =
                    new(
                        ((int)panel.Width).ToString(),
                        System.Globalization.CultureInfo.InvariantCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Arial"),
                        (24),
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#108B96")),
                        196d * dpi
                    );
                FormattedText lengthTextFormat =
                    new(
                        ((int)panel.Length).ToString(),
                        System.Globalization.CultureInfo.InvariantCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Arial"),
                        (24),
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#108B96")),
                        196d * dpi
                    );

                using (DrawingContext context = visual.RenderOpen())
                {
                    //arrowPen.Thickness = area / 20000;
                    //dimensionPen.Thickness = area / 100000;
                    DrawPanel(context, picLength, picWidth);
                    DrawHoles(context, panel.Rounds);
                    DrawDoubleArrowLine(
                        context,
                        picLength,
                        picWidth,
                        new Vector(20 / ratio, 15 / ratio)
                    );
                    DrawDimension(
                        context,
                        lengthTextFormat,
                        widthTextFormat,
                        picLength,
                        picWidth,
                        (picWidth * picLength)
                    );
                }
                RenderTargetBitmap bitmap = new RenderTargetBitmap(
                    (int)((picLength + (picLength + 150)) * dpi),
                    (int)((picWidth + (picWidth + 150)) * dpi),
                    196d * dpi,
                    196d * dpi,
                    PixelFormats.Pbgra32
                );
                bitmap.Render(visual);
                return bitmap;
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}
                //string filePath = Path.Combine(folderPath, panel.PicName + ".png");
                //using (FileStream fs = new(filePath, FileMode.Create))
                //{
                //    BitmapEncoder encoder = new PngBitmapEncoder();
                //    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                //    encoder.Save(fs);
                //}
            }
            catch (Exception ee)
            {
                MessageBox.Show($"{ee.Message}\n生成板件图片错误");
                return null;
            }
        }

        /// <summary>
        /// 画板件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        private static void DrawPanel(DrawingContext context, double length, double width)
        {
            //画板件
            context.DrawRectangle(
                Brushes.White,
                null,
                new System.Windows.Rect(0, 0, length + 2, width + 2)
            );
            context.DrawRectangle(
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D6C560")),
                null,
                new(1, 1, length, width)
            );
        }

        /// <summary>
        /// 画孔
        /// </summary>
        /// <param name="context"></param>
        /// <param name="holes"></param>
        private static void DrawHoles(DrawingContext context, List<Round> holes)
        {
            foreach (var hole in holes)
            {
                context.DrawEllipse(
                    Brushes.White,
                    holePen,
                    new((hole.X * ratio + 1), (hole.Y * ratio + 1)),
                    hole.Radius * ratio,
                    hole.Radius * ratio
                );
            }
        }

        /// <summary>
        /// 画双箭头线
        /// </summary>
        /// <param name="context"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        private static void DrawDoubleArrowLine(
            DrawingContext context,
            double length,
            double width,
            Vector Arrowdirection
        )
        {
            Point startArrowPoint = new(length / 10, width / 2);
            Point endArrowPoint = new(length * 9 / 10, width / 2);
            context.DrawLine(arrowPen, startArrowPoint, endArrowPoint);

            DrawArrowHead(
                context,
                arrowPen,
                startArrowPoint,
                Arrowdirection,
                new Vector(Arrowdirection.X, -Arrowdirection.Y)
            );
            DrawArrowHead(
                context,
                arrowPen,
                endArrowPoint,
                new Vector(-Arrowdirection.X, -Arrowdirection.Y),
                new Vector(-Arrowdirection.X, Arrowdirection.Y)
            );

            //DrawArrowHead(context, arrowPen, startArrowPoint, new Vector(20, 15), new Vector(20, -15));
            //DrawArrowHead(context, arrowPen, endArrowPoint, new Vector(-20, -15), new Vector(-20, 15));
        }

        /// <summary>
        /// 画箭头
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pen"></param>
        /// <param name="basePoint"></param>
        /// <param name="arrowUpOrLeft"></param>
        /// <param name="arrowDownOrRight"></param>
        private static void DrawArrowHead(
            DrawingContext context,
            Pen pen,
            Point basePoint,
            Vector arrowUpOrLeft,
            Vector arrowDownOrRight
        )
        {
            Point arrowUp = new(basePoint.X + arrowUpOrLeft.X, basePoint.Y + arrowUpOrLeft.Y);
            Point arrowDown =
                new(basePoint.X + arrowDownOrRight.X, basePoint.Y + arrowDownOrRight.Y);
            context.DrawLine(pen, basePoint, arrowUp);
            context.DrawLine(pen, basePoint, arrowDown);
        }

        /// <summary>
        /// 画尺寸标注
        /// </summary>
        /// <param name="context"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        private static void DrawDimension(
            DrawingContext context,
            FormattedText lengthTextFormat,
            FormattedText widthTextFormat,
            double length,
            double width,
            double area
        )
        {
            //Text Position
            #region Text
            var TextVerticalOffset = 5.0;
            var lenghtTextStartPoint = new Point(
                length / 2 - lengthTextFormat.Width / 2,
                width + TextVerticalOffset
            );
            var widthTextEndPoint = new Point(
                length / 2 + lengthTextFormat.Width / 2,
                width + TextVerticalOffset
            );

            var TextHorizontalOffset = 5.0;
            var widthTextStartPoint = new Point(
                length + TextHorizontalOffset,
                width / 2 - widthTextFormat.Height / 2
            );
            var lengthTextEndPoint = new Point(
                length + TextHorizontalOffset,
                width / 2 + widthTextFormat.Height / 2
            );

            context.DrawText(lengthTextFormat, lenghtTextStartPoint);
            context.DrawText(widthTextFormat, widthTextStartPoint);
            #endregion

            //Length Dimensioning Style
            var lengthDimensionStart = new Point(
                2,
                width + TextVerticalOffset + lengthTextFormat.Height / 2
            );
            var lengthDimensionToText = new Point(
                length / 2 - lengthTextFormat.Width / 2 - 5,
                width + TextVerticalOffset + lengthTextFormat.Height / 2
            );
            var lengthTextToDimension = new Point(
                length / 2 + lengthTextFormat.Width / 2 + 5,
                width + TextVerticalOffset + lengthTextFormat.Height / 2
            );
            var lengthDimensionEnd = new Point(
                length + 1,
                width + TextVerticalOffset + lengthTextFormat.Height / 2
            );
            DrawLengthDimension(
                context,
                lengthDimensionStart,
                lengthDimensionEnd,
                lengthDimensionToText,
                lengthTextToDimension,
                lengthTextFormat.Height,
                new Vector(area / 10000, area / 20000)
            );

            //Width Dimensioning Style
            var widthDimensionStart = new Point(
                length + TextHorizontalOffset + widthTextFormat.Width / 2,
                2
            );
            var widthDimensionToText = new Point(
                length + TextHorizontalOffset + widthTextFormat.Width / 2,
                width / 2 - widthTextFormat.Height / 2 - 5
            );
            var widthTextToDimension = new Point(
                length + TextHorizontalOffset + widthTextFormat.Width / 2,
                width / 2 + widthTextFormat.Height / 2 + 5
            );
            var widthDimensionEnd = new Point(
                length + TextHorizontalOffset + widthTextFormat.Width / 2,
                width + 1
            );
            DrawWidthDimension(
                context,
                widthDimensionStart,
                widthDimensionEnd,
                widthDimensionToText,
                widthTextToDimension,
                widthTextFormat.Width,
                new Vector(area / 20000, area / 10000)
            );
        }

        /// <summary>
        /// Width Dimension Style
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dimensionStart"></param>
        /// <param name="dimensionEnd"></param>
        /// <param name="dimensionToText"></param>
        /// <param name="TextToDimension"></param>
        private static void DrawLengthDimension(
            DrawingContext context,
            Point dimensionStart,
            Point dimensionEnd,
            Point dimensionToText,
            Point TextToDimension,
            double lineLength,
            Vector arrowDirection
        )
        {
            //

            //dimension Start Line
            Point dimensionStartLineUp = Point.Add(dimensionStart, new Vector(0, -lineLength / 2));
            Point dimensionStartLineDown = Point.Add(dimensionStart, new Vector(0, lineLength / 2));

            context.DrawLine(dimensionPen, dimensionStartLineUp, dimensionStartLineDown);

            //dimension Arrow
            DrawArrowHead(
                context,
                dimensionPen,
                dimensionStart,
                arrowDirection,
                new Vector(arrowDirection.X, -arrowDirection.Y)
            );

            //
            context.DrawLine(dimensionPen, dimensionStart, dimensionToText);
            //
            context.DrawLine(dimensionPen, TextToDimension, dimensionEnd);

            //dimension Arrow
            DrawArrowHead(
                context,
                dimensionPen,
                dimensionEnd,
                new Vector(-arrowDirection.X, arrowDirection.Y),
                new Vector(-arrowDirection.X, -arrowDirection.Y)
            );

            //dimension End Line
            Point dimensionEndLineUp = Point.Add(dimensionEnd, new Vector(0, -lineLength / 2));
            Point dimensionEndLineDown = Point.Add(dimensionEnd, new Vector(0, lineLength / 2));

            context.DrawLine(dimensionPen, dimensionEndLineUp, dimensionEndLineDown);
        }

        private static void DrawWidthDimension(
            DrawingContext context,
            Point dimensionStart,
            Point dimensionEnd,
            Point dimensionToText,
            Point TextToDimension,
            double lineLength,
            Vector arrowDirection
        )
        {
            //

            //dimension Start Line
            Point dimensionStartLineUp = Point.Add(dimensionStart, new Vector(-lineLength / 2, 0));
            Point dimensionStartLineDown = Point.Add(dimensionStart, new Vector(lineLength / 2, 0));

            context.DrawLine(dimensionPen, dimensionStartLineUp, dimensionStartLineDown);

            //dimension Arrow Start
            DrawArrowHead(
                context,
                dimensionPen,
                dimensionStart,
                arrowDirection,
                new Vector(-arrowDirection.X, arrowDirection.Y)
            );

            //
            context.DrawLine(dimensionPen, dimensionStart, dimensionToText);
            //
            context.DrawLine(dimensionPen, TextToDimension, dimensionEnd);

            //dimension Arrow End
            DrawArrowHead(
                context,
                dimensionPen,
                dimensionEnd,
                new Vector(arrowDirection.X, -arrowDirection.Y),
                new Vector(-arrowDirection.X, -arrowDirection.Y)
            );

            //dimension End Line
            Point dimensionEndLineUp = Point.Add(dimensionEnd, new Vector(-lineLength / 2, 0));
            Point dimensionEndLineDown = Point.Add(dimensionEnd, new Vector(lineLength / 2, 0));

            context.DrawLine(dimensionPen, dimensionEndLineUp, dimensionEndLineDown);
        }
    }
}
