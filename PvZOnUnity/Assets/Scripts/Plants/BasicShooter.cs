using UnityEngine;
using System.Collections;

public class BasicShooter : MonoBehaviour
{
    public Animator animator;

    public GameObject bullet;
    public Transform shootOrigin;
    public float cooldown = 2f;
    private bool canShoot;
    public float range;
    public LayerMask shootMask;
    private GameObject target;
    public int count = 1;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, shootMask);
        if (hit.collider)
        {
            target = hit.collider.gameObject;
            Shoot();
        }
    }
    private void Start()
    {
        ResetCooldown();
    }

    void ResetCooldown()
    {
        canShoot = true;
    }

    void Shoot()
    {
        if (!canShoot)
            return;

        canShoot = false;
        animator.SetTrigger("shoot");

        Invoke("ResetCooldown", cooldown);
        StartCoroutine(ShootWithDelay());
    }

    IEnumerator ShootWithDelay()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(bullet, shootOrigin.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
