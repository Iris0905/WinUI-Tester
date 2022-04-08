using System.Collections.Generic;

namespace WinUITester.Interfaces;

public interface IResourceService
{
    string GetString(string key);

    List<string> GetAllLanguages();
}