using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DanceLister : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        TestGet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Test Get")]
    public async void TestGet()
    {
        //var result = await APIServices.GetSongInfo("19267ae9-3421-4c0c-a587-0f914c145c8a");
        var result = await APIServices.GetSongInfoList();

        Debug.Log(result);
        Debug.Log(result.code);
    }
}
