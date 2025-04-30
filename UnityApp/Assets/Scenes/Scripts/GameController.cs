using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PetType 
{
    Pet_DogV1 = 0,
    Pet_DogV2 = 1,
    Pet_DogV3 = 2,
}

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance {
        get { return instance;  }
    }

    public HandClicked handClicked;
    public TMPro.TMP_Text HeartDataText;

    public GameObject[] DogCollections;
    public GameObject[] DogIcons;

    public GameObject DogChest;
    [HideInInspector]
    public bool isShowingTreasure;
    private PetType prePetType;
    public PetType curPetType 
    {
        set {
            if (prePetType != value) {
                prePetType = value;
                RefreshHeartText(prePetType);
            }
        }
        get { return prePetType; }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameData.InitAllData();
        
        DogChest.SetActive(false);
        isShowingTreasure = false;

        handClicked.onClicked = (Transform t) =>
        {
            if (isShowingTreasure) return;

            if (t.name == "Doggy_V1" || t.name == "Doggy_V2" || t.name == "Doggy_V3") 
            {
                var pet = t.name == "Doggy_V1" ? PetType.Pet_DogV1 :
                    t.name == "Doggy_V2" ? PetType.Pet_DogV2 : 
                    PetType.Pet_DogV3;
                var data = GameData.GetGameData(pet) + 5;
                if (data >= 100) data = 100;
                GameData.UpdateGameData(pet, data);
                RefreshHeartText(pet);
                RollClickAction();

                if (DogChest.activeSelf) { DogChest.SetActive(false); }

                bool enable = UnityEngine.Random.Range(0.0f, 1.0f) > 0.3f;
                DogChest.SetActive(enable);
                if (enable) 
                {
                    EnableComps(DogChest);
                    TipManager.Instance.ActiveTip(TipType.ChestTip, 3);
                }
            }

            if (t.name == "Chest")
            {
                DogChest.GetComponent<Animator>().Play("Open", -1, 0);
                Invoke(nameof(RollTreasure), 1.6f);
            }
        };
    }


    public void RollDog(int idx) 
    {
        for (int i = 0; i < DogCollections.Length; i++) {
            DogCollections[i].SetActive(false);
            DogIcons[i].transform.Find("icon").GetComponent<MPUIKIT.MPImage>().color = new Color(0.8f,0.8f,0.8f,1.0f);
        }
        curPetType = (PetType)idx;
        DogCollections[idx].SetActive(true);
        DogIcons[idx].transform.Find("icon").GetComponent<MPUIKIT.MPImage>().color = Color.white;
        EnableComps(DogCollections[idx]);
    }

    public void RollTreasure() 
    {
        DogChest.SetActive(false);
        isShowingTreasure = true;
        TreasuresManager.Instance.ActiveRandomTreasure();
    }

    public void RollClickAction() 
    {
        var idx = UnityEngine.Random.Range(0, 4);
        switch (idx)
        {
            case 0:
                DogCollections[(int)curPetType].GetComponent<Animator>().SetTrigger("Run");
                break;
            case 1:
                DogCollections[(int)curPetType].GetComponent<Animator>().SetTrigger("Jump");
                break;
            case 2:
                DogCollections[(int)curPetType].GetComponent<Animator>().SetTrigger("Hit");
                break;
            case 3:
                DogCollections[(int)curPetType].GetComponent<Animator>().SetTrigger("Slide");
                break;
        }
    }

    public void RefreshHeartText(PetType pet) 
    {
        HeartDataText.text = GameData.GetGameData(pet).ToString();
    }

    private void EnableComps(GameObject gameObject) 
    {
#if UNITY_EDITOR
        var renders = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var r in renders)
        {
            r.enabled = true;
        }
        var mrenders = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var m in mrenders)
        {
            m.enabled = true;
        }
        var colliders = gameObject.GetComponentsInChildren<BoxCollider>();
        foreach (var c in colliders)
        {
            c.enabled = true;
        }
#endif
    }
}
