using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderSharp.Library
{
    class Canvas
    {
        /* Properties */
        Pixel[] pixels;
        private int height;
        public int Height {
            get => height;
        }
        private int width;
        public int Width {
            get => width;
        }

        /* Methods */

        public Canvas(int width, int height)
        {
            this.width = width;
            this.height = height;
            pixels = new Pixel[width * height];
            for (int i = 0; i < pixels.Length; i++) {
                pixels[i] = new Pixel(0, 0, 0, 127); // TODO make this fully transparent
            }
        }
    }
}
