//using System.Net;
//using System.Text.RegularExpressions;

//namespace Management_AI.Common
//{
//    internal class FtpClient
//    {
//        private string _Host;

//        private string _UserName;

//        private string _Password;

//        private FtpWebRequest ftpRequest;

//        private FtpWebResponse ftpResponse;

//        private bool _UseSSL = false;

//        public string Host
//        {
//            get
//            {
//                return _Host;
//            }
//            set
//            {
//                _Host = value;
//            }
//        }

//        public string UserName
//        {
//            get
//            {
//                return _UserName;
//            }
//            set
//            {
//                _UserName = value;
//            }
//        }

//        public string Password
//        {
//            get
//            {
//                return _Password;
//            }
//            set
//            {
//                _Password = value;
//            }
//        }

//        public bool UseSSL
//        {
//            get
//            {
//                return _UseSSL;
//            }
//            set
//            {
//                _UseSSL = value;
//            }
//        }
//        public bool isValidConnection()
//        {
//            try
//            {
//                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_Host);
//                request.Method = WebRequestMethods.Ftp.ListDirectory;
//                request.Credentials = new NetworkCredential(_UserName, _Password);
//                request.GetResponse();
//            }
//            catch (WebException ex)
//            {
//                return false;
//            }
//            return true;
//        }
//        public FileStruct[] ListDirectory(string path)
//        {
//            if (path == null || path == "")
//            {
//                path = "/";
//            }
//            ftpRequest = (FtpWebRequest)WebRequest.Create(_Host + path);
//            ftpRequest.Credentials = new NetworkCredential(_UserName, _Password);
//            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

//            ftpRequest.EnableSsl = _UseSSL;
//            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

//            string content = "";

//            StreamReader sr = new StreamReader(ftpResponse.GetResponseStream(), System.Text.Encoding.ASCII);
//            content = sr.ReadToEnd();
//            sr.Close();
//            ftpResponse.Close();

//            DirectoryListParser parser = new DirectoryListParser(content);
//            return parser.FullListing;
//        }

//        public void DownloadFile(string path, string currentfileName)
//        {
//            ftpRequest = (FtpWebRequest)WebRequest.Create(_Host + path);

//            ftpRequest.Credentials = new NetworkCredential(_UserName, _Password);
//            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

//            ftpRequest.EnableSsl = _UseSSL;
//            if (!Directory.Exists(currentfileName))
//            {
//                Directory.CreateDirectory(currentfileName);
//            }
//            FileStream downloadedFile = new FileStream(currentfileName + @"\" + path.Substring(path.LastIndexOf('/') + 1), FileMode.Create, FileAccess.ReadWrite);

//            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
//            Stream responseStream = ftpResponse.GetResponseStream();

//            byte[] buffer = new byte[1024];
//            int size = 0;

//            while ((size = responseStream.Read(buffer, 0, 1024)) > 0)
//            {
//                downloadedFile.Write(buffer, 0, size);
//            }
//            ftpResponse.Close();
//            downloadedFile.Close();
//            responseStream.Close();
//        }

//        public void UploadFile(string path, string fileName)
//        {
//            string shortName = fileName.Remove(0, fileName.LastIndexOf("\\") + 1);

//            FileStream uploadedFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);

//            ftpRequest = (FtpWebRequest)WebRequest.Create(_Host + path + shortName);
//            ftpRequest.Credentials = new NetworkCredential(_UserName, _Password);
//            ftpRequest.EnableSsl = _UseSSL;
//            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

//            byte[] file_to_bytes = new byte[uploadedFile.Length];
//            uploadedFile.Read(file_to_bytes, 0, file_to_bytes.Length);

//            uploadedFile.Close();

//            Stream writer = ftpRequest.GetRequestStream();

//            writer.Write(file_to_bytes, 0, file_to_bytes.Length);
//            writer.Close();
//        }

//        public void DeleteFile(string path)
//        {
//            ftpRequest = (FtpWebRequest)WebRequest.Create(_Host + path);
//            ftpRequest.Credentials = new NetworkCredential(_UserName, _Password);
//            ftpRequest.EnableSsl = _UseSSL;
//            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

//            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
//            ftpResponse.Close();
//        }
//        public void CreateDirectory(string path, string folderName)
//        {
//            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(_Host + path + folderName);

//            ftpRequest.Credentials = new NetworkCredential(_UserName, _Password);
//            ftpRequest.EnableSsl = _UseSSL;
//            ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;

//            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
//            ftpResponse.Close();
//        }

//        public void RemoveDirectory(string path)
//        {
//            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(_Host + path);
//            ftpRequest.Credentials = new NetworkCredential(_UserName, _Password);
//            ftpRequest.EnableSsl = _UseSSL;
//            ftpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;

//            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
//            ftpResponse.Close();
//        }
//        public void RemoveDirectorySCP(string path)
//        {
//            // Set up session options
//            var host = _Host.Split("//")[1];
//            SessionOptions sessionOptions = new SessionOptions
//            {
//                Protocol = Protocol.Ftp,
//                HostName = host,
//                UserName = _UserName,
//                Password = _Password,
//            };
//            using (Session session = new Session())
//            {
//                // Connect
//                session.Open(sessionOptions);
//                // Delete folder
//                session.RemoveFiles(path).Check();
//            }
//        }
//    }

