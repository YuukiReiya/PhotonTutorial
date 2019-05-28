using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Photon.PunBehaviour, IPunObservable
{
    #region public value
    //  頭上に表示するUIのプレハブ
    public GameObject playerUIPrefab;

    //  現在のHP
    public int HP = 100;

    //  Localのプレイヤーを設定
    public static GameObject LocalPlayerInstance;

    #endregion

    #region private value
    //  頭上のUIオブジェクト
    GameObject UIObject;
    #endregion

    #region MonoBehaviour Callback
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake ()
    {
        if (photonView.isMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
    }
    // Start is called before the first frame update
    void Start ()
    {
        if (playerUIPrefab != null)
        {
            //  Playerの頭上UIの生成とPlayerUIScriptでのSetTarget関数呼び出し
            UIObject = Instantiate (playerUIPrefab) as GameObject;
            UIObject.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning ("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (!photonView.isMine) { return; }

        //  LocalVariablesを参照し、HPを更新
        HP = LocalVariables.currentHP;
    }
    #region OnPhotonSerializeView同期
    public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext (this.HP);
            //stram.SendNext (this.ChatText);
        }
        else
        {
            this.HP = (int) stream.ReceiveNext ();
            //this.ChatText = (string)stream.ReceiveNext();
        }
    }
    #endregion

    #endregion

}