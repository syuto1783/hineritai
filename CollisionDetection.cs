using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
   
   // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    ////当たり判定確認用
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + "Enter");
    }
    
}
