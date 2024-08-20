using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LudensClub.GeoChaos.Debugging.Monitoring;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging
{
  [CreateAssetMenu(menuName = CAC.Names.ECS_UNIVERSE_MENU, fileName = CAC.Names.ECS_UNIVERSE_FILE)]
  public class EcsUniverseConfig : ScriptableObject
  {
    public readonly EcsComponentComparer Comparer = new EcsComponentComparer();

    private readonly Comparison<StringTuple> _tupleComparer =
      (x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal);

    [OnValueChanged(TriConstants.ON + nameof(ComponentOrder) + TriConstants.CHANGED)]
    [ListDrawerSettings(AlwaysExpanded = true, HideAddButton = true, HideRemoveButton = true)]
    public List<StringTuple> ComponentOrder;

    private List<string> _componentNames;

    [Button("Update")]
    [PropertyOrder(0)]
    private void Update()
    {
      Comparer.Config = this;
      GetComponentNames();

      RemoveComponents();
      AddComponents();

      if (ComponentOrder.Count != _componentNames.Count)
        throw new IndexOutOfRangeException();
    }

    [Button("Sort")]
    [PropertyOrder(0)]
    private void Sort()
    {
      var lockedList = new List<StringTuple>();
      var constList = new List<StringTuple>();
      var tempList = new List<StringTuple>();
      foreach (StringTuple tuple in ComponentOrder)
      {
        switch (tuple.Status)
        {
          case StringStatus.Temp:
            tempList.Add(tuple);
            break;
          case StringStatus.Const:
            constList.Add(tuple);
            break;
          case StringStatus.Locked:
            lockedList.Add(tuple);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

      constList.Sort(_tupleComparer);
      tempList.Sort(_tupleComparer);

      ComponentOrder.Clear();
      ComponentOrder = lockedList.Concat(constList).Concat(tempList).ToList();
      Synchronize();
    }

    private void OnEnable()
    {
      Update();
    }

    private void GetComponentNames()
    {
      _componentNames = Assembly.GetAssembly(typeof(EditorContext))
        .GetTypes()
        .Where(type => !type.IsAbstract)
        .Where(type => typeof(IEcsComponent).IsAssignableFrom(type))
        .Where(type => type.IsValueType)
        .Select(x => x.Name)
        .OrderBy(x => x)
        .ToList();
    }

    private void OnComponentOrderChanged()
    {
      Synchronize();
    }

    private void Synchronize()
    {
      for (int i = 0; i < ComponentOrder.Count; i++)
      {
        ComponentOrder[i].Index = i;
        ComponentOrder[i].Index = i;
      }
    }

    private void AddComponents()
    {
      for (int i = 0; i < _componentNames.Count; i++)
      {
        if (null == ComponentOrder.Find(x => x.Name == _componentNames[i]))
        {
          ComponentOrder.Add(new StringTuple
          {
            Index = ComponentOrder.Count,
            Name = _componentNames[i],
            Config = this
          });
        }
      }
    }

    private void RemoveComponents()
    {
      for (int i = 0; i < ComponentOrder.Count; i++)
      {
        if (null == _componentNames.Find(x => x == ComponentOrder[i].Name))
        {
          ComponentOrder.RemoveAt(i--);
        }
      }
    }

    public enum StringStatus
    {
      Temp = 0,
      Const = 1,
      Locked = 2
    }

    [Serializable]
    [InlineProperty]
    [DeclareHorizontalGroup(nameof(StringTuple), Sizes = new float[] { 0, 50 })]
    [DeclareHorizontalGroup(nameof(StringTuple) + "/Buttons")]
    public class StringTuple
    {
      [GroupNext(nameof(StringTuple))]
      [HideLabel]
      [DisplayAsString]
      public string Name;

      [GroupNext(nameof(StringTuple))]
      [HideLabel]
      [OnValueChanged(TriConstants.ON + nameof(Index) + TriConstants.CHANGED)]
      [Dropdown(TriConstants.DROP + nameof(Index))]
      public int Index;

      [HideLabel]
      public StringStatus Status;

      [HideInInspector]
      public EcsUniverseConfig Config;

      public bool CanMoveNext => Index != (Config ? Config.ComponentOrder.Count : 0) - 1;
      public bool CanMovePrevious => Index != 0;

      private IEnumerable<string> DropName()
      {
        return Config._componentNames;
      }

      [GroupNext(nameof(StringTuple) + "/Buttons")]
      [Button("\u2193")]
      [EnableIf(nameof(CanMoveNext))]
      private void MoveNext()
      {
        Index++;
        OnIndexChanged();
      }

      [Button("\u2191")]
      [EnableIf(nameof(CanMovePrevious))]
      private void MovePrevious()
      {
        Index--;
        OnIndexChanged();
      }

      [Button("\u25bc")]
      private void MoveLast()
      {
        Index = Config.ComponentOrder.Count - 1;
        OnIndexChanged();
      }

      [Button("\u25b2")]
      private void MoveFirst()
      {
        Index = 0;
        OnIndexChanged();
      }

      private IEnumerable<int> DropIndex()
      {
        var array = new int[Config.ComponentOrder.Count];
        for (int i = 0; i < array.Length; i++)
        {
          array[i] = i;
        }

        return array;
      }

      private void OnIndexChanged()
      {
        Config.ComponentOrder.Remove(this);
        Config.ComponentOrder.Insert(Index, this);
        Config.Synchronize();
      }
    }

    public class EcsComponentComparer : IComparer<IEcsComponentView>
    {
      public EcsUniverseConfig Config;

      public int Compare(IEcsComponentView left, IEcsComponentView right)
      {
        if (ReferenceEquals(left, right))
          return 0;
        if (ReferenceEquals(null, right))
          return 1;
        if (ReferenceEquals(null, left))
          return -1;

        foreach (StringTuple tuple in Config.ComponentOrder)
        {
          bool isLeft = tuple.Name == left.Name;
          bool isRight = tuple.Name == right.Name;

          if (isRight && isLeft)
            return 0;

          if (isLeft)
            return -1;

          if (isRight)
            return 1;
        }

        return string.Compare(left.Name, right.Name, StringComparison.Ordinal);
      }
    }
  }
}