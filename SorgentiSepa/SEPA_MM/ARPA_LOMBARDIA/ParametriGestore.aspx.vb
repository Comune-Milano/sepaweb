Imports Telerik.Web.UI

Partial Class ARPA_LOMBARDIA_ParametriGestore
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = RadGridGestore.ClientID.ToString.Replace("ctl00", "MasterHomePage")
        End If
    End Sub
    Protected Sub RadGridGestore_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridGestore.NeedDataSource
        Try
            Dim Query As String = "SELECT COD, DESCRIZIONE_PATRIMONIO, CODICE_FISCALE, RAG_SOCIALE, DENOMINAZIONE " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "TAB_GESTORI_ARCHIVIO " _
                                & "WHERE FL_ARPA = 1 "
            RadGridGestore.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_GestioneCodifiche - RadGridGestore_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)
            Dim Errori As String = ""
            For Each item As GridDataItem In RadGridGestore.Items
                If String.IsNullOrEmpty(Trim(CType(item.FindControl("txtCodiceFiscale"), RadTextBox).Text)) Then
                    If String.IsNullOrEmpty(Trim(Errori)) Then
                        Errori = "- Definire il Codice Fiscale Ente Proprietario;"
                    Else
                        Errori &= "<br>" & "- Definire il Codice Fiscale Ente Proprietario;"
                    End If
                    Exit For
                Else
                    If Len(Trim(CType(item.FindControl("txtCodiceFiscale"), RadTextBox).Text)) < 11 Then
                        If String.IsNullOrEmpty(Trim(Errori)) Then
                            Errori = "- Definire il Codice Fiscale Ente Proprietario corretto;"
                        Else
                            Errori &= "<br>" & "- Definire il Codice Fiscale Ente Proprietario corretto;"
                        End If
                        Exit For
                    End If
                End If
            Next
            If String.IsNullOrEmpty(Trim(Errori)) Then
                For Each item As GridDataItem In RadGridGestore.Items
                    par.cmd.CommandText = "UPDATE " & CType(Me.Master, Object).StringaSiscom & "TAB_GESTORI_ARCHIVIO SET CODICE_FISCALE = " & par.insDbValue(CType(item.FindControl("txtCodiceFiscale"), RadTextBox).Text, True) & ", " _
                                        & "RAG_SOCIALE = " & par.insDbValue(CType(item.FindControl("txtRagioneSociale"), RadTextBox).Text, True) & ", " _
                                        & "DENOMINAZIONE = " & par.insDbValue(CType(item.FindControl("txtDenominazione"), RadTextBox).Text, True) & " " _
                                        & "WHERE COD = " & par.insDbValue(item("COD").Text.ToString, True)
                    par.cmd.ExecuteNonQuery()
                Next
                RadNotificationNote.Text = par.Messaggio_Operazione_Eff
                RadNotificationNote.Show()
            Else
                CType(Me.Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert(Errori, 450, 150, "Attenzione", Nothing, Nothing)
            End If
            connData.chiudi(True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_GestioneCodifiche - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
