using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCatalog : MonoBehaviour
{
    public int CategoryCount()
    {
        return transform.childCount;
    }
    public GameObject GetCategory(int index)
    {
        if (index < 0 || index >= transform.childCount)
            return null;
        return transform.GetChild(index).gameObject;
    }
    public int SkillCount(GameObject category)
    {
        return category != null ? category.transform.childCount : 0;
    }
    public Skill GetSkill(int categoryIndex, int skillIndex)
    {
        GameObject category = GetCategory(categoryIndex);
        if (category == null || skillIndex < 0 || skillIndex >= category.transform.childCount)
            return null;
        return category.transform.GetChild(skillIndex).GetComponent<Skill>();
    }
}
