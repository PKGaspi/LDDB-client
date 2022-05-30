using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using APITypes;

public class DanceLister : MonoBehaviour
{
    public GameObject menuDanceEntryPrefab;
    public Transform listMenu;
    // Start is called before the first frame update
    async void Start()
    {
        InfoList<DanceInfo> result = await APIServices.GetDanceInfoList();
        Debug.Log(result.list);
        Debug.Log(result.list.Count);
        foreach (DanceInfo dance in result.list) {
            Debug.Log(dance.song.name);
            Debug.Log(dance.song.author);
            Debug.Log(dance.song.author.name);
            CreateMenuDanceEntry(dance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateMenuDanceEntry(DanceInfo dance) {
        var entry = Instantiate(menuDanceEntryPrefab, listMenu);
        entry.GetComponent<MenuDanceEntry>().SetDance(dance);
    }
}
