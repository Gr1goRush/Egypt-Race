using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeenSetup 
{
    public AnimatorOverrideController ArrowController { get; private set; }
    public AnimatorOverrideController CharacterController { get; private set; }
    public AnimatorOverrideController ChariotController { get; private set; }

    public SkeenSetup(SkeensData arrowData, SkeensData characterData, SkeensData chariotData)
    {
        ArrowController = arrowData.GetAnimator(Saver.GetString("Arrow", "0"));
        CharacterController = characterData.GetAnimator(Saver.GetString("Character", "0"));
        ChariotController = chariotData.GetAnimator(Saver.GetString("Chariot", "0"));
    }

}
