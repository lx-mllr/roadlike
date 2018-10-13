using UnityEngine;

public enum BuilderId {
    GROUPED_BUILDER,
    LINEAR_BUILDER
}

public class IBuilderSettings : ScriptableObject {
    virtual public int Id { get { return -1; } }
}