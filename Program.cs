using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

/*
 https://capnramses.github.io//opengl/vertexbuffers.html
 http://genericgamedev.com/tutorials/opengl-in-csharp-an-object-oriented-introduction-to-opentk/3/
     */

namespace Project_EM
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g1 = new Game(800, 600, "project EM");
            //Point order for auto normal creation. -> ^ => pos z. comp
            /*
            g1.gameObject.Add(top);
            g1.gameObject.Add(bottom);
            g1.gameObject.Add(left);
            g1.gameObject.Add(right);*/
            Cube blokje = new Cube(new Vector3(0, 2, 0),0.2f);
            Cube blokje1 = new Cube(new Vector3(1, 0, 0), 1);
            Cube blokje2 = new Cube(new Vector3(2, 0, 0), 1);
            Cube blokje3 = new Cube(new Vector3(3, 0, 0), 1);

            Player p1 = new Player();
            //new Level
            Level l1 = new Level();
            g1.setLevel(l1);
            g1.setPlayer(p1);
            p1.setEnvironment(l1);
            p1.setPlayerStart(5, 5);

            g1.Run();
        }
    }
}
