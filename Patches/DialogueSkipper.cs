using HarmonyLib;
using KappiMod.Config;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Patches;

public static class DialogueSkipper
{
    public static bool Enabled
    {
        get => ConfigManager.SkipDialogues.Value;
        set
        {
            KappiModCore.Log($"[{nameof(DialogueSkipper)}] " + (value ? "Enabled" : "Disabled"));

            ConfigManager.SkipDialogues.Value = value;
        }
    }

    private static HarmonyLib.Harmony _harmony = null!;

    private static readonly Dictionary<string, List<int>> _ignoredDialoguesId = new()
    {
        {
            "Player 1",
            new() { 71 }
        },
        {
            "Player 2",
            new() { 72 }
        },
        {
            "Player 4",
            new() { 71 }
        },
        {
            "Player 5",
            new() { 72 }
        },
        {
            "Player 6",
            new() { 121 }
        },
        {
            "Mita 1 [Шепотом]",
            new() { 123 }
        },
        {
            "Mita 2",
            new() { 124 }
        },
        {
            "Mita 3",
            new() { 74, 125, 117 }
        },
        {
            "Mita 4",
            new() { 75, 118 }
        },
        {
            "Mita 5",
            new() { 76, 119 }
        },
    };

    public static void Init()
    {
        _harmony = new("com.kappi.mod.skipdialogues");
        _harmony.PatchAll(typeof(Patch));

        KappiModCore.Log($"[{nameof(DialogueSkipper)}] Initialized");
    }

    [HarmonyPatch]
    private static class Patch
    {
        [HarmonyPatch(typeof(Dialogue_3DText), "Start")]
        private static void Prefix(Dialogue_3DText __instance)
        {
            SkipDialouge(__instance);
        }

        [HarmonyPatch(typeof(Dialogue_3DText), "Start")]
        private static void Postfix(Dialogue_3DText __instance)
        {
            SkipDialouge(__instance);
        }

        private static void SkipDialouge(Dialogue_3DText __instance)
        {
            if (!Enabled)
            {
                return;
            }

            string text = __instance.textPrint;

            if (
                _ignoredDialoguesId.ContainsKey(__instance.name)
                && _ignoredDialoguesId[__instance.name].Contains(__instance.indexString)
            )
            {
                KappiModCore.LogWarning(
                    $"[{nameof(DialogueSkipper)}] Ignored dialouge: {__instance.name}"
                );
                KappiModCore.LogWarning($"[{nameof(DialogueSkipper)}] Text: {text}");
                return;
            }

            ApplySkipDialogueSettings(__instance);

            KappiModCore.Log($"----------------------------------------");
            KappiModCore.Log($"[{nameof(DialogueSkipper)}] Skipped dialogue: {__instance.name}");
            KappiModCore.Log($"[{nameof(DialogueSkipper)}] Index: {__instance.indexString}");
            KappiModCore.Log($"[{nameof(DialogueSkipper)}] Text: {text}");
            KappiModCore.Log($"----------------------------------------");
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
                KappiModCore.LogError($"[{nameof(DialogueSkipper)}] {e.Message}");
            }
        }
    }
}
