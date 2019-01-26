using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

public class UIPanel : UIBase
{
    [SerializeField] Button m_start;
    [SerializeField] Button m_quite;

    public override void close()
    {

    }

    public override void show(bool IsfirstOpen, object[] value)
    {
        if (IsfirstOpen)
        {
            m_start.onClick.AddListener(OnStart);
            m_quite.onClick.AddListener(OnQuite);
        }
    }
    public void OnStart()
    {
        LoadSceneManager.Instance.LoadSceneAsync("GameStage");
        
    }

    public void OnQuite()
    {
        Application.Quit();
    }

}
