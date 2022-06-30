using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class Nameplate : Core
{
    #region [ PROPERTIES ]

    [SerializeField] GameObject namePlate;
    public TMP_Text nameTag;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    #region [ BUILT-IN UNITY FUNCTIONS ]

    void Update()
    {
        if (GameManager.ClientPlayer != null)
        {
            transform.LookAt(GetPlayerCamDir());
            transform.eulerAngles = new Vector3(namePlate.transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
        }
    }

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private Vector3 GetPlayerCamDir()
    {
        Vector3 camDir = GameManager.ClientPlayer.playerCamera.transform.position;
        camDir -= namePlate.transform.position;
        camDir = camDir.normalized;
        //Debug.Log(transform.position + ", " + transform.eulerAngles + ", " + camDir);
        return camDir;
    }
}
