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
        var duplicate = Instantiate(this.gameObject);
        FindFirstObjectByType<StickerManager>().AddDuplicate(duplicate);
    }

    public void OnUndoDuplicate()
    {
        Debug.Log("Undo Duplicate!");
    }
    #endregion
}
