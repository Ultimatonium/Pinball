using UnityEngine;

public class DestroyBall : SphereObject
{
    protected override void PostStart()
    {
        base.PostStart();
        transform.position = new Vector3(110, -30, 0); //mesh position fix
    }

    public override void DoTrigger(WorldObject worldObject)
    {
        Destroy(worldObject.gameObject);
        GameController.GetInstance().GameOver();
    }

}
