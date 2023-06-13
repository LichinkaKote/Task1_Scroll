using Assets.Scripts.Core;
using RSG;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.View
{
    public class View : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button closeBtn;

        private void Awake()
        {
            if (Game.FileController.TryGetSprite(Gallery.Gallery.SelectedFileID, out IPromise<Sprite> sprite))
            {
                sprite.Then(s =>
                {
                    if (this) image.sprite = s;
                });
            }
            closeBtn.onClick.AddListener(CloseWindow);
        }
        private void CloseWindow()
        {
            SceneController.LoadGallery();
            closeBtn.enabled = false;
        }
        private void Update()
        {
            Utils.EscapeHandle(CloseWindow);
        }
    }
}