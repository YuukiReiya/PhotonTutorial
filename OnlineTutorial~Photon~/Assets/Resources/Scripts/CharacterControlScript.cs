using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlScript : MonoBehaviour
{
    #region public value
    public Animator animator;
    public CharacterController characterController;
    public float speed;
    public float jumpSpeed;
    public float rotateSpeed;
    public float gravity;
    #endregion

    #region private value
    Vector3 targetDirection;
    Vector3 moveDirection = Vector3.zero;
    #endregion

    #region MonoBehaviour Callback
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        //  移動関数
        moveControl ();

        //  旋回関数
        RotationControl ();

        //  最終的な移動処理
        characterController.Move (moveDirection * Time.deltaTime);
    }
    #endregion

    #region public methods

    #endregion

    #region private methods
    void moveControl ()
    {
        //★進行方向計算
        //  キーボード入力取得
        float v = Input.GetAxisRaw ("Vertical");
        float h = Input.GetAxisRaw ("Horizontal");

        //  カメラの方向ベクトルからY成分を除き、正規化してキャラが走る方向を取得
        Vector3 forward = Vector3.Scale (
            Camera.main.transform.forward,
            new Vector3 (1, 0, 1)
        ).normalized;

        //  カメラの右方向を取得
        Vector3 right = Camera.main.transform.right;

        //  カメラの方向を考慮したキャラの進行方向を計算
        targetDirection = h * right + v * forward;

        //★地上にいる場合の処理
        if (characterController.isGrounded)
        {
            //  移動ベクトル計算
            moveDirection = targetDirection * speed;

            //  ジャンプ
            if (Input.GetButtonDown ("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        //★空中の場合の処理
        else
        {
            float tempy = moveDirection.y;
            //(↓の２文の処理があると空中でも入力方向に動けるようになる)
            //moveDirection = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)).normalized;
            //moveDirection *= speed;
            moveDirection.y = tempy - gravity * Time.deltaTime;
        }

        //★走行アニメーション管理
        //  移動入力があれば
        if (v >.1 || v < -.1 || h >.1 || h < -.1)
        {
            //  走行アニメーションON
            animator.SetFloat ("Speed", 1f);
        }
        //  移動入力がないと
        else
        {
            //  走行アニメーションOFF
            animator.SetFloat ("Speed", 0f);
        }
    }

    void RotationControl ()
    {
        Vector3 rotateDirection = moveDirection;
        rotateDirection.y = 0;

        //  それなりに移動方向が変化する場合のみ移動方向を変える
        if (rotateDirection.sqrMagnitude > 0.01)
        {
            //  緩やかに移動方向を変える
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.Slerp (transform.forward, rotateDirection, step);
            transform.rotation = Quaternion.LookRotation (newDir);
        }
    }
    #endregion
}