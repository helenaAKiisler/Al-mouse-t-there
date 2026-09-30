using UnityEngine;

public class ItemCard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ItemData itemData;
    void Start()
    {
        SetData(itemData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetData(ItemData data)
    {
        if(data == null)return;
        // GetComponent<Image>().sprite = data.Icon;
    }
}
