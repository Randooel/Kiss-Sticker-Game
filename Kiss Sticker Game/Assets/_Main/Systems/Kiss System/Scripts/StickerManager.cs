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
    [SerializeField, ReadOnly] private List<int> _availableStickers = new List<int>();

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
        #region Setting Stickers Up
        foreach(var sticker in _stickers)
        {
            sticker.gameObject.SetActive(false);
        }

        for(int i = 0; i < _stickers.Count; i++)
        {
            _availableStickers.Add(i);
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
        var aStickers = _availableStickers.Count;
        if (aStickers <= _stickers.Count && aStickers > 0)
        {
            return true;
        }
        else return false;
    }

    private void AddSticker(Transform duplicate)
    {
        var index = duplicate.GetComponent<Duplicate>().index;
        var sticker = _stickers[index];

        sticker.gameObject.SetActive(true);
        sticker.position = duplicate.position;
        sticker.parent = duplicate;
        
        HandleStickerIndex(true, index);
    }

    private void RemoveSticker(int index)
    {
        var sticker = _stickers[index];

        sticker.parent = _stickerParent;
        sticker.position = _stickerParent.position;
        sticker.gameObject.SetActive(false);

        HandleStickerIndex(false, index);
    }

    private void HandleStickerIndex(bool add, int index)
    {
        if (add)
        {
            if(CheckStickers())
            {
                for (int i = 0; i < _stickers.Count; i++)
                {
                    if (_availableStickers[i] == index)
                    {
                        _availableStickers.Remove(_availableStickers[i]);
                        return;
                    }
                }
            }
        }
        else
        {
            _availableStickers.Add(index);
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
        duplicate.AddComponent<Duplicate>().index = _stickers.Count - _availableStickers.Count;

        AddSticker(duplicate.transform);
    }

    public void CleanDuplicates()
    {
        _duplicates.Clear();
    }
    #endregion
}
