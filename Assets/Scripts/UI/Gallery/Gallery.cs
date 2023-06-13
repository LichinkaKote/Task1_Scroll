using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Assets.Scripts.UI.Gallery
{
    public class Gallery : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private GalleryImageView imageViewPf;
        [SerializeField] private Button closeBtn;
        [SerializeField] private UI_ScrollRectOcclusion scrollRectOcclusion;
        public static int SelectedFileID;

        private void Start()
        {
            Utils.SetGridCellSizeByWidth(grid, scrollRect.viewport);
            for (int i = 0; i < FileController.FILE_COUNT; i++)
            {
                var inst = Instantiate(imageViewPf, grid.transform);
                inst.Init(i);
                if (i < FileController.INIT_CACHE_SIZE)
                {
                    inst.Load();
                }
            }

            closeBtn.onClick.AddListener(CloseWindow);
            scrollRectOcclusion.Init();
        }

        private void CloseWindow()
        {
            SceneController.LoadMain();
            closeBtn.enabled = false;
        }
        private void Update()
        {
            Utils.EscapeHandle(CloseWindow);
        }
    }
}