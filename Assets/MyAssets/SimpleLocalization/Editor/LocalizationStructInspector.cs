using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LocalizationStruct))]
public class LocalizationStructInspector : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect keyrect = new Rect(position.x, position.y, position.width, position.height/2f);
        EditorGUI.PropertyField(keyrect, property.FindPropertyRelative("key"));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2.5f;
    }
}