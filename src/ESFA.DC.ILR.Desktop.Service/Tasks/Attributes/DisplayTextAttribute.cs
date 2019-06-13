using System;

namespace ESFA.DC.ILR.Desktop.Service.Tasks.Attributes
{
    public class DisplayTextAttribute : Attribute
    {
        public DisplayTextAttribute(string displayText)
        {
            DisplayText = displayText;
        }

        public string DisplayText { get; }
    }
}
