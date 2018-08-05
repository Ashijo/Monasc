using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice {

	public string Name { get; private set; }
    private toCall Handler;

    public Choice(string name, toCall handler) {
        Name = name;
        Handler = handler;  
    }

    public delegate void toCall();

    public void Call() {
        Handler.Invoke();
    }
}
