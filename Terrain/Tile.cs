using System;
using OpenTK.Graphics.OpenGL;

namespace DTerrainGeneration.Terrain
{
    public class Tile
    {
        TileVertices vertices;
        double[] colors = { 1, .75,.5,.25 };
        public Tile(TileVertices vertices)
        {
            this.vertices = vertices;
        }

        public void render()
        {
            GL.Begin(BeginMode.QuadStrip);

            for (int i = 0; i < vertices.vertices.Length; i++)
            {
                GL.Color3(colors[i], colors[i], colors[i]);
                if (vertices.vertices[i] != null)
                {
                    GL.Vertex3(vertices.vertices[i].x, vertices.vertices[i].y, vertices.vertices[i].z);
                }
            }
            GL.End();
        }
    }
}
