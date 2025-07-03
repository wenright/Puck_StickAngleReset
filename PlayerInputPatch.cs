using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StickAngleResetter;

[HarmonyPatch(typeof(PlayerInput))]
public class PlayerInputPatch : IPuckMod
{
  static readonly Harmony harmony = new Harmony("wenright.StickAngleResetter");

  private static Mouse mouse;

  [HarmonyPostfix]
  [HarmonyPatch("Start")]
  public static void PostfixStart()
  {
    mouse = Mouse.current;
  }

  [HarmonyPostfix]
  [HarmonyPatch("Update")]
  public static void PostfixUpdate(PlayerInput __instance, ref float ___bladeAngleBuffer)
  {
    if (mouse != null && mouse.middleButton.wasReleasedThisFrame)
    {
      __instance.BladeAngleInput.ClientValue = 0;
      ___bladeAngleBuffer = 0;
    }
  }

  public bool OnEnable()
  {
    try
    {
      harmony.PatchAll();
    }
    catch (Exception e)
    {
      Debug.LogError($"Harmony patch failed: {e.Message}");

      return false;
    }

    return true;
  }

  public bool OnDisable()
  {
    try
    {
      harmony.UnpatchSelf();
    }
    catch (Exception e)
    {
      Debug.LogError($"Harmony unpatch failed: {e.Message}");

      return false;
    }

    return true;
  }
}