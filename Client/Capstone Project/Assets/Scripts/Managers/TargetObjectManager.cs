using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectManager
{
    // Start is called before the first frame update
    GameObject _targetObject = null;
    Vector3 _currentMapPosition; // = new Vector3();
    Dictionary<string, GameObject> _targetDict = new Dictionary<string, GameObject>();

    public GameObject GetTargetObject(string objectName)
    {
        if (!_targetDict.TryGetValue(objectName, out _targetObject))
        {
            _targetObject = Managers.Resource.Instantiate(objectName, GameObject.Find("Island").transform);

            if (objectName.Equals("Character"))
                _targetObject.AddComponent<Character>();
            else
                _targetObject.AddComponent<BE2_TargetObject>();

            _targetDict.Add(objectName, _targetObject);
        }

        return _targetObject;
    }

    public void SetPosition(Transform transform)
    {
        _currentMapPosition = transform.position + new Vector3(0, 0.85f, 0);
    }

    public void SetMapPosition(int position)
    {

    }
    public Vector3 GetPosition()
    {
        return _currentMapPosition;
    }

    public BE2_TargetObject GetTargetObjectComponent()
    {
        return _targetObject.GetComponent<BE2_TargetObject>();
    }

    public void Clear()
    {
        _targetObject = null;
        _targetDict.Remove("Character");

    }
}
