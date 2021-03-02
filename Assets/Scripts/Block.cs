using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int durability;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (durability > 0)
        {
            durability--;
        } else
        {
          Destroy(gameObject);
        }
    }

    public void setDurability(int _durability)
    {
        durability = _durability;
    }
}
