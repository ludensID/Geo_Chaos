using System;
using System.Collections.Generic;
using System.Reflection;

namespace LudensClub.GeoChaos.Editor.General
{
  public class MemberCache<TMember> where TMember : MemberInfo
  {
    private readonly Func<Type, string, BindingFlags, MemberInfo> _getter;
    private readonly Dictionary<(Type, string), TMember> _members = new Dictionary<(Type, string), TMember>();
    private readonly string _name = typeof(TMember).Name;

    private static BindingFlags GetFlags(bool isPrivate = false, bool isStatic = false)
    {
      return (isPrivate ? BindingFlags.NonPublic : BindingFlags.Public) 
        | (isStatic ? BindingFlags.Static : BindingFlags.Instance);
    }

    public MemberCache()
    {
      _getter = _name switch
      {
        nameof(FieldInfo) => GetField,
        nameof(PropertyInfo) => GetProperty,
        nameof(MethodInfo) => GetMethod,
        _ => throw new ArgumentOutOfRangeException()
      };
    }

    public TMember GetMember(Type type, string name, bool isPrivate = false, bool isStatic = false)
    {
      if (!_members.TryGetValue((type, name), out TMember member))
      {
        member = GetMemberInternal(type, name, GetFlags(isPrivate, isStatic));
        _members.Add((type, name), member);
      }

      return member;
    }

    private TMember GetMemberInternal(Type type, string name, BindingFlags flags)
    {
      MemberInfo member = _getter.Invoke(type, name, flags);
      if (member == null)
        throw new NullReferenceException($"{type.Name}, {name}, {flags}");
      if (member is not TMember value)
        throw new ArgumentException($"{type.Name}, {name}, {flags}");
      
      
      return value;
    }

    private MemberInfo GetMethod(Type type, string s, BindingFlags flags)
    {
      return type.GetMethod(s, flags);
    }

    private MemberInfo GetProperty(Type type, string s, BindingFlags flags)
    {
      return type.GetProperty(s, flags);
    }

    private MemberInfo GetField(Type type, string s, BindingFlags flags)
    {
      return type.GetField(s, flags);
    }
  }
}