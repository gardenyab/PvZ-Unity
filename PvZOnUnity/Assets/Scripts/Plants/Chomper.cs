using UnityEngine;
using System.Collections;

public class Chomper : MonoBehaviour
{
    public bool isEat = false;
    public Animator animator;
    private GameObject target;
    public float range = 0.5f;
    public LayerMask biteMask;
    public int damage = 100;
    public float recharge = 5f;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, biteMask);
        if (hit.collider && !isEat)
        {
            target = hit.collider.gameObject;
            StartCoroutine("Bite");
        }
    }

    IEnumerator Bite()
    {
        isEat = true;
        animator.SetTrigger("bite");
        yield return new WaitForSeconds(0.5f);
        target.GetComponent<Zombie>().Hit(damage, "destroy");
        StartCoroutine("ResetRecharge");
        if (target.GetComponent<Zombie>().health <= 0)
        {
            StartCoroutine("KillZombie");
        }
    }

    IEnumerator KillZombie()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(target);
        StopCoroutine(KillZombie());
    }

    IEnumerator ResetRecharge()
    {
        yield return new WaitForSeconds(0.8f);
        isEat = false;
        StopCoroutine(ResetRecharge());
    }
}
