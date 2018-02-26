using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using System.Runtime.InteropServices;
using System.Drawing;

using RenderSharp.Library;

namespace RenderSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(4, 3);
            m1.SetRow(0, new long[] { 2, 7, 8, 12 });
            m1.SetRow(1, new long[] { 9, 0, -1, -3 });
            m1.SetRow(2, new long[] { 0, 3, 2, 9 });
            Console.Write(m1.ToString());

            Matrix m2 = new Matrix(3, 4);
            m2.SetRow(0, new long[] { 8, 1, -2 });
            m2.SetRow(1, new long[] { -3, 1, -3 });
            m2.SetRow(2, new long[] { 0, 7, 8 });
            m2.SetRow(2, new long[] { 0, 7, 8 });
            Console.Write(m2.ToString());

            Matrix m3 = m1 * 10;
            Console.Write(m3.ToString());

            draw();
            Timer timer = new Timer(16.0);
            timer.Elapsed += DrawLoop;
            timer.Start();
            Console.ReadLine();
        }

        private static void DrawLoop(Object source, ElapsedEventArgs e) {
            draw();
        }

        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        static void draw() {
            using (Graphics g = Graphics.FromHdc(GetDC(IntPtr.Zero))) {
                g.FillRectangle(Brushes.AliceBlue, new Rectangle(0, 0, 1000, 1000));
            }
        } 
    }
}
