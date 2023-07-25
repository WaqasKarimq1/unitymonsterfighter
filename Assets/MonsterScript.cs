using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterScript : MonoBehaviour
{

    public GameObject MonsterSpawnerObject;
    public GameObject Portal;
    int MonsterLevel = 1;
    float hitpoints = 100f;
    public float damage = 5f;
    public int speed = 10;
    float ExpDropped = 0;
    public float TotalExpDropped = 0f;
    public TextMeshProUGUI TotalExpDroppedText;
    public float TotalGoldDropped = 0f;
    public TextMeshProUGUI TotalGoldDroppedText;
    float GoldDropped = 0;
    int UsableMoves = 0;

    public TextMeshProUGUI[] MonsterStatText = new TextMeshProUGUI[11];
    public GameObject MonsterStats;

    public class MonsterMove
    {
        public string Type = "";
        public float Damage = 10f;
        public int CoolDown = 0;
        public int TurnsTillUse = 0;
        public string StatusEffect = "";
        public int StatusEffectTurns = 0;
        public float StatusDamage = 0f;
    }

    public Dictionary<int, MonsterMove> MonsterMoves = new Dictionary<int, MonsterMove>()
    {
        { 1, new MonsterMove {} },
        { 2, new MonsterMove {} },
        { 3, new MonsterMove {} }
    };
    public GameObject gameController;
    public GameObject MainMenu;
    public GameObject WildBattleMenu;
    public GameObject playerObject;
    public TextMeshProUGUI HitpointsText;

    public bool MonsterAlive = false;
   
    //Status Ailments
    public int Knockeddown = 0;
    public int Burned = 0;
    public int Stunned = 0;
    public int Frozen = 0;
    public int Poisoned = 0;
    float BurnDamage = 0f;
    float StunDamage = 0f;
    float PoisonDamage = 0f;

    float PhysicalResistance = 0f;
    float FireResistance = 0f;
    float LightningResistance = 0f;
    float IceResistance = 0f;
    float PoisonResistance = 0f;

    bool KnockdownResist = false;
    bool BurnResist = false;
    bool StunResist = false;
    bool FreezeResist = false;
    bool PoisonResist = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HitpointsText.text = "" + (int)hitpoints;
        //if (Knockeddown > 0) {transform.rotation = Quaternion.Euler(0, 0, 90); }
        //else { transform.rotation = Quaternion.Euler(0, 0, 0); }
    }

    public void TakeDamage(float damage, string Type, string statusEffect, int statusTurns, float statusDamage)
    {
        if (Type == "Physical") { hitpoints -= damage * (1 - PhysicalResistance / 100); }
        else if (Type == "Fire") { hitpoints -= damage * (1 - FireResistance / 100); }
        else if (Type == "Lightning") { hitpoints -= damage * (1 - LightningResistance / 100); }
        else if (Type == "Ice") { hitpoints -= damage * (1 - IceResistance / 100); }
        else if (Type == "Poison") { hitpoints -= damage * (1 - PoisonResistance / 100); }
        else { hitpoints -= damage; }
        if (Knockeddown <= 0 && Stunned <= 0)
        {
            //MonsterSpawnerObject.GetComponent<MonsterSpawner>().ChangeAnimation("Idle");
            if (statusEffect == "Knockdown" && KnockdownResist == false && Knockeddown <= 0) { Knockeddown = statusTurns; MonsterSpawnerObject.GetComponent<MonsterSpawner>().ChangeAnimation("Dead"); }
            if (statusEffect == "Stun" && StunResist == false && Stunned <= 0) { Stunned = statusTurns; StunDamage = statusDamage; MonsterSpawnerObject.GetComponent<MonsterSpawner>().ChangeAnimation("Dead"); }
        }

        if (statusEffect == "Burn" && BurnResist == false && Burned <= 0) { Burned = statusTurns; BurnDamage = statusDamage; }
        if (statusEffect == "Freeze" && FreezeResist == false && Frozen <= 0) { Frozen = statusTurns; }
        if (statusEffect == "Poison" && PoisonResist == false && Poisoned <= 0) { Poisoned = statusTurns; PoisonDamage = statusDamage; }
        if (hitpoints <= 0 ) { MonsterDefeated(); }
    }
    
    public void StatusEffectDamage()
    {
        if (Burned > 0) { hitpoints -= BurnDamage; }
        if (Poisoned > 0) { hitpoints -= PoisonDamage; }
        if (Stunned > 0) { hitpoints -= StunDamage; }
        if (Knockeddown <= 0 && Stunned <= 0 && MonsterAlive)
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().ChangeAnimation("Idle");
        }
        if (hitpoints <= 0) { MonsterDefeated(); }
        Poisoned -= 1;
        Burned -= 1;
        Frozen -= 1;
    }

    public void Attack()
    {
        int x = Random.Range(1, UsableMoves + 1);
        if (UsableMoves == 2)
        {
            if (x == 1 && MonsterMoves[x].TurnsTillUse > 0) { x = 2; }
            if (x == 2 && MonsterMoves[x].TurnsTillUse > 0) { x = 1; }
        }
        else if (UsableMoves == 3)
        {
            if (x == 1 && MonsterMoves[x].TurnsTillUse > 0) { x = Random.Range(2, UsableMoves + 1); }
            if (x == 2 && MonsterMoves[x].TurnsTillUse > 0)
            {
                if (MonsterMoves[1].TurnsTillUse > 0) { x = 3; }
                else { x = 3; }
            }
            if (x == 3 && MonsterMoves[x].TurnsTillUse > 0) { x = Random.Range(1, UsableMoves); }
        }
        if (Knockeddown <= 0 && Stunned <= 0 && MonsterMoves[x].TurnsTillUse <= 0)
        {
            playerObject.GetComponent<PlayerScript>().TakeDamage(MonsterMoves[x].Type, MonsterMoves[x].Damage,
                MonsterMoves[x].StatusEffect, MonsterMoves[x].StatusEffectTurns, MonsterMoves[x].StatusDamage);
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().ChangeAnimation("Attack");
            MonsterMoves[x].TurnsTillUse = MonsterMoves[x].CoolDown;
        }
        else { Knockeddown -= 1; Stunned -= 1;}

        MonsterMoves[1].TurnsTillUse -= 1;
        MonsterMoves[2].TurnsTillUse -= 1;
        MonsterMoves[3].TurnsTillUse -= 1;
    }

    public void MonsterDefeated()
    {
        if (MonsterAlive)
        {
            gameController.GetComponent<GameController>().gold += GoldDropped;
            TotalGoldDropped += GoldDropped;
            playerObject.GetComponent<PlayerScript>().GainExp(ExpDropped);
            TotalExpDropped += ExpDropped;
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().ChangeAnimation("Dead");
            MonsterAlive = false;
            //gameController.GetComponent<GameController>().NextMonster();
        }
        
    }
    public void EndBattle()
    {
        
        MainMenu.SetActive(true);
        WildBattleMenu.SetActive(false);
        gameObject.SetActive(false);
        playerObject.GetComponent<PlayerScript>().NotInBattle();

    }

    public IEnumerator AfterBattleReport()
    {
        for (int i = 0; i <= TotalExpDropped; i++)
        {
            TotalGoldDroppedText.text = "" + i;
            TotalExpDroppedText.text = "" + i;
            yield return new WaitForSeconds(0.0001f);
        }
    }
    public void SpawnMonster(string Name, int level)
    {
        Knockeddown = 0;
        Burned = 0;
        Stunned = 0;
        Frozen = 0;
        Poisoned = 0;
        BurnDamage = 0f;
        StunDamage = 0f;
        PoisonDamage = 0f;
        MonsterLevel = level;
        MonsterAlive = true;
        if (Name == "Salamander")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(0);
            hitpoints = 30f + 10 * MonsterLevel;
            speed = MonsterLevel / 4;
            PhysicalResistance = 0;
            FireResistance = 0;
            LightningResistance = 0;
            IceResistance = 0;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 10f * MonsterLevel;
            GoldDropped = 10f * MonsterLevel;
            UsableMoves = 1;
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 5f + 2 * MonsterLevel;
            MonsterMoves[1].CoolDown = 2;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 0;
            MonsterMoves[1].StatusEffectTurns = 2;
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 10f + MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 0;
            MonsterMoves[2].StatusEffectTurns = 2;
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 20f + MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 0;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Yeti")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(4);
            hitpoints = 50f + 25 * MonsterLevel;
            speed = MonsterLevel / 5;
            PhysicalResistance = 25;
            FireResistance = 25;
            LightningResistance = 25;
            IceResistance = 25;
            PoisonResistance = 25;
            KnockdownResist = true;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 100f * MonsterLevel;
            GoldDropped = 100f * MonsterLevel;
            UsableMoves = 2;
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 0;
            MonsterMoves[1].StatusEffectTurns = 2;
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 15f + 2 * MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 0;
            MonsterMoves[2].StatusEffectTurns = 2;
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 20f + MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 0;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Slime")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(7);
            hitpoints = 25f + 10 * MonsterLevel;
            speed = MonsterLevel;
            PhysicalResistance = -25;
            FireResistance = -50;
            LightningResistance = 0;
            IceResistance = 0;
            PoisonResistance = 75;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = true;
            ExpDropped = 30f * MonsterLevel;
            GoldDropped = 30f * MonsterLevel;
            UsableMoves = 1;
            MonsterMoves[1].Type = "Magic";
            MonsterMoves[1].Damage = 10f + 2 * MonsterLevel;
            MonsterMoves[1].CoolDown = 3;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Poison";
            MonsterMoves[1].StatusDamage = 10f + 3 * MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 10f + MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 0;
            MonsterMoves[2].StatusEffectTurns = 2;
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 20f + MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 0;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Skeleton")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(20);
            hitpoints = 25f + 15 * MonsterLevel;
            speed = 2 * MonsterLevel / 3;
            PhysicalResistance = 0;
            FireResistance = -100;
            LightningResistance = 0;
            IceResistance = 0;
            PoisonResistance = 100;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = true;
            ExpDropped = 35f * MonsterLevel;
            GoldDropped = 35f * MonsterLevel;
            UsableMoves = 1;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f + 7.5f * MonsterLevel;
            MonsterMoves[1].CoolDown = 3;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 10f + 5 * MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 10f + MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 0;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 20f + MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 0;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Wraith")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(15);
            hitpoints = 40f + 14 * MonsterLevel;
            speed = (MonsterLevel * 3) / 2;
            PhysicalResistance = 100;
            FireResistance = 10;
            LightningResistance = 10;
            IceResistance = 10;
            PoisonResistance = 10;
            KnockdownResist = true;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 75f * MonsterLevel;
            GoldDropped = 75f * MonsterLevel;
            UsableMoves = 1;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 50f + 12 * MonsterLevel;
            MonsterMoves[1].CoolDown = 3;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 10f + 5 * MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 10f + MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 0;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 20f + MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 0;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Floating_Ice")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(24);
            hitpoints = 50f + 15 * MonsterLevel;
            speed = MonsterLevel / 2;
            PhysicalResistance = 20;
            FireResistance = -100;
            LightningResistance = 0;
            IceResistance = 100;
            PoisonResistance = 50;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = true;
            PoisonResist = true;
            ExpDropped = 75f * MonsterLevel;
            GoldDropped = 75f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 30f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 3;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 10f + 5 * MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Magic";
            MonsterMoves[2].Damage = 20f + MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Freeze";
            MonsterMoves[2].StatusDamage = 0;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 20f + MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 0;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Lightning_Rabbit")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(33);
            hitpoints = 40f + 5 * MonsterLevel;
            speed = (MonsterLevel * 3) / 2;
            PhysicalResistance = 0;
            FireResistance = -25;
            LightningResistance = 50;
            IceResistance = 0;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = true;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 100f * MonsterLevel;
            GoldDropped = 100f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 40f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 4;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 10f + 5 * MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 20f + 5 * MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Stun";
            MonsterMoves[2].StatusDamage = 5f;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 20f + MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 0;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Fire_Wolf")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(39);
            hitpoints = 40f + 5 * MonsterLevel;
            speed = MonsterLevel;
            PhysicalResistance = 0;
            FireResistance = 50;
            LightningResistance = -25;
            IceResistance = 50;
            PoisonResistance = -25;
            KnockdownResist = false;
            BurnResist = true;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 100f * MonsterLevel;
            GoldDropped = 100f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 40f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 3;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 10f + 5 * MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 20f + 10 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Burn";
            MonsterMoves[2].StatusDamage = 15f;
            MonsterMoves[2].StatusEffectTurns = 3;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 20f + MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 0;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Ghost_Wizard")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(66);
            hitpoints = 75f + 15 * MonsterLevel;
            speed = (MonsterLevel * 3) / 4;
            PhysicalResistance = 100;
            FireResistance = 10;
            LightningResistance = 10;
            IceResistance = 10;
            PoisonResistance = 10;
            KnockdownResist = true;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 200f * MonsterLevel;
            GoldDropped = 200f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Magic";
            MonsterMoves[1].Damage = 20f + 5 * MonsterLevel;
            MonsterMoves[1].CoolDown = 4;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Stun";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Magic";
            MonsterMoves[2].Damage = 20f + 5 * MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Burn";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 20f + 5 * MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Poison";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Tree_Giant")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(34);
            hitpoints = 150f + 20 * MonsterLevel;
            speed = MonsterLevel / 4;
            PhysicalResistance = 25;
            FireResistance = -50;
            LightningResistance = -25;
            IceResistance = -50;
            PoisonResistance = 50;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 250f * MonsterLevel;
            GoldDropped = 250f * MonsterLevel;
            UsableMoves = 1;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 100f + 25 * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Knockdown";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Magic";
            MonsterMoves[2].Damage = 20f + 5 * MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Burn";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 20f + 5 * MonsterLevel;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Poison";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Chain_Yeti")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(6);
            hitpoints = 100f + 20 * MonsterLevel;
            speed = MonsterLevel / 3;
            PhysicalResistance = 25;
            FireResistance = 25;
            LightningResistance = 25;
            IceResistance = 25;
            PoisonResistance = 25;
            KnockdownResist = true;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 250f * MonsterLevel;
            GoldDropped = 250f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 6;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Knockdown";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 100f + 20 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 20f + 5 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Freeze";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Stone_Golem")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(12);
            hitpoints = 125f + 15 * MonsterLevel;
            speed = MonsterLevel / 3;
            PhysicalResistance = 25;
            FireResistance = 25;
            LightningResistance = 25;
            IceResistance = -50;
            PoisonResistance = 50;
            KnockdownResist = false;
            BurnResist = true;
            StunResist = true;
            FreezeResist = false;
            PoisonResist = true;
            ExpDropped = 250f * MonsterLevel;
            GoldDropped = 250f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Knockdown";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 75f + 25 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 50f + 20 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Fire_Golem")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(13);
            hitpoints = 125f + 15 * MonsterLevel;
            speed = MonsterLevel / 3;
            PhysicalResistance = 0;
            FireResistance = 150;
            LightningResistance = 0;
            IceResistance = 100;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = true;
            StunResist = false;
            FreezeResist = true;
            PoisonResist = false;
            ExpDropped = 300f * MonsterLevel;
            GoldDropped = 300f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Knockdown";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 75f + 25 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 50f + 20 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Burn";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 3;
        }
        if (Name == "Ice_Golem")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(14);
            hitpoints = 125f + 15 * MonsterLevel;
            speed = MonsterLevel / 3;
            PhysicalResistance = 0;
            FireResistance = -100;
            LightningResistance = 0;
            IceResistance = 150;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = true;
            PoisonResist = false;
            ExpDropped = 250f * MonsterLevel;
            GoldDropped = 250f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Knockdown";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 75f + 25 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 50f + 20 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Freeze";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Pirate_Skeleton")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(22);
            hitpoints = 50f + 10 * MonsterLevel;
            speed = (MonsterLevel * 3) / 4;
            PhysicalResistance = 0;
            FireResistance = -50;
            LightningResistance = 0;
            IceResistance = 0;
            PoisonResistance = 100;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = true;
            ExpDropped = 100f * MonsterLevel;
            GoldDropped = 100f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 4;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Knockdown";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 30f + 15 * MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 50f + 20 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Poison_Bat")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(41);
            hitpoints = 25f + 5 * MonsterLevel;
            speed = MonsterLevel * 2;
            PhysicalResistance = 0;
            FireResistance = 0;
            LightningResistance = -50;
            IceResistance = -50;
            PoisonResistance = 150;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = true;
            ExpDropped = 200f * MonsterLevel;
            GoldDropped = 200f * MonsterLevel;
            UsableMoves = 1;
            //Move 1
            MonsterMoves[1].Type = "Magic";
            MonsterMoves[1].Damage = 50f + 15 * MonsterLevel;
            MonsterMoves[1].CoolDown = 3;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Poison";
            MonsterMoves[1].StatusDamage = 25f + 5 * MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 3;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 75f + 25 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 50f + 20 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Flower")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(49);
            hitpoints = 150f + 25 * MonsterLevel;
            speed = MonsterLevel / 3;
            PhysicalResistance = 0;
            FireResistance = -50;
            LightningResistance = 0;
            IceResistance = -50;
            PoisonResistance = 50;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 250f * MonsterLevel;
            GoldDropped = 250f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Magic";
            MonsterMoves[1].Damage = 0f + MonsterLevel;
            MonsterMoves[1].CoolDown = 4;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Knockdown";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Magic";
            MonsterMoves[2].Damage = 0f + 3 * MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Poison";
            MonsterMoves[2].StatusDamage = 40f + 10 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 3;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 50f + 20 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Rune_Crab")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(52);
            hitpoints = 100f + 20 * MonsterLevel;
            speed = MonsterLevel / 2;
            PhysicalResistance = 0;
            FireResistance = 50;
            LightningResistance = -50;
            IceResistance = 25;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = true;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 300f * MonsterLevel;
            GoldDropped = 300f * MonsterLevel;
            UsableMoves = 1;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 100f + 30 * MonsterLevel;
            MonsterMoves[1].CoolDown = 3;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 75f + 25 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 50f + 20 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Red_Block")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(60);
            hitpoints = 150f + 35 * MonsterLevel;
            speed = MonsterLevel / 3;
            PhysicalResistance = -50;
            FireResistance = 50;
            LightningResistance = -50;
            IceResistance = 50;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 350f * MonsterLevel;
            GoldDropped = 350f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 50f + 10 * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Stun";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 150f + 25 * MonsterLevel;
            MonsterMoves[2].CoolDown = 4;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Magic";
            MonsterMoves[3].Damage = 50f + 20 * MonsterLevel;
            MonsterMoves[3].CoolDown = 5;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "NA";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Fire_Dragon")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(26);
            hitpoints = 125f + 15 * MonsterLevel;
            speed = MonsterLevel;
            PhysicalResistance = 25;
            FireResistance = 100;
            LightningResistance = -25;
            IceResistance = 50;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = true;
            StunResist = false;
            FreezeResist = true;
            PoisonResist = false;
            ExpDropped = 250f * MonsterLevel;
            GoldDropped = 250f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 100f + 20 * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Magic";
            MonsterMoves[2].Damage = 150f + 35 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Burn";
            MonsterMoves[2].StatusDamage = 25f + 10 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 0f;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Knockdown";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }

        //Boss Monsters
        if (Name == "Salamangreat")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(2);
            hitpoints = 50 * MonsterLevel;
            speed = MonsterLevel / 2;
            PhysicalResistance = -25;
            FireResistance = 25;
            LightningResistance = 25;
            IceResistance = 0;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 200f * MonsterLevel;
            GoldDropped = 200f * MonsterLevel;
            UsableMoves = 1;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 7.5f * MonsterLevel;
            MonsterMoves[1].CoolDown = 2;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Magic";
            MonsterMoves[2].Damage = 150f + 35 * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Burn";
            MonsterMoves[2].StatusDamage = 25f + 10 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 0f;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Knockdown";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Giant_Slime")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(9);
            hitpoints = 35 * MonsterLevel;
            speed = MonsterLevel;
            PhysicalResistance = -25;
            FireResistance = -50;
            LightningResistance = 0;
            IceResistance = 0;
            PoisonResistance = 100;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = true;
            ExpDropped = 250f * MonsterLevel;
            GoldDropped = 250f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 7.5f * MonsterLevel;
            MonsterMoves[1].CoolDown = 3;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Magic";
            MonsterMoves[2].Damage = 2f * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Poison";
            MonsterMoves[2].StatusDamage = 5f * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 0f;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Knockdown";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Pirate_King")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(21);
            hitpoints = 30 * MonsterLevel;
            speed = 2 * MonsterLevel / 3;
            PhysicalResistance = 0;
            FireResistance = -100;
            LightningResistance = 0;
            IceResistance = 0;
            PoisonResistance = 100;
            KnockdownResist = false;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = true;
            ExpDropped = 450f * MonsterLevel;
            GoldDropped = 450f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 5f * MonsterLevel;
            MonsterMoves[1].CoolDown = 2;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 15f * MonsterLevel;
            MonsterMoves[2].CoolDown = 7;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "NA";
            MonsterMoves[2].StatusDamage = 3f * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 0f;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Knockdown";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Beast_Yeti")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(5);
            hitpoints = 75 * MonsterLevel;
            speed = MonsterLevel / 4;
            PhysicalResistance = 25;
            FireResistance = 25;
            LightningResistance = 25;
            IceResistance = 25;
            PoisonResistance = 25;
            KnockdownResist = true;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 750f * MonsterLevel;
            GoldDropped = 750f * MonsterLevel;
            UsableMoves = 2;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 10f * MonsterLevel;
            MonsterMoves[2].CoolDown = 5;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 3f * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 0f;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Knockdown";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        if (Name == "Red_Rune_Wraith")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(67);
            hitpoints = 40 * MonsterLevel;
            speed = (MonsterLevel * 3) / 2;
            PhysicalResistance = 100;
            FireResistance = 10;
            LightningResistance = 10;
            IceResistance = 10;
            PoisonResistance = 10;
            KnockdownResist = true;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 1000f * MonsterLevel;
            GoldDropped = 1000f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 35f * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 5f * MonsterLevel;
            MonsterMoves[2].CoolDown = 6;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 3f * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 0f;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Burn";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 3;
        }
        if (Name == "Blue_Rune_Wraith")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(68);
            hitpoints = 40 * MonsterLevel;
            speed = (MonsterLevel * 3) / 2; ;
            PhysicalResistance = 100;
            FireResistance = 10;
            LightningResistance = 10;
            IceResistance = 10;
            PoisonResistance = 10;
            KnockdownResist = true;
            BurnResist = false;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 1000f * MonsterLevel;
            GoldDropped = 1000f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 35f * MonsterLevel;
            MonsterMoves[1].CoolDown = 5;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 5f * MonsterLevel;
            MonsterMoves[2].CoolDown = 6;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 3f * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 0f;
            MonsterMoves[3].CoolDown = 4;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Freeze";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 3;
        }
        if (Name == "Boss_Crab")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(54);
            hitpoints = 100 * MonsterLevel;
            speed = MonsterLevel;
            PhysicalResistance = -25;
            FireResistance = 50;
            LightningResistance = -50;
            IceResistance = 50;
            PoisonResistance = 0;
            KnockdownResist = false;
            BurnResist = true;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 1500f * MonsterLevel;
            GoldDropped = 1500f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 25f * MonsterLevel;
            MonsterMoves[1].CoolDown = 6;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "Burn";
            MonsterMoves[1].StatusDamage = 6f * MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 3;
            //Move 2
            MonsterMoves[2].Type = "Physical";
            MonsterMoves[2].Damage = 15f * MonsterLevel;
            MonsterMoves[2].CoolDown = 6;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Knockdown";
            MonsterMoves[2].StatusDamage = 3f * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 25f;
            MonsterMoves[3].CoolDown = 6;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Poison";
            MonsterMoves[3].StatusDamage = 6f * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 3;
        }
        if (Name == "Black_Dragon")
        {
            MonsterSpawnerObject.GetComponent<MonsterSpawner>().SummonMonster(27);
            hitpoints = 150f * MonsterLevel;
            speed = MonsterLevel;
            PhysicalResistance = 25;
            FireResistance = 50;
            LightningResistance = -50;
            IceResistance = 50;
            PoisonResistance = -25;
            KnockdownResist = false;
            BurnResist = true;
            StunResist = false;
            FreezeResist = false;
            PoisonResist = false;
            ExpDropped = 2000f * MonsterLevel;
            GoldDropped = 2000f * MonsterLevel;
            UsableMoves = 3;
            //Move 1
            MonsterMoves[1].Type = "Physical";
            MonsterMoves[1].Damage = 40 * MonsterLevel;
            MonsterMoves[1].CoolDown = 4;
            MonsterMoves[1].TurnsTillUse = 0;
            MonsterMoves[1].StatusEffect = "NA";
            MonsterMoves[1].StatusDamage = 2f + MonsterLevel;
            MonsterMoves[1].StatusEffectTurns = 2;
            //Move 2
            MonsterMoves[2].Type = "Magic";
            MonsterMoves[2].Damage = 35 * MonsterLevel;
            MonsterMoves[2].CoolDown = 7;
            MonsterMoves[2].TurnsTillUse = 0;
            MonsterMoves[2].StatusEffect = "Burn";
            MonsterMoves[2].StatusDamage = 20 * MonsterLevel;
            MonsterMoves[2].StatusEffectTurns = 2;
            //Move 3
            MonsterMoves[3].Type = "Physical";
            MonsterMoves[3].Damage = 5f * MonsterLevel;
            MonsterMoves[3].CoolDown = 7;
            MonsterMoves[3].TurnsTillUse = 0;
            MonsterMoves[3].StatusEffect = "Knockdown";
            MonsterMoves[3].StatusDamage = 15f + 5 * MonsterLevel;
            MonsterMoves[3].StatusEffectTurns = 2;
        }
        MonsterStats.SetActive(true);
        MonsterStatText[0].text = "" + Name;
        MonsterStatText[1].text = "Level: " + MonsterLevel;
        MonsterStatText[2].text = "Max HP: " + hitpoints;
        MonsterStatText[3].text = "Speed: " + speed;
        MonsterStatText[4].text = "EXP: " + ExpDropped;
        MonsterStatText[5].text = "Gold: " + GoldDropped;
        MonsterStatText[6].text = "Physical Resistance: " + PhysicalResistance + "%           Fire Resistance: " + FireResistance + "% \nLightning Resistance: " + LightningResistance + "%         Ice Resistance: " + IceResistance + "%\nPoison Resistance: " + PoisonResistance + "%";
        MonsterStatText[7].text = "Move 1\nType: " + MonsterMoves[1].Type + "\nDMG: " + MonsterMoves[1].Damage + "\nEFF: " + MonsterMoves[1].StatusEffect + "\nEFF DMG: " + MonsterMoves[1].StatusDamage;
        if (UsableMoves >= 2) { MonsterStatText[8].text = "Move 1\nType: " + MonsterMoves[2].Type + "\nDMG: " + MonsterMoves[2].Damage + "\nEFF: " + MonsterMoves[2].StatusEffect + "\nEFF DMG: " + MonsterMoves[2].StatusDamage; }
        else { MonsterStatText[8].text = " "; }
        if (UsableMoves >= 3) { MonsterStatText[9].text = "Move 1\nType: " + MonsterMoves[3].Type + "\nDMG: " + MonsterMoves[3].Damage + "\nEFF: " + MonsterMoves[3].StatusEffect + "\nEFF DMG: " + MonsterMoves[3].StatusDamage; }
        else { MonsterStatText[9].text = " "; }
        if (KnockdownResist || BurnResist || StunResist || FreezeResist || PoisonResist)
        {
            MonsterStatText[10].text = "Resists: ";
            if (KnockdownResist)
            {
                MonsterStatText[10].text += "Knockdown";
                if (BurnResist || StunResist || FreezeResist || PoisonResist) { MonsterStatText[10].text += ", "; }
            }
            if (BurnResist)
            {
                MonsterStatText[10].text += "Burn";
                if (StunResist || FreezeResist || PoisonResist) { MonsterStatText[10].text += ", "; }
            }
            if (StunResist)
            {
                MonsterStatText[10].text += "Stun";
                if (FreezeResist || PoisonResist) { MonsterStatText[10].text += ", "; }
            }
            if (FreezeResist)
            {
                MonsterStatText[10].text += "Freeze";
                if (PoisonResist) { MonsterStatText[10].text += ", "; }
            }
            if (PoisonResist) { MonsterStatText[10].text += "Poison"; }
        }
        else { MonsterStatText[10].text = ""; }
        MonsterStats.SetActive(false);
        //float portalSize = 45f;
        //StartCoroutine(MonsterSpawnAnimation(portalSize));
    }

    IEnumerator MonsterSpawnAnimation(float Size)
    {
        Portal.SetActive(true);
        //MonsterSpawnerObject.GetComponent<MonsterSpawner>().TheMonster.transform.position = new Vector3(-25f, 0);
        for (int i = 0; i < Size; i++)
        {
            Portal.transform.localScale = new Vector2(5, i);
            yield return new WaitForSeconds(0.05f);
        }
        MonsterSpawnerObject.GetComponent<MonsterSpawner>().ChangeAnimation("Walk");

        for (int i = 0; i < 100; i++)
        {
            MonsterSpawnerObject.transform.rotation = Quaternion.Euler(0, 90 + (0.9f * i), 0);
            yield return new WaitForSeconds(0.01f);
        }

        MonsterSpawnerObject.GetComponent<MonsterSpawner>().ChangeAnimation("Idle");
        Portal.SetActive(false);
    }
}
