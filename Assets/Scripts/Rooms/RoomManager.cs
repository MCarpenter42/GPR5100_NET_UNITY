using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : Core
{
    #region [ PROPERTIES ]

    [Header("Frames")]
    [SerializeField] GameObject frameMain;
    [SerializeField] GameObject frameNewRoom;
    [SerializeField] GameObject frameJoinRoom;
    private int visFrame = 0;

    [Header("Text Inputs")]
    [SerializeField] TMP_InputField roomNew_Nickname;
    [SerializeField] TMP_InputField roomNew_Name;
    [SerializeField] TMP_InputField roomNew_MaxPlayers;
    [SerializeField] TMP_InputField roomNew_Password;
    [SerializeField] TMP_InputField roomJoin_Nickname;
    [SerializeField] TMP_InputField roomJoin_Name;
    [SerializeField] TMP_InputField roomJoin_Password;

    private byte maxPlayersLimit = 20;
    private byte maxPlayers;

    private string nickName;
    private bool noPassword = false;
	
	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

	#region [ BUILT-IN UNITY FUNCTIONS ]

    void Awake()
    {
        
    }

    void Start()
    {
        ChangeVisFrame(0);
    }
	
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void ChangeVisFrame(int frame)
    {
        frameMain.SetActive(false);
        frameNewRoom.SetActive(false);
        frameJoinRoom.SetActive(false);

        switch (frame)
        {
            case 1:
                frameNewRoom.SetActive(true);
                break;

            case 2:
                frameJoinRoom.SetActive(true);
                break;

            default:
                frameMain.SetActive(true);
                break;
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    public void CreateRoom()
    {
        bool canCreateRoom = true;
        string roomName = "";

        if (!CheckValidNickname(roomNew_Nickname.text))
        {
            canCreateRoom = false;
        }
        else
        {
            nickName = roomNew_Nickname.text;
        }
        if (!CheckValidRoomNameNew(roomNew_Name.text))
        {
            canCreateRoom = false;
        }
        else
        {
            roomName = roomNew_Name.text;
        }
        if (!CheckValidMaxPlayers(roomNew_MaxPlayers.text))
        {
            canCreateRoom = false;
        }
        if (!CheckValidRoomPassword(roomNew_Password.text))
        {
            canCreateRoom = false;
        }

        if (canCreateRoom)
        {
            PhotonNetwork.NickName = nickName;

            RoomOptions rOpt = new RoomOptions();
            rOpt.IsOpen = true;
            rOpt.IsVisible = true;
            rOpt.MaxPlayers = maxPlayers;

            PhotonNetwork.CreateRoom(roomName, rOpt);
        }
        else
        {
            short errorCode = (short)ErrorCodes.Codes.CantCreateRoom;
            string errorText = ErrorCodes.GetErrorText(errorCode);
            OnJoinRoomFailed(errorCode, errorText);
        }
    }

    private bool CheckValidNickname(string nickName)
    {
        bool isValid = true;

        if (IsEmptyOrNullOrWhiteSpace(nickName))
        {
            isValid = false;
        }
        if (!CheckTextChars(nickName))
        {
            isValid = false;
        }

        return isValid;
    }
    
    private bool CheckValidRoomNameNew(string roomName)
    {
        bool isValid = true;

        if (IsEmptyOrNullOrWhiteSpace(roomName))
        {
            isValid = false;
        }
        if (!CheckTextChars(roomName))
        {
            isValid = false;
        }

        return isValid;
    }

    private bool CheckValidMaxPlayers(string maxPlayersString)
    {
        bool isValid = true;

        if (byte.TryParse(maxPlayersString, out maxPlayers))
        {
            if (maxPlayers > maxPlayersLimit)
            {
                maxPlayers = maxPlayersLimit;
            }
            else if (maxPlayers < 2)
            {
                maxPlayers = 2;
            }
        }
        else
        {
            isValid = false;
        }

        return isValid;
    }

    private bool CheckValidRoomPassword(string password)
    {
        bool isValid = true;

        if (IsEmptyOrNullOrWhiteSpace(password))
        {
            noPassword = true;
        }
        else if (!CheckTextChars(password))
        {
            isValid = false;
        }

        return isValid;
    }

    private bool CheckValidRoomName(string roomName)
    {
        bool isValid = true;

        if (IsEmptyOrNullOrWhiteSpace(roomName))
        {
            isValid = false;
        }
        if (!CheckTextChars(roomName))
        {
            isValid = false;
        }

        return isValid;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    public void JoinExistingRoom()
    {
        bool canJoinRoom = true;
        string roomName = "";

        if (!CheckValidNickname(roomJoin_Nickname.text))
        {
            nickName = roomJoin_Nickname.text;
            canJoinRoom = false;
        }
        else
        {
            nickName = roomJoin_Nickname.text;
        }
        if (!CheckValidRoomName(roomJoin_Name.text))
        {
            canJoinRoom = false;
        }
        else
        {
            roomName = roomJoin_Name.text;
        }

        if (canJoinRoom)
        {
            PhotonNetwork.NickName = nickName;

            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            short errorCode = (short)ErrorCodes.Codes.CantJoinRoom;
            string errorText = ErrorCodes.GetErrorText(errorCode);
            OnJoinRoomFailed(errorCode, errorText);
        }
    }

    public void JoinRandomRoom()
    {
        bool canJoinRoom = true;

        if (!CheckValidNickname(roomJoin_Nickname.text))
        {
            nickName = roomJoin_Nickname.text;
            canJoinRoom = false;
        }
        else
        {
            nickName = roomJoin_Nickname.text;
        }

        if (canJoinRoom)
        {
            PhotonNetwork.NickName = nickName;

            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            short errorCode = (short)ErrorCodes.Codes.CantJoinRandom;
            string errorText = ErrorCodes.GetErrorText(errorCode);
            OnJoinRoomFailed(errorCode, errorText);
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            PhotonNetwork.LoadLevel("WaitingRoom");
        }
        else
        {
            PhotonNetwork.LoadLevel("MainGame");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + ": " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + ": " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + ": " + message);

        RoomOptions rOpt = new RoomOptions();
        rOpt.IsOpen = true;
        rOpt.IsVisible = true;
        rOpt.MaxPlayers = 10;
        string roomName = "Room" + RandomInt(1, 10000);

        PhotonNetwork.CreateRoom(roomName, rOpt);
    }
}
