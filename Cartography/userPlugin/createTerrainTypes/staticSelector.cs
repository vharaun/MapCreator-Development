﻿using UltimaSDK;

namespace MapCreator.userPlugin
{
    public partial class staticSelector : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public event EventHandler<int> SelectionChanged;

        public staticSelector()
        {
            InitializeComponent();
        }

        private void staticSelector_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        private void staticSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                var dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void staticSelector_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void staticSelector_closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Refresh();
        }

        private void staticSelector_staticPreview_MouseDown(object sender, MouseEventArgs e)
        {
            var num = 0;
            var num1 = 0;

            if (e.Button == MouseButtons.Left)
            {
                var x = e.X;
                if (x is >= 0 and <= 49)
                {
                    num = 0;
                }
                else if (x is >= 50 and <= 99)
                {
                    num = 1;
                }
                else if (x is >= 100 and <= 149)
                {
                    num = 2;
                }
                else if (x is >= 150 and <= 199)
                {
                    num = 3;
                }
                else if (x is >= 200 and <= 249)
                {
                    num = 4;
                }
                else if (x is >= 250 and <= 399)
                {
                    num = 5;
                }

                var y = e.Y;
                if (y is >= 0 and <= 59)
                {
                    num1 = 0;
                }
                else if (y is >= 60 and <= 118)
                {
                    num1 = 1;
                }
                else if (y is >= 120 and <= 177)
                {
                    num1 = 2;
                }
                else if (y is >= 180 and <= 236)
                {
                    num1 = 3;
                }
                else if (y is >= 240 and <= 295)
                {
                    num1 = 4;
                }
                else if (y is >= 300 and <= 354)
                {
                    num1 = 5;
                }
                else if (y is >= 360 and <= 413)
                {
                    num1 = 6;
                }
                else if (y is >= 420 and <= 472)
                {
                    num1 = 7;
                }

                var iSelected = checked(checked(vScrollBar1.Value + checked(num1 * 6)) + num);

                SelectionChanged?.Invoke(this, iSelected);
            }
        }

        private void staticSelector_staticPreview_Paint(object sender, PaintEventArgs e)
        {
            var font = new Font("Arial", 8f);
            var solidBrush = Brushes.Black;
            var pen = Pens.Black;
            var graphics = e.Graphics;
            graphics.Clear(Color.LightGray);

            var defImage = Art.GetStatic(0);

            var value = vScrollBar1.Value;
            var num = 0;

            do
            {
                var num1 = 0;
                do
                {
                    graphics.DrawRectangle(pen, checked(num1 * 50), checked(num * 60), 48, 58);

                    var image = defImage;

                    if (Art.IsValidStatic(value))
                    {
                        image = Art.GetStatic(value);
                    }

                    graphics.DrawString(value.ToString(), font, solidBrush, checked(checked(num1 * 50) + 1), checked(checked(num * 60) + 1));
                    var rectangle = new Rectangle(checked(checked(num1 * 50) + 2), checked(checked(num * 60) + 12), 44, 44);
                    var rectangle1 = new Rectangle(1, 1, 44, 44);
                    graphics.DrawImage(image, rectangle, rectangle1, GraphicsUnit.Pixel);

                    if (++value > vScrollBar1.Maximum)
                    {
                        return;
                    }

                    num1++;
                }
                while (num1 <= 5);
                num++;
            }
            while (num <= 7);
        }
    }
}
