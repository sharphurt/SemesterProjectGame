using System;
using System.Drawing;
using UnityEngine;

[Serializable]
public class WaveElement
{
    public string enemy;
    public LocationMethod locationMethod;
    public Vector2 position;
    
    public override bool Equals(object other)
    {
        return other is WaveElement element && element.enemy == enemy;
    }
}