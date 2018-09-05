using UnityEngine;

public enum BuilderId {
    MINE_BUILDER
}

public class IBuilderSettings : ScriptableObject {
    virtual public int Id { get { return -1; } }
}