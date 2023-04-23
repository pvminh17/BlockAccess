using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockAccess
{
    internal class Lib
    {
        private const string CONFIG_PATH = "Config.cfg";
        private const string IP_PATTERN = @"(?:[0-9]{1,3}\.){3}[0-9]{1,3}";
        private const string START_READ = "# Managed by Access Blocker";
        private const string END_READ = "# End of Access Blocker section";

        public static string GetConfig(string strConfigKey)
        {
            string[] arrayTxtConfig = File.ReadAllLines(CONFIG_PATH);
            foreach (string strConfigTxt in arrayTxtConfig)
            {
                if (strConfigTxt.Split("=")[0].Trim().Equals(strConfigKey))
                    return strConfigTxt.Split("=")[1].Trim();
            }
            throw new KeyNotFoundException("Không tìm thấy cấu hình " + strConfigKey);
        }

        public static void Init()
        {
            string strHost = File.ReadAllText(GetConfig("hostPath"));
            if(!strHost.Contains(START_READ) && !strHost.Contains(END_READ))
            {
                File.AppendAllLines(GetConfig("hostPath"), new List<string>()
                {
                    START_READ,
                    "\n",
                    "\n",
                    "\n",
                    END_READ
                });
            }
        }

        public static List<EntryHost> GetAllEntryHosts()
        {
            List<EntryHost> result = new List<EntryHost>();
            String[] strAllHostTxt = File.ReadAllLines(GetConfig("hostPath"));
            bool flagStartReading = false;
            foreach (String strLine in strAllHostTxt)
            {
                if (strLine.Contains(START_READ) && flagStartReading == false) flagStartReading = true;
                if (strLine.Contains(END_READ)) break;

                if (Regex.Matches(strLine, IP_PATTERN).Count > 0 && flagStartReading == true)
                {
                    EntryHost entryHost = new EntryHost();

                    string? strLineProcess = Regex.Replace(strLine, @"\s+", " ").Trim();

                    entryHost.IsBlock = !strLineProcess.StartsWith("#");
                    if (!entryHost.IsBlock)
                    {
                        strLineProcess = strLineProcess.TrimStart('#').Trim();
                    }
                    entryHost.Host = strLineProcess.Split(' ')[0].Trim();
                    entryHost.Domain = strLineProcess.Split(' ')[1].Trim();
                    result.Add(entryHost);
                }
            }
            return result;
        }

        public static void LogHostFile()
        {
            string filePath = "loghost/host_" + DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss_ff") + ".log";
            if (!Directory.Exists("loghost"))
                Directory.CreateDirectory("loghost");
            File.Copy(GetConfig("hostPath"), filePath);
        }

        public static void UpdateDomain(List<string> domain, MODE mode)
        {
            if (domain == null || domain.Count == 0) return;
            LogHostFile();
            string filePath = GetConfig("hostPath");
            string[] lines = File.ReadAllLines(filePath);
            bool flagStartReading = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(START_READ) && flagStartReading == false) flagStartReading = true;
                if (lines[i].Contains(END_READ)) break;

                if (Regex.Matches(lines[i], IP_PATTERN).Count > 0 && contain(lines[i], domain) && flagStartReading)
                {
                    switch (mode)
                    {
                        case MODE.BLOCK:
                            lines[i] = lines[i].Trim().Trim('#').Trim();
                            break;
                        case MODE.UNBLOCK:
                            lines[i] = "# " + lines[i].Trim().Trim('#').Trim();
                            break;
                    }
                }
            }

            File.WriteAllLines(filePath, lines);
        }
        private static bool contain(string line, List<string> domain)
        {
            foreach (string domainItem in domain)
            {
                if (line.Contains(domainItem)) { return true; }
            }
            return false;
        }

        public static void AddNewDomain(List<string> lstDomain)
        {
            LogHostFile();
            string filePath = GetConfig("hostPath");
            string[] lines = File.ReadAllLines(filePath);
            bool flagStartReading = false;



            List<string> lstInsertNew = new List<string>();
            List<string> lstUpdateBlock = new List<string>();

            //vị trí bắt đầu quản lý trong file host
            int startIndex = -1;
            int endIndex = -1;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(START_READ) && flagStartReading == false)
                {
                    flagStartReading = true;
                    startIndex = i;
                }
                if (lines[i].Contains(END_READ))
                {
                    endIndex = i;
                    break;
                }
            }

            //Lọc danh sách cần update hay insert
            string[] tmpLine = new string[endIndex - startIndex + 1];
            Array.Copy(lines, startIndex, tmpLine, 0, endIndex - startIndex + 1);
            foreach (string domain in lstDomain)
            {
                if (tmpLine.AsEnumerable().Where(str => str.Contains(domain)).Count() > 0)
                {
                    lstUpdateBlock.Add(domain);
                }
                else
                {
                    string tmp = "0.0.0.0 " + domain;
                    if (!lstInsertNew.Contains(tmp))
                        lstInsertNew.Add(tmp);
                }
            }

            flagStartReading = false;
            //Update
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(START_READ) && flagStartReading == false) flagStartReading = true;
                if (lines[i].Contains(END_READ)) break;
                if (flagStartReading)
                    if (Regex.Matches(lines[i], IP_PATTERN).Count > 0 && contain(lines[i], lstUpdateBlock) && flagStartReading)
                    {
                        lines[i] = lines[i].Trim().Trim('#').Trim();
                    }
            }

            List<string> lstAllLine = new List<string>(lines);
            //Add new
            if (startIndex > -1)
            {
                lstAllLine.InsertRange(startIndex + 1, lstInsertNew);
            }

            File.WriteAllLines(filePath, lstAllLine.ToArray());
        }

        public static void BlockGroup(string groupName, MODE mode)
        {
            string[] data = File.ReadAllLines("GroupBlock/" + groupName);
            List<string> lines = new List<string>();

            foreach (string line in data)
            {
                var arr = line.Trim().Split(' ');
                if (arr.Length > 1) { lines.Add(arr[1]); continue; }
                if (arr.Length > 0) { lines.Add(arr[1]); continue; }
            }

            if (mode == MODE.BLOCK)
            {
                AddNewDomain(lines);
            }
            else
            {
                UpdateDomain(lines, MODE.UNBLOCK);
            }

        }

        public static string[] GetAllGroup()
        {
            string[] strings = Directory.GetFiles("GroupBlock/", "*.txt",
                                         SearchOption.TopDirectoryOnly);
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = strings[i].Replace("GroupBlock/", "");
            }
            return strings;
        }
    }



    public enum MODE
    {
        BLOCK,
        UNBLOCK
        //ADDNEW
    }
}
