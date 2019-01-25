﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaemonTools;
using UnityEngine.UI;




public class Daemon : MonoSingleton<Daemon>
{

    new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        ConfigManager.Instance.InitUIConfig();
        UIManager.Instance.Init();
        ConfigManager.Instance.InitConfigManager();
    }

    private void Start() {
        UIManager.Instance.Open("StartPanel"); 
              
    }


}
