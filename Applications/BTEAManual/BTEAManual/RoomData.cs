namespace BTEAManual
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    class RoomData
    {
        public static string FileName
        { get; private set; }

        public static void Export()
        {
            string fileName = $"PTL__{Params.BTEAPropID}__room__" +
                $"{Main.SelectedDate.AddDays(1).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).ToString("yyyyMMddHHmmss")}.txt";
            FileName = fileName;

            var data = Records();
            Exporter.WriteToFile(data, fileName, Params.BTEADateFormat, Params.BTEAFieldDelimeter, Params.BTEARowTerminator);
        }

        private static DataTable Records()
        {
            var data = new DataTable();

            using (var con = new SqlConnection(Params.ProtelConString))
            {
                try
                {
                    con.Open();

                    using (var adapter = new SqlDataAdapter($@"
                    select
	                    RoomId = case kat.zimmer when 1 then leisthis.zimmer else ukto.kto end,
	                    RoomRev = sum(isnull(case ukto.statgrp when 0 then leisthis.epreis * leisthis.anzahl end, 0)),
	                    FBRev = sum(isnull(case ukto.statgrp when 1 then leisthis.epreis * leisthis.anzahl end, 0)),
	                    NotUsed = '',
	                    OtherRev = sum(isnull(case ukto.statgrp when 2 then leisthis.epreis * leisthis.anzahl end, 0)),
	                    RoomType = kat.kat,
	                    Checkin = case kat.zimmer when 1 then buch.datumvon else null end,
	                    Checkout = case kat.zimmer when 1 then buch.datumbis else null end,
	                    GuestCountry = case kat.zimmer when 1 then isnull(upper(natcode.isocode), 'XX') else null end,
	                    GenderOfMainGuest = case kat.zimmer when 1 then case kunden.gender when 1 then 'M' when 2 then 'M' else '' end else null end,
	                    BirthDate = case kunden.gebdat when '1900-01-01 00:00:00.000' then null else kunden.gebdat end,
	                    Adult = buch.anzerw,
	                    Child = buch.anzkin1+buch.anzkin2+buch.anzkin3+buch.anzkin4,
	                    BusinessDate = {Main.SelectedDate.ToString("yyyyMMdd")},
	                    IsNotRoomData = case kat.zimmer when 1 then 'N' else 'Y' end
                    from
	                    leisthis
	                    left join ukto on ukto.ktonr=leisthis.ukto
	                    left join buch on buch.leistacc=leisthis.origin
	                    left join kat on kat.katnr=buch.katnr
	                    left join kunden on kunden.kdnr=leisthis.kundennr
	                    left join natcode on natcode.codenr=kunden.landkz
                    where
	                    buch.datumbis = @CheckoutDate
	                    and rkz = @PaymentRef
	                    and buchstatus = @CheckedOutStatus
                    group by
	                    case kat.zimmer when 1 then leisthis.zimmer else ukto.kto end,
	                    kat.kat,
	                    buch.datumvon,
	                    buch.datumbis,
	                    natcode.isocode,
	                    kunden.gender,
	                    kunden.gebdat,
	                    buch.anzerw,
	                    buch.anzkin1,
	                    buch.anzkin2,
	                    buch.anzkin3,
	                    buch.anzkin4,
	                    kat.zimmer
                    order by 
	                    kat.kat", con))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@CheckoutDate", Main.SelectedDate.ToString("yyyy/MM/dd"));
                        adapter.SelectCommand.Parameters.AddWithValue("@PaymentRef", 0);
                        adapter.SelectCommand.Parameters.AddWithValue("@CheckedOutStatus", 2);
                        adapter.Fill(data);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteError(ex.Message);
                }
            }
            return data;
        }
    }
}