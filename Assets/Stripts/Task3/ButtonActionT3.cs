using UnityEngine;
using UnityEngine.UI;
using Button = HoloToolkit.Unity.Buttons.Button;

public class ButtonActionT3 : MonoBehaviour {

    private Button button;

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
        string buttonTag = obj.tag;
        switch (buttonTag)
        {
            case "B1":
                transform.parent.gameObject.GetComponent<NewTask3Server>().B1();
                break;
            case "B2":
                transform.parent.gameObject.GetComponent<NewTask3Server>().B2();
                break;
            case "B3":
                transform.parent.gameObject.GetComponent<NewTask3Server>().B3();
                break;
            case "B4":
                transform.parent.gameObject.GetComponent<NewTask3Server>().B4();
                break;
        }
    }
}
