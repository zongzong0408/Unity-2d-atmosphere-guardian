using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Bullet;

    private SpriteRenderer enemy_plane;
    private Color original_color;

    public float flash_time;
    float shoot_wait = 0.0f;
    public int Enemy_blood_count = 50;

    // Start is called before the first frame update
    void Start()
    {
        enemy_plane = GetComponent<SpriteRenderer>();
        original_color = enemy_plane.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameFunction.IsPlaying == true)
        {
            shoot_wait += Time.deltaTime;

            Fly();

            if (shoot_wait <= 5.0f)
            {
                Invoke("Shoot", 0.5f);
                shoot_wait += 0.25f;
            }
            shoot_wait -= 0.1f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ally" || col.tag == "Ally_Bullet" || col.tag == "Ally_Missile")
        {
            if (col.tag == "Ally")
            {
                Destroy(col.gameObject);    // 消滅碰撞的物件
                Destroy(gameObject);        // 消滅物件本身

                Instantiate(explosion, transform.position, transform.rotation);

                // 生成爆炸，在碰撞物件的位置產生爆炸，也就是在太空船的位置產生爆炸
                Instantiate(explosion, col.gameObject.transform.position, col.gameObject.transform.rotation);
                GameFunction.Instance.GameOver();

                GameFunction.Instance.AddScore();       // 呼叫 GameFunction.cs 底下的 AddScore()
            }
            else
            {
                if (Enemy_blood_count <= 0)
                {
                    Destroy(col.gameObject);
                    Destroy(gameObject);

                    Instantiate(explosion, transform.position, transform.rotation);

                    GameFunction.Instance.AddScore();
                }
                else
                {
                    if (col.tag == "Ally_Bullet")
                    {
                        Destroy(col.gameObject);
                        FlashColor();

                        Enemy_blood_count -= 5;
                    }
                    else
                    {
                        Destroy(col.gameObject);
                        FlashColor();

                        Enemy_blood_count -= 100;
                    }
                }
            }
        }
    }

    void Fly()
    {
        gameObject.transform.position += new Vector3(0, -0.08f, 0);
    }

    void Shoot()
    {
        Vector3 pos = gameObject.transform.position + new Vector3(0, -0.6f, 0);
        Instantiate(Bullet, pos, gameObject.transform.rotation);
    }

    void FlashColor()
    {
        enemy_plane.color = Color.red;
        Invoke("ResetColor", flash_time);
    }

    void ResetColor()
    {
        enemy_plane.color = original_color;
    }
}
