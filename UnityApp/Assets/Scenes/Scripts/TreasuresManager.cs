using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasuresManager : MonoBehaviour
{
    private static TreasuresManager instance;
    public static TreasuresManager Instance
    {
        get { return instance; }
    }

    public Transform treasureParent;
    public Transform treasureBg;
    public Transform treasureClose;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitAllTreasures();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAllTreasures() 
    {
        var count = treasureParent.childCount;
        for (int i = 0; i < count; i++) 
        {
            treasureParent.GetChild(i).gameObject.SetActive(false);
        }
        treasureBg.gameObject.SetActive(false);
        treasureClose.gameObject.SetActive(false);
    }

    public void ActiveRandomTreasure() 
    {
        var i = UnityEngine.Random.Range(0, treasureParent.childCount);
        treasureParent.GetChild(i).gameObject.SetActive(true);
        treasureBg.gameObject.SetActive(true);
        treasureClose.gameObject.SetActive(true);
    }

    public void ClosePanel() 
    {
        InitAllTreasures();
        GameController.Instance.isShowingTreasure = false;
    }
}
