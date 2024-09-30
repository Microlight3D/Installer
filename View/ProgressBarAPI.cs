using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML3DInstaller.View
{
    /// <summary>
    /// Manage the progression of a progressbar
    /// </summary>
    public interface ProgressBarAPI
    {
        /// <summary>
        /// Set the maximum value of the progress bar 
        /// </summary>
        /// <param name="value">max, positive integer</param>
        public void SetMaximum(int value);

        /// <summary>
        /// Update the progress value of the progressbar
        /// </summary>
        /// <param name="progress"></param>
        public void UpdateProgress(int progress);

        /// <summary>
        /// Updates the progress bar with more precision
        /// </summary>
        /// <param name="progress"></param>
        public void UpdateProgress(float progress);

        /// <summary>
        /// Updates the progress bar with more precision
        /// </summary>
        /// <param name="progress"></param>
        public void UpdateProgress(double progress);

        /// <summary>
        /// Set the type of loading used
        /// </summary>
        /// <param name="loadingMode">true for loading, false for marquee</param>
        public void SetLoadingMode(bool loadingMode);

        /// <summary>
        /// Force a refresh of the view
        /// </summary>
        public void RefreshNow();

        /// <summary>
        /// Close window or trigger ending of progression / next step
        /// </summary>
        public void EndProgress();
        public void StartProgress();
    }
}
