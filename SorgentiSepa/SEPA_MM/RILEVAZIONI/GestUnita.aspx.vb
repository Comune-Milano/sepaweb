Imports System.IO

Partial Class RILEVAZIONI_Default
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_RILIEVO_GEST") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Session.Item("OPERATORE") = "" Or Session.Item("FL_RILIEVO_GEST") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
                Exit Sub
            End If
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                par.caricaComboBox("SELECT * FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1 ORDER BY DESCRIZIONE ASC", cmbRilievo, "ID", "DESCRIZIONE", False)
                BindGridUnita()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUnita - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub BindGridUnita()
        Try
            connData.apri()
            Dim Str As String = ""
            Str = "SELECT replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LET=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE,EDIFICI.DENOMINAZIONE,INDIRIZZI.CAP,UNITA_IMMOBILIARI.ID,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,INDIRIZZI.LOCALITA,IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB FROM SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.IDENTIFICATIVI_CATASTALI,SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.RILIEVO_UI WHERE EDIFICI.ID(+)=UNITA_IMMOBILIARI.ID_EDIFICIO AND TIPO_LIVELLO_PIANO.COD (+)=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND SCALE_EDIFICI.ID (+)=UNITA_IMMOBILIARI.ID_SCALA AND IDENTIFICATIVI_CATASTALI.ID (+)=UNITA_IMMOBILIARI.ID_CATASTALE AND INDIRIZZI.ID (+)=UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.ID_UNITA AND RILIEVO_UI.ID_RILIEVO=" & cmbRilievo.SelectedItem.Value & " "
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridUnita.DataSource = dt
            DataGridUnita.DataBind()
            connData.chiudi()
            'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key", "<script>MakeStaticHeader('" + DataGridUnita.ClientID + "', 255, 670 , 25 ,false); </script>", False)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUnita - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGridUnita_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridUnita.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_LBLID').value='" & e.Item.Cells(0).Text & "';")
            'e.Item.Attributes.Add("onDblclick", "document.getElementById('MasterPage_ContentPlaceHolder2_ButtonClickEsercizio').click();")
        End If
    End Sub



    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
        CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click

    End Sub

    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        Try
            connData.apri()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.RILIEVO_UI WHERE ID_RILIEVO=" & cmbRilievo.SelectedItem.Value & " AND ID_UNITA = " & Me.LBLID.Value
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            Me.TextBox1.Value = "0"
            Me.LBLID.Value = ""
            BindGridUnita()
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)

        Catch EX1 As Data.OracleClient.OracleException
            connData.chiudi()
            Me.lblErrore.Visible = True
            If EX1.Code = 2292 Then
                lblErrore.Text = "Utente in uso. Non è possibile eliminare!"
            Else
                lblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGridUnita_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridUnita.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridUnita.CurrentPageIndex = e.NewPageIndex
            BindGridUnita()
        End If
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub btnSalvaFile_Click(sender As Object, e As System.EventArgs) Handles btnSalvaFile.Click
        Try
            Dim FileName As String = UCase(UploadOnServer())
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And FileName.Contains(".TXT") Then
                    LeggiFileTXT(FileName)
                Else
                    par.modalDialogMessage("Attenzione", "Tipo file non valido. Selezionare un file txt!", Me.Page)
                End If
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: GestUnita - btnSalvaFile_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub LeggiFileTXT(ByVal nomeFile As String)
        Dim sContenutoRiga As String = ""
        Dim dt As New Data.DataTable
        dt.Columns.Add("ID_UI")
        Dim ContaRighe As Integer = 0
        Dim riga As Data.DataRow
        connData.apri(True)

        Dim sr1 As StreamReader = New StreamReader(nomeFile, System.Text.Encoding.GetEncoding("iso-8859-1"))
        Do While sr1.Peek() >= 0
            sContenutoRiga = sr1.ReadLine()
            If sContenutoRiga <> "" Then
                ContaRighe += 1
                If ContaRighe >= 1000 Then
                    Exit Do
                End If
                par.cmd.CommandText = "SELECT unita_immobiliari.id, (select id from siscom_mi.rilievo_ui where unita_immobiliari.id=siscom_mi.rilievo_ui.id_unita) as idRilievo from siscom_mi.unita_immobiliari WHERE  cod_unita_immobiliare in ('" & sContenutoRiga & "')"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If par.IfNull(myReader("idRilievo"), -1) = -1 Then
                        riga = dt.NewRow
                        riga.Item("ID_UI") = myReader("ID")
                        dt.Rows.Add(riga)
                    End If
                End If
                myReader.Close()
            End If
        Loop
        sr1.Close()

        If dt.Rows.Count > 0 Then
            For Each r1 As Data.DataRow In dt.Rows
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.RILIEVO_UI (ID_RILIEVO, ID_UNITA) VALUES " _
                    & "(" & cmbRilievo.SelectedValue & "," & r1.Item("ID_UI") & ")"
                par.cmd.ExecuteNonQuery()
            Next
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
        Else
            par.modalDialogMessage("Attenzione", "Non ci sono nuove unità da aggiungere!", Me.Page)
        End If

        If ContaRighe = 0 Then
            par.modalDialogMessage("Attenzione", "Nessun\'unità presente nel file!", Me.Page)
        End If

        connData.chiudi(True)
    End Sub

    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE EXCEL ##########
            If FileUpload1.HasFile = True Then
                UploadOnServer = Server.MapPath("..\FileTemp\") & FileUpload1.FileName
                FileUpload1.SaveAs(UploadOnServer)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:UploadOnServer " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return UploadOnServer
    End Function
End Class
