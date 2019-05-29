using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    #region public properties
    //  キャラの頭上に乗るように調整するためのオフセット
    public Vector3 screenOffset = new Vector3 (0f, 30f, 0f);

    //  プレイヤー名前設定用のテキスト
    public Text playerNameText;

    //  プレイヤーのHP用スライダー
    public Slider playerHPSlider;

    //  プレイヤーのチャット用テキスト
    public Text playerChatText;

    #endregion

    #region private properties
    //追従するキャラのPlayerManager情報
    PlayerManager target;

    float characterControllerHeight;
    Transform targetTransform;
    Vector3 targetPosition;
    #endregion

    #region MonoBehaviour Callback
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake ()
    {
        //このオブジェクトはCanvasオブジェクトの子オブジェクトとして生成
        this.GetComponent<Transform> ().SetParent (GameObject.Find ("Canvas").GetComponent<Transform> ());
    }
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        //  名前を表示するプレイヤーがいなくなれば、このオブジェクトも破棄
        if (target == null)
        {
            Destroy (this.gameObject);
            return;
        }

        //  現在のHPをSliderに適用
        if (playerHPSlider != null)
        {
            playerHPSlider.value = target.HP;
        }

        //  頭上にチャットを表示
        if (playerChatText != null)
        {
            playerChatText.text = target.ChatText;
        }
    }

    private void LateUpdate ()
    {
        //targetのオブジェクトを追跡する
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeight;
            //  targetの座標から頭上UIの画面上の二次元座標を計算して移動させる
            this.transform.position = Camera.main.WorldToScreenPoint (targetPosition) + screenOffset;
        }
    }
    #endregion

    #region public methods
    public void SetTarget (PlayerManager target)
    {
        if (target == null)
        {
            Debug.LogError ("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }

        //  Targetの情報をこのスクリプト内で使うのでコピー
        this.target = target;
        targetTransform = target.GetComponent<Transform> ();

        CharacterController characterController = target.GetComponent<CharacterController> ();

        if (characterController != null)
        {
            characterControllerHeight = characterController.height;
        }

        if (playerNameText != null)
        {
            playerNameText.text = target.photonView.owner.NickName;
        }

        if (playerHPSlider != null)
        {
            playerHPSlider.value = target.HP;
        }

        if (playerChatText != null)
        {
            playerChatText.text = target.ChatText;
        }
    }
    #endregion
}