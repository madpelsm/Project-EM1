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
        public float gravity = 2f, jumpHeight = 1.4f, lastDirection, globalFriction = 0.001f, slipDuration = 300,accelDur = 400,accelFriction = 1f;
        private System.Diagnostics.Stopwatch physicsTimer, gravityTimer;
        
        public System.Diagnostics.Stopwatch acceleratorX, decceleratorX;
        private Vector4 ambient, diffuse, specular;
        public Player(float width)
        {
            playerWidth = width;
            cube = new List<Cube>();
            genPlayer();
            physicsTimer = new System.Diagnostics.Stopwatch();
            physicsTimer.Start();
            gravityTimer = new System.Diagnostics.Stopwatch();
            gravityTimer.Start();
            acceleratorX = new System.Diagnostics.Stopwatch();
            decceleratorX = new System.Diagnostics.Stopwatch();
            float r = 0.1f, g = 0.01f, b = 0.01f;
            float spec = 3f, dif = 2f;
            specular = new Vector4(r, g, b, 1f);
            diffuse = new Vector4(r/dif, g/dif, b/dif, 1f);
            ambient = new Vector4(r/spec, g/spec, b/spec, 1f);




        }
        public void genPlayer()
        {
            x = 0;y = 0;
            playerWidth = 0.8f;
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
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, new Vector4(0.05f, 0.05f, 0.05f, 0.5f));
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, ambient);
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, diffuse);
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, specular);
                    c.draw();
                }
            }
        }
        public void Jump()
        {
            
            if (physicsTimer != null)
            {
                if (physicsTimer.ElapsedMilliseconds > 1000)
                {
                    physicsTimer.Restart();
                }
                else
                {
                    float jump = 0.001f*(float)jumpHeight*(1000-physicsTimer.ElapsedMilliseconds);
                    moveY(jump);
                }
            }
        }
        public void updatePhysics()
        {
            float g = -gravity * (0.001f*gravityTimer.ElapsedMilliseconds);
            moveY(g);//let it fall downward
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
                        float zOfCube = c.MMP.Z;
                        xCollision = Math.Abs(x - xOfCube) * 2 <= (playerWidth + CubeWidth);
                        yCollision = Math.Abs(y - yOfCube) * 2 <= (playerWidth + CubeWidth);
                        bool zCol = -playerWidth / 2 <= zOfCube && zOfCube <= playerWidth / 2;
                        noCollision &= !(xCollision && yCollision&&zCol);
                        if (xCollision && yCollision)
                        {
                            physicsTimer.Restart();
                            
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("environment not set");
                //notify is environment was not set
            }
            return noCollision;
        }
        public void setEnvironment(Level l)
        {
            this.environment = l;
        }
        public void accelerateX(float dir)
        {
            if (acceleratorX != null)
            {
                acceleratorX.Start();
                if (acceleratorX.ElapsedMilliseconds > accelDur)
                {
                    acceleratorX.Stop();
                }
                float v = accelFriction * speed.X * acceleratorX.ElapsedMilliseconds/accelDur;
                moveX(dir*v);
                lastDirection = dir;
                
            }
        }
        public void decelerateX()
        {

            if (decceleratorX != null)
            {
                decceleratorX.Start();
                if (decceleratorX.ElapsedMilliseconds > slipDuration)
                {
                    decceleratorX.Stop();
                }
                else
                {
                    float v = globalFriction * speed.X * (slipDuration - decceleratorX.ElapsedMilliseconds);
                    if (v >= 0)
                    {
                        moveX(lastDirection * v);
                    }
                }

            }
        }
        public void moveX(float speedX)
        {
            this.x += speedX;
            if (canMakeMove())
            {
                xFinal = x;
            }
            else
            {
                this.x = xFinal;
                updatePhysics();
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
                gravityTimer.Restart();
            }
        }
    }
}
