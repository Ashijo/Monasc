using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Utils {

    #region Singleton
    private static Utils instance;



    public static Utils Instance {
        get {
            if (instance == null) {
                instance = new Utils();
            }
            return instance;
        }
    }

    #endregion


    /// <summary>
    /// Select a random element of enum
    /// </summary>
    /// <typeparam name="T">Type of the enum</typeparam>
    /// <param name="beginAt">element gonna be randomly selected betwin begin_at (default 0) and the last element</param>
    /// <returns></returns>
    public T GetRandomEnum<T>(int beginAt = 0) where T : struct, IConvertible {
        return GetRandomElementFromArray<T>(GetArrayOfEnum<T>(), beginAt);
    }

    /// <summary>
    /// Gonna return the enum equal to the string
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="name">Need to be exactly the same than enum.ToString()</param>
    /// <returns>Random if not found</returns>
    public T StringToEnum<T>(string name) where T : struct, IConvertible {
        string[] strArray = GetArrayOfEnumStr<T>();
        T[] enumArray = GetArrayOfEnum<T>();
        T backer;

        for (int i = 0; i < strArray.Length; ++i) {
            if (name == strArray[i]) {
                backer = enumArray[i];
                return backer;
            }
        }
        

        Debug.LogError("String not found");
        return default(T);
    }

    public T[] GetArrayOfEnum<T>() where T : struct, IConvertible {
        System.Type tType = typeof(T);

        if (tType.IsEnum) {
            return Enum.GetValues(tType).Cast<T>().ToArray();
        }
        else {
            Debug.LogError(tType.ToString() + " is not an enum");
            return null;
        }
    }

    public string[] GetArrayOfEnumStr<T>() where T : struct, IConvertible {
        T[] enums = GetArrayOfEnum<T>();
        string[] backer = new string[enums.Length];
        for (int i =0; i<enums.Length; i++) {
            backer[i] = enums[i].ToString();
        }
        return backer;
    }

    public T GetRandomElementFromArray<T>(T[] arr, int beginAt = 0) {
        if (arr != null && arr.Length > 0) {
            return arr[UnityEngine.Random.Range(beginAt, arr.Length)];
        }
        else {
            Debug.LogError("Failed array is null or empty");
            return default(T);
        }


    }

}
