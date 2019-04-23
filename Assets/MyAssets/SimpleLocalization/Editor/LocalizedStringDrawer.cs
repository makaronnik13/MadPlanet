using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Localization
{
    [CustomPropertyDrawer(typeof(LocalizedString))]
    public class LocalizedStringDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Debug.Log("draw");
            Rect titlerect = new Rect(position.x, position.y, position.width / 3f, position.height);
            Rect dictrect = new Rect(position.x + position.width / 3f, position.y, position.width / 3f, position.height);
            Rect keyrect = new Rect(position.x + position.width*2f / 3f, position.y, position.width / 3f, position.height);

            EditorGUI.LabelField(titlerect, property.name);
            EditorGUI.PropertyField(dictrect, property.FindPropertyRelative("Dictionary"), GUIContent.none);

            LocalizationDictionary dict = property.FindPropertyRelative("Dictionary").objectReferenceValue as LocalizationDictionary;
            if (dict!=null)
            {
                if (!dict.Keys.Contains(property.FindPropertyRelative("Key").stringValue))
                {
                    property.FindPropertyRelative("Key").stringValue = dict.Keys[0];
                }
                int index = dict.Keys.IndexOf(property.FindPropertyRelative("Key").stringValue);
                index = EditorGUI.Popup(keyrect, index, dict.Keys.ToArray());
                property.FindPropertyRelative("Key").stringValue = dict.localisedStrings[index].key;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}