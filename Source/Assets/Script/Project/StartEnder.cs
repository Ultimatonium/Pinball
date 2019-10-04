using UnityEngine;

public class StartEnder : SphereObject
{
    [SerializeField]
    private GameObject[] reverseObject;
    [HideInInspector]
    public bool triggered = false;
    
    protected override void PostStart()
    {
        base.PostStart();
        transform.position = new Vector3(-13, 5, 21); //mesh position fix
    }

    public override void DoTrigger(WorldObject worldObject)
    {
        if (triggered) return;
        triggered = true;
        Invoke("Reverser", 0.5f);
    }

    public void Reverser()
    {
        foreach (GameObject item in reverseObject)
        {
            item.SetActive(!item.activeSelf);
        }
        gameObject.SetActive(false);
    }
}
