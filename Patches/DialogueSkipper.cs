using System.Text;
using HarmonyLib;
using KappiMod.Config;
using UnityEngine.SceneManagement;
#if ML
using Il2Cpp;
#elif BIE
using BepInEx.IL2CPP;
#endif

namespace KappiMod.Patches;

using DialogueMapping = Dictionary<string, int>;
using DialogueSceneMappings = Dictionary<string, Dictionary<string, int>>;

public static class DialogueSkipper
{
    public static bool Enabled
    {
        get => ConfigManager.DialogueSkipper.Value;
        set
        {
            KappiModCore.Log($"[{nameof(DialogueSkipper)}] " + (value ? "Enabled" : "Disabled"));

            ConfigManager.DialogueSkipper.Value = value;
        }
    }

    private static HarmonyLib.Harmony _harmony = null!;

    private static readonly DialogueSceneMappings _ignoredDialogues = new()
    {
        {
            "Scene 7 - Backrooms",
            new DialogueMapping { { "KindMita 1 [Продолжает]", 203 }, { "KindMita 2", 204 } }
        },
        {
            "Scene 17 - Dreamer",
            new DialogueMapping
            {
                { "Mita 3", 74 },
                { "Mita 4", 75 },
                { "Mita 5", 76 },
                { "Player 7", 100 },
                { "Player 8", 101 },
                { "Mita 1(Clone)", 106 },
                { "Mita 2(Clone)", 105 },
                { "Mita 3(Clone)", 104 },
            }
        },
        {
            "Scene 14 - MobilePlayer",
            new DialogueMapping { { "Player 6", 121 }, { "Mita 1 [Шепотом]", 123 } }
        },
        {
            "Scene 15 - BasementAndDeath",
            new DialogueMapping { { "Player 1", 68 }, { "Player 2", 69 } }
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

            StringBuilder sb = new();
            string objectName = __instance.name;
            string activeSceneName = SceneManager.GetActiveScene().name;
            int indexString = __instance.indexString;
            string text = __instance.textPrint;

            if (
                _ignoredDialogues.ContainsKey(activeSceneName)
                && _ignoredDialogues[activeSceneName].ContainsKey(objectName)
                && _ignoredDialogues[activeSceneName][objectName] == indexString
            )
            {
                sb.AppendLine("\n===============================================");
                sb.AppendLine($"[{nameof(DialogueSkipper)}] Ignored dialouge: {objectName}");
                sb.AppendLine($"[{nameof(DialogueSkipper)}] Scene name: {activeSceneName}");
                sb.AppendLine($"[{nameof(DialogueSkipper)}] Index string: {indexString}");
                sb.AppendLine($"[{nameof(DialogueSkipper)}] Text: {text}");
                sb.AppendLine("===============================================");
                KappiModCore.LogWarning(sb.ToString());
                return;
            }

            ApplySkipDialogueSettings(__instance);

            sb.AppendLine("\n-----------------------------------------------");
            sb.AppendLine($"[{nameof(DialogueSkipper)}] Skipped dialogue: {objectName}");
            sb.AppendLine($"[{nameof(DialogueSkipper)}] Scene name: {activeSceneName}");
            sb.AppendLine($"[{nameof(DialogueSkipper)}] Index string: {indexString}");
            sb.AppendLine($"[{nameof(DialogueSkipper)}] Text: {text}");
            sb.AppendLine("-----------------------------------------------");
            KappiModCore.Log(sb.ToString());
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
