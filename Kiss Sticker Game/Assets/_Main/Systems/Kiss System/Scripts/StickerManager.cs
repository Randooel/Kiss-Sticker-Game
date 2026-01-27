using UnityEngine;
using System.Collections.Generic;

public class StickerManager : MonoBehaviour
{
    private int _maxStickers;
    private int _stickers;

    #region Handle Clone Variables
    [SerializeField] private Transform _duplicateParent;
    [SerializeField] private List<GameObject> _duplicates = new List<GameObject>();
    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDuplicate(GameObject duplicate)
    {
        _duplicates.Add(duplicate);
        duplicate.transform.parent = _duplicateParent;
        duplicate.AddComponent<Duplicate>().index = _duplicates.Count -1;
    }

    public void RemoveDuplicates(GameObject duplicate)
    {
        _duplicates.Remove(duplicate);
    }

    public void CleanDuplicates()
    {
        _duplicates.Clear();
    }
}
