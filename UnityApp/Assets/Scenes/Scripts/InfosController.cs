using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InfosController : MonoBehaviour
{
    private Button JianjieBtn;
    private Button StaticPoseBtn;
    private Button FlyPoseBtn;

    private Transform JianjieInfo;

    public Transform CubeTarget;

    // Start is called before the first frame update
    void Start()
    {
        JianjieInfo = transform.Find("JianjieInfo");

        JianjieBtn = transform.Find("Jianjie").GetComponent<Button>();
        StaticPoseBtn = transform.Find("StaticPose").GetComponent<Button>();
        FlyPoseBtn = transform.Find("FlyPose").GetComponent<Button>();

        JianjieInfo.gameObject.SetActive(false);

        JianjieBtn.onClick.AddListener(()=> {
            JianjieInfo.gameObject.SetActive(!JianjieInfo.gameObject.activeSelf);
        });

        StaticPoseBtn.onClick.AddListener(() => {
            CubeTarget.GetComponentInChildren<Animator>().Play("idle", -1, 0);
        });

        FlyPoseBtn.onClick.AddListener(() => {
            CubeTarget.GetComponentInChildren<Animator>().Play("fly", -1, 0);
        });

    }

    // Update is called once per frame
    void Update() { }
}
