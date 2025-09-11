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
    public float rechargeTime = 1;

    private GameManager gms;
    public AudioSource selectSound;
    public AudioSource errorSelectSound;
    private bool selected = false;
    private GameObject mySlot;
    private Vector3 startPos;
    public float speed = 1f;
    private Vector3 pos1;
    private Vector3 pos2;
    private bool canPlant = true;
    public bool rechargeOnStart = false;
    public Animator slotAnimator;
    public string myId = "plant";
    private bool recharged = false;

    private void Start()
    {
        try
        {
            slotAnimator = gameObject.GetComponent<Animator>();
        }
        catch
        {
            Debug.Log("Нету аниматора");
        }
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
                        transform.position = i.GetComponent<Transform>().position;
                        //pos2 = i.GetComponent<Transform>().position; Vector3.Lerp(startPos, i.GetComponent<Transform>().position, speed * Time.fixedDeltaTime);
                        i.GetComponent<InventorySlot>().plant = myId;
                        selected = true;
                        break;
                    }
                    else Debug.Log("ne OK " + (string)i.GetComponent<InventorySlot>().plant);
                }
            }
            else
            {
                selected = false;
                transform.position = startPos;
                mySlot.GetComponent<InventorySlot>().plant = "none";
            }
        }
        else
        {
            if (!canPlant) return;
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
                    gms.BuyPlant(gameObject, plantObject, plantSprite, price, tile);
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
        if (gms.gameStatus == "game")
        {
            bool hide = true;
            foreach (GameObject i in gms.inventory)
            {
                if (i.GetComponent<InventorySlot>().plant == myId)
                {
                    hide = false;
                    break;
                }
                else hide = true;
            }

            print(hide);

            if (hide == true) gameObject.SetActive(false);
        }

        if (gms.gameStatus == "game")
        {
            if (recharged && rechargeOnStart)
            {
                recharged = true;
                recharge();
            }
            if (gms.suns >= price)
            {
                icon.color = new Color(1f, 1f, 1f);
                BGSprite.color = new Color(1f, 1f, 1f);
            }
            else
            {
                icon.color = new Color(0.49f, 0.49f, 0.49f);
                BGSprite.color = new Color(0.49f, 0.49f, 0.49f);
            }
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
    public void recharge()
    {
        slotAnimator.SetTrigger("recharge");
        canPlant = false;
        slotAnimator.speed = 1f / rechargeTime;
    }

    public void rechargeEnd()
    {
        canPlant = true;
    }
}
