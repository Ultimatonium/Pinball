
using UnityEngine;

public class Ball : SphereObject
{
    private Floor floor;
    private double umfang;

    protected override void Awake()
    {
        base.Awake();
        umfang = 2 * Mathf.PI * radius;
    }

    protected override void Start()
    {
        base.Start();
        floor = FindObjectOfType<Floor>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.position += floor.meshFilter.mesh.normals[0].normalized * (float)(radius - PlaneIntersection(floor));
        double rollDegree = translateVector.Magnitude() / umfang * /*2 * Mathf.PI*/ 360;
        transform.Rotate(VectorXYZ.Cross(translateVector, GravDirection()).ToVector3(), (float)rollDegree * Time.fixedDeltaTime);
    }
}
