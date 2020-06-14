using System;
namespace DTerrainGeneration.Terrain
{
    public class TileVertices
    {
        public Vec3D[] vertices { get; }

        public TileVertices(Vec3D[] verts) {
            vertices = verts;
        }
    }
}
