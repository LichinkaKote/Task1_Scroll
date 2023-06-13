using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button galleryBtn;

        private void Start()
        {
            galleryBtn.onClick.AddListener(OnGalleryClick);
        }
        private void OnGalleryClick()
        {
            SceneController.LoadGallery();
            galleryBtn.enabled = false;
        }
    }
}