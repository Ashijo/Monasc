using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casern : Building {

    public List<Choice> Choices { get; private set; }

    public Casern(int team):base(BuildingType.Casern, team){

    }
	
}
