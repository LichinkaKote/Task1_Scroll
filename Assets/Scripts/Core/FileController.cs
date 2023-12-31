﻿using RSG;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.UI;

namespace Assets.Scripts.Core
{
    public class FileController
    {
        public const int FILE_COUNT = 66;
        public const int INIT_CACHE_SIZE = 12;
        public ReactiveProperty<float> CachingProgress { get; private set; } = new ReactiveProperty<float>();
        private class FileData
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
        public bool TryGetSprite(int index, out Promise<Sprite> sprite)
        {
            sprite = null;
            if (fileCache.Count > index)
            {
                if (fileCache[index].IsLoaded)
                {
                    sprite = new Promise<Sprite>();
                    sprite.Resolve(fileCache[index].data);
                }
                else
                {
                    sprite = Game.WebDataController.SendTextureRequest(fileCache[index].path);
                    sprite.Then((spr) =>
                        {
                            UpdateCache(index, spr);
                        });
                }
                return true;
            }
            else 
                return false;
        }
        private void UpdateCache(int index, Sprite spr)
        {
            fileCache[index].SetData(spr);
        }
        public IPromise CacheFiles()
        {
            CachingProgress.Value = 0f;
            var promiseList = new List<Promise>();
            for (int i = 0; i < INIT_CACHE_SIZE && i < FILE_COUNT; i++)
            {
                if (TryGetSprite(i, out Promise<Sprite> sprite))
                {
                    var promise = new Promise();
                    promiseList.Add(promise);
                    sprite.Finally(() => 
                    { 
                        CachingProgress.Value += Mathf.Round(1f / INIT_CACHE_SIZE * 100f);
                        promise.Resolve();
                    }) ;
                }
            }
            var result = Promise.All(promiseList);
            return result;
        }
    }
    
}