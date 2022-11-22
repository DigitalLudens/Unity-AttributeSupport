using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionalTypeFieldAttribute))]
public class ConditionalTypeFieldPropertyDrawer : PropertyDrawer
{
    public bool IsChangeTypeProperty(SerializedProperty property)
    {
        ConditionalTypeFieldAttribute conditionalTypeField = attribute as ConditionalTypeFieldAttribute;
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, conditionalTypeField.comparedPropertyName) : conditionalTypeField.comparedPropertyName;
        SerializedProperty comparedField = property.serializedObject.FindProperty(path);
        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        if (comparedField.type == "bool")
            return comparedField.boolValue == conditionalTypeField.comparedValue;
        return false;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalTypeFieldAttribute conditionalTypeField  = attribute as ConditionalTypeFieldAttribute;
        if (IsChangeTypeProperty(property))
        {
            switch (conditionalTypeField.comparedType.Name)
            {
                case "Int32":
                    property.floatValue = EditorGUI.IntField(position, property.displayName, (int)property.floatValue);
                    break;
                default:
                    EditorGUI.PropertyField(position, property, label);
                    break;
            }
        }
        else
            EditorGUI.PropertyField(position, property, label);
    }

}

