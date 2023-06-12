﻿using Assets.Scripts.Core;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Gallery
{
    public class Gallery : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private GalleryImageView imageViewPf;
        [SerializeField] private Button closeBtn;
        public static int SelectedFileID;

        private void Awake()
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
        }

        private void CloseWindow()
        {
            SceneController.LoadMain();
            closeBtn.enabled = false;
        }
    }
}