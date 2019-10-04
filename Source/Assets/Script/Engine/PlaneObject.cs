public class PlaneObject : WorldObject
{

    public double high;
    public double width;

    protected override void Start()
    {
        base.Start();
        meshFilter.mesh = Plane.GenerateMesh(width, high, 2, 2);
    }

    protected override void HitPlane(PlaneObject planeObject)
    {
        throw new System.NotImplementedException();
    }

    protected override void HitSphere(SphereObject sphereObject)
    {
        throw new System.NotImplementedException();
    }

    protected override void PostStart()
    {
        postStartDone = true;
    }
}
