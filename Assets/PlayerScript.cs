using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.HeroEditor.Common.CharacterScripts;

public class PlayerScript : MonoBehaviour
{
    public class Move
    {
        public string Name = "";
        public string Type = "";
        public float Damage = 10f;
        public int CoolDown = 0;
        public int TurnsTillUse = 0;
        public string StatusEffect = "";
        public int StatusEffectTurns = 0;
        public float StatusDamage = 0f;
        public Sprite MoveIcon;
        public bool Unlocked = false;
    }

    public class EquipmentIncreases
    {
        public int HitpointsIncrease = 0;
        public int PhysicalAttackIncrease = 0;
        public int PhysicalDefenceIncrease = 0;
        public int MagicAttackIncrease = 0;
        public int MagicDefenceIncrease = 0;
        public int SpeedIncrease = 0;
        public Sprite sprite;
    }

    public GameObject PlayableCharacter;
    public GameObject gameControllerObject;
    public Character Character;

    public bool[] MovesUnlocked = new bool[25];

    //Status Ailment Effects
    public GameObject BurnEffect;
    public GameObject StunEffect;
    public GameObject FreezeEffect;
    public GameObject PoisonEffect;


    public GameObject moveSelect;
    string TempMoveName = "";

    public int PlayerLevel = 1;
    public float EXPNeeded = 100f;
    public float EXP = 0f;

    public int TalentPoints = 10;
    public int PerkPoints = 0;
    public int MoveUnlockPoints = 0;

    //Talents
    public float MaxHealth = 100f;
    public float hitpoints = 100f;
    public int HitPointsLevel = 1;
    public int PhysicalAttackLevel = 1;
    public int PhysicalDefenseLevel = 1;
    public int MagicAttackLevel = 1;
    public int MagicDefenceLevel = 1;
    public int SpeedLevel = 1;
    public int TotalHPIncrease = 0;
    public int TotalPAIncrease = 0;
    public int TotalPDIncrease = 0;
    public int TotalMAIncrease = 0;
    public int TotalMDIncrease = 0;
    public int TotalSPIncrease = 0;
    //Perks

    public TextMeshProUGUI hitpointsText;
    public TextMeshProUGUI TalentPointsText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExpText;

    public Dictionary<int, Move> Moves = new Dictionary<int, Move>()
        {
            { 1, new Move { Name = "", Type = "", Damage = 0 } },
            { 2, new Move { Name = "", Type = "", Damage = 0 } },
            { 3, new Move { Name = "", Type = "", Damage = 0 } },
            { 4, new Move { Name = "", Type = "", Damage = 0 } },
            { 5, new Move { Name = "", Type = "", Damage = 0 } }
        };

    public Dictionary<string, EquipmentIncreases> Equipment = new Dictionary<string, EquipmentIncreases>()
        {
            { "Armour", new EquipmentIncreases { HitpointsIncrease = 0, PhysicalAttackIncrease = 0, PhysicalDefenceIncrease = 0, MagicAttackIncrease = 0, MagicDefenceIncrease = 0, SpeedIncrease = 0 } },
            { "Sword", new EquipmentIncreases { HitpointsIncrease = 0, PhysicalAttackIncrease = 0, PhysicalDefenceIncrease = 0, MagicAttackIncrease = 0, MagicDefenceIncrease = 0, SpeedIncrease = 0} },
            { "Shield", new EquipmentIncreases { HitpointsIncrease = 0, PhysicalAttackIncrease = 0, PhysicalDefenceIncrease = 0, MagicAttackIncrease = 0, MagicDefenceIncrease = 0, SpeedIncrease = 0} }
        };

    int Knockeddown = 0;
    int Burned = 0;
    int Stunned = 0;
    int Frozen = 0;
    int Poisoned = 0;
    float BurnDamage = 0f;
    float StunDamage = 0f;
    float PoisonDamage = 0f;


