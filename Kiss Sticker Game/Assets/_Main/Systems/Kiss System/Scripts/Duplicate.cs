using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;



public class Duplicate : MonoBehaviour
{
    [Title("Transform References")]
    public Sticker Sticker;
    public Transform Original;
    //private Rigidbody2D _rb;

    [Space(10)]
    public bool HasSticker;

    [Title("Speed Variables")]
    [Space(10)]
    private float _force = 50f; // using _rb force = 200f;
    private float _distance;

    public float Acceleration = 0.5f; // Has to be a small value, since it will be increased every frame on the FixedUpdate()
    public float CurrentAcceleration = 0f;

    private void Start()
    {
        //_rb = this.GetComponent<Rigidbody2D>();

        DODuplicateAnim();
    }

    private void FixedUpdate()
    {
        if(!HasSticker)
        {
            ReturnToOriginal();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!HasSticker && collision.transform == Original)
        {
            this.DOKill();
            DODestruction();
        }
    }

    #region Duplicate Animation
    private void DODuplicateAnim()
    {
        var dur = 0.05f;

        Sequence s = DOTween.Sequence();
        s.Append(this.transform.DOScale(Vector3.zero, 0));
        s.Append(this.transform.DOScale(this.transform.localScale * 1.25f, dur));
        s.Append(this.transform.DOScale(this.transform.localScale, dur)).OnComplete(() =>
        {
            this.DOKill();
        });
    }
    #endregion

    #region Undo Duplication Animation
    public void ReturnToOriginal()
    {
        #region DOTween with acceleration
        // Moves this duplicate towards the original position
        _distance = Vector3.Distance(this.transform.position, Original.position);
        this.transform.DOLocalMove(Original.position, _force).SetSpeedBased().SetEase(Ease.InQuad);
        #endregion

        #region rb AddForce
        /*
        float distance = Vector2.Distance(this.transform.position, Original.position);

        // Direction = destiny - origin
        Vector2 direction = (Original.position - this.transform.position).normalized;

        CurrentAcceleration = Mathf.MoveTowards(CurrentAcceleration, Force, Acceleration * Time.fixedDeltaTime);

        _rb.AddForce(direction * Force * CurrentAcceleration, ForceMode2D.Force);
        */
        #endregion

        #region First DOTween Attempt
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
        #endregion
    }

    public void DODestruction()
    {
        //this.DOKill();

        // Actually it doesn't have a sticker. This is used only to cancel the ReturnToOriginal() from running in the FixedUpdate()
        HasSticker = true;
        //rb.simulated = false;

        var dur = 0.1f;
        // TODO: Destruction animation goes here
        Sequence s = DOTween.Sequence();
        s.Append(this.transform.DOScale(transform.localScale * 1.25f, dur));
        s.Append(this.transform.DOScale(Vector3.zero, dur)).OnComplete(() =>
        {
            FindAnyObjectByType<StickerManager>().RemoveDuplicate(this.gameObject);
        });

        // Shakes the Original object when they collide
        var duration = 0.5f;
        var strength = (_distance / 5) * 2;

        /*
        Debug.Log("Distance: " + _distance);
        Debug.Log("Strength: " + strength);
        */

        Original.DOShakeScale(duration, strength);
        Original.DOShakeRotation(duration, strength);
        Original.DOShakePosition(duration, strength);

        DOVirtual.DelayedCall(duration, () =>
        {
            Debug.LogWarning("KILLED");
            Original.DOKill();
        });
    }
    #endregion
}
