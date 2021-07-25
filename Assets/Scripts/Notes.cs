using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{

public class Notes :MonoBehaviour
{
    // Notes

    // Packages:
    //Unity adds
    //analytics
    //In app purchasing
    //rider editor
    //-package manager UI
    //-probuilder
    //-test framework
    //-textmeshpro
    //-timeline
    //-unity collaborate
    //-unityui
    //-vs editor
    //-quicksearch

    // Tutorial publish webgl
    // issue with Cors - disabled with shortcut to browser (chrome = disable-web-security)
    // issue with Gzip compression format - made uncompressed
    // issue with WASM fail - ?


    //Unity code
    #region findScript

    void FindMyScript(string GameObjectName, string ScriptName) // pass game object name that has script
    {
        //private MyAnimationScript someScript;
        //someScript = GameObject.Find($"{Name}").GetComponent<$"{scriptName}">();

    }

    // private MyAnimationScript someScript;
    // ScriptThatYouWant = GameObject.Find("[GameObjectName to which target script is attached]").GetComponent <[Name of target script] > ();
    class MyAnimationScript
    {
        string PlayScript()
        {
            return "hi";
        }
    }
    #endregion
}
}