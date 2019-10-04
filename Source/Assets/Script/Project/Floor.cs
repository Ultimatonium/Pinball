public class Floor : PlaneObject
{
    protected override void Start()
    {
        base.Start();
        meshFilter.mesh = Rotate.RotateZ(meshFilter.mesh, Board.tilt);
    }
}
