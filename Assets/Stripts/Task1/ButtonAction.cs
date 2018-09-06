using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Button = HoloToolkit.Unity.Buttons.Button;

public class ButtonAction : MonoBehaviour {

    private Button button;
    public GameObject theSliderBar;
    private int temp = 0;
    private void Start()
    {
        button = GetComponent<Button>();
        button.OnButtonClicked += OnButtonClicked;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnButtonClicked(GameObject obj)
    {
        //Image[] uii = theSliderBar.GetComponentsInChildren<Image>(true);
        //foreach (Image u in uii)
        //{
        //    u.enabled = true;
        //}
        //transform.parent.gameObject.GetComponent<FindCard>().showProgress = true;
        if (temp>4)
        {
            transform.parent.gameObject.GetComponent<FindCard>().DisplayInfor("Text", "Star");
        }
        else
        {
            transform.parent.gameObject.GetComponent<FindCard>().DisplayInfor("Graphics", "Star");
        }

        temp++;
    }
}
