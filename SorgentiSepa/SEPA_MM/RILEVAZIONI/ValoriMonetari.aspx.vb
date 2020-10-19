
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
  
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                par.caricaComboBox("SELECT * FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1 ORDER BY DESCRIZIONE ASC", cmbRilievo, "ID", "DESCRIZIONE", False)
                CaricaGriglia()
                txtmodificato.Value = "0"
            End If
            AddJavascriptFunction()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - Gestione Valori Monetari - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Function CaricaGriglia()
        Dim i As Integer = 0

        Try
            connData.apri()

            par.cmd.CommandText = "SELECT RILIEVO_VAL_MONETARI.ID_RILIEVO,TAB_VALORI_MONETARI.DESCRIZIONE,RILIEVO_VAL_MONETARI.VALORE FROM SISCOM_MI.RILIEVO_VAL_MONETARI,SISCOM_MI.TAB_VALORI_MONETARI WHERE RILIEVO_VAL_MONETARI.ID_RILIEVO=" & cmbRilievo.SelectedItem.Value & " AND TAB_VALORI_MONETARI.ID (+)=RILIEVO_VAL_MONETARI.ID_TIPO"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                par.cmd.CommandText = "SELECT RILIEVO_VAL_MONETARI.ID_RILIEVO,RILIEVO_VAL_MONETARI.ID_TIPO,TAB_VALORI_MONETARI.DESCRIZIONE,RILIEVO_VAL_MONETARI.VALORE FROM SISCOM_MI.RILIEVO_VAL_MONETARI,SISCOM_MI.TAB_VALORI_MONETARI WHERE RILIEVO_VAL_MONETARI.ID_RILIEVO=" & cmbRilievo.SelectedItem.Value & " AND TAB_VALORI_MONETARI.ID (+)=RILIEVO_VAL_MONETARI.ID_TIPO ORDER BY ID_TIPO DESC"
                esiste.Value = "1"
            Else
                par.cmd.CommandText = "SELECT '" & cmbRilievo.SelectedItem.Value & "' AS ID_RILIEVO,TAB_VALORI_MONETARI.ID AS ID_TIPO,TAB_VALORI_MONETARI.DESCRIZIONE,'0' AS VALORE FROM SISCOM_MI.TAB_VALORI_MONETARI ORDER BY ID_TIPO DESC ASC"
                esiste.Value = "0"
            End If
            myReader.Close()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "RILIEVO_VAL_MONETARI")
            DataGridVoci.DataSource = ds
            DataGridVoci.DataBind()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function


    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
        CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
    End Sub

    Protected Sub cmbRilievo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbRilievo.SelectedIndexChanged
        CaricaGriglia()
    End Sub

    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridVoci.Items.Count - 1
                di = Me.DataGridVoci.Items(i)

                CType(di.Cells(3).FindControl("txtImportoCanone"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal(this,2);")
                CType(di.Cells(3).FindControl("txtImportoCanone"), TextBox).Attributes.Add("onChange", "javascript:document.getElementById('CPContenuto_txtmodificato').value='1';document.getElementById('optMenu').value='1';")
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        SalvaDati()
    End Sub

    Private Sub SalvaDati()
        Try
            Dim buono As Boolean = True

            connData.apri(True)

            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim Importo As Double = 0


            For i = 0 To Me.DataGridVoci.Items.Count - 1
                di = Me.DataGridVoci.Items(i)
                Importo = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoCanone"), TextBox).Text, "0,00")

                If String.IsNullOrEmpty(Importo) = True Then
                    Importo = 0
                End If

                If esiste.Value = "1" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_VAL_MONETARI SET VALORE=" & par.IfEmpty(par.VirgoleInPunti(Importo), "null") & " WHERE ID_RILIEVO=" & cmbRilievo.SelectedItem.Value & " AND ID_TIPO=" & Me.DataGridVoci.Items(i).Cells(1).Text
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RILIEVO_VAL_MONETARI (ID_RILIEVO,ID_TIPO,VALORE) VALUES (" & cmbRilievo.SelectedItem.Value & "," & Me.DataGridVoci.Items(i).Cells(1).Text & "," & par.IfEmpty(par.VirgoleInPunti(Importo), "null") & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            connData.chiudiTransazione(True)
            connData.chiudi()

            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)

            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
            txtmodificato.Value = "0"
            CaricaGriglia()

        Catch ex As Exception
            connData.chiudiTransazione(False)
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - Gestione Valori Monetari - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        If txtmodificato.Value = "0" Then
            Response.Redirect("Home.aspx", False)
        Else
            par.modalDialogConfirm("Attenzione", "Sono state apportate delle modifiche. Uscire senza salvare?", "SI", "validNavigation=true;document.getElementById('optMenu').value='0';location.href='Home.aspx';", "NO", "return false;", Me.Page)
        End If
    End Sub
End Class
