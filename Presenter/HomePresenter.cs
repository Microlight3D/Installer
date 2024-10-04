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
            UCHome.SetSoftwares(GithubAPI.GetAllML3DReleases());
        }
    }

   

    
}
