using DG.Tweening.Core.Easing;
using UnityEngine;

public class Lentitud : MonoBehaviour
{
    [SerializeField] private float Value;
    [SerializeField] private int time;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().StartLento(Value,time);
        }
    }
}
