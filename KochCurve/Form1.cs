using System;
using System.Drawing;
using System.Windows.Forms;

namespace KochCurve
{
    public partial class KochForm : Form
    {
        private bool isPainted = false;

        public KochForm()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            if (isPainted)
            {
                g.Clear(this.BackColor);
            }
            if (!string.IsNullOrEmpty(txtDepth.Text))
            {
                int depth = Convert.ToInt32(txtDepth.Text);
                if (depth > -1 && depth < 6) // depth must be between 0 and 5
                {
                    Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
                    DrawKoch(g, pen, depth, 20, 280, 280, 280);
                    DrawKoch(g, pen, depth, 280, 280, 150, 20);
                    DrawKoch(g, pen, depth, 150, 20, 20, 280);
                    isPainted = true;
                    txtDepth.BackColor = SystemColors.Window;
                }
                else
                {
                    txtDepth.BackColor = Color.Tomato;
                }
            }
            else
            {
                txtDepth.BackColor = Color.Tomato;
            }
        }

        /// <summary>
        /// To allow only numbers in a textbox in a windows application and backspace to remove previously entered text
        /// </summary>
        private void txtDepth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void DrawKoch(Graphics g, Pen p, int lev, int x1, int y1, int x5, int y5)
        {
            int deltaX, deltaY, x2, y2, x3, y3, x4, y4;

            if (lev == 0)
            {
                g.DrawLine(p, x1, y1, x5, y5);
            }
            else
            {
                deltaX = x5 - x1;
                deltaY = y5 - y1;

                x2 = x1 + deltaX / 3;
                y2 = y1 + deltaY / 3;

                x3 = (int)(0.5 * (x1 + x5) + Math.Sqrt(3) * (y1 - y5) / 6);
                y3 = (int)(0.5 * (y1 + y5) + Math.Sqrt(3) * (x5 - x1) / 6);

                x4 = x1 + 2 * deltaX / 3;
                y4 = y1 + 2 * deltaY / 3;

                DrawKoch(g, p, lev - 1, x1, y1, x2, y2);
                DrawKoch(g, p, lev - 1, x2, y2, x3, y3);
                DrawKoch(g, p, lev - 1, x3, y3, x4, y4);
                DrawKoch(g, p, lev - 1, x4, y4, x5, y5);
            }
        }
    }
}