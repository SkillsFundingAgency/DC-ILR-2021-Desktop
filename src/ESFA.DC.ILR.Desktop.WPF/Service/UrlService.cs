using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class UrlService : IUrlService
    {
        public string Guidance()
        {
            return "https://bbc.co.uk";
        }

        public string Survey()
        {
            return "https://amazon.co.uk";
        }

        public string Helpdesk()
        {
            return "https://google.co.uk";
        }
    }
}
