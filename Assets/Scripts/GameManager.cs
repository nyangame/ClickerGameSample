using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager
{
    static private GameManager _instance = new GameManager();
    static public GameManager Instance => _instance;
    private GameManager() { }
    
    int _cookieNum = 0;
    List<UpgradeData> _upgrades = new List<UpgradeData>();
    FactoryManager _factoryMan = null;

    static public int CookieNum => _instance._cookieNum;
    
    static public FactoryManager Factory => _instance._factoryMan;
    static public List<UpgradeData> UpgradeInfo => _instance._upgrades;


    public void Load()
    {
        //�f�o�b�O���Ɋy�Ȃ̂�dataPath�ɂ��Ă邯�ǁApersistentDataPath���K��
        var save = LocalData.Load<SaveData>(Application.dataPath + "/save.json");
        if(save == null)
        {
            save = new SaveData();
        }

        _cookieNum = save.CookieNum;
        
        var root = GameObject.Find("/Factory");
        _factoryMan = root.GetComponent<FactoryManager>();
        int cc = _factoryMan.transform.childCount;
        for (int i = 0; i < cc; ++i)
        {
            GameObject.Destroy(_factoryMan.transform.GetChild(i).gameObject);
        }
        _factoryMan.Setup(save.Factory);
    }

    public void Save()
    {
        SaveData save = new SaveData();

        //Load���Ă΂�Ă���K�v������B������ƃC�P�ĂȂ�
        _factoryMan.Save(save);
        save.CookieNum = _cookieNum;

        //�f�o�b�O���Ɋy�Ȃ̂�dataPath�ɂ��Ă邯�ǁApersistentDataPath���K��
        LocalData.Save<SaveData>(Application.dataPath + "/save.json", save);
    }
}
