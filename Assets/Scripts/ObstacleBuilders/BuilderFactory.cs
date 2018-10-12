using System;
using System.Collections.Generic;
using Zenject;

public class BuilderFactory : PlaceholderFactory<IBuilder> {
}

public class ImplBuilderFactory : IFactory<IBuilder>, IValidatable {

    [Serializable]
    public class Settings {
        public List<IBuilderSettings> builders;
    }

    DiContainer _container;
    Settings _presets;

    public ImplBuilderFactory (DiContainer container, Settings presets) {
        _container = container;
        _presets = presets;
    }

    public IBuilder Create () {
        IBuilder pattern = null;

        int index = (int) UnityEngine.Random.value * _presets.builders.Count;
        IBuilderSettings toCreateSettings = _presets.builders[index];

        switch (toCreateSettings.Id) {
            case (int) BuilderId.GROUPED_BUILDER:
                pattern = _container.Instantiate<GroupedObstacleBuilder>(new object[] {toCreateSettings});
                break;
        }

        return pattern;
    }

    public void Validate () {
        _container.Instantiate<GroupedObstacleBuilder>(new object[] {UnityEngine.ScriptableObject.CreateInstance(typeof(GroupedBuilderSettings))});
    }
}