using UnityEngine;



// This interface is used to mark duplicatable objects and handle their duplication and undo-duplication
[System.Obsolete] // Is was replaced by the Duplicatable class
public interface IDuplicatable
{
    void OnDuplicate();
    void OnUndoDuplicate();
}
