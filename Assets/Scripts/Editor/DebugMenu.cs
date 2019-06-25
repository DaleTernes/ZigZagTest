using UnityEditor;
using UnityEngine;


public class DebugMenu : MonoBehaviour
{
    [MenuItem( "Debug/Run _F5", false, 1 )]
    static void Run()
    {
        EditorApplication.isPlaying = true;
    }


    [MenuItem( "Debug/Run _F5", true, 1 )]
    static bool CanRun()
    {
        return !EditorApplication.isPlayingOrWillChangePlaymode;
    }


    [MenuItem( "Debug/Pause _F9", false, 5 )]
    static void Pause()
    {
        EditorApplication.isPaused = !EditorApplication.isPaused;
    }


    [MenuItem( "Debug/Pause _F9", true, 5 )]
    static bool CanPause()
    {
        return EditorApplication.isPlaying;
    }


    [MenuItem( "Debug/Step _F11", false, 10 )]
    static void Step()
    {
        EditorApplication.Step();
    }


    [MenuItem( "Debug/Step _F11", true, 10 )]
    static bool CanStep()
    {
        return EditorApplication.isPlaying;
    }


    [MenuItem( "Debug/Stop _F12", false, 15 )]
    static void Stop()
    {
        EditorApplication.isPlaying = false;
    }


    [MenuItem( "Debug/Stop _F12", true, 15 )]
    static bool CanStop()
    {
        return EditorApplication.isPlayingOrWillChangePlaymode;
    }
}