using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;



public class Duplicate : MonoBehaviour
{
    [Title("Transform References")]
    public Sticker sticker;
    public Transform original;
    private Rigidbody2D rb;

    [Space(10)]
    public bool hasSticker;

    [Title("Speed Variables")]
    [Space(10)]
    public float force = 200f;
    public float acceleration = 0.5f; // Has to be a small value, since it will be increased every frame on the FixedUpdate()
    public float currentAcceleration = 0f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!hasSticker)
        {
            ReturnToOriginal();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasSticker && collision.transform == original)
        {
            this.DOKill();
            DODestruction();
        }
    }

    #region Undo Duplication Animation
    public void ReturnToOriginal()
    {
        float distance = Vector2.Distance(this.transform.position, original.position);

        // Direction = destiny - origin
        Vector2 direction = (original.position - this.transform.position).normalized;

        currentAcceleration = Mathf.MoveTowards(currentAcceleration, force, acceleration * Time.fixedDeltaTime);

        rb.AddForce(direction * force * currentAcceleration, ForceMode2D.Force);

        /*
        // Gets the original direction (relative to the duplicate)
        var direction = (duplicate.position - original.position).normalized;

        Tweener t = null;
        // TODO: SET ACCELERATION
        t = duplicate.DOLocalMove(original.position, 10).SetSpeedBased().SetLoops(-1).OnUpdate(() =>
        {
            float distance = Vector3.Distance(duplicate.position, original.position);

            // If "collides" with the original, handle the duplication destruction
            if (distance < 1f)
            {
                duplicate.DOKill();

                DODestruction(duplicate);
            }
            else
            {
                // Updates destiny if the object moves
                t.ChangeEndValue(original.position, true);
            }
        });
        */
    }

    public void DODestruction()
    {
        //this.DOKill();

        // Actually it doesn't have a sticker. This is used only to cancel the ReturnToOriginal() from running in the FixedUpdate()
        hasSticker = true;
        //rb.simulated = false;

        var dur = 0.1f;
        // TODO: Destruction animation goes here
        Sequence s = DOTween.Sequence();
        s.Append(this.transform.DOScale(transform.localScale * 1.25f, dur));
        s.Append(this.transform.DOScale(Vector3.zero, dur)).OnComplete(() =>
        {
            FindAnyObjectByType<StickerManager>().RemoveDuplicate(this.gameObject);
        });

    }
    #endregion
}
