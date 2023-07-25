using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;
using TMPro;

public class QuestButtonScript : MonoBehaviour
{
    public GameObject gameControllerObject;
    public int LevelID = 0;
    public int CutsceneID = 0;
    public bool Unlocked;

    public GameObject[] UnlockableLevels;

    public TextMeshProUGUI LevelText;
    // Start is called before the first frame update
    void Start()
    {
        if (!Unlocked) { gameObject.GetComponent<Button>().interactable = false; }
        LevelText.text = "" + LevelID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        gameControllerObject.GetComponent<GameController>().CreateMonsterRoster(LevelID);
    }

    public void UnlockLevels()
    {
        foreach (GameObject level in UnlockableLevels)
        {
            level.GetComponent<Button>().interactable = true;
        }
    }
}
