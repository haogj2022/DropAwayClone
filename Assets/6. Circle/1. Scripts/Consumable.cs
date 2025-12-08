using UnityEngine;
using UnityEngine.UI;

public class Consumable : MonoBehaviour
{
    [SerializeField] private Image CircleImage;

    public void SetData(Color color)
    {
        CircleImage.color = color;
    }

    public Color GetColor()
    {
        return CircleImage.color;
    }
}
