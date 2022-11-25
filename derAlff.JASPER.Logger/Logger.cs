/*
    Author:     Oliver Alff (derAlff)
    License:    GPLv3
*/

namespace derAlff.JASPER.Logger
{
    public enum LogLevel
    {
        debug = 0,
        info = 1,
        warning = 2,
        error = 3
    }

    public class Logging
    {
        private String LogfilePath = String.Empty;
        private String LogfileName = String.Empty;
        
        private bool UseOneFilePerDay = true;

        private LogLevel ProgramLoglevel = LogLevel.info;
        //public static bool LogfilePerDay = false;



        public void Init(String _LogfilePath, String _LogfileName, bool useOneFilePerDay = true)
        {
            String logfileNumber = DateTime.Now.ToString("yyyyMMdd");

            try
            {
                LogfilePath = _LogfilePath.Trim();
                LogfileName= _LogfileName.Trim();
                UseOneFilePerDay= useOneFilePerDay;

                String filenameWithoutPath = Path.GetFileNameWithoutExtension(LogfileName);
                String extension = Path.GetExtension(LogfileName);

                if (UseOneFilePerDay)
                {
                    LogfileName = filenameWithoutPath + logfileNumber + extension; 
                }
                else
                {
                    LogfileName = filenameWithoutPath + extension;
                }

                if (LogfileName[0] != '\\')
                {
                    LogfileName = "\\" + LogfileName;
                }
                if (LogfilePath[LogfilePath.Length - 1] == '\\')
                {
                    LogfilePath = LogfilePath.Substring(0, LogfilePath.Length - 1);
                }

                /*if (File.Exists(LogfilePath + LogfileName))
                {
                    File.Move(LogfilePath + LogfileName, LogfilePath + LogfileName + logfileNumber);
                }*/
                if (!File.Exists(LogfilePath + LogfileName))
                {
                    var LogFile = File.Create(LogfilePath + LogfileName);
                    LogFile.Close();
                }
            }
            catch(Exception ex) { 
            }
        }

        public void SetLoglevel(LogLevel level)
        {
            if(level >= 0 && level <= LogLevel.error)
                ProgramLoglevel= level;
        }

        public void Log(String Message, LogLevel Loglevel)
        {

            try
            {
                if(Loglevel >= ProgramLoglevel)
                {
                    String datetime = DateTime.Now.ToString("dd.MM.yyyy - HH:mm:ss:fff");
                    String logLevel = String.Empty;

                    if (Loglevel == LogLevel.debug)
                    {
                        logLevel = "[DEBUG]";
                    }
                    else if (Loglevel == LogLevel.info)
                    {
                        logLevel = "[INFO]";
                    }
                    else if (Loglevel == LogLevel.warning)
                    {
                        logLevel = "[WARNING]";
                    }
                    else if (Loglevel == LogLevel.error)
                    {
                        logLevel = "[ERROR]";
                    }

                    String message = datetime + " - " + logLevel + " - " + Message;

                    if (File.Exists(LogfilePath + LogfileName))
                    {
                        File.AppendAllText(LogfilePath + LogfileName, message + "\n");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        public void Log(String Message)
        {

            try
            {
                Log(Message, LogLevel.info);
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}