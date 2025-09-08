using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 0.8f;

    private void Update()
    {
        transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Zombie>(out Zombie zombie))
        {
            zombie.Hit(damage);
            Destroy(gameObject);
        }
    }
}
