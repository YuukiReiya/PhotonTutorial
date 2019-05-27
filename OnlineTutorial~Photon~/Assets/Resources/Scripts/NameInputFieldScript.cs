using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NameInputFieldScript : MonoBehaviour
{
    #region private value
    static string playerNamePrefKey = "PlayerName";
    #endregion

    #region Unity MonoBehaviour Callback
    // Start is called before the first frame update
    void Start ()
    {
        string defaultName = "";
        InputField inputField = this.GetComponent<InputField> ();

        //  前回プレイ開始時に入力した名前をロードして表示
        if (!inputField) { Debug.LogWarning ("prev login data is not get!"); }

        if (PlayerPrefs.HasKey (playerNamePrefKey))
        {
            defaultName = PlayerPrefs.GetString (playerNamePrefKey);
            inputField.text = defaultName;
        }
    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion

    #region public method
    public void SetPlayerName (string value)
    {
        //  利用するプレイヤーの名前を設定
        PhotonNetwork.playerName = value + " ";

        //  名前を保存
        PlayerPrefs.SetString (playerNamePrefKey, value);

        Debug.Log ("SetPlayerName : " + PhotonNetwork.player.NickName);
    }
    #endregion
}