Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ElencoGeneraleApplicazioneAU
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            Carica()
        End If
    End Sub

    Private Function Carica()
        Try
            Dim comunicazioni As String = ""
            Dim LimiteIsee As Integer = 0
            Dim DAFARE As Boolean
            Dim CANONE91 As String = ""
            Dim dt As New System.Data.DataTable
            Dim ROW As System.Data.DataRow
            Dim I As Integer = 0
            Dim NUMERORIGHE As Long = 0
            Dim Contatore As Long = 0
            Dim Anomalia As Boolean = False

            par.OracleConn.Open()
            par.SettaCommand(par)


            dt.Columns.Add("COD_CONTRATTO")
            dt.Columns.Add("PG_DICHIARAZIONE_AU_2009")
            dt.Columns.Add("DATA_STIPULA")
            dt.Columns.Add("NUM_COMP")
            dt.Columns.Add("MINORI_15")
            dt.Columns.Add("MAGGIORI_65")
            dt.Columns.Add("NUM_COMP_66")
            dt.Columns.Add("NUM_COMP_100")
            dt.Columns.Add("NUM_COMP_100_CON")
            dt.Columns.Add("PSE")
            dt.Columns.Add("VSE")
            dt.Columns.Add("REDDITI_DIPENDENTI")
            dt.Columns.Add("REDDITI_ALTRI")
            dt.Columns.Add("COEFF_NUCLEO_FAM")
            dt.Columns.Add("LIMITE_PENSIONI")
            dt.Columns.Add("REDD_PREV_DIP")
            dt.Columns.Add("REDD_COMPLESSIVO")
            dt.Columns.Add("REDD_IMMOBILIARI")
            dt.Columns.Add("REDD_MOBILIARI")
            dt.Columns.Add("ISE")
            dt.Columns.Add("DETRAZIONI")
            dt.Columns.Add("DETRAZIONI_FRAGILITA")
            dt.Columns.Add("ISEE")
            dt.Columns.Add("ISR")
            dt.Columns.Add("ISP")
            dt.Columns.Add("ISEE_27")
            dt.Columns.Add("ID_AREA_ECONOMICA")
            dt.Columns.Add("SOTTO_AREA")
            dt.Columns.Add("LIMITE_ISEE")
            dt.Columns.Add("DECADENZA_ALL_ADEGUATO")
            dt.Columns.Add("DECADENZA_VAL_ICI")
            dt.Columns.Add("PATRIMONIO_SUP")
            dt.Columns.Add("ANNO_COSTRUZIONE")
            dt.Columns.Add("LOCALITA")
            dt.Columns.Add("NUMERO_PIANO")
            dt.Columns.Add("PRESENTE_ASCENSORE")
            dt.Columns.Add("PIANO")
            dt.Columns.Add("DEM")
            dt.Columns.Add("ZONA")
            dt.Columns.Add("COSTOBASE")
            dt.Columns.Add("VETUSTA")
            dt.Columns.Add("CONSERVAZIONE")
            dt.Columns.Add("SUP_NETTA")
            dt.Columns.Add("SUPCONVENZIONALE")
            dt.Columns.Add("ALTRE_SUP")
            dt.Columns.Add("SUP_ACCESSORI")
            dt.Columns.Add("VALORE_LOCATIVO")
            dt.Columns.Add("PERC_VAL_LOC")
            dt.Columns.Add("CANONE_CLASSE")
            dt.Columns.Add("PERC_ISTAT_APPLICATA")
            dt.Columns.Add("CANONE_CLASSE_ISTAT")
            dt.Columns.Add("INC_MAX")
            dt.Columns.Add("CANONE_SOPPORTABILE")
            dt.Columns.Add("CANONE_MINIMO_AREA")
            dt.Columns.Add("CANONE")
            dt.Columns.Add("CANONE_ATTUALE")
            dt.Columns.Add("ADEGUAMENTO")
            dt.Columns.Add("ISTAT")
            dt.Columns.Add("NOTE")
            dt.Columns.Add("ANNOTAZIONI")
            dt.Columns.Add("CANONE_91")
            dt.Columns.Add("ID_DICHIARAZIONE")
            dt.Columns.Add("ID_GRUPPO")


            Dim IDAU As Long = 0
            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IDAU = myReader("ID")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT CANONI_EC_ELABORAZIONI.COD_CONTRATTO,UTENZA_DICHIARAZIONI.PG AS PG_DICHIARAZIONE_AU_2009,DATA_STIPULA,NUM_COMP,MINORI_15 AS MINORI_15_ANNI,MAGGIORI_65 AS MAGGIORI_65_ANNI,NUM_COMP_66 AS COMP_INVALIDITA_TRA_66_E_100,NUM_COMP_100 AS COMP_INVALIDITA_100,NUM_COMP_100_CON AS COMP_INVALIDITA_100_INDENNITA,CANONI_EC_ELABORAZIONI.PSE,CANONI_EC_ELABORAZIONI.VSE,REDDITI_DIP AS REDDITI_DIPENDENTI,REDDITI_ATRI AS REDDITI_ALTRO_TIPO,COEFF_NUCLEO_FAM,LIMITE_PENSIONI,DECODE(REDD_PREV_DIP,0,'AUTONOMO',1,'DIPENDENTE') AS PREVALENZA_REDDITO,REDD_COMPLESSIVO AS REDDITO_COMPLESSIVO,REDD_IMMOBILIARI AS REDDITO_IMMOBILIARE,REDD_MOBILIARI AS REDDITO_MOBILIARE,ISE,DETRAZIONI AS DETRAZIONI_REDDITO,DETRAZIONI_FRAGILITA,CANONI_EC_ELABORAZIONI.ISEE,ISR,ISP,ISEE_27 AS ISEE_RIFERIMENTO,DECODE(ID_AREA_ECONOMICA,1,'PROTEZIONE',2,'ACCESSO',3,'PERMANENZA',4,'DECADENZA') AS AREA_ECONOMICA," _
                                & "SOTTO_AREA AS CLASSE,DECODE(LIMITE_ISEE,1,'SI',0,'NO') AS DECADENZA_ISEE,DECODE(DECADENZA_ALL_ADEGUATO,1,'SI',0,'NO') AS DECADENZA_IMMOBILE_ADEGUATO,DECODE(DECADENZA_VAL_ICI,1,'SI',0,'NO') AS DECADENZA_VALORI_IMMOBILIARI,DECODE(PATRIMONIO_SUP,1,'SI',0,'NO') AS LIMITE_PATRIMONIO_SUP,ANNO_COSTRUZIONE AS ANNO_COSTR_RISTR,LOCALITA,NUMERO_PIANO,DECODE(PRESENTE_ASCENSORE,1,'SI',0,'NO') AS PRESENTE_ASCENSORE,CANONI_EC_ELABORAZIONI.PIANO AS COEFF_PIANO,DEM AS COEFF_DEMOGRAFIA,ZONA AS COEFF_UBICAZIONE,COSTOBASE AS COSTO_BASE,VETUSTA AS COEFF_VETUSTA,CONSERVAZIONE AS COEFF_CONSERVAZIONE,SUP_NETTA AS SUP_NETTA,SUPCONVENZIONALE AS SUP_CONVENZIONALE,ALTRE_SUP AS ALTRE_SUPERFICI,SUP_ACCESSORI AS SUPERFICI_ACCESSORI,VALORE_LOCATIVO,PERC_VAL_LOC AS PERCENTUALE_CANONE_CLASSE,CANONE_CLASSE,PERC_ISTAT_APPLICATA AS ISTAT_CANONE_CLASSE,CANONE_CLASSE_ISTAT,INC_MAX AS INCIDENZA_ISE,CANONE_SOPPORTABILE," _
                                & "CANONE_MINIMO_AREA,CANONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT, CANONI_EC_ELABORAZIONI.NOTE, ANNOTAZIONI, CANONE_91 " _
                                & "FROM " _
                                & "UTENZA_DICHIARAZIONI, SISCOM_MI.CANONI_EC_ELABORAZIONI " _
                                & "WHERE CANONI_EC_ELABORAZIONI.ID_BANDO_AU=" & IDAU & " AND CANONI_EC_ELABORAZIONI.ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID (+) ORDER BY id_area_economica ASC,sotto_area ASC"
            myReader = par.cmd.ExecuteReader()

            While myReader.Read


                ROW = dt.NewRow()
                ROW.Item("COD_CONTRATTO") = par.IfNull(myReader("COD_CONTRATTO"), "")
                ROW.Item("PG_DICHIARAZIONE_AU_2009") = par.IfNull(myReader("PG_DICHIARAZIONE_AU_2009"), "")
                ROW.Item("DATA_STIPULA") = par.IfNull(myReader("DATA_STIPULA"), "")
                ROW.Item("NUM_COMP") = par.IfNull(myReader("NUM_COMP"), "")
                ROW.Item("MINORI_15") = par.IfNull(myReader("MINORI_15_ANNI"), "")
                ROW.Item("MAGGIORI_65") = par.IfNull(myReader("MAGGIORI_65_ANNI"), "")
                ROW.Item("NUM_COMP_66") = par.IfNull(myReader("COMP_INVALIDITA_TRA_66_E_100"), "")
                ROW.Item("NUM_COMP_100") = par.IfNull(myReader("COMP_INVALIDITA_100"), "")
                ROW.Item("NUM_COMP_100_CON") = par.IfNull(myReader("COMP_INVALIDITA_100_INDENNITA"), "")
                ROW.Item("PSE") = par.IfNull(myReader("PSE"), "")
                ROW.Item("VSE") = par.IfNull(myReader("VSE"), "")
                ROW.Item("REDDITI_DIPENDENTI") = Format(CDbl(par.IfNull(myReader("REDDITI_DIPENDENTI"), "0")), "##,##0.00")
                ROW.Item("REDDITI_ALTRI") = Format(CDbl(par.IfNull(myReader("REDDITI_ALTRO_TIPO"), "0")), "##,##0.00")
                ROW.Item("COEFF_NUCLEO_FAM") = par.IfNull(myReader("COEFF_NUCLEO_FAM"), "")
                ROW.Item("LIMITE_PENSIONI") = par.IfNull(myReader("LIMITE_PENSIONI"), "")
                ROW.Item("REDD_PREV_DIP") = par.IfNull(myReader("PREVALENZA_REDDITO"), "")
                ROW.Item("REDD_COMPLESSIVO") = Format(CDbl(par.IfNull(myReader("REDDITO_COMPLESSIVO"), "0")), "##,##0.00")
                ROW.Item("REDD_IMMOBILIARI") = Format(CDbl(par.IfNull(myReader("REDDITO_IMMOBILIARE"), "0")), "##,##0.00")
                ROW.Item("REDD_MOBILIARI") = Format(CDbl(par.IfNull(myReader("REDDITO_MOBILIARE"), "0")), "##,##0.00")
                ROW.Item("ISE") = par.Tronca(par.IfNull(myReader("ISE"), "0"))
                ROW.Item("DETRAZIONI") = Format(CDbl(par.IfNull(myReader("DETRAZIONI_REDDITO"), "0")), "##,##0.00")
                ROW.Item("DETRAZIONI_FRAGILITA") = Format(CDbl(par.IfNull(myReader("DETRAZIONI_FRAGILITA"), "0")), "##,##0.00")
                ROW.Item("ISEE") = par.Tronca(par.IfNull(myReader("ISEE"), "0"))
                ROW.Item("ISR") = par.Tronca(par.IfNull(myReader("ISR"), "0"))
                ROW.Item("ISP") = par.IfNull(myReader("ISP"), "0")
                ROW.Item("ISEE_27") = par.Tronca(par.IfNull(myReader("ISEE_RIFERIMENTO"), "0"))
                ROW.Item("ID_AREA_ECONOMICA") = par.IfNull(myReader("AREA_ECONOMICA"), "")


                ROW.Item("SOTTO_AREA") = par.IfNull(myReader("CLASSE"), "")
                If par.IfNull(myReader("CLASSE"), "") = "D6" Then
                    ROW.Item("REDD_PREV_DIP") = ""
                End If

                ROW.Item("LIMITE_ISEE") = par.IfNull(myReader("DECADENZA_ISEE"), "")


                ROW.Item("DECADENZA_ALL_ADEGUATO") = par.IfNull(myReader("DECADENZA_IMMOBILE_ADEGUATO"), "")


                ROW.Item("DECADENZA_VAL_ICI") = par.IfNull(myReader("DECADENZA_VALORI_IMMOBILIARI"), "")


                ROW.Item("PATRIMONIO_SUP") = par.IfNull(myReader("LIMITE_PATRIMONIO_SUP"), "")


                ROW.Item("ANNO_COSTRUZIONE") = par.IfNull(myReader("ANNO_COSTR_RISTR"), "")
                ROW.Item("LOCALITA") = par.IfNull(myReader("LOCALITA"), "")
                ROW.Item("NUMERO_PIANO") = par.IfNull(myReader("NUMERO_PIANO"), "")
                ROW.Item("PRESENTE_ASCENSORE") = par.IfNull(myReader("PRESENTE_ASCENSORE"), "")
                ROW.Item("PIANO") = par.IfNull(myReader("COEFF_PIANO"), "")
                ROW.Item("DEM") = par.IfNull(myReader("COEFF_DEMOGRAFIA"), "")
                ROW.Item("ZONA") = par.IfNull(myReader("COEFF_UBICAZIONE"), "")
                ROW.Item("COSTOBASE") = par.IfNull(myReader("COSTO_BASE"), "")
                ROW.Item("VETUSTA") = par.IfNull(myReader("COEFF_VETUSTA"), "")
                ROW.Item("CONSERVAZIONE") = par.IfNull(myReader("COEFF_CONSERVAZIONE"), "")
                ROW.Item("SUP_NETTA") = par.IfNull(myReader("SUP_NETTA"), "")
                ROW.Item("SUPCONVENZIONALE") = par.IfNull(myReader("SUP_CONVENZIONALE"), "")
                ROW.Item("ALTRE_SUP") = par.IfNull(myReader("ALTRE_SUPERFICI"), "")
                ROW.Item("SUP_ACCESSORI") = par.IfNull(myReader("SUPERFICI_ACCESSORI"), "")
                ROW.Item("VALORE_LOCATIVO") = par.Tronca(par.IfNull(myReader("VALORE_LOCATIVO"), ""))
                ROW.Item("PERC_VAL_LOC") = par.IfNull(myReader("PERCENTUALE_CANONE_CLASSE"), "")
                ROW.Item("CANONE_CLASSE") = Format(CDbl(par.IfNull(myReader("CANONE_CLASSE"), "0")), "##,##0.00")
                ROW.Item("PERC_ISTAT_APPLICATA") = par.IfNull(myReader("ISTAT_CANONE_CLASSE"), "")
                ROW.Item("CANONE_CLASSE_ISTAT") = Format(CDbl(par.IfNull(myReader("CANONE_CLASSE_ISTAT"), "0")), "##,##0.00")
                ROW.Item("INC_MAX") = par.IfNull(myReader("INCIDENZA_ISE"), "")
                ROW.Item("CANONE_SOPPORTABILE") = Format(CDbl(par.IfNull(myReader("CANONE_SOPPORTABILE"), "0")), "##,##0.00")
                ROW.Item("CANONE_MINIMO_AREA") = Format(CDbl(par.IfNull(myReader("CANONE_MINIMO_AREA"), "0")), "##,##0.00")
                ROW.Item("CANONE") = Format(CDbl(myReader("CANONE")), "##,##0.00")
                ROW.Item("CANONE_ATTUALE") = Format(CDbl(par.IfNull(myReader("CANONE_ATTUALE"), 0)), "##,##0.00")
                ROW.Item("ADEGUAMENTO") = Format(CDbl(par.IfNull(myReader("ADEGUAMENTO"), 0)), "##,##0.00")
                ROW.Item("ISTAT") = par.IfNull(myReader("ISTAT"), "0,00")
                ROW.Item("NOTE") = par.IfNull(myReader("NOTE"), "")
                ROW.Item("ANNOTAZIONI") = par.IfNull(myReader("ANNOTAZIONI"), "")
                If CANONE91 <> "" Then
                    ROW.Item("CANONE_91") = Format(CDbl(par.IfNull(myReader("CANONE_91"), "0")), "##,##0.00")
                Else
                    ROW.Item("CANONE_91") = ""
                End If





                I = I + 1
                dt.Rows.Add(ROW)
            End While
            myReader.Close()

            If I > 0 Then
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                HttpContext.Current.Session.Add("AA", dt)

                Label1.Text = "Elenco AU (" & DataGrid1.Items.Count & " nella lista)"



            Else
                Response.Write("<script>alert('Nessuna AU trovata.');</script>")
            End If


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function




End Class
