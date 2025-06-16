using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EPReader : MonoBehaviour
{
    public TMP_Text TextEProgress;

    public Transform SuccessPanel;
    private Transform Success;
    private Transform UnDone;

    // Start is called before the first frame update
    void Start()
    {
       

        Success = SuccessPanel.Find("Success");
        UnDone = SuccessPanel.Find("UnDone");
        SuccessPanel.gameObject.SetActive(false);
        SuccessPanel.Find("Close").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=> 
        {
            SuccessPanel.gameObject.SetActive(false);
        });
        TextEProgress.transform.parent.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=> 
        {
            var ep = CheckEP();
            SuccessPanel.gameObject.SetActive(true);
            Success.gameObject.SetActive(ep >= 6);
            UnDone.gameObject.SetActive(ep < 6);
        });

        var ep = CheckEP();
        if (ep >= 6)
        {
            var s = PlayerPrefs.GetInt("SuccessShow", 0);
            if (s <= 0)
            {
                StartCoroutine(nameof(ShowSuccess));
                PlayerPrefs.SetInt("SuccessShow", 1);
                PlayerPrefs.Save();
            }
        }

    }

    int CheckEP() 
    {
        int c = 0;
        for (int i = 1; i <= 6; i++)
        {
            var r = PlayerPrefs.GetInt("ImageTarget" + i, 0);
            if (r > 0)
            {
                c++;
                Debug.Log("Check EP " + "ImageTarget " + i);
            }
        }
        TextEProgress.text = $"EP:  {c}/6";
        return c;
    }

    IEnumerator ShowSuccess() 
    {
        yield return new WaitForSeconds(1.0f);
        SuccessPanel.gameObject.SetActive(true);
        Success.gameObject.SetActive(true);
        UnDone.gameObject.SetActive(false);
    }
}
