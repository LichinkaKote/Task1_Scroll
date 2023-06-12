using RSG;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public static class SceneController
    {
        public enum Scenes
        {
            MainMenu,
            Loading,
            Gallery,
            View,
        }
        private static IPromise Load(Scenes scene)
        {
            var promise = new Promise();
            var handle = SceneManager.LoadSceneAsync(scene.ToString());
            handle.completed += (h) =>
            {
                promise.Resolve();
            };
            return promise;
        }

        public static void LoadGallery()
        {
            Load(Scenes.Loading)
                .Then(() => Utils.NextFrame(1))
                .Then(() => Game.FileController.CacheFiles(8))
                .Then(() => Load(Scenes.Gallery)); 
        }

        public static void LoadView()
        {
            Load(Scenes.View);
        }
        public static void LoadMain()
        {
            Load(Scenes.MainMenu);
        }
    }
}