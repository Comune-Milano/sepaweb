Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class MANUTENZIONI_rptman
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Dim euro As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""Portale.aspx""</script>")
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:300px; left:750px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        ' Response.Flush() 'se devo soltanto visualizzare risultati di query meglio metterlo qui


        If Not IsPostBack Then
            BindGrid()
            Response.Flush() 'se devo fare reindirizzamento per file xls meglio metterlo qui
        End If
    End Sub

    Private Sub BindGrid()

        Dim I As Integer = 0


        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            dt.Columns.Add("ROWNUM")
            dt.Columns.Add("ID")
            dt.Columns.Add("COD. IMMOBILE")
            dt.Columns.Add("DENOMINAZIONE")
            dt.Columns.Add("INDIRIZZO")
            dt.Columns.Add("CIVICO")
            dt.Columns.Add("DATA ORDINE")
            dt.Columns.Add("DATA INIZIO INTERVENTO")
            dt.Columns.Add("DATA FINE INTERVENTO")
            dt.Columns.Add("REVERSIBILE")
            dt.Columns.Add("COSTO REVERSIBILE")
            dt.Columns.Add("TIPO INTERVENTO")
            dt.Columns.Add("TIPO SERVIZIO")
            dt.Columns.Add("DESCRIZIONE INTERVENTO")

            Dim RIGA As System.Data.DataRow

            ' RICERCA PER COMPLESSI
            par.cmd.CommandText = "SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID, COMPLESSI_IMMOBILIARI.cod_complesso AS COD_IMMOBILE,COMPLESSI_IMMOBILIARI.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_ORDINE,'yyyymmdd'),'dd/mm/yyyy')AS DATA_ORDINE,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_FINE_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_FINE,(CASE (INTERVENTI_MANUTENZIONE.REVERSIBILE) WHEN 0 THEN 'NO' ELSE 'SI' END) AS REVERSIBILE,INTERVENTI_MANUTENZIONE.COSTO_REVERSIBILE AS ""COSTO REVERSIBILE"",INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO,TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE   COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_COMPLESSO AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read()
                If IsDBNull(myReader1("COSTO REVERSIBILE")) Then
                    euro = ""
                Else
                    euro = " €"
                End If
                RIGA = dt.NewRow()
                RIGA.Item("ROWNUM") = myReader1("ROWNUM")
                RIGA.Item("ID") = par.IfNull(myReader1("ID"), " ")
                RIGA.Item("COD. IMMOBILE") = par.IfNull(myReader1("COD_IMMOBILE"), " ")
                RIGA.Item("DENOMINAZIONE") = par.IfNull(myReader1("DENOMINAZIONE"), " ")
                RIGA.Item("INDIRIZZO") = par.IfNull(myReader1("DESCRIZIONE"), " ")
                RIGA.Item("CIVICO") = par.IfNull(myReader1("CIVICO"), " ")
                RIGA.Item("DATA ORDINE") = par.IfNull(myReader1("DATA_ORDINE"), " ")
                RIGA.Item("DATA INIZIO INTERVENTO") = par.IfNull(myReader1("DATA_INIZIO"), " ")
                RIGA.Item("DATA FINE INTERVENTO") = par.IfNull(myReader1("DATA_FINE"), " ")
                RIGA.Item("REVERSIBILE") = par.IfNull(myReader1("REVERSIBILE"), " ")
                RIGA.Item("COSTO REVERSIBILE") = par.IfNull(myReader1("COSTO REVERSIBILE"), " ") & euro
                RIGA.Item("TIPO INTERVENTO") = par.IfNull(myReader1("TIPO_INTERVENTO"), " ")
                RIGA.Item("TIPO SERVIZIO") = par.IfNull(myReader1("TIPO_SERVIZIO"), " ")
                RIGA.Item("DESCRIZIONE INTERVENTO") = par.IfNull(myReader1("DESCRIZIONE_INTERVENTO"), " ")

                dt.Rows.Add(RIGA)
            End While

            'RICERCA PER EDIFICI
            par.cmd.CommandText = "SELECT ROWNUM,Interventi_manutenzione.id,edifici.cod_edificio AS COD_IMMOBILE, EDIFICI.DENOMINAZIONE, INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_ORDINE,'yyyymmdd'),'dd/mm/yyyy')AS DATA_ORDINE,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_FINE_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_FINE,(CASE (INTERVENTI_MANUTENZIONE.REVERSIBILE) WHEN 0 THEN 'NO' ELSE 'SI' END) AS REVERSIBILE,INTERVENTI_MANUTENZIONE.COSTO_REVERSIBILE AS ""COSTO REVERSIBILE"",INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE ,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE  EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.edifici.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.id_edificio AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO"

            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read()
                If IsDBNull(myReader1("COSTO REVERSIBILE")) Then
                    euro = " "
                Else
                    euro = " €"
                End If
                RIGA = dt.NewRow()
                RIGA.Item("ROWNUM") = myReader1("ROWNUM")
                RIGA.Item("ID") = par.IfNull(myReader1("ID"), " ")
                RIGA.Item("COD. IMMOBILE") = par.IfNull(myReader1("COD_IMMOBILE"), " ")
                RIGA.Item("DENOMINAZIONE") = par.IfNull(myReader1("DENOMINAZIONE"), " ")
                RIGA.Item("INDIRIZZO") = par.IfNull(myReader1("DESCRIZIONE"), " ")
                RIGA.Item("CIVICO") = par.IfNull(myReader1("CIVICO"), " ")
                RIGA.Item("DATA ORDINE") = par.IfNull(myReader1("DATA_ORDINE"), " ")
                RIGA.Item("DATA INIZIO INTERVENTO") = par.IfNull(myReader1("DATA_INIZIO"), " ")
                RIGA.Item("DATA FINE INTERVENTO") = par.IfNull(myReader1("DATA_FINE"), " ")
                RIGA.Item("REVERSIBILE") = par.IfNull(myReader1("REVERSIBILE"), " ")
                RIGA.Item("COSTO REVERSIBILE") = par.IfNull(myReader1("COSTO REVERSIBILE"), " ") & euro
                RIGA.Item("TIPO INTERVENTO") = par.IfNull(myReader1("TIPO_INTERVENTO"), " ")
                RIGA.Item("TIPO SERVIZIO") = par.IfNull(myReader1("TIPO_SERVIZIO"), " ")
                RIGA.Item("DESCRIZIONE INTERVENTO") = par.IfNull(myReader1("DESCRIZIONE_INTERVENTO"), " ")

                dt.Rows.Add(RIGA)
            End While

            'RICERCA PER UNITA' COMUNI
            par.cmd.CommandText = "SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_ORDINE,'yyyymmdd'),'dd/mm/yyyy')AS DATA_ORDINE,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_FINE_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_FINE,(CASE (INTERVENTI_MANUTENZIONE.REVERSIBILE) WHEN 0 THEN 'NO' ELSE 'SI' END) AS REVERSIBILE,INTERVENTI_MANUTENZIONE.COSTO_REVERSIBILE AS ""COSTO REVERSIBILE"",INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE, SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE COMPLESSI_IMMOBILIARI.ID= UNITA_COMUNI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_COMUNE AND UNITA_COMUNI.id_complesso IS NOT NULL AND UNITA_COMUNI.id_edificio IS NULL AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO" & " " _
                & "UNION SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_ORDINE,'yyyymmdd'),'dd/mm/yyyy')AS DATA_ORDINE,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_FINE_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_FINE,(CASE (INTERVENTI_MANUTENZIONE.REVERSIBILE) WHEN 0 THEN 'NO' ELSE 'SI' END) AS REVERSIBILE,INTERVENTI_MANUTENZIONE.COSTO_REVERSIBILE AS ""COSTO REVERSIBILE"",INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE EDIFICI.ID= UNITA_COMUNI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_COMUNE AND UNITA_COMUNI.id_complesso IS NULL AND UNITA_COMUNI.id_edificio IS NOT NULL AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO"

            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read()
                If IsDBNull(myReader1("COSTO REVERSIBILE")) Then
                    euro = " "
                Else
                    euro = " €"
                End If
                RIGA = dt.NewRow()
                RIGA.Item("ROWNUM") = myReader1("ROWNUM")
                RIGA.Item("ID") = par.IfNull(myReader1("ID"), " ")
                RIGA.Item("COD. IMMOBILE") = par.IfNull(myReader1("COD_IMMOBILE"), " ")
                RIGA.Item("DENOMINAZIONE") = par.IfNull(myReader1("DENOMINAZIONE"), " ")
                RIGA.Item("INDIRIZZO") = par.IfNull(myReader1("DESCRIZIONE"), " ")
                RIGA.Item("CIVICO") = par.IfNull(myReader1("CIVICO"), " ")
                RIGA.Item("DATA ORDINE") = par.IfNull(myReader1("DATA_ORDINE"), " ")
                RIGA.Item("DATA INIZIO INTERVENTO") = par.IfNull(myReader1("DATA_INIZIO"), " ")
                RIGA.Item("DATA FINE INTERVENTO") = par.IfNull(myReader1("DATA_FINE"), " ")
                RIGA.Item("REVERSIBILE") = par.IfNull(myReader1("REVERSIBILE"), " ")
                RIGA.Item("COSTO REVERSIBILE") = par.IfNull(myReader1("COSTO REVERSIBILE"), " ") & euro
                RIGA.Item("TIPO INTERVENTO") = par.IfNull(myReader1("TIPO_INTERVENTO"), " ")
                RIGA.Item("TIPO SERVIZIO") = par.IfNull(myReader1("TIPO_SERVIZIO"), " ")
                RIGA.Item("DESCRIZIONE INTERVENTO") = par.IfNull(myReader1("DESCRIZIONE_INTERVENTO"), " ")

                dt.Rows.Add(RIGA)
            End While

            'RICERCA PER UNITA' IMMOBILIARI
            par.cmd.CommandText = "SELECT ROWNUM,INTERVENTI_MANUTENZIONE.ID,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_IMMOBILE, ('SCALA - '||(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala)||' INTERNO - '||UNITA_IMMOBILIARI.INTERNO)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_ORDINE,'yyyymmdd'),'dd/mm/yyyy')AS DATA_ORDINE,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_FINE_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_FINE,(CASE (INTERVENTI_MANUTENZIONE.REVERSIBILE) WHEN 0 THEN 'NO' ELSE 'SI' END) AS REVERSIBILE,INTERVENTI_MANUTENZIONE.COSTO_REVERSIBILE AS ""COSTO REVERSIBILE"",INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE, SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE EDIFICI.ID= UNITA_IMMOBILIARI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_IMMOBILIARE AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO "
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read()
                If IsDBNull(myReader1("COSTO REVERSIBILE")) Then
                    euro = ""
                Else
                    euro = " €"
                End If
                RIGA = dt.NewRow()
                RIGA.Item("ROWNUM") = myReader1("ROWNUM")
                RIGA.Item("ID") = par.IfNull(myReader1("ID"), " ")
                RIGA.Item("COD. IMMOBILE") = par.IfNull(myReader1("COD_IMMOBILE"), " ")
                RIGA.Item("DENOMINAZIONE") = par.IfNull(myReader1("DENOMINAZIONE"), " ")
                RIGA.Item("INDIRIZZO") = par.IfNull(myReader1("DESCRIZIONE"), " ")
                RIGA.Item("CIVICO") = par.IfNull(myReader1("CIVICO"), " ")
                RIGA.Item("DATA ORDINE") = par.IfNull(myReader1("DATA_ORDINE"), " ")
                RIGA.Item("DATA INIZIO INTERVENTO") = par.IfNull(myReader1("DATA_INIZIO"), " ")
                RIGA.Item("DATA FINE INTERVENTO") = par.IfNull(myReader1("DATA_FINE"), " ")
                RIGA.Item("REVERSIBILE") = par.IfNull(myReader1("REVERSIBILE"), " ")
                RIGA.Item("COSTO REVERSIBILE") = par.IfNull(myReader1("COSTO REVERSIBILE"), " ") & euro
                RIGA.Item("TIPO INTERVENTO") = par.IfNull(myReader1("TIPO_INTERVENTO"), " ")
                RIGA.Item("TIPO SERVIZIO") = par.IfNull(myReader1("TIPO_SERVIZIO"), " ")
                RIGA.Item("DESCRIZIONE INTERVENTO") = par.IfNull(myReader1("DESCRIZIONE_INTERVENTO"), " ")

                dt.Rows.Add(RIGA)
            End While

            'RICERCA PER IMPIANTI
            par.cmd.CommandText = "SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,IMPIANTI.COD_IMPIANTO AS COD_IMMOBILE, ('TIPO - '||TIPOLOGIA_IMPIANTI.DESCRIZIONE||'DESCRIZIONE - '||IMPIANTI.DESCRIZIONE)AS  DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_ORDINE,'yyyymmdd'),'dd/mm/yyyy')AS DATA_ORDINE,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_FINE_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_FINE,(CASE (INTERVENTI_MANUTENZIONE.REVERSIBILE) WHEN 0 THEN 'NO' ELSE 'SI' END) AS REVERSIBILE,INTERVENTI_MANUTENZIONE.COSTO_REVERSIBILE AS ""COSTO REVERSIBILE"",INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.TIPOLOGIA_IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.IMPIANTI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE, SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE COMPLESSI_IMMOBILIARI.ID= IMPIANTI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.IMPIANTI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_IMPIANTO AND IMPIANTI.ID_COMPLESSO IS NOT NULL AND IMPIANTI.ID_EDIFICIO IS NULL AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO AND TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " & " " _
                & "UNION SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,IMPIANTI.COD_IMPIANTO AS COD_IMMOBILE, ('TIPO - '||TIPOLOGIA_IMPIANTI.DESCRIZIONE||'DESCRIZIONE - '||IMPIANTI.DESCRIZIONE)AS  DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_ORDINE,'yyyymmdd'),'dd/mm/yyyy')AS DATA_ORDINE,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_FINE_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_FINE,(CASE (INTERVENTI_MANUTENZIONE.REVERSIBILE) WHEN 0 THEN 'NO' ELSE 'SI' END) AS REVERSIBILE,INTERVENTI_MANUTENZIONE.COSTO_REVERSIBILE AS ""COSTO REVERSIBILE"",INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.EDIFICI, SISCOM_MI.IMPIANTI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE EDIFICI.ID= IMPIANTI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.IMPIANTI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_IMPIANTO AND IMPIANTI.ID_COMPLESSO IS NULL AND IMPIANTI.ID_EDIFICIO IS NOT NULL AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO AND TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA"
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read()
                If IsDBNull(myReader1("COSTO REVERSIBILE")) Then
                    euro = " "
                Else
                    euro = " €"
                End If
                RIGA = dt.NewRow()
                RIGA.Item("ROWNUM") = myReader1("ROWNUM")
                RIGA.Item("ID") = par.IfNull(myReader1("ID"), " ")
                RIGA.Item("COD. IMMOBILE") = par.IfNull(myReader1("COD_IMMOBILE"), " ")
                RIGA.Item("DENOMINAZIONE") = par.IfNull(myReader1("DENOMINAZIONE"), " ")
                RIGA.Item("INDIRIZZO") = par.IfNull(myReader1("DESCRIZIONE"), " ")
                RIGA.Item("CIVICO") = par.IfNull(myReader1("CIVICO"), " ")
                RIGA.Item("DATA ORDINE") = par.IfNull(myReader1("DATA_ORDINE"), " ")
                RIGA.Item("DATA INIZIO INTERVENTO") = par.IfNull(myReader1("DATA_INIZIO"), " ")
                RIGA.Item("DATA FINE INTERVENTO") = par.IfNull(myReader1("DATA_FINE"), " ")
                RIGA.Item("REVERSIBILE") = par.IfNull(myReader1("REVERSIBILE"), " ")
                RIGA.Item("COSTO REVERSIBILE") = par.IfNull(myReader1("COSTO REVERSIBILE"), " ") & euro
                RIGA.Item("TIPO INTERVENTO") = par.IfNull(myReader1("TIPO_INTERVENTO"), " ")
                RIGA.Item("TIPO SERVIZIO") = par.IfNull(myReader1("TIPO_SERVIZIO"), " ")
                RIGA.Item("DESCRIZIONE INTERVENTO") = par.IfNull(myReader1("DESCRIZIONE_INTERVENTO"), " ")

                dt.Rows.Add(RIGA)
            End While

            Session.Add("MIADT", dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
    
            'LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
    '    Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    'End Sub

    'Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
    '    If txtid.Text = "" Then
    '        Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
    '    Else
    '        Session.Add("ID", txtid.Text)
    '        If Request.QueryString("CHIAMA") = "S" Then
    '            Response.Redirect("Servizio_Manut.aspx")
    '        Else
    '            Response.Redirect("Int_Manutenzione.aspx")
    '        End If
    '    End If

    'End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        'LBLID.Text = e.Item.Cells(0).Text
        'Label2.Text = "Hai selezionato Cod. Complesso: " & e.Item.Cells(1).Text

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
    e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("Export\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD. IMMOBILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DENOMINAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "DATA ORDINE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DATA INIZIO INTERVENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DATA FINE INTERVENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "REVERSIBILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "COSTO REVERSIBILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "TIPO INTERVENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "TIPO SERVIZIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "DESCRIZIONE INTERVENTO", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD. IMMOBILE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DENOMINAZIONE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA ORDINE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA INIZIO INTERVENTO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA FINE INTERVENTO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("REVERSIBILE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COSTO REVERSIBILE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO INTERVENTO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO SERVIZIO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE INTERVENTO"), 0)))

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("Export\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("Export\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)
            Response.Redirect("Export\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
