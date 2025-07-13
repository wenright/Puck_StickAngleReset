using System;
using HarmonyLib;
using UnityEngine;

namespace StickAngleResetter;

public class Plugin : IPuckMod
{
  public static string MOD_NAME = "StickAngleResetter";
  public static string MOD_VERSION = "0.2.0";
  public static string MOD_GUID = "wenright.StickAngleResetter";

  static readonly Harmony harmony = new Harmony(MOD_GUID);

  public static ModSettings modSettings;

  public bool OnEnable()
  {
    try
    {
      harmony.PatchAll();

      Debug.Log($"Enabled {MOD_GUID}");

      modSettings = ModSettings.Load();
      modSettings.Save();
      PlayerInputPatch.BindInput();

      return true;
    }
    catch (Exception e)
    {
      Debug.LogError($"Failed enabling {MOD_GUID}: {e}");
      return false;
    }
  }

  public bool OnDisable()
  {
    try
    {
      harmony.UnpatchSelf();
      return true;
    }
    catch (Exception e)
    {
      Debug.LogError($"Failed to disable {MOD_GUID}: {e.Message}!");
      return false;
    }
  }
}