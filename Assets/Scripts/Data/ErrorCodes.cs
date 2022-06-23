using System;
using System.Collections;
using System.Collections.Generic;

public class ErrorCodes
{
    #region [ PROPERTIES ]

    public enum Codes { UNKNOWN = 0, CantCreateRoom = 010100, CantJoinRoom = 010200, CantJoinRandom = 010300 };

    public Dictionary<short, string> Errors = new Dictionary<short, string>()
    {
        { 0, "UNKNOWN" },
        { 010100, "Unable to create room" },
        { 010200, "Unable to join room" },
        { 010300, "Unable to join random room" },
    };
	
	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

	#region [ BUILT-IN UNITY FUNCTIONS ]

    void Awake()
    {
        
    }

    void Start()
    {
        
    }
	
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    public bool ValidErrorCode(short errorCode)
    {
        if (Errors.ContainsKey(errorCode))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string GetErrorText(short errorCode)
    {
        if (ValidErrorCode(errorCode))
        {
            string text;
            Errors.TryGetValue(errorCode, out text);
            return text;
        }
        else
        {
            return "INVALID ERROR CODE";
        }
    }
}
