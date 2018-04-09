using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace RenderSharp.Library
{
    class Polygon
    {
        private List<Matrix> vertices = new List<Matrix>();
        public List<Matrix> Vertices {
            get {
                return vertices;
            }
            set {
                vertices = value;
            }
        }
        public Matrix CenterPoint;


        public Polygon() {
            CenterPoint = new Matrix(1, 2);
        }


        public Polygon(long x, long y) : this() {
            CenterPoint[0, 0] = x;
            CenterPoint[0, 1] = y;

            Matrix m = new Matrix(1, 2);
            m[0, 0] = x;
            m[0, 1] = y;
            vertices.Add(m);
        }


        public void Render(Graphics g) {
            Pen pen = new Pen(Color.Red, 4.0f);
            for (int i = 0; i < vertices.Count - 1; i++) {
                g.DrawLine(pen, vertices[i], vertices[i + 1]);
            }
        }


        public void AddVertex(Matrix m) {
            vertices.Add(m);
        }


        public void AddVertex(long x, long y) {
            Matrix m = new Matrix(1, 2);
            m[0, 0] = x;
            m[0, 1] = y;
            vertices.Add(m);
        }


        public static Polygon operator *(Polygon p, Matrix m) {
            Polygon newP = new Polygon();

            for (int i = 0; i < p.vertices.Count; i++)
            {
                newP.AddVertex(p.vertices.ElementAt(i) * m);
            }

            return newP;
        }

        public static Polygon operator *(Matrix m, Polygon p) {
            Polygon newP = new Polygon();

            for (int i = 0; i < p.vertices.Count; i++)
            {
                newP.AddVertex(m * p.vertices.ElementAt(i));
            }

            return newP;
        }


        public Polygon Clone() {
            Polygon newP = new Polygon();
            Matrix m, newM;

            for (int i = 0; i < this.vertices.Count; i++)
            {
                m = this.vertices.ElementAt(i);
                newM = new Matrix(m.Width, m.Height);
                for (int x = 0; x < m.Width; x++)
                {
                    for (int y = 0; y < m.Height; y++)
                    {
                        newM[x, y] = m[x, y];
                    }
                }
                newP.AddVertex(newM);
            }

            return newP;
        }


        public void Translate(double x, double y)
        {
            foreach (Matrix vertex in vertices)
            {
                vertex[0, 0] += x;
                vertex[0, 1] += y;
            }
        }


        public void Translate(Matrix m)
        {
            foreach (Matrix vertex in vertices)
            {
                vertex[0, 0] += m[0, 0];
                vertex[0, 1] += m[0, 1];
            }
        }


        public Polygon GetRotated(double theta) {
            Polygon rotated = this.Clone();

            rotated.Translate(CenterPoint * -1);

            Matrix rot = new Matrix(2, 2);
            rot.SetRow(0, new double[] { Math.Cos(theta), Math.Sin(theta) });
            rot.SetRow(1, new double[] { -Math.Sin(theta), Math.Cos(theta) });
            
            rotated = rot * rotated;

            rotated.Translate(CenterPoint * 1);

            return rotated;
        }
    }
}
