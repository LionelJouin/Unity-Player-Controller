using UnityEngine;
using System.Collections;

public class Joystick {

    private int playerNumber;

    readonly public string Horizontal;
    readonly public string Vertical;
    readonly public string Dash;
    readonly public string Action;

    public Joystick(int playerNumber) {
        this.playerNumber = playerNumber;

        Horizontal = "Horizontal_P" + playerNumber;
        Vertical = "Vertical_P" + playerNumber;
        Dash = "Dash_P" + playerNumber;
        Action = "Action_P" + playerNumber;
    }
}
