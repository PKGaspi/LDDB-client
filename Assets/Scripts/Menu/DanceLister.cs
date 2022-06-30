using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using APITypes;

public class DanceLister : MonoBehaviour
{
    public GameObject menuDanceEntryPrefab;
    public ScrollRect menuScrollRect;
    public Text messageText;
    public bool autoLoad;
    // Start is called before the first frame update
    async void Start()
    {
        messageText.text = "Connecting to server...";
        var status = await APIServices.GetStatus();
        messageText.text = "";
        switch (status.code) {
            case 200: 
                if (autoLoad)
                    Load();
                break;
            default:
                messageText.text = $"Error:\n{status.message}\n(code {status.code})";
                break;
        }
    }

    async void Load() {
        messageText.text = "Loading Dances...";
        InfoList<DanceInfo> result = await APIServices.GetDanceInfoList();
        // Empty menu first
        foreach (Transform entry in menuScrollRect.content) {
            GameObject.Destroy(entry);
        }
        foreach (DanceInfo dance in result.list) {
            CreateMenuDanceEntry(dance);
        }
        messageText.text = "";
    }

    private void CreateMenuDanceEntry(DanceInfo dance) {
        var entry = Instantiate(menuDanceEntryPrefab, menuScrollRect.content);
        entry.GetComponent<MenuDanceEntry>().SetDance(dance);
    }

    
    public void ShowMessage(string msg) {
        menuScrollRect.transform.parent.gameObject.SetActive(false);
        messageText.text = msg;
    }
}
