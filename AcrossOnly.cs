using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**********************************************************
//  ���̃X�N���v�g�͓����蔻����������܂Ƃ߂郂�m�ł���B
//**********************************************************
public class AcrossOnly : MonoBehaviour
{
    //****************************
    //   �O���[�o���ϐ��錾
    //****************************

    public int Throughflg = 0;

    PlayerController script;  //PlayerController�̃X�N���v�g���i�[����ϐ�
    GameObject player2;        //�v���C���[���̂��̂��i�[����ϐ�
    BoxCollider2D m_objectCollider;
    GameObject redbox;


    void Start()
    {
        player2 = GameObject.Find("Player");     //�v���C���[���I�u�W�F�N�g�̖��O����擾���ĕϐ��ɃL���b�V��
        script = player2.GetComponent<PlayerController>();   //Player�̒��ɂ���PlayerController�X�N���v�g���擾���ĕϐ��ɃL���b�V��
        m_objectCollider = GetComponent<BoxCollider2D>();
        redbox = GameObject.Find("OnlyRed");     //�v���C���[���I�u�W�F�N�g�̖��O����擾���ĕϐ��ɃL���b�V��

        m_objectCollider.isTrigger = false;
        
    }



    void Update()
    {
        //�c���������Ă���Ԃ͕�����ɓ�����Ȃ�
        if (script.direction == PlayerController.Direction.up)
        {
            Throughflg = 1;
        }
        else if (script.direction == PlayerController.Direction.down)
        {

            Throughflg = 1;
        }
        else if (script.direction == PlayerController.Direction.right)
        {

            Throughflg = 2;
        }
        else if (script.direction == PlayerController.Direction.reft)
        {

            Throughflg = 2;
        }

        if (Throughflg == 2)
        {
            m_objectCollider.isTrigger = true;
        }
        else
        {
            m_objectCollider.isTrigger = false;
        }

    }

   
}
