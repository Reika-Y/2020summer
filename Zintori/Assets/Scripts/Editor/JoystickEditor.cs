using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// ジョイスティックのInspectorを変えるエディター
[CustomEditor(typeof(Joystick))]
public class JoystickEditor : Editor
{
    // プロパティ編集用
    private SerializedProperty radiusProperty;
    private SerializedProperty positionProperty;

    // 初期化
    private void OnEnable()
    {
        radiusProperty = serializedObject.FindProperty("radius");
        positionProperty = serializedObject.FindProperty("position");
    }

    // 更新
    public override void OnInspectorGUI()
    {
        // 更新開始
        serializedObject.Update();

        // ジョイスティックを取得
        Joystick joystick = target as Joystick;

        // 初期化してない時は初期化ボタンを表示
        if(!joystick.IsInitialized)
        {
            if(GUILayout.Button("Init"))
            {
                joystick.Init();
            }
        }
        else
        {
            // スティックが動く半径
            float radius = EditorGUILayout.FloatField(radiusProperty.floatValue);
            if(radius != radiusProperty.floatValue)
            {
                radiusProperty.floatValue = radius;
            }

            // 現在地の表示
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(
                "X : " + positionProperty.vector2Value.x.ToString("F2") +
                ", Y : " + positionProperty.vector2Value.y.ToString("F2")
                );
            EditorGUILayout.EndVertical();
        }

        // 更新終了
        serializedObject.ApplyModifiedProperties();
    }
}
