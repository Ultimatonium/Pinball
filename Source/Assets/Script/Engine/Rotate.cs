using UnityEngine;

public class Rotate
{
    private static double[,] rotation;
    public static Mesh RotateX(Mesh mesh, double delta)
    {
        mesh.vertices = RotateX(mesh.vertices, delta);
        mesh.normals = RotateX(mesh.normals, delta);
        return mesh;
    }

    public static Vector3[] RotateX(Vector3[] vectors, double delta)
    {
        return VectorXYZ.ToVector3(RotateX(VectorXYZ.FromVector3(vectors), delta));
    }

    public static VectorXYZ[] RotateX(VectorXYZ[] vectors, double delta)
    {
        for (int i = 0; i < vectors.Length; i++)
        {
            vectors[i] = RotateX(vectors[i], delta);
        }
        return vectors;
    }

    public static VectorXYZ RotateX(VectorXYZ vector, double delta)
    {
        rotation = new double[3, 3] { {1, 0, 0}
                                    , {0, Mathf.Cos((float)delta), -Mathf.Sin((float)delta)}
                                    , {0, Mathf.Sin((float)delta), Mathf.Cos((float)delta)}
                                    };
        return rotation * vector;
    }

    public static Mesh RotateY(Mesh mesh, double delta)
    {
        mesh.vertices = RotateY(mesh.vertices, delta);
        mesh.normals = RotateY(mesh.normals, delta);
        return mesh;
    }

    public static Vector3[] RotateY(Vector3[] vectors, double delta)
    {
        return VectorXYZ.ToVector3(RotateY(VectorXYZ.FromVector3(vectors), delta));
    }

    public static VectorXYZ[] RotateY(VectorXYZ[] vectors, double delta)
    {
        for (int i = 0; i < vectors.Length; i++)
        {
            vectors[i] = RotateY(vectors[i], delta);
        }
        return vectors;
    }

    public static VectorXYZ RotateY(VectorXYZ vector, double delta)
    {
        rotation = new double[3, 3] { {Mathf.Cos((float)delta), 0, Mathf.Sin((float)delta)}
                                    , {0, 1, 0}
                                    , {-Mathf.Sin((float)delta), 0, Mathf.Cos((float)delta)}
                                    };
        return rotation * vector;
    }

    public static Mesh RotateZ(Mesh mesh, double delta)
    {
        mesh.vertices = RotateZ(mesh.vertices, delta);
        mesh.normals = RotateZ(mesh.normals, delta);
        return mesh;
    }

    public static Vector3[] RotateZ(Vector3[] vectors, double delta)
    {
        return VectorXYZ.ToVector3(RotateZ(VectorXYZ.FromVector3(vectors), delta));
    }

    public static VectorXYZ[] RotateZ(VectorXYZ[] vectors, double delta)
    {
        for (int i = 0; i < vectors.Length; i++)
        {
            vectors[i] = RotateZ(vectors[i], delta);
        }
        return vectors;
    }

    public static VectorXYZ RotateZ(VectorXYZ vector, double delta)
    {
        rotation = new double[3, 3] { {Mathf.Cos((float)delta), -Mathf.Sin((float)delta), 0}
                                    , {Mathf.Sin((float)delta), Mathf.Cos((float)delta), 0}
                                    , {0, 0, 1}
                                    };
        return rotation * vector;
    }
}
