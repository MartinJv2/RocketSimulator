using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneManager", menuName = "ScriptableObjects/SceneManager", order = 1)]
public class ManageScene : ScriptableObject
{
    public PhysicEngine physicengine;
    public floatscriptableobject altitude;
    [Serializable]
    public struct ChangeScene
    {
        public string name;
        public float minaltitudechange;
        public float maxaltitudechange;
    }
    public ChangeScene[] scenelist;
    private int _currentscene;
    public void OnEnable()
    {
        SortList();
        _currentscene = 0;
    }

    private void SortList()
    {
        Array.Sort(scenelist, delegate(ChangeScene x, ChangeScene y) { return x.minaltitudechange.CompareTo(y.minaltitudechange); });
    }

    public void SwitchedScene()
    {
        if (scenelist[_currentscene].maxaltitudechange < altitude.value && scenelist[_currentscene].maxaltitudechange != 0)
        {
            _currentscene++;
            SceneManager.LoadScene(scenelist[_currentscene].name);
        }
        else if (scenelist[_currentscene].minaltitudechange > altitude.value)
        {
            if (_currentscene == 0)
            {
                return;
            }
            _currentscene -= 1;
            SceneManager.LoadScene(scenelist[_currentscene].name);
        }
    }
}
