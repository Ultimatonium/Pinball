using UnityEngine;

public class Plane
{
    public static Mesh GenerateMesh(double length, double width, int resolutionX, int resolutionZ)
    {
        #region Vertices
        VectorXYZ[] vertices = new VectorXYZ[resolutionX * resolutionZ];
        for (int z = 0; z < resolutionZ; z++)
        {
            // [ -length / 2, length / 2 ]
            double zPos = ((float)z / (resolutionZ - 1) - .5f) * length;
            for (int x = 0; x < resolutionX; x++)
            {
                // [ -width / 2, width / 2 ]
                double xPos = ((float)x / (resolutionX - 1) - .5f) * width;
                vertices[x + z * resolutionX] = new VectorXYZ(xPos, 0f, zPos);
            }
        }
        #endregion

        #region Normales
        VectorXYZ[] normales = new VectorXYZ[vertices.Length];
        for (int n = 0; n < normales.Length; n++)
            normales[n] = VectorXYZ.up;
        #endregion

        #region UVs
        VectorXY[] uvs = new VectorXY[vertices.Length];
        for (int v = 0; v < resolutionZ; v++)
        {
            for (int u = 0; u < resolutionX; u++)
            {
                uvs[u + v * resolutionX] = new VectorXY((float)u / (resolutionX - 1), (float)v / (resolutionZ - 1));
            }
        }
        #endregion

        #region Triangles
        int nbFaces = (resolutionX - 1) * (resolutionZ - 1);
        int[] triangles = new int[nbFaces * 6];
        int t = 0;
        for (int face = 0; face < nbFaces; face++)
        {
            int i = face % (resolutionX - 1) + (face / (resolutionZ - 1) * resolutionX);

            triangles[t++] = i + resolutionX;
            triangles[t++] = i + 1;
            triangles[t++] = i;

            triangles[t++] = i + resolutionX;
            triangles[t++] = i + resolutionX + 1;
            triangles[t++] = i + 1;
        }
        #endregion

        return new Mesh
        {
            vertices = VectorXYZ.ToVector3(vertices),
            normals = VectorXYZ.ToVector3(normales),
            uv = VectorXY.ToVector2(uvs),
            triangles = triangles
        };
    }
}
