using HarmonyLib;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Patches;

public static class SkipDialogues
{
    private static bool _enabled = true;
    public static bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;

            KappiModCore.Log($"[{nameof(SkipDialogues)}] " + (_enabled ? "Enabled" : "Disabled"));
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
        private static void Prefix(Dialogue_3DText instance)
        {
            if (!Enabled)
            {
                return;
            }

            ApplySkipDialogueSettings(instance);
        }

        [HarmonyPatch(typeof(Dialogue_3DText), "Start")]
        private static void Postfix(Dialogue_3DText instance)
        {
            if (!Enabled)
            {
                return;
            }

            ApplySkipDialogueSettings(instance);
        }

        private static void ApplySkipDialogueSettings(Dialogue_3DText instance)
        {
            try
            {
                instance.dontSubtitles = true;
                instance.dontVoice = true;
                instance.timeFinish = 0;
                instance.timeShow = 0;
                instance.timePrint = 0;
                instance.timeSound = 0;
                instance.textPrint = " ";

                KappiModCore.Log($"[{nameof(SkipDialogues)}] Skipped dialogue: {instance.name}");
            }
            catch (Exception e)
            {
                KappiModCore.LogError($"[{nameof(SkipDialogues)}] {e.Message}");
            }
        }
    }
}
