using System;
using LudensClub.GeoChaos.Debugging.Persistence;
using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime;
using UnityEditor;
using Zenject;
using TypeCache = LudensClub.GeoChaos.Editor.General.TypeCache;

namespace LudensClub.GeoChaos.Editor
{
  [InitializeOnLoad]
  public static class EditorEnterPoint
  {
    private static EditorContext _context;
    private static DiContainer _container => _context.Container;

    static EditorEnterPoint()
    {
      EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

      InitializeContext();
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
      if (state == PlayModeStateChange.EnteredEditMode)
      {
        RecreateContainer();
      }
    }

    private static void InitializeContext()
    {
      _context = new EditorContext();
      EditorMediator.Context = _context;
      EditorMediator.Container = new EditorContainer
      {
        ProfilerService = new ProfilerService(),
        TypeCache = new TypeCache()
      };

      InstallBindings();
      _context.ResolveRoots();
    }

    private static void RecreateContainer()
    {
      _context.CreateContainer();
        
      InstallBindings();
      _context.ResolveRoots();
    }

    private static void InstallBindings()
    {
      _container
        .Bind<IEditorInitializer>()
        .To<EditorInitializer>()
        .AsSingle()
        .NonLazy();

      _container
        .Bind<IProfilerService>()
        .To<ProfilerService>()
        .AsSingle();

      _container
        .Bind<ITypeCache>()
        .To<TypeCache>()
        .AsSingle();

      PersistenceEditorInstaller.Install(_container);
    }
  }
}