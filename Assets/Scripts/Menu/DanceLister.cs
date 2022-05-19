using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DanceLister : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        APIServices.SongInfo song = APIServices.GetSongInfo("19267ae9-3421-4c0c-a587-0f914c145c8a");
        Debug.Log(song.code);
        APIServices.InfoList<APIServices.SongInfo> list = APIServices.GetSongInfoList();
        Debug.Log(list.code);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
