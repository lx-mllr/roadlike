using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ChunkElement", menuName = "TrackSettings/ChunkElement")]
public class ChunkElement : ScriptableObject {
    public List<Tile> tiles;
    public List<IBuilderSettings> obstacleBuilders;
}