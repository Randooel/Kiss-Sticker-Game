using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using DG.Tweening;
using Unity.VisualScripting;
using static UnityEngine.UI.Image;

public class StickerManager : MonoBehaviour
{
    #region Stickers Related
    [Title("Stiker Related")]
    [SerializeField] private Transform _stickerParent;

    [Space(10)]
    [SerializeField, ReadOnly] private int _maxStickers;
    [Space(10)]
    [SerializeField] private List<Sticker> _availableStickers = new List<Sticker>();
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

        sticker.transform.parent = duplicate;
        sticker.transform.position = duplicate.position;
        sticker.gameObject.SetActive(true);
        sticker.DOAddAnim();

        _availableStickers.Remove(_availableStickers[index]);
    }

    public void RemoveSticker(Sticker sticker)
    {
        sticker.transform.parent = _stickerParent;
        sticker.transform.position = _stickerParent.position;
        sticker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sticker.gameObject.SetActive(false);

        _availableStickers.Add(sticker);
    }

    private void ResetStickers()
    {
        foreach(Transform sticker in _stickerParent)
        {
            _availableStickers.Add(sticker.GetComponent<Sticker>());
        }
    }
    #endregion

    #region Handle Duplicates 
    public void AddDuplicate(GameObject original)
    {
        // Checks if there is any sticker left
        if (CheckStickers())
        {
            var duplicate = Instantiate(original);

            ConfigureDuplicate(duplicate, original.transform);

            // Adds this duplicate to the original object's Duplicates list
            original.GetComponent<Duplicatable>().Duplicates.Add(duplicate.GetComponent<Duplicate>());
        }
        else Debug.Log("Can't duplicate. No stickers left! :("); return;
    }

    // Calls duplicate and sticker remove animations. Is called once the player kiss a GameObject with the Duplicate script.
    public void RemoveDuplicatePreparation(GameObject duplicate)
    {
        // Sets the duplicate variables
        var dC = duplicate.GetComponent<Duplicate>();
        var original = dC.Original;
        dC.HasSticker = false; // Once this is changed, the Duplicate FixedUpdate will automatically call the ReturnToOriginal()

        var sticker = dC.Sticker;
        sticker.DORemoveAnim();
    }

    // Destroy the duplicate. Should only be called once the duplicate hits the original.
    public void RemoveDuplicate(GameObject duplicate)
    {
        _duplicates.Remove(duplicate);

        var dC = duplicate.GetComponent<Duplicate>();
        var original = dC.Original;

        // Removes the duplicate from the original object's Duplicates list
        original.GetComponent<Duplicatable>().Duplicates.Remove(dC);
        //RemoveSticker(duplicate.GetComponent<Duplicate>());

        Destroy(duplicate);
    }

    public void ConfigureDuplicate(GameObject duplicate, Transform original)
    {
        _duplicates.Add(duplicate);
        duplicate.transform.parent = _duplicateParent;

        var dC = duplicate.AddComponent<Duplicate>();
        dC.Sticker = _availableStickers[_availableStickers.Count - 1];
        dC.Original = original;
        dC.HasSticker = true;

        AddSticker(duplicate.transform);
    }

    public void CleanDuplicates()
    {
        _duplicates.Clear();
    }
    #endregion
}
