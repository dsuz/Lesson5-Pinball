using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    //ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    private GameManager m_gameManager;

    void Start()
    {
        m_gameManager = GameObject.FindObjectOfType<GameManager>();
        if (!m_gameManager)
            Debug.LogWarning("m_gameManager is null for " + gameObject.name + " in " + gameObject.scene.name);
    }

    void Update()
    {
        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ)
        {
            m_gameManager.GameOver();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの種類により異なる点数を加算する
        switch (collision.gameObject.tag)
        {
            case "SmallStarTag":
                AddScore(5);
                break;
            case "LargeStarTag":
                AddScore(10);
                break;
            case "SmallCloudTag":
                AddScore(50);
                break;
            case "LargeCloudTag":
                AddScore(100);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 点数を加算し、表示を更新する。
    /// </summary>
    /// <param name="score">加算する点数</param>
    private void AddScore(int score)
    {
        m_gameManager.AddScore(score);
    }
}