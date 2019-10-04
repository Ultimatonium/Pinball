using System.Collections.Generic;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    [SerializeField]
    private Material material;
    [SerializeField]
    public bool isStatic;
    [SerializeField]
    public bool isTrigger;
    [SerializeField]
    protected double friction;
    [SerializeField]
    public double bounceFactor;

    protected WorldObject prevHit;

    private const double BounceThreshold = 0.14;
    protected const double gravity = 9.80665;
    protected bool postStartDone = false;

    [HideInInspector]
    public VectorXYZ translateVector = VectorXYZ.zero;

    [HideInInspector]
    public MeshFilter meshFilter;
    protected static List<WorldObject> objects = new List<WorldObject>();

    protected virtual void Awake()
    {
        translateVector = GravDirection();
    }

    protected virtual void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        if (material != null)
        {
            meshRenderer.material = material;
        }
        InvokeRepeating("ResetPrevHit", 0, 0.5f);
    }

    protected virtual void FixedUpdate()
    {
        if (!postStartDone) PostStart();
        CalcMove();
        DoMove();
    }

    private void OnEnable()
    {
        try
        {
            objects.Add(this);
        }
        catch (System.InvalidOperationException) { }
    }

    private void OnDisable()
    {
        try
        {
            objects.Remove(this);
        }
        catch (System.InvalidOperationException) { }
    }

    private void OnDestroy()
    {
        try
        {
            objects.Remove(this);
        }
        catch (System.InvalidOperationException) { }
    }

    protected abstract void PostStart();

    private void ResetPrevHit()
    {
        prevHit = null;
    }

    protected void CalcMove()
    {
        if (isStatic) return;
        try
        {
            objects.RemoveAll(WorldObject => WorldObject == null);
            foreach (WorldObject worldObject in objects)
            {

                if (worldObject == this) continue;
                if (worldObject.GetComponent<Floor>() != null) continue;
                Hit(worldObject);
            }
        }
        catch (System.InvalidOperationException) { }
        Roll();
    }

    protected void Roll()
    {
        float tilt = (float)Board.tilt;
        VectorXYZ Fg = GravDirection();
        VectorXYZ Fm = Mathf.Cos(tilt) * Fg;
        VectorXYZ Fn = Mathf.Sin(tilt) * Fg;
        Fn *= friction;
        Fm -= Fm.Normalize() * Fn.Magnitude();
        translateVector += new VectorXYZ(Fm.Magnitude() * Mathf.Cos(tilt), Fm.Magnitude() * -Mathf.Sin(tilt), 0);

    }
    
    protected void DoMove()
    {
        if (isStatic) return;

        transform.position = Translate.TranslateVector3(transform.position, translateVector * Time.fixedDeltaTime).ToVector3();
    }

    public static VectorXYZ CalcBounce(VectorXYZ movement, Vector3 normal, double bounceFactor)
    {
        movement = VectorXYZ.FromVector3(Quaternion.AngleAxis(180, normal) * movement.ToVector3());
        movement *= -1;
        movement *= bounceFactor;
        return movement;
    }

    protected void Hit(WorldObject worldObject)
    {
        PlaneObject planeObject;
        SphereObject sphereObject;

        planeObject = worldObject.GetComponent<PlaneObject>();
        sphereObject = worldObject.GetComponent<SphereObject>();
        if (planeObject != null)
        {
            HitPlane(planeObject);
        }
        else if (sphereObject != null)
        {
            HitSphere(sphereObject);
        }
        else
        {
            Debug.LogWarning("No known object");
            return;
        }
    }

    protected abstract void HitPlane(PlaneObject planeObject);

    protected abstract void HitSphere(SphereObject sphereObject);

    protected VectorXYZ GravDirection()
    {
        return VectorXYZ.down * gravity * Time.fixedDeltaTime;
    }

    public virtual void DoTrigger(WorldObject worldObject)
    {
        if (!isTrigger) return;
        Debug.LogWarning("trigger not defined for " + worldObject.name);
    }

    protected double PlaneIntersection(WorldObject worldObject)
    {
        float A = worldObject.meshFilter.mesh.normals[0].x;
        float B = worldObject.meshFilter.mesh.normals[0].y;
        float C = worldObject.meshFilter.mesh.normals[0].z;
        float x = worldObject.transform.position.x;
        float y = worldObject.transform.position.y;
        float z = worldObject.transform.position.z;
        float xs = transform.position.x;
        float ys = transform.position.y;
        float zs = transform.position.z;
        float D = A * x + B * y + C * z;
        double d = (Mathf.Abs(A * xs + B * ys + C * zs - D)) / (Mathf.Sqrt(A * A + B * B + C * C));
        return d;
    }

    protected double Distance(Vector3 vector)
    {
        return VectorXYZ.Direction(this.transform.position, vector).Magnitude();
    }
}
