using System.Collections.Generic;
using UnityEngine;



public class PlayerCollision : MonoBehaviour, IDuplicatable
{
    public void OnDuplicate()
    {
        Debug.Log("Duplication done!");
    }

    public void OnUndoDuplicate()
    {
        Debug.Log("Duplication undone!");
    }
}
