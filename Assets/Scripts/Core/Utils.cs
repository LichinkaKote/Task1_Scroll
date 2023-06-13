using RSG;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

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
            int offset = 1;
            var result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var file = "http://data.ikppbb.com/test-task-unity-data/pics/" + (i + offset).ToString() + ".jpg";
                result.Add(file);
            }
            return result;
        }
        public static void SetGridCellSizeByWidth(GridLayoutGroup grid, RectTransform parent)
        {
            var width = parent.rect.width;
            var padding = grid.padding.left + grid.padding.right;
            var spacing = grid.spacing.x * (grid.constraintCount - 1);
            var size = (width - padding - spacing) / grid.constraintCount;
            grid.cellSize = new Vector2(size, size);
        }
        public static void EscapeHandle(Action action)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                action.Invoke();
            }
        }
    }
}