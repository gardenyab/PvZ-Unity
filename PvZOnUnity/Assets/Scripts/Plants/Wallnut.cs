using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Wallnut : MonoBehaviour
{
    public SpriteRenderer spriteObject;
    public Sprite[] sprites;

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Plant>().health <= 33) spriteObject.sprite = sprites[0];
        else if (gameObject.GetComponent<Plant>().health <= 66) spriteObject.sprite = sprites[1];
        else spriteObject.sprite = sprites[2];
    }
}
