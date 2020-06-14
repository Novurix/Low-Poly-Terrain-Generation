using System;
using OpenTK;

namespace DTerrainGeneration
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            GameWindow window = new GameWindow(1000,720);
            new Game(window);
        }
    }
}
