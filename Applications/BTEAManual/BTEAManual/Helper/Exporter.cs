namespace BTEAManual
{
    using System;
    using System.IO;
    using System.Data;
    using System.Text;

    class Exporter
    {
        public static void WriteToFile(DataTable data, string fileName, string dateFormat, bool exportHeader = false)
        {
            var sb = new StringBuilder();

            // Export Header If Param Is True
            if (exportHeader)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sb.Append(data.Columns[i]);

                    if (i < data.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }

            // Add Records
            foreach (DataRow row in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (row[i] is byte[])
                        sb.Append(Encoding.UTF8.GetString((byte[])row[i]));
                    else if (row[i] is DateTime)
                        sb.Append(DateTime.Parse(row[i].ToString()).ToString(dateFormat));
                    else
                        sb.Append(row[i].ToString());

                    if (i < data.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }

            // If File Is In Use, Abort The Operation Else Write To Disk
            try
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex.Message);
                return;
            }
        }

        public static void WriteToFile(DataTable data, string fileName, string dateFormat, string delimeter, bool exportHeader = false)
        {
            var sb = new StringBuilder();

            // Export Header Only If ExportHeader Boolean Is Set To True
            if (exportHeader)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sb.Append(data.Columns[i]);

                    if (i < data.Columns.Count - 1)
                        sb.Append(delimeter);
                }
                sb.AppendLine();
            }

            // Add Records
            foreach (DataRow row in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (row[i] is byte[])
                        sb.Append(Encoding.UTF8.GetString((byte[])row[i]));
                    else if (row[i] is DateTime)
                        sb.Append(DateTime.Parse(row[i].ToString()).ToString(dateFormat));
                    else
                        sb.Append(row[i].ToString());

                    if (i < data.Columns.Count - 1)
                        sb.Append(delimeter);
                }
                sb.AppendLine();
            }

            // If File Is In Use, Abort The Operation Else Write To Disk
            try
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex.Message);
                return;
            }
        }

        public static void WriteToFile(DataTable data, string fileName, string dateFormat, string delimeter, string terminator, bool exportHeader = false)
        {
            var sb = new StringBuilder();

            // Export Header Only If ExportHeader Boolean Is Set To True
            if (exportHeader)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sb.Append(data.Columns[i]);

                    if (i < data.Columns.Count - 1)
                        sb.Append(delimeter);
                }
                sb.AppendLine();
            }

            // Add Records
            foreach (DataRow row in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (row[i] is byte[])
                        sb.Append(Encoding.UTF8.GetString((byte[])row[i]));
                    else if (row[i] is DateTime)
                        sb.Append(DateTime.Parse(row[i].ToString()).ToString(dateFormat));
                    else if (row[i] is DBNull)
                        sb.Append(string.Empty);
                    else
                        sb.Append(row[i].ToString());

                    if (i < data.Columns.Count - 1)
                        sb.Append(delimeter);
                }
                sb.AppendLine(terminator);
            }

            // If File Is In Use, Abort The Operation Else Write To Disk
            try
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex.Message);
                return;
            }
        }
    }
}