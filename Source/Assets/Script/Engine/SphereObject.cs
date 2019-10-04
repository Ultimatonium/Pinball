using UnityEngine;

public class SphereObject : WorldObject
{

    [SerializeField]
    public double radius;
    [SerializeField]
    public int longitude;
    [SerializeField]
    public int latitude;

    protected override void Awake()
    {
        base.Awake();
        friction = 0.05;
    }

    protected override void Start()
    {
        base.Start();
        meshFilter.mesh = Sphere.GenerateMesh(longitude, latitude, radius);
    }

    protected override void HitSphere(SphereObject sphereObject)
    {
        if (sphereObject == null) return;
        if (Distance(sphereObject.transform.position) <= sphereObject.radius + radius)
        {
            if (sphereObject.isTrigger)
            {
                sphereObject.DoTrigger(this);
                return;
            }
            if (prevHit == sphereObject) return;
            prevHit = sphereObject;
            translateVector = CalcBounce(translateVector, sphereObject.meshFilter.mesh.normals[0], Mathf.Max((float)bounceFactor, (float)sphereObject.bounceFactor));
        }
    }

    protected override void HitPlane(PlaneObject planeObject)
    {
        if (planeObject == null) return;
        if (PlaneIntersection(planeObject) <= radius)
        {
            if (Distance(planeObject.transform.position) < planeObject.GetComponent<PlaneObject>().width / 2)
            {
                if (prevHit == planeObject) return;
                prevHit = planeObject;
                translateVector = CalcBounce(translateVector, planeObject.meshFilter.mesh.normals[0], Mathf.Max((float)bounceFactor, (float)planeObject.bounceFactor));
                transform.position += planeObject.meshFilter.mesh.normals[0].normalized * (float)(radius - PlaneIntersection(planeObject));
            }
        }
    }

    protected override void PostStart()
    {
        postStartDone = true;
    }
}
