using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Information")]
    public int health;
    public int buildingMaterials = 0;
    public TextMeshProUGUI numMaterialsText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numMaterialsText.text = buildingMaterials.ToString();

        if (Input.GetKeyDown(KeyCode.M)){
            buildingMaterials += 9999;
        }
    }

    public void recieveMaterials(int droppedMaterials){
        buildingMaterials += droppedMaterials;
    }
}
