using Assets.Scripts.Core;
using System;
using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Loading
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] private TMP_Text loadText;
        [SerializeField] private Slider slider;

        private void Awake()
        {
            Game.FileController.CachingProgress
                .Subscribe(UpdateProgress).AddTo(this);
        }
        private void UpdateProgress(float progress)
        {
            loadText.text = "Loading " + progress.ToString() + "%";
            slider.value = progress;
        }
    }
}