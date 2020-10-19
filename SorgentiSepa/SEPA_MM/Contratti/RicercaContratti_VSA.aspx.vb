
Partial Class Contratti_RicercaContratti_VSA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCodF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCodContr.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            cmbMotivo.Items.Add(New ListItem("TUTTI", "-1"))
            cmbMotivo.SelectedItem.Text = "TUTTI"

            cmbTipoDom.Items.Add("TUTTI")
            cmbTipoDom.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "cmbTipo", "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            cmbTipo.Items.Add("TUTTI")
            cmbTipo.Items.FindByText("TUTTI").Selected = True

            par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID > 0 AND ID <10 OR ID = 22 ORDER BY NOME ASC", "NOME", "ID")
            cmbFiliale.Items.Add("TUTTI")
            cmbFiliale.Items.FindByText("TUTTI").Selected = True

            Query = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=0 OR FL_NUOVA_NORMATIVA=1 ORDER BY DESCRIZIONE ASC"
            CaricaTipologie()



            par.RiempiDList(Me, par.OracleConn, "cmbstatodom", "SELECT * FROM T_TIPO_DECISIONI_VSA WHERE COD < 7 ORDER BY COD ASC", "DESCRIZIONE", "COD")
            cmbstatodom.Items.Add(New ListItem("AUTORIZZATA", "001"))
            cmbstatodom.Items.Add(New ListItem("CONTABILIZZATA", "002"))
            cmbstatodom.Items.Add(New ListItem("NESSUNA DECISIONE", "0"))
            cmbstatodom.Items.Add("TUTTI")
            cmbstatodom.Items.FindByText("TUTTI").Selected = True


           
            par.caricaComboBox("SELECT distinct OPERATORI.id,OPERATORE FROM OPERATORI,eventi_bandi_vsa where eventi_bandi_vsa.id_operatore=operatori.id(+) ORDER BY OPERATORE ASC", cmbOperatore, "ID", "OPERATORE", True)


            txtDataAutorizzAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataAutorizzDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataEventoAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataEventoDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataFineAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataFineDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtDataInizioAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataInizioDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPresAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPresDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPGDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPGal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If

    End Sub

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
        End If

        If cmbTipo.SelectedValue <> "ERP" And cmbTipo.SelectedValue <> "L43198" Then
            cmbProvenASS.Visible = "False"
            lblSpecifico.Visible = "False"
        End If

    End Sub

    Protected Sub cmbTipoDom_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoDom.SelectedIndexChanged
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            cmbMotivo.Items.Clear()
            If cmbTipoDom.SelectedValue <> "TUTTI" Then
                par.cmd.CommandText = "SELECT * FROM T_CAUSALI_DOMANDA_VSA WHERE ID_MOTIVO=" & cmbTipoDom.SelectedValue & " ORDER BY DESCRIZIONE ASC"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.HasRows = True Then
                    cmbMotivo.Items.Add(New ListItem("TUTTI", "-1"))
                    Do While myReaderA.Read
                        lblMotivo.Visible = True
                        cmbMotivo.Visible = True
                        cmbMotivo.Items.Add(New ListItem(par.IfNull(myReaderA("DESCRIZIONE"), ""), par.IfNull(myReaderA("COD"), "")))
                    Loop
                End If
                myReaderA.Close()
            Else
                lblMotivo.Visible = False
                cmbMotivo.Visible = False
            End If
            'cmbMotivo.Items.Add("TUTTI")
            'cmbMotivo.Items.FindByText("TUTTI").Selected = True

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        'Response.Write("<script>location.replace('RisultatoContratti.aspx?GIMI=" & txtCodGIMI.Text & "&PIVA=" & txtpiva.Text & "&FDAL=" & par.AggiustaData(txtScadeDal.Text) & "&FAL=" & par.AggiustaData(txtScadeAl.Text) & "&RDAL=" & par.AggiustaData(txtRinnovoDal.Text) & "&RAL=" & par.AggiustaData(txtRinnovoAl.Text) & "&SDAL=" & par.AggiustaData(txtStipulaDal.Text) & "&SAL=" & par.AggiustaData(txtStipulaAl.Text) & "&DDAL=" & par.AggiustaData(txtDecorrenzaDal.Text) & "&DAL=" & par.AggiustaData(txtDecorrenzaAl.Text) & "&ST=" & cmbStato.SelectedValue & "&UN=" & txtUnita.Text & "&TI=" & sTipoImm & "&TC=" & sTipo & "&CO=" & par.VaroleDaPassare(txtCod.Text) & "&CG=" & par.VaroleDaPassare(txtCognome.Text) & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCF.Text) & "&RS=" & par.VaroleDaPassare(txtRagione.Text) & "&RECDA=" & par.AggiustaData(txtDisdettaDal.Text) & "&RECA=" & par.AggiustaData(txtDisdettaAl.Text) & "&SLODA=" & par.AggiustaData(txtSloggioDal.Text) & "&SLOA=" & par.AggiustaData(txtSloggioAl.Text) & "&SLOV=" & Valore01(ChSloggio.Checked) & "&INSDA=" & par.AggiustaData(txtInseritoDal.Text) & "&INSA=" & par.AggiustaData(txtInseritoAl.Text) & "&VIRT=" & Valore01(ChVirtuali.Checked) & "&TIPO=" & cmbTipo.SelectedValue & "&PROV=" & cmbProvenASS.SelectedValue & "&DUR=" & txtDurata.Text & "&RINN=" & txtRinnovo.Text & "&INTEST=" & intest & "');</script>")
        Dim sTipoRapp As String = ""
        Dim sFiliale As String = ""
        Dim sMotivo As String = ""
        Dim sTipoDom As String = ""
        Dim sStatoDom As String = ""
        Dim sInvalid As String = ""



        If cmbTipo.Items.FindByText("TUTTI").Selected = True Then
            sTipoRapp = ""
        Else
            sTipoRapp = cmbTipo.SelectedItem.Value
        End If
        If cmbFiliale.Items.FindByText("TUTTI").Selected = True Then
            sFiliale = ""
        Else
            sFiliale = cmbFiliale.SelectedItem.Value
        End If
        If cmbMotivo.Visible = True Then
            If cmbMotivo.SelectedItem.Text = "TUTTI" Then
                sMotivo = ""
            Else
                sMotivo = cmbMotivo.SelectedItem.Value
            End If
        End If

        If cmbTipoDom.Items.FindByText("TUTTI").Selected = True Then
            If DropDownListType.SelectedItem.Value = "TUTTI" Then
                sTipoDom = ""
            Else
                sTipoDom = DropDownListType.SelectedItem.Value
            End If

        Else
            sTipoDom = cmbTipoDom.SelectedItem.Value
        End If

        If cmbstatodom.Items.FindByText("TUTTI").Selected = True Then
            sStatoDom = ""
        Else
            sStatoDom = cmbstatodom.SelectedItem.Value
        End If

        If ddl_invalid.SelectedItem.Value = "0" Then
            sInvalid = "0"
        End If

        If ddl_invalid.SelectedItem.Value = "1" Then
            sInvalid = "SI"
        End If

        If ddl_invalid.SelectedItem.Value = "2" Then
            sInvalid = "NO"
        End If

        Response.Write("<script>location.replace('RisultatoContratti_VSA.aspx?PIVA=" & txtIva.Text & "&FDAL=" & par.AggiustaData(txtDataFineDAL.Text) & "&FAL=" & par.AggiustaData(txtDataFineAL.Text) & "&AUTDAL=" & par.AggiustaData(txtDataAutorizzDAL.Text) & "&AUTAL=" & par.AggiustaData(txtDataAutorizzAL.Text) & "&PDAL=" & par.AggiustaData(txtDataPresDAL.Text) & "&PAL=" & par.AggiustaData(txtDataPresAL.Text) & "&IDAL=" & par.AggiustaData(txtDataInizioDAL.Text) & "&IAL=" & par.AggiustaData(txtDataInizioAL.Text) & "&EDAL=" & par.AggiustaData(txtDataEventoDAL.Text) & "&EAL=" & par.AggiustaData(txtDataEventoAL.Text) & "&ST=" & cmbStato.SelectedValue & "&UN=" & txtCodUI.Text & "&TI=" & cmbTipoUnita.SelectedValue & "&TC=" & sTipoRapp & "&CO=" & par.VaroleDaPassare(txtCodContr.Text) & "&CG=" & par.VaroleDaPassare(txtCognome.Text) & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCodF.Text) & "&TDOM=" & sTipoDom & "&PROV=" & cmbProvenASS.SelectedValue & "&DUR=" & txtDurata.Text & "&RINN=" & txtRinnovo.Text & "&PG=" & txtNumPG.Text & "&STDOM=" & sStatoDom & "&CAUS=" & sMotivo & "&FIL=" & sFiliale & "&INVAL=" & sInvalid & "&DPGDAL=" & par.AggiustaData(txtDataPGDal.Text) & "&DPGAL=" & par.AggiustaData(txtDataPGal.Text) & "&OP=" & cmbOperatore.SelectedValue & "&STSOSP=" & cmbStatoSospesa.SelectedValue & "&QN=" & DropDownListType.SelectedValue & "');</script>")
    End Sub

    Private Sub CaricaTipologie()
        cmbTipoDom.Items.Clear()
        par.RiempiDList(Me, par.OracleConn, "cmbTipoDom", Query, "DESCRIZIONE", "ID")
        cmbTipoDom.Items.Add("TUTTI")
        cmbTipoDom.Items.FindByText("TUTTI").Selected = True
    End Sub

    Public Property Query() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property

    Protected Sub DropDownListType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListType.SelectedIndexChanged
        Select Case DropDownListType.SelectedItem.Value
            Case "TUTTI"
                Query = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=0 OR FL_NUOVA_NORMATIVA=1 ORDER BY DESCRIZIONE ASC"
            Case "R.R. 1/2004 e s.m.i." 'VECCHIA NORMATIVA
                Query = "Select * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=0 Or FL_NUOVA_NORMATIVA=0 ORDER BY DESCRIZIONE ASC"
            Case "R.R. 4/2017 e s.m.i." 'NUOVANORMATIVA
                Query = "SELECT * FROM T_MOTIVO_DOMANDA_VSA WHERE FL_NUOVA_NORMATIVA=1 ORDER BY DESCRIZIONE ASC"
        End Select
        CaricaTipologie()
    End Sub
End Class
