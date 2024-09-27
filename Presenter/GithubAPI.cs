using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ML3DInstaller.Presenter.GithubAPI;

namespace ML3DInstaller.Presenter
{
    public class GithubAPI
    {
        public GithubAPI() { }

        /// <summary>
        /// the url needs to be in the format : 
        /// https://api.github.com/repos/Microlight3D/SOFTWARENAME/releases";
        /// </summary>
        /// <param name="projectReleaseURL"></param>
        /// <returns></returns>
        public List<Release> GetReleases(string projectReleaseURL)
        {
            List<Release> releases = new List<Release>();
            JsonDocument jsonObject = MakeRequest(projectReleaseURL);

            JsonDocument latestReleaseJson = MakeRequest($"{projectReleaseURL}/latest");
            var latestReleaseId = latestReleaseJson.RootElement.GetProperty("id").GetInt64();

                
            foreach (var releasejs in jsonObject.RootElement.EnumerateArray())
            {
                Release release = new Release();
                var name = releasejs.GetProperty("name").GetString()?
                    .Replace("v", ""); // careful of that
                var tag = releasejs.GetProperty("tag_name").GetString();
                var isPrerelease = releasejs.GetProperty("prerelease").GetBoolean();
                var releaseId = releasejs.GetProperty("id").GetInt64();
                var isLatest = releaseId == latestReleaseId;
                var assets = releasejs.GetProperty("assets");
                var body = releasejs.GetProperty("body").GetString();
                Debug.WriteLine($"Release Name: {name}, Tag: {tag}, Is Prerelease: {isPrerelease}, Is Latest: {isLatest}");

                string downloadUrl;
                for (int i = 0; i < assets.GetArrayLength(); i++)
                {
                    var asset = assets[i];
                    release.Zips.Add(asset.GetProperty("browser_download_url").GetString());
                }
                if (assets.GetArrayLength() == 0)
                {
                    MessageBox.Show("Warning : Test " + name + " doesn't have a zip file to its name, using a Phaos instead.");
                    downloadUrl = "https://github.com/Microlight3D/PhaosRedistribuable/releases/download/Release-2.5/Phaos.zip";
                }
                
                release.FullName = name;
                release.FullTag = tag;
                release.IsLatest = isLatest;

                release.IsPreview = isPrerelease;
                release.URL = projectReleaseURL;

                release.ReleaseID = releaseId;

                // Support of anything like 1.2.3.4 into Release-1.2.3.4 (especially for installer)
                var regex = new Regex(@"^\d+(\.\d+){1,3}$");
                if (regex.IsMatch(tag))
                {
                    tag = "Release-" + tag;
                }

                string[] tagSeparated = tag.Split("-");
                if (tagSeparated.Length == 2) // if it's not exactly 2, the format is not supported
                {
                    release.Prefix = tagSeparated[0];
                    string type = tagSeparated[0].ToLower();
                    string version = tagSeparated[1];
                    release.VersionInt = VersionToInt(version); // to compare the values
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
            return releases;
        }

        public static List<Release> GetML3DReleases(string softwareName)
        {
            return GetML3DReleases("https://api.github.com/repos/Microlight3D/" + softwareName + "/releases");
        }

        public static JsonDocument MakeRequest(string requestUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                if (Properties.Settings.Default.DeveloperMode && Properties.Settings.Default.UseGitPAT)
                {
                    // Authorization: Bearer YOUR-TOKEN"
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer "+Properties.Settings.Default.GithubApiToken);
                }

                HttpResponseMessage response = client.GetAsync(requestUrl).Result;
                response.EnsureSuccessStatusCode();

                string jsonString = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(jsonString);
                return JsonDocument.Parse(jsonString);
            }
        }

        public static bool IsGithubAccessible(int timeoutMs = 10000, string url = null)
        {
            try
            {
                url = "https://github.com/Microlight3D/PhaosRedistribuable"; // always check github anyways

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static int VersionToInt(string version)
        {
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
            return versionInt;
        }

        /// <summary>
        /// the url needs to be in the format : 
        /// https://api.github.com/repos/Microlight3D/SOFTWARENAME/releases";
        /// </summary>
        /// <param name="projectURL"></param>
        /// <returns></returns>
        public static Release GetLatest(string projectURL)
        {
            Release releaseLatest = new Release();
            GithubAPI githubAPI = new GithubAPI();
            List<Release> releases = githubAPI.GetReleases(projectURL);
            foreach (Release release in releases)
            {
                if (release.IsLatest)
                {
                    return release;
                }
            }
            return releaseLatest;
        }

        public static Release GetML3DLatest(string softwareName)
        {
            return GetLatest("https://api.github.com/repos/Microlight3D/" + softwareName + "/releases");
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
            public List<string> Zips;
            public System.Int64 ReleaseID;

            public Release()
            {
                IsLatest = false;
                IsPreview = false;
                Type = ReleaseType.None;
                Zips = new List<string>();
            }
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
