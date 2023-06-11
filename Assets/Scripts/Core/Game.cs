using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : MonoBehaviour
    {
        public static WebDataController WebDataController { get; private set; }
        public static FileController FileController { get; private set; }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            FileController = new FileController();
            WebDataController = new WebDataController();
        }

    }
}