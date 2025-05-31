using UnityEngine;
using UnityEngine.UI;

public class SelecImage : MonoBehaviour
{
    [SerializeField] private Sprite[] image;
    [SerializeField] private Image imageSelect;

    private void Awake()
    {
        imageSelect.GetComponent<Image>();
        imageSelect.sprite = image[Random.Range(0, image.Length)];
    }
}
