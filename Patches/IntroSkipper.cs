using HarmonyLib;
using KappiMod.Utils;
using UnityEngine;
using UnityEngine.Playables;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Patches;

public static class IntroSkipper
{
    private static HarmonyLib.Harmony _harmony = null!;

    public static void Init()
    {
        KappiModCore.Loader.SceneWasInitialized += OnSceneWasInitialized;

        _harmony = new("com.introskipper.miside");
        _harmony.PatchAll(typeof(Patch));

        KappiModCore.Log("Initialized");
    }

    private static void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        if (sceneName != ObjectNames.AIHASTO_INTRO_SCENE)
        {
            return;
        }

        try
        {
            PlayableDirector? playableDirector = GameObject
                .Find("Scene")
                ?.GetComponent<PlayableDirector>();
            if (playableDirector == null)
            {
                return;
            }

            playableDirector.time = playableDirector.duration;

            KappiModCore.Log("Aihasto intro skipped");
        }
        catch (Exception e)
        {
            KappiModCore.LogError(e.Message);
        }
    }

    [HarmonyPatch]
    private static class Patch
    {
        [HarmonyPatch(typeof(Menu), "Start")]
        private static void Postfix(Menu __instance)
        {
            try
            {
                if (SceneTracker.LastSceneName == ObjectNames.ENDING_GAME_SCENE)
                {
                    return;
                }

                __instance.eventSkip.Invoke();
                __instance.SkipStart();
            }
            catch (Exception)
            {
                /*
                __instance.SkipStart() throws an exception
                but it works anyway and we ignore this exception
                */
            }

            KappiModCore.Log("The opening menu cutscene should be skipped");
        }
    }
}
