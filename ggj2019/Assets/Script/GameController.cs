using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;

public class GameController : MonoSingleton<GameController>
{

    [SerializeField] public List<List<WordsConfig.KeyCodeType>> m_keysGroup = new List<List<WordsConfig.KeyCodeType>>();
    [SerializeField] List<WordsConfig> m_words;
    [SerializeField] float m_lastXPosition = 0.0f;
    [SerializeField] Word m_word;

    [SerializeField, Range(-10, 0), Header("随机words刷新左边缘")] public float m_leftEdge = -3;
    [SerializeField, Range(0, 10), Header("随机words刷新右边缘")] public float m_rightEdge = 3;
    [SerializeField, Range(0, 10), Header("随机刷新起始高度")] public float m_Top = 5.0f;
    [SerializeField, Range(-10, 0), Header("words消失高度")] public float m_Button = -10.0f;
    [SerializeField, Header("随机刷新最短时间")] float m_minRefresh = 1.0f;
    [SerializeField, Header("随机刷新最长时间")] float m_maxRefresh = 2.0f;
    [SerializeField, Header("随机刷新时间每秒递减值")] float m_decreaseRefresh = 0.3f;
    [SerializeField, Header("随机刷新时间递减至最小值")] float m_decreaseRefreshMin = 0.1f;
    [SerializeField, Header("随机刷新位置与上个词条x轴差值")] float m_xChange = 1.5f;
    [Space(20)]
    [SerializeField, Header("下落速度")] public float m_fallSpeed = 1.0f;
    [SerializeField, Header("下落速度每秒增加值")]public float m_fallSpeedCrease = 0.3f;
    [SerializeField, Header("下落速度最大值")] public float m_fallSpeedMax = 5.0f;

    [Space(20)]
    [SerializeField, Range(0, 100), Header("初始爱情值")] int m_love;
    [SerializeField, Range(0, 100), Header("初始责任值")] int m_responsibility;
    [SerializeField, Range(0, 100), Header("初始压力值")] int m_stress;
    [SerializeField, Header("L属性每秒减少值")] float m_attributeDecreaseLove = 1.0f;
	[SerializeField, Header("R属性每秒减少值")] float m_attributeDecreaseResponsibility = 1.0f;
	[SerializeField, Header("S属性每秒减少值")] float m_attributeDecreaseStress = 1.0f;



    public Attribute LevelAttribute = new Attribute();


    public event UnityAction OnWin;
    // Start is called before the first frame update
    public bool m_isWin = false;
    private List<List<WordsConfig>> Words = new List<List<WordsConfig>>();

    public UnityAction OnWordsSuccessful;

    void Start()
    {
        LevelAttribute = new Attribute(m_love, m_responsibility, m_stress);


        List<WordsConfig> wordsGroup1 = new List<WordsConfig>();
        List<WordsConfig> wordsGroup2 = new List<WordsConfig>();
        List<WordsConfig> wordsGroup3 = new List<WordsConfig>();
        for (int i = 0; i < ConfigManager.Instance.WordsConfigData.Count ; i++)
        {
            switch (ConfigManager.Instance.WordsConfigData[i+1].group)
            {
                case 1:
                    wordsGroup1.Add(ConfigManager.Instance.WordsConfigData[i+1]);
                    break;
                case 2:
                    wordsGroup2.Add(ConfigManager.Instance.WordsConfigData[i+1]);
                    break;
                case 3:
                    wordsGroup3.Add(ConfigManager.Instance.WordsConfigData[i+1]);
                    break;
                default:
                    break;
            }
        }
        Words.Add(wordsGroup1);
        Words.Add(wordsGroup2);
        Words.Add(wordsGroup3);

        StartCoroutine("CreatWordsLoop");

        UIManager.Instance.Open("PlayerGUI");
        OnWin += Win;
    }

