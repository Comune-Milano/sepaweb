
Imports System.IO
Imports Telerik.Web.UI

Partial Class Contratti_Spalmatore_RptBollContabili
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Private Sub Contratti_Spalmatore_RptBollContabili_Load(sender As Object, e As EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        'If Not IsPostBack Then
        'CreaRptContabileCSV()
        tipoBoll = par.IfEmpty(cmbTipoBoll.SelectedValue, "-1")
        'End If
    End Sub
    Public Property tipoBoll() As Integer
        Get
            If Not (ViewState("par_tipoBoll") Is Nothing) Then
                Return CInt(ViewState("par_tipoBoll"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_tipoBoll") = value
        End Set

    End Property
    Private Sub CreaRptContabileCSV()
        Try

            Dim condizioneTipoBoll As String = ""
            If tipoBoll = 0 Then
                condizioneTipoBoll = " and b.id < 0 "
            End If
            If tipoBoll = 1 Then
                condizioneTipoBoll = " and b.id > 0 "
            End If

            'Dim ik1 As Long = 0
            'CType(Me.Master.FindControl("RadProgressArea1"), RadProgressArea).Localization.Uploaded = "Avanzamento Totale"
            'CType(Me.Master.FindControl("RadProgressArea1"), RadProgressArea).Localization.UploadedFiles = "Avanzamento"
            'CType(Me.Master.FindControl("RadProgressArea1"), RadProgressArea).Localization.CurrentFileName = "Elaborazione in corso: "


            'Dim Total As Integer = 7
            'Dim progress1 As Telerik.Web.UI.RadProgressContext = Telerik.Web.UI.RadProgressContext.Current
            'progress1.Speed = "N/A"


            par.cmd.CommandText = "select b.Id as ID_BOLLETTA, " _
                    & "       Num_bolletta as NUM_BOLLETTA," _
                    & "       siscom_mi.getdata(Data_emissione) as DATA_EMISSIONE," _
                    & "       siscom_mi.getdata(b.riferimento_da) as RIFERIMENTO_DA," _
                    & "       siscom_mi.getdata(b.riferimento_a) as RIFERIMENTO_A," _
                    & "       importo_totale as IMPORTO_TOTALE," _
                    & "       b.EMESSO_CONTABILE as IMPORTO_TOTALE_CONTABILE," _
                    & "       siscom_mi.getdata(b.data_scadenza) as DATA_SCADENZA," _
                    & "       nvl(u.cod_contratto,'Non Disponibile') as COD_CONTRATTO," _
                    & "       data_pagamento as DATA_PAGAMENTO," _
                    & "       siscom_mi.getdata(b.data_valuta) as DATA_CONTABILE," _
                    & "       b.importo_pagato as IMPORTO_PAGATO," _
                    & "       b.saldo_contabile as RESIDUO_CONTABILE," _
                    & "       b.note_pagamento as NOTE_INCASSO," _
                    & "       b.RIF_FILE_RENDICONTO as RIF_FILE_RENDICONTO," _
                    & "       p.descrizione as TIPO_INCASSO," _
                    & "       decode(nvl(b.fl_annullata, 0), 0, 'NO', 'SI') as ANNULLATA," _
                    & "       siscom_mi.getdata(b.data_annullo) as DATA_ANNULLO," _
                    & "       t.descrizione as TIPOLOGIA," _
                    & "       t.acronimo as ACRONIMO," _
                    & "       decode(b.n_rata, 99, 'MANUALE', 999, 'AUTOMATICA', 'ORDINARIA') as GENERAZIONE," _
                    & "       decode(nvl(b.id_morosita, 0), 0, 'NO', 'SI') as MOROSITA," _
                    & "       decode(nvl(b.id_rateizzazione, 0), 0, 'NO', 'SI') as RATEIZZATO," _
                    & "       decode(nvl(b.id_rateizzazione, 0), 0, decode(nvl(b.id_morosita, 0), 0, 'NO', 'SI'), 'SI') as RICLASSIFICATA," _
                    & "       b.importo_riclassificato as IMPORTO_RICLASSIFICATO," _
                    & "       u.ex_gestore as EX_GESTORE," _
                    & "       ID_CONTRATTO as ID_CONTRATTO," _
                    & "       b.anno as ANNO_BOLLETTA," _
                    & "       b.rif_bollettino as NUMERO_MAV," _
                    & "       ID_BOLLETTA_STORNO as ID_BOLLETTA_STORNO," _
                    & "       IMPORTO_RUOLO as IMPORTO_RUOLO," _
                    & "       b.imp_ruolo_pagato as IMP_RUOLO_PAGATO," _
                    & "       ID_BOLLETTA_RIC as ID_BOLLETTA_RIC," _
                    & "       b.quota_sind_b as QUOTA_PARTITA_GIRO," _
                    & "       b.quota_sind_pagata_b as QUOTA_PARTITA_GIRO_PAG," _
                    & "       trim(REPLACE(b.note,CHR(13) || CHR(10), ' ')) as NOTE" _
                    & "  from siscom_mi.v_saldo_bollette            b," _
                    & "       siscom_mi.rapporti_utenza             u," _
                    & "       SISCOM_MI.TIPO_BOLLETTE               t," _
                    & "       siscom_mi.bol_bollette_tipo_pagamento p" _
                    & " where b.id_contratto = u.id" _
                    & "   and b.id_tipo_pagamento = p.id" _
                    & "   and b.id_tipo = t.id  " _
                    & condizioneTipoBoll

            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dtBollette As New Data.DataTable()
            'da.Fill(dtBollette)
            'da.Dispose()
            'connData.chiudi()
            EstraiDati(par.cmd.CommandText, 13)
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Estrazione avviata correttamente!", 300, 150, "Info", "apriMaschera", Nothing)


            'Dim nomeFileCSV As String = par.DataTableALCSV(dtBollette, "Export_boll_contabili", ";", True, True)


            'progress1.PrimaryTotal = 1
            'progress1.PrimaryValue = Math.Round(CDec((100 * 1) / Total), 0)
            'progress1.PrimaryPercent = Math.Round(CDec((100 * 1) / Total), 0)
            'progress1.TimeEstimated = (Total - 1) * 1000


            'If File.Exists(Server.MapPath("~\FileTemp\") & nomeFileCSV) Then
            '    Response.Write("<script>window.open('../../FileTemp/" & nomeFileCSV & "','Expt', '');</script>")
            'Else
            '    Response.Write("<script>alert('Si è verificato un errore durante la creazione del file CSV. Riprovare!')</script>")
            'End If

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub EstraiDati(ByVal strQuery As String, ByVal idTipoReport As Integer)
        Try
            Dim sComando As String = strQuery
            connData.apri()


            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_REPORT.NEXTVAL FROM DUAL"
            Dim idReport As Integer = par.cmd.ExecuteScalar

            If Len(strQuery) < 4000 Then

                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE, Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'" & strQuery.ToString.Replace("'", "''") & "', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,NULL)"
            Else
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE,Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,:TEXT_DATA)"


                Dim paramData As New Oracle.DataAccess.Client.OracleParameter
                With paramData
                    .Direction = Data.ParameterDirection.Input
                    .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
                    .ParameterName = "TEXT_DATA"
                    .Value = strQuery
                End With

                par.cmd.Parameters.Add(paramData)
                paramData = Nothing


            End If
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
            Dim p As New System.Diagnostics.Process
            Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
            Dim dicParaConnection As New Generic.Dictionary(Of String, String)
            Dim sParametri As String = ""
            For i As Integer = 0 To elParameter.Length - 1
                Dim s As String() = elParameter(i).Split("=")
                If s.Length > 1 Then
                    dicParaConnection.Add(s(0), s(1))
                End If
            Next
            sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idReport
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Report.exe")
            p.StartInfo.Arguments = sParametri
            p.Start()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnProcedi_Click(sender As Object, e As EventArgs) Handles btnProcedi.Click
        CreaRptContabileCSV()
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Redirect("SpalmatoreHome.aspx", False)
    End Sub
End Class
