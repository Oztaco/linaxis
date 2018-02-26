using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderSharp.Library
{
    class Matrix<T>
    {
        int height;
        int Height
        {
            get
            {
                return height;
            }
        }
        int width;
        int Width
        {
            get
            {
                return height;
            }
        }
        private T[] matrixData;
        public T this[int x, int y] {
            get {
                return matrixData[(y * width) + x];
            }
        }

        public Matrix(int height, int width) {
            this.height = height;
            this.width = width;
        }
    }
}
