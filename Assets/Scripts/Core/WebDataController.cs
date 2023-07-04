using RSG;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Core
{
    public class WebDataController
    {
        public Promise<Sprite> SendTextureRequest(string url)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            request.timeout = 7;
            var result = new Promise<Sprite>();
            request.SendWebRequest().completed += (req) =>
            {
                switch (request.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(request.url + ": " + request.error);
                        result.Reject(null);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(request.url + ": " + request.error);
                        result.Reject(null);
                        break;
                    case UnityWebRequest.Result.Success:
                        result.Resolve(GetSprite(request));
                        break;
                }
            };
            return result;
        }
        private Sprite GetSprite(UnityWebRequest req)
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(req);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 50f);
        }
    }
}