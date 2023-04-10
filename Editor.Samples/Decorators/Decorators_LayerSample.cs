using TriInspector;
using UnityEngine;

public class Decorators_LayerSample : ScriptableObject
{
    [Layer]
    public int _firstLayer = 2;
    
    [Layer]
    public LayerMask _secondLayer = 5;
}