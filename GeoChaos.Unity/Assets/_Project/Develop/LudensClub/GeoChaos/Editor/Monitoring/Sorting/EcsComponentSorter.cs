using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LudensClub.GeoChaos.Editor.Monitoring.Component;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Editor.Monitoring.Sorting
{
  public class EcsComponentSorter : IEcsComponentSorter
  {
    private readonly Dictionary<string, int> _componentOrders = new Dictionary<string, int>();

    public Comparison<IEcsComponentView> EcsComponentViewComparator { get; }

    public EcsComponentSorter()
    {
      List<OrderTuple> orders = Assembly.GetAssembly(typeof(IEcsComponent))
        .GetTypes()
        .Where(type => !type.IsAbstract)
        .Where(type => typeof(IEcsComponent).IsAssignableFrom(type))
        .Where(type => type.IsValueType)
        .Select(x => new AttributeTuple(x.GetCustomAttribute<EcsComponentOrderAttribute>(), x.Name))
        .Select(x => new OrderTuple(x.Attribute?.Order ?? EcsComponentOrder.TEMPORARY, x.Name))
        .ToList();

      orders.Sort(CompareOrders);
      for (var i = 0; i < orders.Count; i++)
      {
        _componentOrders[orders[i].Name] = i;
      }

      EcsComponentViewComparator = CompareComponents;
    }

    private int CompareComponents(IEcsComponentView left, IEcsComponentView right)
    {
      if (ReferenceEquals(left, right))
        return 0;
      if (ReferenceEquals(null, right))
        return 1;
      if (ReferenceEquals(null, left))
        return -1;

      int leftOrder = _componentOrders[left.Name];
      int rightOrder = _componentOrders[right.Name];
      return leftOrder.CompareTo(rightOrder);
    }

    private int CompareOrders(OrderTuple left, OrderTuple right)
    {
      int orderComparison = left.Order.CompareTo(right.Order);
      return orderComparison != 0 ? orderComparison : string.CompareOrdinal(left.Name, right.Name);
    }

    private struct AttributeTuple
    {
      public readonly EcsComponentOrderAttribute Attribute;
      public readonly string Name;

      public AttributeTuple(EcsComponentOrderAttribute attribute, string name)
      {
        Attribute = attribute;
        Name = name;
      }
    }

    private struct OrderTuple
    {
      public readonly int Order;
      public readonly string Name;

      public OrderTuple(int attribute, string name)
      {
        Order = attribute;
        Name = name;
      }
    }
  }
}