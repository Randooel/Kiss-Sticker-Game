using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DoorBlock : BlockClass, IActionable
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Key"))
        {
            OnAction();
            collision.gameObject.SetActive(false);
        }
    }

    // OpenDoor()
    public void OnAction()
    {
        this.gameObject.SetActive(false);
    }

    // CloseDoor()
    public void UndoAction()
    {
        this.gameObject.SetActive(true);
    }
}
