using RSG;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Screen.orientation = ScreenOrientation.Portrait;
            Load(Scenes.Loading)
                .Then(() => Utils.NextFrame(1))
                .Then(() => Game.FileController.CacheFiles())
                .Then(() => Load(Scenes.Gallery));
        }

        public static void LoadView()
        {
            Load(Scenes.View)
                .Then(() => Screen.orientation = ScreenOrientation.AutoRotation);
            
        }
        public static void LoadMain()
        {
            Load(Scenes.MainMenu);
        }
    }
}