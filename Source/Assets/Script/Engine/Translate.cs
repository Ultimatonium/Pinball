using UnityEngine;

public class Translate
{
    /*
    private static double[,] translation = new double[4, 4] { {1,0,0,0}
                                                            , {0,1,0,0}
                                                            , {0,0,1,0}
                                                            , {0,0,0,1}
                                                            };
    */
    public static Mesh TranslateMesh(Mesh mesh, VectorXYZ delta)
    {
        mesh.vertices = TranslateVector3(mesh.vertices, delta);
        return mesh;
    }

    public static Vector3[] TranslateVector3(Vector3[] vertices, VectorXYZ delta)
    {
        return VectorXYZ.ToVector3(TranslateVectorXYZ(VectorXYZ.FromVector3(vertices), delta));
    }

    public static VectorXYZ[] TranslateVectorXYZ(VectorXYZ[] vertices, VectorXYZ delta)
    {
        /*
        translation[0, 3] = delta.x;
        translation[1, 3] = delta.y;
        translation[2, 3] = delta.z;
        */

        //translation[3, 0] = delta.x;
        //translation[3, 1] = delta.y;
        //translation[3, 2] = delta.z;
        //PrintMatrix(translation);
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = TranslateVectorXYZ(vertices[i], delta);
            //vertices[i] = new VectorXYZ(vertices[i].x + delta.x, vertices[i].y + delta.y, vertices[i].z + delta.z);
            //vertices[i] = translation * vertices[i];
        }
        return vertices;
    }

    public static VectorXYZ TranslateVector3(Vector3 vector, VectorXYZ delta)
    {
        return TranslateVectorXYZ(VectorXYZ.FromVector3(vector), delta);
    }

    public static VectorXYZ TranslateVectorXYZ(VectorXYZ vector, VectorXYZ delta)
    {
        return new VectorXYZ(vector.x + delta.x, vector.y + delta.y, vector.z + delta.z);
    }

    public static void PrintMatrix(double[,] matrix)
    {
        string printString = "";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int ii = 0; ii < matrix.GetLength(1); ii++)
            {
                printString += "[" + i + "," + ii + "]" + matrix[i, ii];
            }
            printString += System.Environment.NewLine;
        }
        Debug.Log(printString);
    }
}