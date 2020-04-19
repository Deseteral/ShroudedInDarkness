using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CollectedWood = 0;

    void Start()
    {
    }

    void Update()
    {
    }

    public void PickUpWood()
    {
        CollectedWood += 1;
    }
}
