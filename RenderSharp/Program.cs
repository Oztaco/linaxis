using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using System.Runtime.InteropServices;
using System.Drawing;

using RenderSharp.Library;

using System.Windows.Forms;

namespace RenderSharp
{
    class Program
    {
        static Polygon p = new Polygon(500, 500);
        static Mesh my_mesh = new Mesh(100, 100, 1);
        static float theta = 0;


        static void Main(string[] args)
        {
            Form f = new Form();
            f.BackColor = Color.Red;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Bounds = new Rectangle(0, 0, 400, 400);
            //f.TopMost = true;


            //Matrix m1 = new Matrix(4, 3);
            //m1.SetRow(0, new double[] { 1, 2, 3, 4 });
            //m1.SetRow(1, new double[] { 5, 6, 7, 8 });
            //m1.SetRow(2, new double[] { 9, 10, 11, 12 });
            //Console.Write(m1.ToString());

            //Matrix m2 = new Matrix(3, 4);
            //m2.SetRow(0, new double[] { -5, -4, -3 });
            //m2.SetRow(1, new double[] { -2, -1, 0 });
            //m2.SetRow(2, new double[] { 1, 2, 3 });
            //m2.SetRow(2, new double[] { 4, 5, 6 });
            //Console.Write(m2.ToString());

            //Matrix m3 = m1 * 10;
            //Console.Write(m3.ToString());

            p.CenterPoint[0, 0] = 750;
            p.CenterPoint[0, 1] = 700;
            p.AddVertex(600, 500);
            p.AddVertex(600, 600);
            p.AddVertex(500, 600);
            p.AddVertex(500, 500);

            my_mesh.AddVertex(100, 100, 1);
            my_mesh.AddVertex(200, 100, 1);
            my_mesh.AddVertex(200, 200, 1);
            my_mesh.AddVertex(100, 200, 1);
            my_mesh.AddVertex(100, 100, 1);
            my_mesh.AddVertex(100, 100, 1);
            my_mesh.AddVertex(200, 200, 2);

            draw();
            System.Timers.Timer timer = new System.Timers.Timer(16.0);
            timer.Elapsed += DrawLoop;
            timer.Start();

            //Application.EnableVisualStyles();
            //Application.Run(f);
            Console.ReadLine();
        }

        private static void DrawLoop(Object source, ElapsedEventArgs e) {
            draw();
        }

        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        static void draw() {
            theta += 0.05f;
            //theta = (float)Math.PI / 180 * 1;
            //theta = 0;

            using (Graphics g = Graphics.FromHdc(GetDC(IntPtr.Zero))) {
                g.FillRectangle(Brushes.AliceBlue, new Rectangle(0, 0, 2400, 2700));

                Polygon rotated = p.GetRotated(theta);

                Matrix my_translation = new Matrix(1, 2);
                my_translation.SetRow(0, new double[] { -300 });
                my_translation.SetRow(1, new double[] { -300 });

                rotated.Translate(my_translation);

                Mesh rot_mesh = my_mesh.GetRotated(theta);

                //rotated.Translate(rotated.CenterPoint * -1);
                rotated.Render(g);
                rot_mesh.Render(g);

            }
        } 
    }
}
