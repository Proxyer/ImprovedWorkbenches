﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using RimWorld;
using UnityEngine;
using Verse;

namespace ImprovedWorkbenches
{
    [HarmonyPatch(typeof(Bill_Production), "DoConfigInterface")]
    public static class Bill_Production_DoConfigInterface_Detour
    {
        [HarmonyPostfix]
        public static void Postfix(Bill_Production __instance, Rect baseRect, Color baseColor)
        {
            Rect copyBillRect = new Rect(baseRect.xMax - 80f, baseRect.y, 24f, 24f);
            if (Widgets.ButtonImage(copyBillRect, Resources.CopyButton, baseColor))
            {
                Main.Instance.BillCopyPasteHandler.DoCopy(__instance);
            }
			TooltipHandler.TipRegion(copyBillRect, "Copy just this bill");

            var extendedBillDataStorage = Main.Instance.GetExtendedBillDataStorage();
            if (!extendedBillDataStorage.IsLinkedBill(__instance))
                return;

            Rect breakLinkRect = new Rect(copyBillRect.xMin - 28f, baseRect.y, 24f, 24f);
            if (Widgets.ButtonImage(breakLinkRect, Resources.BreakLink, baseColor))
            {
                extendedBillDataStorage.RemoveBillFromLinkSets(__instance);
            }
            TooltipHandler.TipRegion(breakLinkRect, "Break link to other bills");

        }
    }
}