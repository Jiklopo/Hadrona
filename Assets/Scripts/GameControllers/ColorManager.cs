using UnityEngine;

public class ColorManager : MonoBehaviour
{
    
    private static int curHadronaColor = 0;

    static Color[] hadronaColors = new[]
    {
        Color.red,
        Color.green,
        Color.yellow,
        Color.magenta
    };

    public static ColorManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        curHadronaColor = 0;
    }

    public static Color GetHadronaColor()
    {
        curHadronaColor = (curHadronaColor + 1) % hadronaColors.Length;
        return hadronaColors[curHadronaColor];
    }
}