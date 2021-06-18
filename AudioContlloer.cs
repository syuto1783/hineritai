using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContlloer : MonoBehaviour
{

    public AudioClip sound1;    //�A�C�e���Q�b�g��
    public AudioClip sound2;    //�v���C���[�̑���
    public AudioClip sound3;    //�{�^�����������̉��i�X�^�[�g�⃊�X�^�[�g�Ȃǁj
    public AudioClip hasami;
    public AudioClip start;
    public AudioClip select;
    public AudioClip letplay;
   
    AudioSource audioSource;
    

    void Start()
    {
        //Component���擾
        audioSource = GetComponent<AudioSource>();

    }

   
    void Update()
    {
       
    }

    public void Move_audio()
    {
        audioSource.PlayOneShot(sound2);
    }

    public void Item_audio()
    {
        audioSource.PlayOneShot(sound1);
    }

    public void MoveCarsol()
    {
        audioSource.PlayOneShot(sound3);
    }

    public void Cut_Player()
    {
        audioSource.PlayOneShot(hasami);
    }
    public void StartGame()
    {
        audioSource.PlayOneShot(start);
    }

    public void SelectStage()
    {
        audioSource.PlayOneShot(select);
    }

    public void LetPlay()
    {
        audioSource.PlayOneShot(letplay);
    }


}
