using Assets.Scripts.Core;
using RSG;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Gallery
{
    public class GalleryImageView : MonoBehaviour
    {
        [SerializeField] private Image image;

        [SerializeField] private Button btn;

        private int ID;
        private bool inited;

        private void Start()
        {
            btn.onClick.AddListener(OnClick);
        }

        public void Init(int id)
        {
            ID = id;
            if (Game.FileController.TryGetSprite(ID, out IPromise<Sprite> sprite))
            {
                sprite.Then(s => 
                {
                    if (this) 
                    { 
                        image.sprite = s; 
                        inited = true; 
                    }
                });
            }
        }
        private void OnClick()
        {
            if (!inited) return;
            Gallery.SelectedFileID = ID;
            SceneController.LoadView();
        }
    }
}