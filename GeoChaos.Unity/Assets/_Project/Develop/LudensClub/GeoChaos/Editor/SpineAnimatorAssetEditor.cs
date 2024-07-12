using System;
using System.Collections;
using System.Reflection;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Infrastructure.Spine;
using TriInspector.Editors;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LudensClub.GeoChaos.Editor
{
  [CanEditMultipleObjects]
  [CustomEditor(typeof(SpineAnimatorAsset<,>), true)]
  public class SpineAnimatorAssetEditor : UnityEditor.Editor
  {
    private TriEditorCore _core;
    private bool _initialized;

    private Object _value;
    private Type _valueType;

    private IEnumerable _parameters;
    private Type _parameterType;
    private FieldInfo _parameterIdField;
    private FieldInfo _variableTypeField;
    private FieldInfo _variableField;
    private FieldInfo _isTriggerField;

    private IEnumerable _transitions;
    private Type _transitionType;
    private FieldInfo _conditionsField;
    private Type _conditionsType;
    private MethodInfo _conditionsClearMethod;
    private Type _conditionType;
    private FieldInfo _parameterTypeField;
    private FieldInfo _processorField;
    private ITypeCache _cache;

    private void OnEnable()
    {
      _cache = EditorContext.Container.TypeCache;
      _core = new TriEditorCore(this);
      _initialized = false;
    }

    private void Initialize()
    {
      _value = serializedObject.targetObject;
      _valueType = _value.GetType();

      _parameters = (IEnumerable)_cache.GetCachedField(_valueType, "Parameters")!.GetValue(_value);

      _parameterType = _parameters.GetType().GetGenericArguments()[0];
      _parameterIdField = _cache.GetCachedField(_parameterType, "_id", true);
      _variableTypeField = _cache.GetCachedField(_parameterType, "_variableType", true);
      _variableField = _cache.GetCachedField(_parameterType, "_variable", true);
      _isTriggerField = _cache.GetCachedField(_parameterType, "_isTrigger", true);

      _transitions = (IEnumerable)_cache.GetCachedField(_valueType, "Transitions")!.GetValue(_value);
      _transitionType = _transitions.GetType().GetGenericArguments()[0];
      _conditionsField = _cache.GetCachedField(_transitionType, "_conditions", true);
      _conditionsType = _conditionsField.FieldType;
      _conditionsClearMethod = _cache.GetCachedMethod(_conditionsType, "Clear");
      _conditionType = _conditionsType.GetGenericArguments()[0];
      _parameterTypeField = _cache.GetCachedField(_conditionType, "Parameter");
      _processorField = _cache.GetCachedField(_conditionType, "Processor");

      _initialized = true;
    }

    private void OnDisable()
    {
      _core?.Dispose();
    }

    public override void OnInspectorGUI()
    {
      if (!_initialized)
        Initialize();

      EditorGUI.BeginChangeCheck();

      UpdateVariables();
      UpdateProcessors();

      EditorGUI.EndChangeCheck();


      _core.OnInspectorGUI();
    }

    private void UpdateVariables()
    {
      foreach (object parameter in _parameters)
      {
        var type = (SpineVariableType)_variableTypeField.GetValue(parameter);

        ISpineVariable variable = type switch
        {
          SpineVariableType.Trigger => new SpineVariable<bool>(),
          SpineVariableType.Bool => new SpineVariable<bool>(),
          SpineVariableType.Integer => new SpineVariable<int>(),
          SpineVariableType.Float => new SpineVariable<float>(),
          _ => throw new ArgumentOutOfRangeException()
        };

        object instanceVariable = _variableField.GetValue(parameter);

        if (instanceVariable == null || instanceVariable.GetType() != variable.GetType())
          _variableField.SetValue(parameter, variable);

        var trigger = (bool)_isTriggerField.GetValue(parameter);
        if (type == SpineVariableType.Trigger || trigger)
          _isTriggerField.SetValue(parameter, type == SpineVariableType.Trigger);
      }
    }

    private void UpdateProcessors()
    {
      foreach (object transition in _transitions)
      {
        var conditions = (IEnumerable)_conditionsField.GetValue(transition);
        bool needClear = false;
        foreach (object cond in conditions)
        {
          object value = _parameterTypeField.GetValue(cond);

          var index = FindParameter(value, out object parameter);
          if (index == -1)
          {
            needClear = true;
          }
          else if (index == 0)
          {
            Debug.LogError($"Parameter {value} is not found");
            Undo.PerformUndo();
          }
          else if (index == 1)
          {
            GetParameterInfo(parameter, out _, out Type generic, out bool trigger);
            ISpineProcessor proc = GetProcessor(generic, trigger);

            object procValue = _processorField.GetValue(cond);
            if (procValue == null || procValue.GetType() != proc.GetType())
            {
              _processorField.SetValue(cond, proc);
            }
          }
        }

        if (needClear)
        {
          Debug.LogError("Parameters not found");
          _conditionsClearMethod.Invoke(conditions, null);
        }
      }
    }

    private void GetParameterInfo(object parameter, out object id, out Type generic, out bool trigger)
    {
      id = _parameterIdField.GetValue(parameter);
      object variable = _variableField.GetValue(parameter);
      generic = variable?.GetType().GetGenericArguments()[0];
      trigger = (bool)_isTriggerField.GetValue(parameter);
    }

    private int FindParameter(object value, out object parameter)
    {
      parameter = null;
      if (_parameters == null)
        return -1;

      var count = 0;
      foreach (object par in _parameters)
      {
        count++;
        parameter = par;
        GetParameterInfo(parameter, out object id, out _, out _);
        bool equals = id.Equals(value);
        if (equals)
          return 1;
      }

      return count > 0 ? 0 : -1;
    }

    private static ISpineProcessor GetProcessor(Type generic, bool trigger)
    {
      return generic!.Name switch
      {
        nameof(Boolean) when trigger => new SpineTriggerProcessor(),
        nameof(Boolean) => new SpineBoolProcessor(),
        nameof(Int32) => new SpineNumberProcessor<int>(),
        nameof(Single) => new SpineNumberProcessor<float>(),
        _ => throw new ArgumentException()
      };
    }
  }
}