using UnityEngine;
using DG.Tweening;

public class DOAnimations : MonoBehaviour
{
    #region Duplication Related Variables
    private bool _hitOriginal;
    #endregion

    void FixedUpdate()
    {

    }

    #region Duplication Related Animations
    public void DODuplicate()
    {

    }

    public void DOUndoDuplicate(Transform duplicate, Transform original /*, Transform sticker */)
    {
        duplicate.DOKill();

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
    }

    public void DODestruction(Transform duplicate)
    {
        // TODO: Destruction animation goes here

        FindAnyObjectByType<StickerManager>().RemoveDuplicate(duplicate.gameObject);
    }
    #endregion
}