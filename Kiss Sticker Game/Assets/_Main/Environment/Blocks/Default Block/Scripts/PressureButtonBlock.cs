using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

// This class is responsible for activating other blocks actions
public class PressureButtonBlock : BlockClass, IActionable
{
    // References to the blocks IActionable interface
    [PropertySpace(SpaceBefore = 15)]
    [SerializeField] private List<BlockClass> _actionableBlocks = new List<BlockClass>();
    [SerializeField] private List<IActionable> _iActionables = new List<IActionable>();


    void Start()
    {
        TransferElements();
    }

    private void TransferElements()
    {
        // Transfers elements from _actionableBlocks to _actionables
        foreach (var b in _actionableBlocks)
        {
            var a = b.GetComponent<IActionable>();
            _iActionables.Add(a);
        }
    }


    #region Collision Treatment
    // Once something touches it
    private void OnTriggerEnter2D()
    {
        OnAction();
    }
    // Once something exit it
    private void OnTriggerExit2D(Collider2D collision)
    {
        UndoAction();
    }
    #endregion



    #region IActionable Related
    // OnPress()
    public void OnAction()
    {
        // Activates all the actionables' OnAction() tied to this script
        foreach(var a in _iActionables)
        {
            a.OnAction();
        }
    }

    // UndoPress()
    public void UndoAction()
    {
        // Deactivates all the actionables' OnAction() tied to this script
        foreach (var a in _iActionables)
        {
            a.UndoAction();
        }
    }
    #endregion
}
