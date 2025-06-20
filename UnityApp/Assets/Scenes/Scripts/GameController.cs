using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TrackingManager trackingMgr1;
    public TrackingManager trackingMgr2;
    public TrackingManager trackingMgr3;
    public TrackingManager trackingMgr4;

    public HandClicked handClicked;
    public HandMove handMove;
    private Transform clickedTarget;
    private List<string> movedTarget = new List<string>();

    public Transform Target1;
    public Transform Target2;

    public Transform TargetPanel1;
    public Transform TargetPanel2;
    public Transform TargetPanel3;
    public Transform TargetPanel4;

    public AudioSource audioGoodJob;
    public AudioSource audioCongratulations;

    // Start is called before the first frame update
    void Start()
    {
        TargetPanel1.gameObject.SetActive(false);
        TargetPanel2.gameObject.SetActive(false);
        TargetPanel3.gameObject.SetActive(false);
        TargetPanel4.gameObject.SetActive(false);
        TargetPanel1.GetComponentInChildren<Button>().onClick.AddListener(()=> 
        {
            // 取消选中状态
            TargetPanel1.gameObject.SetActive(false);
            if (clickedTarget != null)
            {
                var heal = clickedTarget.Find("FX_Heal_02");
                if (heal) heal.gameObject.SetActive(false);
            }
        });
        TargetPanel3.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
        {
            // 取消选中状态
            TargetPanel3.gameObject.SetActive(false);
        });
        TargetPanel3.Find("Volume").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (clickedTarget != null) 
            {
                clickedTarget.GetComponent<AudioSource>().Play();
            }
        });
        trackingMgr1.onTracked += (Transform trans) =>
        {
            clickedTarget = null;
            TargetPanel1.gameObject.SetActive(false);
            var Center = Target1.Find("Center");
            Center.Find("statue_gandhara/FX_Heal_02").gameObject.SetActive(false);
            Center.Find("statue_thrower/FX_Heal_02").gameObject.SetActive(false);
            Center.Find("law_hammurabi/FX_Heal_02").gameObject.SetActive(false);
            Center.Find("oracle_bone/FX_Heal_02").gameObject.SetActive(false);
            Center.Find("pyramid/FX_Heal_02").gameObject.SetActive(false);
        };
        trackingMgr1.onLost += (Transform trans) => 
        {
            TargetPanel1.gameObject.SetActive(false);
        };
        trackingMgr2.onTracked += (Transform trans) =>
        {
            var red = Target2.Find("Red");
            red.gameObject.SetActive(false);
            var white = Target2.Find("White");
            white.gameObject.SetActive(false);
            var black = Target2.Find("Black");
            black.gameObject.SetActive(false);
            var brown = Target2.Find("Brown");
            brown.gameObject.SetActive(false);
            var yellow = Target2.Find("Yellow");
            yellow.gameObject.SetActive(false);

            var brush_red = Target2.Find("brush_red");
            brush_red.localPosition = new Vector3(0.26f, 0.05f, -0.726f);
            var brush_white = Target2.Find("brush_white");
            brush_white.localPosition = new Vector3(0.13f, 0.05f, -0.726f);
            var brush_black = Target2.Find("brush_black");
            brush_black.localPosition = new Vector3(0.0f, 0.05f, -0.726f);
            var brush_brown = Target2.Find("brush_brown");
            brush_brown.localPosition = new Vector3(-0.13f, 0.05f, -0.726f);
            var brush_yellow = Target2.Find("brush_yellow");
            brush_yellow.localPosition = new Vector3(-0.26f, 0.05f, -0.726f);

            TargetPanel2.gameObject.SetActive(true);
            TargetPanel2.Find("Tip").gameObject.SetActive(true);
            TargetPanel2.Find("Success").gameObject.SetActive(false);

            movedTarget.Clear();
        };
        trackingMgr2.onLost += (Transform trans) =>
        {
            TargetPanel2.gameObject.SetActive(false);
        };
        trackingMgr3.onTracked += (Transform trans) =>
        {
            clickedTarget = null;
            TargetPanel3.gameObject.SetActive(false);
        };
        trackingMgr3.onLost += (Transform trans) =>
        {
            TargetPanel3.gameObject.SetActive(false);
        };
        trackingMgr4.onTracked += (Transform trans) =>
        {
            TargetPanel4.gameObject.SetActive(true);
        };
        trackingMgr4.onLost += (Transform trans) =>
        {
            TargetPanel4.gameObject.SetActive(false);
        };

        handMove.handMoveFinished += (string objname) =>
        {
            if (!movedTarget.Contains(objname)) 
            {
                movedTarget.Add(objname);
                if (movedTarget.Count >= 5)
                {
                    audioCongratulations.Play();
                    TargetPanel2.gameObject.SetActive(true);
                    TargetPanel2.Find("Tip").gameObject.SetActive(false);
                    TargetPanel2.Find("Success").gameObject.SetActive(true);
                }
                else {
                    audioGoodJob.Play();
                }
            }
        };

        handClicked.onClicked += (Transform trans) =>
        {
            Debug.Log("handClicked.onClicked --- " + trans.name);
            // 场景1 特效
            if (trans.name == "statue_gandhara" || trans.name == "statue_thrower" ||
                trans.name == "law_hammurabi" || trans.name == "oracle_bone" ||
                trans.name == "pyramid")
            {
                if (clickedTarget != null) 
                {
                    if (clickedTarget.name != trans.name)
                    {
                        var heal = clickedTarget.Find("FX_Heal_02");
                        if(heal) heal.gameObject.SetActive(false);
                    }
                }
                if (clickedTarget == null || clickedTarget.name != trans.name)
                {
                    var heal = trans.Find("FX_Heal_02");
                    if (heal) heal.gameObject.SetActive(true);

                    TargetPanel1.gameObject.SetActive(true);
                    TargetPanel1.Find("MPImage").GetComponentInChildren<TMPro.TMP_Text>().text =
                        trans.name == "statue_gandhara" ? "Gandhara Buddha statues - Ancient India" :
                        trans.name == "statue_thrower" ? "Discobolus - Ancient Greece" :
                        trans.name == "law_hammurabi" ? "Code of Hammurabi - Ancient Babylon" :
                        trans.name == "oracle_bone" ? "Oracle Bone Inscriptions - Ancient China" :
                        trans.name == "pyramid" ? "Pyramid - Ancient Egypt" : "";

                    clickedTarget = trans;
                }
            }

            if (trans.name == "2-1" || trans.name == "2-2" || trans.name == "2-3" || trans.name == "2-4") 
            {
                if (clickedTarget == null || clickedTarget.name != trans.name) 
                {
                    TargetPanel3.gameObject.SetActive(true);
                    TargetPanel3.Find("MPImage").GetComponentInChildren<TMPro.TMP_Text>().text =
                        trans.name == "2-1" ? "Ra is the sun god in ancient Egyptian culture." :
                        trans.name == "2-2" ? "Nun is the water in ancient Egyptian culture." :
                        trans.name == "2-3" ? "Ankh means life, eternity and  kingship in ancient Egyptian culture." :
                        trans.name == "2-4" ? "Ḥtp means peace, tranquility, and contentment in ancient Egyptian culture." : ""; ;

                    clickedTarget = trans;
                }
            }
        };

        //Ra is the sun god in ancient Egyptian culture.
        //Nun is the water in ancient Egyptian culture.
        //Ankh means life, eternity and  kingship in ancient Egyptian culture.
        //Ḥtp means peace, tranquility, and contentment in ancient Egyptian culture.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
