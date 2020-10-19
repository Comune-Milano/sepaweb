Namespace CM

Partial Class RicercaDomande
    Inherits PageSetIdMode
    Dim par As New [Global]()
#Region " Codice generato da Progettazione Web Form "

    'Chiamata richiesta da Progettazione Web Form.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
        'Non modificarla nell'editor del codice.
        InitializeComponent()
    End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
            If Not IsPostBack Then
                txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                Label3.Text = "La ricerca sarà effettuata solo sulle domande acquisite dall'ente " & Session.Item("CAAF")
                'par.RiempiDList(Me, par.OracleConn, "cmbStato", "SELECT * FROM T_STATI_DICHIARAZIONE ORDER BY COD ASC", "DESCRIZIONE", "COD")
                If cmbStato.Items.Count = 0 Then
                    cmbStato.Items.Add("TUTTI")
                    cmbStato.Items.Add("FORMALIZZAZIONE")
                    cmbStato.Items.Add("ISTRUTTORIA")
                    cmbStato.Items.Add("IDONEE")
                    cmbStato.Items.Add("RESPINTE")
                    cmbStato.Items.FindByText("TUTTI").Selected = True
                End If
                If cmbBando.Items.Count = 0 Then

                    Dim lsiFrutto As New ListItem("TUTTI", "-2")
                    cmbBando.Items.Add(lsiFrutto)

                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.cmd.CommandText = "SELECT * FROM BANDI WHERE ID<>-1 ORDER BY ID ASC"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader.Read
                        lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("ID"))
                        cmbBando.Items.Add(lsiFrutto)
                    End While
                    myReader.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            End If
        End Sub

        Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        End Sub

        Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
            Response.Redirect("RisultatoRicDom.aspx?CG=" & par.VaroleDaPassare(txtCognome.Text.ToUpper) & "&NM=" & par.VaroleDaPassare(txtNome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(txtCF.Text.ToUpper) & "&PG=" & par.VaroleDaPassare(txtPG.Text.ToUpper) & "&ST=" & cmbStato.SelectedItem.Text & "&BA=" & cmbBando.SelectedItem.Value)
        End Sub


    End Class

End Namespace
