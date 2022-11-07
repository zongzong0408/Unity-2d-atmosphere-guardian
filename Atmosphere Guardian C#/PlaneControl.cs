using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public GameObject Missile;
    public GameObject Bullet;

    public float fly_speed;

    public GameObject explosion;

    private Rigidbody2D MainRole_Rigidbody;
    private SpriteRenderer MainRole_plane;
    private Color original_color;
    public Animator MainRole_Animator;

    int RorL = 1;
    public float flash_time;
    float machine_gun_fire_wait = 0.0f;
    public int MainRole_blood_count = 500;

    // Start is called before the first frame update
    void Start()
    {
        MainRole_Rigidbody = GetComponent<Rigidbody2D>();
        MainRole_plane = GetComponent<SpriteRenderer>();
        MainRole_Animator = GetComponent<Animator>();
        original_color = MainRole_plane.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFunction.IsPlaying == true)
        {
            Fly();
            Shoot();
        }
    }

    /* 飛機控制 */
    void Fly()
    {
        bool IsFlying = false;
        int turn_pos = 0;

        // using Rigidbody2D to control plane move
        //float move_div = Input.GetAxis("Horizontal");

        //Vector2 MainRole_pos = new Vector2(move_div * fly_speed, MainRole_Rigidbody.velocity.y);
        //MainRole_Rigidbody.velocity = MainRole_pos;

        // 3d coordinate displacement
        if (Input.GetKey(KeyCode.RightArrow))
        {
            IsFlying = true;
            MainRole_Animator.SetInteger("Status", 1);
            gameObject.transform.position += new Vector3(0.06f, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            IsFlying = true;
            MainRole_Animator.SetInteger("Status", 2);
            gameObject.transform.position += new Vector3(-0.06f, 0, 0);
        }
        if (IsFlying == false)
        {
            MainRole_Animator.SetInteger("Status", 0);
        }
        //if (Input.GetKey(KeyCode.UpArrow))        // 主角飛機 UP
        //{
        //    gameObject.transform.position += new Vector3(0, 0.01f, 0);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))      // 主角飛機 DOWN
        //{
        //    gameObject.transform.position += new Vector3(0, -0.01f, 0);
        //}
    }

    /* 武器控制 */
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))                            // 飛彈 Missile
        {
            //int RorL = UnityEngine.Random.Range(0, 5);

            if (RorL == 1)
            {
                Vector3 pos = gameObject.transform.position + new Vector3(0.25f, 0.25f, 0);
                Instantiate(Missile, pos, gameObject.transform.rotation);

                RorL = 2;
            }
            else
            {
                Vector3 pos = gameObject.transform.position + new Vector3(-0.25f, 0.25f, 0);
                Instantiate(Missile, pos, gameObject.transform.rotation);

                RorL = 1;
            }
        }
        if (Input.GetKey(KeyCode.Q) && machine_gun_fire_wait <= 10.0f)  // 機槍 Bullet
        {
            Vector3 pos = gameObject.transform.position + new Vector3(0, 0.6f, 0);
            Instantiate(Bullet, pos, gameObject.transform.rotation);

            machine_gun_fire_wait += 1.0f;
        }

        machine_gun_fire_wait -= 0.1f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy_Bullet")
        {
            Destroy(col.gameObject);

            FlashColor();

            if (MainRole_blood_count > 0)
            {
                MainRole_blood_count -= 1;
            }
            else
            {
                Destroy(gameObject);

                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                GameFunction.Instance.GameOver();
            }
        }
    }

    void FlashColor()
    {
        MainRole_plane.color = Color.red;
        Invoke("ResetColor", flash_time);
    }

    void ResetColor()
    {
        MainRole_plane.color = original_color;
    }
}
