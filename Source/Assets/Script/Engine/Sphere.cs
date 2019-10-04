using UnityEngine;

public class Sphere
{
    private static float _PI = Mathf.PI;
    private static float _2PI = 2f * Mathf.PI;

    public static Mesh GenerateMesh(int longitude, int latitude, double radius)
    {
        #region Vertices
        VectorXYZ[] vertices = new VectorXYZ[(longitude + 1) * latitude + 2];
        vertices[0] = VectorXYZ.up * radius;
        for (float iLatitude = 0; iLatitude < latitude; iLatitude++)
        {
            float aLatitude = _PI * (iLatitude + 1) / (latitude + 1);
            float sinLatitude = Mathf.Sin(aLatitude);
            float cosLatitude = Mathf.Cos(aLatitude);

            for (float iLongitude = 0; iLongitude <= longitude; iLongitude++)
            {
                float aLongitude = _2PI * (iLongitude == longitude ? 0 : iLongitude) / longitude;
                float sinLongitude = Mathf.Sin(aLongitude);
                float cosLongitude = Mathf.Cos(aLongitude);

                vertices[(int)(iLongitude + iLatitude * (longitude + 1) + 1)] = new VectorXYZ(sinLatitude * cosLongitude, cosLatitude, sinLatitude * sinLongitude) * radius;
            }
        }
        vertices[vertices.Length - 1] = VectorXYZ.down * radius;
        #endregion

        #region Normales
        VectorXYZ[] normales = new VectorXYZ[vertices.Length];
        for (int n = 0; n < vertices.Length; n++)
            normales[n] = vertices[n].Normalize();
        #endregion

        #region UVs
        VectorXY[] uvs = new VectorXY[vertices.Length];
        uvs[0] = VectorXY.up;
        uvs[uvs.Length - 1] = VectorXY.zero;
        for (float iLatitude = 0; iLatitude < latitude; iLatitude++)
            for (float iLongitude = 0; iLongitude <= longitude; iLongitude++)
                uvs[(int)(iLongitude + iLatitude * (longitude + 1) + 1)] = new VectorXY(iLongitude / longitude, 1f - (iLatitude + 1) / (latitude + 1));
        #endregion

        #region Triangles
        int faces = vertices.Length;
        int totalTriangles = faces * 2;
        int indices = totalTriangles * 3;
        int[] triangles = new int[indices];
        int i = 0;
        for (int iLongitude = 0; iLongitude < longitude; iLongitude++)
        {
            triangles[i++] = iLongitude + 2;
            triangles[i++] = iLongitude + 1;
            triangles[i++] = 0;
        }
        for (int iLatitude = 0; iLatitude < latitude - 1; iLatitude++)
        {
            for (int iLongitude = 0; iLongitude < longitude; iLongitude++)
            {
                int current = iLongitude + iLatitude * (longitude + 1) + 1;
                int next = current + longitude + 1;

                triangles[i++] = current;
                triangles[i++] = current + 1;
                triangles[i++] = next + 1;

                triangles[i++] = current;
                triangles[i++] = next + 1;
                triangles[i++] = next;

            }
        }
        for (int iLongitude = 0; iLongitude < longitude; iLongitude++)
        {
            triangles[i++] = vertices.Length - 1;
            triangles[i++] = vertices.Length - (iLongitude + 2) - 1;
            triangles[i++] = vertices.Length - (iLongitude + 1) - 1;
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
