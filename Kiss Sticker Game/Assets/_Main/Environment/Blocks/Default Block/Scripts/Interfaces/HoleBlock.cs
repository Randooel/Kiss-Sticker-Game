using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class HoleBlock : BlockClass
{
    [TabGroup("Fall Animation Related")]
    [SerializeField] private float _duration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var entitie = collision.transform;
        if (entitie.CompareTag("Player"))
        {
            DOFall(entitie, true);
        }
        else if (entitie.GetComponent<BlockClass>())
        {
            DOFall(entitie, false);
        }
    }

    private void DOFall(Transform entitie, bool isPlayer)
    {
        entitie.DOKill();

        EntitieChecks(entitie, isPlayer);

        entitie.DOLocalMove(this.transform.position, 0).OnComplete(() =>
        {
            entitie.DORotate(new Vector3(0f, 0f, 360f), _duration, RotateMode.FastBeyond360).SetLoops(-1);
            entitie.DOScale(Vector3.zero, _duration).OnComplete(() =>
            {
                entitie.DOKill();

                if (entitie.GetComponent<Duplicate>())
                {
                    entitie.GetComponent<Duplicate>().DestroyDuplicate();
                }

                entitie.gameObject.SetActive(false);
            });
        });
    }

    private void EntitieChecks(Transform entitie, bool isPlayer)
    {
        if (isPlayer)
        {
            entitie.GetComponent<PlayerMovement>().DisableMovement();
        }

        if(entitie.GetComponent<BoxCollider2D>() != null)
        {
            entitie.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}