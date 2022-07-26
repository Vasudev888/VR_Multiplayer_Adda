using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This Script is responsible fo creating and joining rooms", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;

        if(GUILayout.Button("Join PlayArea Room"))
        {
            roomManager.OnEnteredButtonClicked_PlayArea();
        }

        if (GUILayout.Button("Join Outdoor Room"))
        {
            roomManager.OnEnteredButtonClicked_Outdoor();
        }
    }
}
