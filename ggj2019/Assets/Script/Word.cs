using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Word : MonoBehaviour
{
    [SerializeField] Text m_text;
    [SerializeField] List<Image> m_keyImages;
    [SerializeField] List<Animator> m_animator;
    [SerializeField] List<WordsConfig.KeyCodeType> m_keys;
    [Space (20)]
    [SerializeField] Sprite m_Left;
    [SerializeField] Sprite m_Right;
    [SerializeField] Sprite m_Middle;
    [SerializeField] Color m_SuccessColor;
    int m_KeyNow = 0;
    WordsConfig m_word;
    public event UnityAction OnKeyPressSuccessHandler;
    public event UnityAction OnKeyPressFailedHandler;

    public void Init(WordsConfig word)
    {
        m_keys = word.Keys;
        m_text.text = word.text;
        m_word = word;
        for (int i = 0; i < 3; i++)
        {
            switch (m_keys[i])
            {
                case WordsConfig.KeyCodeType.Left:
                    m_keyImages[i].sprite = m_Left;
                    break;
                case WordsConfig.KeyCodeType.Right:
                    m_keyImages[i].sprite = m_Right;
                    break;
                case WordsConfig.KeyCodeType.Middle:
                    m_keyImages[i].sprite = m_Middle;
                    break;
                default:
                    break;
            }
            m_animator[i].speed = 0;
        }

        OnKeyPressSuccessHandler += OnKeyPressSuccessful;
        OnKeyPressFailedHandler += OnKeyPressFailed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            OnKeyDown();
        }
    }


    void OnKeyDown()
    {
        if (Input.GetKeyDown("Left"))
        {
            if (m_keys[m_KeyNow] == WordsConfig.KeyCodeType.Left)
            {
                m_keyImages[m_KeyNow].color = m_SuccessColor;
                m_animator[m_KeyNow].speed = 1;
                m_animator[m_KeyNow].Play("Shake");
                m_KeyNow++;

            }
            else
            {
                if (null != OnKeyPressFailedHandler)
                {
                    OnKeyPressFailedHandler.Invoke();
                }
            }

        }
        if (Input.GetKeyDown("Right"))
        {
            if (m_keys[m_KeyNow] == WordsConfig.KeyCodeType.Right)
            {
                m_keyImages[m_KeyNow].color = m_SuccessColor;
                m_animator[m_KeyNow].speed = 1;
                m_animator[m_KeyNow].Play("Shake");
                m_KeyNow++;
            }
            else
            {
                if (null != OnKeyPressFailedHandler)
                {
                    OnKeyPressFailedHandler.Invoke();
                }
            }

        }
        if (Input.GetKeyDown("Middle"))
        {
            if (m_keys[m_KeyNow] == WordsConfig.KeyCodeType.Middle)
            {
                m_keyImages[m_KeyNow].color = m_SuccessColor;
                m_animator[m_KeyNow].speed = 1;
                m_animator[m_KeyNow].Play("Shake");
                m_KeyNow++;

            }
            else
            {
                if (OnKeyPressFailedHandler!= null)
                {
                    OnKeyPressFailedHandler.Invoke();
                }
            }

        }

        if (m_KeyNow == 3)
        {
            if (null != OnKeyPressSuccessHandler)
            {
                OnKeyPressSuccessHandler.Invoke();
            }

        }
    }

    void OnKeyPressSuccessful()
    {

    }
    void OnKeyPressFailed()
    {
        for (int i = 0; i < 3; i++)
        {
            m_keyImages[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

}
