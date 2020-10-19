
Partial Class Condomini_EventiPatrimoniali
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        If Not IsPostBack Then
            txtMil_Pro.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
            txtAsc.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
            txtMil_Compro.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
            txtMil_Gest.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
            txt_Mil_Risc.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")
            txtAddebito.Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

            Cerca()
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Me.cmbTipoCor.Items.Clear()
            Do While myReader1.Read
                Me.cmbTipoCor.Items.Add(New ListItem(myReader1("DESCRIZIONE"), myReader1("COD")))
            Loop
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End If
    End Sub
    Public Property vIndirizzoCor() As String
        Get
            If Not (ViewState("par_vIndirizzoCor") Is Nothing) Then
                Return CStr(ViewState("par_vIndirizzoCor"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIndirizzoCor") = value
        End Set

    End Property
    Public Property vTutti() As String
        Get
            If Not (ViewState("vTutti") Is Nothing) Then
                Return CStr(ViewState("vTutti"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("vTutti") = value
        End Set

    End Property
    Private Sub Cerca()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim STR As String
            vTutti = Request.QueryString("TUTTI")
            If vTutti = 0 Then
                STR = "SELECT cond_avvisi.id,condomini.ID as id_condominio,condomini.denominazione as condominio, cond_avvisi.id_ui," _
                    & " to_char(to_date(COND_AVVISI.DATA,'yyyymmdd'),'dd/mm/yyyy')AS DATA,tipo_cond_avvisi.descrizione AS evento, cod_unita_immobiliare AS COD_UI, COND_AVVISI.ID_CONTRATTO " _
                    & " FROM siscom_mi.condomini,siscom_mi.cond_avvisi, siscom_mi.unita_immobiliari,siscom_mi.tipo_cond_avvisi WHERE COND_AVVISI.VISTO = 0 AND" _
                    & " cond_avvisi.ID_CONDOMINIO=CONDOMINI.ID AND COND_AVVISI.id_ui = unita_immobiliari.ID" _
                    & " AND (id_tipo = 4 OR ((id_tipo = 3 OR id_tipo = 1 or id_tipo = 7 or id_tipo = 5 or id_tipo = 6) AND ID_UI IN (SELECT DISTINCT(ID_UI) FROM SISCOM_MI.COND_UI WHERE id_condominio = cond_avvisi.id_condominio)))" _
                    & " AND cond_avvisi.id_tipo = tipo_cond_avvisi.ID(+) ORDER BY COND_AVVISI.DATA DESC, ID_UI ASC"
            Else
                STR = "SELECT cond_avvisi.id,condomini.ID as id_condominio,condomini.denominazione as condominio, cond_avvisi.id_ui," _
                    & " to_char(to_date(COND_AVVISI.DATA,'yyyymmdd'),'dd/mm/yyyy')AS DATA,tipo_cond_avvisi.descrizione AS evento, cod_unita_immobiliare AS COD_UI, COND_AVVISI.ID_CONTRATTO " _
                    & " FROM siscom_mi.condomini,siscom_mi.cond_avvisi, siscom_mi.unita_immobiliari,siscom_mi.tipo_cond_avvisi WHERE " _
                    & " cond_avvisi.ID_CONDOMINIO=CONDOMINI.ID AND COND_AVVISI.id_ui = unita_immobiliari.ID" _
                    & " AND (id_tipo = 4 OR ((id_tipo = 3 OR id_tipo = 1 or id_tipo = 7 or id_tipo = 5 or id_tipo = 6) AND ID_UI IN (SELECT DISTINCT(ID_UI) FROM SISCOM_MI.COND_UI WHERE id_condominio = cond_avvisi.id_condominio)))" _
                    & " AND cond_avvisi.id_tipo = tipo_cond_avvisi.ID(+) ORDER BY COND_AVVISI.DATA DESC, ID_UI ASC"
            End If

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(STR, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)


            DataGridBManager.DataSource = dt
            DataGridBManager.DataBind()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub ChkLetto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If DirectCast(sender, System.Web.UI.WebControls.CheckBox).Checked = True Then
            DirectCast(sender, System.Web.UI.WebControls.CheckBox).Attributes("ID_UI").ToString()
        End If

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        Dim scriptblock As String = ""

        If Me.IdCondominio.Value <> 0 AndAlso Me.IdUnita.Value <> 0 AndAlso Me.id.Value.Replace("&nbsp", 0) <> 0 Then
            Me.txtPosBil.Text = ""
            Me.txtMil_Pro.Text = ""
            Me.txtAsc.Text = ""
            Me.txtMil_Compro.Text = ""
            Me.txtMil_Gest.Text = ""
            Me.txt_Mil_Risc.Text = ""
            Me.txtNote.Text = ""
            Me.txtStatoRapp.Text = ""
            Me.TxtTipologia.Text = ""
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Try
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "SELECT CONDOMINI.DENOMINAZIONE,(SELECT tipo_cond_avvisi.descrizione " _
                                    & "FROM siscom_mi.cond_avvisi,siscom_mi.tipo_cond_avvisi " _
                                    & "WHERE cond_avvisi.id_tipo = tipo_cond_avvisi.ID(+) " _
                                    & "AND id_condominio = condomini.ID AND id_ui = " & IdUnita.Value & " and cond_avvisi.DATA = '" & par.AggiustaData(DataEvento.Value) & "' and cond_avvisi.id=" & id.Value & ") AS tipo " _
                                    & "FROM SISCOM_MI.CONDOMINI WHERE ID = " & IdCondominio.Value
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    Me.lblTitolo.Text = par.IfNull(myReader1("TIPO"), "") & " Condominio " & myReader1("DENOMINAZIONE")
                    If par.IfNull(myReader1("TIPO"), "").ToString.Contains("SLOGGIO") Then
                        Me.btnComunicazione.Visible = True
                    Else
                        Me.btnComunicazione.Visible = False
                    End If
                End If
                myReader1.Close()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_UI WHERE ID_UI = " & IdUnita.Value & " AND ID_CONDOMINIO = " & IdCondominio.Value
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                Else
                    InsertCondUi()
                End If

                'Dim condInCorso As String
                'If descEvento.Value.ToString.ToUpper.Contains("ATTIVAZIONE") Then
                '    condInCorso = " ORDER BY ID_CONTRATTO DESC"
                'Else
                '    condInCorso = ""
                'End If

                descEvento.Value = ""
                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,nvl(ID_CONTRATTO,'') as ID_CONTRATTO, " _
                    & "UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA, " _
                    & "POSIZIONE_BILANCIO, TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO,TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC, " _
                    & "TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST,TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC, " _
                    & "COND_UI.NOTE,(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,1) END) AS STATO,siscom_mi.GetIntestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,1) AS INTESTATARIO,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO " _
                    & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.ANAGRAFICA " _
                    & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO = " & idContratto.Value & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) " _
                    & "AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio=" & IdCondominio.Value & " OR cond_ui.id_condominio IS NULL) AND unita_immobiliari.ID =" & IdUnita.Value
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                Dim dt As New Data.DataTable()
                da.Fill(dt)
                If dt.Rows.Count = 0 Then
                    Response.Write("<script>alert('Operazione annullata!Ci sono dei problemi!\nAnnotare il condominio selezionato e contattare l\'amministratore');</script>")
                    Me.TextBox4.Value = 1
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If
                If dt.Rows(0).Item("ID_CONTRATTO").ToString <> "" Then
                    'idContratto.Value = dt.Rows(0).Item("ID_CONTRATTO").ToString
                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & dt.Rows(0).Item("ID_CONTRATTO").ToString & " FOR UPDATE NOWAIT"
                    'myReader1 = par.cmd.ExecuteReader()
                    'If myReader1.Read Then

                    'End If
                    'myReader1.Close()
                    Me.cmbTipoCor.SelectedIndex = -1
                    par.cmd.CommandText = "SELECT DATA_DECORRENZA,DATA_RICONSEGNA,PRESSO_COR,TIPO_COR,VIA_COR,CIVICO_COR,LUOGO_COR,CAP_COR,(CASE WHEN RAPPORTI_UTENZA.ID IS NULL THEN 'LIBERO' ELSE  SISCOM_MI.Getstatocontratto2(RAPPORTI_UTENZA.ID,0) END) AS STATO  FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & dt.Rows(0).Item("ID_CONTRATTO").ToString
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        vIndirizzoCor = (Trim(par.IfNull(myReader1("TIPO_COR"), "") & par.PulisciStrSql(par.IfNull(myReader1("VIA_COR").ToString, "")) & par.PulisciStrSql(par.IfNull(myReader1("CIVICO_COR"), "")) & myReader1("CAP_COR") & par.PulisciStrSql(par.IfNull(myReader1("LUOGO_COR"), ""))).ToUpper)
                        Me.cmbTipoCor.Items.FindByText(par.IfNull(myReader1("TIPO_COR"), "VIA")).Selected = True
                        Me.txtVia.Text = par.IfNull(myReader1("VIA_COR"), "")
                        Me.txtCivico.Text = par.IfNull(myReader1("CIVICO_COR"), "")
                        Me.txtCap.Text = par.IfNull(myReader1("CAP_COR"), "")
                        Me.txtLocalità.Text = par.IfNull(myReader1("LUOGO_COR"), "")
                        Me.txtPresso.Text = par.IfNull(myReader1("PRESSO_COR"), "")
                        Me.txtStatoRapp.Text = par.IfNull(myReader1("STATO"), "")
                        Me.txtDataDecorrenza.Text = par.FormattaData(par.IfNull(myReader1("DATA_DECORRENZA"), ""))
                        Me.txtDataSloggio.Text = par.FormattaData(par.IfNull(myReader1("DATA_RICONSEGNA"), ""))
                    End If
                    myReader1.Close()
                    par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND  nvl(DATA_FINE,'29991231')>= to_char(to_date(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd') AND ID_CONTRATTO=" & dt.Rows(0).Item("ID_CONTRATTO").ToString
                    myReader1 = par.cmd.ExecuteReader()
                    Me.cmbIntestatari.Items.Clear()
                    cmbIntestatari.Items.Add(New ListItem(" ", ""))
                    Dim i As Integer = 0
                    Do While myReader1.Read
                        If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") <> "" Then
                            Me.cmbIntestatari.Items.Add(New ListItem(myReader1("RAGIONE_SOCIALE"), myReader1("ID")))
                        Else
                            Me.cmbIntestatari.Items.Add(New ListItem(myReader1("COGNOME") & " " & myReader1("NOME"), myReader1("ID")))
                        End If
                        i = i + 1
                    Loop
                    myReader1.Close()
                    Me.txtnumPers.Text = i

                    'Se singolo intestatario seleziono direttamente quello
                    If Me.cmbIntestatari.Items.Count = 2 Then
                        Me.cmbIntestatari.SelectedIndex = 1
                        'Else
                        '    Me.cmbIntestatari.SelectedValue = ""
                    End If
                Else
                    Me.cmbIntestatari.Items.Clear()
                    cmbIntestatari.Items.Add(New ListItem(" ", ""))

                    Me.txtVia.Text = ""
                    Me.txtCivico.Text = ""
                    Me.txtCap.Text = ""
                    Me.txtLocalità.Text = ""
                    Me.txtPresso.Text = ""

                End If

                'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.cond_ui WHERE ID_UI = " & IdUnita.Value & " FOR UPDATE NOWAIT"
                'myReader1 = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                'End If
                'myReader1.Close()

                par.cmd.CommandText = "SELECT COND_UI.*, UNITA_IMMOBILIARI.INTERNO, SCALE_EDIFICI.DESCRIZIONE,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO, (INDIRIZZI.DESCRIZIONE ||', '||INDIRIZZI.CIVICO||' '||INDIRIZZI.CAP||' '||INDIRIZZI.LOCALITA) AS INDIRIZZO FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.COND_UI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.INDIRIZZI WHERE ID_UI = " & IdUnita.Value & " AND ID_CONDOMINIO = " & IdCondominio.Value & " AND UNITA_IMMOBILIARI.ID = COND_UI.ID_UI AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID"
                Me.txtCodUI.Text = dt.Rows(0).Item("COD_UNITA_IMMOBILIARE")
                Me.TxtTipologia.Text = dt.Rows(0).Item("TIPOLOGIA")
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.txtMillPres.Text = Format(CDbl(par.IfNull(myReader1("MILL_PRES_ASS"), "0")), "0.0000")
                    Me.txtPosBil.Text = myReader1("POSIZIONE_BILANCIO").ToString
                    Me.txtMil_Pro.Text = Format(CDbl(par.IfNull(myReader1("MIL_PRO"), "0")), "0.0000")
                    Me.txtAsc.Text = Format(CDbl(par.IfNull(myReader1("MIL_ASC"), "0")), "0.0000")
                    Me.txtMil_Compro.Text = Format(CDbl(par.IfNull(myReader1("MIL_COMPRO"), "0").ToString), "0.0000")
                    Me.txtMil_Gest.Text = Format(CDbl(par.IfNull(myReader1("MIL_GEST"), "0")), "0.0000")
                    Me.txt_Mil_Risc.Text = Format(CDbl(par.IfNull(myReader1("MIL_RISC"), "0")), "0.0000")
                    Me.txtAddebito.Text = Format(CDbl(par.IfNull(myReader1("ADDEBITO_SINGOLO"), "0")), "0.00")
                    Me.txtNote.Text = myReader1("NOTE").ToString
                    Me.txtIndirizzoUI.Text = myReader1("INDIRIZZO").ToString
                    Me.txtInterno.Text = myReader1("INTERNO").ToString
                    Me.txtScala.Text = myReader1("DESCRIZIONE").ToString
                    Me.txtPiano.Text = myReader1("PIANO").ToString
                    Me.cmbIntestatari.ClearSelection()
                    If Me.cmbIntestatari.Items.Count = 2 And myReader1("ID_INTESTARIO").ToString = "" Then
                        Me.cmbIntestatari.SelectedIndex = 1
                    Else
                        Dim item = Me.cmbIntestatari.Items.FindByValue(myReader1("ID_INTESTARIO").ToString)
                        If Not IsNothing(item) Then
                            item.Selected = True
                        Else
                            Me.cmbIntestatari.SelectedIndex = 0
                        End If
                        'Me.cmbIntestatari.SelectedValue = myReader1("ID_INTESTARIO").ToString
                    End If
                    myReader1.Close()


                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Else
                    myReader1.Close()





                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    'par.OracleConn.Close()
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Il contratto associato, è aperto da un altro utente!Attendere...');" _
                    & "</script>"
                    TextBox4.Value = 0
                    Me.btnSalvaInquilini.Visible = False
                    'ApriFrmWithDBLock()
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                    End If
                Else
                    par.OracleConn.Close()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End If



            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try

            TextBox4.Value = 2
        Else
            Response.Write("<script>alert('Scegliere un elemento!');</script>")
            TextBox4.Value = 0

        End If
    End Sub
    Private Sub InsertCondUi()
        Try

            Dim ID_EDIFICIO As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO = (SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID =" & IdUnita.Value & ") AND ID_CONDOMINIO = " & IdCondominio.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                ID_EDIFICIO = lettore("ID_EDIFICIO")
            End If
            lettore.Close()


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_UI (ID_CONDOMINIO, ID_UI, ID_EDIFICIO) VALUES (" & IdCondominio.Value & ", " & IdUnita.Value & "," & ID_EDIFICIO & ")"
            par.cmd.ExecuteNonQuery()

            '****************MYEVENT*****************
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & IdCondominio.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F33','MILLESIMI INQUILINO CONDOMINIO')"
            par.cmd.ExecuteNonQuery()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try


    End Sub
    Protected Sub DataGridBManager_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridBManager.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor = 'pointer'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'evento del condominio: " & e.Item.Cells(4).Text.Replace("'", "\'") & "';document.getElementById('IdUnita').value='" & e.Item.Cells(1).Text & "';document.getElementById('IdCondominio').value='" & e.Item.Cells(2).Text & "';document.getElementById('DataEvento').value='" & e.Item.Cells(5).Text & "';document.getElementById('id').value='" & e.Item.Cells(0).Text & "';document.getElementById('idContratto').value='" & e.Item.Cells(3).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor = 'pointer'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'evento del condominio: " & e.Item.Cells(4).Text.Replace("'", "\'") & "';document.getElementById('IdUnita').value='" & e.Item.Cells(1).Text & "';document.getElementById('IdCondominio').value='" & e.Item.Cells(2).Text & "';document.getElementById('DataEvento').value='" & e.Item.Cells(5).Text & "';document.getElementById('id').value='" & e.Item.Cells(0).Text & "';document.getElementById('idContratto').value='" & e.Item.Cells(3).Text & "'")

        End If
    End Sub

    Protected Sub btnSalvaInquilini_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaInquilini.Click
        Dim scriptblock As String = ""

        Try
            If idContratto.Value <> "" Then

                If Me.cmbIntestatari.SelectedValue <> "" Then

                    '*******************APERURA CONNESSIONE*********************
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_UI SET POSIZIONE_BILANCIO= '" & par.PulisciStrSql(Me.txtPosBil.Text) & "' , MIL_PRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Pro.Text), "Null") & " " _
                   & " , MIL_ASC = " & par.IfEmpty(par.VirgoleInPunti(Me.txtAsc.Text), "Null") & ", MIL_COMPRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Compro.Text), "Null") & "" _
                   & " , MIL_GEST = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Gest.Text), "Null") & ", MIL_RISC = " & par.IfEmpty(par.VirgoleInPunti(Me.txt_Mil_Risc.Text), "Null") & "" _
                   & " , NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', ADDEBITO_SINGOLO = " & par.VirgoleInPunti(par.IfEmpty(Me.txtAddebito.Text, "Null")) & ", ID_INTESTARIO = " & par.IfEmpty(Me.cmbIntestatari.SelectedValue.ToString, "Null") & ", MILL_PRES_ASS = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMillPres.Text), "Null") & "  WHERE COND_UI.ID_UI = " & IdUnita.Value & " AND COND_UI.ID_CONDOMINIO = " & IdCondominio.Value

                    par.cmd.ExecuteNonQuery()

                    ''****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & IdCondominio.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F34','MILLESIMI INQUILINO')"
                    par.cmd.ExecuteNonQuery()


                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    '************* CONTROLLO BLOCCO RAPPORTO UTENZA ***********
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    If vIndirizzoCor <> (Trim(Me.cmbTipoCor.SelectedItem.Text & par.PulisciStrSql(Me.txtVia.Text) & par.PulisciStrSql(Me.txtCivico.Text) & Me.txtCap.Text & par.PulisciStrSql(Me.txtLocalità.Text)).ToUpper) Then

                        Try
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & idContratto.Value & " FOR UPDATE NOWAIT"
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                            End If
                            myReader1.Close()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET TIPO_COR='" & Me.cmbTipoCor.SelectedItem.Text & "', LUOGO_COR= '" & par.PulisciStrSql(Me.txtLocalità.Text) & "',VIA_COR='" & par.PulisciStrSql(Me.txtVia.Text) & "', CIVICO_COR='" & par.PulisciStrSql(Me.txtCivico.Text) & "', CAP_COR='" & Me.txtCap.Text & "' WHERE ID = " & idContratto.Value
                            par.cmd.ExecuteNonQuery()
                            '****************EVENTO PER RAPPORTI UTENZA!!!!!****************
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F59','')"
                            par.cmd.ExecuteNonQuery()

                        Catch EX1 As Oracle.DataAccess.Client.OracleException
                            If EX1.Number = 54 Then
                                'par.OracleConn.Close()
                                scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('Il contratto associato, è aperto da un altro utente!Per modificare l\'indirizzo riprovare più tardi!');" _
                                & "</script>"
                                TextBox4.Value = 0

                                'ApriFrmWithDBLock()
                                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                                End If
                            Else
                                par.OracleConn.Close()
                                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                            End If

                        Catch ex As Exception
                            lblErrore.Visible = True
                            lblErrore.Text = ex.Message
                        End Try

                    End If
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    '********************* END CONTROLLO **********************
                    '**********************************************************
                    '**********************************************************
                    '**********************************************************
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_AVVISI SET VISTO = 1 WHERE ID_UI = " & IdUnita.Value & " AND DATA = " & par.AggiustaData(DataEvento.Value) & " AND ID_CONDOMINIO = " & IdCondominio.Value
                    par.cmd.ExecuteNonQuery()

                    TextBox4.Value = 1
                    Me.idContratto.Value = 0
                    Me.IdUnita.Value = 0
                    Me.IdCondominio.Value = 0
                    txtmia.Text = "Nessuna Selezione"
                    DataEvento.Value = 0
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    Cerca()
                Else
                    TextBox4.Value = 2
                    'MESSAGGIO OBBLIGATORIO INTESTATARIO
                    Response.Write("<script>alert('E\' obbligatorio scegliere un intestatario principale!');</script>")
                End If


            Else

                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_UI SET POSIZIONE_BILANCIO= '" & par.PulisciStrSql(Me.txtPosBil.Text) & "' , MIL_PRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Pro.Text), "Null") & " " _
                & " , MIL_ASC = " & par.IfEmpty(par.VirgoleInPunti(Me.txtAsc.Text), "Null") & ", MIL_COMPRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Compro.Text), "Null") & "" _
                & " , MIL_GEST = " & par.IfEmpty(par.VirgoleInPunti(Me.txtMil_Gest.Text), "Null") & ", MIL_RISC = " & par.IfEmpty(par.VirgoleInPunti(Me.txt_Mil_Risc.Text), "Null") & "" _
                & " , NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', ADDEBITO_SINGOLO = " & par.VirgoleInPunti(par.IfEmpty(Me.txtAddebito.Text, "Null")) & ", ID_INTESTARIO = " & par.IfEmpty(Me.cmbIntestatari.SelectedValue.ToString, "Null") & " WHERE COND_UI.ID_UI = " & IdUnita.Value & " AND COND_UI.ID_CONDOMINIO = " & IdCondominio.Value

                par.cmd.ExecuteNonQuery()

                ''****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & IdCondominio.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F34','MILLESIMI INQUILINO CONDOMINIO')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_AVVISI SET VISTO = 1 WHERE ID_UI = " & IdUnita.Value & " AND DATA = " & par.AggiustaData(DataEvento.Value) & ""
                par.cmd.ExecuteNonQuery()

                TextBox4.Value = 1
                Me.idContratto.Value = 0
                Me.IdUnita.Value = 0
                Me.IdCondominio.Value = 0
                txtmia.Text = "Nessuna Selezione"
                DataEvento.Value = 0

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                Cerca()
            End If
        
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub



    Protected Sub DataGridBManager_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridBManager.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGridBManager.CurrentPageIndex = e.NewPageIndex
            Cerca()
        End If

    End Sub
End Class
