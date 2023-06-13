using Assets.Scripts.Core;
using RSG;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Gallery
{
    public class GalleryImageView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button btn;
        [SerializeField] private Loader loader;

        private int ID;
        private bool loaded, inited;

        private void Start()
        {
            btn.onClick.AddListener(OnClick);
        }

        public void Init(int id)
        {
            ID = id;
            inited = true;
        }
        private void OnClick()
        {
            if (!loaded) return;
            Gallery.SelectedFileID = ID;
            SceneController.LoadView();
        }
        private void OnEnable()
        {
            Load();
        }
        public void Load()
        {
            if (loaded || !inited) return;

            if (Game.FileController.TryGetSprite(ID, out IPromise<Sprite> sprite))
            {
                sprite.Then(s =>
                {
                    if (this)
                    {
                        image.sprite = s;
                        loaded = true;
                        loader.gameObject.SetActive(false);
                    }
                });
            }
        }
    }
}