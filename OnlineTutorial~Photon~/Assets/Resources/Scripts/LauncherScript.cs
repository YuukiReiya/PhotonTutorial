using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LauncherScript : Photon.PunBehaviour
{
    #region const value
    const string NextScene = "battle";
    #endregion

    #region public value

    #endregion

    #region private value
    //  ゲームのバージョン。
    //  仕様が異なるバージョンとなったときはバージョンを変更しないとエラーが発生
    string gameVersion = "test";

    #endregion

    #region Photon Callback
    //  Auto-JoinLobbyにチェックを入れているとPhotonに接続後OnJoinLobby()が呼ばれる。
    public override void OnJoinedLobby ()
    {
        Debug.Log ("Lobby in");
        //  Randomで部屋を選び、部屋に入る（部屋が無ければOnPhotonRandomJoinFailedが呼ばれる）
        PhotonNetwork.JoinRandomRoom ();
    }

    //  JoinRandomRoomが失敗したときに呼ばれる
    public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
    {
        Debug.Log ("Failed to Lobby in");
        //  TestRoomという名前の部屋を作成して、部屋に入る
        PhotonNetwork.CreateRoom ("TestRoom");
    }

    //  部屋に入った時に呼ばれる
    public override void OnJoinedRoom ()
    {
        Debug.Log ("Room in");
        //  遷移先シーンをロード
        PhotonNetwork.LoadLevel (NextScene);
    }
    #endregion

    #region public methods
    //  ログインボタンを押したときに実行
    public void Connect ()
    {
        //  Photonに接続されていれば処理しない
        if (PhotonNetwork.connected) { return; }

        //  Photonに接続
        PhotonNetwork.ConnectUsingSettings (gameVersion);
        Debug.Log ("Photon connected!");
    }
    #endregion
}