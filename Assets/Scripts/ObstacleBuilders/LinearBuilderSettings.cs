using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LinearBuilder", menuName = "BuilderSettings/Linear")]
public class LinearBuilderSettings : IBuilderSettings {
    public GameObject prefab;

    public int minCount;
    public int maxCount;
    
    override public int Id { get { return (int) BuilderId.LINEAR_BUILDER; } }   
}