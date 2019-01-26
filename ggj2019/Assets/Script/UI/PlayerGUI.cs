using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : UIBase
{
    [SerializeField] Text m_love;
    [SerializeField] Text m_responsibility;
    [SerializeField] Text m_Stress;
    string m_loveStart;
    string m_responsibilityStart;
    string m_stressStart;

    public override void close()
    {

    }

    public override void show(bool IsfirstOpen, object[] value)
    {
        if (IsfirstOpen)
        {
            m_loveStart = m_love.text;
            m_responsibilityStart = m_responsibility.text;
            m_stressStart = m_Stress.text;
        }
    }

    private void Update()
    {
        m_love.text = m_loveStart + (int)GameController.Instance.LevelAttribute.Love;
        m_responsibility.text = m_responsibilityStart + (int)GameController.Instance.LevelAttribute.Responsibility;
        m_Stress.text = m_stressStart + (int)GameController.Instance.LevelAttribute.Stress;

    }

}
