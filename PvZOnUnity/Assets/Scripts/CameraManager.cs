using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Позиция 1")]
    public Vector3 pos1;
    [Header("Обьект иконки")]
    public Vector3 pos2;
    private bool used = false;
    void Start()
    {
        transform.position = pos1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!used)
        {
            transform.position = Vector3.Lerp(pos1, pos2, 1 * Time.fixedDeltaTime);
            used = true;
        }
    }
}
