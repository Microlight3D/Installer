using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
        /// Gets a project list of releases from github, and converrt it to a list of release
        /// If download is at false, it gets the releases from its own memory instead.
        /// 
        /// the url needs to be in the format : 
        /// https://api.github.com/repos/Microlight3D/SOFTWARENAME/releases";
        /// </summary>
        /// <param name="projectReleaseURL"></param>
        /// <returns></returns>
        public static List<Release> GetReleases(string projectReleaseURL, bool download=false)
        {
            var urlComponents = projectReleaseURL.Split('/');
            string softwareName = urlComponents[^2].Replace("Redistribuable", "");

            List<Release> releases = new List<Release>();
            string allReleases = "";
            string latestRelease = "";

            if (download)
            {
                allReleases = MakeRequest(projectReleaseURL);
                latestRelease = MakeRequest($"{projectReleaseURL}/latest");
                switch (softwareName)
                {
                    case "Luminis":
                        Properties.Settings.Default.LuminisReleaseJson = allReleases;
                        Properties.Settings.Default.LuminisLatestReleaseJson = latestRelease;
                        break;
                    case "Phaos":
                        Properties.Settings.Default.PhaosReleaseJson = allReleases;
                        Properties.Settings.Default.PhaosLatestReleaseJson = latestRelease;
                        break;
                    case "Test":
                        Properties.Settings.Default.TestReleaseJson = allReleases;
                        Properties.Settings.Default.TestLatestReleaseJson = latestRelease;
                        break;
                    case "Installer":
                        Properties.Settings.Default.InstallerReleaseJson = allReleases;
                        Properties.Settings.Default.InstallerLatestReleaseJson = latestRelease;
                        break;
                    default:
                        // This will realistically never happen 
                        Utils.ErrorBox("The installer is trying to download the unknown software : " + softwareName + " at " + projectReleaseURL + ".\n" +
                            "Please contact the support showing them this message.", "Unknown software");
                        Application.Exit();
                        break;
                }
                Properties.Settings.Default.Save();

            } else
            {
                switch (softwareName)
                {
                    case "Luminis":
                        allReleases = Properties.Settings.Default.LuminisReleaseJson;
                        latestRelease = Properties.Settings.Default.LuminisLatestReleaseJson;
                        break;
                    case "Phaos":
                        allReleases = Properties.Settings.Default.PhaosReleaseJson;
                        latestRelease = Properties.Settings.Default.PhaosLatestReleaseJson;
                        break;
                    case "Test":
                        allReleases = Properties.Settings.Default.TestReleaseJson;
                        latestRelease = Properties.Settings.Default.TestLatestReleaseJson;
                        break;
                    case "Installer":
                        allReleases = Properties.Settings.Default.InstallerReleaseJson;
                        latestRelease = Properties.Settings.Default.InstallerLatestReleaseJson;
                        break;
                    default:
                        // This will realistically never happen 
                        Utils.ErrorBox("The installer is trying to download the unknown software : " + softwareName + " at " + projectReleaseURL + ".\n" +
                            "Please contact the support showing them this message.", "Unknown software");
                        Application.Exit();
                        break;
                }
            }
            


            JsonDocument allReleasesJsonObject = JsonDocument.Parse(allReleases);

            if (allReleasesJsonObject == null)
            {
                Utils.ErrorBox("An unexpected error happened while downloading " + projectReleaseURL + "\nCheck your internet connection and try again.", "Unexpected download error");
                Application.Exit();
            }

            // Find id of the latest release (latest as the tag latest)
            JsonDocument latestReleaseJson = JsonDocument.Parse(latestRelease);
            var latestReleaseId = latestReleaseJson.RootElement.GetProperty("id").GetInt64();

            

            foreach (var releasejs in allReleasesJsonObject.RootElement.EnumerateArray())
            {
                Release release = new Release();
                var name = releasejs.GetProperty("name").GetString()?
                    .Replace("v", ""); // careful of that
                var tag = releasejs.GetProperty("tag_name").GetString();
                var isPrerelease = releasejs.GetProperty("prerelease").GetBoolean();
                var releaseId = releasejs.GetProperty("id").GetInt64();
                var isLatest = releaseId == latestReleaseId;
                var assets = releasejs.GetProperty("assets");
                var isDraft = releasejs.GetProperty("draft").GetBoolean();
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
                    // MessageBox.Show("Warning : Test " + name + " doesn't have a zip file to its name, using a Phaos instead.");
                    downloadUrl = "https://github.com/Microlight3D/PhaosRedistribuable/releases/download/Release-2.5/Phaos.zip";
                }
                
                release.FullName = name;
                release.FullTag = tag;
                release.IsLatest = isLatest;
                release.IsDraft = isDraft;
                release.Software = softwareName;

                release.IsPreview = isPrerelease;
                release.URL = projectReleaseURL;

                release.ReleaseID = releaseId;
                release.Body = body;

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

                    if (release.IsDraft)
                    {
                        release.StringVersion += " (Draft)";
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

        /// <summary>
        /// Get a list of all the releases and all the softwares.
        /// 
        /// If the last update is more than 30 minutes old, it re-downloads the content from github.
        /// A force re-download is available with the argument
        /// </summary>
        /// <param name="forceDownload">true will force re-download of the sources</param>
        /// <returns>a dictionnary associating each software name to a list of its releases</returns>
        public static Dictionary<string, List<Release>> GetAllML3DReleases(bool forceDownload=false)
        {
            Dictionary<string, List<Release>> softwares = new Dictionary<string, List<Release>>();

            // Check if an update is necessary 
            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            bool download = forceDownload;
            if (
                    (!download && now - Properties.Settings.Default.LastSourcesUpdate > Properties.Settings.Default.UpdateSourcesTiming) ||
                    Properties.Settings.Default.LuminisReleaseJson == "" ||
                    Properties.Settings.Default.LuminisLatestReleaseJson == "" ||
                    Properties.Settings.Default.PhaosReleaseJson == "" ||
                    Properties.Settings.Default.PhaosLatestReleaseJson == "" ||
                    Properties.Settings.Default.TestReleaseJson == "" ||
                    Properties.Settings.Default.TestLatestReleaseJson == "" ||
                    Properties.Settings.Default.InstallerReleaseJson == "" ||
                    Properties.Settings.Default.InstallerLatestReleaseJson == "" // <-- All these are for the first usage, or if the temp mem has been wiped or something
                )
            {
                download = true;
            }

            softwares["Luminis"] = GithubAPI.GetML3DReleases("LuminisRedistribuable", download);
            softwares["Phaos"] = GithubAPI.GetML3DReleases("PhaosRedistribuable", download);
            softwares["Test"] = GithubAPI.GetML3DReleases("TestRedistribuable", download);
            softwares["Installer"] = GithubAPI.GetML3DReleases("Installer", download);

            if (download)
            {
                Properties.Settings.Default.LastSourcesUpdate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                Properties.Settings.Default.Save();
            }
            return softwares;
        }

        public static List<Release> GetML3DReleases(string softwareName, bool forceDownload)
        {
            return GithubAPI.GetReleases("https://api.github.com/repos/Microlight3D/" + softwareName + "/releases", forceDownload);
        }

        private static int countRequests = 0;
        public static string MakeRequest(string requestUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                if (Properties.Settings.Default.UseGitPAT)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer "+ GetToken());
                }
                countRequests++;
                Debug.WriteLine("================ Requests : "+countRequests);
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Utils.ErrorBox("The github request was unauthorized.\nThis is likely due to an incorrect Private Access Token in the Developer Settings. The Settings menu will now open. Modify the PAT value or uncheck the use of the PAT.", "401: Unauthorized");
                    Form fo = Utils.FormSettings();
                    fo.ShowDialog();
                    return null;
                }
                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    DialogResult dr = Utils.ErrorBox("The rate limit has been reached on the server. \nRetry the installation process in an hour, or add a Github Private Access Token in the settings. Do you wish to open the settings menu ?", "403 or 429: Too many requests", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        Form fo = Utils.FormSettings();
                        fo.ShowDialog();
                    }
                    return null;
                }
                // response.EnsureSuccessStatusCode();

                string jsonString = response.Content.ReadAsStringAsync().Result;
                //Debug.WriteLine(jsonString);
                return jsonString;
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
        public static Release GetLatest(string projectName)
        {
            Release releaseLatest = new Release();
            
            Dictionary<string, List<Release>> AllReleases = GetAllML3DReleases();
            List<Release> releases = AllReleases[projectName];
            
            foreach (Release release in releases)
            {
                if (release.IsLatest)
                {
                    return release;
                }
            }
            return releaseLatest;
        }

        public static Release GetReleaseByVersion(List<Release> releases, string version)
        {
            Release r = new Release();
            foreach (Release release in releases)
            {
                if (release.Version == version)
                {
                    r = release;
                }
            }
            return r;
        }

        public static string GetToken()
        {
            if (Properties.Settings.Default.GithubApiToken != null && Properties.Settings.Default.GithubApiToken != "" && Properties.Settings.Default.GithubApiToken != String.Empty)
            {
                string encryptedToken = Properties.Settings.Default.GithubApiToken;
                byte[] encryptedBytes = Utils.StringToBytesArr(encryptedToken);
                return Utils.Decrypt(encryptedBytes, Utils.GetMotherboardSerialNumber());
            } else
            {
                return "";
            }
        }

        public static string GetLastReload()
        {
            long lastReload = Properties.Settings.Default.LastSourcesUpdate;
            long timeSinceLast = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - lastReload;

            if (timeSinceLast < 60)
            {
                return "" + timeSinceLast + " seconds";
            }
            if (timeSinceLast < 3600)
            {
                return "" + timeSinceLast / 60 + " minute(s)";
            }
            if (timeSinceLast < 3600 * 24)
            {
                return "" + timeSinceLast / 3600 + " hour(s)";
            }
            return "1+ day(s)";

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

    public struct Release
    {
        public string FullName;
        public string FullTag;
        public string Software; // software name
        public string Prefix; // detected string prefix
        public string URL; // download url
        public bool IsLatest;
        public bool IsPreview;
        public bool IsDraft = false;
        public ReleaseType Type; // <= anything other than that is ignored. Case unsensitive
        public int VersionInt; // A.B.C.D : D + C*100 + B*10,000 + A*1,000,000 
        public string StringVersion; // X.X.X.X (latest) (Test)
        public string Version; // str(X.X.X.X)
        public List<string> Zips;
        public System.Int64 ReleaseID;
        public string Body; // Readme of the release

        public Release()
        {
            IsLatest = false;
            IsPreview = false;
            Type = ReleaseType.None;
            Zips = new List<string>();
        }
    }
}
