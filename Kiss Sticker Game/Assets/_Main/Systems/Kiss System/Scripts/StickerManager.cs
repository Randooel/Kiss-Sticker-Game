using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using DG.Tweening;

public class StickerManager : MonoBehaviour
{
    #region Stickers Related
    [Title("Stiker Related")]
    [SerializeField] private Transform _stickerParent;

    [Space(10)]
    [SerializeField, ReadOnly] private int _availableStickers;

    [Space(5)]
    [SerializeField] private List<Transform> _stickers = new List<Transform>();
    #endregion

    #region Handle Clone Variables
    [Title("Clone Related")]
    [PropertySpace(SpaceBefore = 15)]
    [SerializeField] private Transform _duplicateParent;
    [SerializeField] private List<GameObject> _duplicates = new List<GameObject>();
    #endregion

    void Start()
    {
        _availableStickers = _stickers.Count;

        foreach(var sticker in _stickers)
        {
            sticker.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    #region Handle Stickesr
    public bool CheckStickers()
    {
        // If it has stickers left, lets the duplicate process continue. Else doesn't
        if (_availableStickers <= _stickers.Count && _availableStickers > 0)
        {
            return true;
        }
        else return false;
    }

    private void AddSticker(Transform duplicate)
    {
        var sticker = _stickers[_availableStickers - 1];

        sticker.gameObject.SetActive(true);
        sticker.position = duplicate.position;
        sticker.parent = duplicate;

        _availableStickers--;
    }

    private void RemoveSticker(int index)
    {
        var sticker = _stickers[index];

        sticker.parent = _stickerParent;
        sticker.position = _stickerParent.position;
        sticker.gameObject.SetActive(false);

        _availableStickers++;
    }
    #endregion

    #region Handle Duplicates 
    public void AddDuplicate(GameObject original)
    {
        if (CheckStickers())
        {
            var duplicate = Instantiate(original);

            ConfigureDuplicate(duplicate);
        }
        else Debug.Log("Can't duplicate. No stickers left! :(");
    }

    public void RemoveDuplicate(GameObject duplicate)
    {
        _duplicates.Remove(duplicate);
        RemoveSticker(duplicate.GetComponent<Duplicate>().index);

        DOVirtual.DelayedCall(0.1f, () =>
        {
            Destroy(duplicate);
        });
        
    }

    public void ConfigureDuplicate(GameObject duplicate)
    {
        _duplicates.Add(duplicate);
        duplicate.transform.parent = _duplicateParent;
        duplicate.AddComponent<Duplicate>().index = _stickers.Count - _availableStickers;

        AddSticker(duplicate.transform);
    }

    public void CleanDuplicates()
    {
        _duplicates.Clear();
    }
    #endregion
}
