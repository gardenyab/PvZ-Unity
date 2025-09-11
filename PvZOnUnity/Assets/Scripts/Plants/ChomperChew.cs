using UnityEngine;

public class ChomperChew : MonoBehaviour
{
    public GameObject Chomper;

    private void toChew()
    {
        gameObject.GetComponent<Animator>().SetTrigger("back");
        gameObject.GetComponent<Animator>().SetTrigger("chew");
        Invoke("toSwallow", Chomper.GetComponent<Chomper>().recharge);
    }

    private void toSwallow()
    {
        gameObject.GetComponent<Animator>().SetTrigger("swallow");
    }

    private void setEat()
    {
        Chomper.GetComponent<Chomper>().isEat = false;
    }
}
