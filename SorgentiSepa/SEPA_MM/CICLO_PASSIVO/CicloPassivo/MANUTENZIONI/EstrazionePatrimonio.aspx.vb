
Partial Class CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_EstrazionePatrimonio
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" And Session.Item("FL_ESTRAZIONE_STR") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
        End If
    End Sub
    Protected Sub RadButtonEstrazioneComplessi_Click(sender As Object, e As System.EventArgs) Handles RadButtonEstrazioneComplessi.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COD_COMPLESSO,TRIM(DENOMINAZIONE) AS DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY 2"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ComplessiImmobiliari", "Complessi", dt, , )
            If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: RadButtonEstrazioneComplessi_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonEstrazioneEdifici_Click(sender As Object, e As System.EventArgs) Handles RadButtonEstrazioneEdifici.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COD_EDIFICIO,TRIM(DENOMINAZIONE) AS DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY 2"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Edifici", "Edifici", dt, , )
            If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: RadButtonEstrazioneEdifici_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonEstrazioneImpianti_Click(sender As Object, e As System.EventArgs) Handles RadButtonEstrazioneImpianti.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COD_IMPIANTO,TRIM(DESCRIZIONE) AS DENOMINAZIONE FROM SISCOM_MI.IMPIANTI ORDER BY 2"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Edifici", "Edifici", dt, , )
            If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: RadButtonEstrazioneImpianti_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonEstrazioniUI_Click(sender As Object, e As System.EventArgs) Handles RadButtonEstrazioniUI.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COD_UNITA_IMMOBILIARE,trim(EDIFICI.DENOMINAZIONE)||'-Scala '||(SELECT SCALE_eDIFICI.DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID=ID_SCALA)||'-Interno '||INTERNO as denominazione FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND EDIFICI.ID<>1 ORDER BY 2"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "UI", "UI", dt, , )
            If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: RadButtonEstrazioniUI_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonEstrazioniUC_Click(sender As Object, e As System.EventArgs) Handles RadButtonEstrazioniUC.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COD_UNITA_COMUNE, (CASE WHEN ID_COMPLESSO IS NOT NULL THEN 'COMPLESSO '||(SELECT COD_COMPLESSO||'-'||TRIM(DENOMINAZIONE) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=ID_COMPLESSO)       WHEN ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO '||(SELECT COD_EDIFICIO||'-'||TRIM(DENOMINAZIONE) FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO) ELSE NULL END) AS DENOMINAZIONE FROM SISCOM_MI.UNITA_COMUNI ORDER BY 2"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "UC", "UC", dt, , )
            If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: RadButtonEstrazioniUC_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonEstrazioneScale_Click(sender As Object, e As System.EventArgs) Handles RadButtonEstrazioneScale.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COD_EDIFICIO||LPAD(DESCRIZIONE,4,'0') AS COD_SCALA,DENOMINAZIONE||' Scala '||DESCRIZIONE AS DENOMINAZIONE FROM SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO ORDER BY 2"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Scale", "Scale", dt, , )
            If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: RadButtonEstrazioneScale_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonEstrazioneDGR_Click(sender As Object, e As System.EventArgs) Handles RadButtonEstrazioneDGR.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CODIFICA_STR ORDER BY CODICE"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "DGR", "DGR", dt, , )
            If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: RadButtonEstrazioneDGR_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
End Class
