using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Windows;

namespace LudensClub.GeoChaos.Runtime.Gameplay.View
{
  public class ShowNothingHappensWindowSystem : IEcsRunSystem
  {
    private readonly IWindowManager _windowManager;
    private readonly EcsWorld _message;
    private readonly EcsEntities _nothingHappensMessages;

    public ShowNothingHappensWindowSystem(MessageWorldWrapper messageWorldWrapper, IWindowManager windowManager)
    {
      _windowManager = windowManager;
      _message = messageWorldWrapper.World;

      _nothingHappensMessages = _message
        .Filter<NothingHappensMessage>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      if (_nothingHappensMessages.Any())
      {
        _windowManager.Open(WindowType.NothingHappens);
      }
    }
  }
}