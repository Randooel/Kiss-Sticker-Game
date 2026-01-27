using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class PlayerDuplicates : MonoBehaviour, IDuplicatable
{
    #region Duplication Related Functions
    public void OnDuplicate()
    {
        var duplicate = Instantiate(this.gameObject);
        FindFirstObjectByType<StickerManager>().AddDuplicate(duplicate);
    }

    public void OnUndoDuplicate()
    {
        Debug.Log("Player duplication undone!");
    }
    #endregion
}
