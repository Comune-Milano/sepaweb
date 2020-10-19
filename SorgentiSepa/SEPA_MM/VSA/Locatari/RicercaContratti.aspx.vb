
Partial Class Contratti_RicercaContratti
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            ModRich.Value = Request.QueryString("ModR")

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCod.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            par.RiempiDList(Me, par.OracleConn, "cmbTipo", "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            cmbTipo.Items.Add("TUTTI")
            cmbTipo.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "cmbTipoImm", "SELECT * FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            cmbTipoImm.Items.Add("TUTTI")
            cmbTipoImm.Items.FindByText("TUTTI").Selected = True


            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim I As Integer = 0
            chIndirizzi.Items.Clear()
            par.cmd.CommandText = "select DISTINCT DESCRIZIONE FROM SISCOM_MI.INDIRIZZI ORDER BY DESCRIZIONE ASC"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReaderA.Read
                If par.IfEmpty(par.IfNull(myReaderA("DESCRIZIONE"), ""), "") <> "" Then
                    chIndirizzi.Items.Add(par.IfNull(myReaderA("DESCRIZIONE"), "")) ' & ", " & par.IfNull(myReaderA("CIVICO"), ""))
                    'chIndirizzi.Items(I).Value = par.IfNull(myReaderA("ID"), -1)
                    I = I + 1
                End If
            Loop
            myReaderA.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDecorrenzaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDecorrenzaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtScadeDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtScadeAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtDisdettaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDisdettaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtSloggioDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtSloggioAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtInseritoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtInseritoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim sTipo As String
        Dim sTipoImm As String
        Dim I As Integer = 0
        Dim INDIRIZZI As String = ""
        For I = 0 To chIndirizzi.Items.Count - 1
            If chIndirizzi.Items(I).Selected = True Then
                If INDIRIZZI <> "" Then INDIRIZZI = INDIRIZZI & " OR "
                INDIRIZZI = INDIRIZZI & " INDIRIZZI.DESCRIZIONE='" & par.PulisciStrSql(chIndirizzi.Items(I).Text) & "' "
            End If
        Next

        If INDIRIZZI <> "" Then
            Session.Add("INDIRIZZI", INDIRIZZI)
        End If

        If cmbTipo.Items.FindByText("TUTTI").Selected = True Then
            sTipo = ""
        Else
            sTipo = cmbTipo.SelectedItem.Value
        End If
        If cmbTipoImm.Items.FindByText("TUTTI").Selected = True Then
            sTipoImm = ""
        Else
            sTipoImm = cmbTipoImm.SelectedItem.Value
        End If



        Response.Write("<script>location.replace('RisultatoContratti.aspx?GIMI=" & txtCodGIMI.Text & "&PIVA=" & txtpiva.Text & "&FDAL=" & par.AggiustaData(txtScadeDal.Text) & "&FAL=" & par.AggiustaData(txtScadeAl.Text) & "&SDAL=" & par.AggiustaData(txtStipulaDal.Text) & "&SAL=" & par.AggiustaData(txtStipulaAl.Text) & "&DDAL=" & par.AggiustaData(txtDecorrenzaDal.Text) & "&DAL=" & par.AggiustaData(txtDecorrenzaAl.Text) & "&ST=" & cmbStato.SelectedValue & "&UN=" & txtUnita.Text & "&TI=" & sTipoImm & "&TC=" & sTipo & "&CO=" & par.VaroleDaPassare(txtCod.Text) & "&CG=" & par.VaroleDaPassare(txtCognome.Text) & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCF.Text) & "&RS=" & par.VaroleDaPassare(txtRagione.Text) & "&RECDA=" & par.AggiustaData(txtDisdettaDal.Text) & "&RECA=" & par.AggiustaData(txtDisdettaAl.Text) & "&SLODA=" & par.AggiustaData(txtSloggioDal.Text) & "&SLOA=" & par.AggiustaData(txtSloggioAl.Text) & "&SLOV=" & Valore01(ChSloggio.Checked) & "&INSDA=" & par.AggiustaData(txtInseritoDal.Text) & "&INSA=" & par.AggiustaData(txtInseritoAl.Text) & "&ModR=" & ModRich.Value & "');</script>")
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Protected Sub btnIndietro_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        Response.Redirect("nuova_domanda.aspx")
    End Sub
End Class
