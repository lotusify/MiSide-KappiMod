using HarmonyLib;
using KappiMod.Config;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Patches;

public static class SkipDialogues
{
    public static bool Enabled
    {
        get => ConfigManager.SkipDialogues.Value;
        set
        {
            ConfigManager.SkipDialogues.Value = value;

            KappiModCore.Log($"[{nameof(SkipDialogues)}] " + (value ? "Enabled" : "Disabled"));
        }
    }

    private static HarmonyLib.Harmony _harmony = null!;

    public static void Init()
    {
        _harmony = new("com.kappi.mod.skipdialogues");
        _harmony.PatchAll(typeof(Patch));

        KappiModCore.Log($"[{nameof(SkipDialogues)}] Initialized");
    }

    [HarmonyPatch]
    private static class Patch
    {
        [HarmonyPatch(typeof(Dialogue_3DText), "Start")]
        private static void Prefix(Dialogue_3DText __instance)
        {
            if (!Enabled)
            {
                return;
            }

            ApplySkipDialogueSettings(__instance);
        }

        [HarmonyPatch(typeof(Dialogue_3DText), "Start")]
        private static void Postfix(Dialogue_3DText __instance)
        {
            if (!Enabled)
            {
                return;
            }

            ApplySkipDialogueSettings(__instance);

            KappiModCore.Log($"[{nameof(SkipDialogues)}] Skipped dialogue: {__instance.name}");
        }

        private static void ApplySkipDialogueSettings(Dialogue_3DText __instance)
        {
            try
            {
                __instance.dontSubtitles = true;
                __instance.dontVoice = true;
                __instance.timeFinish = 0;
                __instance.timeShow = 0;
                __instance.timePrint = 0;
                __instance.timeSound = 0;
                __instance.textPrint = " ";
            }
            catch (Exception e)
            {
                KappiModCore.LogError($"[{nameof(SkipDialogues)}] {e.Message}");
            }
        }
    }
}
