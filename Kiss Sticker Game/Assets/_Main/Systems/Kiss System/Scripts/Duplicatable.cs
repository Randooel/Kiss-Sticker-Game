using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Duplicatable : MonoBehaviour
{
    [SerializeField] private bool _isBreakable;

    [ShowIf("_isBreakable")]
    public Transform Cracks;

    public List<Duplicate> Duplicates = new List<Duplicate>();

    private void Start()
    {
        //_cracks = this.transform.Find("Cracks");
        Cracks.gameObject.SetActive(false);
    }

    #region State Related
    #region State Machine
    public enum State
    {
        Normal,
        Damaged,
        Broken
    }

    protected State _currentState;

    public State CurrentState => _currentState;

    public void SwitchState()
    {
        if(_isBreakable)
        {
            switch (_currentState)
            {
                // Since this switch is only called during colllision with duplicates, the states call the next state
                // Ex: If State was Normal, since it collided with a duplicate, now it is Damaged, and so on.
                case State.Normal:
                    HandleDamaged();
                    break;
                case State.Damaged:
                    HandleBroken();
                    break;
            }
        }
    }
    #endregion

    #region Handle States
    [System.Obsolete]
    protected void HandleNormal()
    {
        throw new NotImplementedException();
    }

    private void HandleDamaged()
    {
        _currentState = State.Damaged;
        if(Cracks != null)
        {
            Cracks.gameObject.SetActive(true);
        }
    }

    private void HandleBroken()
    {
        _currentState = State.Broken;
        DestroyAllDuplicates();

        this.gameObject.SetActive(false);
    }
    #endregion
    #endregion



    #region Handle Duplication
    public virtual void OnDuplicate()
    {
        FindFirstObjectByType<StickerManager>().AddDuplicate(this.gameObject);
    }
    public virtual void OnUndoDuplicate()
    {
        FindFirstObjectByType<StickerManager>().RemoveDuplicatePreparation(this.gameObject);
    }

    protected void DestroyAllDuplicates()
    {
        foreach(var d in Duplicates)
        {
            d.DODestroyDuplicate();
        }
    }
    #endregion
}
