using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private Transform loaderTr;

        private void Update()
        {
            loaderTr.Rotate(Vector3.forward, -40 * Time.deltaTime);
        }
    }
}