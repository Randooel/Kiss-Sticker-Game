using UnityEngine;



public class FlagBlock : BlockClass
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // If it collides with Player, the level is completed
        if(collision.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().OnWin();
        }
    }
}
