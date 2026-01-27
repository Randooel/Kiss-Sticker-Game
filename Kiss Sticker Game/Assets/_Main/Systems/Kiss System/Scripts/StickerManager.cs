using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class StickerManager : MonoBehaviour
{
    #region Stickers Related
    [Title("Stiker Related")]
    [SerializeField] private Transform _stickerParent;

    [Space(10)]
    [SerializeField] private int _maxStickers;
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
        foreach(var sticker in _stickers)
        {
            sticker.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    #region Add Sticker
    private void AddSticker(Transform duplicate)
    {
        var sticker = _stickers[_duplicates.Count - 1];

        sticker.gameObject.SetActive(true);
        sticker.position = duplicate.position;
        sticker.parent = duplicate;
    }

    private void RemoveSticker(int index)
    {
        var sticker = _stickers[index];

        sticker.gameObject.SetActive(true);
        sticker.position = _stickerParent.position;
        sticker.parent = _stickerParent;

    }
    #endregion

    #region Handle Duplicates 
    public void AddDuplicate(GameObject duplicate)
    {
        _duplicates.Add(duplicate);
        duplicate.transform.parent = _duplicateParent;
        duplicate.AddComponent<Duplicate>().index = _duplicates.Count -1;

        AddSticker(duplicate.transform);
    }

    public void RemoveDuplicates(GameObject duplicate)
    {
        _duplicates.Remove(duplicate);
    }

    public void CleanDuplicates()
    {
        _duplicates.Clear();
    }
    #endregion
}
