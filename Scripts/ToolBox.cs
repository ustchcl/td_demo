using QFramework;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hong;

public class ToolBox : Singleton<ToolBox> {
    private ToolBox() {}

    public float Clamp(float min, float max, float value)
    {
        return Mathf.Min(max, Mathf.Max(value, min));
    }


    public bool InRange(int value, double min, double max) {
        return value <= max && value >= min;
    }

    public bool InRange(float value, double min, double max) {
        return value <= max && value >= min;
    }

    public bool InRange(double value, double min, double max) {
        return value <= max && value >= min;
    }
    

    public void Swap<T>(T[] arr, int fromIndex, int toIndex) {
        if (!InRange(fromIndex, 0, arr.Length) || !InRange(toIndex, 0, arr.Length)) {
            return;
        }

        var temp = arr[fromIndex];
        arr[fromIndex] = arr[toIndex];
        arr[toIndex] = temp; 
    }

    public int Count<T>(T[] arr, Func<T, bool> predicator) {
        var result = 0;
        arr.ForEach(t => {
            if (predicator(t)) { result += 1; }
        });
        return result;
    }

    public int FindIndex<T>(T[] arr, Predicate<T> predicator) {
        if (arr == null) {
            return -1;
        }
        int result = -1;
        for (int i = 0; i < arr.Length; ++i) {
            if (predicator(arr[i])) {
                result = i;
                break;
            }
        }
        return result;
    }


    public Maybe<T> FirstWhere<T>(T[] selfObj, Predicate<T> predicate)
	{
        var index = FindIndex<T>(selfObj, predicate);
        if (index == -1) {
            return new Maybe<T>();
        } else {
            return new Maybe<T>(selfObj[index]);
        }
    }

    // 数值量级转化
    private String Tsg3(double d) => d.ToString("G3");
    public string FormatNum(double d) {
        if (d < 1e4) {
            return Tsg3(d);
        } else if (d < 1e7) {
            return Tsg3(d / 1e3) + "K";
        } else if (d < 1e10) {
            return Tsg3(d / 1e6) + "M";
        } else if (d < 1e13) {
            return Tsg3(d / 1e9) + "G";
        } else if (d < 1e16) {
            return Tsg3(d / 1e12) + "T";
        } else if (d < 1e19) {
            return Tsg3(d / 1e15) + "P";
        } else if (d < 1e22) {
            return Tsg3(d / 1e18) + "E";
        } else if (d < 1e25) {
            return Tsg3(d / 1e21) + "Z";
        } else if (d < 1e28) {
            return Tsg3(d / 1e24) + "Y";
        } else if (d < 1e31) {
            return Tsg3(d / 1e27) + "B";
        } else {
            return Tsg3(d / 1e30) + "N";
        }
    }

    public int Min(int a, int b) => a < b ? a : b;
    public float Min(float a, float b) => a < b ? a : b;
    public double Min(double a, double b) => a < b ? a : b;
    public int Max(int a, int b) => a > b ? a : b;
    public float Max(float a, float b) => a > b ? a : b;
    public double Max(double a, double b) => a > b ? a : b;


    public T GetPropertyValue<T>(object obj, string propName)
    {
        var type = obj.GetType();
        var p = type.GetField(propName);
        return (T)p.GetValue(obj);
    }

    public Func<object, T> Prop<T>(string propName) => (object obj) => GetPropertyValue<T>(obj, propName);

    public Predicate<T> Equals<T>(T t1) => (T t2) => t1.Equals(t2);

    public int Inc(int i) => i + 1;
    public int Dec(int i) => i - 1;

    public Func<List<T>, List<U>> Lift<T, U>(Func<T, U> f) => (List<T> lt) =>
    {
        var result = new List<U>();
        foreach (T t in lt)
        {
            result.Add(f(t));
        }
        return result;
    };

    public B Fold<A, B>(List<A> list, Func<B, A, B> f, B initial)
    {
        B result = initial;
        list.ForEach(a => result = f(result, a));
        return result;
    }


    public List<A> Where<A>(List<A> list, Predicate<A> predicator)
    {
        List<A> result = new List<A>();

        list.ForEach(a => {
            if (predicator(a)) result.Add(a);
        });
        return result;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns>[start, end)</returns>
    public List<int> Range(int start, int end)
    {
        var step = start > end ? -1 : 1;
        var result = new List<int>(new int[step * (end - start)]);
        var i = start;
        var n = 0;
        while (i != end)
        {
            result[n++] = i;
            i += step;
        }
        return result;
    }


    public Vector3 ScrrenToGroundPoint(float y) {
        var mousePos = Input.mousePosition;
        mousePos.z = 1;
        var m = Camera.main.transform.position;
        var vl = (Camera.main.ScreenToWorldPoint(mousePos) - m).normalized;
        var n = new Vector3(0f, y, 0f);
        var vp = new Vector3(0f, 1f, 0f);
        var t = Vector3.Dot((n - m ), vp) / Vector3.Dot(vp, vl);
        return  m + Vector3.Scale(vl, new Vector3(t, t, t));
    }
}