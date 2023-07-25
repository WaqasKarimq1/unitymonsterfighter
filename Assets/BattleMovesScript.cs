using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMovesScript : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject MoveSelectionObject;

    public Text Move1Text;
    public Text Move2Text;
    public Text Move3Text;
    public Text Move4Text;
    public Text Move5Text;

    public Image[] Moves = new Image[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moves[0].sprite = MoveSelectionObject.GetComponent<MoveSelectorScript>().MoveSelected1.sprite;
        Moves[1].sprite = MoveSelectionObject.GetComponent<MoveSelectorScript>().MoveSelected2.sprite;
        Moves[2].sprite = MoveSelectionObject.GetComponent<MoveSelectorScript>().MoveSelected3.sprite;
        Moves[3].sprite = MoveSelectionObject.GetComponent<MoveSelectorScript>().MoveSelected4.sprite;
        Moves[4].sprite = MoveSelectionObject.GetComponent<MoveSelectorScript>().MoveSelected5.sprite;

        if (playerObject.GetComponent<PlayerScript>().Moves[1].TurnsTillUse > 0) { Moves[0].color = Color.black; }
        else { Moves[0].color = Color.white; }

        if (playerObject.GetComponent<PlayerScript>().Moves[2].TurnsTillUse > 0) { Moves[1].color = Color.black; }
        else { Moves[1].color = Color.white; }

        if (playerObject.GetComponent<PlayerScript>().Moves[3].TurnsTillUse > 0) { Moves[2].color = Color.black; }
        else { Moves[2].color = Color.white; }

        if (playerObject.GetComponent<PlayerScript>().Moves[4].TurnsTillUse > 0) { Moves[3].color = Color.black; }
        else { Moves[3].color = Color.white; }

        if (playerObject.GetComponent<PlayerScript>().Moves[5].TurnsTillUse > 0) { Moves[4].color = Color.black; }
        else { Moves[4].color = Color.white; }


        Move1Text.text = "" + playerObject.GetComponent<PlayerScript>().Moves[1].Name +
        "\n[" + playerObject.GetComponent<PlayerScript>().Moves[1].Type +
        "]\n" + playerObject.GetComponent<PlayerScript>().Moves[1].TurnsTillUse;
        
        Move2Text.text = "" + playerObject.GetComponent<PlayerScript>().Moves[2].Name +
        "\n[" + playerObject.GetComponent<PlayerScript>().Moves[2].Type +
        "]\n" + playerObject.GetComponent<PlayerScript>().Moves[2].TurnsTillUse;

        Move3Text.text = "" + playerObject.GetComponent<PlayerScript>().Moves[3].Name +
        "\n[" + playerObject.GetComponent<PlayerScript>().Moves[3].Type +
        "]\n" + playerObject.GetComponent<PlayerScript>().Moves[3].TurnsTillUse;

        Move4Text.text = "" + playerObject.GetComponent<PlayerScript>().Moves[4].Name +
        "\n[" + playerObject.GetComponent<PlayerScript>().Moves[4].Type +
        "]\n" + playerObject.GetComponent<PlayerScript>().Moves[4].TurnsTillUse;
        
        Move5Text.text = "" + playerObject.GetComponent<PlayerScript>().Moves[5].Name +
        "\n[" + playerObject.GetComponent<PlayerScript>().Moves[5].Type +
        "]\n" + playerObject.GetComponent<PlayerScript>().Moves[5].TurnsTillUse;
    }
}
