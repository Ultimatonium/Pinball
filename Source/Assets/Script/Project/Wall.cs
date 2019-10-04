using UnityEngine;
public class Wall : PlaneObject
{
    protected override void Start()
    {
        base.Start();

        switch (name)
        {
            case "WallTop":
                meshFilter.mesh = Rotate.RotateZ(meshFilter.mesh, Board.tilt + Mathf.PI / 2);
                break;
            case "WallDown":
                meshFilter.mesh = Rotate.RotateZ(meshFilter.mesh, Board.tilt + -Mathf.PI / 2);
                break;
            case "WallLeft":
                meshFilter.mesh = Rotate.RotateX(meshFilter.mesh, -Mathf.PI / 2);
                meshFilter.mesh = Rotate.RotateZ(meshFilter.mesh, Board.tilt + Mathf.PI / 2);
                break;
            case "WallRight":
                meshFilter.mesh = Rotate.RotateX(meshFilter.mesh, Mathf.PI / 2);
                meshFilter.mesh = Rotate.RotateZ(meshFilter.mesh, Board.tilt + Mathf.PI / 2);
                break;
            case "Redirecter":

                meshFilter.mesh = Rotate.RotateZ(meshFilter.mesh, Board.tilt + Mathf.PI / 2);
                meshFilter.mesh = Rotate.RotateY(meshFilter.mesh, -Mathf.PI / 4);
                break;
            default:
                Debug.LogError("Wrong Walltype");
                break;
        }
    }
}
