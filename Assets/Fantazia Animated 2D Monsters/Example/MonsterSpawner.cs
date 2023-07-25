﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject TheMonster; //The current created monster
    SkeletonAnimation monsterAnimator; //The animator script of the monster
    //public Text monsterNameText;

    public List<GameObject> AllMonsters = new List<GameObject>(); //a list of all monsters in the asset

     int CurrentMonster = 0;

    private void Start()
    {
        //SummonARandomMonster();
    }

    public void SummonARandomMonster() //Summon a random monster from the asset
    {
        CurrentMonster = Random.Range(0, AllMonsters.Count);

        SummonNewMonster();
    }

    public void SummonMonster(int x)
    {
        CurrentMonster = x;

        SummonNewMonster();
    }

    public void SummonNextPrevMonster(int Direction) //Summon either next or previous monster
    {
        CurrentMonster += Direction;
        if (CurrentMonster >= AllMonsters.Count)
            CurrentMonster = AllMonsters.Count - 1;
        else if (CurrentMonster < 0)
            CurrentMonster = 0;

        SummonNewMonster();
    }
       
    public void SummonNewMonster()
    {
        if (TheMonster != null)
            Destroy(TheMonster);

        //Create the selected monster
        TheMonster = Instantiate(AllMonsters[CurrentMonster], transform);

        //Special dimensions for certain monsters
        string MonsterName = TheMonster.name;
        float additioanlYPos = 0;
        float scalingFactor = 1;

        if (MonsterName.Contains("Salamander"))
        {
            scalingFactor = 1.5f;
        }
        else if (MonsterName.Contains("Floating"))
        {
            scalingFactor = 0.67f;
            additioanlYPos = 12;
        }
        else if (MonsterName.Contains("Rabbit"))
        {
            scalingFactor = 0.75f;
        }
        else if (MonsterName.Contains("Mushroom"))
        {
            scalingFactor = 0.67f;
        }
        else if (MonsterName.Contains("Book"))
        {
            scalingFactor = 0.67f;
            additioanlYPos = 60;
        }

        //Change position and scale of the monster
        TheMonster.transform.position = new Vector2(transform.position.x, transform.position.y + additioanlYPos);
        TheMonster.transform.localScale = new Vector2(4, 4) * scalingFactor;

        monsterAnimator = TheMonster.GetComponent<SkeletonAnimation>();

        //string manipulation to get the name and id of the monster
        int idIndexStart = MonsterName.IndexOf('_', 1);
        int nameIndexStart = MonsterName.IndexOf('_', 9);
        int nameIndexEnd = MonsterName.IndexOf('(', 9);

        //monsterNameText.text = "Monster "+ MonsterName.Substring (idIndexStart+1,nameIndexStart-idIndexStart-1)+ " : " +MonsterName.Substring(nameIndexStart + 1, nameIndexEnd - nameIndexStart -1);
    }

    public void ChangeAnimation(string AnimationName)  //Names are: Idle, Walk, Dead and Attack
    {
        if (monsterAnimator == null)
            return;

        bool IsLoop = true;
        if (AnimationName == "Dead" || AnimationName == "Attack")
            IsLoop = false;

        //set the animation state to the selected one
        monsterAnimator.AnimationState.SetAnimation(0, AnimationName, IsLoop);
    }

    public void RateUs()
    {
        System.Diagnostics.Process.Start("https://assetstore.unity.com/packages/slug/174782");
    }

}
