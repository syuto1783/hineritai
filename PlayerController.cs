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
    //    �N���X�p�����O���[�o���錾
    //***********************************

    Rigidbody2D rb;                 //�ʒu�G�l���M�[�̎擾
    Renderer rnd;                   //�����_�[�̎擾
    CameraSizeUpdater ScreensSize;  //�X�N���[���T�C�Y�̎擾
    CollisionDetection collision;   //�����蔻��̎擾
    MovingCounter movingCounter;    //���͐����̎擾
    AudioContlloer audioContlloer;  //�T�E���h�̎擾
    GetPattern getPattern;          //�A�C�e���l���v�f�̎擾
    FollowTracks follow;        �@�@//���Ղ̎擾
    pausemenu pause;


    float IntervalTime = 0.309f;   //�ړ����̃v���C���[�𐧌䂷��C���^�[�o���^�C��
    //[SerializeField] public float velocity = 3.0f; //�P�}�X�̈ړ���
    public bool isinput = false;                          //���͂��ꂽ���ۂ��̔���t���O
    public float speed_x;
    public float speed_y;
    Vector3 memo_pos;
    float poscomx;
    float poscomy;
    public int CanInput = 1;
    public bool inred = false;
    public bool inbule = false;

    float time = 0;                                //�C���^�[�o���^�C���ɒB����܂Ōv������^�C���ϐ�
    public float movecnt = 0;                        //�ړ������́u�萔�R���R�}�X�v�ɒB����܂ŉ��Z�����J�E���g�ϐ�

    //***********************************
    //  �@�X�^�[�g�֐�
    //***********************************
    void Start()
    {
        //�p�������e�X�̕ϐ��ɃR���|�[�l���g���L���b�V������
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
    //    �A�b�v�f�[�g�֐�
    //***********************************
    public void Update()
    {
        if (!isinput)
        {
            Move();
        }
    }
    //fixed�̊Ԋu��0.02f�b
    public void FixedUpdate()
    {
      //  Debug.Log("�^�C����" + time);

        //�����uW�AS�AA�AD�L�[�v�̂ǂꂩ�����͂��ꂽ��
        if (isinput)
        {
            this.time += Time.deltaTime;    //�ϐ��utime�v�Ɍ������E�̎��ԑ��x�����Z
            

            poscomx = gameObject.transform.position.x;
            poscomy = gameObject.transform.position.y;

            Debug.Log("�J�E���g��"+movecnt);
            //Debug.Log("�o�ߎ���" + time);
            //Debug.Log("" + gameObject.transform.position);
            //Debug.Log("���������|�W�V����" + memo_pos);
            //Debug.Log("�|�W�V������" +poscomx);

            //�����utime�v���uIntervalTime = 3.5f�v���傫�����l�ɂȂ�����
            if (time > IntervalTime)
            {
                time = 0;      //�ϐ��utime�v�����ɖ߂�

                //�uMove�֐��v�ŏ������ꂽ���ʂ��udirection�v�ɑ�����ăX�C�b�`���򂳂���
                switch (direction)
                {
                    case Direction.up:   //�@W�L�[��������

                        poscomy = Mathf.Floor(poscomy);
                        poscomy += 0.5f;
                        poscomx = memo_pos.x;

                        gameObject.transform.position = new Vector3(poscomx, poscomy, 0.0f);

                        Debug.Log("" + gameObject.transform.position);

                        //�ʒu�G�l���M�[�Ɉړ���������
                        rb.velocity = new Vector2(speed_x, 3.125f);
                        //rb.MovePosition(rb.position + new Vector2(0, velocity) * Time.fixedDeltaTime);
                        //speed_y = 3;
                        follow.BlueMethod();
                        break;

                    case Direction.down:  //�@S�L�[��������

                        poscomy = Mathf.Ceil(poscomy);
                        poscomy -= 0.5f;
                        poscomx = memo_pos.x;
                        gameObject.transform.position = new Vector3(poscomx, poscomy, 0.0f);

                        rb.velocity = new Vector2(speed_x, -3.125f);
                        //speed_y = -3;
                        //rb.MovePosition(rb.position + new Vector2(0, -velocity) * Time.fixedDeltaTime);
                        follow.BlueMethod();
                        break;

                    case Direction.reft:  //�@A�L�[��������

                        poscomx = Mathf.Ceil(poscomx);
                        poscomx -= 0.5f;
                        poscomy = memo_pos.y;
                        gameObject.transform.position = new Vector3(poscomx, poscomy, 0.0f);

                        rb.velocity = new Vector2(-3.125f, speed_y);
                        //speed_x = 3;
                        //rb.MovePosition(rb.position + new Vector2(-velocity, 0) * Time.fixedDeltaTime);
                        follow.RedMethod();
                        break;

                    case Direction.right: //�@D�L�[��������

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
                    //�ŏ��̈���ڂŐ�̃}�X�ڂւƈړ����s�\�������ꍇ(�v�Z���ʂ����߂̃��������|�W�V�����ƕς���Ă��Ȃ��ꍇ)
                    if (memo_pos == gameObject.transform.position)
                    {
                        isinput = false;
                        movecnt = 0;
                        rb.velocity = new Vector2(0, 0);
                        Debug.Log("��" );

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
                        Debug.Log("��");

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
            //�@W�L�[(�����)
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
            //�@A�L�[(������)
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

            //�@S�L�[(������)
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
            //�@D�L�[(�E����)
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

