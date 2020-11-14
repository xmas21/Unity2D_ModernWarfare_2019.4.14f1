using UnityEngine;

public class Player : MonoBehaviour
{
    #region
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("跳要高度"), Range(0, 3000)]
    public float jump = 100f;
    [Header("是否在地板上")]
    public bool IsGround = false;
    [Header("子彈"),Tooltip("射出的子彈")]
    public GameObject bullet;
    [Header("子彈生成點"), Tooltip("生成子彈的位置")]
    public Transform point;
    [Header("子彈速度"), Range(0, 5000)]
    public float BulletSpeed = 800;
    [Header("開槍音效"), Tooltip("射擊的音效")]
    public AudioClip SoundFIre;
    [Header("生命數量"), Range(0,10)]
    public int HpNumber = 3;

    private int score = 0;
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;
    #endregion

    private void Start()
    {
        Drive();
    }

    private void Update()
    {
        
    }

    private void Drive()
    {

    }


}
