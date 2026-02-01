using UnityEngine;
using DG.Tweening;

public class Sticker : MonoBehaviour
{
    [SerializeField] private float dur = 0.1f;

    public void DOAddAnim()
    {
        this.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(this.transform.DOScale(Vector3.zero, 0));
        s.Append(this.transform.DOScale(transform.localScale * 1.25f, dur).SetEase(Ease.OutSine));
        s.Append(this.transform.DOScale(transform.localScale, dur).SetEase(Ease.OutSine));
    }

    public void DORemoveAnim()
    {
        this.DOKill();

        this.transform.parent = null;

        Sequence s = DOTween.Sequence();
        s.Append(this.transform.DOScale(transform.localScale * 1.25f, dur).SetEase(Ease.OutSine));
        s.Append(this.transform.DOScale(Vector3.zero, dur).SetEase(Ease.OutSine)).OnComplete(() =>
        {
            FindAnyObjectByType<StickerManager>().RemoveSticker(this);
        });
    }
}
