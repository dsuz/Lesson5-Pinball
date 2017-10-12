using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タッチ入力を検出して左右のフリッパーを動かす機能を提供する。
/// 適当な GameObject に追加して使う。
/// </summary>
public class TouchManager : MonoBehaviour
{
    [SerializeField] FripperController m_leftFripper;
    [SerializeField] FripperController m_rightFripper;

    void Update()
    {
        // 左側にタッチを検出したら左フリッパーを上げる。左側にタッチを検出しなければ左フリッパーを下げる。右も同様。
        bool isLeftTouched = false, isRightTouched = false;
        if (DetectTouches(ref isLeftTouched, ref isRightTouched))
            ControlFrippers(isLeftTouched, isRightTouched);
	}

    /// <summary>
    /// 画面の右半分/左半分がタッチされているかどうかを検出する。
    /// </summary>
    /// <param name="isLeftTouched">画面の左半分がタッチされているかを返す</param>
    /// <param name="isRightTouched">画面の右半分がタッチされているかを返す</param>
    /// <returns>タッチが検出されない場合 false。一つでも検出されたら true</returns>
    bool DetectTouches(ref bool isLeftTouched, ref bool isRightTouched)
    {
        Touch[] touches = Input.touches;
        if (touches.Length < 1)
            return false;

        foreach (var touch in touches)
        {
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touch.position.x > Screen.currentResolution.width / 2)
                    isRightTouched = true;
                else
                    isLeftTouched = true;
            }
        }
        return true;
    }

    /// <summary>
    /// 左右のフリッパーを操作する。
    /// </summary>
    /// <param name="isLeftFlick">true ならば左フリッパーを上げる。false なら下げる</param>
    /// <param name="isRightFlick">true ならば右フリッパーを上げる。false なら下げる</param>
    void ControlFrippers(bool isLeftFlick, bool isRightFlick)
    {
        if (m_leftFripper)
        {
            if (isLeftFlick)
                m_leftFripper.Flick();
            else
                m_leftFripper.Release();
        }
        else
            Debug.LogWarning("m_leftFripper is null for " + gameObject.name + " in scene " + gameObject.scene.name);

        if (m_rightFripper)
        {
            if (isRightFlick)
                m_rightFripper.Flick();
            else
                m_rightFripper.Release();
        }
        else
            Debug.LogWarning("m_rightFripper is null for " + gameObject.name + " in scene " + gameObject.scene.name);
    }
}
