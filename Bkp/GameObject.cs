using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Project_EM
{

    public class Plane3D
    {
        public Vector3[] P;
        public Vector3[] Color;
        public Vector3 Normal;
        public Vector4 ambient, diffuse, specular;
        public Plane3D(Vector3[] P, Vector3[] Color, Vector3 normal)
        {
            this.P = P;//bv P[0].x is x van Punt 1
            this.Color = Color;
            ambient= new Vector4(0.3f,0f,0f,1f);
            diffuse = new Vector4(0.5f,0.5f,0.5f, 1f);
            specular = new Vector4(1f,1f,1f, 1f);
            Normal = normal;
        }
        public void draw()
        {
            GL.Begin(PrimitiveType.TriangleStrip);
            for (int i = 0; i < P.Length; i++)
            {
                GL.Normal3(Cross(P[0],P[1]));
                //GL.Color3(Color[i].X, Color[i].Y, Color[i].Z);
                GL.Vertex3(P[i].X, P[i].Y, P[i].Z);
            }
            GL.End();
        }
        public Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = Vector3.Cross(v1, v2);
            return Vector3.Normalize(v3);
        }
        public Vector3[] TranslatePlane(Vector3 displacement, Vector3[] P)
        {
            Vector3[] newP = new Vector3[P.Length];
            for (int i = 0; i < P.Length; i++)//plane bestaat uit i punten, verplaats elk punt met displacement
            {
                newP[i].X = P[i].X + displacement.X;
                newP[i].Y = P[i].Y + displacement.Y;
                newP[i].Z = P[i].Z + displacement.Z;
            }
            return newP;
        }
        public Vector3[] RotateX(float angle, Vector3[] P)
        {
            Vector3[] newP = new Vector3[P.Length];
            for (int i = 0; i < P.Length; i++)
            {
                newP[i] = Matrix3.CreateRotationX(angle)*P[i];
            }
            return newP;
        }
    }
    class Cube
    {
        public Cubic3D cube;
        public Vector3 MMP;
        public float width;
        public Cube(Vector3 MMP, float width)
        {
            cube = new Cubic3D(width, width, width, MMP);
            this.MMP = MMP;
            this.width = width;
        }
        public void draw()
        {
            cube.back.draw();
            cube.front.draw();
            cube.left.draw();
            cube.right.draw();
            cube.top.draw();
            cube.bottom.draw();
        }
        }
    
    class Cubic3D
    {
        public Plane3D front, right, top, back, left, bottom;

        public Cubic3D(float width, float height, float depth, Vector3 MMP)
        {
            Plane3D FRONT = new Plane3D(new Vector3[] {//Plane in XY with MMP origin
                    new Vector3((float)-width/2,(float)-height/2,0),
                    new Vector3((float)width/2,(float)-height/2,0),
                    new Vector3((float)-width/2,(float)height/2,0),
                    new Vector3((float)width/2,(float)height/2,0)
                }, new Vector3[] {
                    new Vector3(1,1,1),
                    new Vector3(1, 1, 1),
                    new Vector3(1,1,1),
                    new Vector3(1, 1, 1)
                },new Vector3(0,0,1));
            Plane3D RIGHT = new Plane3D(new Vector3[] {//Plane in YZ with MMP origin
                    new Vector3(0,-height/2,depth/2),
                    new Vector3(0, -height / 2, -depth / 2),
                    new Vector3(0, height / 2, depth / 2),
                    new Vector3(0, height / 2, -depth / 2)
            }, new Vector3[] {
                    new Vector3(1,1,1),
                    new Vector3(1, 1, 1),
                    new Vector3(1,1,1),
                    new Vector3(1, 1, 1)
                }, new Vector3(1, 0, 0));
            Plane3D TOP = new Plane3D(new Vector3[] {//Plane in XZ with MMP origin
                    new Vector3(width/2,0,depth/2),
                    new Vector3(width/2,0,-depth/2),
                    new Vector3(-width/2,0,depth/2),
                    new Vector3(-width/2,0,-depth/2)
            }, new Vector3[] {
                    new Vector3(1,1,1),
                    new Vector3(1, 1, 1),
                    new Vector3(1,1,1),
                    new Vector3(1, 1, 1)
                }, new Vector3(0, 1, 0));
            front = new Plane3D(TranslatePlane(MMP, TranslatePlane(new Vector3(0,0,(float)depth / 2), FRONT.P)), FRONT.Color, new Vector3(0, 0, 1));
            right = new Plane3D(TranslatePlane(MMP, TranslatePlane(new Vector3((float)width / 2,0,0), RIGHT.P)), RIGHT.Color, new Vector3(1, 0, 0));
            top = new Plane3D(TranslatePlane(MMP, TranslatePlane(new Vector3(0, (float)height/2, 0), TOP.P)), TOP.Color, new Vector3(0, 1, 0));
            bottom = new Plane3D(TranslatePlane(MMP, TranslatePlane(new Vector3(0,-(float)height/2,0),RotateX((float)Math.PI, TOP.P))), TOP.Color, new Vector3(0, 1, 0));
            left = new Plane3D(TranslatePlane(MMP, TranslatePlane(new Vector3(-width/2,0,0),RotateZ((float)Math.PI, RIGHT.P))), RIGHT.Color, new Vector3(1, 0, 0));
            back = new Plane3D(TranslatePlane(MMP, TranslatePlane(new Vector3(0,0,-depth/2),RotateZ((float)Math.PI, FRONT.P))), FRONT.Color, new Vector3(0, 0, 1));

        }
        public Vector3[] TranslatePlane(Vector3 displacement, Vector3[] P)
        {
            Vector3[] newP = new Vector3[P.Length];
            for (int i = 0; i < P.Length; i++)//plane bestaat uit i punten, verplaats elk punt met displacement
            {
                newP[i].X = P[i].X + displacement.X;
                newP[i].Y = P[i].Y + displacement.Y;
                newP[i].Z = P[i].Z + displacement.Z;
            }
            return newP;
        }
        public Vector3[] RotateX(float angle, Vector3[] P)
        {
            Vector3[] newP = new Vector3[P.Length];
            for (int i = 0; i < P.Length; i++)
            {
                newP[i] = Matrix3.CreateRotationX(angle) * P[i];
            }
            return newP;
        }
        public Vector3[] RotateY(float angle, Vector3[] P)
        {
            Vector3[] newP = new Vector3[P.Length];
            for (int i = 0; i < P.Length; i++)
            {
                newP[i] = Matrix3.CreateRotationY(angle) * P[i];
            }
            return newP;
        }
        public Vector3[] RotateZ(float angle, Vector3[] P)
        {
            Vector3[] newP = new Vector3[P.Length];
            for (int i = 0; i < P.Length; i++)
            {
                newP[i] = Matrix3.CreateRotationZ(angle) * P[i];
            }
            return newP;
        }

    }
}
