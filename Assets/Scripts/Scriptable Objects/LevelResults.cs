using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level Results", menuName = "Scriptable Objects/Level Results")]
public class LevelResults : ScriptableObject
{
    [SerializeField]
    private List<Result> results = new List<Result>();

    public void SaveResult(int buildIndex, float time)
    {
        int index = FindResult(buildIndex);
        if (index == -1)
            results.Add(new Result(buildIndex, time));

        else
            results[index].SetTime(time);
    }

    public float GetResult(int buildIndex)
    {
        int index = FindResult(buildIndex);
        if (index == -1)
            return 0;
        return results[index].time;
    }

    private int FindResult(int buildIndex)
    {
        for(int i = 0; i < results.Count; i++)
        {
            if (results[i].buildIndex == buildIndex)
                return i;
        }
        return -1;
    }

    private bool ContainsResult(int buildIndex)
    {
        return FindResult(buildIndex) != -1;
    }

    [Serializable]
    private struct Result
    {
        public int buildIndex;
        public float time;

        public Result(int buildIndex, float time)
        {
            this.buildIndex = buildIndex;
            this.time = time;
        }

        public void SetTime(float time)
        {
            this.time = Mathf.Min(time, this.time);
        }
    }
}
