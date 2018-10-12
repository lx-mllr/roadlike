using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Chunk", menuName = "TrackSettings/Chunk")]
public class Chunk : ScriptableObject {
    public List<ChunkElement> elements;
}