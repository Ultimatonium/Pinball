using UnityEngine;

public class Teleporter : SphereObject
{
    private bool used = false;
    
    public override void DoTrigger(WorldObject worldObject)
    {
        if (used) return;
        GameObject target = null;
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            GameObject teleporter = transform.parent.GetChild(i).gameObject;
            teleporter.GetComponent<Teleporter>().used = true;
            teleporter.GetComponent<MeshRenderer>().enabled = false;
            if (teleporter == gameObject) continue;
            target = transform.parent.GetChild(i).gameObject;
        }
        worldObject.transform.position = target.transform.position;
        Invoke("ReactivateTeleporter", 3);
        GameController.score += 100;
    }

    private void ReactivateTeleporter()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            GameObject teleporter = transform.parent.GetChild(i).gameObject;
            teleporter.GetComponent<Teleporter>().used = false;
            teleporter.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
