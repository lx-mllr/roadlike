using UnityEngine;
using System;
using System.Collections;
using Zenject;

[CreateAssetMenu(fileName = "ObstacleInstaller", menuName = "Installers/ObstacleInstaller")]
public class ObstacleInstaller : ScriptableObjectInstaller<ObstacleInstaller>
{
    public ImplBuilderFactory.Settings _spawnFactorySettings;

    [Serializable]
    public class Info {
        
    }
    public Info info;

    public override void InstallBindings()
    {
        Container.BindInstance(_spawnFactorySettings);

        Container.BindFactory<IBuilder, BuilderFactory>().FromFactory<ImplBuilderFactory>();
    }
}