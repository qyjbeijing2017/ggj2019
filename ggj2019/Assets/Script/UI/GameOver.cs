using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

public class GameOver : UIBase
{
    [SerializeField] Button m_restart;
    [SerializeField] Button m_back;
    [SerializeField] Text text;

    public override void close()
    {
        Time.timeScale = 1;
    }

    public override void show(bool IsfirstOpen, object[] value)
    {
        if (IsfirstOpen)
        {
            m_back.onClick.AddListener(OnBack);
            m_restart.onClick.AddListener(OnRestart);
            text.text = value[0] as string;

        }
        Time.timeScale = 0;
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
