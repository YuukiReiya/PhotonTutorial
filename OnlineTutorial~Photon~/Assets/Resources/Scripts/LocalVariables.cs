using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalVariables : MonoBehaviour
{
    #region static value
    static public int currentHP = 100;
    #endregion

    #region MonoBehaviour Callback
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
    #endregion

    #region static methods
    static public void VariableReset ()
    {
        currentHP = 100;
    }
    #endregion
}