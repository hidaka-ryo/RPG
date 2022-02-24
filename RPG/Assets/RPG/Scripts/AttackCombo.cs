using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�A�j���[�V������State���Ǘ��ł���StateMachinBehaviour���g����I
public class AttackCombo : StateMachineBehaviour
{
    //�n�߂̏�Ԃ�Attack��false�ɂ��Ďg���Ȃ��悤�ɂ��܂��B
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
    }
    //�����}�E�X�̍��N���b�N�������ꂽ��Attack��true�ɂ��ăA�j���[�V�������g����悤�ɂ����I
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attack", true);
        }
    }
    //����ȊO�̏�Ԃł�Attack�̃A�j���[�V������false�ɂ��܂��B
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
    }
}
