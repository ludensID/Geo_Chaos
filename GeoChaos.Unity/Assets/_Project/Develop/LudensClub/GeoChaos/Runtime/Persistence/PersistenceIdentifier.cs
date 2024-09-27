using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  [AddComponentMenu(ACC.Names.PERSISTENCE_IDENTIFIER)]
  [DisallowMultipleComponent]
  public class PersistenceIdentifier : MonoBehaviour
  {
    [SerializeField]
    [ReadOnly]
    private int _identifier;

    public int Identifier => _identifier;

#if UNITY_EDITOR
    [OnValueChanged(nameof(OnCustomIdentifierChanged))]
    public int CustomIdentifier;

    public bool IsChanged { get; set; }

    private void OnCustomIdentifierChanged()
    {
      IsChanged = true;
    }
#endif

    private void Reset()
    {
      HashSet<int> identifiers = FindObjectsByType<PersistenceIdentifier>(FindObjectsSortMode.None)
        .Except(new[] { this })
        .Select(x => x.Identifier)
        .ToHashSet();

      for (int i = 1; i < int.MaxValue; i++)
      {
        if (!identifiers.Contains(i))
        {
          _identifier = i;
          break;
        }
      }

      CustomIdentifier = _identifier;
    }
  }
}