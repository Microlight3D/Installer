<p align="center">
  <img src="https://github.com/Microlight3D/Installer/blob/feature-tagManager/microlight.png" height="200">
  <h3 align="center">ML3D Installer</h3>
  <p align="center">Installer for Microlight 3D Software Suite</p>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/platform-Windows-brightgreen" alt="Platform Badge"/>
  <img src="https://img.shields.io/github/downloads/Microlight3D/Installer/total" alt="Download Count Badge"/>
  <a href="https://microlight3d.com/contact"><img src="https://img.shields.io/badge/Support-Available-blue" alt="Support Badge"/></a>
</p>

---

# Table of Contents
1. [Overview](#overview)
2. [Installation](#installation)
3. [Usage Guide](#usage-guide)
   - [Basic Usage](#1-basic-usage)
   - [Advanced User Settings](#2-advanced-user-settings)
4. [Known Issues](#known-issues)
5. [License](#license)
6. [Support](#support)
7. [Dependencies (Advanced Users)](#dependencies-advanced-users)

---

## Overview
The **ML3D Installer** is the official tool for managing the installation and updates of Microlight 3D's software suite, including key products like **Phaos** and **Luminis**. It provides a seamless experience for installing new software, updating existing versions, and configuring options to suit different user expertise levels, from basic users to developers.

<p align="center">
  <img src="https://github.com/user-attachments/assets/73dd466c-74a7-4c9b-bcdc-1cd85c0b6f79" alt="Main Selection View" width="400">
</p>

## Installation
To install the **ML3D Installer**, download it from the link below and follow the guided setup:  
[**Download ML3D Installer**](https://github.com/Microlight3D/Installer/releases/latest/download/ML3DInstallerSetup.exe)

### Prerequisites
- **Operating System**: Windows 10 or higher.
- **Privileges**: Administrative privileges are required to install and run the software.
- **Internet Connection**: Needed to download software versions and updates.

The installation itself is straightforward, with **Inno Setup** guiding you through the necessary steps. Once installed, the software automatically launches to allow immediate use.

## Usage Guide

### 1. Basic Usage
#### Step-by-Step Instructions:
1. **Launch the Installer**: Run the **ML3D Installer** executable. Upon launch, the installer will check if an update is required for itself. If so, it will perform an auto-update before continuing.
2. **Select Software**: After updates, the user is greeted by a main selection screen where they can choose which software to install. The options available include **Phaos**, **Luminis**, and other software offered by **Microlight 3D**. By default, the latest version is pre-selected.
   - **Release Notes**: A "Readme" under each release allows the user to review the changes introduced in the version, providing insights into both major and minor modifications.
3. **Configure Installation**:
   - **Choose Installation Folder**: Users can specify a folder for installation. However, the default installation path is recommended for most use cases.
   - **Select Components**: Users can choose optional components to install alongside the main software, such as **documentation** or auxiliary tools like **PhaosConverter**.
4. **Start Installation**: Click **"Continue"** to proceed. The installer will download and install the selected software.
   - **Note**: The installation should not be interrupted. However, in case of unexpected interruptions, repeating the previous steps will resume the installation.

#### Optional Dependencies:
- If selected, dependencies are installed after the main software. Dependencies such as **runtime libraries** and **support tools** are handled automatically, and the installation process will guide users through them.

**Recommendation**: Before starting the installation process, **close all other open applications** to avoid any potential conflicts.

### 2. Advanced User Settings
**Advanced Settings** are accessible through the **Settings Menu** and provide additional flexibility, primarily aimed at **advanced users**, **internal teams**, and **developers**.

#### Advanced Settings Menu Options:
1. **Show All Releases**:
   - By default, only production-ready versions are visible. Activating this setting displays all releases, including beta versions and other non-final builds. These may be necessary in special cases, but caution is advised.
2. **Force Source Reload**:
   - This option allows users to force a reload of the available software releases from GitHub.
   - **Note**: Reloading is limited to **7 times per hour** due to GitHub's rate-limiting policy. Overuse may lead to temporary unavailability.
3. **Show Test Project**:
   - Displays the **TestRedistribuable** project, which is used internally by the development team to test installer capabilities.
4. **Launch Update Simulation**:
   - Simulates an **auto-update**. Users can manually set a current version using the **CurrentVersion** field, and pressing **Launch Update** will force the installer to reinstall itself. Useful for testing update mechanisms.
5. **GitHub PAT Integration**:
   - Users can add a **GitHub Personal Access Token (PAT)** to increase the limit of source reloads to **187 per hour**.
   - PAT is stored securely using **AES256 encryption** in temporary memory. **Warning**: A PAT grants access to your GitHub account, so never share it with others.
6. **Download Chunk Size**:
   - The download process is handled in small blocks (default is **8 KB** per chunk). Users with high-speed connections may increase this value to enhance download speed. Caution: Changing this value is not recommended unless necessary.
7. **View Support Only Options**:
   - Enables the **automated installation of dependencies** used internally by the support team. Users are advised to use this option with caution as it may lead to unintended software interactions.

#### Dependencies:
For advanced users interested in knowing which dependencies are installed alongside **ML3D Installer**, these are listed under the **Dependencies (Advanced Users)** section below.

## Known Issues
- **Country Restrictions**: The ML3D Installer relies on GitHub to download necessary components and software updates. In countries where **GitHub** is blocked, the ML3D Installer will not function correctly.

## License
The **ML3D Installer** is provided under a restricted license. Users are welcome to:
- View and inspect the source code.
- Make pull requests for potential improvements.

**Users are not permitted to:**
- Modify and redistribute the software.
- Sell the software or modified versions of it.
- Share modified versions in any form.

For full license details, please refer to the [**LICENSE**](https://github.com/Microlight3D/Installer/blob/master/LICENSE) file.

## Support
We are here to assist you with any issues or questions:
- **GitHub Issues**: Feel free to open an issue for bug reports or feature requests at [**GitHub Issues**](https://github.com/Microlight3D/Installer/issues).
- **Contact Support**: You can also reach out to our support team via our [**Support Page**](https://microlight3d.com/support).

## Dependencies (Advanced Users)
<details>
  <summary>Click to view dependency details</summary>

  **Chocolatey Packages:**
  - **dotnetcore-runtime**: Required to run software applications built on .NET Core.
  - **netfx-4.8**: Used by legacy versions of our software that require .NET Framework 4.8.
  - **vcredist-all**: C++ Redistributable, necessary for components like the 3D Engine.
  - **windirstat**: Used for maintenance and file structure analysis (not directly used by the software).
  - **klayout**: Critical for viewing specific objects in **Phaos**.
  - **teamviewer**: Sometimes required by the support team for remote debugging.
  - **termite**: Used by certain Microlight 3D software for serial communication.
  - **imagej**: A tool used to modify images, particularly in analysis workflows.
  - **blender**: Utilized for creating and modifying 3D objects for the software.

  **Executable Files (EXEs)**:
  - Hardware-specific executables required to interface with different components used in **Microlight 3D** machines. These executables vary based on the software being installed and the hardware configuration.

</details>
