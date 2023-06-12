using Assets.Scripts.Core;
using System;
using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.UI.Loading
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] private TMP_Text loadText;

        private void Awake()
        {
            Game.FileController.CachingProgress
                .Subscribe(value => loadText.text = "Loading " + value.ToString() + "%").AddTo(this);
        }
    }
}