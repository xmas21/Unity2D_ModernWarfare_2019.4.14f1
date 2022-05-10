using UnityEngine;
using UnityEngine.UI;   // 引用 介面 API

/// <summary>
/// 遊戲管理器：管理生命、分數
/// </summary>
public class GameManager : MonoBehaviour
{
    // 陣列
    [Header("生命物件陣列")]
    public GameObject[] lives;
    [Header("分數文字介面")]
    public Text textScore;
    // 一般欄位 重新載入場景 會還原為預設值
    // 靜態欄位 重新載入場景 不會還原為預設值
    public static int live = 3;

    public static int score;

    private void Awake()
    {
        SetCollision();

        SetLive();
        AddScore(0);
    }

    /// <summary>
    /// 添加分數與更新分數介面
    /// </summary>
    /// <param name="add">要添加多少分數</param>
    public void AddScore(int add)
    {
        score += add;                           // 累加分數
        textScore.text = "分數：" + score;       // 更新文字介面
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void PlayerDead()
    {
        live--;

        SetLive();
    }

    /// <summary>
    ///  更新生命介面
    /// </summary>
    private void SetLive()
    {
        // 陣列欄位[編號] 的 方法()
        //lives[0].SetActive(false);

        //for (int i = 1; i < 100; i++)
        //{
        //    print("迴圈：" + i);
        //}

        for (int i = 0; i < lives.Length; i++)
        {
            // 判斷式 只有一行敘述時 可以省略 大括號
            if (i >= live) lives[i].SetActive(false);
        }
    }

    /// <summary>
    /// 設定碰撞：所有圖層的碰撞
    /// </summary>
    private void SetCollision()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("敵人"), LayerMask.NameToLayer("敵人"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("玩家"), LayerMask.NameToLayer("玩家子彈"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("敵人"), LayerMask.NameToLayer("敵人子彈"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("玩家子彈"), LayerMask.NameToLayer("敵人子彈"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("敵人子彈"), LayerMask.NameToLayer("敵人子彈"));
    }
}

