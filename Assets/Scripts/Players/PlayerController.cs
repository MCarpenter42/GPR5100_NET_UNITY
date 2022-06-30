using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : Core
{
    #region [ PROPERTIES ]

    public GameObject playerCamera;
    [SerializeField] GameObject headPivot;
    [SerializeField] List<MeshRenderer> modelComponents = new List<MeshRenderer>();
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float rotSpeed = 2.0f;

    [HideInInspector] public PhotonView view;

    private Vector3 playerRot = Vector3.zero;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    #region [ BUILT-IN UNITY FUNCTIONS ]

    void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (!view.IsMine)
        {
            playerCamera.GetComponent<Camera>().enabled = false;
            playerCamera.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            foreach (MeshRenderer rndr in modelComponents)
            {
                rndr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
        }
        playerRot = transform.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (view.IsMine)
        {
            Movement();
            Rotation();
        }
    }

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void Movement()
    {
        if (Input.GetKey("w"))
        {
            transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.localPosition -= transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            transform.localPosition -= transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            transform.localPosition += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    private void Rotation()
    {
        playerRot.y += Input.GetAxis("Mouse X") * rotSpeed;
        playerRot.x -= Input.GetAxis("Mouse Y") * rotSpeed;

        Mathf.Clamp(playerRot.x, -90.0f, 90.0f);
        WrapClamp(playerRot.y, 0.0f, 360.0f);

        transform.eulerAngles = new Vector3(0.0f, playerRot.y, 0.0f);
        headPivot.transform.localEulerAngles = new Vector3(playerRot.x, 0.0f, 0.0f);
    }
}
