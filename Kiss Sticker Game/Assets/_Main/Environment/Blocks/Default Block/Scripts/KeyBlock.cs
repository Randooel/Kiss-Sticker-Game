using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class KeyBlock : BlockClass
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if(collision.gameObject.CompareTag("Player"))
        {
            // Turns the collider off
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        */
    }
}
