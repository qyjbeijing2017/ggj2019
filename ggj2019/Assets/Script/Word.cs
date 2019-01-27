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
    //[SerializeField] List<Text> m_keyText;
    [SerializeField] public List<WordsConfig.KeyCodeType> Keys;
    [Space(20)]
    [SerializeField] Sprite m_Left;
    [SerializeField] Sprite m_Right;
    [SerializeField] Sprite m_Middle;
    [SerializeField] Color m_SuccessColor;
    int m_KeyNow = 0;
    WordsConfig m_word;
    public event UnityAction OnKeyPressSuccessHandler;
    public event UnityAction OnKeyPressFailedHandler;

    private bool m_isSuccessful = false;
    public void Init(WordsConfig word, List<WordsConfig.KeyCodeType> keys)
    {

        Keys = keys;
        m_text.text = word.text;
        m_word = word;
        for (int i = 0; i < 3; i++)
        {
            switch (Keys[i])
            {
                case WordsConfig.KeyCodeType.Left:
                    m_keyImages[i].sprite = m_Left;
                    //m_keyText[i].text = "F";
                    break;
                case WordsConfig.KeyCodeType.Right:
                    m_keyImages[i].sprite = m_Right;
                    //m_keyText[i].text = "J";
                    break;
                case WordsConfig.KeyCodeType.Middle:
                    m_keyImages[i].sprite = m_Middle;
                    //m_keyText[i].text = "SPACE";
                    break;
                default:
                    break;
            }
            m_animator[i].speed = 0;
        }

        OnKeyPressSuccessHandler += OnKeyPressSuccessful;
        OnKeyPressFailedHandler += OnKeyPressFailed;
        GameController.Instance.OnWordsSuccessful += OnKeyPressFailed;

    }

    public static List<WordsConfig.KeyCodeType> RandomKeys()
    {
        List<WordsConfig.KeyCodeType> words = new List<WordsConfig.KeyCodeType>();
        for (int i = 0; i < 3; i++)
        {
            words.Add((WordsConfig.KeyCodeType)Random.Range(0, 3));
        }
        return words;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.m_isWin)
        {
            Destroy(gameObject);
        }
        if (Input.anyKeyDown && !m_isSuccessful)
        {
            OnKeyDown();
        }

        if (transform.position.y <= GameController.Instance.m_Button)
        {
            if (GameController.Instance.m_keysGroup.Contains(Keys))
            {
                GameController.Instance.m_keysGroup.Remove(Keys);
            }
            Destroy(gameObject);


        }
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(0, GameController.Instance.m_fallSpeed, 0) * Time.fixedDeltaTime;
    }



    void OnKeyDown()
    {
        if (Input.GetButtonDown("Left"))
        {
            if (Keys[m_KeyNow] == WordsConfig.KeyCodeType.Left)
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
        if (Input.GetButtonDown("Right"))
        {
            if (Keys[m_KeyNow] == WordsConfig.KeyCodeType.Right)
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
        if (Input.GetButtonDown("Middle"))
        {
            if (Keys[m_KeyNow] == WordsConfig.KeyCodeType.Middle)
            {
                m_keyImages[m_KeyNow].color = m_SuccessColor;
                m_animator[m_KeyNow].speed = 1;
                m_animator[m_KeyNow].Play("Shake");
                m_KeyNow++;

            }
            else
            {
                if (OnKeyPressFailedHandler != null)
                {
                    OnKeyPressFailedHandler.Invoke();
                }
            }

        }

        if (m_KeyNow >= 3)
        {
            if (null != OnKeyPressSuccessHandler)
            {
                OnKeyPressSuccessHandler.Invoke();
            }

        }
    }

    void OnKeyPressSuccessful()
    {
        GameController.Instance.OnWordsSuccessful -= OnKeyPressFailed;
        m_lateSuccessful = true;

        m_isSuccessful = true;
        if (GameController.Instance.m_keysGroup.Contains(Keys))
        {
            GameController.Instance.m_keysGroup.Remove(Keys);
        }
        StartCoroutine("SuccessfulTimer");

    }





    IEnumerator SuccessfulTimer()
    {
        yield return new WaitForSeconds(0.13f);
        GameController.Instance.LevelAttribute += m_word.attribute;
        Destroy(gameObject);
    }

    void OnKeyPressFailed()
    {
        for (int i = 0; i < 3; i++)
        {
            m_keyImages[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        m_KeyNow = 0;
    }


    private void OnDestroy()
    {
        if (FindObjectOfType<GameController>())
        {
            GameController.Instance.OnWordsSuccessful -= OnKeyPressFailed;
        }

    }


    bool m_lateSuccessful = false;
    private void LateUpdate()
    {
        if (m_lateSuccessful == true)
        {
            if (null != GameController.Instance.OnWordsSuccessful)
            {
                GameController.Instance.OnWordsSuccessful.Invoke();
            }
        }
        else
            m_lateSuccessful = false;
    }
}
