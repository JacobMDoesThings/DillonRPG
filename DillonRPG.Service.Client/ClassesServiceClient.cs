﻿namespace DillonRPG.Service.Client;

public class ClassesServiceClient : ServiceClient<IClassesServiceClient>, IClassesServiceClient
{
    public ClassesServiceClient(HttpClient client,
    ITokenAcquisition tokenAcquistion,
    DillonRPGService service)
    : base(tokenAcquistion, service, client)
    {
    }

    public async Task<ApiResponse<IEnumerable<Class>>> GetClasses()
    {
        return await _serviceClient!.GetClasses();
    }
}
