using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CaseStats)),CanEditMultipleObjects]
public class CaseStatsEd : Editor{
    private SerializedObject s_CaseStat;

    private SerializedProperty s_HasDeco;
    private SerializedProperty s_DecoSprite;

    [Tooltip("Required")]
    private SerializedProperty s_TileSprite;

    private SerializedProperty s_Walkable;
    private SerializedProperty s_BonusDef;
    private SerializedProperty s_MoveCost;

    private SerializedProperty s_HasEffect;
    private SerializedProperty s_LapsState;
    private SerializedProperty s_Power;
    private SerializedProperty s_effect;

    void OnEnable() {
        s_CaseStat      = new SerializedObject(target);

        s_TileSprite    = s_CaseStat.FindProperty("TileSprite");


        s_HasDeco       = s_CaseStat.FindProperty("HasDeco");
        s_DecoSprite    = s_CaseStat.FindProperty("DecoSprite");

        s_Walkable      = s_CaseStat.FindProperty("Walkable");
        s_BonusDef      = s_CaseStat.FindProperty("BonusDef");
        s_MoveCost      = s_CaseStat.FindProperty("MoveCost");

        s_HasEffect     = s_CaseStat.FindProperty("HasEffect");
        s_LapsState     = s_CaseStat.FindProperty("LapsState");
        s_Power         = s_CaseStat.FindProperty("Power");
        s_effect        = s_CaseStat.FindProperty("effect");


    }


    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        s_CaseStat.Update();

        EditorGUILayout.PropertyField(s_TileSprite);

        EditorGUILayout.PropertyField(s_HasDeco);

        if (s_HasDeco.boolValue) {
            EditorGUILayout.PropertyField(s_DecoSprite);
        }

        EditorGUILayout.PropertyField(s_Walkable);
        EditorGUILayout.PropertyField(s_BonusDef);
        EditorGUILayout.PropertyField(s_MoveCost);


        
        EditorGUILayout.PropertyField(s_HasEffect);

        if (s_HasEffect.boolValue) {
            EditorGUILayout.PropertyField(s_LapsState);
            EditorGUILayout.PropertyField(s_Power);
            EditorGUILayout.PropertyField(s_effect);
        }

        s_CaseStat.ApplyModifiedProperties();
    }
}
