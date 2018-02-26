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
            Matrix<int> m = new Matrix<int>(4, 4);
            Console.Write(m.ToString());

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
