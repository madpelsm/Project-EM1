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
        public float dif = 2f, spec = 3f;
        public LevelObject(float spacingX,float spacingY, float width, int amountX, int amountY, Vector3 color, Vector3 Origin)
        {
            geometry = new List<Cube>();

            
                for (float i = 0; i < amountX; i++)
                {
                        for (int j = 0; j < amountY; j++)
                        {
                            Cube t = new Cube(new Vector3(Origin.X + i*spacingX, Origin.Y + j*spacingY, Origin.Z), width);
                            geometry.Add(t);
                            specular = new Vector4(color, 1f);
                            diffuse = new Vector4(color.X / dif, color.Y / dif, color.Z / dif, 1f);
                            ambient = new Vector4(color.X / spec, color.Y / spec, color.Z / spec, 1f);
                        }
                    
                }
            }
        public LevelObject(float width, int amountX, int amountY, Vector3 color, Vector3 Origin): this(1,1, width, amountX, amountY, color, Origin)
        {
            
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
        public void Add(LevelObject o)
        {
            levelObjects.Add(o);
        }
    }
}
