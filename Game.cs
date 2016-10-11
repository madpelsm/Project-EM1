using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using OpenTK.Input;

namespace Project_EM
{
    class Game : OpenTK.GameWindow
    {
        public List<Plane3D> gameObject;
        public int w, h;
        public Plane3D testObject;
        public System.Diagnostics.Stopwatch watch, watch2, spamTimer;
        public List<Cube> cubes;
        public int frames = 0;
        public float angle = 0;
        public string name;
        public Level l1;
        public Player P1;
        public float x, y;
        public Game(int width, int height, String title) : base(width, height, new OpenTK.Graphics.GraphicsMode(32, 8, 0, 0), title)
        {
            X = Screen.PrimaryScreen.Bounds.Width / 2 - width;
            Y = Screen.PrimaryScreen.Bounds.Height / 2 - height;
            w = width;
            h = height;
            this.name = title;
            gameObject = new List<Plane3D>();
            cubes = new List<Cube>();
            spamTimer = new System.Diagnostics.Stopwatch();
            spamTimer.Start();
            this.KeyDown += window_KeyDown;

            //testObject aanmaken
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            watch2 = new System.Diagnostics.Stopwatch();
            watch2.Start();
            VSync = VSyncMode.On;
            x = 0; y = 0;


        }
        public void setPlayer(Player P1)
        {
            this.P1 = P1;
        }
        public void setLevel(Level l1)
        {
            this.l1 = l1;
        }
        void window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            
        }
        protected override void OnResize(EventArgs e)
        {
        }
        protected void LightRender()
        {
            //Light 0
            Vector4 position = new Vector4(P1.x,P1.y,0f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Position, position);
            Vector4 ambient0 = new Vector4(5f, 5f, 5f, 1.0f);
            Vector4 diffuse0 = new Vector4(8f, 8f, 8f, 1.0f);
            Vector4 specular0 = new Vector4(1f, 1f, 1f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuse0);
            GL.Light(LightName.Light0, LightParameter.Specular, specular0);
            GL.Light(LightName.Light0, LightParameter.SpotDirection,new Vector4(0,-1,0,1));
            GL.Light(LightName.Light0, LightParameter.QuadraticAttenuation, 0.5f);
            //Light 1
            GL.Enable(EnableCap.Light1);
            Vector4 ambient = new Vector4(5f, 5f, 5f, 1.0f);
            Vector4 specular = new Vector4(9, 9, 9, 1.0f);
            Vector4 diffuse = new Vector4(6, 6, 6, 1f);
            GL.Light(LightName.Light1, LightParameter.Position, new Vector4(2f, 8f, 0f,1f));
            GL.Light(LightName.Light1, LightParameter.Ambient, ambient);
            GL.Light(LightName.Light1, LightParameter.Diffuse, diffuse);
            GL.Light(LightName.Light1, LightParameter.Specular, specular);
            GL.Light(LightName.Light1, LightParameter.QuadraticAttenuation, 0.5f);
            //Light 2
            GL.Enable(EnableCap.Light2);
            Vector4 ambient2 = new Vector4(2f, 2f, 2f, 1.0f);
            Vector4 specular2 = new Vector4(6f, 6f, 6f, 1.0f);
            Vector4 diffuse2 = new Vector4(5f, 5f, 5f, 1f);
            GL.Light(LightName.Light2, LightParameter.Position, new Vector4(10f, 10f, 0f, 1f));
            GL.Light(LightName.Light2, LightParameter.Ambient, ambient2);
            GL.Light(LightName.Light2, LightParameter.Diffuse, diffuse2);
            GL.Light(LightName.Light2, LightParameter.Specular, specular2);
            GL.Light(LightName.Light2, LightParameter.LinearAttenuation, 0.5f);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            updateCamera();
            GL.ClearColor(Color.SkyBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);// initiate modelview
            GL.LoadIdentity(); //load as identity Use GL.MultMatrix(ref TransformationMatrix);
                               //Do transformations on Object
                               //Render object
            float deltaT = watch2.ElapsedMilliseconds;
            angle +=deltaT/1000;
            x = (float)Math.Sin(angle)*10;
            y = (float)Math.Cos(angle)*10;
            watch2.Restart();
            GL.PushMatrix();
            LightRender();
            GL.PopMatrix();
            //GL.Rotate(angle, Vector3.UnitX);
            //GL.Rotate((float)angle,Vector3.UnitY);
            if (gameObject != null)
            {
                if (gameObject.Count != 0)
                {
                    foreach (Plane3D o in gameObject)
                    {
                        o.draw();
                    }
                }
            }
            GL.LoadIdentity();
            //Matrix4 rot = Matrix4.CreateRotationY(angle);
            //GL.MultMatrix(ref rot);
            if (cubes != null)
            {
                if (cubes.Count != 0)
                {
                    foreach (Cube o in cubes)
                    {
                        o.draw();
                    }
                }
            }
            GL.PopMatrix();
            if (l1 != null)
            {
                l1.draw();
            }
            if(P1!= null)
            {
                P1.draw();
            }
            frames++;
            

            /*
            GL.Begin(BeginMode.TriangleFan);
            GL.Vertex3(-1, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(0, 1, 0);
            GL.End();
            GL.Color3(Color.Blue);
            GL.Translate(-1, -1, -2);
            GL.Begin(BeginMode.TriangleFan);
            GL.Vertex3(-1, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(0, 1, 0);
            GL.End();
            */

            this.SwapBuffers();
            if (watch.ElapsedMilliseconds > 1000)
            {
                
                Console.WriteLine(frames);

                this.Title = name + " " + frames + "FPS";
                frames = 0;
                watch.Restart();  
            }
            //render
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            setPlayerMove(Keyboard.GetState());
            P1.updatePhysics();
            //gamelogic

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.Dispose();
        }
        public void setPlayerMove(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Key.Up) || currentKeyboardState.IsKeyDown(Key.Space))
            {
                if (spamTimer.ElapsedMilliseconds > 100)
                {
                    P1.Jump();
                }
                spamTimer.Restart();
                //P1.updatePhysics();
                //P1.moveY(1);
            }
            if (currentKeyboardState.IsKeyDown(Key.Down))
            {
                P1.moveY(-1);
            }
            if (currentKeyboardState.IsKeyDown(Key.Right))
            {
                P1.moveX(1);
            }
            if (currentKeyboardState.IsKeyDown(Key.Left))
            {
                P1.moveX(-1);
            }
            if (currentKeyboardState.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }
        public void updateCamera()
        {
            int w = Width;
            int h = Height;
            // Initialise the projection view matrix
            //GL.MatrixMode(MatrixMode.Projection);
            GL.MatrixMode(MatrixMode.Projection);
            // Setup a perspective view
            GL.LoadIdentity();
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)w / h, 1, 4000);
            GL.MultMatrix(ref perspective);

            // Setup Camera

            Matrix4 camera = Matrix4.LookAt(new Vector3(10f, 4f, 30f), new Vector3(8f, 3f, 0f), Vector3.UnitY);
            GL.MultMatrix(ref camera);
            // So far Projection * Camera * modelView (which is identity up to now)

            // Set the viewport to the whole window
            GL.Viewport(0, 0, w, h);

            //Place lights
        }
        protected override void OnClosed(EventArgs e)
        {
            this.Dispose();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.ShadeModel(ShadingModel.Smooth);
        }
    }
}

