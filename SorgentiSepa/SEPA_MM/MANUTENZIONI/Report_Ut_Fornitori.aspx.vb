
Partial Class MANUTENZIONI_Report_Ut_Fornitori
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim IdFornitore As String = Request.QueryString("FORNITORE")
            Dim TipoImmobile As String
            Dim Str As String

            If Request.QueryString("TIPO") = 0 Then
                TipoImmobile = "COMPLESSI"
            Else
                TipoImmobile = "EDIFICII"
            End If
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"
            Response.Write(Str)
            Response.Flush()
            Try
                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt As New Data.DataTable

                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Dim f As String = ""

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.ANAGRAFICA_FORNITORI WHERE ID = " & IdFornitore
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    f = myReader(0).ToString
                End If
                myReader.Close()
                Me.LblTitolo.Text = LblTitolo.Text & " ASSOCIATE A " & TipoImmobile & ", FORNITORE: " & f

                If Request.QueryString("TIPO") = 0 Then
                    par.cmd.CommandText = "SELECT DISTINCT UTENZE.ID AS ID_UTENZA,COMPLESSI_IMMOBILIARI.ID AS ID_IMMOBILE, COMPLESSI_IMMOBILIARI.COD_COMPLESSO as COD_IMMOBILE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS DENOMINAZIONE_IMMOBILE,TIPOLOGIA_UTENZA.DESCRIZIONE AS TIPOLOGIA , ANAGRAFICA_FORNITORI.DESCRIZIONE AS FORNITORE, UTENZE.CONTATORE, UTENZE.CONTRATTO, UTENZE.DESCRIZIONE AS DESCRIZIONE_UTENZA FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.UTENZE, SISCOM_MI.ANAGRAFICA_FORNITORI, SISCOM_MI.TIPOLOGIA_UTENZA, SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI WHERE COMPLESSI_IMMOBILIARI.ID = TABELLE_MILLESIMALI.ID_COMPLESSO AND SISCOM_MI.TABELLE_MILLESIMALI.ID = SISCOM_MI.UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE AND SISCOM_MI.UTENZE_TABELLE_MILLESIMALI.ID_UTENZA = UTENZE.ID AND UTENZE.COD_TIPOLOGIA = TIPOLOGIA_UTENZA.COD AND ANAGRAFICA_FORNITORI.ID= UTENZE.ID_FORNITORE  AND ID_FORNITORE = " & IdFornitore & " ORDER by DENOMINAZIONE_IMMOBILE ASC"
                Else
                    par.cmd.CommandText = "SELECT DISTINCT UTENZE.ID AS ID_UTENZA,EDIFICI.ID AS ID_IMMOBILE, EDIFICI.COD_EDIFICIO as COD_IMMOBILE, EDIFICI.DENOMINAZIONE AS DENOMINAZIONE_IMMOBILE,TIPOLOGIA_UTENZA.DESCRIZIONE AS TIPOLOGIA , ANAGRAFICA_FORNITORI.DESCRIZIONE AS FORNITORE, UTENZE.CONTATORE, UTENZE.CONTRATTO, UTENZE.DESCRIZIONE AS DESCRIZIONE_UTENZA FROM SISCOM_MI.EDIFICI, SISCOM_MI.UTENZE, SISCOM_MI.ANAGRAFICA_FORNITORI, SISCOM_MI.TIPOLOGIA_UTENZA, SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI WHERE EDIFICI.ID = TABELLE_MILLESIMALI.ID_EDIFICIO AND SISCOM_MI.TABELLE_MILLESIMALI.ID = SISCOM_MI.UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE AND SISCOM_MI.UTENZE_TABELLE_MILLESIMALI.ID_UTENZA = UTENZE.ID AND UTENZE.COD_TIPOLOGIA = TIPOLOGIA_UTENZA.COD AND ANAGRAFICA_FORNITORI.ID= UTENZE.ID_FORNITORE  AND ID_FORNITORE = " & IdFornitore & " ORDER by DENOMINAZIONE_IMMOBILE ASC"
                End If

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt)
                If dt.Rows.Count > 0 Then

                    DataGridRptUtenze.DataSource = dt
                    DataGridRptUtenze.DataBind()
                Else
                    Response.Write("<script>alert('Nessun risultato per questo fornitore!')</script>")
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script language='javascript'> { self.close() }</script>")
                    Exit Sub
                End If


                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
                par.OracleConn.Close()
            End Try

        End If

    End Sub
End Class
