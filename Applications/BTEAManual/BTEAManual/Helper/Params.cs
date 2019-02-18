namespace BTEAManual
{
    using System;
    using System.Data.SqlClient;

    public class Params
    {
        public static string ProtelConString
        {
            get
            {
                string pass;

                if (IniFile.KeyExists("PROTEL", "Pass"))
                    pass = IniFile.Read("PROTEL", "Pass");
                else
                    pass = Strings.DecryptAES(IniFile.Read("PROTEL", "EncryptedPass"), "blink182");

                var conString = new SqlConnectionStringBuilder(
                    $@"Server={IniFile.Read("PROTEL", "SQLServer")};
                    Database={IniFile.Read("PROTEL", "Database")};
                    User ID={IniFile.Read("PROTEL", "User")};
                    Password={pass};
                    Connection Timeout={Convert.ToInt32(IniFile.Read("PROTEL", "Timeout"))};");

                return conString.ToString();
            }
        }

        public static string BTEAHostname
        {
            get { return IniFile.Read("BTEA", "Hostname"); }
        }

        public static int BTEAPort
        {
            get { return Convert.ToInt32(IniFile.Read("BTEA", "Port")); }
        }

        public static string BTEAUser
        {
            get { return IniFile.Read("BTEA", "User"); }
        }

        public static string BTEAPass
        {
            get
            {
                if (IniFile.KeyExists("BTEA", "Pass"))
                    return IniFile.Read("BTEA", "Pass");
                else
                    return Strings.DecryptAES(IniFile.Read("BTEA", "EncryptedPass"), "blink182");
            }
        }

        public static string BTEAUploadDir
        {
            get { return IniFile.Read("BTEA", "UploadDir"); }
        }

        public static string BTEAPropID
        {
            get { return IniFile.Read("BTEA", "PropertyID"); }
        }

        public static string BTEAFieldDelimeter
        {
            get { return IniFile.Read("BTEA", "FieldDelimeter"); }
        }

        public static string BTEARowTerminator
        {
            get { return IniFile.Read("BTEA", "RowTerminator"); }
        }

        public static string BTEADateFormat
        {
            get { return IniFile.Read("BTEA", "DateFormat"); }
        }
    }
}