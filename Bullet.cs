using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Project_EM{
    class Bullet : Player{
        public float dir,fixY;
        public Bullet(float width, float dir,Player P) : base(width)
        {
            float r = 0.5f, g = 0.1f, b = 0.01f;
            float spec = 3f, dif = 2f;
            specular = new Vector4(r, g, b, 1f);
            diffuse = new Vector4(r / dif, g / dif, b / dif, 1f);
            ambient = new Vector4(r / spec, g / spec, b / spec, 1f);
            this.dir = dir;
            this.xFinal = P.xFinal+dir*P.playerWidth;
            this.yFinal = P.yFinal+0.1f;
            this.x = xFinal;
            this.y = yFinal;
            fixY = yFinal;
            this.environment = P.environment;
            this.speed.X = 0.3f;
            Console.WriteLine("Bullet created");
        }
        public void accelerateX()
        {
            accelerateX(dir);
            yFinal = fixY;
        }
    }
    
}
