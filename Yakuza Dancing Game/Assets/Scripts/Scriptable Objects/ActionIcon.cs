using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action Icon", menuName = "Scriptable Objects/Action Icon", order = 1)]
public class ActionIcon : ScriptableObject
{
    public Sprite Sprite;
    public Color Color;
}
