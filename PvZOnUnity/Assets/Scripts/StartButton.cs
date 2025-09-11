using UnityEngine;

public class StartButton : MonoBehaviour
{
    private GameManager gms;
    public GameObject plantSelecter;
    public GameObject zombiesSpawner;

    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void startGame()
    {
        gms.gameStatus = "game";
        zombiesSpawner.SetActive(true);
        plantSelecter.SetActive(false);
        gameObject.SetActive(false);
    }
}
