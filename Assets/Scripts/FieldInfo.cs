using UnityEngine;

public class FieldInfo<T>
{
    public GameObject gO;
    public T data;

    public FieldInfo(GameObject _gO, T _data)
    {
        gO = _gO;
        data = _data;
    }
}
