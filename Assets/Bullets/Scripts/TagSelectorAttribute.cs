#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class TagSelectorAttribute : PropertyAttribute{}

[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    { 
        if (IsPropertyTypeString(property))
        {
            EditorGUI.BeginProperty(position, label, property);

            // Use EditorGUI.TagField to display a dropdown with Unity tags and set the selected tag to the property's value.
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            
            EditorGUI.EndProperty();
        }
    }

    private bool IsPropertyTypeString(SerializedProperty property)
    {
        return property.propertyType == SerializedPropertyType.String;
    }

}
#endif