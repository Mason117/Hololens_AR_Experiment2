using UnityEngine;
using System.Collections;
using ZXing;
using UnityEngine.UI;

public class QRcode : MonoBehaviour
{
    /// <summary> 包含RGBA </summary>
    public Color32[] data;

    /// <summary> canvas上的Text，显示获取的二维码内部信息 </summary>
    public Text QRcodeText;

    /// <summary> 相机捕捉到的图像 </summary>
    private WebCamTexture webCameraTexture;

    /// <summary> ZXing中的方法，可读取二维码中的内容 </summary>
    private BarcodeReader barcodeReader;

    private string resultText;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    IEnumerator Start()
    {
        barcodeReader = new BarcodeReader();
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam); //请求授权使用摄像头
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices; //获取摄像头设备
            string devicename = devices[0].name;
            //这两个参数是我们要求的款和高。这两个参数如果设置大了，运行很缓慢！！！！！！
            webCameraTexture = new WebCamTexture(devicename, 400, 300);
            webCameraTexture.Play();
            InvokeRepeating("Scan", 0.5f, 0.5f);
        }

    }

    private void Scan()
    {
        if (webCameraTexture != null && webCameraTexture.width > 100)
        {
            resultText = Decode(webCameraTexture.GetPixels32(), webCameraTexture.width, webCameraTexture.height);
            QRcodeText.text = resultText;
            Debug.Log("always scaing ");
            if (resultText=="test")
            {
                //webCameraTexture.Pause();
                this.CancelInvoke();
            }
        }
    }

    void Update()
    {
        //直接把识别代码写在updata（）里似乎有些快？？？？？
        //if (webCameraTexture != null && webCameraTexture.width > 100)
        //{
        //    resultText = Decode(webCameraTexture.GetPixels32(), webCameraTexture.width, webCameraTexture.height);
        //    QRcodeText.text = resultText;
        //    Debug.Log(resultText);
        //}
    }

    public string Decode(Color32[] colors, int width, int height)
    {
        BarcodeReader reader = new BarcodeReader();
        var result = reader.Decode(colors, width, height);
        if (result != null)
        {
            return result.Text;
        }
        return null;
    }

}