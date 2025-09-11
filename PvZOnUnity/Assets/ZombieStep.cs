using UnityEngine;

public class ZombieStep : MonoBehaviour
{
    public GameObject Zombie;
    public float speed = 2.576666f;
    void Step()
    {
        Zombie.transform.position -= new Vector3(speed, 0, 0);
    }
}
