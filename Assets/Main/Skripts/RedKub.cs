using UnityEngine;

public class RedKub : MonoBehaviour
{
    [SerializeField] private GameObject loseUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 2)
        {
            loseUI.SetActive(true);
        }
    }
}
