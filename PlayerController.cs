using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

using define;

public class PlayerController : MonoBehaviour
{
    public enum Direction
    {
        up,
        down,
        right,
        reft,
    }
    public Direction direction;

    //***********************************
    //    クラス継承＆グローバル宣言
    //***********************************

    Rigidbody2D rb;                 //位置エネルギーの取得
    Renderer rnd;                   //レンダーの取得
    CameraSizeUpdater ScreensSize;  //スクリーンサイズの取得
    CollisionDetection collision;   //当たり判定の取得
    MovingCounter movingCounter;    //入力制限の取得
    AudioContlloer audioContlloer;  //サウンドの取得
    GetPattern getPattern;          //アイテム獲得要素の取得
    FollowTracks follow;        　　//足跡の取得
    pausemenu pause;


    float IntervalTime = 0.309f;   //移動中のプレイヤーを制御するインターバルタイム
    //[SerializeField] public float velocity = 3.0f; //１マスの移動量
    public bool isinput = false;                          //入力されたか否かの判定フラグ
    public float speed_x;
    public float speed_y;
    Vector3 memo_pos;
    float poscomx;
    float poscomy;
    public int CanInput = 1;
    public bool inred = false;
    public bool inbule = false;

    float time = 0;                                //インターバルタイムに達するまで計測するタイム変数
    public float movecnt = 0;                        //移動距離の「定数３＝３マス」に達するまで加算されるカウント変数

    //***********************************
    //  　スタート関数
    //***********************************
    void Start()
    {
        //継承した各々の変数にコンポーネントをキャッシュする
        rb = GetComponent<Rigidbody2D>();
        follow = GetComponent<FollowTracks>();
        rnd = GetComponent<Renderer>();
        collision = GetComponent<CollisionDetection>();
        movingCounter = GetComponent<MovingCounter>();
        audioContlloer = GetComponent<AudioContlloer>();
        getPattern = GetComponent<GetPattern>();
        pause = GetComponent<pausemenu>();

    }

