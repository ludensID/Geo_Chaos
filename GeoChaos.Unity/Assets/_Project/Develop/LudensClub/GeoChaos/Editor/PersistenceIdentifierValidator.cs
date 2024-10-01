using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Persistence;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudensClub.GeoChaos.Editor
{
  [InitializeOnLoad]
  public static class PersistenceIdentifierValidator
  {
    private static readonly Dictionary<int, int> _identifierCounts = new Dictionary<int, int>();
    private static readonly List<PersistenceIdentifier> _duplicateIdentifiers = new List<PersistenceIdentifier>();
    private static readonly List<PersistenceIdentifier> _tempIdentifiers = new List<PersistenceIdentifier>();

    private static PersistenceIdentifier[] _identifiers;

    static PersistenceIdentifierValidator()
    {
      EditorSceneManager.sceneOpened += ValidateIdentifiers;
      SceneManager.sceneLoaded += ValidateIdentifiers;
      ValidateIdentifiersInternal();
    }

    private static void ValidateIdentifiers(Scene scene, LoadSceneMode mode)
    {
      ValidateIdentifiersInternal();
    }

    private static void ValidateIdentifiers(Scene scene, OpenSceneMode mode)
    {
      ValidateIdentifiersInternal();
    }

    private static void ValidateIdentifiersInternal()
    {
      _identifiers =
        Object.FindObjectsByType<PersistenceIdentifier>(FindObjectsInactive.Include, FindObjectsSortMode.None);

      CollectIdentifierCounts();
      CollectDuplicateIdentifiers();
      ChangeDuplicateIdentifiers();
    }

    private static void CollectIdentifierCounts()
    {
      _identifierCounts.Clear();
      foreach (PersistenceIdentifier identifier in _identifiers)
      {
        if (!_identifierCounts.TryAdd(identifier.Id, 1))
          _identifierCounts[identifier.Id]++;
      }
    }

    private static void CollectDuplicateIdentifiers()
    {
      _tempIdentifiers.Clear();
      foreach (PersistenceIdentifier identifier in _identifiers)
        _tempIdentifiers.Add(identifier);

      _duplicateIdentifiers.Clear();
      foreach (KeyValuePair<int, int> pair in _identifierCounts)
      {
        int i = pair.Value;
        int end = pair.Key > 0 ? 1 : 0;
        while (i > end)
        {
          PersistenceIdentifier identifier = _tempIdentifiers.Last(x => x.Id == pair.Key);
          _duplicateIdentifiers.Add(identifier);
          _tempIdentifiers.Remove(identifier);
          i--;
        }
      }
    }

    private static void ChangeDuplicateIdentifiers()
    {
      int k = 0;
      for (int i = 1; i < int.MaxValue; i++)
      {
        if (k >= _duplicateIdentifiers.Count)
          break;

        if (_identifiers.All(x => x.Id != i))
        {
          SetIdentifierWithLog(_duplicateIdentifiers[k++], i);
        }
      }
    }

    private static void SetIdentifierWithLog(PersistenceIdentifier identifier, int value)
    {
      int oldIdentifier = identifier.Id;
      SetIdentifier(identifier, value);
      Debug.LogWarning($"Persistence identifier of object {identifier.gameObject.name} was changed "
        + $"from {oldIdentifier} to {identifier.Id} in order to prevent duplicate identifiers.",
        identifier);
    }

    private static void SetIdentifier(PersistenceIdentifier identifier, int value)
    {
      identifier.SetId(value);
      identifier.CustomId = value;
      EditorUtility.SetDirty(identifier);
    }
  }
}