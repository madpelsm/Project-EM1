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
        public System.Diagnostics.Stopwatch watch, watch2;
        public List<Cube> cubes;
        public int frames = 0;
        public float angle = 0;
        public string name;
        public Level l1;
        public Player P1;
        public float x, y,mouseX,mouseY,mouseLookSensitivity,cameraX,cameraZ;
        public bool cameraControl = false;
        public Vector2 lastMousePos = new Vector2();//so it isn't null :) 
        public Game(int width, int height, String title) : base(width, height, new OpenTK.Graphics.GraphicsMode(32, 8, 0, 0), title)
        {

            this.name = title;
            X = Screen.PrimaryScreen.Bounds.Width / 2 - width;
            Y = Screen.PrimaryScreen.Bounds.Height / 2 - height;
            w = width;
            h = height;
            //mouseX mouseY define the camera look at target in X and Y, Z=0
            mouseX = 3f;
            mouseY = 4f;
            //cameraX and cameraZ define the camera position in X and Z, Y is constant
            cameraX = 10f;
            cameraZ = 30f;
            mouseLookSensitivity = 0.01f;
            gameObject = new List<Plane3D>();
            cubes = new List<Cube>();
            //CursorVisible = false;
            //testObject aanmaken
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            watch2 = new System.Diagnostics.Stopwatch();
            watch2.Start();
            VSync = VSyncMode.On;
            //this.WindowBorder = WindowBorder.Hidden;
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
        protected override void OnResize(EventArgs e)
        {
        }
        protected void LightRender()
        {
            //Light 0
            Vector4 position = new Vector4(P1.x,P1.y,0f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Position, position);
            Vector4 ambient0 = new Vector4(0.7f, 0.7f,0.7f, 1.0f);
            Vector4 diffuse0 = new Vector4(.8f, .8f, .8f, 1.0f);
            Vector4 specular0 = new Vector4(0.7f, 0.7f, 0.7f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuse0);
            GL.Light(LightName.Light0, LightParameter.Specular, specular0);
            GL.Light(LightName.Light0, LightParameter.QuadraticAttenuation, 0.5f);
            //Light 1
            GL.Enable(EnableCap.Light1);
            float ff = 3f;
            Vector4 ambient = new Vector4(3f, 3f, 3f, 1.0f);
            Vector4 specular = new Vector4(ff, ff, ff, 1.0f);
            Vector4 diffuse = new Vector4(2f, 2f, 2f, 1f);
            GL.Light(LightName.Light1, LightParameter.Position, new Vector4(5f, 10f, 0f,1f));
            GL.Light(LightName.Light1, LightParameter.Ambient, ambient);
            GL.Light(LightName.Light1, LightParameter.Diffuse, diffuse);
            GL.Light(LightName.Light1, LightParameter.Specular, specular);
            GL.Light(LightName.Light1, LightParameter.QuadraticAttenuation, 0.02f);
            //Light 2
            GL.Enable(EnableCap.Light2);
            Vector4 ambient2 = new Vector4(4f, 4f, 4f, 1.0f);
            Vector4 specular2 = new Vector4(1f, 1f, 1f, 1.0f);
            Vector4 diffuse2 = new Vector4(7f, 7f, 7f, 1f);
            GL.Light(LightName.Light2, LightParameter.Position, new Vector4(10f, 10f, 0f, 1f));
            GL.Light(LightName.Light2, LightParameter.Ambient, ambient2);
            GL.Light(LightName.Light2, LightParameter.Diffuse, diffuse2);
            GL.Light(LightName.Light2, LightParameter.Specular, specular2);
            GL.Light(LightName.Light2, LightParameter.LinearAttenuation, 0.2f);
            //1GlobalLight
            GL.Enable(EnableCap.Light3);
            GL.Light(LightName.Light3, LightParameter.Position, new Vector4(200f, 200f, 200f, 1f));
            GL.Light(LightName.Light3, LightParameter.Ambient, new Vector4(1f, 1f, 1f, 1f));
            GL.Light(LightName.Light3, LightParameter.Diffuse, new Vector4(1f, 1f, 1f, 1f));
            GL.Light(LightName.Light3, LightParameter.Specular, new Vector4(1f, 1f,1f, 1f));
            GL.Light(LightName.Light3, LightParameter.LinearAttenuation, 0f);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {

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
            if (cameraControl) { 
                Vector2 mouseDelta = lastMousePos - new Vector2(Mouse.GetCursorState().X, Mouse.GetCursorState().Y);
                mouseX -= mouseLookSensitivity * mouseDelta.X;
                mouseY += mouseLookSensitivity * mouseDelta.Y;
                resetCursor();
            }
            cameraMove(Keyboard.GetState());
            updateCamera();
            setPlayerMove(Keyboard.GetState());
            P1.updatePhysics();
            //gamelogic

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.Dispose();
        }
        public void resetCursor()
        {
            OpenTK.Input.Mouse.SetPosition(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);
            lastMousePos = new Vector2(Mouse.GetCursorState().X, Mouse.GetCursorState().Y);
            
        }
        public void cameraMove(KeyboardState c)
        {
            if (c.IsKeyDown(Key.A))
            {
                cameraX--;
            }
            if (c.IsKeyDown(Key.S))
            {
                cameraZ++;
            }
            if (c.IsKeyDown(Key.W))
            {
                cameraZ--;
            }
            if (c.IsKeyDown(Key.D))
            {
                cameraX++;
            }
        }
        public void setPlayerMove(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Key.Up) || currentKeyboardState.IsKeyDown(Key.W) || currentKeyboardState.IsKeyDown(Key.Space))
            {
                
                    P1.Jump();
            }
            if (currentKeyboardState.IsKeyDown(Key.Down)|| currentKeyboardState.IsKeyDown(Key.S))
            {
                    P1.moveY(-1);
            }
            if (currentKeyboardState.IsKeyDown(Key.Right)|| currentKeyboardState.IsKeyDown(Key.D))
            {
                P1.decceleratorX.Restart();
                P1.accelerateX(1f);
            }
            if (currentKeyboardState.IsKeyDown(Key.Left) || currentKeyboardState.IsKeyDown(Key.A))
            {
                P1.decceleratorX.Restart();
                P1.accelerateX(-1f);
            }
            if (!(currentKeyboardState.IsKeyDown(Key.Left) || currentKeyboardState.IsKeyDown(Key.A))&&!(currentKeyboardState.IsKeyDown(Key.Right) || currentKeyboardState.IsKeyDown(Key.D)) )
            {
                
                P1.decelerateX();
                P1.acceleratorX.Restart();
            }
            if (currentKeyboardState.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
            if (currentKeyboardState.IsKeyDown(Key.F))
            {
                if (this.WindowState != WindowState.Fullscreen)
                {
                    this.WindowState = WindowState.Fullscreen;
                }else
                {
                    this.WindowState = WindowState.Normal;
                }
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
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(0.50f, (float)w / h, 1, 4000);
            GL.MultMatrix(ref perspective);

            // Setup Camera
            Matrix4 camera = Matrix4.LookAt(new Vector3(P1.x,1f,30f), new Vector3(P1.xFinal, P1.yFinal+1f, 0f), Vector3.UnitY);
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
            GL.Enable(EnableCap.PolygonSmooth);

        }
    }
}

