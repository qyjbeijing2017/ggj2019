using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordsConfig :BaseConfig
{

    public enum KeyCodeType
    {
        Left = 0,
        Right = 1,
        Middle = 2
    }
    public int ID;
    public string text;
    public List<KeyCodeType> Keys = new List<KeyCodeType>();
    public Attribute attribute = new Attribute();
    public int group = 0;
    
    public void InitConfig(List<string> m_data)
    {
        int.TryParse(m_data[0], out ID);
        text = m_data[1];

        string[] attributes = m_data[2].Split('#');
        if (attributes.Length == 3)
        {
            int love = 0;
            int.TryParse(attributes[0], out love);
            int responsibility = 0;
            int.TryParse(attributes[1], out responsibility);
            int stress = 0;
            int.TryParse(attributes[2], out stress);
            attribute = new Attribute(love, responsibility, stress);
        }

        int.TryParse(m_data[4], out group);

    }

}
