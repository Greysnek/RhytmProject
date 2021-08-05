using UnityEngine;

public class Marking : MonoBehaviour
{
    [SerializeField] private GameObject mark;
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.M)) return;
        
        SetMark();
    }

    public void SetMark()
    {
        var newMark = Instantiate(mark).transform;
        newMark.position = transform.position;
    }
}
