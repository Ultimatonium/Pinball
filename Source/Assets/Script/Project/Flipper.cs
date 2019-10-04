using UnityEngine;

public class Flipper : PlaneObject
{
    protected override void Awake()
    {
        base.Awake();
        width = 50;
        high = 6;
    }

    protected override void Start()
    {
        base.Start();

        meshFilter.mesh = Rotate.RotateZ(meshFilter.mesh, Board.tilt + -Mathf.PI / 2);

        switch (name)
        {
            case "FlipperLeft":
                meshFilter.mesh = Rotate.RotateY(meshFilter.mesh, -Mathf.PI / 4);
                meshFilter.mesh = Rotate.RotateX(meshFilter.mesh, -Board.tilt);
                break;
            case "FlipperRight":
                meshFilter.mesh = Rotate.RotateY(meshFilter.mesh, Mathf.PI / 4);
                meshFilter.mesh = Rotate.RotateX(meshFilter.mesh, Board.tilt);
                break;
            default:
                Debug.LogError("Wrong Walltype");
                break;
        }
    }
}
