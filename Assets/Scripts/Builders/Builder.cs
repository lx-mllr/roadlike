using System;
using System.Collections.Generic;
using Zenject;

public interface IBuilder {
    void SpawnForTile (Tile tile);
}

public class BuilderFactory : PlaceholderFactory<IBuilder> {
}

public class ImplBuilderFactory : IFactory<IBuilder> {

    [Serializable]
    public struct Settings {
        public IBuilderSettings[] builders;
    }

    DiContainer _container;
    Settings _presets;

    public ImplBuilderFactory (DiContainer container, Settings presets) {
        _container = container;
        _presets = presets;
    }

    public IBuilder Create () {
        IBuilder pattern = null;

        int index = (int) UnityEngine.Random.value * _presets.builders.Length;
        IBuilderSettings toCreate = _presets.builders[index];

        switch (toCreate.Id) {
            case (int) BuilderId.MINE_BUILDER:
                pattern = _container.Instantiate<MineBuilder>(new object[] {toCreate});
                break;
            //case (int) SpawnPatternID.DEFAULT:
        }

        return pattern;
    }
}