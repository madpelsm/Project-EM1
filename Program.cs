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

            Player p1 = new Player(0.6f); // define player width here
            //new Level init
            Level l1 = new Level();
            //design level
            LevelObject Stone = new LevelObject(1f, 2, 3, new Vector3(.6f, .6f, .6f), new Vector3(1, 0, -1f));//steen
            l1.Add(Stone);
            l1.Add(new LevelObject(1f, 1, 4, new Vector3(0.20f, 0.07f, 0.03f), new Vector3(2f, 0, 0)));//stam
            l1.Add(new LevelObject(1f, 2, 2, new Vector3(0f, 0.15f, 0f), new Vector3(1.5f, 4, .2f)));//blaadjes
            l1.Add(new LevelObject(2f, 1f, 1f, 70, 1, new Vector3(0f, 0.06f, 0f), new Vector3(1, 0, 0)));//gras
            l1.Add(new LevelObject(2f, 1f, 1f, 70, 1, new Vector3(0f, 0.06f, 0f), new Vector3(0, 0, -1)));
            l1.Add(new LevelObject(2f, 1f, 1f, 70, 1, new Vector3(0f, 0.1f, 0f), new Vector3(0, 0, 1)));//gras
            l1.Add(new LevelObject(2f, 1f, 1f, 70, 1, new Vector3(0f, 0.1f, 0f), new Vector3(1, 0, -1)));
            l1.Add(new LevelObject(2f, 1f, 1f, 70, 1, new Vector3(0f, 0.1f, 0f), new Vector3(0, 0, 0)));//gras
            l1.Add(new LevelObject(0.35f, 2, 2, new Vector3(0.5f, 0.0f, 0f), new Vector3(1.5f, 4f, .8f)));
            l1.Add(new LevelObject(1, 5, 1, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(12f, 0f, 0f)));//stones
            l1.Add(new LevelObject(1, 4, 1, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(13f, 1f, 0f)));
            l1.Add(new LevelObject(1, 3, 1, new Vector3(0.08f, 0.1f, 0.1f), new Vector3(14f, 2f, 0f)));
            l1.Add(new LevelObject(1, 2, 1, new Vector3(0.08f, 0.1f, 0.1f), new Vector3(15f, 3f, 0f)));
            l1.Add(new LevelObject(1, 1, 1, new Vector3(0.08f, 0.1f, 0.1f), new Vector3(16f, 4f, 0f)));
            l1.Add(new LevelObject(1, 1, 2, new Vector3(0.05f, 0.1f, 0.00f), new Vector3(17f, 1f, 0.0f)));//bush
            l1.Add(new LevelObject(1, 1, 1, new Vector3(0.05f, 0.1f, 0.00f), new Vector3(18f, 1f, 0.0f)));
            l1.Add(new LevelObject(1, 1, 1, new Vector3(0.05f, 0.05f, 0.00f), new Vector3(19f, 3f, 0.0f)));
            l1.Add(new LevelObject(1, 1, 2, new Vector3(0.09f, 0.01f, 0.00f), new Vector3(19f, 1f, -1f)));
            l1.Add(new LevelObject(0.4f, 1, 1, new Vector3(0.21f, 0.01f, 0.00f), new Vector3(19f, 3f, 0.4f)));
            l1.Add(new LevelObject(1f, 1, 1, new Vector3(0.4f, 0.4f, 0.6f), new Vector3(20f, 4f, 0f)));
            l1.Add(new LevelObject(0.4f, 1, 2, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(17f, 1f, 0.4f)));//cherries
            //E
            l1.Add(new LevelObject(1f, 3, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(22f, 1f, 0f)));
            l1.Add(new LevelObject(1f, 1, 4, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(22f, 2f, 0f)));
            l1.Add(new LevelObject(1f, 2, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(23f, 3f, 0f)));
            l1.Add(new LevelObject(1f, 2, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(23f, 5f, 0f)));
            //L

            l1.Add(new LevelObject(1f, 3, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(26f, 1f, 0f)));
            l1.Add(new LevelObject(1f, 1, 4, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(26f, 2f, 0f)));
            //i
            l1.Add(new LevelObject(1f, 1, 5, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(30f, 1f, 0f)));
            //N
            l1.Add(new LevelObject(1f, 1, 5, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(32f, 1f, 0f)));
            l1.Add(new LevelObject(1f, 1, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(33f, 4f, 0f)));
            l1.Add(new LevelObject(1f, 1, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(34f, 3f, 0f)));
            l1.Add(new LevelObject(1f, 1, 5, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(35f, 1f, 0f)));
            //E
            l1.Add(new LevelObject(1f, 3, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(37f, 1f, 0f)));
            l1.Add(new LevelObject(1f, 1, 4, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(37f, 2f, 0f)));
            l1.Add(new LevelObject(1f, 2, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(38f, 3f, 0f)));
            l1.Add(new LevelObject(1f, 2, 1, new Vector3(0.3f, 0.0f, 0.00f), new Vector3(38f, 5f, 0f)));

            l1.Add(new LevelObject(1, 1, 4, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(50f, 0f, -0.5f)));
            l1.Add(new LevelObject(1, 1, 4, new Vector3(0.2f, 0.2f, 0.2f), new Vector3(50.5f, 0f, 0f)));
            g1.setLevel(l1);
            //add player to the gamescreen
            g1.setPlayer(p1);
            //set environment for the player
            p1.setEnvironment(l1);
            //set the init player position
            p1.setPlayerStart(5, 5);

            g1.Run();
        }
    }
}
