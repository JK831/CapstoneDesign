﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance
    {
       get
        { 
            Init();
            return s_instance;
        }
    }

    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    StageManager _stage = new StageManager();
    UIManager _ui = new UIManager();
    MapManager _map = new MapManager();
    TargetObjectManager _targetObject = new TargetObjectManager();
    CoinManager _coin = new CoinManager();
    CodeBlockManager _block = new CodeBlockManager();
    CodingAreaManager _codingArea  = new CodingAreaManager();
    MusicManager _music = new MusicManager();
    MapObjectManager _mapObject = new MapObjectManager();
    UserManager _user = new UserManager();
    //NetworkManager _netWork = new NetworkManager();
    SessionManager _session = new SessionManager();
    //LoginSetting _loginSetting = new LoginSetting();
    MessageBoxManager _messageBox = new MessageBoxManager();

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static StageManager Stage { get { return Instance._stage; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static MapManager Map { get { return Instance._map; } }
    public static TargetObjectManager TargetObject { get { return Instance._targetObject; } }
    public static CoinManager Coin { get { return Instance._coin; } }
    public static CodeBlockManager CodeBlock { get { return Instance._block; } }
    public static CodingAreaManager CodingArea { get { return Instance._codingArea; } }
    public static MusicManager Music { get { return Instance._music; } }

    public static UserManager User { get { return Instance._user; } }
    //public static NetworkManager Network { get { return Instance._netWork; } }
    public static SessionManager Session { get { return Instance._session; } }
    //public static LoginSetting Login { get { return Instance._loginSetting; } }
    public static MapObjectManager MapObject { get { return Instance._mapObject; } }
    public static MessageBoxManager MessageBox { get { return Instance._messageBox; } }

    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
        //_netWork.OnUpdate();
    }
    static void Init()
    {

        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
                
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            //s_instance._netWork.Init();
            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
            s_instance._music.Init();

            //s_instance._netWork.Init();

        }
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        //Scene.Clear();
        UI.Clear();
        Pool.Clear();
        Map.Clear();
        Coin.Clear();
        Stage.Clear();
        TargetObject.Clear();
        MapObject.Clear();
    }
}
