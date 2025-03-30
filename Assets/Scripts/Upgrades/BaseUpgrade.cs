using UnityEngine;

public abstract class BaseUpgrade : MonoBehaviour
{
    public int currentLevel = 0;
    public int maxLevel = 4;
    public string title;
    public string description;
    public float cost;

    public virtual void DoUpgrade()
    {
        currentLevel++;
    }

    public bool CheckMaxLevel()
    {
        if (currentLevel >= maxLevel)
        {
            return true;
        }
        return false;
    }
}
