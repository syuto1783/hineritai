using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameratest : MonoBehaviour
{
    //�ŏ��ɍ������ʂ�width
    public float defaultWidth = 16.0f;

    //�ŏ��ɍ������ʂ�height
    public float defaultHeight = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        //camera.main��ϐ��Ɋi�[
        Camera mainCamera = Camera.main;

        //�ŏ��ɍ������ʂ̃A�X�y�N�g�� 
        float defaultAspect = defaultWidth / defaultHeight;

        //���ۂ̉�ʂ̃A�X�y�N�g��
        float actualAspect = (float)Screen.width / (float)Screen.height;

        //���@��unity��ʂ̔䗦
        float ratio = actualAspect / defaultAspect;

        //�T�C�Y����
        mainCamera.orthographicSize /= ratio;
    }

}
