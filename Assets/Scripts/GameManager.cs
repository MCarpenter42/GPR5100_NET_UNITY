using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : Core
{
    #region [ OBJECTS ]

    private static GameManager instance = null;
    private Controls controlsInstance;
    //private VideoSettings vidSettingsInstance;

    //public static Player Player;
    //public static LevelController LevelController;

    //public static UIController UIController;
    //public static PauseMenu PauseMenu;

    DefaultPool DefaultPool;

    #endregion

    #region [ PROPERTIES ]

    public static bool isPaused = false;

    public static bool firstLoad = true;

    public static float FPS;
    private List<float> frameTimes = new List<float>();

    [SerializeField] List<GameObject> prefabsList = new List<GameObject>();

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    #region [ SINGLETON CONTROL ]

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameManager inst = FindObjectOfType<GameManager>();
                if (inst == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();

                    instance.Init();

                    // Prevents game manager from being destroyed on loading of a new scene
                    DontDestroyOnLoad(obj);

                    Debug.Log(obj.name);
                }
            }
            return instance;
        }
    }

    // Initialiser function, serves a similar purpose to a constructor
    private void Init()
    {
        Setup();
    }

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    #region [ BUILT-IN UNITY FUNCTIONS ]

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        Setup();
    }

    void Start()
    {
        DebugOnStart();
    }

    void Update()
    {
        //UIController.fps = CalcFPS();
        HandleInputs();

        DebugOnUpdate();
    }

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void Setup()
    {
        controlsInstance = gameObject.AddComponent<Controls>();
        Controls = controlsInstance;
        /*vidSettingsInstance = gameObject.AddComponent<VideoSettings>();
        VideoSettings = vidSettingsInstance;*/

        /*LevelController = FindObjectOfType<LevelController>();
        if (LevelController.isGameplayLevel)
        {
            Player = FindObjectOfType<Player>();
        }
        UIController = FindObjectOfType<UIController>();*/

        SetupPrefabPool();

        Application.targetFrameRate = 60;

        if (firstLoad)
        {
            firstLoad = false;
            OnFirstLoad();
        }
    }

    private void OnFirstLoad()
    {

    }

    private float CalcFPS()
    {
        if (frameTimes.Count >= 60)
        {
            frameTimes.RemoveAt(0);
        }
        frameTimes.Add(Time.deltaTime);
        float total = 0.0f;
        foreach (float f in frameTimes)
        {
            total += f;
        }
        float fps = (float)frameTimes.Count / total;
        fps -= (fps % 0.01f);
        return FPS = fps;
    }

    private void HandleInputs()
    {
        /*if (LevelController.isGameplayLevel)
        {
            LevelController.LevelInputs();
        }

        if (Input.GetKeyDown(Controls.General.Pause.Key))
        {
            if (isPaused)
            {
                UIController.pauseMenu.PauseMenuResume();
            }
            else
            {
                UIController.pauseMenu.PauseMenuPause();
            }
        }*/
    }

    #region [ SETTINGS ]

    public void LoadSettings()
    {
        LoadControls();
    }

    public void SaveSettings()
    {
        SaveControls();
        PlayerPrefs.Save();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void LoadControls()
    {
        List<string> controlNames = Controls.GetNamesList();
        foreach (string name in controlNames)
        {
            if (PlayerPrefs.HasKey(name))
            {
                int keyIndex = PlayerPrefs.GetInt(name);
                Controls.SetControlByName(name, (KeyCode)keyIndex);
            }
            else
            {
                int keyIndex = (int)Controls.GetControlByName(name);
                PlayerPrefs.SetInt(name, keyIndex);
            }
        }
    }

    public void SaveControls()
    {
        List<string> controlNames = Controls.GetNamesList();
        foreach (string name in controlNames)
        {
            int keyIndex = (int)Controls.GetControlByName(name);
            PlayerPrefs.SetInt(name, keyIndex);
        }
    }

    #endregion

    #region [ SCENE HANDLING ]



    #endregion

    #region [ OBJECT HANDLING ]

    private void SetupPrefabPool()
    {
        DefaultPool = PhotonNetwork.PrefabPool as DefaultPool;
        if (prefabsList.Count > 0)
        {
            foreach (GameObject prefab in prefabsList)
            {
                if (!DefaultPool.ResourceCache.ContainsKey(prefab.name))
                {
                    DefaultPool.ResourceCache.Add(prefab.name, prefab);
                }
            }
        }
    }

    #endregion

    #region [ DEBUG ]

    private void DebugOnStart()
    {

    }

    private void DebugOnUpdate()
    {
        //Debug.Log(FPS);
    }

    #endregion
}
