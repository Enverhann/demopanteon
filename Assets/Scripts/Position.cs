using UnityEngine;
using TMPro;

public class Position : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI finishText;
    public TextMeshProUGUI percentage;
    public void PositionUpdate(string textPosition)
    {
        text.text = textPosition;
    }
    public void FinishPosition(string textPosition)
    {
        finishText.text = textPosition;
    }
}
