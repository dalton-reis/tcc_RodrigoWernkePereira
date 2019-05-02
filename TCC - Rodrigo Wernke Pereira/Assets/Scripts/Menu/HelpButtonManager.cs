using UnityEngine;
using UnityEngine.UI;

public class HelpButtonManager : MonoBehaviour
{
    public Button HelpTextButton;

    void Start()
    {
        HelpTextButton.gameObject.SetActive(false);
    }
}