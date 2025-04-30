using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{

    public static Dictionary<PetType, int> PetsHeartData = new Dictionary<PetType, int>();

    public static void InitAllData() 
    {
        PetsHeartData.Clear();
        PetsHeartData.Add(PetType.Pet_DogV1, 0);
        PetsHeartData.Add(PetType.Pet_DogV2, 0);
        PetsHeartData.Add(PetType.Pet_DogV3, 0);
        var d = PlayerPrefs.GetInt("vdata1", 0);
        PetsHeartData[PetType.Pet_DogV1] = d;
        PetsHeartData[PetType.Pet_DogV2] = PlayerPrefs.GetInt("vdata2", 0);
        PetsHeartData[PetType.Pet_DogV3] = PlayerPrefs.GetInt("vdata3", 0);
    }

    public static int GetGameData(PetType pet)
    {
       return PetsHeartData[pet];
    }

    public static void UpdateGameData(PetType pet, int data)
    {
        PetsHeartData[pet] = data;
        PlayerPrefs.SetInt(pet == PetType.Pet_DogV1 ? "vdata1" :
            pet == PetType.Pet_DogV2 ? "vdata2" :
            pet == PetType.Pet_DogV3 ? "vdata3" :
            "", 0);
    }
}
