using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

public class Pause : UIBase
{
    [SerializeField]Button m_restart;
    [SerializeField]Button m_menu;
    public override void close()
    {
        Time.timeScale = 1;
    }

    public override void show(bool IsfirstOpen, object[] value)
    {

        if (IsfirstOpen)
        {
            m_menu.onClick.AddListener(OnBack);
            m_restart.onClick.AddListener(OnRestart);

        }
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UIManager.Instance.close();
        }
    }

    public void OnRestart()
    {
        LoadSceneManager.Instance.LoadSceneAsync("GameStage");
        Time.timeScale = 1;

    }

    public void OnBack()
    {
        LoadSceneManager.Instance.LoadSceneAsync("StartStage");
        Time.timeScale = 1;
    }

}
