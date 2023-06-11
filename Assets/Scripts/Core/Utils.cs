using RSG;
using System;
using System.Collections.Generic;
using UniRx;

namespace Assets.Scripts.Core
{
    public static class Utils
    {
        public static IPromise NextFrame(int count)
        {
            Promise promise = new Promise();

            if (count <= 0)
                promise.Resolve();
            else
            {
                IDisposable dis = null;
                dis = ForEachFrame(() =>
                {
                    count--;

                    if (count <= 0)
                    {
                        dis?.Dispose();
                        promise.Resolve();
                    }
                });
            }

            return promise;
        }
        public static IDisposable ForEachFrame(Action action)
        {
            return Observable.EveryUpdate().Subscribe(x => action());
        }

        public static List<string> CreateFilesList(int count)
        {
            var result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var file = "http://data.ikppbb.com/test-task-unity-data/pics/" + i.ToString() + ".jpg";
                result.Add(file);
            }
            return result;
        }
    }
}