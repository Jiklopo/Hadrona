using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level Stats", menuName = "Scriptable Objects/Level Stats")]
public class LevelStats : ScriptableObject
{
    [SerializeField]
    private List<Stat> results = new List<Stat>();

    public void Initialize()
    {
        int scenesNumber = SceneManager.sceneCountInBuildSettings;
        if (results.Count == scenesNumber - 4)
            return;

        results = new List<Stat>();
        for(int i = 3; i < scenesNumber - 1; i++)
        {
            Stat stat = new Stat();
            stat.buildIndex = i;
            results.Add(stat);
        }
    }

    public void SaveResult(int buildIndex, float time)
    {
        int index = FindResult(buildIndex);
        if (index == -1)
            return;

        else
        {
            Stat s1 = results[index];
            Stat s2 = new Stat();
            s2.buildIndex = s1.buildIndex;
            s2.twoStarTime = s1.twoStarTime;
            s2.threeStarTime = s1.threeStarTime;
            s2.SetTime(time);
            results.RemoveAt(index);
            results.Add(s2);
        }
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
    private struct Stat
    {
        public int buildIndex;
        public int twoStarTime;
        public int threeStarTime;
        public float time;

        public void SetTime(float time)
        {
            if(this.time == 0)
            {
                this.time = time;
                return;
            }
            this.time = Mathf.Min(time, this.time);
        }
    }
}
