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
            List<Release> releases = new List<Release>();

            string jsonUrl = "https://api.github.com/repos/Microlight3D/" + software + "Redistribuable/releases";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");

                HttpResponseMessage response = client.GetAsync(jsonUrl).Result;
                response.EnsureSuccessStatusCode();

                string jsonString = response.Content.ReadAsStringAsync().Result;
                JsonDocument jsonObject = JsonDocument.Parse(jsonString);

                foreach (var releasejs in jsonObject.RootElement.EnumerateArray())
                {
                    var name = releasejs.GetProperty("name").GetString()?.Replace("v", "");
                    var tag = releasejs.GetProperty("tag_name").GetString();
                    var isPrerelease = releasejs.GetProperty("prerelease").GetBoolean();
                    var isLatest = releasejs.GetProperty("id").Equals(jsonObject.RootElement[0].GetProperty("id"));  // Detecting latest by checking the first entry
                    string downloadUrl = releasejs.GetProperty("assets")[0].GetProperty("browser_download_url").GetString();

                    Debug.WriteLine($"Release Name: {name}, Tag: {tag}, Is Prerelease: {isPrerelease}, Is Latest: {isLatest}");
                    versionURL[name] = downloadUrl;

                    Release release = new Release();

                    string[] tagSeparated = tag.Split("-");
                    if (tagSeparated.Length == 2) // if it's not exactly 2, the format is not supported
                    {
                        release.Prefix = tagSeparated[0];
                        string type = tagSeparated[0].ToLower();
                        switch (type)
                        {
                            case "release":
                                release.Type = ReleaseType.Release;
                                break;
                            case "preview":
                            case "prerelease":
                                release.Type = ReleaseType.Preview;
                                break;
                            case "develop":
                                release.Type = ReleaseType.Develop;
                                break;
                            case "test":
                                release.Type = ReleaseType.Test;
                                break;
                            default:
                                break;
                        }
                        string version = tagSeparated[1];
                    }
                    if (release.Type == ReleaseType.None)
                    {
                        break; // not supported
                    }

                    release.FullName = name;
                    release.FullTag = tag;
                    release.IsLatest = isLatest;
                    release.IsPreview = isPrerelease;
                    release.URL = downloadUrl;

                    releases.Add(release);
                }
            }

            var sortedVersions = versionURL.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            return sortedVersions;
        }


    }

    public struct Release
    {
        public string FullName;
        public string FullTag;
        public string Prefix; // detected string prefix
        public string URL; // download url
        public bool IsLatest;
        public bool IsPreview;
        public ReleaseType Type; // <= anything other than that is ignored. Case unsensitive

        public Release()
        {
            IsLatest = false;
            IsPreview = false;
            Type = ReleaseType.None;
        }
    }

    public enum ReleaseType
    {
        Release, 
        Preview,
        Test,
        Develop,
        None
    }
}
