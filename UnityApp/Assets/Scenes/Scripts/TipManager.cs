using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipType 
{
    None = -1,
    FindingPlane = 0,
    TouchTip = 1,
    ChestTip = 2
}


public class TipManager : MonoBehaviour
{
    private static TipManager instance;
    public static TipManager Instance
    {
        get { return instance; }
    }

    public Transform TipParent;
    private TipType curTipType = TipType.None;
    private Dictionary<TipType, Transform> TipDic = new Dictionary<TipType, Transform>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        TipDic.Clear();
        TipDic.Add(TipType.FindingPlane, TipParent.Find("FindingPlane"));
        TipDic.Add(TipType.TouchTip, TipParent.Find("TouchTip"));
        TipDic.Add(TipType.ChestTip, TipParent.Find("ChestTip"));

        ClearTip();
        ActiveTip(TipType.FindingPlane, -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearTip()
    {
        foreach(var tip in TipDic)
        {
            tip.Value.gameObject.SetActive(false);
        }
        TipParent.GetComponent<MPUIKIT.MPImage>().enabled = false;
    }

    public void ActiveTip(TipType tipType, float lasttime = 3) 
    {
        if (curTipType != tipType) 
        {
            if(TipDic.ContainsKey(curTipType)) TipDic[curTipType].gameObject.SetActive(false);
            curTipType = tipType;
            TipParent.GetComponent<MPUIKIT.MPImage>().enabled = true;
            if (TipDic.ContainsKey(curTipType)) TipDic[curTipType].gameObject.SetActive(true);
            if (lasttime > 0) {
                Invoke(nameof(CloseCurTip), lasttime);
            }
        }
    }

    public void CloseCurTip() 
    {
        if (TipDic.ContainsKey(curTipType)) TipDic[curTipType].gameObject.SetActive(false);
        TipParent.GetComponent<MPUIKIT.MPImage>().enabled = false;
        curTipType = TipType.None;
    }
}
