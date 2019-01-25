using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndConfig : BaseConfig
{
    public enum SymbolType
    {
        Equal = 0,
        Greater = 1,
        Less = 2
    }
    public int Id = 0;
    public string Text = string.Empty;
    public List<SymbolType> Symbol = new List<SymbolType>();
    public Vector3 Value = Vector3.zero;



    public void InitConfig(List<string> m_data)
    {
        int.TryParse(m_data[0], out Id);
        Text = m_data[1];
        string[] symbols = m_data[2].Split('#');
        if (symbols.Length == 3)
        {
            Symbol.Add((SymbolType)Enum.Parse(typeof(SymbolType), symbols[0]));
            Symbol.Add((SymbolType)Enum.Parse(typeof(SymbolType), symbols[1]));
            Symbol.Add((SymbolType)Enum.Parse(typeof(SymbolType), symbols[2]));
        }
        else { Debug.LogError("SymbolType Error!"); }

        string[] value = m_data[3].Split('#');
        if (value.Length == 3)
        {
            int x = 0;
            int.TryParse(value[0], out x);
            int y = 0;
            int.TryParse(value[1], out y);
            int z = 0;
            int.TryParse(value[2], out z);
            Value = new Vector3(x, y, z);
        }
        else { Debug.LogError("Value Error!"); }


    }


}
