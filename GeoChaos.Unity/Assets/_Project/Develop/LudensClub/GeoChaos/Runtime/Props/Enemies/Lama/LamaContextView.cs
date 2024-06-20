using LudensClub.GeoChaos.Runtime.AI;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props.Enemies.Lama
{
  public class LamaContextView : BrainContextView
  {
    [SerializeField]
    private LamaContext _context;

    public override IBrainContext Context
    {
      get => _context;
      set => _context = (LamaContext) value;
    }
  }
  
  public abstract class BrainContextView : MonoBehaviour, IBrainContextView
  {
    public abstract IBrainContext Context { get; set; }
  }

  public interface IBrainContextView
  {
    public IBrainContext Context { get; }
  }
}