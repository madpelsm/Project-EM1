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
        public LevelObject(float width, int amountX, int amounY, Vector3 color, Vector3 Origin)
        {
            geometry = new List<Cube>();

            
                for (float i = -(int)Math.Floor(1.0*amountX / 2); i < Math.Ceiling(1.0*amountX / 2); i++)
                {
                    
                        for (int j = -(int)Math.Floor(1.0*amounY / 2); j < Math.Ceiling(1.0*amounY / 2); j++)
                        {
                            Cube t = new Cube(new Vector3(Origin.X + i, Origin.Y + j, Origin.Z), width);
                            geometry.Add(t);
                            specular = new Vector4(color, 1f);
                            diffuse = new Vector4(color.X / dif, color.Y / dif, color.Z / dif, 1f);
                            ambient = new Vector4(color.X / spec, color.Y / spec, color.Z / spec, 1f);
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
            LevelObject Stone = new LevelObject(1f, 2, 3, new Vector3(.6f, .6f, .6f), new Vector3(1, 0, -1f));//steen
            levelObjects.Add(Stone);
            levelObjects.Add(new LevelObject(1f, 1, 4, new Vector3(0.20f, 0.07f, 0.03f), new Vector3(1, 2, 0)));//stam
            levelObjects.Add(new LevelObject(1f, 2, 2, new Vector3(0f, 0.15f, 0f), new Vector3(1.5f, 4, .2f)));//blaadjes
            levelObjects.Add(new LevelObject(1f, 50, 1, new Vector3(0f, 0.1f, 0f), new Vector3(25, -1, 0)));//gras
            levelObjects.Add(new LevelObject(1f, 50, 1, new Vector3(0f, 0.1f, 0f), new Vector3(25, -1, 1)));//gras
            levelObjects.Add(new LevelObject(0.35f, 2, 2, new Vector3(0.5f, 0.0f, 0f), new Vector3(1.5f, 4f, .8f)));
            levelObjects.Add(new LevelObject(1, 5, 1, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(12f, 0f, 0f)));//stones
            levelObjects.Add(new LevelObject(1, 4, 1, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(13f, 1f, 0f)));
            levelObjects.Add(new LevelObject(1, 3, 1, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(13f, 2f, 0f)));
            levelObjects.Add(new LevelObject(1, 2, 1, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(14f, 3f, 0f)));
            levelObjects.Add(new LevelObject(1, 1, 1, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(14f, 4f, 0f)));
            Box2d a = new Box2d(2, 2, 2, 2);

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
