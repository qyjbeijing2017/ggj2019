using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GameController))]
public class GameControlEditor : Editor
{
    private void OnSceneGUI()
    {
        GameController gc = (GameController)target;
        Handles.color = new Color(1.0f, 1.0f, 0.0f);
        Handles.DrawLine(new Vector3(gc.m_leftEdge, gc.m_Top, 0.0f), new Vector3(gc.m_rightEdge, gc.m_Top, 0.0f));
        Handles.DrawLine(new Vector3(gc.m_leftEdge, gc.m_Button, 0), new Vector3(gc.m_leftEdge, gc.m_Top, 0));
        Handles.DrawLine(new Vector3(gc.m_leftEdge, gc.m_Button, 0), new Vector3(gc.m_rightEdge, gc.m_Button, 0));
        Handles.DrawLine(new Vector3(gc.m_rightEdge, gc.m_Button, 0), new Vector3(gc.m_rightEdge, gc.m_Top, 0));
    }
}
