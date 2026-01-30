using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using DG.Tweening;
using Unity.VisualScripting;

public class StickerManager : MonoBehaviour
{
    #region Stickers Related
    [Title("Stiker Related")]
    [SerializeField] private Transform _stickerParent;

    [Space(10)]
    [SerializeField, ReadOnly] private int _maxStickers;
    [Space(10)]
    [SerializeField] private List<Transform> _availableStickers = new List<Transform>();
    #endregion

    #region Handle Clone Variables
    [Title("Clone Related")]
    [PropertySpace(SpaceBefore = 15)]
    [SerializeField] private Transform _duplicateParent;
    [SerializeField] private List<GameObject> _duplicates = new List<GameObject>();
    #endregion

    void Start()
    {
        #region Setting Stickers Up
        ResetStickers();

        _maxStickers = _availableStickers.Count;

        foreach(var sticker in _availableStickers)
        {
            sticker.gameObject.SetActive(false);
        }
        #endregion
    }

    void Update()
    {
        
    }

    #region Handle Stickesr
    public bool CheckStickers()
    {
        // If it has stickers left, lets the duplicate process continue. Else doesn't
        var availableStickers = _availableStickers.Count;
        if (availableStickers <= _maxStickers && availableStickers > 0)
        {
            return true;
        }
        else return false;
    }

    private void AddSticker(Transform duplicate)
    {
        var index = _availableStickers.Count - 1;
        var sticker = _availableStickers[index];

        sticker.gameObject.SetActive(true);
        sticker.position = duplicate.position;
        sticker.parent = duplicate;

        _availableStickers.Remove(_availableStickers[index]);
    }

    private void RemoveSticker(Duplicate duplicate)
    {
        var sticker = duplicate.sticker;

        sticker.parent = _stickerParent;
        sticker.position = _stickerParent.position;
        sticker.gameObject.SetActive(false);

        _availableStickers.Add(sticker);
    }

    private void ResetStickers()
    {
        foreach(Transform sticker in _stickerParent)
        {
            _availableStickers.Add(sticker);
        }
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
        else Debug.Log("Can't duplicate. No stickers left! :("); return;
    }

    public void RemoveDuplicate(GameObject duplicate)
    {
        _duplicates.Remove(duplicate);
        RemoveSticker(duplicate.GetComponent<Duplicate>());

        Destroy(duplicate);

        /*
        DOVirtual.DelayedCall(0.1f, () =>
        {
            Destroy(duplicate);
        });
        */
    }

    public void ConfigureDuplicate(GameObject duplicate)
    {
        _duplicates.Add(duplicate);
        duplicate.transform.parent = _duplicateParent;
        duplicate.AddComponent<Duplicate>().sticker = _availableStickers[_availableStickers.Count - 1];

        AddSticker(duplicate.transform);
    }

    public void CleanDuplicates()
    {
        _duplicates.Clear();
    }
    #endregion
}
