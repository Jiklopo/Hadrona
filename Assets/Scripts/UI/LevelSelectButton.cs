using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    public int sceneIndex;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    public void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(sceneIndex);
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }
}
