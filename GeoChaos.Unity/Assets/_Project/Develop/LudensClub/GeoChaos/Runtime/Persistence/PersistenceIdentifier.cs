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
    private int _id;

    public int Id => _id;

#if UNITY_EDITOR
    [OnValueChanged(nameof(OnCustomIdentifierChanged))]
    public int CustomId;

    public bool IsChanged { get; set; }

    private void OnCustomIdentifierChanged()
    {
      IsChanged = true;
    }

    public void SetId(int id)
    {
      _id = id;
    }
#endif

    private void Reset()
    {
      HashSet<int> identifiers = FindObjectsByType<PersistenceIdentifier>(FindObjectsSortMode.None)
        .Except(new[] { this })
        .Select(x => x.Id)
        .ToHashSet();

      for (int i = 1; i < int.MaxValue; i++)
      {
        if (!identifiers.Contains(i))
        {
          _id = i;
          break;
        }
      }

      CustomId = _id;
    }
  }
}