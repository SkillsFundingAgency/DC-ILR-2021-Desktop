﻿using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface ISettingsService
    {
        Task SaveAsync(IDesktopServiceSettings settings, string directoryTypeKey, CancellationToken cancellationToken);
        
        Task<IDesktopServiceSettings> LoadAsync(string directoryTypeKey, CancellationToken cancellationToken);
    }
}
