using System;
using UnityEngine;

namespace CustomInspector.ShowIf {
  [AttributeUsage(AttributeTargets.Field)]
  public class ShowIf : PropertyAttribute {
    public ConditionFail Action     { get; }
    public Operator      Operator   { get; }
    public string[]      Conditions { get; }

    public ShowIf(
      ConditionFail   action,
      Operator        @operator,
      params string[] conditions
    ) {
      Action     = action;
      Operator   = @operator;
      Conditions = conditions;
    }
  }
}