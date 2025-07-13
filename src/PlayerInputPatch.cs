using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StickAngleResetter;

[HarmonyPatch(typeof(PlayerInput))]
public class PlayerInputPatch
{
  static readonly Harmony harmony = new Harmony("wenright.StickAngleResetter");

  static InputControl stickAngleResetBinding;

  [HarmonyPostfix]
  [HarmonyPatch("Update")]
  public static void PostfixUpdate(PlayerInput __instance, ref float ___bladeAngleBuffer)
  {
    if (stickAngleResetBinding != null && stickAngleResetBinding.IsPressed())
    {
      __instance.BladeAngleInput.ClientValue = 0;
      ___bladeAngleBuffer = 0;
    }
  }

  public static void BindInput()
  {
    string path = Plugin.modSettings.BindingResetStickAngle;
    stickAngleResetBinding = InputSystem.FindControl(Plugin.modSettings.BindingResetStickAngle);
    if (stickAngleResetBinding == null)
    {
      Debug.LogError($"Failed to set stick angle reset binding with bind '{path}'");
    }
  }
}