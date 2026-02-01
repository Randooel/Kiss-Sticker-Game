using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class BlockClass : Duplicatable
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colided with" + collision.gameObject.name);
    }
}
