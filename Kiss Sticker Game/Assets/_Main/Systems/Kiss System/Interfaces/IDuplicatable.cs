using UnityEngine;



// This interface is used to mark duplicatable objects and handle their duplication and undo-duplication
public interface IDuplicatable
{
    void OnDuplicate();
    void OnUndoDuplicate();
}
