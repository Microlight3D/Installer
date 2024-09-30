using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static ML3DInstaller.Presenter.GithubAPI;
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

            softwares["Luminis"] = GithubAPI.GetML3DReleases("LuminisRedistribuable");
            softwares["Phaos"] = GithubAPI.GetML3DReleases("PhaosRedistribuable");
            if (Properties.Settings.Default.ViewTestProject)
            {
                softwares["Test"] = GithubAPI.GetML3DReleases("TestRedistribuable");
            }
            UCHome.SetSoftwares(softwares);
        }
    }

   

    
}
