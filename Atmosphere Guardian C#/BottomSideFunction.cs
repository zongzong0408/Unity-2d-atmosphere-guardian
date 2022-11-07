using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSideFunction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)   // 名為 col 的觸發事件(碰撞事件)
    {
        if (col.tag == "Enemy" || col.tag == "Enemy_Bullet")             
        {
            Destroy(col.gameObject);        // 消滅碰撞的物件
        }
    }
}
