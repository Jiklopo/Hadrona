using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    public int sceneIndex;
    [SerializeField] private LevelStats results;
    [SerializeField] private TextMeshProUGUI nameText, recordText;
    public void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(sceneIndex);
    }

    public void SetText(string text)
    {
        nameText.text = text;
    }

    public void SetRecordText()
    {
        float record = results.GetResult(sceneIndex);
        if (record == 0)
            recordText.text = "Not played yet";
        else
            recordText.text = Timer.FormatTime(record);
    }
}
