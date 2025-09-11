using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float speed = 0.008f;
    public Animator animator;
    public int health = 10;
    private float nSpeed = 0f;
    private bool lostHand = false;
    public bool canMove = true;

    public int damage;
    public float range;
    public LayerMask plantMask;
    public float eatCooldown;
    private bool canEat = true;
    private Plant target;
    public AudioClip[] groans;
    public AudioSource groan;
    public AudioClip[] splats;
    public AudioSource splat;
    public AudioClip[] fallings;
    public AudioSource falling;
    public AudioClip[] chomps;
    public AudioSource chomp;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, range, plantMask);

        if (hit.collider && health > 0)
        {
            target = hit.collider.GetComponent<Plant>();
            Eat();
        }
    }

    void Eat()
    {
        if (!canEat || !target)
            return;
        canEat = false;

        Invoke("ResetEatCooldown", eatCooldown);
        chomp.clip = chomps[Random.Range(0, chomps.Length - 1)];
        chomp.Play();
        target.Hit(damage);
    }

    void ResetEatCooldown()
    {
        canEat = true;
    }


    private void FixedUpdate()
    {
        if (!target)
        {
            if (canMove)
                transform.position -= new Vector3(nSpeed, 0, 0);
        }
        if (target) animator.SetBool("isEat", true);
        else animator.SetBool("isEat", false);

    }

    public void Go(float[] newSpeed)
    {
        //StartCoroutine(Step({1f}));
    }

    IEnumerator Step(float time)
    {
        int num = Random.Range(1, 15);
        if (num == 3)
        {
            groan.clip = groans[Random.Range(0, groans.Length - 1)];
            groan.Play();
        }
        nSpeed = speed;
        yield return new WaitForSeconds(time);
        nSpeed = speed - speed / 1.4f;
        yield return new WaitForSeconds(time - time / 1.5f);
    }

    public void Hit(int damage, string deadType = "standart")
    {
        health -= damage;
        splat.clip = splats[Random.Range(0, splats.Length - 1)];
        splat.Play();
        if (health <= 0)
        {
            canMove = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if (deadType == "standart")
            {
                animator.SetBool("isEat", false);
                animator.SetTrigger("die");
                falling.clip = fallings[Random.Range(0, fallings.Length - 1)];
                falling.Play();
            }
            else if (deadType == "destroy")
            {
                Kill();
            }
        }
        if (health <= 5 && !lostHand)
        {
            animator.SetTrigger("lostHand");
            lostHand = true;
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    
}
