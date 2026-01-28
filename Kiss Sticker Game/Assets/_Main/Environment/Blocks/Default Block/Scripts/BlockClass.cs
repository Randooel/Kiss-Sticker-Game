using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class BlockClass : MonoBehaviour, IDuplicatable
{
    [TabGroup("Duplicates Variables")]
    [SerializeField] [ReadOnly] private int _duplicateIndex;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colided with" + collision.gameObject.name);
    }

    #region Handle Duplication
    public void OnDuplicate()
    {
        FindFirstObjectByType<StickerManager>().AddDuplicate(this.gameObject);
    }

    public void OnUndoDuplicate()
    {
        FindFirstObjectByType<StickerManager>().RemoveDuplicate(this.gameObject);
    }
    #endregion
}
