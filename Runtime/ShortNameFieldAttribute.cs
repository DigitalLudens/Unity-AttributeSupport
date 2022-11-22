using UnityEngine;
using System;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class ShortNameFieldAttribute : PropertyAttribute
{
    public string shortName { get; private set; }
    public string targetName { get; private set; }

    public ShortNameFieldAttribute(string shortName, string targetName)
    {
        this.shortName = shortName;
        this.targetName = targetName;
    }
}
