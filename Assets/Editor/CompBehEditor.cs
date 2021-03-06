﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CompositeBehavior))]
public class CompositeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CompositeBehavior cb = (CompositeBehavior)target;

        if (cb.behaviors == null || cb.behaviors.Length == 0)
        {
            EditorGUILayout.HelpBox("No Behaviors in array.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.LabelField("Behaviors", GUILayout.MinWidth(60f));
            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < cb.behaviors.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                cb.behaviors[i] = (FlockBehavior)EditorGUILayout.ObjectField(cb.behaviors[i], typeof(FlockBehavior), false, GUILayout.MinWidth(60f));
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(cb);
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Behavior"))
        {
            AddBehavior(cb);
            EditorUtility.SetDirty(cb);
        }
        EditorGUILayout.EndHorizontal();

        if (cb.behaviors != null && cb.behaviors.Length > 0)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove Behavior"))
            {
                RemoveBehavior(cb);
                EditorUtility.SetDirty(cb);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void AddBehavior(CompositeBehavior cb)
    {
        int oldCount = (cb.behaviors != null) ? cb.behaviors.Length : 0;
        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];
        for (int i = 0; i < oldCount; i++)
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }
    private void RemoveBehavior(CompositeBehavior cb)
    {
        int oldCount = cb.behaviors.Length;
        if (oldCount == 1)
        {
            cb.behaviors = null;
            cb.weights = null;
            return;
        }
        else
        {
            FlockBehavior[] newBehaviors = new FlockBehavior[oldCount - 1];
            float[] newWeights = new float[oldCount - 1];
            for (int i = 0; i < oldCount; i++)
            {
                newBehaviors[i] = cb.behaviors[i];
                newWeights[i] = cb.weights[i];
            }
            cb.behaviors = newBehaviors;
            cb.weights = newWeights;
        }
    }
}