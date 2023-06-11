using RSG;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace Assets.Scripts.Core
{
    public class FileController
    {
        public const int FILE_COUNT = 1;
        private struct FileData
        {
            public bool IsLoaded => data != null;
            public string path;
            public Sprite data;
            public void SetData(Sprite spr) 
            {
                data = spr;
            }
        }
        private List<FileData> fileCache = new List<FileData>();

        public FileController()
        {
            var files = Utils.CreateFilesList(FILE_COUNT);
            foreach (var file in files)
            {
                fileCache.Add(new FileData { path = file });
            }
        }
        public bool TryGetSprite(int index, out IPromise<Sprite> sprite)
        {
            sprite = null;
            if (fileCache.Count > index)
            {
                if (fileCache[index].IsLoaded)
                {
                    sprite = new Promise<Sprite>();
                    (sprite as Promise<Sprite>).Resolve(fileCache[index].data);
                }
                else
                {
                    sprite = Game.WebDataController.SendTextureRequest(fileCache[index].path)
                        .Then((spr) => UpdateCache(index, spr));
                }
                return true;
            }
            else 
                return false;
        }
        private IPromise<Sprite> UpdateCache(int index, Sprite spr)
        {
            fileCache[index].SetData(spr);
            var result = new Promise<Sprite>();
            result.Resolve(spr);
            return result;
        }
    }
    
}