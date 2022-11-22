using UnityEditor;
using UnityEngine;

/// <summary>
/// Based on: https://forum.unity.com/threads/draw-a-field-only-if-a-condition-is-met.448855/
/// </summary>
[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldPropertyDrawer : PropertyDrawer
{
    #region Fields

    // Reference to the attribute on the property.
    ConditionalFieldAttribute conditionalField;

    // Field that is being compared.
    SerializedProperty comparedField;

    #endregion

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!IsShowProperty(property) && conditionalField.disablingType == ConditionalFieldAttribute.DisablingType.DontDraw)
            return 0f;

        // The height of the property should be defaulted to the default height.
        return base.GetPropertyHeight(property, label);
    }

    /// <summary>
    /// Errors default to showing the property.
    /// </summary>
    private bool IsShowProperty(SerializedProperty property)
    {
        conditionalField = attribute as ConditionalFieldAttribute;
        // Replace propertyname to the value from the parameter
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, conditionalField.comparedPropertyName) : conditionalField.comparedPropertyName;

        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        // get the value & compare based on types
        switch (comparedField.type)
        { // Possible extend cases to support your own type
            case "bool":
                return comparedField.boolValue.Equals(conditionalField.comparedValue);
            case "Enum":
                return comparedField.enumValueIndex.Equals((int)conditionalField.comparedValue);
            default:
                Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                return true;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // If the condition is met, simply draw the field.
        if (IsShowProperty(property))
        {
            EditorGUI.PropertyField(position, property, label);
        } //...check if the disabling type is read only. If it is, draw it disabled
        else if (conditionalField.disablingType == ConditionalFieldAttribute.DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }

}

