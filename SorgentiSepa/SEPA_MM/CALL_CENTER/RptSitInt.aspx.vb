Imports System.IO
Partial Class CALL_CENTER_RptSitInt
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
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Me.lbTitolo.Text = "Situazione Interventi"
            Me.lblSpiega.Text = "Informazioni sulle richieste d'intervento"
            Dim swhere As Boolean = True
            Dim e As String = ""
            Me.lblFiltri.Text = ""
            If Not String.IsNullOrEmpty(Request.QueryString("DAL")) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_ORA_RICHIESTA,1,8),'30000000') >='" & Request.QueryString("DAL") & "'"
                Me.lblFiltri.Text = lblFiltri.Text & " Data apertura dal: " & par.FormattaData(Request.QueryString("DAL"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("AL")) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_CHIUSURA,1,8),'10000000') <= '" & Request.QueryString("AL") & "'"
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
                whereCond = whereCond & " " & e & " segnalazioni.ID_STRUTTURA  = " & Request.QueryString("STR")
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

            If par.IfEmpty(Request.QueryString("ST"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " " & e & " SEGNALAZIONI.ID_STATO  = " & Request.QueryString("ST")
                par.cmd.CommandText = "SELECT DESCRIZIONE FROM siscom_mi.TAB_STATI_SEGNALAZIONI WHERE ID = " & Request.QueryString("ST")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Stato Segnalazione: " & par.IfNull(lettore("DESCRIZIONE"), "")
                End If
                lettore.Close()

            End If
            If par.IfEmpty(Request.QueryString("DAY"), "") <> "" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " AND NVL(TO_DATE(TO_CHAR(SYSDATE,'yyyymmdd'),'yyyymmdd') - TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,0,8),'yyyymmdd') ,0 )>=" & Request.QueryString("DAY")

                lblFiltri.Text = lblFiltri.Text & " Aperto da: " & Request.QueryString("DAY")
            End If

            'If whereCond <> "" Then
            '    whereCond = "WHERE " & whereCond
            'End If


            par.cmd.CommandText = "SELECT SEGNALAZIONI.ID,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,0,8),'yyyymmdd'),'dd/mm/yyyy') AS data_ora_richiesta, " _
                                & "TO_CHAR(TO_DATE(SUBSTR(data_in_carico,0,8),'yyyymmdd'),'dd/mm/yyyy') AS data_in_carico, " _
                                & "TO_CHAR(TO_DATE(SUBSTR(data_chiusura,0,8),'yyyymmdd'),'dd/mm/yyyy') AS data_chiusura, " _
                                & "TIPOLOGIE_GUASTI.descrizione  AS tipo, " _
                                & "DESCRIZIONE_RIC " _
                                & "FROM siscom_mi.SEGNALAZIONI,SISCOM_MI.TIPOLOGIE_GUASTI WHERE TIPOLOGIE_GUASTI.ID = SEGNALAZIONI.id_tipologie " & whereCond _
                                & " ORDER BY data_ora_richiesta DESC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.DataGridRptSituaz.DataSource = dt
            Me.DataGridRptSituaz.DataBind()

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
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRptSituaz, "RptSegn", False)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

    End Sub
End Class
