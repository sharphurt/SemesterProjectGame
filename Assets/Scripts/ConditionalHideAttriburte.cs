using UnityEngine;
using UnityEngine;
using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public readonly string ConditionalSourceField;

    //TRUE = Hide in inspector / FALSE = Disable in inspector 
    public readonly bool HideInInspector;

    public readonly bool InverseValue;

    public ConditionalHideAttribute(string conditionalSourceField, bool inverseValue,  bool hideInInspector = true)
    {
        ConditionalSourceField = conditionalSourceField;
        HideInInspector = hideInInspector;
        InverseValue = inverseValue;
    }
}