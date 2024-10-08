using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime;
using UnityEditor;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Boot
{
  [InitializeOnLoad]
  public static class EditorEntryPoint
  {
    private static EditorContext _context;
    private static DiContainer _container => _context.Container;

    static EditorEntryPoint()
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
      EditorInstaller.Install(_container);
    }
  }
}