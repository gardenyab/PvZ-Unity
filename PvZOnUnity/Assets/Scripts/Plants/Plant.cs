using UnityEngine;

public class Plant : MonoBehaviour
{
    public int health = 4;

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}