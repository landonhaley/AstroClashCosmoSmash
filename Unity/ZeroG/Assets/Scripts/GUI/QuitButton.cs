using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {
    public void OnClick() 
    {
        ControlCenter.Instance.Quit();
    }
}
