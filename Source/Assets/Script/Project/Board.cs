using UnityEngine;
public class Board : MonoBehaviour
{
    public static double tilt;

    private void Awake()
    {
        tilt = Mathf.PI / 20;
    }
}
