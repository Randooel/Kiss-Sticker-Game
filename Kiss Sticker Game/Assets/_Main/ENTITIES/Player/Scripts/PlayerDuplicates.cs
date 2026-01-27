using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class PlayerDuplicates : MonoBehaviour, IDuplicatable
{
    #region Duplication Handle Functions
    public void OnDuplicate()
    {
        FindFirstObjectByType<StickerManager>().AddDuplicate(this.gameObject);
    }

    public void OnUndoDuplicate()
    {
        Debug.Log("Player duplication undone!");
    }
    #endregion
}
