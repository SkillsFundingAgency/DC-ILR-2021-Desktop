﻿using System;
using System.Linq;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Attributes;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Extensions
{
    public static class IlrDesktopTaskKeyExtensions
    {
        public static string GetDisplayText(this IlrDesktopTaskKeys desktopTaskKey)
        {
            var type = desktopTaskKey.GetType();
            var name = Enum.GetName(type, desktopTaskKey);

            var displayTextAttribute = type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<DisplayTextAttribute>()
                .First();

            return displayTextAttribute.DisplayText;
        }
    }
}
