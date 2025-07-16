using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour
{
    public HandClicked handClicked;
    public HandRotate handRotate;
    public PlayBtn playBtn;
    public PlaceAndMove placeAndMove;

    public Button switchBtn;
    private bool switched = true;
    public Transform placedSuccessPanel;

    public Transform scaningTip;
    private bool firstTracked = false;

    // Start is called before the first frame update
    void Start()
    {
        placedSuccessPanel.gameObject.SetActive(false);
        scaningTip.gameObject.SetActive(true);
        playBtn.playBtn.gameObject.SetActive(true);
        switchBtn.gameObject.SetActive(false);

        playBtn.onPlacedEvent = () =>
        {
            scaningTip.gameObject.SetActive(false);
            StartCoroutine(nameof(ShowSuccessPanel));
        };

        switchBtn.onClick.AddListener(() =>
        {
            if (switched)
            {
                SwitchTansuo();
            }
            else
            {
                SwitchJiaohu();
            }
            switched = !switched;
        });

        handClicked.onClicked = (trans) =>
        {
            if (trans.name == "Target") 
            {
                // fix： 模型动画不够，暂不支持切换动画
                //trans.GetComponentInChildren<Animator>().SetTrigger("Play" + Random.Range(1, 4).ToString());
            }
        };
    }

    IEnumerator ShowSuccessPanel() 
    {
        placedSuccessPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        placedSuccessPanel.gameObject.SetActive(false);

        switchBtn.gameObject.SetActive(true);
        SwitchJiaohu();
    }

    public void OnTracked() 
    {
        if (!firstTracked) 
        {
            firstTracked = true;
            scaningTip.gameObject.SetActive(false);
            playBtn.playBtn.gameObject.SetActive(true);
        }
    }

    public void SwitchJiaohu() 
    {
        placeAndMove.enable = false;
        handClicked.enable = true;
        handRotate.enable = true;
        placeAndMove.Revert();
        switchBtn.transform.Find("moshiTxt").GetComponent<TMPro.TMP_Text>().text = "当前模式：交互模式";
    }

    public void SwitchTansuo()
    {
        placeAndMove.enable = true;
        handClicked.enable = false;
        handRotate.enable = false;
        switchBtn.transform.Find("moshiTxt").GetComponent<TMPro.TMP_Text>().text = "当前模式：探索模式";
    }
}
