using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalentEditorScript : MonoBehaviour
{
    public GameObject playerObject;

    public TextMeshProUGUI HitPointsLevelText;
    public TextMeshProUGUI PhysicalAttackLevelText;
    public TextMeshProUGUI PhysicalDefenceLevelText;
    public TextMeshProUGUI MagicAttackLevelText;
    public TextMeshProUGUI MagicDefenceLevelText;
    public TextMeshProUGUI SpeedLevelText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        HitPointsLevelText.text = "HP\nLVL: " + playerObject.GetComponent<PlayerScript>().HitPointsLevel;
        PhysicalAttackLevelText.text = "PHYS ATK\nLVL: " + playerObject.GetComponent<PlayerScript>().PhysicalAttackLevel;
        PhysicalDefenceLevelText.text = "PHYS DEF\nLVL: " + playerObject.GetComponent<PlayerScript>().PhysicalDefenseLevel;
        MagicAttackLevelText.text = "MAGIC ATK\nLVL: " + playerObject.GetComponent<PlayerScript>().MagicAttackLevel;
        MagicDefenceLevelText.text = "MAGIC DEF\nLVL: " + playerObject.GetComponent<PlayerScript>().MagicDefenceLevel;
        SpeedLevelText.text = "SPEED\nLVL: " + playerObject.GetComponent<PlayerScript>().SpeedLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseTalent(string Name)
    {
        playerObject.GetComponent<PlayerScript>().IncreaseTalent(Name);
        HitPointsLevelText.text = "HP\nLVL: " + playerObject.GetComponent<PlayerScript>().HitPointsLevel;
        PhysicalAttackLevelText.text = "PHYS ATK\nLVL: " + playerObject.GetComponent<PlayerScript>().PhysicalAttackLevel;
        PhysicalDefenceLevelText.text = "PHYS DEF\nLVL: " + playerObject.GetComponent<PlayerScript>().PhysicalDefenseLevel;
        MagicAttackLevelText.text = "MAGIC ATK\nLVL: " + playerObject.GetComponent<PlayerScript>().MagicAttackLevel;
        MagicDefenceLevelText.text = "MAGIC DEF\nLVL: " + playerObject.GetComponent<PlayerScript>().MagicDefenceLevel;
        SpeedLevelText.text = "SPEED\nLVL: " + playerObject.GetComponent<PlayerScript>().SpeedLevel;
    }

    public void DecreaseTalent(string Name)
    {
        playerObject.GetComponent<PlayerScript>().DecreaseTalent(Name);
        HitPointsLevelText.text = "HP\nLVL: " + playerObject.GetComponent<PlayerScript>().HitPointsLevel;
        PhysicalAttackLevelText.text = "PHYS ATK\nLVL: " + playerObject.GetComponent<PlayerScript>().PhysicalAttackLevel;
        PhysicalDefenceLevelText.text = "PHYS DEF\nLVL: " + playerObject.GetComponent<PlayerScript>().PhysicalDefenseLevel;
        MagicAttackLevelText.text = "MAGIC ATK\nLVL: " + playerObject.GetComponent<PlayerScript>().MagicAttackLevel;
        MagicDefenceLevelText.text = "MAGIC DEF\nLVL: " + playerObject.GetComponent<PlayerScript>().MagicDefenceLevel;
        SpeedLevelText.text = "SPEED\nLVL: " + playerObject.GetComponent<PlayerScript>().SpeedLevel;
    }
}
