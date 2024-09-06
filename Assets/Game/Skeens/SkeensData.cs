using UnityEngine;

[CreateAssetMenu(fileName = "SkeensData", menuName = "Skeens/SkeensData")]
public class SkeensData : ScriptableObject
{
    [SerializeField] private Skeen[] skeens;
    
    public AnimatorOverrideController GetAnimator(string Name)
    {
        for (int i = 0; i < skeens.Length; i++)
        {
            if(skeens[i].Name == Name)
            {
                return skeens[i].animatorOverride;
            }
        }
        return null;

    }
}
[System.Serializable]
public struct Skeen
{
    public string Name;
    public AnimatorOverrideController animatorOverride;
}