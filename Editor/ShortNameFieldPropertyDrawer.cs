using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(ShortNameFieldAttribute))]
public class ShortNameFieldPropertyDrawer : PropertyDrawer
{
    GUIContent ShortName = null;
    public ShortNameFieldPropertyDrawer()
    {
        
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if(ShortName == null)
        {
            ShortNameFieldAttribute shortNameField = attribute as ShortNameFieldAttribute;
            ShortName = new GUIContent(shortNameField.shortName);
        }
        return EditorGUIUtility.singleLineHeight;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShortNameFieldAttribute shortNameField = attribute as ShortNameFieldAttribute;
        SerializedProperty targetProperty = property.FindPropertyRelative(shortNameField.targetName);
        EditorGUI.PropertyField(position, targetProperty, ShortName, false);
    }
}
