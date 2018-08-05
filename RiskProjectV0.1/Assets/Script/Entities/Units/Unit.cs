
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit {

    public int Team         { get; private set; }
    public GV.Units Type    { get; private set; }

    public int speed        { get; private set; }
    public int defense      { get; private set; }
    public int attack       { get; private set; }
    
    public int experience   { get; private set; }
    public int level        { get; private set; }
    public int expNextLvl   { get; private set; }

    public int maxHP        { get; private set; }
    public int actualHP     { get; private set; }
    bool isDead;
    
    public Vector2Int position { get; private set; }
    GameObject gameObject;

    public Unit( int team, GV.Units type,Vector3Int stats, Vector2Int position) {
        Team            = team;
        Type            = type;

        speed           = stats.x;
        defense         = stats.y;
        attack          = stats.z;

        experience      = 0;
        level           = 1;
        expNextLvl      = GetNextLvlUpXP();

        maxHP           = 10;
        actualHP        = 10;
        isDead          = false;

        this.position   = position;

        gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Units/"+Type.ToString())) as GameObject;

        gameObject.transform.parent = GameObject.Find("BattleField").transform;

        UpdatePosition();

    }

    public Unit(UnitData UD) {

        Team            = UD.Team;
        Type            = UD.Type;

        speed           = UD.Speed;
        defense         = UD.Defense;
        attack          = UD.Attack;

        experience      = UD.Experience;
        level           = UD.Level;
        expNextLvl      = UD.ExpNextLvl;

        maxHP           = UD.MaxHP;
        actualHP        = UD.ActualHP;
        isDead          = false;

        position        = UD.Position;

        gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Units/" + Type.ToString())) as GameObject;

        gameObject.transform.parent = BattlefieldManager.Instance.GetBattlefieldTransform();
        UpdatePosition();
    }

    public void Regenerate(UnitData UD) {
        Team            = UD.Team;
        Type            = UD.Type;


        speed           = UD.Speed;
        defense         = UD.Defense;
        attack          = UD.Attack;


        experience      = UD.Experience;

        maxHP           = UD.MaxHP;
        actualHP        = UD.ActualHP;
        isDead          = false;

        position        = UD.Position;

        gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Units/" + Type.ToString())) as GameObject;

        gameObject.transform.parent = GameObject.Find("BattleField").transform;
        UpdatePosition();
    }

    protected void UpdatePosition() {
        Vector3 pos = BattlefieldManager.Instance.GetLocalPos(position);
        pos.y = BattlefieldManager.Instance.GetHeight(position);

        gameObject.transform.localPosition = pos;
    }

    public void Move(Vector3 to) {
        gameObject.transform.localPosition = to;
        //UpdatePosition();
    }

    
    public bool IsDead() {
        return isDead;
    }



    public void Attack(Unit defender) {
        actualHP -= defender.Defend(this);
        //Debug.Log("attacker hp : "+actualHP);
        if (actualHP <= 0) {
            Killed();
        }
    }

    public int Defend(Unit attacker) {
        int returnedDmg;
        int damage = (int)Mathf.Round(attacker.attack + (attacker.actualHP / 2));

        //affect damage by strengh table
        damage += GV._StrenghTable[(int)attacker.Type, (int)Type];

        actualHP -= damage;

        if (actualHP > 0) {
            returnedDmg = (int)Mathf.Round(actualHP / 2) + defense;
        }
        else {
            Killed();
            returnedDmg = 0;
        }

        if (returnedDmg < 0) returnedDmg = 0;

        //Debug.Log("Defender hp : " + actualHP);

        return returnedDmg; 
    }

    private void Killed() {
        //TODO call dead anim
        GameObject.Destroy(gameObject);

        isDead = true;
    }

    public void Move(Vector2Int to) {
        position = to;
        UpdatePosition();
    }

    public UnitData GenerateUD() {
        return new UnitData(Team, Type, speed ,defense, attack, experience, level, expNextLvl, maxHP, actualHP,isDead, position); 
    }

    private int GetNextLvlUpXP() {
        int backer = 0;
        switch (level) {
            case 1:
                backer = 3;
                break;
            case 2:
                backer = 6;
                break;
            case 3:
                backer = 12;
                break;
            case 4:
                backer = -1;
                break;
            default:
                break;
        }

        if (backer==0) Debug.LogError("Unit should not have this level");
        return backer;
    }
}
