using UnityEngine;
public class Sun : MonoBehaviour
{
    public float dropToYPos;
    public float speed = .15f;
    void Start()
    {
        Destroy(gameObject, Random.Range(6f, 12f));
    }

    private void Update() {
        if (transform.position.y >= dropToYPos)
            transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);
    }
}