    //***********************************
    //    アップデート関数
    //***********************************
    public void Update()
    {
        if (!isinput)
        {
            Move();
        }
    }
    //fixedの間隔は0.02f秒
    public void FixedUpdate()
    {
      //  Debug.Log("タイムは" + time);

        //もし「W、S、A、Dキー」のどれかが入力された時
        if (isinput)
        {
            this.time += Time.deltaTime;    //変数「time」に現実世界の時間速度を加算
            

            poscomx = gameObject.transform.position.x;
            poscomy = gameObject.transform.position.y;

            Debug.Log("カウントは"+movecnt);
            //Debug.Log("経過時間" + time);
            //Debug.Log("" + gameObject.transform.position);
            //Debug.Log("メモしたポジション" + memo_pos);
            //Debug.Log("ポジション比" +poscomx);

            //もし「time」が「IntervalTime = 3.5f」より大きい数値になった時
            if (time > IntervalTime)
            {
                time = 0;      //変数「time」を元に戻す

                //「Move関数」で処理された結果を「direction」に代入してスイッチ分岐させる
                switch (direction)
                {
                    case Direction.up:   //　Wキーだった時

                        poscomy = Mathf.Floor(poscomy);
                        poscomy += 0.5f;
                        poscomx = memo_pos.x;

                        gameObject.transform.position = new Vector3(poscomx, poscomy, 0.0f);

                        Debug.Log("" + gameObject.transform.position);

                        //位置エネルギーに移動を代入する
                        rb.velocity = new Vector2(speed_x, 3.125f);
                        //rb.MovePosition(rb.position + new Vector2(0, velocity) * Time.fixedDeltaTime);
                        //speed_y = 3;
                        follow.BlueMethod();
                        break;

                    case Direction.down:  //　Sキーだった時

                        poscomy = Mathf.Ceil(poscomy);
                        poscomy -= 0.5f;
                        poscomx = memo_pos.x;
                        gameObject.transform.position = new Vector3(poscomx, poscomy, 0.0f);

                        rb.velocity = new Vector2(speed_x, -3.125f);
                        //speed_y = -3;
                        //rb.MovePosition(rb.position + new Vector2(0, -velocity) * Time.fixedDeltaTime);
                        follow.BlueMethod();
                        break;

                    case Direction.reft:  //　Aキーだった時

                        poscomx = Mathf.Ceil(poscomx);
                        poscomx -= 0.5f;
                        poscomy = memo_pos.y;
                        gameObject.transform.position = new Vector3(poscomx, poscomy, 0.0f);

                        rb.velocity = new Vector2(-3.125f, speed_y);
                        //speed_x = 3;
                        //rb.MovePosition(rb.position + new Vector2(-velocity, 0) * Time.fixedDeltaTime);
                        follow.RedMethod();
                        break;

                    case Direction.right: //　Dキーだった時

                        poscomx = Mathf.Floor(poscomx);
                        poscomx += 0.5f;
                        poscomy = memo_pos.y;
                        gameObject.transform.position = new Vector3(poscomx, poscomy, 0.0f);

                        rb.velocity = new Vector2(3.125f, speed_y);

                        //speed_x = -3;
                        //rb.MovePosition(rb.position + new Vector2(velocity, 0) * Time.fixedDeltaTime);
                        follow.RedMethod();
                        break;

                }
                movecnt++;
                if (movecnt >= GrovalConst.MAX_MOVE_COUNT)
                {
                    isinput = false;
                    movecnt = 0;
                    rb.velocity = new Vector2(0, 0);
                    //time = 0;
                }
                if (movecnt == 2)
                {
                    //最初の一歩目で先のマス目へと移動が不可能だった場合(計算結果が初めのメモしたポジションと変わっていない場合)
                    if (memo_pos == gameObject.transform.position)
                    {
                        isinput = false;
                        movecnt = 0;
                        rb.velocity = new Vector2(0, 0);
                        Debug.Log("壁" );

                    }
                    else
                    {
                        movingCounter.MovingCount();
                    }
                }
                if(movecnt == 3)
                {
                    if (memo_pos == gameObject.transform.position)
                    {
                        isinput = false;
                        movecnt = 0;
                        rb.velocity = new Vector2(0, 0);
                        Debug.Log("壁");

                    }
                }
                memo_pos = gameObject.transform.position;

            }
        }

        movingCounter.CounterText.text = "" + movingCounter.movecount;

        
    }

    public void Move()
    {
        if (pause.moveStop == true)
        {
            //　Wキー(上入力)
            if (Input.GetKeyDown(KeyCode.W))
            {
                /*if(inred == true)
                {
                    return;
                }*/
                direction = Direction.up;
                rnd.material.color = Color.black;
                isinput = true;
                //movingCounter.MovingCount();
                audioContlloer.Move_audio();
                memo_pos = gameObject.transform.position;
                //pause.nowPauseSelect = 1;
                //limitcount.LimitCount();
                //itemGet.ItemGetFlg();
                
            }
            //　Aキー(左入力)
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (inbule == true)
                {
                    return;
                }
                direction = Direction.reft;
                rnd.material.color = Color.black;
                isinput = true;
                //movingCounter.MovingCount();
                audioContlloer.Move_audio();
                memo_pos = gameObject.transform.position;
                //pause.nowPauseSelect = 1;
                //limitcount.LimitCount();
                //itemGet.ItemGetFlg();
            }

            //　Sキー(下入力)
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (inred == true)
                {
                    return;
                }
                direction = Direction.down;
                rnd.material.color = Color.black;
                isinput = true;
                //movingCounter.MovingCount();
                audioContlloer.Move_audio();
                memo_pos = gameObject.transform.position;
                //pause.nowPauseSelect = 1;
                //limitcount.LimitCount();
                //itemGet.ItemGetFlg();
            }
            //　Dキー(右入力)
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (inbule == true)
                {
                    return;
                }
                direction = Direction.right;
                rnd.material.color = Color.black;
                isinput = true;
                //movingCounter.MovingCount();
                audioContlloer.Move_audio();
                memo_pos = gameObject.transform.position;
                //pause.nowPauseSelect = 1;
                //limitcount.LimitCount();
                //itemGet.ItemGetFlg();
            }

        }

    }
}

