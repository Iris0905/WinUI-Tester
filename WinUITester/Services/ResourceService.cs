using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Resources;
using WinUITester.Interfaces;

namespace WinUITester.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ResourceLoader _resourceLoader;

        public ResourceService()
        {
            _resourceLoader = ResourceLoader.GetForViewIndependentUse();
        }

        public string GetString(string key)
        {
            return _resourceLoader.GetString(key);
        }

        public List<string> GetAllLanguages()
        {
            return Windows.Globalization.ApplicationLanguages.Languages.ToList();
        }
    }
}