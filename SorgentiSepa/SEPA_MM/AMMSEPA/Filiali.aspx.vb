
Partial Class AMMSEPA_Filiali
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim stringa As String = ""

    Function Eventi_Gestione(ByVal operatore As String, ByVal cod_evento As String, ByVal motivazione As String) As Integer
        Dim data As String = Format(Now, "yyyyMMddHHmmss")
        Try
            PAR.cmd.CommandText = "INSERT INTO EVENTI_GESTIONE (ID_OPERATORE, COD_EVENTO, DATA_ORA, MOTIVAZIONE) VALUES" _
                            & " (" & operatore & ",'" & cod_evento & "'," & data & ",'" & motivazione & "')"
            PAR.cmd.ExecuteNonQuery()
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
        Return 0
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()

        Try
            If Not IsPostBack Then
                BindGrid()
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim Str As String = "SELECT TAB_FILIALI.*,TIPOLOGIA_STRUTTURA_ALER.DESCRIZIONE AS TIPOFILIALE,  INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,INDIRIZZI.CAP,COMUNI_NAZIONI.NOME AS ""COMUNE"" FROM SISCOM_MI.TIPOLOGIA_STRUTTURA_ALER,SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE TAB_FILIALI.ID_TIPO_ST=TIPOLOGIA_STRUTTURA_ALER.ID AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO ORDER BY TAB_FILIALI.NOME asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "TAB_FILIALI,INDIRIZZI")

            DataGridIntLegali.DataSource = ds
            DataGridIntLegali.DataBind()

            par.RiempiDList(Me, par.OracleConn, "cmbTecnica", "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST IN (1,2) ORDER BY NOME ASC", "NOME", "ID")
            cmbTecnica.Items.Add(New ListItem("--", "-1"))

            cmbTecnica.ClearSelection()
            cmbTecnica.Items.FindByValue("-1").Selected = True

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub img_InserisciSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            Dim CodComune As String = ""

            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then
                Dim lIdIndirizzo As Double = 0

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If



                If cmbTipo.SelectedItem.Value = "0" And cmbTecnica.SelectedItem.Value = "-1" Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Attenzione...specificare la Struttura Tecnica di appartenenza!')</script>")
                    par.RiempiDList(Me, par.OracleConn, "cmbTecnica", "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST IN (1,2) ORDER BY NOME ASC", "NOME", "ID")
                    cmbTecnica.Items.Add(New ListItem("--", "-1"))
                    cmbTecnica.ClearSelection()
                    cmbTecnica.Items.FindByValue("-1").Selected = True
                    Dvisibile.value = "1"
                    Exit Sub
                End If

                If par.IfEmpty(Me.txtTelefono.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtIndirizzo.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCivico.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCap.Text, "Null") <> "Null" Then



                    par.cmd.CommandText = "SELECT COD FROM COMUNI_NAZIONI WHERE UPPER(NOME)='" & par.PulisciStrSql(txtComune.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CodComune = par.IfNull(myReader(0), "")
                    End If
                    myReader.Close()

                    If CodComune = "" Then
                        Response.Write("<script>alert('Il Comune inserito non è stato trovato!')</script>")
                        Me.txtComune.Text = ""
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If

                    par.cmd.CommandText = "Insert into SISCOM_MI.INDIRIZZI (ID, DESCRIZIONE, CIVICO, CAP, LOCALITA, " _
                                        & "COD_COMUNE) Values " _
                                        & "(SISCOM_MI.SEQ_INDIRIZZI.NEXTVAL, '" & par.PulisciStrSql(txtIndirizzo.Text.ToUpper) _
                                        & "', '" & par.PulisciStrSql(txtCivico.Text.ToUpper) _
                                        & "', '" & par.PulisciStrSql(txtCap.Text.ToUpper) _
                                        & "', '" & par.PulisciStrSql(txtComune.Text.ToUpper) _
                                        & "', '" & CodComune & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_indirizzi.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        lIdIndirizzo = myReader(0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.TAB_FILIALI (ID,NOME,ID_INDIRIZZO,ID_TIPO_ST,ID_TECNICA,N_TELEFONO,N_FAX,REF_AMMINISTRATIVO,N_TELEFONO_VERDE,RESPONSABILE,CENTRO_DI_COSTO,ACRONIMO,PROCURA,PROCURA1) VALUES " _
                        & "(SISCOM_MI.SEQ_TAB_FILIALI.NEXTVAL,'" & par.PulisciStrSql(Me.txtNome.Text.ToUpper) & "'," & lIdIndirizzo & "," _
                        & cmbTipo.SelectedItem.Value & "," & cmbTecnica.SelectedItem.Value _
                                        & ",'" & par.PulisciStrSql(txtTelefono.Text.ToUpper) _
                                        & "','" & par.PulisciStrSql(txtFax.Text.ToUpper) _
                                        & "','" & par.PulisciStrSql(txtReferente.Text.ToUpper) & "','" & par.PulisciStrSql(txtTelefonoVerde.Text.ToUpper) & "','" _
                                        & par.PulisciStrSql(txtResponsabile.Text.ToUpper) _
                                        & "','" & par.PulisciStrSql(txtCentro.Text.ToUpper) & "','" & par.PulisciStrSql(txtAcronimo.Text.ToUpper) & "','" & par.PulisciStrSql(txtprocura.Text) & "','" & par.PulisciStrSql(txtprocura.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    '------------INSERT DENTRO PF_VOCI_sTRUTTURA------------
                    par.cmd.CommandText = "select id from siscom_mi.pf_main"
                    Dim lettorePF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim idPF As Integer = 0
                    While lettorePF.Read
                        idPF = par.IfNull(lettorePF(0), 0)
                        par.cmd.CommandText = "SELECT 'INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (ASSESTAMENTO_VALORE_LORDO, COMPLETO, COMPLETO_ALER, COMPLETO_COMUNE, DATA_ASSESTAMENTO, ID_STRUTTURA, ID_VOCE, IVA, VALORE_LORDO, VALORE_LORDO_ALER, VALORE_NETTO, VARIAZIONI) VALUES (0,0,0,2,NULL,' " _
                            & " || TAB_FILIALI.ID " _
                            & " || ',' " _
                            & " || PF_VOCI.ID " _
                            & " || ',0,0,0,0,0)' " _
                            & " FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.PF_VOCI " _
                            & " WHERE ID_PIANO_FINANZIARIO = " & idPF _
                            & " MINUS " _
                            & " SELECT DISTINCT " _
                            & " 'INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (ASSESTAMENTO_VALORE_LORDO, COMPLETO, COMPLETO_ALER, COMPLETO_COMUNE, DATA_ASSESTAMENTO, ID_STRUTTURA, ID_VOCE, IVA, VALORE_LORDO, VALORE_LORDO_ALER, VALORE_NETTO, VARIAZIONI) VALUES (0,0,0,2,NULL,' " _
                            & " || ID_STRUTTURA " _
                            & " || ',' " _
                            & " || ID_VOCE " _
                            & " || ',0,0,0,0,0)' " _
                            & " FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                            & " WHERE ID_VOCE IN (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI " _
                            & " WHERE ID_PIANO_FINANZIARIO = " & idPF & ")"
                        Dim LettoreInsert As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        While LettoreInsert.Read
                            par.cmd.CommandText = par.IfNull(LettoreInsert(0), "")
                            par.cmd.ExecuteNonQuery()
                        End While
                        LettoreInsert.Close()
                    End While
                    lettorePF.Close()
                    '------------INSERT DENTRO PF_VOCI_sTRUTTURA------------


                    Response.Write("<script>alert('Operazione effettuata!');</script>")

                    Try
                        Dim operatore As String = Session.Item("ID_OPERATORE")
                        Eventi_Gestione(operatore, "F55", "INSERIMENTO Sede Territoriale " & txtNome.Text)
                    Catch ex As Exception
                        lblErrore.Text = ex.Message
                        lblErrore.Visible = True
                    End Try
                    Me.txtNome.Text = ""
                    Me.txtIndirizzo.Text = ""
                    Me.txtCivico.Text = ""
                    Me.txtCap.Text = ""
                    Me.txtid.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"
                    Me.txtTelefono.Text = ""
                    Me.txtTelefonoVerde.Text = ""
                    Me.txtFax.Text = ""
                    Me.txtReferente.Text = ""
                    Me.txtResponsabile.Text = ""
                    Me.txtCentro.Text = ""
                    Me.txtAcronimo.Text = ""
                    Me.txtprocura.Text = ""
                    par.RiempiDList(Me, par.OracleConn, "cmbTecnica", "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST IN (1,2) ORDER BY NOME ASC", "NOME", "ID")
                    cmbTecnica.Items.Add(New ListItem("--", "-1"))
                    cmbTecnica.ClearSelection()
                    cmbTecnica.Items.FindByValue("-1").Selected = True

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    BindGrid()
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Attenzione, tutti i campi sono obbligatori!')</script>")
                    Dvisibile.Value = "1"
                    par.RiempiDList(Me, par.OracleConn, "cmbTecnica", "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_S IN (1,2) ORDER BY NOME ASC", "NOME", "ID")
                    cmbTecnica.Items.Add(New ListItem("--", "-1"))
                    cmbTecnica.ClearSelection()
                    cmbTecnica.Items.FindByValue("-1").Selected = True
                End If
                ' Me.txtNome.Text = ""
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    'Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
    '    txtNome.Text = ""
    'End Sub



    Protected Sub DataGridIntLegali_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIntLegali.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub DataGridIntLegali_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIntLegali.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridIntLegali.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Public Property vIdIndirizzo() As Long
        Get
            If Not (ViewState("par_vIdIndirizzo") Is Nothing) Then
                Return CLng(ViewState("par_vIdIndirizzo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdIndirizzo") = value
        End Set

    End Property

    Protected Sub ImgModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        If txtid.Value.ToString <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT  TAB_FILIALI.*, INDIRIZZI.ID AS ID_INDIRIZZO ,  INDIRIZZI.DESCRIZIONE ,  INDIRIZZI.CIVICO, INDIRIZZI.CAP, INDIRIZZI.localita,COMUNI_NAZIONI.nome FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD AND TAB_FILIALI.ID = " & Me.txtid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtNome.Text = par.IfNull(myReader("NOME").ToString, "")
                Me.txtIndirizzo.Text = par.IfNull(myReader("DESCRIZIONE").ToString, "")
                Me.txtCivico.Text = par.IfNull(myReader("CIVICO").ToString, "")
                Me.txtCap.Text = par.IfNull(myReader("CAP").ToString, "")
                txtComune.Text = par.IfNull(myReader("LOCALITA").ToString, "MILANO")

                txtTelefono.Text = par.IfNull(myReader("N_TELEFONO").ToString, "")
                txtTelefonoVerde.Text = par.IfNull(myReader("N_TELEFONO_VERDE").ToString, "")
                txtCentro.Text = par.IfNull(myReader("CENTRO_DI_COSTO").ToString, "")
                txtAcronimo.Text = par.IfNull(myReader("ACRONIMO").ToString, "")

                txtFax.Text = par.IfNull(myReader("N_FAX").ToString, "")
                txtReferente.Text = par.IfNull(myReader("REF_AMMINISTRATIVO").ToString, "")
                txtResponsabile.Text = par.IfNull(myReader("RESPONSABILE").ToString, "")

                vIdIndirizzo = myReader("ID_INDIRIZZO").ToString
                cmbTipo.ClearSelection()
                cmbTipo.Items.FindByValue(par.IfNull(myReader("ID_TIPO_ST"), "0")).Selected = True



                If par.IfNull(myReader("ID_TIPO_ST"), "0") = "0" Then
                    cmbTecnica.ClearSelection()
                    cmbTecnica.Items.FindByValue(par.IfNull(myReader("ID_TECNICA"), "-1")).Selected = True
                End If

                If cmbTipo.SelectedItem.Value <> "0" Then
                    cmbTecnica.Visible = False
                    lblTecnica.Visible = False
                Else
                    cmbTecnica.Visible = True
                    lblTecnica.Visible = True
                End If

                txtprocura.Text = par.IfNull(myReader("PROCURA").ToString, "")

            End If
            'vTesto = Me.txtDescrizione.Text
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"
            Dvisibile.Value = "1"

        Else
            Me.TextBox1.Value = 0
            Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")

        End If
    End Sub

    'Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
    '    Me.txtNome.Text = ""
    '    Me.txtIndirizzo.Text = ""
    '    Me.txtCivico.Text = ""
    '    Me.txtCap.Text = ""
    '    Me.txtid.Value = ""
    '    Me.txtmia.Text = "Nessuna Selezione"

    'End Sub
    Private Sub Update()

        If cmbTipo.SelectedItem.Value = "0" And cmbTecnica.SelectedItem.Value = "-1" Then
            Response.Write("<script>alert('Attenzione...specificare la Struttura Tecnica di appartenenza!')</script>")
            Dvisibile.Value = "1"
            Exit Sub
        End If

        If par.IfEmpty(Me.txtTelefono.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtIndirizzo.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCivico.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCap.Text, "Null") <> "Null" Then
            ''*********************APERTURA CONNESSIONE**********************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            par.cmd.CommandText = "UPDATE SISCOM_MI.TAB_FILIALI SET ACRONIMO='" & par.PulisciStrSql(Me.txtAcronimo.Text.ToUpper) & "',CENTRO_DI_COSTO='" & par.PulisciStrSql(Me.txtCentro.Text.ToUpper) & "',RESPONSABILE='" & par.PulisciStrSql(Me.txtResponsabile.Text.ToUpper) & "',N_TELEFONO_VERDE='" & par.PulisciStrSql(Me.txtTelefonoVerde.Text.ToUpper) & "',N_TELEFONO='" & par.PulisciStrSql(Me.txtTelefono.Text.ToUpper) & "',N_FAX='" & par.PulisciStrSql(Me.txtFax.Text.ToUpper) & "',REF_AMMINISTRATIVO='" & par.PulisciStrSql(Me.txtReferente.Text.ToUpper) & "', NOME = '" & par.PulisciStrSql(Me.txtNome.Text.ToUpper) & "',ID_TIPO_ST=" & cmbTipo.SelectedItem.Value & ",ID_TECNICA=" & cmbTecnica.SelectedItem.Value & ",PROCURA='" & par.PulisciStrSql(txtprocura.Text) & "',PROCURA1='" & par.PulisciStrSql(txtprocura.Text) & "' WHERE TAB_FILIALI.ID = " & Me.txtid.Value
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.INDIRIZZI SET COD_COMUNE=(SELECT COD FROM COMUNI_NAZIONI WHERE UPPER(NOME)='" & par.PulisciStrSql(Me.txtComune.Text.ToUpper) & "'),LOCALITA='" & par.PulisciStrSql(Me.txtComune.Text.ToUpper) & "',INDIRIZZI.DESCRIZIONE = '" & par.PulisciStrSql(Me.txtIndirizzo.Text.ToUpper) & "', INDIRIZZI.CIVICO = '" & par.PulisciStrSql(txtCivico.Text.ToUpper) & "', CAP = '" & par.PulisciStrSql(txtCap.Text.ToUpper) & "' WHERE INDIRIZZI.ID = " & vIdIndirizzo
            par.cmd.ExecuteNonQuery()

            stringa = txtmia.Text
            NomeCommissariato()
            Try
                Dim operatore As String = Session.Item("ID_OPERATORE")
                Eventi_Gestione(operatore, "F02", "MODIFICA SEDE TERRITORIALE" & stringa)
            Catch ex As Exception
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
            End Try

            par.RiempiDList(Me, par.OracleConn, "cmbTecnica", "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST in (1,2) ORDER BY NOME ASC", "NOME", "ID")
            cmbTecnica.Items.Add(New ListItem("--", "-1"))

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()
            BindGrid()
            Dvisibile.Value = "0"
            Me.TextBox1.Value = "0"
            Me.txtNome.Text = ""
            Me.txtIndirizzo.Text = ""
            Me.txtCivico.Text = ""
            Me.txtCap.Text = ""
            Me.txtid.Value = ""
            Me.txtmia.Text = "Nessuna Selezione"
            Me.txtTelefono.Text = ""
            Me.txtTelefonoVerde.Text = ""
            Me.txtFax.Text = ""
            Me.txtReferente.Text = ""
            Me.txtResponsabile.Text = ""
            Me.txtCentro.Text = ""
            Me.txtAcronimo.Text = ""
            Me.txtprocura.Text = ""
            Response.Write("<script>alert('Operazione effettuata!');</script>")
        Else

            Response.Write("<script>alert('Attenzione, tutti i campi sono obbligatori!')</script>")
            Dvisibile.Value = "1"
            par.RiempiDList(Me, par.OracleConn, "cmbTecnica", "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST IN (1,2) ORDER BY NOME ASC", "NOME", "ID")
            cmbTecnica.Items.Add(New ListItem("--", "-1"))
            cmbTecnica.ClearSelection()
            cmbTecnica.Items.FindByValue("-1").Selected = True
        End If


    End Sub


    Protected Sub cmbTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        If cmbTipo.SelectedItem.Value <> "0" Then
            cmbTecnica.Visible = False
            lblTecnica.Visible = False
            cmbTecnica.ClearSelection()
            cmbTecnica.Items.FindByValue("-1").Selected = True
        Else
            cmbTecnica.Visible = True
            lblTecnica.Visible = True
        End If
        Dvisibile.Value = "1"
    End Sub

    Protected Sub ImgBtnAggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAggiungi.Click
        Try
            If sicuro.Value = "1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                If par.IfEmpty(Me.txtid.Value, "") <> "" Then

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.TAB_FILIALI WHERE ID = " & Me.txtid.Value
                    par.cmd.ExecuteNonQuery()
                    stringa = txtmia.Text
                    NomeCommissariato()
                    Try
                        Dim operatore As String = Session.Item("ID_OPERATORE")
                        Eventi_Gestione(operatore, "F56", "CANCELLAZIONE SEDE TERRITORIALE" & stringa)
                    Catch ex As Exception
                        lblErrore.Text = ex.Message
                        lblErrore.Visible = True
                    End Try
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    BindGrid()
                    Me.txtNome.Text = ""
                    Me.txtIndirizzo.Text = ""
                    Me.txtCivico.Text = ""
                    Me.txtCap.Text = ""
                    Me.txtid.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"
                    Me.txtTelefono.Text = ""
                    Me.txtTelefonoVerde.Text = ""
                    Me.txtFax.Text = ""
                    Me.txtReferente.Text = ""
                    Me.txtResponsabile.Text = ""
                    Me.txtCentro.Text = ""
                    Me.txtAcronimo.Text = ""
                    txtprocura.Text = ""
                Else
                    Response.Write("<script>alert('Nessuna voce selezionata!')</script>")
                    par.OracleConn.Close()

                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            If EX1.Number = 2292 Then
                lblErrore.Text = "Sede Territoriale in uso. Non è possibile eliminare!"
            Else
                lblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub NomeCommissariato()
        Try
            Dim posiniziale As Long
            Dim posfinale As Long
            Dim rimozione As String
            Do
                posiniziale = InStr(1, stringa, "Hai selezionato")
                If posiniziale > 0 Then
                    posfinale = InStr(1, stringa, ": ")
                    If posfinale > 0 Then
                        rimozione = Mid$(stringa, posiniziale, posfinale - posiniziale + 1)
                        stringa = Replace(stringa, rimozione, vbNullString)
                    Else
                        Exit Do
                    End If
                End If
            Loop Until posiniziale = 0
        Catch ex As Exception

        End Try
    End Sub
End Class
