using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class HelpButtonManager : MonoBehaviour
{
    public Button HelpTextButton;

    public GameObject WindTargetVirtualButtonGameObject;
    public GameObject TemperatureTargetVirtualButtonGameObject;
    public GameObject SceneTargetVirtualButtonGameObject;

    void Start()
    {
        HelpTextButton.gameObject.SetActive(false);

        SceneTargetVirtualButtonGameObject.GetComponent<VirtualButtonBehaviour>().enabled = false;
        WindTargetVirtualButtonGameObject.GetComponent<VirtualButtonBehaviour>().enabled = false;
        TemperatureTargetVirtualButtonGameObject.GetComponent<VirtualButtonBehaviour>().enabled = false;
    }

    public void ActivateVirtualButtons()
    {
        SceneTargetVirtualButtonGameObject.GetComponent<VirtualButtonBehaviour>().enabled = true;
        WindTargetVirtualButtonGameObject.GetComponent<VirtualButtonBehaviour>().enabled = true;
        TemperatureTargetVirtualButtonGameObject.GetComponent<VirtualButtonBehaviour>().enabled = true;
    }
}