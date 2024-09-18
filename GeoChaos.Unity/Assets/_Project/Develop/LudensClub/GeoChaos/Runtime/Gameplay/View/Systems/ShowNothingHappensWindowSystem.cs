using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.UI.NothingHappensWindow;

namespace LudensClub.GeoChaos.Runtime.Gameplay.View
{
  public class ShowNothingHappensWindowSystem : IEcsRunSystem
  {
    private readonly INothingHappensPresenter _presenter;
    private readonly EcsWorld _message;
    private readonly EcsEntities _nothingHappensMessages;

    public ShowNothingHappensWindowSystem(MessageWorldWrapper messageWorldWrapper, INothingHappensPresenter presenter)
    {
      _presenter = presenter;
      _message = messageWorldWrapper.World;

      _nothingHappensMessages = _message
        .Filter<NothingHappensMessage>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      if (_nothingHappensMessages.Any())
      {
       _presenter.ShowWindow(); 
      }
    }
  }
}