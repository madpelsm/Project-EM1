using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Project_EM
{
    class Player
    {
        public List<Cube> cube;
        public Vector2 speed = new Vector2(0.1f, 0.1f);
        public float scale = 1f;
        public float x, y;
        public Player()
        {
            cube = new List<Cube>();
            genPlayer();
        }
        public void genPlayer()
        {
            x = 0;y = 0;
            Cube c1 = new Cube(new Vector3(0, 0, 0), 0.4f);
            cube.Add(c1);
        }
        public void draw()
        {
            if (cube != null)
            {
                foreach (Cube c in cube)
                {
                    GL.Translate(new Vector3(x, y, 0));
                    GL.Color3(new Vector3(0.3f, 0.9f, 0.8f));
                    c.draw();
                }
            }
        }
        public void moveX(float dir)
        {
            this.x += speed.X*dir;
        }
        public void moveY(float dir)
        {
            this.y += speed.Y*dir;
        }
    }
}
