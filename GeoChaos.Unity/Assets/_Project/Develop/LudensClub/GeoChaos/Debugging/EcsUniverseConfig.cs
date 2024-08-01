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
  [CreateAssetMenu(menuName = CAC.Names.ECS_UNIVERSE_CONFIG_MENU, fileName = CAC.Names.ECS_UNIVERSE_CONFIG_FILE)]
  public class EcsUniverseConfig : ScriptableObject
  {
    public readonly EcsComponentComparer Comparer = new EcsComponentComparer();

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
      List<StringTuple> sortedList = ComponentOrder.Where(x => !x.Locked).OrderBy(x => x.Name).ToList();
      List<StringTuple> lockedList = ComponentOrder.Where(x => x.Locked).ToList();
      ComponentOrder.Clear();
      ComponentOrder = lockedList.Concat(sortedList).ToList();
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

      [HideInInspector]
      public bool Locked;

      [HideInInspector]
      public EcsUniverseConfig Config;

      public bool CanMoveNext => Index != (Config ? Config.ComponentOrder.Count : 0) - 1;
      public bool CanMovePrevious => Index != 0;

      private IEnumerable<string> DropName()
      {
        return Config._componentNames;
      }

      public string LockedButtonName => Locked ? "\u25cf" : "\u21bb";

      [GroupNext(nameof(StringTuple) + "/Buttons")]
      [Button(ButtonSizes.Small, "$" + nameof(LockedButtonName))]
      private void Lock()
      {
        Locked = !Locked;
      }

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

    public class EcsComponentComparer : IComparer<EcsComponentView>
    {
      public EcsUniverseConfig Config;

      public int Compare(EcsComponentView left, EcsComponentView right)
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