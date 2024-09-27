using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LudensClub.GeoChaos.Runtime.Persistence;
using TriInspector.Editors;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using TypeCache = LudensClub.GeoChaos.Editor.General.TypeCache;

namespace LudensClub.GeoChaos.Editor
{
  [CustomEditor(typeof(PersistenceIdentifier), true)]
  public class PersistenceIdentifierDrawer : UnityEditor.Editor
  {
    private static readonly TypeCache _typeCache = new TypeCache();

    private readonly List<PersistenceIdentifier> _identifiers = new List<PersistenceIdentifier>();
    private readonly HashSet<PersistenceIdentifier> _hashedIdentifiers = new HashSet<PersistenceIdentifier>();

    private TriEditorCore _core;
    private bool _initialized;

    private FieldInfo _identifierField;
    private PersistenceIdentifier _value;

    private void OnEnable()
    {
      _identifierField = _typeCache.GetCachedField(typeof(PersistenceIdentifier), "_identifier", true);
      _core = new TriEditorCore(this);
      _initialized = false;
    }

    private void Initialize()
    {
      _value = (PersistenceIdentifier)serializedObject.targetObject;
      _value.CustomIdentifier = _value.Identifier;
      _initialized = true;
    }

    private void OnDisable()
    {
      _core?.Dispose();
    }

    public override void OnInspectorGUI()
    {
      UpdateGUI();
      _core.OnInspectorGUI();
    }

    public override VisualElement CreateInspectorGUI()
    {
      VisualElement root = new VisualElement();
      root.Add(new IMGUIContainer(UpdateGUI));
      root.Add(_core.CreateVisualElement());
      return root;
    }

    private void UpdateGUI()
    {
      if (!_initialized)
        Initialize();

      UpdateList();

      CheckIdentifier();

      if (_value.IsChanged)
      {
        if (!IsBadIdentifier(_value.CustomIdentifier))
        {
          _identifierField.SetValue(_value, _value.CustomIdentifier);
          Save();
        }

        _value.IsChanged = false;
      }
    }
      
    private void Save()
    {
      serializedObject.ApplyModifiedProperties();
      EditorUtility.SetDirty(serializedObject.targetObject);
    }

    private bool IsBadIdentifier(int id)
    {
      return id <= 0
        || _identifiers.Any(x => x != _value && x.Identifier == id);
    }

    private void CheckIdentifier()
    {
      if (IsBadIdentifier(_value.Identifier))
      {
        int id = _value.Identifier;
        RecalculateIdentifier();
        Debug.Log($"Recalculated identifier {id} to {_value.Identifier}", _value);
      }
    }

    private void RecalculateIdentifier()
    {
      for (int i = 1; i < int.MaxValue; i++)
      {
        if (_identifiers.All(x => x.Identifier != i))
        {
          _identifierField.SetValue(_value, i);
          _value.CustomIdentifier = i;
          Save();
          break;
        }
      }
    }

    private void UpdateList()
    {
      for (int i = 0; i < _identifiers.Count; i++)
      {
        if (!_identifiers[i])
        {
          _hashedIdentifiers.Remove(_identifiers[i]);
          _identifiers.RemoveAt(i);
        }
      }

      foreach (PersistenceIdentifier identifier in FindObjectsByType<PersistenceIdentifier>(FindObjectsSortMode.None))
      {
        if (_hashedIdentifiers.Add(identifier))
        {
          _identifiers.Add(identifier);
        }
      }
    }
  }
}