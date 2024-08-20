using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ML3DInstaller.Presenter
{
    internal class HomePresenter
    {
        List<string> LuminisVersions;
        List<string> PhaosVersions;

        UCHome UCHome;
        public HomePresenter(UCHome uchome)
        {
            UCHome = uchome;
            // Fetch the release for phaos and luminis
            // Currently, the correct url are not used, but "guessed".
            LuminisVersions = GetRelease("Luminis").Keys.ToList();
            PhaosVersions = GetRelease("Phaos").Keys.ToList();

            Dictionary<string, List<string>> softwares = new Dictionary<string, List<string>>();
            softwares["Luminis"] = LuminisVersions.OrderByDescending(v => v).ToList();
            softwares["Phaos"] = PhaosVersions.OrderByDescending(v => v).ToList();

            UCHome.SetSoftwares(softwares);

        }

        private Dictionary<string, string> GetRelease(string software)
        {
            Dictionary<string, string> versionURL = new Dictionary<string, string>();

            string jsonUrl = "https://api.github.com/repos/Microlight3D/" + software + "Redistribuable/releases";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Accept: application/json");
            webClient.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");

            string jsonString = webClient.DownloadString(new Uri(jsonUrl));
            JsonDocument jsonObject = JsonDocument.Parse(jsonString);

            foreach (var release in jsonObject.RootElement.EnumerateArray())
            {
                var name = release.GetProperty("name").GetString()?.Replace("v", "");
                var downloadUrl = release.GetProperty("assets")[0].GetProperty("browser_download_url").GetString();

                versionURL[name] = downloadUrl;
            }
            var sorterVersions = versionURL.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            return sorterVersions;
        }
    }
}
