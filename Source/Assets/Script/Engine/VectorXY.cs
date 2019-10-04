using UnityEngine;

public struct VectorXY
{
    public float x, y;
    public static VectorXY zero = new VectorXY(0, 0);
    public static VectorXY one = new VectorXY(1, 1);
    public static VectorXY right = new VectorXY(1, 0);
    public static VectorXY left = new VectorXY(-1, 0);
    public static VectorXY up = new VectorXY(0, 1);
    public static VectorXY down = new VectorXY(0, -1);
    public static VectorXY negativeInfinity = new VectorXY(float.NegativeInfinity, float.NegativeInfinity);
    public static VectorXY positiveInfinity = new VectorXY(float.PositiveInfinity, float.PositiveInfinity);

    public VectorXY(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public Vector2 ToVector2()
    {
        return ToVector2(this);
    }

    public static Vector2 ToVector2(VectorXY vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    public static Vector2[] ToVector2(VectorXY[] vectors)
    {
        Vector2[] target = new Vector2[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            target[i] = ToVector2(vectors[i]);
        }
        return target;
    }
}
