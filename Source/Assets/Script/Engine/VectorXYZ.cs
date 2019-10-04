using UnityEngine;

public struct VectorXYZ
{
    public double x, y, z;
    public static VectorXYZ zero = new VectorXYZ(0, 0, 0);
    public static VectorXYZ one = new VectorXYZ(1, 1, 1);
    public static VectorXYZ right = new VectorXYZ(1, 0, 0);
    public static VectorXYZ left = new VectorXYZ(-1, 0, 0);
    public static VectorXYZ up = new VectorXYZ(0, 1, 0);
    public static VectorXYZ down = new VectorXYZ(0, -1, 0);
    public static VectorXYZ forward = new VectorXYZ(0, 0, 1);
    public static VectorXYZ back = new VectorXYZ(0, 0, -1);
    public static VectorXYZ negativeInfinity = new VectorXYZ(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
    public static VectorXYZ positiveInfinity = new VectorXYZ(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

    public VectorXYZ(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public VectorXYZ Normalize()
    {
        double length = Magnitude();
        return new VectorXYZ(x / length, y / length, z / length);
    }

    public double Magnitude()
    {
        return Mathf.Sqrt((float)(x * x + y * y + z * z));
    }

    public VectorXYZ Abs()
    {
        return new VectorXYZ(Mathf.Abs((float)x), Mathf.Abs((float)y), Mathf.Abs((float)z));
    }

    public static VectorXYZ operator +(VectorXYZ summand1, VectorXYZ summand2)
    {
        return new VectorXYZ(summand1.x + summand2.x, summand1.y + summand2.y, summand1.z + summand2.z);
    }

    public static VectorXYZ operator -(VectorXYZ minuend, VectorXYZ subtrahend)
    {
        return new VectorXYZ(minuend.x - subtrahend.x, minuend.y - subtrahend.y, minuend.z - subtrahend.z);
    }

    public static VectorXYZ operator *(VectorXYZ vector, double faktor)
    {
        return new VectorXYZ(vector.x * faktor, vector.y * faktor, vector.z * faktor);
    }

    public static VectorXYZ operator *(VectorXYZ vector, float faktor)
    {
        return vector * (double)faktor;
    }

    public static VectorXYZ operator *(double faktor, VectorXYZ vector)
    {
        return vector * faktor;
    }

    public static VectorXYZ operator *(float faktor, VectorXYZ vector)
    {
        return vector * (double)faktor;
    }

    public static double operator *(VectorXYZ vector1, VectorXYZ vector2)
    {
        return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
    }

    public static double operator *(Vector3 vector1, VectorXYZ vector2)
    {
        return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
    }

    public static VectorXYZ operator *(double[,] matrix, VectorXYZ vector)
    {
        switch (matrix.GetLength(1))
        {
            case 3:
                return new VectorXYZ(vector.x * matrix[0, 0] + vector.y * matrix[1, 0] + vector.z * matrix[2, 0]
                                   , vector.x * matrix[0, 1] + vector.y * matrix[1, 1] + vector.z * matrix[2, 1]
                                   , vector.x * matrix[0, 2] + vector.y * matrix[1, 2] + vector.z * matrix[2, 2]);
            case 4:
                return new VectorXYZ(vector.x * matrix[0, 0] + vector.y * matrix[1, 0] + vector.z * matrix[2, 0] + 1 * matrix[3, 0]
                                   , vector.x * matrix[0, 1] + vector.y * matrix[1, 1] + vector.z * matrix[2, 1] + 1 * matrix[3, 1]
                                   , vector.x * matrix[0, 2] + vector.y * matrix[1, 2] + vector.z * matrix[2, 2] + 1 * matrix[3, 2]);
            default:
                throw new System.ArgumentException();
        }
    }

    public static VectorXYZ Cross(VectorXYZ a, VectorXYZ b)
    {
        return new VectorXYZ(a.y * b.z - a.z * b.y
                           , a.z * b.x - a.x * b.z
                           , a.x * b.y - a.y * b.x);
    }

    public static VectorXYZ Direction(VectorXYZ from, VectorXYZ to)
    {
        return to - from;
    }

    public static VectorXYZ Direction(Vector3 from, Vector3 to)
    {
        return Direction(FromVector3(from), FromVector3(to));
    }

    public Vector3 ToVector3()
    {
        return ToVector3(this);
    }

    public static Vector3 ToVector3(VectorXYZ vector)
    {
        return new Vector3((float)vector.x, (float)vector.y, (float)vector.z);
    }

    public static Vector3[] ToVector3(VectorXYZ[] vectors)
    {
        Vector3[] target = new Vector3[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            target[i] = ToVector3(vectors[i]);
        }
        return target;
    }

    public static VectorXYZ FromVector3(Vector3 vector)
    {
        return new VectorXYZ(vector.x, vector.y, vector.z);
    }

    public static VectorXYZ[] FromVector3(Vector3[] vectors)
    {
        VectorXYZ[] target = new VectorXYZ[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            target[i] = FromVector3(vectors[i]);
        }
        return target;
    }

    public override string ToString()
    {
        return "x=" + x + ", y=" + y + ", z=" + z;
    }
}
