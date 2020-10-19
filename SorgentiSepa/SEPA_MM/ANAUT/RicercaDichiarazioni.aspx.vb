
Partial Class ANAUT_RicercaDichiarazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try


                txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                par.RiempiDList(Me, par.OracleConn, "cmbStato", "SELECT * FROM T_STATI_DICHIARAZIONE ORDER BY COD ASC", "DESCRIZIONE", "COD")
                cmbStato.Items.Add("TUTTI")
                cmbStato.Items.FindByText("TUTTI").Selected = True

                par.RiempiDList(Me, par.OracleConn, "cmbBando", "SELECT * FROM utenza_bandi ORDER BY id desc", "DESCRIZIONE", "ID")
                cmbBando.Items.Add("TUTTI")
                cmbBando.Items.FindByValue("TUTTI").Selected = True

                'If Mid(Session.Item("OPERATORE"), 1, 8) = "ANAGRAFE" Then
                'cmbEnte.Items.Add("ALTRI ENTI")
                'cmbEnte.Items.FindByText("ALTRI ENTI").Selected = True

                'Else
                'cmbEnte.Items.Add("QUESTO ENTE")
                'cmbEnte.Items.Add("ALTRI ENTI")
                'cmbEnte.Items.FindByText("QUESTO ENTE").Selected = True
                'End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim iStato As Integer
        Dim iBando As Integer
        Dim s45 As String = "0"
        Dim AU As String = ""
        Dim ART15 As String = ""
        Dim INVAL As String = ""

        If cmbStato.Items.FindByText("TUTTI").Selected = True Then
            iStato = -1
        Else
            iStato = cmbStato.SelectedItem.Value
        End If

        If cmbBando.Items.FindByText("TUTTI").Selected = True Then
            iBando = -1
        Else
            iBando = cmbBando.SelectedItem.Value
        End If

        If ch45.checked = True Then
            s45 = "1"
        Else
            s45 = "0"
        End If

        If chAutomatiche.Checked = True Then
            AU = "1"
        Else
            AU = "0"
        End If



        If CMBaRT15.SelectedItem.Value = "1" Then
            ART15 = "1"
        Else
            ART15 = "0"
        End If



        If ddl_carrozzina.SelectedItem.Value = "0" Then
            INVAL = "0"
        End If

        If ddl_carrozzina.SelectedItem.Value = "1" Then
            INVAL = "SI"
        End If

        If ddl_carrozzina.SelectedItem.Value = "2" Then
            INVAL = "NO"
        End If



        Response.Write("<script>location.replace('RisultatoRicercaD.aspx?OP=" & par.Cripta(txtOp.Text) & "&SDAL=" & par.AggiustaData(txtStipulaDal.Text) & "&SAL=" & par.AggiustaData(txtStipulaAl.Text) & "&GA=" & AU & "&S45=" & s45 & "&UN=" & par.VaroleDaPassare(txtUnita.Text.ToUpper) & "&ENTE=ALTRI ENTI&CO=" & par.VaroleDaPassare(txtCod.Text.ToUpper) & "&CG=" & par.VaroleDaPassare(txtCognome.Text.ToUpper) & "&NM=" & par.VaroleDaPassare(txtNome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(txtCF.Text.ToUpper) & "&PG=" & par.VaroleDaPassare(txtPG.Text.ToUpper) & "&ST=" & iStato & "&BD=" & iBando & "&TI=" & cmbTipo.SelectedItem.Value & "&ART15=" & ART15 & "&INVAL=" & INVAL & "');</script>")
    End Sub
End Class
