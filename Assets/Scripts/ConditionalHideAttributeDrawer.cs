using UnityEngine;
using UnityEditor;
using PropertyDrawer = UnityEditor.PropertyDrawer;

[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var condHAtt = (ConditionalHideAttribute) attribute;
        var enabled = GetConditionalHideAttributeResult(condHAtt, property) ^ condHAtt.InverseValue;

        var wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if (!condHAtt.HideInInspector || enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var condHAtt = (ConditionalHideAttribute) attribute;
        var enabled = GetConditionalHideAttributeResult(condHAtt, property) ^ condHAtt.InverseValue;

        if (!condHAtt.HideInInspector || enabled)
            return EditorGUI.GetPropertyHeight(property, label);

        return -EditorGUIUtility.standardVerticalSpacing;
    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    {
        var enabled = true;
        var propertyPath = property.propertyPath;
        var conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); 
        var sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        if (sourcePropertyValue != null)
            enabled = sourcePropertyValue.boolValue;
        else
            Debug.LogWarning(
                "Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " +
                condHAtt.ConditionalSourceField);

        return enabled;
    }
}