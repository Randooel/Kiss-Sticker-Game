using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Duplicatable : MonoBehaviour
{
    #region State Related
    #region State Machine
    protected enum State
    {
        Normal,
        Damaged,
        Broken
    }

    protected State _currentState;

    protected void SwitchState(State newState)
    {
        _currentState = newState;

        switch(_currentState)
        {
            case State.Normal:
                HandleNormal();
                break;
            case State.Damaged:
                HandleDamaged();
                break;
            case State.Broken:
                HandleBroken();
                break;
        }
    }
    #endregion

    #region Handle States
    protected void HandleNormal()
    {
        throw new NotImplementedException();
    }

    private void HandleDamaged()
    {
        throw new NotImplementedException();
    }

    private void HandleBroken()
    {
        throw new NotImplementedException();
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
        FindFirstObjectByType<StickerManager>().RemoveDuplicate(this.gameObject);
    }
    #endregion
}
