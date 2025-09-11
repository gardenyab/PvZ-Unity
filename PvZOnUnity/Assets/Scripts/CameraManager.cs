using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Позиция 1")]
    public Vector3 pos1;
    [Header("Обьект иконки")]
    public Vector3 pos2;
    private bool used = false;
    private GameManager gms;

    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        gms.gameStatus = "inventory";
        transform.position = pos2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!used && gms.gameStatus == "game")
        {
            transform.position = pos1;
            used = true;
        }
    }
}
