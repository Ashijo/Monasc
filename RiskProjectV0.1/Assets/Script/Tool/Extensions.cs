
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    /// <summary>
    /// return true if the difference between a & b < epsilon
    /// </summary>
    /// <param name="a">first number</param>
    /// <param name="b">number to check</param>
    /// <param name="epsilon">the accept difference between a & b</param>
    /// <returns></returns>
    public static bool Near(this float a, float b, float epsilon = .1f) {
        return (a + epsilon > b && a - epsilon < b);
    }

    /// <summary>
    /// Return true if b.x, b.y and b.z are betwen + epsilon and - epsilon of a.x, a.y, and a.z
    /// </summary>
    /// <param name="b">compared to a</param>
    /// <param name="epsilon">default : .1</param>
    /// <returns></returns>
    public static bool Near(this Vector3 a, Vector3 b, float epsilon = .1f) {
        return !((a.x + epsilon < b.x || a.x - epsilon > b.x)
            || (a.y + epsilon < b.y || a.y - epsilon > b.y)
            || (a.z + epsilon < b.z || a.z - epsilon > b.z));
    }

    /// <summary>
    /// Swap the object in pos A with the object in pos B
    /// </summary>
    public static List<T> Swap<T>(this List<T> toSwap, int a, int b)  {
        T aValue = toSwap[a];
        toSwap[a] = toSwap[b];
        toSwap[b] = aValue;
        return toSwap;
    }

    /// <summary>
    /// Will return a string with the position and the ToString of the object
    /// </summary>
    public static string ToString<T>(this List<T> list) {
        string backer = "(";
        for (int i = 0; i<list.Count; ++i) {
            backer += " n" + i + " - v" + list[i];
            if (i != list.Count - 1) backer += ",";
        }
        backer += ")";
        return backer;
    } 

    /// <summary>
    /// Sadly i cannot keep this one in a library, or, 
    /// i must link this class with my utils one :/
    /// Soon change to fit with a tupple system
    /// </summary>
    /// <typeparam name="K">Finally they would not be use</typeparam>
    /// <typeparam name="V">but i need this info here anyways</typeparam>
    /// <param name="list">the list to check</param>
    /// <returns>The number of KVPaire without any null elements</returns>
    public static int GetCountNotNull<K, V>(this List<Utils.KVPaires<K, V>> list) {
        int count = 0;
        for (int i = 0; i < list.Count; i++) {
            if (list[i].Key != null && list[i].Value != null) count++;
        }
        return count;
    }

    /// <summary>
    /// Simple cycled Clamp, instead of % will return 0 if > max
    /// </summary>
    /// <param name="max">well, the max I guess</param>
    /// <returns>if the value is > max ret 0</returns>
    public static int Clamp(this int a, int max) {
        if (a < 0) a = max;
        else if (a > max) a = 0;
        return a;
    }

    public static int Clamp(this int a, int min ,int max) {
        if (a < min) a = max;
        else if (a > max) a = min;
        return a;
    }

    public static Stack<T> ToStack<T>(this List<T> ToStack) {
        Stack<T> stack = new Stack<T>();
        foreach (T t in ToStack) stack.Push(t);
        return stack;
    }
}
