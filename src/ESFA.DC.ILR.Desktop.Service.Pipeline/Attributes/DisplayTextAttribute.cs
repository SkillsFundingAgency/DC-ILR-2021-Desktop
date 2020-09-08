using System;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Attributes
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
