using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderSharp.Library
{
    class Polygon
    {
        private Matrix[] vertices;
        public Matrix[] Vertices {
            get {
                return vertices;
            }
            set {
                vertices = value;
            }
        }

        public Polygon()
        {

        }
    }
}