    private void FixedUpdate()
    {
        if (m_minRefresh > m_decreaseRefreshMin)
        {
            m_minRefresh -= m_decreaseRefresh * Time.fixedDeltaTime;
            m_maxRefresh -= m_decreaseRefresh * Time.fixedDeltaTime;
        }

        if (m_fallSpeed < m_fallSpeedMax)
        {
            m_fallSpeed += m_fallSpeedCrease * Time.fixedDeltaTime;
        }
    }



    void Win()
    {
        m_isWin = true;
        EndConfig end = new EndConfig();
        for (int i = 0; i < ConfigManager.Instance.EndConfigData.Count; i++)
        {
            if (Attribute.Like(LevelAttribute, ConfigManager.Instance.EndConfigData[i+1]))
            {
                end = ConfigManager.Instance.EndConfigData[i+1];
            }
        }
        Debug.Log(end.Id);
        Debug.Log(LevelAttribute.Love +"," +LevelAttribute.Responsibility+"," +LevelAttribute.Stress);
        UIManager.Instance.Open("GameOver", true,end.Text);
    }


    bool KeysOk(List<WordsConfig.KeyCodeType> keys)
    {
        for (int i = 0; i < m_keysGroup.Count; i++)
        {
            if (keys[0] == m_keysGroup[i][0] && 
                keys[1] == m_keysGroup[i][1] &&
                keys[2] == m_keysGroup[i][2])
            {
                return false;
            }
        }
        return true;
    }


    // Update is called once per frame
    void Update()
    {
        if (m_isWin)
            return;


        if ((int)LevelAttribute.Love >= 100 ||
            (int)LevelAttribute.Love <= 0 ||
            (int)LevelAttribute.Responsibility >= 100 ||
            (int)LevelAttribute.Responsibility <= 0 ||
            (int)LevelAttribute.Stress >= 100.0f ||
            (int)LevelAttribute.Stress <= 0)
        {          
            if (null != OnWin)
            {
                OnWin();
            }
        }


        LevelAttribute -= new Attribute(m_attributeDecreaseLove * Time.deltaTime, m_attributeDecreaseResponsibility * Time.deltaTime, m_attributeDecreaseStress * Time.deltaTime);

        m_love = (int)LevelAttribute.Love;
        m_stress = (int)LevelAttribute.Stress;
        m_responsibility = (int)LevelAttribute.Responsibility;

        if (FindObjectOfType<Pause>() && FindObjectOfType<Pause>().gameObject.activeSelf)
        {
            return;
        }
 
        if (Input.GetButtonDown("Cancel"))
        {
            UIManager.Instance.Open("Pause");
        }
    }


    WordsConfig CreatWord()
    {
        float group = Random.Range(0.0f, 1.0f);
        if (group>0.9f)
        {
            int group1 = Random.Range(0, Words[2].Count);
            return Words[2][group1];
        }
        if (group>0.6f)
        {
            int group2 = Random.Range(0, Words[1].Count);
            return Words[1][group2];
        }

        int group3 = Random.Range(0, Words[0].Count);
        return Words[0][group3];
    }


    IEnumerator CreatWordsLoop()
    {
        float nextrefreshTime = 0.0f;
        while (!m_isWin)
        {
            List<WordsConfig.KeyCodeType> keys = Word.RandomKeys();
            while (!KeysOk(keys))
            {
                keys = Word.RandomKeys();
            }
            WordsConfig words = CreatWord();

            m_keysGroup.Add(keys);

            if (m_word == null) { break; }
            Word obj = Instantiate(m_word);
            obj.Init(words, keys);
            float positionX = Random.Range(m_leftEdge, m_rightEdge);
            while (Mathf.Abs(positionX - m_lastXPosition) <= m_xChange)
            {
                positionX = Random.Range(m_leftEdge, m_rightEdge);
            }

            obj.transform.position = new Vector3(positionX, m_Top, 0);

            m_lastXPosition = positionX;

            nextrefreshTime = Random.Range(m_minRefresh, m_maxRefresh);
            yield return new WaitForSeconds(nextrefreshTime);

        }

        
        

    }



}
