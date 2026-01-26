using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    public GameObject visual;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    #region Animation Functions
    public void PlayIdle()
    {
        _animator.Play("Idle");
    }

    public void PlayWalk()
    {
        _animator.Play("Walk");
    }

    public void PlayKiss()
    {
        _animator.Play("Kiss");
    }
    #endregion
}
