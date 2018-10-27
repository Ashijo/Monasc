using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Custom/L0_Micro/new tileStat", order = 35),Serializable]
public class CaseStats : ScriptableObject {
    private static int nbCaseStats;
    private static int id;

    [SerializeField] [HideInInspector] Sprite TileSprite;

    [SerializeField] [HideInInspector] bool HasDeco;
    [SerializeField] [HideInInspector] Sprite DecoSprite;

    [SerializeField] [HideInInspector] bool Walkable;
    [SerializeField] [HideInInspector] int BonusDef;
    [SerializeField] [HideInInspector] int MoveCost;


    [SerializeField] [HideInInspector] bool HasEffect;
    [SerializeField] [HideInInspector] GV.LapsState LapsState;
    [SerializeField] [HideInInspector] int Power;
    [SerializeField] [HideInInspector] GV.TileEffect effect;

    void Start() {
        id = nbCaseStats++;
    }

}