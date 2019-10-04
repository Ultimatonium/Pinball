using UnityEngine;

public class Bumper : SphereObject {

    private double ballRadius;
    private Floor floor;

    protected override void Awake()
    {
        base.Awake();
        radius = 3;
    }

    protected override void Start()
    {
        base.Start();
        ballRadius = FindObjectOfType<Ball>().radius;
        floor = FindObjectOfType<Floor>();
    }
    
    protected override void PostStart()
    {
        base.PostStart();
        transform.position = new Vector3(10, 0, 10); //mesh position fix
        transform.position += floor.meshFilter.mesh.normals[0].normalized * (float)(ballRadius + -radius + PlaneIntersection(floor));
    }

    public override void DoTrigger(WorldObject worldObject)
    {
        if (prevHit == worldObject) return;
        prevHit = worldObject;
        worldObject.translateVector = CalcBounce(worldObject.translateVector, (worldObject.transform.position - transform.position).normalized, Mathf.Max((float)bounceFactor, (float)worldObject.bounceFactor));
        worldObject.translateVector *= 2.5; //add power
        worldObject.translateVector.y = 0; //fake it
        GameController.score += 1000;
    }
}
