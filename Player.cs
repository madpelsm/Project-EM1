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
        public float x, y,playerWidth,xFinal,yFinal;
        public Level environment;
        public float gravity = 3f,jumpHeight =3f;
        private System.Diagnostics.Stopwatch physicsTimer,gravityTimer;
        public Player()
        {
            cube = new List<Cube>();
            genPlayer();
            gravityTimer = new System.Diagnostics.Stopwatch();
            gravityTimer.Start();
        }
        public void genPlayer()
        {
            x = 0;y = 0;
            playerWidth = 0.5f;
            //always generate in origin, translate in draw
            Cube c1 = new Cube(new Vector3(0, 0, 0),playerWidth);
            cube.Add(c1);
        }
        public void setPlayerStart(float X, float Y)
        {
            xFinal = X;yFinal = Y;
            x = X;y = Y;
        }
        public void draw()
        {
            if (cube != null)
            {
                foreach (Cube c in cube)
                {
                    GL.Translate(new Vector3(xFinal, yFinal, 0));
                    GL.Color3(new Vector3(0.3f, 0.9f, 0.8f));
                    c.draw();
                }
            }
        }
        public void Jump()
        {
            physicsTimer = new System.Diagnostics.Stopwatch();
            physicsTimer.Start();

        }
        public void updatePhysics()
        {
            if (physicsTimer != null)
            {
                if (physicsTimer.ElapsedMilliseconds > 100)
                {
                    physicsTimer = null;
                }
                else
                {
                    moveY((float)jumpHeight*(1- 1*(physicsTimer.ElapsedMilliseconds / 1000)));
                }
            }
            moveY(-gravity*gravityTimer.ElapsedMilliseconds/1000);//let it fall downward
        }
        public bool canMakeMove()//check if a certain move can be done
        {
            bool noCollision = true;
            bool xCollision = false;
            bool yCollision = false;
            if (environment != null)
            {
                foreach (LevelObject levelObject in environment.levelObjects)//A level contains objects that make the environment
                {
                    foreach (Cube c in levelObject.geometry)//get the cubes making up said object
                    {
                        //Since we defined cubes with a MMP and a width, we can easily get that MMP en width to match for collision
                        //collision in X-axis
                        float CubeWidth = c.width;
                        float xOfCube = c.MMP.X;
                        float yOfCube = c.MMP.Y;
                        xCollision = Math.Abs(x - xOfCube) * 2 <= (playerWidth + CubeWidth);
                        yCollision = Math.Abs(y - yOfCube) * 2 <= (playerWidth + CubeWidth);
                        if (yCollision && xCollision)
                        {
                            gravityTimer.Restart();
                        }
                        noCollision &= !(xCollision && yCollision);
                    }
                }
            }
            else
            {
                Console.WriteLine("environment not set");
                //notify is environment was not set
            }
            if (!noCollision)
            {
            }
            return noCollision;
        }
        public void setEnvironment(Level l)
        {
            this.environment = l;
        }
        public void moveX(float dir)
        {
            this.x += speed.X*dir;
            if (canMakeMove())
            {
                xFinal = x;
            }
            else
            {
                this.x = xFinal;
            }
        }
        public void moveY(float dir)
        {
            this.y += speed.Y*dir;
            if (canMakeMove())
            {
                yFinal = this.y;
            }
            else
            {
                this.y = yFinal;
            }
        }
    }
}