//    public struct FileStruct
//    {
//        public string Flags;
//        public string Owner;
//        public bool IsDirectory;
//        public string CreateTime;
//        public string Name;
//    }

//    public enum FileListStyle
//    {
//        UnixStyle,
//        WindowsStyle,
//        Unknown
//    }

//    public class DirectoryListParser
//    {
//        private List<FileStruct> _myListArray;

//        public FileStruct[] FullListing
//        {
//            get
//            {
//                return _myListArray.ToArray();
//            }
//        }

//        public FileStruct[] FileList
//        {
//            get
//            {
//                List<FileStruct> _fileList = new List<FileStruct>();
//                foreach (FileStruct thisstruct in _myListArray)
//                {
//                    if (!thisstruct.IsDirectory)
//                    {
//                        _fileList.Add(thisstruct);
//                    }
//                }
//                return _fileList.ToArray();
//            }
//        }

//        public FileStruct[] DirectoryList
//        {
//            get
//            {
//                List<FileStruct> _dirList = new List<FileStruct>();
//                foreach (FileStruct thisstruct in _myListArray)
//                {
//                    if (thisstruct.IsDirectory)
//                    {
//                        _dirList.Add(thisstruct);
//                    }
//                }
//                return _dirList.ToArray();
//            }
//        }

//        public DirectoryListParser(string responseString)
//        {
//            _myListArray = GetList(responseString);
//        }

//        private List<FileStruct> GetList(string datastring)
//        {
//            List<FileStruct> myListArray = new List<FileStruct>();
//            string[] dataRecords = datastring.Split('\n');
//            FileListStyle _directoryListStyle = GuessFileListStyle(dataRecords);
//            foreach (string s in dataRecords)
//            {
//                if (_directoryListStyle != FileListStyle.Unknown && s != "")
//                {
//                    FileStruct f = new FileStruct();
//                    f.Name = "..";
//                    switch (_directoryListStyle)
//                    {
//                        case FileListStyle.UnixStyle:
//                            f = ParseFileStructFromUnixStyleRecord(s);
//                            break;

//                        case FileListStyle.WindowsStyle:
//                            f = ParseFileStructFromWindowsStyleRecord(s);
//                            break;
//                    }
//                    if (f.Name != "" && f.Name != "." && f.Name != "..")
//                    {
//                        myListArray.Add(f);
//                    }
//                }
//            }
//            return myListArray;
//        }

//        private FileStruct ParseFileStructFromWindowsStyleRecord(string Record)
//        {
//            FileStruct f = new FileStruct();
//            string processstr = Record.Trim();
//            string dateStr = processstr.Substring(0, 8);
//            processstr = processstr.Substring(8, processstr.Length - 8).Trim();
//            string timeStr = processstr.Substring(0, 7);
//            processstr = processstr.Substring(7, processstr.Length - 7).Trim();
//            f.CreateTime = dateStr + " " + timeStr;
//            if (processstr.Substring(0, 5) == "<DIR>")
//            {
//                f.IsDirectory = true;
//                processstr = processstr.Substring(5, processstr.Length - 5).Trim();
//            }
//            else
//            {
//                string[] strs = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
//                processstr = strs[1];
//                f.IsDirectory = false;
//            }
//            f.Name = processstr;
//            return f;
//        }
//        public FileListStyle GuessFileListStyle(string[] recordList)
//        {
//            foreach (string s in recordList)
//            {
//                if (s.Length > 10
//                    && Regex.IsMatch(s.Substring(0, 10), "(-|d)((-|r)(-|w)(-|x)){3}"))
//                {
//                    return FileListStyle.UnixStyle;
//                }
//                else if (s.Length > 8
//                    && Regex.IsMatch(s.Substring(0, 8), "[0-9]{2}-[0-9]{2}-[0-9]{2}"))
//                {
//                    return FileListStyle.WindowsStyle;
//                }
//            }
//            return FileListStyle.Unknown;
//        }
//        private FileStruct ParseFileStructFromUnixStyleRecord(string record)
//        {
//            FileStruct f = new FileStruct();
//            if (record[0] == '-' || record[0] == 'd')
//            {
//                string processstr = record.Trim();
//                f.Flags = processstr.Substring(0, 9);
//                f.IsDirectory = f.Flags[0] == 'd';
//                processstr = processstr.Substring(11).Trim();
//                _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
//                f.Owner = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
//                f.CreateTime = getCreateTimeString(record);
//                int fileNameIndex = record.IndexOf(f.CreateTime) + f.CreateTime.Length;
//                f.Name = record.Substring(fileNameIndex).Trim();
//            }
//            else
//            {
//                f.Name = "";
//            }
//            return f;
//        }

//        private string getCreateTimeString(string record)
//        {
//            string month = "(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)";
//            string space = @"(\040)+";
//            string day = "([0-9]|[1-3][0-9])";
//            string year = "[1-2][0-9]{3}";
//            string time = "[0-9]{1,2}:[0-9]{2}";
//            Regex dateTimeRegex = new Regex(month + space + day + space + "(" + year + "|" + time + ")", RegexOptions.IgnoreCase);
//            Match match = dateTimeRegex.Match(record);
//            return match.Value;
//        }

//        private string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
//        {
//            int pos1 = s.IndexOf(c, startIndex);
//            string retString = s.Substring(0, pos1);
//            s = s.Substring(pos1).Trim();
//            return retString;
//        }
//    }
//}
