using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CustomInspector.ShowIf.Editor {
  [CustomPropertyDrawer(typeof(ShowIf), true)]
  public class ShowIfAttributeDrawer : PropertyDrawer {
    private bool MeetsConditions(SerializedProperty property) {
      var    showIfAttribute = attribute as ShowIf;
      Object target          = property.serializedObject.targetObject;
      var    conditionValues = new List<bool>();

      if (showIfAttribute != null) {
        foreach (string condition in showIfAttribute.Conditions) {
          FieldInfo conditionField = GetField(target, condition);
          if (conditionField != null && conditionField.FieldType == typeof(bool))
            conditionValues.Add((bool) conditionField.GetValue(target));

          MethodInfo conditionMethod = GetMethod(target, condition);
          if (conditionMethod                        != null
           && conditionMethod.ReturnType             == typeof(bool)
           && conditionMethod.GetParameters().Length == 0)
            conditionValues.Add((bool) conditionMethod.Invoke(target, null));
        }

        if (conditionValues.Count > 0)
          return showIfAttribute.Operator == Operator.And
            ? conditionValues.Aggregate(true,  (current, value) => current && value)
            : conditionValues.Aggregate(false, (current, value) => current || value);
      }

      Debug.LogError("Invalid boolean condition fields or methods used!");
      return true;
    }

    public override float GetPropertyHeight(
      SerializedProperty property,
      GUIContent
        label
    ) {
      // Calcluate the property height, if we don't meet the condition and the draw mode is DontDraw, then height will be 0.
      bool meetsCondition  = MeetsConditions(property);
      var  showIfAttribute = attribute as ShowIf;

      if (showIfAttribute != null && !meetsCondition && showIfAttribute.Action == ConditionFail.DontDraw)
        return 0;

      return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      bool meetsCondition = MeetsConditions(property);

      // Early out, if conditions met, draw and go.
      if (meetsCondition) {
        EditorGUI.PropertyField(position, property, label, true);
        return;
      }

      if (attribute is not ShowIf showIfAttribute)
        return;

      switch (showIfAttribute.Action) {
        case ConditionFail.DontDraw:
          return;
        case ConditionFail.JustDisable:
          EditorGUI.BeginDisabledGroup(true);
          EditorGUI.PropertyField(position, property, label, true);
          EditorGUI.EndDisabledGroup();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }


    #region Reflection helpers.

    private static MethodInfo GetMethod(object target, string methodName) {
      return GetAllMethods(
          target,
          m => m.Name.Equals(
            methodName,
            StringComparison.InvariantCulture
          )
        )
       .FirstOrDefault();
    }

    private static FieldInfo GetField(object target, string fieldName) {
      return GetAllFields(
          target,
          f => f.Name.Equals(
            fieldName,
            StringComparison.InvariantCulture
          )
        )
       .FirstOrDefault();
    }

    private static IEnumerable<FieldInfo> GetAllFields(
      object target,
      Func<FieldInfo,
        bool> predicate
    ) {
      var types = new List<Type> { target.GetType() };

      while (types.Last().BaseType != null)
        types.Add(types.Last().BaseType);

      for (int i = types.Count - 1; i >= 0; i--) {
        IEnumerable<FieldInfo> fieldInfos = types[i]
                                           .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                           .Where(predicate);

        foreach (FieldInfo fieldInfo in fieldInfos)
          yield return fieldInfo;
      }
    }

    private static IEnumerable<MethodInfo> GetAllMethods(
      object                 target,
      Func<MethodInfo, bool> predicate
    ) {
      IEnumerable<MethodInfo> methodInfos = target.GetType()
                                                  .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                  .Where(predicate);

      return methodInfos;
    }

    #endregion
  }
}