using UnityEngine;

[CreateAssetMenu(fileName = "GroupedBuilder", menuName = "BuilderSettings/Group")]
public class GroupedBuilderSettings : IBuilderSettings {        
    public GameObject prefab;

    public int minCount;
    public int maxCount;
    public int minSpread;
    public int maxSpread;
    public Vector2 circleSize;
    [Range(0.0f, 0.5f)] public float distReduction;
    [Range(0.0f, 0.785f)] public float rotVariance;
    
    override public int Id { get { return (int) BuilderId.GROUPED_BUILDER; } }
}