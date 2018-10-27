using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class StringSpritePair {
    [SerializeField] string Key;
    [SerializeField] Sprite Value;

    public StringSpritePair(string key, Sprite value)  {
        Key = key;
        Value = value;
    }

    public Sprite _value() {
        return Value;
    }

    public string _key() {
        return Key;
    }
}