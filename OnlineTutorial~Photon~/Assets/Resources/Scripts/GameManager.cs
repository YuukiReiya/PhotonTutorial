using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region public value
    //  誰かがログインする度に生成するPrefab
    public GameObject playerPrefab;
    #endregion
    #region 
    #endregion
    #region Monobehaviour Callback
    // Start is called before the first frame update
    void Start ()
    {
        //  Photonに接続されていなければ
        if (!PhotonNetwork.connected)
        {
            SceneManager.LoadScene ("Launcher");
            return;
        }

        //  prefab生成
        GameObject player = PhotonNetwork.Instantiate (
            "Prefab/"+this.playerPrefab.name,
             new Vector3 (0f, 0f, 0f),
              Quaternion.identity, 
              0
              );
    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion
}