using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Curtain
{
  [AddComponentMenu(ACC.Names.CURTAIN_VIEW)]
  public class CurtainView : MonoBehaviour
  {
    [SerializeField]
    private Slider _slider;
      
    [Inject]
    public void Construct(ICurtainPresenter presenter)
    {
      presenter.SetView(this);
    }

    public void SetSliderValue(float value)
    {
      _slider.value = value;
    }

    private void Reset()
    {
      _slider = GetComponentInChildren<Slider>();
    }
  }
}