using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    public void OnClick()
    {
        ControlCenter.Instance.LoadTeamSelect();
    }
}
