using UnityEngine;
using System.Collections;
using System.Globalization;
using HoloToolkit.Unity;
using UnityEngine.SceneManagement;
using ZXing;
using UnityEngine.UI;

public class FindCard : MonoBehaviour
{

    [SerializeField]
    public GameObject allElements1;

    [SerializeField]
    public Slider TimeSlider;

    public Text StopText;

    private GameObject allElements;
    private Text TextUI;
    private Image ImageUI;


    private string resultText;
    private bool rescan = true;
    public bool showProgress = false;
    private int blink = 0;
    private int trigger = 0;
    private float startTime = 0;
    private float timeTotal = 180;
    //public static readonly string[] namseStrings = { "Star" , "Cube", "Cylinder", "Hand", "Basketball","Sonw", "Pentagon", "Hexagon","Alien","Skull","Manager","BusinessMan" };
    private WebCamTexture webCameraTexture;
    private BarcodeReader barcodeReader;//Zxing

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
        }

        allElements = allElements1;
    }

    private void Update()
    {
        //直接把识别代码写在updata（）里似乎有些快？？？？？
        if (webCameraTexture != null && webCameraTexture.width > 100)
        {
            resultText = Decode(webCameraTexture.GetPixels32(), webCameraTexture.width, webCameraTexture.height);
            string type = GetComponent<Lunch>().informationType;
            if (resultText != null && rescan == true)
            {
                trigger++;
                if (trigger == 1)
                {
                    startTime = Time.time;
                }
                DisplayInfor(type, resultText);
                rescan = false;
            }

            if (showProgress == true && trigger > 0)
            {
                TimeSlider.value = (Time.time - startTime) / timeTotal;
            }

            if (Time.time - startTime > timeTotal)
            {
                StopText.text = "Please Stop the Experiment";
            }

        }
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

    public void DisplayInfor(string str1, string str2)
    {

        if (str1 == "Text")
        {
            rescan = false;
            TextUI = allElements.transform.Find("Text").GetComponent<Text>();
            TextUI.text = str2;
            InvokeRepeating("RepeatingText", 0.01f, 0.3f);
        }
        else if (str1 == "Graphics")
        {
            rescan = false;
            switch (str2)
            {
                case "Star":
                    ImageUI = allElements.transform.Find("Star").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Cube":
                    ImageUI = allElements.transform.Find("Cube").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Cylinder":
                    ImageUI = allElements.transform.Find("Cylinder").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Hand":
                    ImageUI = allElements.transform.Find("Hand").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Basketball":
                    ImageUI = allElements.transform.Find("Basketball").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Snow":
                    ImageUI = allElements.transform.Find("Snow").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Quite":
                    ImageUI = allElements.transform.Find("Pentagon").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Quiet":
                    ImageUI = allElements.transform.Find("Hexagon").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Adapt":
                    ImageUI = allElements.transform.Find("Alien").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Adopt":
                    ImageUI = allElements.transform.Find("Skull").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Compliment":
                    ImageUI = allElements.transform.Find("Manager").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
                case "Complement":
                    ImageUI = allElements.transform.Find("BusinessMan").GetComponent<Image>();
                    InvokeRepeating("RepeatingImage", 0.01f, 0.3f);
                    break;
            }
        }

    }

    private void RepeatingImage()
    {
        blink++;
        // Debug.Log(blink % 2);
        if (blink % 2 == 0)
        {
            ImageUI.enabled = true;
        }
        else
        {
            ImageUI.enabled = false;
        }

        if (blink == 17)
        {
            CancelInvoke("RepeatingImage");
            blink = 0;
            rescan = true;
        }

    }

    private void RepeatingText()
    {
        blink++;
        // Debug.Log(blink % 2);
        if (blink % 2 == 0)
        {
            TextUI.enabled = true;
        }
        else
        {
            TextUI.enabled = false;
        }

        if (blink == 13)
        {
            CancelInvoke("RepeatingText");
            blink = 0;
            rescan = true;
        }
    }

    private void OnButtonClicked(GameObject obj)
    {
        Debug.Log("test");
    }
}
