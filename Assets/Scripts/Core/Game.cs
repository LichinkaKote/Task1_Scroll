using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : MonoBehaviour
    {
        private static bool inited;
        public static WebDataController WebDataController { get; private set; }
        public static FileController FileController { get; private set; }
        
        private void Awake()
        {
            if (!inited)
            {
                DontDestroyOnLoad(gameObject);
                FileController = new FileController();
                WebDataController = new WebDataController();
                inited = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}