using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    static private SceneManager _this;
    static public SceneManager Instance
    {
        get
        {
            if(_this == null)
            {
                _this = GameObject.Find("SceneManager").GetComponent<SceneManager>();
            }
            return _this;
        }
    }
    
    [HideInInspector]
    public SceneScript currentScene;

    //제작 과정용
    public enum SceneState
    {
        None = 0,

        MainScene,

        Max
    }
    public SceneState sceneState;


    void OnEnable()
    {
        //Debug.Log("SceneManager Enable");
    }

    void OnDisable()
    {
        //Debug.Log("SceneManager Disabled");
        _this = null;
        currentScene = null;
    }

    void Start ()
    {
        //Debug.Log("SceneManager Start");
        InitCurScene();

       

        

        

    }
    private void InitCurScene()
    {

        if( transform.childCount > 0)
        {
            currentScene = transform.GetChild(0).GetComponent<SceneScript>();
        }
        else
        {
            GameObject scenePrefab = Resources.Load<GameObject>("Scenes/" + sceneState.ToString());
            if(scenePrefab == null)
            {
                scenePrefab = Resources.Load<GameObject>("Scenes/MainScene");
            }

            GameObject sceneObj = Instantiate(scenePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            sceneObj.transform.parent = this.transform;
            sceneObj.transform.name = Util.StrUtils.Cut(sceneObj.transform.name, "(Clone)");
            currentScene = sceneObj.GetComponent<SceneScript>();
        }
        

    }
	
    


	
	void Update ()
    {
	
	}
}
