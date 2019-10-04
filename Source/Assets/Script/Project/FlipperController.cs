using UnityEngine;

public class FlipperController : SphereObject
{
    private bool flipped = false;
    private Mesh idle;
    private WorldObject parent;
    private WorldObject worldObject;
    private int mouseButton;
    private float maxRotation;

    protected override void Awake()
    {
        base.Awake();
        maxRotation = Mathf.PI / 4;
        parent = transform.parent.GetComponent<WorldObject>();
        switch (parent.name)
        {
            case "FlipperLeft":
                mouseButton = 0;
                break;

            case "FlipperRight":
                mouseButton = 1;
                break;
            default:
                Debug.LogError("Wrong Flipper");
                break;
        }
    }

    protected override void Start()
    {
        radius = parent.GetComponent<PlaneObject>().width / 2;
        base.Start();
    }

    private void Update()
    {
        float delta;
        if (Input.GetMouseButtonDown(mouseButton))
        {
            if (flipped) return;
            flipped = true;
            if (worldObject == null)
            {
                delta = maxRotation;
            }
            else
            {
                VectorXYZ flipperStandard;
                if (mouseButton == 1)
                {
                    flipperStandard = VectorXYZ.FromVector3(parent.transform.position + (parent.meshFilter.mesh.vertices[0] + parent.meshFilter.mesh.vertices[1]).normalized * parent.meshFilter.mesh.vertices[0].magnitude);

                }
                else
                {
                    flipperStandard = VectorXYZ.FromVector3(parent.transform.position + (parent.meshFilter.mesh.vertices[2] + parent.meshFilter.mesh.vertices[3]).normalized * parent.meshFilter.mesh.vertices[0].magnitude);
                }
                VectorXYZ flipperTarget = VectorXYZ.FromVector3(parent.transform.position + (worldObject.transform.position - parent.transform.position));
                flipperTarget -= VectorXYZ.FromVector3(parent.meshFilter.mesh.normals[0]) * worldObject.GetComponent<Ball>().radius;
                flipperStandard = flipperStandard.Normalize();
                flipperTarget = flipperTarget.Normalize();
                delta = (float)((flipperTarget * flipperStandard) / (flipperTarget.Magnitude() * flipperStandard.Magnitude()));
                delta = Mathf.Cos(delta);
            };
            if (mouseButton == 1) delta *= -1;
            if (Mathf.Abs(delta) > maxRotation)
            {
                delta = maxRotation * PosNeg(delta);
                parent.meshFilter.mesh = Rotate.RotateY(parent.meshFilter.mesh, delta);
            }
            else
            {
                parent.meshFilter.mesh = Rotate.RotateY(parent.meshFilter.mesh, delta);
                AddFlipperForce(parent.meshFilter.mesh.normals[0]);
            }
            Invoke("ResetFlipper", 0.1f);
        }
    }

    protected override void PostStart()
    {
        base.PostStart();
        transform.position = parent.transform.position; //mesh position fix
        idle = new Mesh()
        {
            vertices = parent.meshFilter.mesh.vertices,
            normals = parent.meshFilter.mesh.normals,
            uv = parent.meshFilter.mesh.uv,
            triangles = parent.meshFilter.mesh.triangles
        };
    }

    public override void DoTrigger(WorldObject worldObject)
    {
        this.worldObject = worldObject;
        Invoke("ResetWorldObject", 1);
    }

    private int PosNeg(float value)
    {
        if (value == 0) return 0;
        return (int)(value / Mathf.Abs(value));
    }

    private void AddFlipperForce(Vector3 normal)
    {
        if (worldObject == null) return;
        if (worldObject.translateVector.x > 0)
        {
            worldObject.translateVector = CalcBounce(worldObject.translateVector, normal, Mathf.Max((float)bounceFactor, (float)worldObject.bounceFactor));
        }
        worldObject.translateVector += worldObject.translateVector.Normalize() * 20; //extra power
        GameController.score += 10;
    }

    private void ResetWorldObject()
    {
        worldObject = null;
    }

    private void ResetFlipper()
    {
        parent.meshFilter.mesh = idle;
        flipped = false;
    }
}
