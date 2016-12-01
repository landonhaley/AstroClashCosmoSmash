
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TeamSelect:MonoBehaviour
{
    public List<TeamSelectButton> Buttons = new List<TeamSelectButton>();

    public List<PlayerData> Players = new List<PlayerData>();

    public int CurrentPlayer = 0;

    public TeamSelectButton CurSelected;

    public Text PlayerText;

    public void Start()
    {
        CurSelected = Buttons[0];

        CurSelected.Selected();

        InputControl.Instance.RegisterInputEvent(CurrentPlayer, new InputControlData.InputAction(OnClick));

        SetPlayerNumber(CurrentPlayer);
    }

    public bool SelectionCap()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (Buttons[i].IsInteractable())
                return false;
        }

        return true;
    }


    public void OnClick(float XAxis, float YAxis, bool MouseDown, bool MouseUp)
    {
        if (CurSelected != null)
        {
            if ((XAxis != 0 || YAxis != 0) && !SelectionCap())
            {
                TeamSelectButton temp = NextButton(XAxis, YAxis);
                CurSelected = temp != null ? temp : CurSelected;
                CurSelected.Selected();
            }

            if (MouseDown)
            {
                ChoosePlayer();
            }
        }
    }

    private void ChoosePlayer()
    {

        CurSelected.Deselect();

        CurSelected.Disable();

        PlayerData player = new PlayerData();
        player.PlayerNum = CurrentPlayer;

        switch (CurSelected.name) 
        {
            case "USA":
                player.PlayerCountry = PlayerData.Country.USA;
                break;
            case "Japan":
                player.PlayerCountry = PlayerData.Country.Japan;
                break;
            case "Russian":
                player.PlayerCountry = PlayerData.Country.Russia;
                break;
            case "China":
                player.PlayerCountry = PlayerData.Country.China;
                break;
        }

        Players.Add(player);

        InputControl.Instance.UnRegisterInputEvent(CurrentPlayer, OnClick);

        CurrentPlayer++;

        SetPlayerNumber(CurrentPlayer);

        if (CurrentPlayer < 4)
        {
            InputControl.Instance.RegisterInputEvent(CurrentPlayer, OnClick);
        }


        CurSelected = NextButtonAvailable(Buttons.IndexOf(CurSelected));

        if (CurSelected != null)
        {
            CurSelected.Selected();
        }
        else 
        {
			ControlCenter.Instance.LoadGame(Players);
        }
    }

    private void SetPlayerNumber(int num)
    {
        PlayerText.text = "Select Player: " + (num + 1);
    }

    private TeamSelectButton NextButton(float XAxis, float YAxis)
    {
        int index = CurSelected != null ? Buttons.IndexOf(CurSelected) : 0;

        TeamSelectButton button = CurSelected;

        CurSelected.Deselect();

        if (XAxis > 0)
        {
            button = (TeamSelectButton)CurSelected.FindSelectableOnRight();
        }
        else
            if (XAxis < 0)
            {
                button = (TeamSelectButton)CurSelected.FindSelectableOnLeft();
            }

        if (button != null)
            return button;

        if (YAxis > 0)
        {
            button = (TeamSelectButton)CurSelected.FindSelectableOnUp();
        }
        else
            if (YAxis < 0)
            {
                button = (TeamSelectButton)CurSelected.FindSelectableOnDown();
            }
        return button;
    }

    private TeamSelectButton NextButtonAvailable(int index)
    {
        if(CurSelected != null)
        {
            int nextIndex = (index + 1) % Buttons.Count;
            if (Buttons[nextIndex].IsInteractable())
            {
                return Buttons[nextIndex];
            }
            else
                if(!SelectionCap())
            {
                return NextButtonAvailable(index);
            }
        }
        return null;
    }

}
