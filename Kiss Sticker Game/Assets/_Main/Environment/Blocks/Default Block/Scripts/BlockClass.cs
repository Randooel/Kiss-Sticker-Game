using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class BlockClass : MonoBehaviour, IDuplicatable
{
    [TabGroup("Duplicates Variables")]
    [SerializeField] [ReadOnly] private int _duplicateIndex;
    [SerializeField] [ReadOnly] private List<BlockClass> _duplicates = new List<BlockClass>();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colided with" + collision.gameObject.name);
    }

    #region Handle Duplication
    public void OnDuplicate()
    {
        _duplicates.Add(Instantiate(this));

        Debug.Log("Duplicate!");
    }

    public void OnUndoDuplicate()
    {
        Debug.Log("Undo Duplicate!");
    }
    #endregion
}
