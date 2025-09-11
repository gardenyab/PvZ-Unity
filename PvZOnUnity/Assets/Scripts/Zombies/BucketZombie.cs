using UnityEngine;

public class BucketZombie : MonoBehaviour
{
    public SpriteRenderer spriteObject;
    public Sprite[] sprites;

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Zombie>().health <= 10) spriteObject.sprite = null;
        else if (gameObject.GetComponent<Zombie>().health <= 20) spriteObject.sprite = sprites[0];
        else if (gameObject.GetComponent<Zombie>().health <= 30) spriteObject.sprite = sprites[1];
        else spriteObject.sprite = sprites[2];
    }
}
