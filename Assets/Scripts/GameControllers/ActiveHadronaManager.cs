using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHadronaManager : MonoBehaviour
{
    public static ActiveHadronaManager Instance { get; private set; }
    private List<Hadrona> hadronas = new List<Hadrona>();
    private int currentHadrona = 0;
    public Hadrona ActiveHadrona => hadronas[currentHadrona];
    public bool AnyoneAlive => hadronas.Count > 0;

    private void Awake()
    {
        Instance = this;
        foreach (Hadrona h in FindObjectsOfType<Hadrona>())
            hadronas.Add(h);
    }

    private void Start()
    {
        currentHadrona = 0;
        hadronas[0].Select();
    }

    public void DeleteHadrona()
    {
        Destroy(ActiveHadrona.gameObject);
        hadronas.RemoveAt(currentHadrona);
        if (AnyoneAlive)
        {
            currentHadrona = 0;
            hadronas[0].Select();
            return;
        }
        ResultsManager.Instance.FinishLevel();
        LevelManager.Instance.NextLevel();
    }

    public void NextHadrona()
    {
        currentHadrona = (currentHadrona + 1) % hadronas.Count;
    }

    public void ActivateHadrona(Hadrona hadrona)
    {
        for (int i = 0; i < hadronas.Count; i++)
        {
            if (hadronas[i].Equals(hadrona))
            {
                hadronas[i].Select();
                currentHadrona = i;
            }
        }

    }
}
