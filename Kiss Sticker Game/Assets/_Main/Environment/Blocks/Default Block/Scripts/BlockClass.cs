using UnityEngine;

public class BlockClass : MonoBehaviour, IDuplicatable
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void OnDuplicate()
    {
        Instantiate(this);
    }

    public void OnUndoDuplicate()
    {

    }
}
