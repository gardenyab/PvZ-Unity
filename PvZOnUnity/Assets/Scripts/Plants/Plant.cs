using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int health = 4;

    public GameObject tile;

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            tile.GetComponent<Tile>().hasPlant = false;
            Destroy(gameObject);
        }
    }
}