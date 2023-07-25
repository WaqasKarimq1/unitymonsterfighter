using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveSelectorScript : MonoBehaviour
{
    public int MoveSlot = 1;
    public GameObject playerObject;
    public Image MoveSelected1;
    public Image MoveSelected2;
    public Image MoveSelected3;
    public Image MoveSelected4;
    public Image MoveSelected5;

    Sprite TempImage;

    public GameObject[] MoveSelectButtons = new GameObject[25];
    public bool[] MovesUnlocked = new bool[25];

    string SelectedMove1 = "x";
    string SelectedMove2 = "x";
    string SelectedMove3 = "x";
    string SelectedMove4 = "x";
    string SelectedMove5 = "x";

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Type;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI Cooldown;
    public TextMeshProUGUI StatusType;
    public TextMeshProUGUI StatusTurns;
    public TextMeshProUGUI StatusDamage;
    // Start is called before the first frame update
    void Start()
    {
        MovesUnlocked[0] = true;
        MovesUnlocked[1] = true;
        MoveSelectButtons[0].GetComponent<Button>().onClick.Invoke();
        MoveSlot = 2;
        MoveSelectButtons[1].GetComponent<Button>().onClick.Invoke();
        for (int i = 2; i < MoveSelectButtons.Length; i++ ) { MoveSelectButtons[i].GetComponent<Button>().interactable = false; }
        for (int i = 2; i < MoveSelectButtons.Length; i++) { MovesUnlocked[i] = false; }
        for (int i = 2; i < MoveSelectButtons.Length; i++)
        {
            if (playerObject.GetComponent<PlayerScript>().MovesUnlocked[i]) { MoveSelectButtons[i].GetComponent<Button>().interactable = true; }
        }
    }

    private void OnEnable()
    {
        for (int i = 2; i < MoveSelectButtons.Length; i++) 
        { 
            if (playerObject.GetComponent<PlayerScript>().MovesUnlocked[i]) { MoveSelectButtons[i].GetComponent<Button>().interactable = true; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMove(string MoveName)
    {
        if (MoveName == SelectedMove1) { return; }
        if (MoveName == SelectedMove2) { return; }
        if (MoveName == SelectedMove3) { return; }
        if (MoveName == SelectedMove4) { return; }
        if (MoveName == SelectedMove5) { return; }
        
        playerObject.GetComponent<PlayerScript>().SetMoveName(MoveName);
        playerObject.GetComponent<PlayerScript>().setMove(MoveSlot);

        setMoveIcon(MoveName);
        
        Name.text = "" + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Name;
        Type.text = "[" + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type + "]";
        if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Physical") { Type.color = Color.gray; }
        if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Fire") { Type.color = Color.red; }
        if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Lightning") { Type.color = Color.cyan; }
        if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Ice") { Type.color = Color.blue; }
        if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Poison") { Type.color = Color.green; }

        Damage.text = "DMG: " + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Damage;
        Cooldown.text = "CDN: " + (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].CoolDown - 1);
        if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect != "NA")
        {
            StatusType.text = "Can " + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect;
            StatusTurns.text = "" + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffectTurns + " Turns";
            StatusDamage.text = "DMG: " + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusDamage;

            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Knockdown") { StatusType.color = Color.gray; }
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Burn") { StatusType.color = Color.red; }
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Stun") { StatusType.color = Color.cyan; }
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Freeze") { StatusType.color = Color.blue; }
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Poison") { StatusType.color = Color.green; }
        }
        else
        {
            StatusType.text = "";
            StatusTurns.text = "";
            StatusDamage.text = "";
        }
        
        if (MoveSlot == 1) { SelectedMove1 = MoveName; }
        if (MoveSlot == 2) { SelectedMove2 = MoveName; }
        if (MoveSlot == 3) { SelectedMove3 = MoveName; }
        if (MoveSlot == 4) { SelectedMove4 = MoveName; }
        if (MoveSlot == 5) { SelectedMove5 = MoveName; }

    }

    void setMoveIcon(string name)
    {
        if (name == "Punch") { TempImage = MoveSelectButtons[0].GetComponent<Image>().sprite; }
        if (name == "Slash") { TempImage = MoveSelectButtons[1].GetComponent<Image>().sprite; }
        if (name == "Take Down") { TempImage = MoveSelectButtons[2].GetComponent<Image>().sprite; }
        if (name == "Shield Bash") { TempImage = MoveSelectButtons[3].GetComponent<Image>().sprite; }
        if (name == "Giga Strike") { TempImage = MoveSelectButtons[4].GetComponent<Image>().sprite; }
        if (name == "Fire Ball") { TempImage = MoveSelectButtons[5].GetComponent<Image>().sprite; }
        if (name == "Flamethrower") { TempImage = MoveSelectButtons[6].GetComponent<Image>().sprite; }
        if (name == "Explosion") { TempImage = MoveSelectButtons[7].GetComponent<Image>().sprite; }
        if (name == "Ignite") { TempImage = MoveSelectButtons[8].GetComponent<Image>().sprite; }
        if (name == "Solar Flare") { TempImage = MoveSelectButtons[9].GetComponent<Image>().sprite; }
        if (name == "Spark") { TempImage = MoveSelectButtons[10].GetComponent<Image>().sprite; }
        if (name == "Stun Shot") { TempImage = MoveSelectButtons[11].GetComponent<Image>().sprite; }
        if (name == "Bolt") { TempImage = MoveSelectButtons[12].GetComponent<Image>().sprite; }
        if (name == "Electrocution") { TempImage = MoveSelectButtons[13].GetComponent<Image>().sprite; }
        if (name == "Lightning Strike") { TempImage = MoveSelectButtons[14].GetComponent<Image>().sprite; }
        if (name == "Icicle Throw") { TempImage = MoveSelectButtons[15].GetComponent<Image>().sprite; }
        if (name == "Ice Ball") { TempImage = MoveSelectButtons[16].GetComponent<Image>().sprite; }
        if (name == "Zero") { TempImage = MoveSelectButtons[17].GetComponent<Image>().sprite; }
        if (name == "Ice Spike") { TempImage = MoveSelectButtons[18].GetComponent<Image>().sprite; }
        if (name == "Blizzard") { TempImage = MoveSelectButtons[19].GetComponent<Image>().sprite; }
        if (name == "Poison Dart") { TempImage = MoveSelectButtons[20].GetComponent<Image>().sprite; }
        if (name == "Numbing Gas") { TempImage = MoveSelectButtons[21].GetComponent<Image>().sprite; }
        if (name == "Poison Gas") { TempImage = MoveSelectButtons[22].GetComponent<Image>().sprite; }
        if (name == "Venom Bite") { TempImage = MoveSelectButtons[23].GetComponent<Image>().sprite; }
        if (name == "Radiation") { TempImage = MoveSelectButtons[24].GetComponent<Image>().sprite; }

        if (MoveSlot == 1) { MoveSelected1.GetComponent<Image>().sprite = TempImage; }
        if (MoveSlot == 2) { MoveSelected2.GetComponent<Image>().sprite = TempImage; }
        if (MoveSlot == 3) { MoveSelected3.GetComponent<Image>().sprite = TempImage; }
        if (MoveSlot == 4) { MoveSelected4.GetComponent<Image>().sprite = TempImage; }
        if (MoveSlot == 5) { MoveSelected5.GetComponent<Image>().sprite = TempImage; }

    }

    public void ChangeMoveSlot(int x)
    {
        MoveSlot = x;
        if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Name != "")
        {
            Name.text = "" + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Name;
            Type.text = "[" + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type + "]";
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Physical") { Type.color = Color.gray; }
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Fire") { Type.color = Color.red; }
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Lightning") { Type.color = Color.cyan; }
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Ice") { Type.color = Color.blue; }
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Type == "Poison") { Type.color = Color.green; }

            Damage.text = "DMG: " + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].Damage;
            Cooldown.text = "CDN: " + (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].CoolDown - 1);
            if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect != "NA")
            {
                StatusType.text = "Can " + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect;
                StatusTurns.text = "" + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffectTurns + " Turns";
                StatusDamage.text = "DMG: " + playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusDamage;

                if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Knockdown") { StatusType.color = Color.gray; }
                if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Burn") { StatusType.color = Color.red; }
                if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Stun") { StatusType.color = Color.cyan; }
                if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Freeze") { StatusType.color = Color.blue; }
                if (playerObject.GetComponent<PlayerScript>().Moves[MoveSlot].StatusEffect == "Poison") { StatusType.color = Color.green; }
            }
            else
            {
                StatusType.text = "";
                StatusTurns.text = "";
                StatusDamage.text = "";
            }
        }
    }
}
