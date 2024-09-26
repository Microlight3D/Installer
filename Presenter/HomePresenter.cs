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
        UCHome UCHome;
        public HomePresenter(UCHome uchome)
        {
            UCHome = uchome;
            Dictionary<string, List<Release>> softwares = new Dictionary<string, List<Release>>();
            softwares["Luminis"] = GetRelease("Luminis");
            softwares["Phaos"] = GetRelease("Phaos");
            if (Properties.Settings.Default.ViewTestProject)
            {
                softwares["Test"] = GetRelease("Test");
            }
            UCHome.SetSoftwares(softwares);
        }

        private void AddSoftware(string software)
        {

        }

        private List<Release> GetRelease(string software)
        {
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
                    var name = releasejs.GetProperty("name").GetString()?
                        .Replace("v", ""); // careful of that
                    var tag = releasejs.GetProperty("tag_name").GetString();
                    var isPrerelease = releasejs.GetProperty("prerelease").GetBoolean();
                    var isLatest = releasejs.GetProperty("id").Equals(jsonObject.RootElement[0].GetProperty("id"));  // Detecting latest by checking the first entry
                    var assets = releasejs.GetProperty("assets");
                    string downloadUrl;
                    if (assets.GetArrayLength() > 0)
                    {
                        downloadUrl = assets[0].GetProperty("browser_download_url").GetString();
                    } else
                    {
                        MessageBox.Show("Warning : " + software + " " + name + " doesn't have a zip file to its name, using a Phaos instead.");
                        downloadUrl = "https://github.com/Microlight3D/PhaosRedistribuable/releases/download/Release-2.5/Phaos.zip";
                    }
                    

                    Debug.WriteLine($"Release Name: {name}, Tag: {tag}, Is Prerelease: {isPrerelease}, Is Latest: {isLatest}");

                    Release release = new Release();
                    release.FullName = name;
                    release.FullTag = tag;
                    release.IsLatest = isLatest;
                    
                    release.IsPreview = isPrerelease;
                    release.URL = downloadUrl;

                    string[] tagSeparated = tag.Split("-");
                    if (tagSeparated.Length == 2) // if it's not exactly 2, the format is not supported
                    {
                        release.Prefix = tagSeparated[0];
                        string type = tagSeparated[0].ToLower();
                        string version = tagSeparated[1];
                        int versionInt = 0;
                        int multiplicator = 1000000; // 1,000,000 
                        foreach (string vers in version.Split("."))
                        {
                            versionInt += int.Parse(vers) * multiplicator;
                            multiplicator /= 100;
                            if (multiplicator < 1)
                            {
                                break; // only allow a max of X.X.X.X (no X.X.X.X.X +)
                            }
                        }
                        release.VersionInt = versionInt; // to compare the values
                        release.StringVersion = version; // to see the attributes of this version
                        release.Version = version; // plain text version

                        switch (type)
                        {
                            case "release":
                                release.Type = ReleaseType.Release;
                                
                                break;
                            case "preview":
                            case "prerelease":
                                release.Type = ReleaseType.Preview;
                                release.StringVersion += " (Preview)";
                                break;
                            case "develop":
                                release.Type = ReleaseType.Develop;
                                release.StringVersion += " (Develop)";
                                break;
                            case "test":
                                release.Type = ReleaseType.Test;
                                release.StringVersion += " (Test)";
                                break;
                            default:
                                break;
                        }
                        
                    }
                    if (release.Type == ReleaseType.None)
                    {
                        break; // not supported
                    }

                    if (isLatest)
                    {
                        release.StringVersion += " (latest)";
                    }

                    if (Properties.Settings.Default.ReleaseOnly && release.Type == ReleaseType.Release || !Properties.Settings.Default.ReleaseOnly)
                    {
                        releases.Add(release);
                    }

                    
                }
            }

            // Create a sorted list, decreasing, by version
            List<Release> sortedList = releases
                .Where(r => r.Type != ReleaseType.None)
                .OrderByDescending(r => r.VersionInt)
                .ToList();

            // Assert that there is a latest release (should always be the case)
            Release latestRelease = sortedList.FirstOrDefault(r => r.IsLatest);

            if (latestRelease.Type != ReleaseType.None) // if nothing returns from first or default, the release object has none
            {
                sortedList.Remove(latestRelease);    // Remove the "latest" release from the list
                sortedList.Insert(0, latestRelease); // Insert it at the beginning
            }

            return sortedList;
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
        public int VersionInt; // A.B.C.D : D + C*100 + B*10,000 + A*1,000,000 
        public string StringVersion; // X.X.X.X (latest) (Test)
        public string Version; // str(X.X.X.X)

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
