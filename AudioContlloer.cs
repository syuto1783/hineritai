using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContlloer : MonoBehaviour
{

    public AudioClip sound1;    //アイテムゲット音
    public AudioClip sound2;    //プレイヤーの足音
    public AudioClip sound3;    //ボタン押した時の音（スタートやリスタートなど）
    public AudioClip hasami;
    public AudioClip start;
    public AudioClip select;
    public AudioClip letplay;
   
    AudioSource audioSource;
    

    void Start()
    {
        //Componentを取得
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
