using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] private Transform Head;
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right;
    [SerializeField] private Transform Target;

    [SerializeField] private bool isAktiveIK;
    [SerializeField] private bool isTarget;

    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!_anim)
            return;

        if(isAktiveIK)
        {
            if(!Head)
            {
                return;
            }

            _anim.SetLookAtWeight(1);
            _anim.SetLookAtPosition(Target.position);
            Head.LookAt(Target);

            if (!Left)
                return;

            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            _anim.SetIKPosition(AvatarIKGoal.RightHand, Right.position);

            _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _anim.SetIKPosition(AvatarIKGoal.LeftHand, Left.position);
        }

        if(isTarget)
        {
            Right.SetParent(Head);
            Left.SetParent(Head);
        }
        
    }
}
