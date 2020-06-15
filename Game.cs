using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

using DTerrainGeneration.Terrain;

namespace DTerrainGeneration
{
    public class Game
    {
        int perlinScale = 100, verticesPerPlain = 4, areaOfMap, verticesIndex, groupVertID = 0, grassTexture;
        Vec3D[] vertices;
        Tile[] tiles;
        TileVertices[] tileVertices;

        PerlinNoise noise;
        float[] heightMaps;

        GameWindow window;
        public Game(GameWindow window) {
            this.window = window;

            areaOfMap = perlinScale * perlinScale * verticesPerPlain;

            vertices = new Vec3D[areaOfMap];
            tileVertices = new TileVertices[perlinScale * perlinScale];

            noise = new PerlinNoise(perlinScale);
            tiles = new Tile[perlinScale * perlinScale];

            start();
        }

        void start() {

            GL.Rotate(90, 0, 1, 0);
            GL.Translate(10, -20, -55);

            // Creates height map
            heightMaps = noise.generateHeightIndexs();

            for (int x = 0; x < Math.Sqrt(areaOfMap); x++) {
                for (int z = 0; z < Math.Sqrt(areaOfMap); z++) {
                    vertices[verticesIndex] = new Vec3D(x*2, heightMaps[verticesIndex], z*2);
                    verticesIndex++;
                }
            }

            // Groups 4 vertices into one to render a tile on
            for (int vert = 0; vert < tileVertices.Length; vert++)  {
                Vec3D[] verts = new Vec3D[verticesPerPlain];

                verts[0] = vertices[groupVertID];
                verts[1] = vertices[groupVertID+1];

                if (vertices[groupVertID + (int)Math.Sqrt(areaOfMap)].x == vertices[groupVertID + (int)Math.Sqrt(areaOfMap) + 1].x)
                {
                    verts[2] = vertices[groupVertID + (int)Math.Sqrt(areaOfMap)];
                    verts[3] = vertices[groupVertID + (int)Math.Sqrt(areaOfMap) + 1];
                }

                groupVertID++;

                try {
                    tileVertices[vert] = new TileVertices(verts);
                    tiles[vert] = new Tile(tileVertices[vert]);
                }catch(Exception exception) {}
            }

            window.RenderFrame += render;
            window.Resize += resize;
            window.Load += load;

            window.Run(1 / 60);
        }

        private void render(object sender, EventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            for (int tile = 0; tile < tiles.Length-1; tile++)
            {
                if (tiles[tile] != null) {
                    tiles[tile].render();
                }
            }

            window.SwapBuffers();
        }

        private void resize(object sender, EventArgs e) {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(1, window.Width / window.Height, 1.0f, 1000.0f);
            GL.LoadMatrix(ref perspectiveMatrix);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.End();
        }

        private void load(object sender, EventArgs e) {
            GL.ClearColor(0, 0, 0, 0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);

            float[] lightPos = { 20f, 20, 80f };
            float[] lightDiffuse = { .1f, .1f, .1f };
            float[] lightAmbient = { .3f, .5f, .6f };

            GL.Light(LightName.Light0, LightParameter.Position, lightPos);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightDiffuse);
            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);

            GL.Enable(EnableCap.Light0);
        }
    }
}
