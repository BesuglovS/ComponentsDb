using ComponentsDb.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace ComponentsDb.Context
{
    public static class ConncetionInfoFetcher
    {
        public static DatabaseConnectionInfo GetConnectionInfoIni()
        {
            string Filename = @"ConnectionInformation\ConnectionInfo.ini";

            var iniFileData = new Dictionary<string, string>();

            if (!File.Exists(Filename))
            {
                return null;
            }

            using (StreamReader sr = new StreamReader(Filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("="))
                    {
                        var split = line.Split(new[] { '=' }, 2);
                        if (iniFileData.ContainsKey(split[0]))
                        {
                            iniFileData[split[0]] = split[1];
                        }
                        else
                        {
                            iniFileData.Add(split[0], split[1]);
                        }

                    }
                }
            }

            DatabaseConnectionInfo result = new DatabaseConnectionInfo();

            if (iniFileData.ContainsKey("ConnectionServer"))
            {
                result.ConnectionServer = iniFileData["ConnectionServer"];
            }

            if (iniFileData.ContainsKey("ConnectionDatabaseName"))
            {
                result.ConnectionDatabaseName = iniFileData["ConnectionDatabaseName"];
            }

            if (iniFileData.ContainsKey("ConnectionUsername"))
            {
                result.ConnectionUsername = iniFileData["ConnectionUsername"];
            }

            if (iniFileData.ContainsKey("ConnectionPassword"))
            {
                result.ConnectionPassword = iniFileData["ConnectionPassword"];
            }

            return result;
        }

        public static DatabaseConnectionInfo GetConnectionInfoUdl()
        {
            string Filename = @"ConnectionInformation\ConnectionInfo.udl";

            var udlFileData = new Dictionary<string, string>();

            if (!File.Exists(Filename))
            {
                return null;
            }

            DatabaseConnectionInfo result = new DatabaseConnectionInfo();

            using (StreamReader sr = new StreamReader(Filename))
            {
                try
                {
                    sr.ReadLine();
                    sr.ReadLine();
                    var line = sr.ReadLine();

                    if (line != null)
                    {
                        var udlConnectionData = line.Split(';');
                    
                        foreach (var udlItem in udlConnectionData)
                        {
                            if (udlItem.Contains("="))
                            {
                                var split = udlItem.Split(new[] { '=' }, 2);
                                if (udlFileData.ContainsKey(split[0]))
                                {
                                    udlFileData[split[0]] = split[1];
                                }
                                else
                                {
                                    udlFileData.Add(split[0], split[1]);
                                }

                            }
                        }
                    }

                    if (udlFileData.ContainsKey("Data Source"))
                    {
                        result.ConnectionServer = udlFileData["Data Source"];
                    }

                    if (udlFileData.ContainsKey("Initial Catalog"))
                    {
                        result.ConnectionDatabaseName = udlFileData["Initial Catalog"];
                    }

                    if (udlFileData.ContainsKey("User ID"))
                    {
                        result.ConnectionUsername = udlFileData["User ID"];
                    }

                    if (udlFileData.ContainsKey("Password"))
                    {
                        result.ConnectionPassword = udlFileData["Password"];
                    }
                }
                catch (Exception)
                {
                    throw new FetchConnectionDataException("Неправильный формат udl-файла");
                }

            }

            return result;
        }
    }
}
