using UnityEngine;
public class Sunflower : MonoBehaviour
{
    public GameObject sun;
    public float cooldown;
    public Animator animator;

    private void Start()
    {
        InvokeRepeating("SpawnSun", cooldown, cooldown);
    }

    void SpawnSun()
    {
        animator.SetTrigger("spawn");
        GameObject mySun = Instantiate(sun, transform.position, Quaternion.identity);
    }
}
