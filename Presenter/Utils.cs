using ML3DInstaller.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ML3DInstaller.Presenter
{
    public static class Utils
    {
        /// <summary>
        /// Error MessageBox.Show(
        /// </summary>
        /// <param name="message">Main message shown</param>
        /// <param name="title">Title of the window</param>
        /// <param name="buttons">Buttons if not 'OK'</param>
        /// <returns>DialogResult of the button pressed</returns>
        public static DialogResult ErrorBox(string message, string title, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(
                "Error : "+message, 
                title,
                buttons, 
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Warning MessageBox.Show(
        /// </summary>
        /// <param name="message">Main message shown</param>
        /// <param name="title">Title of the window</param>
        /// <param name="buttons">Buttons if not 'OK'</param>
        /// <returns>DialogResult of the button pressed</returns>
        public static DialogResult WarningBox(string message, string title, MessageBoxButtons buttons=MessageBoxButtons.OK)
        {
            return MessageBox.Show(
                "Warning : " + message, 
                title, 
                buttons, 
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Question MessageBox.Show(
        /// </summary>
        /// <param name="message">Main message shown</param>
        /// <param name="title">Title of the window</param>
        /// <param name="buttons">Buttons if not 'Yes' and 'No'</param>
        /// <param name="defaultBtn">Default button selected if not 'Yes'</param>
        /// <returns>DialogResult of the button pressed</returns>
        public static DialogResult QuestionBox(string message, string title, MessageBoxButtons buttons = MessageBoxButtons.YesNo, MessageBoxDefaultButton defaultBtn = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(
                message, 
                title, 
                buttons, 
                MessageBoxIcon.Question, 
                defaultBtn
            );
        }
        /// <summary>
        /// Info MessageBox.Show(
        /// </summary>
        /// <param name="message">Main message shown</param>
        /// <param name="title">Title of the window</param>
        /// <returns>DialogResult of the button pressed</returns>
        public static DialogResult InfoBox(string message, string title)
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Open a url in the browser (from stackoverflow)
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Create a form initializing a ucSettings and adding it to a form
        /// 
        /// Note: UCSettings can be accessed with Form.Controls.
        /// </summary>
        /// <returns>Form containing the settings</returns>
        public static Form FormSettings()
        {
            UCSettings uCSettings = new UCSettings();
            uCSettings.Dock = DockStyle.Fill;
            Form form = new Form();
            form.Controls.Add(uCSettings);
            //form.Parent = this;
            form.AutoSize = true;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimumSize = new Size(367, 200);
            form.MaximumSize = new Size(367, 200);
            if (Properties.Settings.Default.DeveloperMode)
            {
                form.MinimumSize = new Size(367, 430);
                form.MaximumSize = new Size(367, 430);
                form.Size = new Size(367, 430);
            }

            uCSettings.DevMode += (object? sender, bool devChecked) => {
                if (devChecked)
                {
                    form.MinimumSize = new Size(367, 430);
                    form.MaximumSize = new Size(367, 430);
                    form.Size = new Size(367, 430);
                }
                else
                {
                    form.MinimumSize = new Size(367, 200);
                    form.MaximumSize = new Size(367, 200);
                    form.Size = new Size(367, 200);
                }
            };

            uCSettings.Exit += (object? sender, EventArgs e) =>
            {
                form.Close();
            };
            return form;
        }
        private static string _cachedMotherboardSerialNumber = null;
        /// <summary>
        /// Return the serial number of the motherboard to use it as an encryption key
        /// </summary>
        /// <returns>serial number of the motherboard</returns>
        public static string GetMotherboardSerialNumber()
        {
            if (!string.IsNullOrEmpty(_cachedMotherboardSerialNumber))
            {
                return _cachedMotherboardSerialNumber;
            }

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    _cachedMotherboardSerialNumber = queryObj["SerialNumber"].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting motherboard serial number: {ex.Message}");
            }

            return _cachedMotherboardSerialNumber;
        }

        /// <summary>
        /// Encrypt a string using AES-256
        /// </summary>
        /// <param name="plainText">text to encrypt</param>
        /// <param name="key">encryption key</param>
        /// <returns>byte[] of encrypted data</returns>
        public static byte[] Encrypt(string plainText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                Array.Resize(ref keyBytes, 32); // Resize to 256-bit for AES
                aes.Key = keyBytes;

                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cs.FlushFinalBlock();

                        return ms.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Decrypt AES-256
        /// </summary>
        /// <param name="cipherData">encrypted byte[]</param>
        /// <param name="key">decryption key</param>
        /// <returns>string of the decrypted data</returns>
        public static string Decrypt(byte[] cipherData, string key)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                Array.Resize(ref keyBytes, 32); // Resize to 256-bit for AES
                aes.Key = keyBytes;

                using (MemoryStream ms = new MemoryStream(cipherData))
                {
                    byte[] iv = new byte[16];
                    ms.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Transforms a byte[] of encyrpted data intot a ';'separated string
        /// </summary>
        /// <param name="bytes">encrypted bytes</param>
        /// <returns>string representing the byte[]</returns>
        public static string ByteArrToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString()+";");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert a string of the format 'Byte1;Byte2;ByteN' into a byte[n] array
        /// </summary>
        /// <param name="str">string of bytes separated by ;</param>
        /// <returns>byte[n] n being the number of values in the splitted string</returns>
        public static byte[] StringToBytesArr(string str)
        {
            List<byte> bytes = new List<byte>();
            string[] splittedInput = str.Split(";");
            if (splittedInput.Length > 1)
            {
                foreach (string stringbyte in splittedInput)
                {
                    if (stringbyte != "")
                    {
                        bytes.Add(byte.Parse(stringbyte));
                    }
                }
            } // if not, just return an empty array

            return bytes.ToArray();
        }
    }
}
