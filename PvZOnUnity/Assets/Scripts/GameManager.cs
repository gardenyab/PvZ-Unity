using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject[] inventory;
    public GameObject currentPlant;
    public Sprite currentPlantSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public LayerMask sunMask;
    public int suns = 0;
    public TextMeshProUGUI sunText;
    private int plantPrice;
    private bool setTile;
    public AudioSource plantSound;
    public AudioSource sunSound;
    public string gameStatus = "inventory";

    public void BuyPlant(GameObject plant, Sprite sprite, int price, bool tile)
    {
        currentPlant = plant;
        currentPlantSprite = sprite;
        plantPrice = price;
        setTile = tile;
    }

    private void Update()
    {
        sunText.text = suns.ToString();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

        foreach (Transform tile in tiles)
            tile.GetComponent<SpriteRenderer>().enabled = false;

        if (hit.collider && currentPlant)
        {
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlantSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasPlant)
            {
                Instantiate(currentPlant, hit.collider.transform.position, Quaternion.identity);
                currentPlant = null;
                currentPlantSprite = null;
                suns -= plantPrice;
                plantPrice = 0;
                plantSound.Play();
                if (setTile) hit.collider.GetComponent<Tile>().hasPlant = true;
            }
        }

        RaycastHit2D sunHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, sunMask);
        if (sunHit.collider && Input.GetMouseButtonDown(0))
        {
            sunSound.Play();
            suns += 25;
            Destroy(sunHit.collider.gameObject);
        }
    }
}
