﻿using System;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [Serializable]
  public class EcsConverterValue : IEcsConverter
  {
    [SerializeReference]
    [ShowIf(nameof(ShowSerializedConverter))]
    [LabelText("$" + nameof(_serializedConverter) + "Name")]
    private IEcsSerializedConverter _serializedConverter;

    [SerializeField]
    [ShowIf(nameof(ShowComponents))]
    [InlineProperty]
    [HideLabel]
    private EcsComponentsConverter _components;

    [SerializeField]
    [ShowIf(nameof(ShowScriptableConverter))]
    [InlineEditor]
    [HideLabel]
    [LabelText("$" + nameof(_scriptableConverter) + "Name")]
    private EcsConverterAsset _scriptableConverter;

    public bool IsEmpty => _serializedConverter == null
      && _components.Components.Count == 0
      && _scriptableConverter == null;

    public bool ShowSerializedConverter => IsEmpty || _serializedConverter != null;
    public bool ShowComponents => IsEmpty || _components.Components.Count > 0;
    public bool ShowScriptableConverter => IsEmpty || _scriptableConverter != null;

    public IEcsConverter GetValue()
    {
      if (_serializedConverter != null) return _serializedConverter;
      if (_components.Components.Count > 0) return _components;
      if (_scriptableConverter != null) return _scriptableConverter;

      return null;
    }

    public void Convert(EcsEntity entity)
    {
      IEcsConverter value = GetValue();
      value?.Convert(entity);
    }

#if UNITY_EDITOR
    private string _serializedConverterName => UnityEditor.ObjectNames.NicifyVariableName(_serializedConverter?.GetType().Name ?? TriConstants.NONE);
    private string _scriptableConverterName => UnityEditor.ObjectNames.NicifyVariableName(_scriptableConverter ? _scriptableConverter.name : "None");
#endif
  }
}