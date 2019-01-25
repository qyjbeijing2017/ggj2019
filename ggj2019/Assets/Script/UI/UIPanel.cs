using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

public class UIPanel : UIBase
{
    [SerializeField] Button m_start;

    public override void close()
    {

    }

    public override void show(bool IsfirstOpen, object[] value)
    {
        if (IsfirstOpen)
        {
            m_start.onClick.AddListener(OnStart);
        }
    }
    public void OnStart()
    {
        LoadSceneManager.Instance.LoadSceneAsync("GameStage");
    }

}
