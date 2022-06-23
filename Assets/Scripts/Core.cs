using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Photon.Pun;

public class Core : MonoBehaviourPunCallbacks
{
    #region [ OBJECTS ]

    public static ErrorCodes ErrorCodes = new ErrorCodes();

    #endregion

    #region [ PROPERTIES ]

    [HideInInspector] public List<char> alphaNumUnderscore = new List<char>
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        '_', '-'
    };

    #endregion

    #region [ DELEGATES ]



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

    #region [ GAME STATE CONTROL ]



    #endregion

    #region [ MATHEMATICS & BOOLEAN LOGIC ]

    // I just added the ToRad and ToDeg functions for the sake of
    // my personal convenience.
    public static float ToRad(float degrees)
    {
        return degrees * Mathf.PI / 180.0f;
    }

    public static float ToDeg(float radians)
    {
        return radians * 180.0f / Mathf.PI;
    }

    public static int ToInt(bool intBool)
    {
        if (intBool)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public static int ToInt(bool intBool, int trueVal, int falseVal)
    {
        if (intBool)
        {
            return trueVal;
        }
        else
        {
            return falseVal;
        }
    }

    public static bool ToBool(int boolInt)
    {
        if (boolInt > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Vector3 RestrictRotVector(Vector3 rotVect)
    {
        if (rotVect.x > 180.0f)
        {
            rotVect.x -= 360.0f;
        }
        else if (rotVect.x < -180.0f)
        {
            rotVect.x += 360.0f;
        }

        if (rotVect.y > 180.0f)
        {
            rotVect.y -= 360.0f;
        }
        else if (rotVect.y < -180.0f)
        {
            rotVect.y += 360.0f;
        }

        if (rotVect.z > 180.0f)
        {
            rotVect.z -= 360.0f;
        }
        else if (rotVect.z < -180.0f)
        {
            rotVect.z += 360.0f;
        }

        return rotVect;
    }

    public static float WrapClamp(float value, float min, float max)
    {
        float range = max - min;
        if (value < min)
        {
            float diff = min - value;
            int mult = (int)((diff - (diff % range)) / range) + 1;
            return value + (float)mult * range;
        }
        else if (value > max)
        {
            float diff = value - max;
            int mult = (int)((diff - (diff % range)) / range) + 1;
            return value - (float)mult * range;
        }
        else
        {
            return value;
        }
    }

    // This is just to make it easier to generate random integers
    public static int RandomInt(int valMin, int valMax)
    {
        float r = Random.Range(valMin, valMax + 1);
        int i = Mathf.FloorToInt(r);
        if (i > valMax)
        {
            i = valMax;
        }
        return i;
    }

    public static string[] StopwatchTime(float time)
    {
        int seconds = (int)Mathf.FloorToInt(time);
        int subSeconds = (int)Mathf.Floor((time - seconds) * 100.0f);

        int tMinutes = seconds - seconds % 60;
        int tSeconds = seconds % 60;

        string strMinutes = tMinutes.ToString();
        string strSeconds = tSeconds.ToString();
        string strSubSecs = subSeconds.ToString();

        if (strSeconds.Length < 2)
        {
            strSeconds = "0" + strSeconds;
        }
        if (strSubSecs.Length < 2)
        {
            strSubSecs = "0" + strSubSecs;
        }

        return new string[] { strMinutes, strSeconds, strSubSecs };
    }

    #endregion

    #region [ DATA STRUCTURE HANDLING ]

    public static bool InBounds<T>(int index, T[] array)
    {
        if (index > -1 && index < array.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool InBounds<T>(int index, List<T> list)
    {
        if (index > -1 && index < list.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ClearArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = default(T);
        }
    }

    public static List<T> ArrayToList<T>(T[] array)
    {
        List<T> listOut = new List<T>();
        foreach (T item in array)
        {
            listOut.Add(item);
        }
        return listOut;
    }

    public static void CopyListData<T>(List<T> source, List<T> destination)
    {
        for (int i = 0; i < source.Count; i++)
        {
            destination.Add(source[i]);
        }
    }

    #endregion

    #region [ OBJECT HANDLING ]

    // I makes lists of children with a specific component often enough that
    // this was worth creating as a static function.
    public static List<GameObject> GetChildrenWithComponent<T>(GameObject parentObj)
    {
        List<GameObject> childrenWithComponent = new List<GameObject>();
        if (parentObj.transform.childCount > 0)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject;
                T childComponent;
                if (child.TryGetComponent<T>(out childComponent))
                {
                    childrenWithComponent.Add(child);
                }
            }
        }

        return childrenWithComponent;
    }

    // Pretty much the same deal here as with the "children with component" function.
    public static List<GameObject> GetChildrenWithTag(GameObject parentObj, string tag)
    {
        List<GameObject> childrenWithTag = new List<GameObject>();
        if (parentObj.transform.childCount > 0)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject;
                if (child.CompareTag(tag))
                {
                    childrenWithTag.Add(child);
                }
            }
        }

        return childrenWithTag;
    }

    // I makes lists of children with a specific component often enough that
    // this was worth creating as a static function.
    public static List<GameObject> GetListItemsWithComponent<T>(List<GameObject> objects)
    {
        List<GameObject> itemsWithComponent = new List<GameObject>();
        if (objects.Count > 0)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                GameObject item = objects[i];
                T itemComponent = item.GetComponent<T>();
                if (!itemComponent.Equals(null))
                {
                    itemsWithComponent.Add(item);
                }
            }
        }

        return itemsWithComponent;
    }

    // Pretty much the same deal here as with the "children with component" function.
    public static List<GameObject> GetListItemsWithTag(List<GameObject> objects, string tag)
    {
        List<GameObject> itemsWithTag = new List<GameObject>();
        if (objects.Count > 0)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                GameObject item = objects[0];
                if (item.CompareTag(tag))
                {
                    itemsWithTag.Add(item);
                }
            }
        }

        return itemsWithTag;
    }

    public static object GetProperty(object obj, string propertyName)
    {
        return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
    }

    public static T GetPropertyValue<T>(object obj, string propertyName)
    {
        return (T)obj.GetType().GetProperty(propertyName).GetValue(obj, null);
    }

    #endregion

    #region [ INPUT HANDLING ]



    #endregion

    #region [ TEXT VALIDATION ]

    public bool IsEmptyOrNullOrWhiteSpace(string text)
    {
        return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
    }

    public bool CheckTextChars(string text)
    {
        return CheckTextChars(alphaNumUnderscore, text);
    }

    public bool CheckTextChars(List<char> validChars, string text)
    {
        bool textValid = true;

        int n = text.Length;

        for (int i = 0; i < n; i++)
        {
            char toCheck = char.Parse(text.Substring(i, 1));
            if (!validChars.Contains(toCheck))
            {
                textValid = false;
                break;
            }
        }

        return textValid;
    }

    #endregion

}
