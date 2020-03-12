using System;
using System.Collections.Generic;
using QFramework;

public class EjectorCarry
{
    private string sourceJson = "";
    public List<List<(int, int)>> enemyInfoList;

    string ToString()
    {
        string result = "[";
        foreach(var ps in enemyInfoList)
        {
            result += "[";
            foreach (var p in ps)
            {
                result += "(" + p.Item1 + "," + p.Item2 + ")";
            }
            result += "],";
        }
        result += "]";
        return result;

    }

    public void Reset() {
        Init(sourceJson);
    }   

    public EjectorCarry(string json)
    {
        sourceJson = json;
        Init(sourceJson);
    }

    void Init(string json) {
        enemyInfoList = new List<List<(int, int)>>();
        // 1. ȥ���ո�
        json = json.Replace(" ", "");
        if (json.StartsWith("[{") && json.EndsWith("}]"))
        {
            // 2. ȥ���������
            json = json.Substring(2, json.Length - 4);
            // 3. �ָ�
            var arr = new List<string>(json.Split(new string[]{"},{"}, StringSplitOptions.None));
            // 4. ÿ����������   "key":value, "key": value
            arr.ForEach(str =>
            {
                List<(int, int)> temp = new List<(int, int)>();
                foreach(string pair in str.Split(','))
                {
                    // 5. pair : "key": value
                    var arr2 = pair.Split(':');
                    int id = int.Parse(arr2[0].Replace("\"", ""));
                    int amount = int.Parse(arr2[1]);
                    temp.Add((id, amount));
                }
                enemyInfoList.Add(temp);
            });
        }
        Log.I("json={0}, list={1}", json, ToString());
    }
}

