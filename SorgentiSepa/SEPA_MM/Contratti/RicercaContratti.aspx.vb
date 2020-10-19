
Partial Class Contratti_RicercaContratti
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

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

            par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_ORIGINE_CONTRATTO where COD_TIPOLOGIA_CONTR='" & cmbTipo.SelectedValue & "' ORDER BY ID ASC", cmbOrigineContratto, "ID", "DESCRIZIONE", True)
            'cmbOrigineContratto.Items.Add("TUTTI")
            'cmbOrigineContratto.Items.FindByText("TUTTI").Selected = True


            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim I As Integer = 0
            chIndirizzi.Items.Clear()
            par.cmd.CommandText = "SELECT DISTINCT DESCRIZIONE FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI WHERE INDIRIZZI.DESCRIZIONE='* BOLLETTAZIONE *' OR UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID ORDER BY DESCRIZIONE ASC"
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

        txtRinnovoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtRinnovoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim sTipo As String
        Dim sTipoImm As String
        Dim I As Integer = 0
        Dim INDIRIZZI As String = ""
        Dim intest As Integer = 1

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

        If ChIntest.Checked = True Then
            intest = 1
        Else
            intest = 0
        End If

        Response.Write("<script>location.replace('RisultatoContratti.aspx?EXG=" & Valore01(chkdoc.Checked) & "&GIMI=" & txtCodGIMI.Text & "&PIVA=" & txtpiva.Text & "&FDAL=" & par.AggiustaData(txtScadeDal.Text) & "&FAL=" & par.AggiustaData(txtScadeAl.Text) & "&RDAL=" & par.AggiustaData(txtRinnovoDal.Text) & "&RAL=" & par.AggiustaData(txtRinnovoAl.Text) & "&SDAL=" & par.AggiustaData(txtStipulaDal.Text) & "&SAL=" & par.AggiustaData(txtStipulaAl.Text) & "&DDAL=" & par.AggiustaData(txtDecorrenzaDal.Text) & "&DAL=" & par.AggiustaData(txtDecorrenzaAl.Text) & "&ST=" & cmbStato.SelectedValue & "&UN=" & txtUnita.Text & "&TI=" & sTipoImm & "&TC=" & sTipo & "&CO=" & par.VaroleDaPassare(txtCod.Text) & "&CG=" & par.VaroleDaPassare(txtCognome.Text) & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCF.Text) & "&RS=" & par.VaroleDaPassare(txtRagione.Text) & "&RECDA=" & par.AggiustaData(txtDisdettaDal.Text) & "&RECA=" & par.AggiustaData(txtDisdettaAl.Text) & "&SLODA=" & par.AggiustaData(txtSloggioDal.Text) & "&SLOA=" & par.AggiustaData(txtSloggioAl.Text) & "&SLOV=" & Valore01(ChSloggio.Checked) & "&INSDA=" & par.AggiustaData(txtInseritoDal.Text) & "&INSA=" & par.AggiustaData(txtInseritoAl.Text) & "&VIRT=" & Valore01(ChVirtuali.Checked) & "&TIPO=" & cmbTipo.SelectedValue & "&PROV=" & cmbProvenASS.SelectedValue & "&ORIG=" & cmbOrigineContratto.SelectedValue & "&DUR=" & txtDurata.Text & "&RINN=" & txtRinnovo.Text & "&INTEST=" & intest & "&DECAD=" & cmbDecretoDec.SelectedValue & "&CTRANAG=" & cmbControlloAnag.SelectedValue & "');</script>")
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

   
    Protected Sub cmbTipo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged

        If cmbTipo.SelectedValue = "ERP" Then
            cmbProvenASS.Items.Clear()
            cmbProvenASS.Items.Add(New ListItem("TUTTI", "0"))
            cmbProvenASS.Items.Add(New ListItem("Canone Convenzionato", "12"))
            cmbProvenASS.Items.Add(New ListItem("Forze dell'Ordine", "10"))
            cmbProvenASS.Items.Add(New ListItem("ERP Moderato", "2"))
            cmbProvenASS.Items.Add(New ListItem("ERP Sociale", "1"))
            cmbProvenASS.Visible = "True"
            lblSpecifico.Visible = "True"

            par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_ORIGINE_CONTRATTO where COD_TIPOLOGIA_CONTR='" & cmbTipo.SelectedValue & "' ORDER BY ID ASC", cmbOrigineContratto, "ID", "DESCRIZIONE", True)
            If cmbOrigineContratto.Items.Count > 1 Then
                cmbOrigineContratto.Visible = True
                lblOrigineRU.Visible = "True"
            Else
                cmbOrigineContratto.Visible = False
                lblOrigineRU.Visible = "False"
            End If
        ElseIf cmbTipo.SelectedValue = "L43198" Then
            cmbProvenASS.Items.Clear()
            cmbProvenASS.Items.Add(New ListItem("TUTTI", "-1"))
            cmbProvenASS.Items.Add(New ListItem("Standard", "0"))
            cmbProvenASS.Items.Add(New ListItem("Cooperative", "C"))
            cmbProvenASS.Items.Add(New ListItem("431 P.O.R.", "P"))
            cmbProvenASS.Items.Add(New ListItem("431/98 Art.15 R.R.1/2004", "D"))
            cmbProvenASS.Items.Add(New ListItem("431/98 Art.15 C.2 R.R.1/2004", "V"))
            cmbProvenASS.Items.Add(New ListItem("431/98 Speciali", "S"))
            cmbProvenASS.Visible = "True"
            lblSpecifico.Visible = "True"
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_ORIGINE_CONTRATTO where COD_TIPOLOGIA_CONTR='" & cmbTipo.SelectedValue & "' ORDER BY ID ASC", cmbOrigineContratto, "ID", "DESCRIZIONE", True)
            If cmbOrigineContratto.Items.Count > 1 Then
                cmbOrigineContratto.Visible = True
                lblOrigineRU.Visible = "True"
            Else
                cmbOrigineContratto.Visible = False
                lblOrigineRU.Visible = "False"
        End If
        End If

        If cmbTipo.SelectedValue <> "ERP" And cmbTipo.SelectedValue <> "L43198" Then
            cmbProvenASS.Visible = "False"
            lblSpecifico.Visible = "False"
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_ORIGINE_CONTRATTO where COD_TIPOLOGIA_CONTR='" & cmbTipo.SelectedValue & "' ORDER BY ID ASC", cmbOrigineContratto, "ID", "DESCRIZIONE", True)
            If cmbOrigineContratto.Items.Count > 1 Then
                cmbOrigineContratto.Visible = True
                lblOrigineRU.Visible = "True"
            Else
                cmbOrigineContratto.Visible = False
                lblOrigineRU.Visible = "False"
            End If
        End If

    End Sub
End Class
