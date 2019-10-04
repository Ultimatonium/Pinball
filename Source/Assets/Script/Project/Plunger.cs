using UnityEngine;

public class Plunger : MonoBehaviour
{
    [HideInInspector]
    public Ball ball;
    private float power = 0;
    private float maxPower = 100;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            power += 40 * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            power = Mathf.Min(power, maxPower);
            transform.position = new Vector3(ball.transform.position.x + 2, ball.transform.position.y, ball.transform.position.z);
            ball.translateVector = VectorXYZ.FromVector3((ball.transform.position - transform.position) * power);
            power = 0;
        }
    }
}
