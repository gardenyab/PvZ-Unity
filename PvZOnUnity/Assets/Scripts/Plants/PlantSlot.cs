using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantSlot : MonoBehaviour
{
    [Header("Спрайт")]
    public Sprite plantSprite;
    [Header("Растение")]
    public GameObject plantObject;
    [Header("Обьект иконки")]
    public Image icon;
    [Header("Цена")]
    public int price = 0;
    public TextMeshProUGUI priceText;
    [Header("Занять слот")]
    public bool tile = true;
    [Header("Фон слота")]
    public Image BGSprite;

    private GameManager gms;
    public AudioSource selectSound;
    public AudioSource errorSelectSound;
    private bool selected = false;
    private GameObject mySlot;
    private Vector3 startPos;
    public float speed = 1f;
    private Vector3 pos1;
    private Vector3 pos2;

    private void Start()
    {
        startPos = transform.position;
        pos1 = startPos;
        pos2 = startPos;
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(Click);
    }

    private void Click()
    {
        if (gms.gameStatus == "inventory")
        {
            if (!selected)
            {
                foreach (GameObject i in gms.inventory)
                {
                    if (i.GetComponent<InventorySlot>().plant == "none")
                    {
                        Debug.Log("OK");
                        mySlot = i;
                        pos1 = startPos;
                        pos2 = i.GetComponent<Transform>().position; //Vector3.Lerp(startPos, i.GetComponent<Transform>().position, speed * Time.fixedDeltaTime);
                        i.GetComponent<InventorySlot>().plant = "test";
                        selected = true;
                        break;
                    }
                    else Debug.Log("ne OK " + (string)i.GetComponent<InventorySlot>().plant);
                }
            }
            else
            {
                selected = false;
                pos2 = startPos;
                pos1 = mySlot.GetComponent<Transform>().position;
                mySlot.GetComponent<InventorySlot>().plant = "none";
            }
        }
        else
        {
            if (gms.currentPlant == plantObject && gms.currentPlantSprite == plantSprite)
            {
                gms.currentPlant = null;
                gms.currentPlantSprite = null;
            }
            else
            {
                if (gms.suns >= price)
                {
                    selectSound.Play();
                    gms.BuyPlant(plantObject, plantSprite, price, tile);
                }
                else
                {
                    errorSelectSound.Play();
                }
            }
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, speed * Time.deltaTime);
        if (gms.suns >= price)
        {
            icon.color = new Color(255, 255, 255);
            BGSprite.color = new Color(255, 255, 255);
        }
        else
        {
            icon.color = new Color(125, 125, 125);
            BGSprite.color = new Color(125, 125, 125);
        }
    }

    private void OnValidate()
    {
        if (plantSprite)
        {
            icon.enabled = true;
            icon.sprite = plantSprite;
            priceText.text = price.ToString();
        }
        else
        {
            icon.enabled = false;
        }

        
    }
}
