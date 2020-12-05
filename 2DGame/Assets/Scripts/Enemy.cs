using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10.5f;

    [Header("子彈"), Tooltip("射出的子彈")]
    public GameObject bullet;
    [Header("子彈生成點"), Tooltip("生成子彈的位置")]
    public Transform point;
    [Header("子彈速度"), Range(0, 5000)]
    public float BulletSpeed = 800;
    [Header("開槍音效"), Tooltip("射擊的音效")]
    public AudioClip SoundFIre;
    [Header("攻擊間隔"), Range(0, 5)]
    public float intervalAttack = 2.5f;

    [Header("追蹤範圍")]
    public float rangeTrack;
    [Header("攻擊範圍")]
    public float rangeAttack;

    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;
    public Transform player;
    private float timer;

    private void Update()
    {
        Move();
    }

    private void Start()
    {
        player = GameObject.Find("玩家").transform;


        aud = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        OnDrawGizmos();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="子彈")
        {
            Dead();
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        // 面向玩家：如果玩家的 X 大於 敵人的 X 角度 0，否則 角度 180
        if (player.position.x > transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // 距離 = 三維 的 距離(A點，B點)
        float dis = Vector3.Distance(player.position, transform.position);

        // 如果 距離 < 攻擊：攻擊
        if (dis < rangeAttack)
        {
            Fire();
        }
        // 否則 距離 < 追蹤：追蹤
        else if (dis < rangeTrack)
        {
            rig.velocity = transform.right * speed;
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y);
        }
    }

    /// <summary>
    /// 開槍
    /// </summary>
    private void Fire()
    {
        rig.velocity = new Vector2(0, rig.velocity.y);                                                      // 加速度 = X 0，Y 原本的 Y

        // 如果 計時器 大於等於 間隔 就攻擊
        if (timer >= intervalAttack)
        {
            timer = 0;
            aud.PlayOneShot(SoundFIre, Random.Range(0.3f, 0.5f));                                               // 播放音效
            GameObject temp = Instantiate(bullet, point.position, point.rotation);                              // 生成子彈
            temp.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed + transform.up * 100);      // 子彈賦予推力
        }
        else
        {
            timer += Time.deltaTime;            // 累加時間
        }
        GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed + transform.up * 100);      // 子彈賦予推力
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        enabled = false;
        ani.SetBool("死亡開關", true);
        GetComponent<CapsuleCollider2D>().enabled = false;      // 關閉碰撞器
        rig.Sleep();                                            // 剛體 睡著
        Destroy(gameObject, 2.5f);
    }

    /// <summary>
    /// 畫圈
    /// </summary>
    private void OnDrawGizmos()
    {
        // 圖示 顏色
        Gizmos.color = new Color(0.5f, 0.5f, 0, 0.5f);
        // 圖示 繪製球體(中心點，半徑)
        Gizmos.DrawSphere(new Vector2(transform.position.x, transform.position.y), rangeAttack);

        Gizmos.color = new Color(0, 0.5f, 0.5f, 0.5f);
        // 圖示 繪製球體(中心點，半徑)
        Gizmos.DrawSphere(new Vector2(transform.position.x, transform.position.y), rangeTrack);
    }
}
