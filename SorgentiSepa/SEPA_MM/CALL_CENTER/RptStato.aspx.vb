Imports System.IO

Partial Class CALL_CENTER_RptStato
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Ricerca()
        End If
    End Sub
    Private Sub Ricerca()
        Try


            Dim whereCond As String = ""


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim TOTAPERTI As Integer = 0
            Dim TOTCARICO As Integer = 0
            Dim TOTSOPRALL As Integer = 0
            Dim TOTORDINE As Integer = 0
            Dim TOTRESPINTI As Integer = 0
            Dim TOTCHIUSI As Integer = 0
            Dim TOTANNULLATI As Integer = 0
            Dim TOTTOTALE As Integer = 0
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            Me.lbTitolo.Text = "Sintesi stato interventi"
            Me.lblSpiega.Text = "Informazioni sul numero degli interventi in relazione al loro stato"
            Me.lblFiltri.Text = ""

            Me.lblFiltri.Text = ""

            Dim swhere As Boolean = True
            Dim e As String = ""

            If Not String.IsNullOrEmpty(Request.QueryString("DAL")) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_ORA_RICHIESTA,1,8),'30000000') >='" & Request.QueryString("DAL") & "' "
                Me.lblFiltri.Text = lblFiltri.Text & " Data apertura dal: " & par.FormattaData(Request.QueryString("DAL"))

            End If

            If Not String.IsNullOrEmpty(Request.QueryString("AL")) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_CHIUSURA,1,8),'10000000') <='" & Request.QueryString("AL") & "'"
                Me.lblFiltri.Text = lblFiltri.Text & " Data chiusura al: " & par.FormattaData(Request.QueryString("AL"))

            End If

            If par.IfEmpty(Request.QueryString("TIPO"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " " & e & " ID_TIPOLOGIE =" & Request.QueryString("TIPO")
                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI WHERE ID = " & Request.QueryString("TIPO")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Tipologia: " & par.IfNull(lettore("DESCRIZIONE"), "")
                End If
                lettore.Close()
            End If

            If par.IfEmpty(Request.QueryString("STR"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " ID_STRUTTURA  = " & Request.QueryString("STR")
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID = " & Request.QueryString("STR")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Filiale: " & par.IfNull(lettore("NOME"), "")
                End If
                lettore.Close()
            End If

            If par.IfEmpty(Request.QueryString("OP"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " " & e & " ID_OPERATORE_INS  = " & Request.QueryString("OP")
                par.cmd.CommandText = "SELECT OPERATORE FROM OPERATORI WHERE ID = " & Request.QueryString("OP")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Operatore: " & par.IfNull(lettore("OPERATORE"), "")
                End If
                lettore.Close()
            End If

            par.cmd.CommandText = "SELECT TIPOLOGIE_GUASTI.DESCRIZIONE AS TIPO," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=0 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS APERTI, " _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=3 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS IN_cARICO," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=1 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS IN_SOPRALLUOGO," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=4 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS IN_ORDINE," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=5 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS RESPINTI," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=10 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS CHIUSI," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=2 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS ANNULLATI," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS TOTALE " _
                & "FROM SISCOM_MI.TIPOLOGIE_GUASTI ORDER BY TIPOLOGIE_GUASTI.DESCRIZIONE ASC"

            'par.cmd.CommandText = "SELECT " _
            '                    & "TIPOLOGIE_GUASTI.descrizione  AS tipo, " _
            '                    & "(SELECT COUNT (ID) FROM siscom_mi.SEGNALAZIONI  WHERE id_stato = 0 AND TIPO_RICHIESTA = A.TIPO_RICHIESTA) AS aperti, " _
            '                    & "(SELECT COUNT (ID) FROM siscom_mi.SEGNALAZIONI  WHERE id_stato = 3 AND TIPO_RICHIESTA = A.TIPO_RICHIESTA) AS in_carico, " _
            '                    & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_STATO = 1 AND TIPO_RICHIESTA = A.TIPO_RICHIESTA)AS IN_SOPRALLUOGO, " _
            '                    & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_STATO = 4 AND TIPO_RICHIESTA = A.TIPO_RICHIESTA)AS IN_ORDINE, " _
            '                    & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_STATO = 10 AND TIPO_RICHIESTA = A.TIPO_RICHIESTA)AS CHIUSI, " _
            '                    & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_STATO = 0 AND TIPO_RICHIESTA = A.TIPO_RICHIESTA)AS ANNULLATI, " _
            '                    & "COUNT (A.ID) AS TOTALE FROM siscom_mi.SEGNALAZIONI A ,siscom_mi.TIPOLOGIE_GUASTI " _
            '                    & "WHERE TIPOLOGIE_GUASTI.ID = A.id_tipologie AND TIPO_RICHIESTA = 1 " & whereCond _
            '                    & " GROUP BY A.TIPO_RICHIESTA,DESCRIZIONE "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            Dim riga As Data.DataRow
            For Each row As Data.DataRow In dt.Rows

                'riga = dt.NewRow()
                'riga.Item("TIPO") = "TOTALE " & row.Item("TIPO")
                'riga.Item("IN_CARICO") = row.Item("IN_CARICO")
                'riga.Item("IN_SOPRALLUOGO") = row.Item("IN_SOPRALLUOGO")
                'riga.Item("IN_ORDINE") = row.Item("IN_ORDINE")
                'riga.Item("CHIUSI") = row.Item("CHIUSI")
                'dt.Rows.Add(riga)
                TOTAPERTI = TOTAPERTI + row.Item("APERTI")

                TOTCARICO = TOTCARICO + row.Item("IN_CARICO")
                TOTSOPRALL = TOTSOPRALL + row.Item("IN_SOPRALLUOGO")
                TOTORDINE = TOTORDINE + row.Item("IN_ORDINE")
                TOTRESPINTI = TOTRESPINTI + row.Item("RESPINTI")
                TOTCHIUSI = TOTCHIUSI + row.Item("CHIUSI")
                TOTANNULLATI = TOTANNULLATI + row.Item("ANNULLATI")
                TOTTOTALE = TOTTOTALE + row.Item("TOTALE")
            Next

            riga = dt.NewRow()

            riga = dt.NewRow()
            riga.Item("TIPO") = "TOTALE COMPLESSIVO"
            riga.Item("APERTI") = TOTAPERTI
            riga.Item("IN_CARICO") = TOTCARICO
            riga.Item("IN_SOPRALLUOGO") = TOTSOPRALL
            riga.Item("IN_ORDINE") = TOTORDINE
            riga.Item("RESPINTI") = TOTRESPINTI
            riga.Item("CHIUSI") = TOTCHIUSI
            riga.Item("ANNULLATI") = TOTANNULLATI
            riga.Item("TOTALE") = TOTTOTALE

            dt.Rows.Add(riga)



            Me.DataGridRptTipo.DataSource = dt
            Me.DataGridRptTipo.DataBind()


            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRptTipo, "RptSegn", False)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If


    End Sub
End Class
