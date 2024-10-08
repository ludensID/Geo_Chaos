using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.Monitoring.Component
{
  [Serializable]
  public class EcsComponentView
  {
    [LabelText("$" + nameof(ValueName))]
    [SerializeReference]
    public IEcsComponent Value;

    private string ValueName => ObjectNames.NicifyVariableName(Value?.GetType().Name ?? "[None]");
  }

  public interface IEcsComponentView
  {
    string Name { get; set; }
    bool HasValue { get; set; }
    int Entity { get; set; }
    IEcsPool Pool { get; }
    void SetPool(IEcsPool pool);
    void Update();
    void AssignComponent();
  }

  [Serializable]
  [InlineProperty]
  public class EcsComponentView<TComponent> : IEcsComponentView
    where TComponent : struct, IEcsComponent
  {
    [LabelText("$" + nameof(_valueName))]
    [SerializeReference]
    [HideReferencePicker]
    [OnValueChanged(nameof(OnValueChanged))]
    public IEcsComponent Value;

    [HideInInspector]
    public TComponent Component;
    
    private EcsPool<TComponent> _pool;
    private readonly Type _componentType;
    private readonly string _valueName;

    public string Name { get; set; }
    public bool HasValue { get; set; }
    public int Entity { get; set; }
    public IEcsPool Pool => _pool;

    public EcsComponentView()
    {
      _componentType = typeof(TComponent);
      _valueName = ObjectNames.NicifyVariableName(_componentType.Name);
    }

    public void SetPool(IEcsPool pool)
    {
      if (pool is not EcsPool<TComponent> implicitPool)
        throw new ArgumentException();

      _pool = implicitPool;
    }

    public void Update()
    {
      HasValue = _pool.Has(Entity);
      if (HasValue)
        Component = _pool.Get(Entity);
    }

    public void AssignComponent()
    {
      Value = Component;
    }

    private void OnValueChanged()
    {
      Component = (TComponent)Value;
      _pool.Get(Entity) = Component;
    }
  }
}