using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace RenderSharp.Library
{
    class Mesh
    {
        private List<Matrix> vertices = new List<Matrix>();
        public List<Matrix> Vertices
        {
            get
            {
                return vertices;
            }
            set
            {
                vertices = value;
            }
        }
        public Matrix CenterPoint;


        public Mesh()
        {
            CenterPoint = new Matrix(1, 3);
        }


        public Mesh(long x, long y, long z) : this()
        {
            CenterPoint[0, 0] = x;
            CenterPoint[0, 1] = y;
            CenterPoint[0, 2] = z;

            //Matrix m = new Matrix(1, 3);
            //m[0, 0] = x;
            //m[0, 1] = y;
            //m[0, 2] = z;
            //vertices.Add(m);
        }


        public void Render(Graphics g)
        {
            Pen pen = new Pen(Color.Red, 4.0f);
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                Point a = vertices[i];
                Point b = vertices[i + 1];
                Console.WriteLine("BEFORE : x {0} y {1}, Point B: x {2} y {3}", a.X, a.Y, b.X, b.Y);
                a.X = (int)(((vertices[i][0, 0] - this.CenterPoint[0, 0]) / vertices[i][0, 2]) + this.CenterPoint[0, 0]);
                a.Y = (int)(((vertices[i][0, 1] - this.CenterPoint[0, 1]) / vertices[i][0, 2]) + this.CenterPoint[0, 1]);
                b.X = (int)(((vertices[i + 1][0, 0] - this.CenterPoint[0, 0]) / vertices[i + 1][0, 2]) + this.CenterPoint[0, 0]);
                b.Y = (int)(((vertices[i + 1][0, 1] - this.CenterPoint[0, 1]) / vertices[i][0, 2] )+ this.CenterPoint[0, 1]);
                //a.Y = (int)(((double)a.Y) / vertices[i][0, 2]);
                //b.X = (int)(((double)b.X) / vertices[i + 1][0, 2]);
                //b.Y = (int)(((double)b.Y) / vertices[i + 1][0, 2]);
                Console.WriteLine("Point A: x {0} y {1}, Point B: x {2} y {3}", a.X, a.Y, b.X, b.Y);
                g.DrawLine(pen, a, b);
            }
        }


        public void AddVertex(Matrix m)
        {
            vertices.Add(m);
        }


        public void AddVertex(long x, long y, long z)
        {
            Matrix m = new Matrix(1, 3);
            m[0, 0] = x;
            m[0, 1] = y;
            m[0, 2] = z;
            vertices.Add(m);
        }


        public static Mesh operator *(Mesh p, Matrix m)
        {
            Mesh newP = new Mesh();

            for (int i = 0; i < p.vertices.Count; i++)
            {
                newP.AddVertex(p.vertices.ElementAt(i) * m);
            }

            return newP;
        }

        public static Mesh operator *(Matrix m, Mesh p)
        {
            Mesh newP = new Mesh();

            for (int i = 0; i < p.vertices.Count; i++)
            {
                newP.AddVertex(m * p.vertices.ElementAt(i));
            }

            return newP;
        }


        public Mesh Clone()
        {
            Mesh newP = new Mesh();
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


        public void Translate(double x, double y, double z)
        {
            foreach (Matrix vertex in vertices)
            {
                vertex[0, 0] += x;
                vertex[0, 1] += y;
                vertex[0, 2] += z;
            }
        }


        public void Translate(Matrix m)
        {
            foreach (Matrix vertex in vertices)
            {
                vertex[0, 0] += m[0, 0];
                vertex[0, 1] += m[0, 1];
                vertex[0, 2] += m[0, 2];
            }
        }


        public Mesh GetRotated(double theta)
        {
            Mesh rotated = this.Clone();

            rotated.Translate(CenterPoint * -1);

            Matrix rot = new Matrix(3, 3);
            rot.SetRow(0, new double[] { Math.Cos(theta), 0, Math.Sin(theta) });
            rot.SetRow(1, new double[] { 0, 1, 0});
            rot.SetRow(2, new double[] { -Math.Sin(theta), 0, Math.Cos(theta) });

            rotated = rot * rotated;

            rotated.Translate(CenterPoint * 1);

            return rotated;
        }
    }
}
