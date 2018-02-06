using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderSharp.Library
{
    struct Pixel
    {
        byte Red;
        byte Green;
        byte Blue;
        byte Alpha;

        public Pixel(byte R, byte G, byte B)
        {
            Red = R;
            Green = G;
            Blue = B;
            Alpha = 255;
        }
        public Pixel(byte R, byte G, byte B, byte A) : this(R, G, B)
        {
            Alpha = A;
        }
    }
}
