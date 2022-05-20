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
        //var result = await APIServices.GetSongInfoList();
        //var result = await APIServices.GetDanceInfo("e289043b-d156-4652-ae63-85f489f66481");
        var result = await APIServices.GetDanceData("e289043b-d156-4652-ae63-85f489f66481");

        Debug.Log(result);
        //Debug.Log(result.code);

    }
}
