using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RenderSharp.Library
{
    class Matrix
    {
        int height; // Number of rows
        public int Height
        {
            get
            {
                return height;
            }
        }
        int width; // Number of columns
        public int Width
        {
            get
            {
                return width;
            }
        }
        private double[] matrixData;
        public double this[int x, int y] {
            get {
                return matrixData[(y * width) + x];
            }
            set {
                matrixData[(y * width) + x] = value;
            }
        }

        public Matrix(int width, int height) {
            this.height = height;
            this.width = width;
            this.matrixData = new double[height * width];
        }

        public void SetRow(int rowNumber, double[] row) {
            if (rowNumber >= height) {
                throw new IndexOutOfRangeException(String.Format("The row {0} does not exist in a matrix of height {1}", rowNumber, this.height));
            }
            if (row.Length != this.width) {
                throw new Exception(String.Format("The specified row has a Length {0}, but the matrix has a Width {1}", row.Length, this.width));
            }

            for (int columnNumber = 0; columnNumber < width; columnNumber++) {
                this[columnNumber, rowNumber] = row[columnNumber];
            }
        }

        public static Matrix operator +(Matrix m1, Matrix m2) {
            Matrix answer = new Matrix(m1.width, m1.height);
            for (int row = 0; row < answer.height; row++)
            {
                for (int col = 0; col < answer.width; col++)
                {
                    answer[col, row] = m1[col, row] + m2[col, row];
                }
            }
            return answer;
        }

        public static Matrix operator *(Matrix m, double scalar) {
            Matrix answer = new Matrix(m.width, m.height);
            for (int row = 0; row < answer.height; row++)
            {
                for (int col = 0; col < answer.width; col++)
                {
                    answer[col, row] = m[col, row] * scalar;
                }
            }
            return answer;
        }

        public static Matrix operator *(Matrix m1, Matrix m2) {
            if (m1.width != m2.height) {
                throw new ArgumentException(String.Format("The rows of the first matrix {0} does not match the columns of the second matrix {1}.", m1.width, m2.height));
            }
            int cellCount = m1.width;
            double cellValue = 0;
            Matrix answer = new Matrix(m2.width, m1.height);
            for (int row = 0; row < answer.height; row++)
            {
                for (int col = 0; col < answer.width; col++)
                {
                    cellValue = 0;
                    for (int n = 0; n < cellCount; n++) {
                        cellValue += m1[n, row] * m2[col, n];
                    }
                    answer[col, row] = cellValue;
                }
            }
            return answer;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int row = 0; row < this.height; row++) {
                for (int col = 0; col < this.width; col++) {
                    str.AppendFormat("{0} ", this[col, row]);
                    if (col == this.width - 1)
                        str.AppendLine();
                }
            }
            str.AppendLine();
            return str.ToString();
        }

        public static implicit operator Point(Matrix m)
        {
            if (m.height >= 2 && m.width == 1)
            {
                return new Point((int)m[0, 0], (int)m[0, 1]);
            }
            else if (m.height == 1 && m.width >= 2)
            {
                return new Point((int)m[0, 0], (int)m[1, 0]);
            }
            else {
                throw new ArgumentException("Matrices can only be cast to points if they have a height or width of 1.");
            }
        }
    }
}