    float PhysicalResistance = 0;
    float FireResistance = 0;
    float LightningResistance = 0;
    float IceResistance = 0;
    float PoisonResistance = 0;
    float KnockdownResist = 0;
    float BurnResist = 0;
    float StunResist = 0;
    float FreezeResist = 0;
    float PoisonResist = 0;
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        SetMoveName("Punch");
        setMove(1);
        SetMoveName("Slash");
        setMove(2);
        TalentPoints = 5;
        GainExp(0);

        MovesUnlocked[0] = true;
        MovesUnlocked[1] = true;
        for (int i = 2; i < MovesUnlocked.Length; i++) { MovesUnlocked[i] = false; }
    }

    // Update is called once per frame
    void Update()
    {
        hitpointsText.text = "HP: " + (int)hitpoints + " / " + (int)MaxHealth;
        TalentPointsText.text = "Talent Points Available: " + TalentPoints;
        if (Knockeddown > 0) { transform.rotation = Quaternion.Euler(0, 0, 90); }
        else { transform.rotation = Quaternion.Euler(0, 0, 0); }
        if (hitpoints > MaxHealth) { hitpoints = MaxHealth; }
        if (hitpoints <= 0) { Application.Quit(); } // UnityEditor.EditorApplication.isPlaying = false; }
    }

    IEnumerator DoBattle(int x)
    {
        float Damage = 0;
        float playerSpeed = 0;
        float MonsterSpeed = 0;
        if (x != 0)
        {
            if (Moves[x].Type == "Physical") { Damage = Moves[x].Damage * (1 + 0.2f * PhysicalAttackLevel); }
            else { Damage = Moves[x].Damage * (1 + 0.2f * MagicAttackLevel); }
        }

        if (Frozen > 0) { playerSpeed = SpeedLevel / 2; }
        else { playerSpeed = SpeedLevel; }
        if (monster.GetComponent<MonsterScript>().Frozen > 0) { MonsterSpeed = monster.GetComponent<MonsterScript>().speed / 2; }
        else { MonsterSpeed = monster.GetComponent<MonsterScript>().speed; }
        yield return new WaitForSeconds(0.5f);
        if (playerSpeed >= MonsterSpeed && x != 0)
        {
            if (Knockeddown <= 0 && Stunned <= 0)
            {
                PlayAnimation(Moves[x].Name);
                yield return new WaitForSeconds(0.5f);
                monster.GetComponent<MonsterScript>().TakeDamage(Damage, Moves[x].Type, Moves[x].StatusEffect, Moves[x].StatusEffectTurns, Moves[x].StatusDamage * (1 + 0.2f * MagicAttackLevel));
                Moves[x].TurnsTillUse = Moves[x].CoolDown;
            }
            else { Knockeddown -= 1; Stunned -= 1;}
            yield return new WaitForSeconds(0.5f);
            if (monster.GetComponent<MonsterScript>().MonsterAlive) { monster.GetComponent<MonsterScript>().Attack(); }
        }
        else
        {
            if (monster.GetComponent<MonsterScript>().MonsterAlive) { monster.GetComponent<MonsterScript>().Attack(); }
            yield return new WaitForSeconds(0.5f);
            if (Knockeddown <= 0 && Stunned <= 0 && x != 0)
            {
                PlayAnimation(Moves[x].Name);
                yield return new WaitForSeconds(0.5f);
                monster.GetComponent<MonsterScript>().TakeDamage(Damage, Moves[x].Type, Moves[x].StatusEffect, Moves[x].StatusEffectTurns, Moves[x].StatusDamage * (1 + 0.2f * MagicAttackLevel));
                Moves[x].TurnsTillUse = Moves[x].CoolDown;
            }
            else { Knockeddown -= 1; Stunned -= 1;}
        }
        yield return new WaitForSeconds(0.5f);
        monster.GetComponent<MonsterScript>().StatusEffectDamage();
        yield return new WaitForSeconds(0.5f);
        StatusEffectDamage();


        moveSelect.SetActive(true);
        if (!monster.GetComponent<MonsterScript>().MonsterAlive) { gameControllerObject.GetComponent<GameController>().NextMonster(); }
        if (Moves[1].TurnsTillUse > 0) { Moves[1].TurnsTillUse -= 1; }
        if (Moves[2].TurnsTillUse > 0) { Moves[2].TurnsTillUse -= 1; } 
        if (Moves[3].TurnsTillUse > 0) { Moves[3].TurnsTillUse -= 1; }
        if (Moves[4].TurnsTillUse > 0) { Moves[4].TurnsTillUse -= 1; }
        if (Moves[5].TurnsTillUse > 0) { Moves[5].TurnsTillUse -= 1; }
    }
    public void DealDamage(int x)
    {
        if (x != 0)
        {
            if (Moves[x].TurnsTillUse == 0 && Moves[x].Name != "")
            {
                moveSelect.SetActive(false);
                StartCoroutine(DoBattle(x));
            }
            else { Debug.Log("Turns Till Use:" + Moves[x].TurnsTillUse); }
        }
        else { StartCoroutine(DoBattle(x)); moveSelect.SetActive(false); }

        
        //monster.GetComponent<MonsterScript>().TakeDamage(damage);
    }

    public void TakeDamage(string Type, float Damage, string statusEffect, int statusTurns, float statusDamage)
    {
        if (Type == "Physical") { Damage = Damage * (100f / (100f + 5f * PhysicalDefenseLevel));
                                  Debug.Log("" + Damage); }
        else { Damage = Damage * (100f / (100f + 5f * MagicDefenceLevel)); }
        if (Damage <= 0) { Damage = 1f; }
        hitpoints -= Damage;
        if (statusEffect == "Knockdown" && Knockeddown <= 0) { Knockeddown = statusTurns; }
        if (statusEffect == "Burn" && Burned <= 0) { Burned = statusTurns; statusDamage = statusDamage * (100f / (100f + 5f * MagicDefenceLevel)); if (statusDamage < 0) { statusDamage = 1; } BurnDamage = statusDamage; BurnEffect.SetActive(true); }
        if (statusEffect == "Stun" && Stunned <= 0) { Stunned = statusTurns; statusDamage = statusDamage * (100f / (100f + 5f * MagicDefenceLevel)); if (statusDamage < 0) { statusDamage = 1; } StunDamage = statusDamage; StunEffect.SetActive(true); }
        if (statusEffect == "Freeze" && Frozen <= 0) { Frozen = statusTurns; FreezeEffect.SetActive(true); }
        if (statusEffect == "Poison" && Poisoned <= 0) { Poisoned = statusTurns; statusDamage = statusDamage * (100f / (100f + 5f * MagicDefenceLevel)); if (statusDamage < 0) { statusDamage = 1; } PoisonDamage = statusDamage; PoisonEffect.SetActive(true); }
        if (Burned > 0) { BurnEffect.SetActive(true); } else { BurnEffect.SetActive(false); }
        if (Stunned > 0) { StunEffect.SetActive(true); } else { StunEffect.SetActive(false); }
        if (Frozen > 0) { FreezeEffect.SetActive(true); } else { FreezeEffect.SetActive(false); }
        if (Poisoned > 0) { PoisonEffect.SetActive(true); } else { PoisonEffect.SetActive(false); }
    }

    public void StatusEffectDamage()
    {
        if (Burned > 0) { hitpoints -= BurnDamage; }
        if (Poisoned > 0) { hitpoints -= PoisonDamage; }
        if (Stunned > 0) { hitpoints -= StunDamage; }
        Poisoned -= 1;
        Burned -= 1;
        Frozen -= 1;
    }

    public void NotInBattle()
    {
        Knockeddown = 0;
        Burned = 0;
        Stunned = 0;
        Frozen = 0;
        Poisoned = 0;
        BurnEffect.SetActive(false);
        StunEffect.SetActive(false);
        FreezeEffect.SetActive(false);
        PoisonEffect.SetActive(false);
    }

    public void SetMoveName(string name)
    {
        TempMoveName = name;
    }

    public void PlayAnimation(string Name)
    {
        if (Name == "Punch") { Character.Jab(); }
        else if (Name == "Slash") { Character.Slash(); }
        else if (Name == "Take Down") { Character.TakeDown(); }
        else if (Name == "Shield Bash") { Character.ShieldBash(); }
        else if (Name == "Giga Strike") { Character.GigaStrike(); }
    }
    public void setMove(int MoveSlot)
    {
        for (int i = 1; i <= 2; i++) { if (TempMoveName == Moves[i].Name) { Debug.Log("Move Already In Use"); return; } }
        //Physical Moves
        if (TempMoveName == "Punch")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Physical";
            Moves[MoveSlot].Damage = 10f;
            Moves[MoveSlot].CoolDown = 1;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "NA";
            Moves[MoveSlot].StatusEffectTurns = 0;
            Moves[MoveSlot].StatusDamage = 0f;
            Moves[MoveSlot].Unlocked = true;
        }
        if (TempMoveName == "Slash")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Physical";
            Moves[MoveSlot].Damage = 20f;
            Moves[MoveSlot].CoolDown = 3;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "NA";
            Moves[MoveSlot].StatusEffectTurns = 0;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Take Down")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Physical";
            Moves[MoveSlot].Damage = 5f;
            Moves[MoveSlot].CoolDown = 4;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Knockdown";
            Moves[MoveSlot].StatusEffectTurns = 2;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Shield Bash")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Physical";
            Moves[MoveSlot].Damage = 25f;
            Moves[MoveSlot].CoolDown = 4;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Knockdown";
            Moves[MoveSlot].StatusEffectTurns = 1;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Giga Strike")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Physical";
            Moves[MoveSlot].Damage = 50f;
            Moves[MoveSlot].CoolDown = 7;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Knockdown";
            Moves[MoveSlot].StatusEffectTurns = 2;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        //Fire Moves
        if (TempMoveName == "Fire Ball")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Fire";
            Moves[MoveSlot].Damage = 10f;
            Moves[MoveSlot].CoolDown = 2;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Burn";
            Moves[MoveSlot].StatusEffectTurns = 2;
            Moves[MoveSlot].StatusDamage = 3f;
        }
        if (TempMoveName == "Flamethrower")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Fire";
            Moves[MoveSlot].Damage = 15f;
            Moves[MoveSlot].CoolDown = 4;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Burn";
            Moves[MoveSlot].StatusEffectTurns = 3;
            Moves[MoveSlot].StatusDamage = 5f;
        }
        if (TempMoveName == "Explosion")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Fire";
            Moves[MoveSlot].Damage = 75f;
            Moves[MoveSlot].CoolDown = 7;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "NA";
            Moves[MoveSlot].StatusEffectTurns = 0;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Ignite")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Fire";
            Moves[MoveSlot].Damage = 5f;
            Moves[MoveSlot].CoolDown = 7;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Burn";
            Moves[MoveSlot].StatusEffectTurns = 5;
            Moves[MoveSlot].StatusDamage = 20f;
        }
        if (TempMoveName == "Solar Flare")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Fire";
            Moves[MoveSlot].Damage = 50f;
            Moves[MoveSlot].CoolDown = 10;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Burn";
            Moves[MoveSlot].StatusEffectTurns = 6;
            Moves[MoveSlot].StatusDamage = 15f;
        }
        //Lightning Moves
        if (TempMoveName == "Spark")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Lightning";
            Moves[MoveSlot].Damage = 25f;
            Moves[MoveSlot].CoolDown = 2;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "NA";
            Moves[MoveSlot].StatusEffectTurns = 0;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Stun Shot")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Lightning";
            Moves[MoveSlot].Damage = 20f;
            Moves[MoveSlot].CoolDown = 4;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Stun";
            Moves[MoveSlot].StatusEffectTurns = 1;
            Moves[MoveSlot].StatusDamage = 2f;
        }
        if (TempMoveName == "Bolt")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Lightning";
            Moves[MoveSlot].Damage = 50f;
            Moves[MoveSlot].CoolDown = 6;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "NA";
            Moves[MoveSlot].StatusEffectTurns = 0;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Electrocution")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Lightning";
            Moves[MoveSlot].Damage = 30f;
            Moves[MoveSlot].CoolDown = 6;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Stun";
            Moves[MoveSlot].StatusEffectTurns = 2;
            Moves[MoveSlot].StatusDamage = 2f;
        }
        if (TempMoveName == "Lightning Strike")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Lightning";
            Moves[MoveSlot].Damage = 50f;
            Moves[MoveSlot].CoolDown = 11;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Stun";
            Moves[MoveSlot].StatusEffectTurns = 3;
            Moves[MoveSlot].StatusDamage = 5f;
        }
        //Ice Moves
        if (TempMoveName == "Icicle Throw")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Ice";
            Moves[MoveSlot].Damage = 10f;
            Moves[MoveSlot].CoolDown = 2;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Freeze";
            Moves[MoveSlot].StatusEffectTurns = 2;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Ice Ball")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Ice";
            Moves[MoveSlot].Damage = 25f;
            Moves[MoveSlot].CoolDown = 4;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Freeze";
            Moves[MoveSlot].StatusEffectTurns = 2;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Zero")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Ice";
            Moves[MoveSlot].Damage = 5f;
            Moves[MoveSlot].CoolDown = 7;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Freeze";
            Moves[MoveSlot].StatusEffectTurns = 7;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Ice Spike")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Ice";
            Moves[MoveSlot].Damage = 50f;
            Moves[MoveSlot].CoolDown = 6;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "NA";
            Moves[MoveSlot].StatusEffectTurns = 0;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Blizzard")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Ice";
            Moves[MoveSlot].Damage = 100f;
            Moves[MoveSlot].CoolDown = 11;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Freeze";
            Moves[MoveSlot].StatusEffectTurns = 4;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        //Poison Moves
        if (TempMoveName == "Poison Dart")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Poison";
            Moves[MoveSlot].Damage = 5f;
            Moves[MoveSlot].CoolDown = 2;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Poison";
            Moves[MoveSlot].StatusEffectTurns = 2;
            Moves[MoveSlot].StatusDamage = 5f;
        }
        if (TempMoveName == "Numbing Gas")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Poison";
            Moves[MoveSlot].Damage = 5f;
            Moves[MoveSlot].CoolDown = 5;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Knockdown";
            Moves[MoveSlot].StatusEffectTurns = 2;
            Moves[MoveSlot].StatusDamage = 0f;
        }
        if (TempMoveName == "Poison Gas")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Poison";
            Moves[MoveSlot].Damage = 5f;
            Moves[MoveSlot].CoolDown = 6;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Poison";
            Moves[MoveSlot].StatusEffectTurns = 4;
            Moves[MoveSlot].StatusDamage = 10f;
        }
        if (TempMoveName == "Venom Bite")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Poison";
            Moves[MoveSlot].Damage = 35f;
            Moves[MoveSlot].CoolDown = 5;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Poison";
            Moves[MoveSlot].StatusEffectTurns = 4;
            Moves[MoveSlot].StatusDamage = 10f;
        }
        if (TempMoveName == "Radiation")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "Poison";
            Moves[MoveSlot].Damage = 20f;
            Moves[MoveSlot].CoolDown = 11;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "Poison";
            Moves[MoveSlot].StatusEffectTurns = 7;
            Moves[MoveSlot].StatusDamage = 25f;
        }

        if (TempMoveName == "")
        {
            Moves[MoveSlot].Name = TempMoveName;
            Moves[MoveSlot].Type = "";
            Moves[MoveSlot].Damage = 0f;
            Moves[MoveSlot].CoolDown = 0;
            Moves[MoveSlot].TurnsTillUse = 0;
            Moves[MoveSlot].StatusEffect = "";
            Moves[MoveSlot].StatusEffectTurns = 0;
            Moves[MoveSlot].StatusDamage = 0f;
        }


    }

    public void IncreaseTalent(string TalentName)
    {
        if (TalentPoints > 0)
        {
            if (TalentName == "HitPointsLevel") { HitPointsLevel += 1; TalentPoints -= 1; }
            if (TalentName == "PhysicalAttackLevel") { PhysicalAttackLevel += 1; TalentPoints -= 1; }
            if (TalentName == "PhysicalDefenseLevel") { PhysicalDefenseLevel += 1; TalentPoints -= 1; }
            if (TalentName == "MagicAttackLevel") { MagicAttackLevel += 1; TalentPoints -= 1; }
            if (TalentName == "MagicDefenceLevel") { MagicDefenceLevel += 1; TalentPoints -= 1; }
            if (TalentName == "SpeedLevel") { SpeedLevel += 1; TalentPoints -= 1; }

            MaxHealth = 75 + 25 * HitPointsLevel;
            
        }
    }
    public void DecreaseTalent(string TalentName)
    {
            if (TalentName == "HitPointsLevel" && HitPointsLevel > 1) { HitPointsLevel -= 1; TalentPoints += 1; }
            if (TalentName == "PhysicalAttackLevel" && PhysicalAttackLevel > 1) { PhysicalAttackLevel -= 1; TalentPoints += 1; }
            if (TalentName == "PhysicalDefenseLevel" && PhysicalDefenseLevel > 1) { PhysicalDefenseLevel -= 1; TalentPoints += 1; }
            if (TalentName == "MagicAttackLevel" && MagicAttackLevel > 1) { MagicAttackLevel -= 1; TalentPoints += 1; }
            if (TalentName == "MagicDefenceLevel" && MagicDefenceLevel > 1) { MagicDefenceLevel -= 1; TalentPoints += 1; }
            if (TalentName == "SpeedLevel" && SpeedLevel > 1) { SpeedLevel -= 1; TalentPoints += 1; }
    }

    public void GainExp(float expGained)
    {
        EXP += expGained;
        while (EXP >= EXPNeeded)
        {
            EXP -= EXPNeeded;
            PlayerLevel += 1;
            EXPNeeded = 100 * Mathf.Pow(PlayerLevel, 2);
            TalentPoints += 5;
        }
        LevelText.text = "Level: " + PlayerLevel;
        ExpText.text = "Exp: " + EXP + " / " + EXPNeeded;
    }

    public void Equips()
    {
        HitPointsLevel -= TotalHPIncrease;
        PhysicalAttackLevel -= TotalPAIncrease;
        PhysicalDefenseLevel -= TotalPDIncrease;
        MagicAttackLevel -= TotalMAIncrease;
        MagicDefenceLevel -= TotalMDIncrease;
        SpeedLevel -= TotalSPIncrease;
        TotalHPIncrease = Equipment["Armour"].HitpointsIncrease + Equipment["Sword"].HitpointsIncrease + Equipment["Shield"].HitpointsIncrease;
        TotalPAIncrease = Equipment["Armour"].PhysicalAttackIncrease + Equipment["Sword"].PhysicalAttackIncrease + Equipment["Shield"].PhysicalAttackIncrease;
        TotalPDIncrease = Equipment["Armour"].PhysicalDefenceIncrease + Equipment["Sword"].PhysicalDefenceIncrease + Equipment["Shield"].PhysicalDefenceIncrease;
        TotalMAIncrease = Equipment["Armour"].MagicAttackIncrease + Equipment["Sword"].MagicAttackIncrease + Equipment["Shield"].MagicAttackIncrease;
        TotalMDIncrease = Equipment["Armour"].MagicDefenceIncrease + Equipment["Sword"].MagicDefenceIncrease + Equipment["Shield"].MagicDefenceIncrease;
        TotalSPIncrease = Equipment["Armour"].SpeedIncrease + Equipment["Sword"].SpeedIncrease + Equipment["Shield"].SpeedIncrease;
        HitPointsLevel += TotalHPIncrease;
        PhysicalAttackLevel += TotalPAIncrease;
        PhysicalDefenseLevel += TotalPDIncrease;
        MagicAttackLevel += TotalMAIncrease;
        MagicDefenceLevel += TotalMDIncrease;
        SpeedLevel += TotalSPIncrease;
        PlayableCharacter.SetActive(false);
        PlayableCharacter.SetActive(true);
    }
}
