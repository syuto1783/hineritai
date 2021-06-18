using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**********************************************************
//  このスクリプトは当たり判定を処理をまとめるモノである。
//**********************************************************
public class AcrossOnly : MonoBehaviour
{
    //****************************
    //   グローバル変数宣言
    //****************************

    public int Throughflg = 0;

    PlayerController script;  //PlayerControllerのスクリプトを格納する変数
    GameObject player2;        //プレイヤーそのものを格納する変数
    BoxCollider2D m_objectCollider;
    GameObject redbox;


    void Start()
    {
        player2 = GameObject.Find("Player");     //プレイヤーをオブジェクトの名前から取得して変数にキャッシュ
        script = player2.GetComponent<PlayerController>();   //Playerの中にあるPlayerControllerスクリプトを取得して変数にキャッシュ
        m_objectCollider = GetComponent<BoxCollider2D>();
        redbox = GameObject.Find("OnlyRed");     //プレイヤーをオブジェクトの名前から取得して変数にキャッシュ

        m_objectCollider.isTrigger = false;
        
    }



    void Update()
    {
        //縦軸が動いている間は柄を手に入れられない
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
