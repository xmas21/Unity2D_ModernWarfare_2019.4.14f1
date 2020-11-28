using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("跳要高度"), Range(0, 3000)]
    public float jump = 100f;
    [Header("是否在地板上")]
    public bool IsGround = false;
    [Header("子彈"), Tooltip("射出的子彈")]
    public GameObject bullet;
    [Header("子彈生成點"), Tooltip("生成子彈的位置")]
    public Transform point;
    [Header("子彈速度"), Range(0, 5000)]
    public float BulletSpeed = 800;
    [Header("開槍音效"), Tooltip("射擊的音效")]
    public AudioClip SoundFIre;
    [Header("生命數量"), Range(0, 10)]
    public int HpNumber = 3;
    [Header("檢查地面位移")]
    public Vector2 offset;
    [Header("檢查地面半徑")]
    public float radius = 0.3f;



    private int score = 0;
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;
    #endregion

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();
        Fire();
        Jump();
    }


    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rig.velocity = new Vector2(h * speed, rig.velocity.y);
        ani.SetBool("跑步開關", h != 0);

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        // 如果 角色在地面上 並且 按下空白鍵 才能跳躍
        if (IsGround && Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(transform.up * jump);
        }
        // 如果 物理 圓形範圍 碰到 圖層 8 的地板物件
        else if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + offset, radius, 1 << 8))
        {
            IsGround = true;
        }
        // 沒有碰到地板物件
        else
        {
            IsGround = false;                     // 不在地面上了
        }
    }

    /// <summary>
    /// 開槍
    /// </summary>
    private void Fire()
    {
        // 按下左鍵之後
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 生成 子彈在槍口
            GameObject temp = Instantiate(bullet, point.position, point.rotation);
            temp.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed + transform.up * 80);
            aud.PlayOneShot(SoundFIre, 1f);
        }
        // 讓子彈飛
    }

    private void Replay()
    {
        SceneManager.LoadScene("關卡 1");
    }

    /// <summary>
    /// 死亡 
    /// </summary>
    /// <param name="obj">碰到的物件</param>
    private void Dead(string obj)
    {
        if (obj == "死亡區域")
        {
            enabled = false;
            ani.SetBool("死亡開關", true);
            Invoke("Replay", 3f);
        }

        // 延遲呼叫("方法名稱"，延遲時間)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dead(collision.gameObject.name);
    }

    private void OnDrawGizmos()
    {
        // 圖示 顏色
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        // 圖示 繪製球體(中心點，半徑)
        Gizmos.DrawSphere(new Vector2(transform.position.x, transform.position.y) + offset, radius);
    }
    
}
