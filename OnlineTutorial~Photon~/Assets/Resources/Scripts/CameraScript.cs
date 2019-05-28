using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region const value
    //  カメラのY方向の最小角度
    private const float YAngle_Min = -89.0f;

    //  カメラのY方向の最大角度
    private const float YAngle_Max = 89.0f;

    //  キャラクターとの最小距離
    private const float distance_min = 1.0f;

    //  キャラクターとの最大距離
    private const float distance_max = 20.0f;
    #endregion

    #region public value
    //  追跡オブジェクトのTransform
    public Transform target;

    //  追跡対象の中心位置調整用のオフセット
    public Vector3 offset;
    #endregion

    #region private value
    //  targetとoffsetによる注視する座標
    private Vector3 lookAt;

    //  キャラクターとカメラ間の距離
    private float distance = 10.0f;

    //  カメラをX軸方向に回転させる角度
    private float currentX = 0.0f;
    //  カメラをY軸方向に回転させる角度
    private float currentY = 0.0f;

    //  マウスドラッグによるカメラX方向回転係数
    private float moveX = 4.0f;

    //  マウスドラッグによるカメラY方向回転係数
    private float moveY = 2.0f;

    //  QEによるカメラX方向回転係数
    private float moveX_QE = 2.0f;
    #endregion

    #region MonoBehavour Callback
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        //  QとEキーでカメラ回転
        if (Input.GetKey (KeyCode.Q))
        {
            currentX += -moveX_QE;
        }
        if (Input.GetKey (KeyCode.E))
        {
            currentX += +moveX_QE;
        }

        //  マウス右クリックを押しているときだけマウスの移動量に応じてカメラが回転
        if (Input.GetMouseButton (1))
        {
            currentX += Input.GetAxis ("Mouse X") * moveX;
            currentY += Input.GetAxis ("Mouse Y") * moveY;
            currentY = Mathf.Clamp (currentY, YAngle_Min, YAngle_Max);
        }
        distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel"), distance_min, distance_max);
    }

    private void LateUpdate ()
    {
        if (target != null)
        {
            lookAt = target.position + offset;

            //  カメラ旋回
            Vector3 dir = new Vector3 (0, 0, -distance);
            Quaternion rotation = Quaternion.Euler (-currentY, currentX, 0);

            transform.position = lookAt + rotation * dir;
            transform.LookAt (lookAt);
        }
    }
    #endregion

}