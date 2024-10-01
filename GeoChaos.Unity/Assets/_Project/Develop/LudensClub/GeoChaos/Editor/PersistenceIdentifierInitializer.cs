using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LudensClub.GeoChaos.Runtime.Persistence;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using TypeCache = LudensClub.GeoChaos.Editor.General.TypeCache;

namespace LudensClub.GeoChaos.Editor
{
  [InitializeOnLoad]
  public static class PersistenceIdentifierValidator
  {
    private static readonly Dictionary<int, int> _identifierCounts = new Dictionary<int, int>();
    private static readonly List<PersistenceIdentifier> _duplicateIdentifiers = new List<PersistenceIdentifier>();
    private static readonly TypeCache _cache = new TypeCache();
    private static readonly FieldInfo _identifierField;
    
    private static PersistenceIdentifier[] _identifiers;

    static PersistenceIdentifierValidator()
    {
      _identifierField = _cache.GetCachedField(typeof(PersistenceIdentifier), "_identifier", true);
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
      _identifiers = Object.FindObjectsByType<PersistenceIdentifier>(FindObjectsInactive.Include, FindObjectsSortMode.None);

      CollectIdentifierCounts();
      CollectDuplicateIdentifiers();
      ChangeDuplicateIdentifiers();
    }

    private static void CollectIdentifierCounts()
    {
      _identifierCounts.Clear();
      foreach (PersistenceIdentifier identifier in _identifiers)
      {
        if (!_identifierCounts.TryAdd(identifier.Identifier, 1))
          _identifierCounts[identifier.Identifier]++;
      }
    }

    private static void CollectDuplicateIdentifiers()
    {
      _duplicateIdentifiers.Clear();
      foreach (KeyValuePair<int, int> pair in _identifierCounts.Where(x => x.Value > 1))
      {
        int i = pair.Value;
        while (i > 1)
        {
          PersistenceIdentifier identifier = _identifiers.Last(x => x.Identifier == pair.Key);
          _duplicateIdentifiers.Add(identifier);
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

        if (_identifiers.All(x => x.Identifier != i))
        {
          SetIdentifierWithLog(_duplicateIdentifiers[k++], i);
        }
      }
    }

    private static void SetIdentifierWithLog(PersistenceIdentifier identifier, int value)
    {
      int oldIdentifier = identifier.Identifier;
      SetIdentifier(identifier, value);
      Debug.LogWarning($"Persistence identifier of object {identifier.gameObject.name} was changed "
        + $"from {oldIdentifier} to {identifier.Identifier} in order to prevent duplicate identifiers.",
        identifier);
    }

    private static void SetIdentifier(PersistenceIdentifier identifier, int value)
    {
      _identifierField.SetValue(identifier, value);
      identifier.CustomIdentifier = value;
      EditorUtility.SetDirty(identifier);
    }
  }
}