using UnityEngine;

public class Physic
{
    public static double gravitationalConstant = 6.67f * Mathf.Pow(10, -11);

    public static VectorXYZ Gravity(VectorXYZ direction, double massSelf, double massOther, double radius)
    {
        return GravityDirection(direction) * (float)GravityVelocity(massSelf, massOther, radius);
    }

    public static VectorXYZ GravityDirection(VectorXYZ direction)
    {
        return direction.Normalize();
    }

    public static double GravityVelocity(double massSelf, double massOther, double radius)
    {
        const int t = 1; //weil danach mit deltatime gerechnet wird
        return GravityAcceleration(massSelf, massOther, radius) / t;
    }

    public static double GravityAcceleration(double massSelf, double massOther, double radius)
    {
        return GravityForce(massSelf, massOther, radius) / massSelf;
    }

    public static double GravityForce(double mass1, double mass2, double radius)
    {
        return gravitationalConstant * mass1 * mass2 / (radius * radius);
    }

    public static double PerfectVelocity(double centralObjectMass, double radius)
    {
        return Mathf.Sqrt((float)(gravitationalConstant * centralObjectMass / radius));
    }
}
