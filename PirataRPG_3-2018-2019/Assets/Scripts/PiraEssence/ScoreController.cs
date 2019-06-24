using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    Dictionary<string, int> essenceScores = 
        new Dictionary<string, int> {   { "SeaweedEssence", 0 },
                                        { "OrangeEssence", 0 },
                                        { "LemonEssence", 0 },
                                        { "GrapeEssence", 0 },
                                        { "CherryEssence", 0 },
                                        { "BananaEssence", 0 }};

    public TextMesh SeaweedText;
    public TextMesh OrangeText;
    public TextMesh LemonText;
    public TextMesh GrapeText;
    public TextMesh CherryText;
    public TextMesh BananaText;

    // Start is called before the first frame update
    void Start()
    {
        ResetTextScores();
    }

    void ResetTextScores()
    {
        SeaweedText.text = essenceScores["SeaweedEssence"].ToString();
        OrangeText.text = essenceScores["OrangeEssence"].ToString();
        LemonText.text = essenceScores["LemonEssence"].ToString();
        GrapeText.text = essenceScores["GrapeEssence"].ToString();
        CherryText.text = essenceScores["CherryEssence"].ToString();
        BananaText.text = essenceScores["BananaEssence"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEssence(string essenceTag)
    {
        essenceScores[essenceTag]++;

        switch(essenceTag)
        {
            case "SeaweedEssence":
                SeaweedText.text = essenceScores[essenceTag].ToString();
                break;
            case "OrangeEssence":
                OrangeText.text = essenceScores[essenceTag].ToString();
                break;
            case "LemonEssence":
                LemonText.text = essenceScores[essenceTag].ToString();
                break;
            case "GrapeEssence":
                GrapeText.text = essenceScores[essenceTag].ToString();
                break;
            case "CherryEssence":
                CherryText.text = essenceScores[essenceTag].ToString();
                break;
            case "BananaEssence":
                BananaText.text = essenceScores[essenceTag].ToString();
                break;
        }
    }
}
