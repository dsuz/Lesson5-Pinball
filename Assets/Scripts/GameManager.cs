using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームを管理する。ボールの spawn、対話型インターフェイスの処理、スコアの更新などの機能を持つ。
/// 適当な GameObject に追加して使う。ゲーム内に一つしか存在してはいけない。
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary></summary>
    [SerializeField] Text m_messageText;

    /// <summary></summary>
    [SerializeField] Button m_startButton;

    /// <summary></summary>
    [SerializeField] GameObject m_spawnPoint;

    /// <summary></summary>
    [SerializeField] GameObject m_ball;

    /// <summary></summary>
    [SerializeField] Text m_scoreText;

    int m_score;

    /// <summary>
    /// スタートボタンを表示する
    /// </summary>
    /// <param name="caption">ボタンに表示するテキスト</param>
    void ShowButton(string caption)
    {
        if (m_startButton)
        {
            Text captionText = m_startButton.GetComponentInChildren<Text>();
            if (captionText)
                captionText.text = caption;
            m_startButton.gameObject.SetActive(true);
        }
        else
            Debug.LogWarning("m_startButton is null for " + gameObject.name + " in " + gameObject.scene.name);
    }

    void HideButton()
    {
        if (m_startButton)
            m_startButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// メッセージを表示する
    /// </summary>
    /// <param name="message">表示するメッセージ</param>
    void ShowMessage(string message)
    {
        if (m_messageText)
        {
            m_messageText.text = message;
            m_messageText.gameObject.SetActive(true);
        }
        else
            Debug.LogWarning("m_messageText is null for " + gameObject.name + " in " + gameObject.scene.name);
    }

    void HideMessage()
    {
        if (m_messageText)
            m_messageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ゲームオーバーの時に呼ぶ
    /// </summary>
    public void GameOver()
    {
        ShowMessage("Game Over");
        ShowButton("Restart");
    }

    /// <summary>
    /// ゲーム開始時に呼ぶ
    /// </summary>
    public void StartGame()
    {
        ShowMessage("Game Start");
        ShowButton("Start");
    }

    /// <summary>
    /// ボールを出現させる
    /// </summary>
    public void SpawnBall()
    {
        Instantiate(m_ball, m_spawnPoint.transform.position, Quaternion.identity);
        HideMessage();
        HideButton();
        ResetScore();
    }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    /// <param name="scoreToBeAdded">加算するスコア</param>
    public void AddScore(int scoreToBeAdded)
    {
        m_score += scoreToBeAdded;
        UpdateScore(m_score);
    }

    /// <summary>
    /// スコアを 0 に戻す
    /// </summary>
    void ResetScore()
    {
        m_score = 0;
        UpdateScore(m_score);
    }

    /// <summary>
    /// スコアの表示を更新する
    /// </summary>
    /// <param name="score">更新する点数</param>
    void UpdateScore(int score)
    {
        if (m_scoreText)
            m_scoreText.text = "Score: " + score;
    }

    void Start ()
    {
        // 複数の GameManager が存在しないかチェックし、複数あった場合は警告をログする
        GameManager[] gameManagerList = GameObject.FindObjectsOfType<GameManager>();
        if (gameManagerList.Length > 1)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.AppendLine("GameManager should be unique. There's more than one GameManager in " + gameObject.scene.name);
            foreach (var gameManager in gameManagerList)
                builder.AppendLine("Object name: " + gameManager.gameObject.name);
            Debug.LogWarning(builder.ToString());
        }
        else
            StartGame();    // 一つしかなかったら正常なのでゲームを開始する。
	}
}
