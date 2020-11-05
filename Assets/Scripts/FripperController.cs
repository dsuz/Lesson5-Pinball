using UnityEngine;

public class FripperController : MonoBehaviour
{
    //HingiJointコンポーネントを入れる
    private HingeJoint myHingeJoint;
    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;
    /// <summary>タッチしている指の ID</summary>
    int m_fingerId;

    // Use this for initialization
    void Start()
    {
        //HinjiJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {

        //左矢印キーを押した時左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右矢印キーを押した時右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }

        //矢印キー離された時フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }

        // タッチ判定
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    if (tag == "RightFripperTag")
                    {
                        m_fingerId = t.fingerId;
                        SetAngle(this.flickAngle);
                    }
                }
                else if (tag == "LeftFripperTag")
                {
                    m_fingerId = t.fingerId;
                    SetAngle(this.flickAngle);
                }
            }
            else if (t.phase == TouchPhase.Ended && t.fingerId == m_fingerId)
            {
                SetAngle(this.defaultAngle);
            }
        }
    }

    //フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}