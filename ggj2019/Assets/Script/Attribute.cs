using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute
{
    Vector3 m_arrtibute;
    public float Love { get { return m_arrtibute.x; } set { m_arrtibute.x = value; } }
    public float Responsibility { get { return m_arrtibute.y; } set { m_arrtibute.y = value; } }
    public float Stress { get { return m_arrtibute.z; } set { m_arrtibute.z = value; } }

    public Attribute(float love, float responsibility, float stress)
    {
        m_arrtibute = new Vector3(love, responsibility, stress);
    }
    public Attribute()
    {
        m_arrtibute = Vector3.zero;
    }
    public Attribute(Vector3 vec3)
    {
        m_arrtibute = vec3;
    }

    public static Attribute operator +(Attribute a, Attribute b)
    {
        Attribute c = new Attribute();
        c.Love = a.Love + b.Love;
        c.Responsibility = a.Responsibility + b.Responsibility;
        c.Stress = a.Stress + b.Stress;
        return c;
    }

    public static Attribute operator -(Attribute a, Attribute b)
    {
        Attribute c = new Attribute();
        c.Love = a.Love - b.Love;
        c.Responsibility = a.Responsibility - b.Responsibility;
        c.Stress = a.Stress - b.Stress;
        return c;
    }

    public static bool Like(Attribute attribute, EndConfig end)
    {
        if (!LikeHelp((int)attribute.Love, end.Symbol[0], (int)end.Value.x))
            return false;
        if (!LikeHelp((int)attribute.Responsibility, end.Symbol[1], (int)end.Value.y))
            return false;
        if (!LikeHelp((int)attribute.Stress, end.Symbol[2], (int)end.Value.z))
            return false;

        return true;

    }

    static bool LikeHelp(int a, EndConfig.SymbolType type, int value)
    {
        switch (type)
        {
            case EndConfig.SymbolType.Equal:
                if (value == a)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case EndConfig.SymbolType.Greater:
                if (a >= value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case EndConfig.SymbolType.Less:
                if (a < value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }

}
