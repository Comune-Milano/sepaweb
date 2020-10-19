
Partial Class ANAUT_RicercaInquilino
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            If Request.QueryString("T") <> "1" Then
                If Session.Item("MOD_AU_CONV_VIS_TUTTO") = "1" Then
                    par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM siscom_mi.tab_filiali where ID IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID_BANDO=(SELECT MAX(ID) FROM UTENZA_BANDI)) order by NOME asc", "NOME", "ID")
                    cmbFiliale.Items.Add("TUTTE LE SEDI")
                    cmbFiliale.Items.FindByText("TUTTE LE SEDI").Selected = True
                Else
                    par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM siscom_mi.tab_filiali where id in (select id_UFFICIO from operatori where id=" & Session.Item("ID_OPERATORE") & ") order by NOME asc", "NOME", "ID")
                    cmbFiliale.Enabled = False
                End If
                par.RiempiDList(Me, par.OracleConn, "cmbStatoConv", "SELECT ID,DESCRIZIONE FROM SISCOM_MI.CONVOCAZIONI_AU_STATI ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "ID")
                cmbStatoConv.Items.Add("TUTTI")
                cmbStatoConv.Items.FindByText("TUTTI").Selected = True

                par.RiempiDList(Me, par.OracleConn, "cmbMotivo", "SELECT id,descrizione FROM siscom_mi.tab_Motivo_Annullo_App WHERE ID_AU=(SELECT MAX(ID) FROM UTENZA_BANDI) order by descrizione asc", "DESCRIZIONE", "ID")
                cmbMotivo.Items.Add("--")
                cmbMotivo.Items.FindByText("--").Selected = True
            Else
                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM siscom_mi.tab_filiali where ID IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID_BANDO=(SELECT MAX(ID) FROM UTENZA_BANDI)) order by NOME asc", "NOME", "ID")
                cmbFiliale.Items.Add("TUTTE LE SEDI")
                cmbFiliale.Items.FindByText("TUTTE LE SEDI").Selected = True
                'cmbOperatore.Enabled = False
                'cmbOperatore.Items.Add("TUTTI GLI SPORTELLI")
                'cmbOperatore.Items.FindByText("TUTTI GLI SPORTELLI").Selected = True
                par.RiempiDList(Me, par.OracleConn, "cmbStatoConv", "SELECT ID,DESCRIZIONE FROM SISCOM_MI.CONVOCAZIONI_AU_STATI where id=1", "DESCRIZIONE", "ID")
                Label9.Visible = True
                par.RiempiDList(Me, par.OracleConn, "cmbMotivo", "SELECT id,descrizione FROM siscom_mi.tab_Motivo_Annullo_App WHERE ID_AU=(SELECT MAX(ID) FROM UTENZA_BANDI) order by descrizione asc", "DESCRIZIONE", "ID")
                cmbMotivo.Items.Add("--")
                'cmbMotivo.Items.Add("INVIO TRAMITE SINDACATI")
                cmbMotivo.Items.FindByText("--").Selected = True
                'cmbMotivo.Enabled = False
                cmbMotivo.Visible = True

                'lblPresa.Visible = True
                'CheckBox1.Visible = True


            End If



        End If
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If cmbFiliale.Items.Count > 0 Then
            If Request.QueryString("T") <> "1" Then
                Response.Write("<script>location.replace('RisultatiRicInquilino.aspx?OP=" & par.Cripta(txtOperatore.Text) & "&431=" & par.CriptaMolto(Chk431.Checked) & "&392=" & par.CriptaMolto(chk392.Checked) & "&AU=" & par.CriptaMolto(CheckBox1.Checked) & "&M=" & par.CriptaMolto(cmbMotivo.SelectedItem.Value) & "&X=0&DA=" & par.AggiustaData(txtDal.Text) & "&A=" & par.AggiustaData(txtAl.Text) & "&idF=" & par.CriptaMolto(cmbFiliale.SelectedItem.Value) & "&idO=" & par.CriptaMolto("TUTTI GLI SPORTELLI") & "&C=" & par.CriptaMolto(UCase(Trim(txtCognome.Text))) & "&N=" & par.CriptaMolto(UCase(Trim(txtNome.Text))) & "&CO=" & par.CriptaMolto(UCase(Trim(txtContratto.Text))) & "&ST=" & cmbStatoConv.SelectedItem.Value & "&CON=" & par.CriptaMolto(UCase(Trim(txtConvocazione.Text))) & "');</script>")
            Else
                Response.Write("<script>location.replace('RisultatiRicInquilino.aspx?OP=" & par.Cripta(txtOperatore.Text) & "&431=" & par.CriptaMolto(Chk431.Checked) & "&392=" & par.CriptaMolto(chk392.Checked) & "&AU=" & par.CriptaMolto(CheckBox1.Checked) & "&M=" & par.CriptaMolto(cmbMotivo.SelectedItem.Value) & "&X=1&DA=" & par.AggiustaData(txtDal.Text) & "&A=" & par.AggiustaData(txtAl.Text) & "&idF=" & par.CriptaMolto(cmbFiliale.SelectedItem.Value) & "&idO=" & par.CriptaMolto("TUTTI GLI SPORTELLI") & "&C=" & par.CriptaMolto(UCase(Trim(txtCognome.Text))) & "&N=" & par.CriptaMolto(UCase(Trim(txtNome.Text))) & "&CO=" & par.CriptaMolto(UCase(Trim(txtContratto.Text))) & "&ST=" & cmbStatoConv.SelectedItem.Value & "&CON=" & par.CriptaMolto(UCase(Trim(txtConvocazione.Text))) & "');</script>")
            End If
        Else
            Response.Write("<script>alert('Selezionare una filiale');</script>")
        End If
    End Sub

    Protected Sub cmbFiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiliale.SelectedIndexChanged
        Try
            'cmbOperatore.Items.Clear()
            If cmbFiliale.SelectedItem.Text <> "TUTTE LE SEDI" Then
                Try
                    'par.RiempiDList(Me, par.OracleConn, "cmbOperatore", "SELECT ID,DESCRIZIONE FROM UTENZA_SPORTELLI WHERE ID IN (SELECT DISTINCT ID_SPORTELLO FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_FILIALE=" & cmbFiliale.SelectedItem.Value & " AND ID_SPORTELLO IS NOT NULL)  ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "ID")
                    'cmbOperatore.Enabled = True
                    'cmbOperatore.Items.Add("TUTTI GLI SPORTELLI")
                    'cmbOperatore.Items.FindByText("TUTTI GLI SPORTELLI").Selected = True

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch ex As Exception
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Add("ERRORE", "Provenienza:RicercaConvocazioni - " & ex.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End Try
            Else
                'cmbOperatore.Enabled = False
                'cmbOperatore.Items.Add("TUTTI GLI SPORTELLI")
                'cmbOperatore.Items.FindByText("TUTTI GLI SPORTELLI").Selected = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub cmbStatoConv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatoConv.SelectedIndexChanged
        Try
            If cmbStatoConv.SelectedItem.Value = "1" Then

                cmbMotivo.Visible = True
                Label9.Visible = True
                CheckBox1.Enabled = True
                If Request.QueryString("T") = "1" Then
                    CheckBox1.Visible = True
                    lblPresa.Visible = True
                    cmbMotivo.SelectedIndex = -1
                    cmbMotivo.Items.FindByText("INVIO TRAMITE SINDACATI").Selected = True
                    cmbMotivo.Enabled = False
                End If
            Else
                cmbMotivo.SelectedIndex = -1
                cmbMotivo.Items.FindByText("--").Selected = True
                cmbMotivo.Visible = False
                Label9.Visible = False
                CheckBox1.Visible = False
                lblPresa.Visible = False
                CheckBox1.Checked = False

                If Request.QueryString("T") = "1" Then
                    CheckBox1.Visible = True
                    CheckBox1.Checked = True
                    CheckBox1.Enabled = False
                    lblPresa.Visible = True
                End If
            End If
        Catch ex As Exception

        End Try
        
    End Sub

    Protected Sub cmbMotivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMotivo.SelectedIndexChanged
        If cmbMotivo.SelectedItem.Text = "INVIO TRAMITE SINDACATI" Then
            lblPresa.Visible = True
            CheckBox1.Visible = True
        Else
            lblPresa.Visible = False
            CheckBox1.Visible = False
            CheckBox1.Checked = False
        End If
    End Sub
End Class
