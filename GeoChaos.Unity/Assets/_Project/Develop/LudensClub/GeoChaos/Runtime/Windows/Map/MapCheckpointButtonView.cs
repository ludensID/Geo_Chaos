using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Map
{
  [AddComponentMenu(ACC.Names.MAP_CHECKPOINT_BUTTON_VIEW)]
  public class MapCheckpointButtonView : MonoBehaviour
  {
    public CheckpointView Checkpoint;

    [SerializeField]
    private Button _button;
      
    public IMapCheckpointButtonPresenter Presenter { get; private set; }

    [Inject]
    public void Construct(IMapCheckpointButtonPresenter presenter)
    {
      Presenter = presenter;
      Presenter.SetView(this);
      
      _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
      _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
      Presenter.MoveHero();
    }

    public void SetInteraction(bool active)
    {
      _button.interactable = active;
    }

    private void Reset()
    {
      _button = GetComponentInChildren<Button>();
    }
  }
}