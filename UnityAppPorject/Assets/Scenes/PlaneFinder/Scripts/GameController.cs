using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public HandClicked handClicked;
    public HandRotate handRotate;
    public PlayBtn playBtn;
    public PlaceAndMove placeAndMove;

    public Button switchBtn;
    private bool switched = true;
    public Transform placedSuccessPanel;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        placedSuccessPanel.gameObject.SetActive(false);
        playBtn.playBtn.gameObject.SetActive(true);
        switchBtn.gameObject.SetActive(false);

        playBtn.onPlacedEvent = () =>
        {
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
                trans.GetComponentInChildren<Animator>().SetTrigger("Play" + Random.Range(1, 4).ToString());
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

    // Update is called once per frame
    void Update()
    {
        
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
