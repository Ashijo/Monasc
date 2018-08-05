using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData {
    public UnitData(int team, GV.Units type, int speed, int defense, int attack, int experience, int level, int expNextLvl, int maxHP, int actualHP, bool isDead, Vector2Int position) {
        Id = NbUnit++;
        Type = type;
        Team = team;
        Speed = speed;
        Defense = defense;
        Attack = attack;
        Experience = experience;
        Level = level;
        ExpNextLvl = ExpNextLvl;
        MaxHP = maxHP;
        ActualHP = actualHP;
        IsDead = isDead;
        Position = position;
    }

    public UnitData(int team, GV.Units type, int speed, int defense, int attack, int experience, int level, int expNextLvl, int maxHP, int actualHP, bool isDead) {
        Id = NbUnit++;
        Type = type;
        Team = team;
        Speed = speed;
        Defense = defense;
        Attack = attack;
        Experience = experience;
        Level = level;
        ExpNextLvl = ExpNextLvl;
        MaxHP = maxHP;
        ActualHP = actualHP;
        IsDead = isDead;
        Position = new Vector2Int(1,1);
    }

    public void UpdatePos(Vector2Int position) {
        Position = position;
    }

    private static int NbUnit = 0;
    public  int Id    { get; private set; }

    public GV.Units Type        { get; private set; }

    public int Team             { get; private set; }

    public int Speed            { get; private set; }
    public int Defense          { get; private set; }
    public int Attack           { get; private set; }


    public int Experience       { get; private set; }
    public int Level            { get; private set; }
    public int ExpNextLvl       { get; private set; }


    public int MaxHP            { get; private set; }
    public int ActualHP         { get; private set; }
    public bool IsDead          { get; private set; }
    
    public Vector2Int Position  { get; private set; }

}