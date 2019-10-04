using UnityEngine;

public class Rollover : SphereObject
{
    private const int points = 100;

    public override void DoTrigger(WorldObject worldObject)
    {
        GameController.score += (int)(points * Time.fixedDeltaTime);
    }
}
