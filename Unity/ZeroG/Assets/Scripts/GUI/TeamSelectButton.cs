using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

class TeamSelectButton : Button
{
    public Image Border;

    public void Start()
    {
        Border = gameObject.GetComponentsInChildren<Image>()[1];
    }

    public void Selected()
    {
        BorderSwitch(true);
        base.Select();
    }

    public override void Select()
    {
        base.Select();
    }

    public void Deselect()
    {
        BorderSwitch(false);
    }

    public void BorderSwitch(bool on)
    {
        Border.enabled = on;
    }

    public void Disable()
    {
        interactable = false;
        GetComponent<Image>().color = Color.gray;
        DoStateTransition(SelectionState.Disabled, true);
    }

    public void Test()
    {

    }
}

