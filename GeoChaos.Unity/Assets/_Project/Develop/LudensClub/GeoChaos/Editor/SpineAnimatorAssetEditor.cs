using System;
using LudensClub.GeoChaos.Runtime.Infrastructure.Spine;
using TriInspector.Editors;
using UnityEditor;
using UnityEngine.UIElements;

namespace LudensClub.GeoChaos.Editor
{
  [CanEditMultipleObjects]
  [CustomEditor(typeof(SpineAnimatorAsset), true)]
  public class SpineAnimatorAssetEditor : UnityEditor.Editor
  {
    private TriEditorCore _core;
    private bool _initialized;

    private SpineAnimatorAsset _value;


    private void OnEnable()
    {
      _core = new TriEditorCore(this);
      _initialized = false;
    }

    private void Initialize()
    {
      _value = (SpineAnimatorAsset)serializedObject.targetObject;
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
      VisualElement container = _core.CreateVisualElement();
      container.Add(new IMGUIContainer(UpdateGUI));
      
      return container;
    }

    private void UpdateGUI()
    {
      if (!_initialized)
        Initialize();

      EditorGUI.BeginChangeCheck();

      UpdateProcessors();

      EditorGUI.EndChangeCheck();
    }

    private void UpdateProcessors()
    {
      foreach (SpineTransition transition in _value.Transitions)
      {
        foreach (SpineCondition condition in transition.Conditions)
        {
          SpineParameter parameter = _value.Parameters.Find(x => x.Name == condition.Parameter);
          ISpineProcessor processor =
            parameter != null ? GetProcessor(parameter.VariableType) : new EmptySpineProcessor();

          if (condition.Processor == null || condition.WasChanged
            || condition.Processor.GetType() != processor.GetType())
          {
            condition.Processor = processor;
            condition.WasChanged = false;
          }
        }
      }
    }

    private static ISpineProcessor GetProcessor(SpineVariableType type)
    {
      return type switch
      {
        SpineVariableType.Trigger => new SpineTriggerProcessor(),
        SpineVariableType.Bool => new SpineBoolProcessor(),
        SpineVariableType.Integer => new SpineIntegerProcessor(),
        SpineVariableType.Float => new SpineFloatProcessor(),
        _ => throw new ArgumentException()
      };
    }
  }
}