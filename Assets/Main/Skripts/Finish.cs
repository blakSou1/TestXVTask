using UnityEngine;

namespace Assets.Main.Skripts
{
    public class Finish : MonoBehaviour
    {

        [SerializeField] private GameObject FinishUI;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 2)
            {
                FinishUI.SetActive(true);
            }
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
}