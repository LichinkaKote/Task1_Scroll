using RSG;
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
        public static IPromise Load(Scenes scene)
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
                //.Then(() => Game.WebDataController.LoadData(""))
                .Then(() => Load(Scenes.Gallery)); 
        }
    }
}