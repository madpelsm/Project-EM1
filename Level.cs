using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Project_EM
{
    class LevelObject
    {
        public List<Cube> geometry { get; set; }
        public Vector4 diffuse, ambient, specular;
        public float dif = 0.4f, spec = 0.7f;
        public LevelObject(float width, int amountX, int amounY, Vector3 color, Vector3 Origin)
        {
            geometry = new List<Cube>();

            if (amountX == 1)
            {
                if (amounY == 1)
                {
                    Cube t = new Cube(new Vector3(Origin.X + 0, Origin.Y + 0, Origin.Z), width);
                    geometry.Add(t);
                    specular = new Vector4(color, 1f);
                    diffuse = new Vector4(color.X - dif, color.Y - dif, color.Z - dif, 1f);
                    ambient = new Vector4(color.X - spec, color.Y - spec, color.Z - spec, 1f);
                }
                else
                {
                    for (int j = -(int)amounY / 2; j < (amounY / 2); j++)
                    {
                        Cube t = new Cube(new Vector3(Origin.X + 0, Origin.Y + j, Origin.Z), width);
                        geometry.Add(t);
                        specular = new Vector4(color, 1f);
                        diffuse = new Vector4(color.X - dif, color.Y - dif, color.Z - dif, 1f);
                        ambient = new Vector4(color.X - spec, color.Y - spec, color.Z - spec, 1f);
                    }
                }
            }
            else
            {
                for (float i = -amountX / 2; i < amountX / 2; i++)
                {
                    if (amounY == 1)
                    {
                        Cube t = new Cube(new Vector3(Origin.X + i, Origin.Y + 0, Origin.Z), width);
                        geometry.Add(t);
                        specular = new Vector4(color, 1f);
                        diffuse = new Vector4(color.X - dif, color.Y - dif, color.Z - dif, 1f);
                        ambient = new Vector4(color.X - spec, color.Y - spec, color.Z - spec, 1f);
                    }
                    else
                    {
                        for (int j = -(int)amounY / 2; j < (amounY / 2); j++)
                        {
                            Cube t = new Cube(new Vector3(Origin.X + i, Origin.Y + j, Origin.Z), width);
                            geometry.Add(t);
                            specular = new Vector4(color, 1f);
                            diffuse = new Vector4(color.X - dif, color.Y - dif, color.Z - dif, 1f);
                            ambient = new Vector4(color.X - spec, color.Y - spec, color.Z - spec, 1f);
                        }
                    }
                }
            }
        }
        public void draw()
        {
            if (geometry != null)
            {
                foreach(Cube obj in geometry)
                {
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, ambient);
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, diffuse);
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, specular);
                    GL.Color3(specular.X, specular.Y, specular.Z);
                    obj.draw();
                }
            }
        }
    }
    class Level
    {
        private Vector4 grassDiffuse, grassAmbient, grassSpecular, stoneDiffuse, stoneAmbient, stoneSpecular;
        public List<LevelObject> levelObjects;
        private float dif = 0.4f;
        private float spec = 0.7f;
        public Level()
        {
            levelObjects = new List<LevelObject>();
            LevelObject Stone = new LevelObject(1f, 2, 3, new Vector3(.6f, .6f, .6f), new Vector3(1, 0, -1f));
            levelObjects.Add(Stone);
            levelObjects.Add(new LevelObject(1.1f, 1, 4, new Vector3(0.80f, 0.53f, 0.20f), new Vector3(1, 2, 0)));
            levelObjects.Add(new LevelObject(1.1f, 2, 2, new Vector3(0f, 0.53f, 0f), new Vector3(1.5f, 4, .2f)));
            levelObjects.Add(new LevelObject(1f, 50, 1, new Vector3(0f, 0.53f, 0f), new Vector3(0, -1, 0)));//gras
            levelObjects.Add(new LevelObject(1f, 50, 1, new Vector3(0f, 0.53f, 0f), new Vector3(0, -1, 1)));//gras
            levelObjects.Add(new LevelObject(0.5f, 2, 2, new Vector3(0.9f, 0.0f, 0f), new Vector3(1.5f, 4f, .8f)));
        }
        public void draw()
        {
            if (levelObjects != null)
            {
                foreach(LevelObject obj in levelObjects)
                {
                    obj.draw();
                }
            }
        }
    }
}
