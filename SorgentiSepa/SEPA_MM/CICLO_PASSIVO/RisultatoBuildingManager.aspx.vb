
Imports Telerik.Web.UI

Partial Class AMMSEPA_RisultatoBuildingManager
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        'Dim Str As String
        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:290px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"
        'Response.Write(Str)
        'Response.Flush()
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            'BindGrid()

            par.caricaComboTelerik("SELECT id,nome FROM siscom_mi.tab_filiali order by nome", cmbfiliale, "ID", "nome", True)
            par.caricaComboTelerik("select OPERATORI.ID,(OPERATORI.cognome||' '||operatori.nome) as operatore from OPERATORI where id_caf=2 and revoca=0 and cognome is not null ORDER BY OPERATORE ASC", cmbOperatore1, "ID", "OPERATORE", True)
            par.caricaComboTelerik("select OPERATORI.ID,(OPERATORI.cognome||' '||operatori.nome) as operatore from OPERATORI where id_caf=2 and revoca=0 and cognome is not null ORDER BY OPERATORE ASC", cmbOperatore2, "ID", "OPERATORE", True)
            txtInizio1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtInizio2.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub

    Private Sub BindGrid()
        Try
            connData.apri()

            Dim Str As String = ""
            Dim condizioniSQL As String = ""
            Dim bTrovato As Boolean = False
            Dim idOp1 As Integer = 0
            Dim condizioneOp1 As String = ""
            Dim idOp2 As Integer = 0
            Dim condizioneOp2 As String = ""
            Dim codice As String = ""
            Dim sCompara As String = ""
            Dim sValore As String = ""
            Dim condizioneCod As String = ""
            Dim idStruttura As Long = 0
            Dim condizioneIdStr As String = ""

            If Not IsNothing(Request.QueryString("IDOP1")) Then
                idOp1 = Request.QueryString("IDOP1")
                If idOp1 <> -1 Then
                    bTrovato = True
                    condizioneOp1 = " BUILDING_MANAGER_OPERATORI.tipo_operatore=1 and id_operatore=" & idOp1 & ""
                    condizioniSQL = condizioneOp1
                End If
            End If
            If Not IsNothing(Request.QueryString("IDOP2")) Then
                idOp2 = Request.QueryString("IDOP2")
                If idOp2 <> -1 Then
                    condizioneOp2 = " BUILDING_MANAGER_OPERATORI.tipo_operatore=2 and id_operatore=" & idOp2
                    If condizioneOp1 <> "" Then
                        condizioneOp1 = "(" & condizioneOp1
                        condizioniSQL = condizioneOp1
                        condizioneOp2 = " OR " & condizioneOp2 & ")"
                    Else
                        If bTrovato = True Then condizioniSQL = condizioniSQL & " AND"
                    End If
                    bTrovato = True
                    condizioniSQL = condizioniSQL & condizioneOp2
                End If
            End If
            If Not IsNothing(Request.QueryString("COD")) Then
                codice = Request.QueryString("COD")
                If codice <> "" Then
                    If bTrovato = True Then condizioniSQL = condizioniSQL & " AND"
                    bTrovato = True
                    sCompara = " = "
                    sValore = codice.ToUpper
                    If InStr(sValore, "*") Then
                        sCompara = " LIKE "
                        Call par.ConvertiJolly(sValore)
                    Else
                        sCompara = " = "
                    End If
                    condizioneCod = " SISCOM_MI.BUILDING_MANAGER.codice " & sCompara & " '" & par.PulisciStrSql(sValore) & "'"
                    condizioniSQL = condizioniSQL & condizioneCod
                End If
            End If
            If Not IsNothing(Request.QueryString("FILIALE")) Then
                idStruttura = Request.QueryString("FILIALE")
                If idStruttura <> -1 Then
                    If bTrovato = True Then condizioniSQL = condizioniSQL & " AND"
                    bTrovato = True
                    condizioneIdStr = " id_struttura=" & idStruttura
                    condizioniSQL = condizioniSQL & condizioneIdStr
                End If
            End If
            If condizioniSQL <> "" Then condizioniSQL = " and " & condizioniSQL
            'Str = "select building_manager.id,building_manager_operatori.ID AS ID_OP,building_manager.codice,(OPERATORI.cognome||' '||operatori.nome) as operatore,building_manager_operatori.tipo_operatore,(select nome from siscom_mi.tab_filiali where tab_filiali.id=building_manager.id_struttura) as filiale, TO_CHAR(TO_DATE(INIZIO_VALIDITA,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO_VALIDITA, TO_CHAR(TO_DATE(FINE_VALIDITA,'YYYYmmdd'),'DD/MM/YYYY') AS FINE_VALIDITA, " _
            '    & " case when building_manager_operatori.tipo_operatore=1 then operatori.operatore end as operatore1,case when building_manager_operatori.tipo_operatore=2 then operatori.operatore end as operatore2 from siscom_mi.building_manager,siscom_mi.building_manager_operatori,operatori where " _
            '    & " siscom_mi.building_manager.id=siscom_mi.building_manager_operatori.id_bm(+) and operatori.id(+)=building_manager_operatori.id_operatore " & condizioniSQL & " ORDER BY filiale, building_manager.CODICE ASC,building_manager_operatori.tipo_operatore asc,INIZIO_VALIDITA DESC"
            Str = "SELECT building_manager.id,building_manager.codice,(select nome from siscom_mi.tab_filiali where tab_filiali.id=building_manager.id_struttura) as filiale," _
                & " (select (OPERATORI.cognome||' '||operatori.nome) as operatore from operatori,siscom_mi.building_manager_operatori where operatori.id=building_manager_operatori.id_operatore and building_manager_operatori.id_bm=building_manager.id" _
                & " AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '19000101') <= TO_CHAR (SYSDATE, 'YYYYMMDD') and building_manager_operatori.tipo_operatore=1) as operatore1," _
                & " (select (OPERATORI.cognome||' '||operatori.nome) as operatore from operatori,siscom_mi.building_manager_operatori where operatori.id=building_manager_operatori.id_operatore and building_manager_operatori.id_bm=building_manager.id" _
                & " AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '19000101') <= TO_CHAR (SYSDATE, 'YYYYMMDD') and building_manager_operatori.tipo_operatore=2 ) as operatore2" _
                & " FROM siscom_mi.building_manager" _
                & " ORDER BY filiale asc,building_manager.CODICE ASC"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            DataGridBM.DataSource = dt
            DataGridBM.DataBind()
            'lblRisultati.Text = "- Trovati: " & dt.Rows.Count & " risultati"
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: RisultatoBuildingManager - BindGrid - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    'Protected Sub DataGridBM_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridBM.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Building Manager " & Replace(e.Item.Cells(1).Text, "'", "\'") & " - " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('hiddenID').value='" & e.Item.Cells(0).Text & "';")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Building Manager " & Replace(e.Item.Cells(1).Text, "'", "\'") & " - " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('hiddenID').value='" & e.Item.Cells(0).Text & "';")

    '    End If
    'End Sub

    Protected Sub btnSalvaBM_Click(sender As Object, e As EventArgs) Handles btnSalvaBM.Click
        Try
            Dim CodComune As String = ""
            Dim idBMnuovo As Long = 0
            err.Value = "0"
            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then
                connData.apri(True)

                If par.IfEmpty(Me.txtCodice.Text, "Null") <> "Null" And cmbfiliale.SelectedValue <> -1 Then
                    par.cmd.CommandText = "select * FROM SISCOM_MI.building_manager WHERE codice='" & par.PulisciStrSql(txtCodice.Text.ToUpper) & "' and id_struttura=" & cmbfiliale.SelectedValue
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        'Response.Write("<script>alert('Attenzione, Elemento già inserito!');</script>")
                        RadWindowManager1.RadAlert("Attenzione, Elemento già inserito!", 300, 150, "Attenzione", "", "null")

                        err.Value = "1"
                        txtCodice.Text = ""
                        myReader.Close()
                        connData.chiudi(False)
                        Exit Sub
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "select siscom_mi.seq_BUILDING_MANAGER.nextval from dual"
                    idBMnuovo = par.cmd.ExecuteScalar

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BUILDING_MANAGER (ID, ID_STRUTTURA, CODICE) VALUES (" & idBMnuovo & "," & par.insDbValue(cmbfiliale.SelectedValue, False) & "," & par.insDbValue(txtCodice.Text.ToUpper, True) & ")"
                    par.cmd.ExecuteNonQuery()

                    If cmbOperatore1.SelectedValue <> -1 Or cmbOperatore2.SelectedValue <> -1 Then
                        If cmbOperatore1.SelectedValue <> -1 Then
                            If txtInizio1.Text = "" Then
                                connData.chiudi(False)
                                err.Value = "1"
                                ' Response.Write("<script>alert('Attenzione, data inizio validità obbligatoria!');</script>")
                                RadWindowManager1.RadAlert("Attenzione, data inizio validità obbligatoria!", 300, 150, "Attenzione", "", "null")

                                Exit Sub
                            Else

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BUILDING_MANAGER_OPERATORI (ID, ID_BM, TIPO_OPERATORE, ID_OPERATORE, INIZIO_VALIDITA) " _
                                    & " VALUES (siscom_mi.seq_BUILDING_MANAGER_OPERATORI.nextval," & idBMnuovo & ",1," & par.insDbValue(cmbOperatore1.SelectedValue, False) & "," & par.insDbValue(txtInizio1.Text, True, True) & ")"
                                par.cmd.ExecuteNonQuery()

                            End If
                        End If
                        If cmbOperatore2.SelectedValue <> -1 Then
                            If txtInizio2.Text = "" Then
                                connData.chiudi(False)
                                err.Value = "1"
                                '  Response.Write("<script>alert('Attenzione, data inizio validità obbligatoria!');</script>")
                                RadWindowManager1.RadAlert("Attenzione, data inizio validità obbligatoria!", 300, 150, "Attenzione", "", "null")

                                Exit Sub
                            Else

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BUILDING_MANAGER_OPERATORI (ID, ID_BM, TIPO_OPERATORE, ID_OPERATORE, INIZIO_VALIDITA) " _
                                    & " VALUES (siscom_mi.seq_BUILDING_MANAGER_OPERATORI.nextval," & idBMnuovo & ",2," & par.insDbValue(cmbOperatore2.SelectedValue, False) & "," & par.insDbValue(txtInizio2.Text, True, True) & ")"
                                par.cmd.ExecuteNonQuery()

                            End If
                        End If
                    End If

                    Me.TextBox1.Value = "0"
                    lblErrore.Visible = False
                    hiddenID.Value = ""
                    connData.chiudi(True)
                    '  Response.Write("<script>alert('');</script>")
                    RadWindowManager1.RadAlert("Operazione effettuata!", 300, 150, "Attenzione", "", "null")
                    DataGridBM.Rebind()
                Else
                    connData.chiudi(False)
                    err.Value = "1"
                    RadWindowManager1.RadAlert("Attenzione, Campi Obbligatori!", 300, 150, "Attenzione", "", "null")
                    'Response.Write("<script>alert('Attenzione, Campi Obbligatori!');</script>")

                End If
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub Update()

        If par.IfEmpty(Me.txtCodice.Text, "Null") <> "Null" And cmbfiliale.SelectedValue <> -1 Then
            connData.apri(True)

            par.cmd.CommandText = "select * FROM SISCOM_MI.BUILDING_MANAGER WHERE id<>" & hiddenID.Value & " and CODICE='" & par.PulisciStrSql(txtCodice.Text.ToUpper) & "' and ID_STRUTTURA=" & cmbfiliale.SelectedValue
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                ' Response.Write("<script>alert('Valore già inserito!');</script>")
                RadWindowManager1.RadAlert("Valore già inserito", 300, 150, "Attenzione", "", "null")

                myReader.Close()
                connData.chiudi(False)
                Exit Sub
            End If
            myReader.Close()

            par.cmd.CommandText = "UPDATE SISCOM_MI.BUILDING_MANAGER SET CODICE='" & par.PulisciStrSql(txtCodice.Text.ToUpper) & "',ID_STRUTTURA=" & cmbfiliale.SelectedValue & " WHERE ID=" & hiddenID.Value
            par.cmd.ExecuteNonQuery()

            Dim idBMop1 As Integer = 0
            Dim idOperatore1 As Integer = 0
            Dim idBMop2 As Integer = 0
            Dim idOperatore2 As Integer = 0
            Dim tipoOperatore1 As Boolean = False
            Dim tipoOperatore2 As Boolean = False
            Dim dataInizioVal1 As String = ""
            Dim dataInizioVal2 As String = ""
            par.cmd.CommandText = "SELECT * from SISCOM_MI.BUILDING_MANAGER_OPERATORI where NVL (inizio_validita, '19000101') <= TO_CHAR (SYSDATE, 'YYYYMMDD') and NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') and ID_BM=" & hiddenID.Value
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da1.Fill(dt)
            da1.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    If par.IfNull(row.Item("tipo_operatore"), 0) = 1 Then
                        idOperatore1 = par.IfNull(row.Item("id_operatore"), 0)
                        idBMop1 = par.IfNull(row.Item("id"), 0)
                        dataInizioVal1 = par.IfNull(row.Item("inizio_validita"), "19000101")
                        tipoOperatore1 = True
                    End If
                    If par.IfNull(row.Item("tipo_operatore"), 0) = 2 Then
                        idOperatore2 = par.IfNull(row.Item("id_operatore"), 0)
                        idBMop2 = par.IfNull(row.Item("id"), 0)
                        dataInizioVal2 = par.IfNull(row.Item("inizio_validita"), "19000101")
                        tipoOperatore2 = True
                    End If
                Next
            End If

            If cmbOperatore1.SelectedValue <> -1 Or cmbOperatore2.SelectedValue <> -1 Then
                If cmbOperatore1.SelectedValue <> -1 Then
                    If txtInizio1.Text = "" Then
                        connData.chiudi(False)
                        err.Value = "1"
                        ' Response.Write("<script>alert('');</script>")
                        RadWindowManager1.RadAlert("Attenzione, data inizio validità obbligatoria!", 300, 150, "Attenzione", "", "null")

                        Exit Sub
                    Else
                        If idBMop1 <> 0 Then
                            If par.AggiustaData(txtInizio1.Text) < dataInizioVal1 Then
                                connData.chiudi(False)
                                err.Value = "1"
                                ' Response.Write("<script>alert('Attenzione, la data non può essere precedente a quella già presente a sistema!');</script>")
                                RadWindowManager1.RadAlert("Attenzione, la data non può essere precedente a quella già presente a sistema!", 300, 150, "Attenzione", "", "null")

                                Exit Sub
                            Else
                                If idOperatore1 <> cmbOperatore1.SelectedValue Then
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.BUILDING_MANAGER_OPERATORI SET FINE_VALIDITA = " & par.insDbValue(DateAdd(DateInterval.Day, -1, CDate(par.IfEmpty(txtInizio1.Text, Format(Now, "dd/MM/yyyy")))), True, True) & " WHERE ID =" & idBMop1
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BUILDING_MANAGER_OPERATORI (ID, ID_BM, TIPO_OPERATORE, ID_OPERATORE, INIZIO_VALIDITA) " _
                                    & " VALUES (siscom_mi.seq_BUILDING_MANAGER_OPERATORI.nextval," & hiddenID.Value & ",1," & par.insDbValue(cmbOperatore1.SelectedValue, False) & "," & par.insDbValue(txtInizio1.Text, True, True) & ")"
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.BUILDING_MANAGER_OPERATORI SET INIZIO_VALIDITA = " & par.insDbValue(txtInizio1.Text, True, True) & " WHERE ID =" & idBMop1
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                        Else

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BUILDING_MANAGER_OPERATORI (ID, ID_BM, TIPO_OPERATORE, ID_OPERATORE, INIZIO_VALIDITA) " _
                                & " VALUES (siscom_mi.seq_BUILDING_MANAGER_OPERATORI.nextval," & hiddenID.Value & ",1," & par.insDbValue(cmbOperatore1.SelectedValue, False) & "," & par.insDbValue(txtInizio1.Text, True, True) & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                    End If

                End If

                If cmbOperatore2.SelectedValue <> -1 Then
                    If txtInizio2.Text = "" Then
                        connData.chiudi(False)
                        err.Value = "1"
                        'Response.Write("<script>alert('Attenzione, data inizio validità obbligatoria!');</script>")
                        RadWindowManager1.RadAlert("Attenzione, data inizio validità obbligatoria!", 300, 150, "Attenzione", "", "null")

                        Exit Sub
                    Else
                        If idBMop2 <> 0 Then
                            If par.AggiustaData(txtInizio2.Text) < dataInizioVal2 Then
                                connData.chiudi(False)
                                err.Value = "1"
                                'Response.Write("<script>alert('');</script>")
                                RadWindowManager1.RadAlert("Attenzione, la data non può essere precedente a quella già presente a sistema!", 300, 150, "Attenzione", "", "null")

                                Exit Sub
                            Else
                                If idOperatore2 <> cmbOperatore2.SelectedValue Then
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.BUILDING_MANAGER_OPERATORI SET FINE_VALIDITA = " & par.insDbValue(DateAdd(DateInterval.Day, -1, CDate(par.IfEmpty(txtInizio2.Text, Format(Now, "dd/MM/yyyy")))), True, True) & " WHERE ID =" & idBMop2
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BUILDING_MANAGER_OPERATORI (ID, ID_BM, TIPO_OPERATORE, ID_OPERATORE, INIZIO_VALIDITA) " _
                                    & " VALUES (siscom_mi.seq_BUILDING_MANAGER_OPERATORI.nextval," & hiddenID.Value & ",2," & par.insDbValue(cmbOperatore2.SelectedValue, False) & "," & par.insDbValue(txtInizio2.Text, True, True) & ")"
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.BUILDING_MANAGER_OPERATORI SET INIZIO_VALIDITA = " & par.insDbValue(txtInizio2.Text, True, True) & " WHERE ID =" & idBMop2
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                        Else

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BUILDING_MANAGER_OPERATORI (ID, ID_BM, TIPO_OPERATORE, ID_OPERATORE, INIZIO_VALIDITA) " _
                                & " VALUES (siscom_mi.seq_BUILDING_MANAGER_OPERATORI.nextval," & hiddenID.Value & ",2," & par.insDbValue(cmbOperatore2.SelectedValue, False) & "," & par.insDbValue(txtInizio2.Text, True, True) & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                    End If
                Else
                    If Not String.IsNullOrEmpty(idOperatore2) Then
                        EliminaBuildingManagerOperatore(idOperatore2)
                    End If
                End If
            End If

            connData.chiudi(True)
            'Response.Write("<script>alert('Operazione effettuata!');</script>")
            RadWindowManager1.RadAlert("Operazione effettuata!", 300, 150, "Attenzione", "", "null")
            DataGridBM.Rebind()

            Me.TextBox1.Value = "0"
            txtCodice.Text = ""
            Me.hiddenID.Value = ""
        Else
            connData.chiudi(False)
            'Response.Write("<script>alert('Attenzione, Campi Obbligatori!');</script>")
            RadWindowManager1.RadAlert("Attenzione, campi obbligatori!", 300, 150, "Attenzione", "", "null")

        End If

    End Sub

    Protected Sub ImgModificaBM_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgModificaBM.Click
        Try
            If hiddenID.Value <> "" Then
                connData.apri()
                par.cmd.CommandText = "SELECT BUILDING_MANAGER.codice,BUILDING_MANAGER.id_struttura,BUILDING_MANAGER_OPERATORI.*,operatori.operatore " _
                    & "FROM SISCOM_MI.BUILDING_MANAGER,SISCOM_MI.BUILDING_MANAGER_OPERATORI,operatori WHERE BUILDING_MANAGER.ID=BUILDING_MANAGER_OPERATORI.ID_BM(+) AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '19000101') <= TO_CHAR (SYSDATE, 'YYYYMMDD') and operatori.id(+)=building_manager_operatori.id_operatore AND BUILDING_MANAGER.ID = " & hiddenID.Value

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    For Each row As Data.DataRow In dt.Rows
                        txtCodice.Text = par.IfNull(row.Item("CODICE"), "")
                        cmbfiliale.SelectedValue = par.IfNull(row.Item("ID_STRUTTURA"), -1)
                        If par.IfNull(row.Item("tipo_operatore"), "") = "1" Then
                            cmbOperatore1.SelectedValue = par.IfNull(row.Item("ID_OPERATORE"), -1)
                            txtInizio1.Text = par.FormattaData(par.IfNull(row.Item("INIZIO_VALIDITA"), ""))

                        End If
                        If par.IfNull(row.Item("tipo_operatore"), "") = "2" Then
                            cmbOperatore2.SelectedValue = par.IfNull(row.Item("ID_OPERATORE"), -1)
                            txtInizio2.Text = par.FormattaData(par.IfNull(row.Item("INIZIO_VALIDITA"), ""))

                        End If
                    Next
                End If
                connData.chiudi()
                Me.TextBox1.Value = "2"
                Dim script As String = "function f(){$find(""" + RadWindowBM.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            Else
                Me.TextBox1.Value = "0"
                'Response.Write("<script>alert('Attenzione, Nessun elemento selezionato!');</script>")
                RadWindowManager1.RadAlert("Nessun elemento selezionato!", 300, 150, "Attenzione", "", "null")

            End If
        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        'Try
        '    connData.apri()

        '    par.cmd.CommandText = "select id from siscom_mi.edifici where id_bm=" & hiddenID.Value
        '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If myReader.Read Then
        '        ' Response.Write("<script>alert('');</script>")
        '        RadWindowManager1.RadAlert("Impossibile eliminare! Building Manager associato a complesso.", 300, 150, "Attenzione", "", "null")
        '        myReader.Close()

        '        connData.chiudi(False)
        '        Exit Sub
        '    Else
        '        par.cmd.CommandText = "DELETE FROM SISCOM_MI.BUILDING_MANAGER WHERE ID = " & hiddenID.Value
        '        par.cmd.ExecuteNonQuery()

        '        connData.chiudi()
        '        Me.TextBox1.Value = "0"
        '        Me.hiddenID.Value = ""
        '        'Response.Write("<script>alert('Operazione effettuata!');</script>")
        '        RadWindowManager1.RadAlert("Operazione effettuata!", 300, 150, "Attenzione", "", "null")

        '        DataGridBM.Rebind()
        '    End If
        '    myReader.Close()



        'Catch EX1 As Data.OracleClient.OracleException
        '    connData.chiudi()
        '    Me.lblErrore.Visible = True
        '    If EX1.Code = 2292 Then
        '        lblErrore.Text = "Elemento in uso. Non è possibile eliminare!"
        '    Else
        '        lblErrore.Text = EX1.Message
        '    End If
        'Catch ex As Exception
        '    connData.chiudi()
        '    Me.lblErrore.Visible = True
        '    lblErrore.Text = ex.Message
        'End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home_ncp.aspx""</script>")
    End Sub

    Private Sub DataGridBM_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGridBM.NeedDataSource

        Try
            connData.apri()

            Dim Str As String = ""
            Dim condizioniSQL As String = ""
            Dim bTrovato As Boolean = False
            Dim idOp1 As Integer = 0
            Dim condizioneOp1 As String = ""
            Dim idOp2 As Integer = 0
            Dim condizioneOp2 As String = ""
            Dim codice As String = ""
            Dim sCompara As String = ""
            Dim sValore As String = ""
            Dim condizioneCod As String = ""
            Dim idStruttura As Long = 0
            Dim condizioneIdStr As String = ""

            If Not IsNothing(Request.QueryString("IDOP1")) Then
                idOp1 = Request.QueryString("IDOP1")
                If idOp1 <> -1 Then
                    bTrovato = True
                    condizioneOp1 = " BUILDING_MANAGER_OPERATORI.tipo_operatore=1 and id_operatore=" & idOp1 & ""
                    condizioniSQL = condizioneOp1
                End If
            End If
            If Not IsNothing(Request.QueryString("IDOP2")) Then
                idOp2 = Request.QueryString("IDOP2")
                If idOp2 <> -1 Then
                    condizioneOp2 = " BUILDING_MANAGER_OPERATORI.tipo_operatore=2 and id_operatore=" & idOp2
                    If condizioneOp1 <> "" Then
                        condizioneOp1 = "(" & condizioneOp1
                        condizioniSQL = condizioneOp1
                        condizioneOp2 = " OR " & condizioneOp2 & ")"
                    Else
                        If bTrovato = True Then condizioniSQL = condizioniSQL & " AND"
                    End If
                    bTrovato = True
                    condizioniSQL = condizioniSQL & condizioneOp2
                End If
            End If
            If Not IsNothing(Request.QueryString("COD")) Then
                codice = Request.QueryString("COD")
                If codice <> "" Then
                    If bTrovato = True Then condizioniSQL = condizioniSQL & " AND"
                    bTrovato = True
                    sCompara = " = "
                    sValore = codice.ToUpper
                    If InStr(sValore, "*") Then
                        sCompara = " LIKE "
                        Call par.ConvertiJolly(sValore)
                    Else
                        sCompara = " = "
                    End If
                    condizioneCod = " SISCOM_MI.BUILDING_MANAGER.codice " & sCompara & " '" & par.PulisciStrSql(sValore) & "'"
                    condizioniSQL = condizioniSQL & condizioneCod
                End If
            End If
            If Not IsNothing(Request.QueryString("FILIALE")) Then
                idStruttura = Request.QueryString("FILIALE")
                If idStruttura <> -1 Then
                    If bTrovato = True Then condizioniSQL = condizioniSQL & " AND"
                    bTrovato = True
                    condizioneIdStr = " id_struttura=" & idStruttura
                    condizioniSQL = condizioniSQL & condizioneIdStr
                End If
            End If
            If condizioniSQL <> "" Then condizioniSQL = " and " & condizioniSQL
            'Str = "select building_manager.id,building_manager_operatori.ID AS ID_OP,building_manager.codice,(OPERATORI.cognome||' '||operatori.nome) as operatore,building_manager_operatori.tipo_operatore,(select nome from siscom_mi.tab_filiali where tab_filiali.id=building_manager.id_struttura) as filiale, TO_CHAR(TO_DATE(INIZIO_VALIDITA,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO_VALIDITA, TO_CHAR(TO_DATE(FINE_VALIDITA,'YYYYmmdd'),'DD/MM/YYYY') AS FINE_VALIDITA, " _
            '    & " case when building_manager_operatori.tipo_operatore=1 then operatori.operatore end as operatore1,case when building_manager_operatori.tipo_operatore=2 then operatori.operatore end as operatore2 from siscom_mi.building_manager,siscom_mi.building_manager_operatori,operatori where " _
            '    & " siscom_mi.building_manager.id=siscom_mi.building_manager_operatori.id_bm(+) and operatori.id(+)=building_manager_operatori.id_operatore " & condizioniSQL & " ORDER BY filiale, building_manager.CODICE ASC,building_manager_operatori.tipo_operatore asc,INIZIO_VALIDITA DESC"
            Str = "SELECT building_manager.id,building_manager.codice,(select nome from siscom_mi.tab_filiali where tab_filiali.id=building_manager.id_struttura) as filiale," _
                & " (select (OPERATORI.cognome||' '||operatori.nome) as operatore from operatori,siscom_mi.building_manager_operatori where operatori.id=building_manager_operatori.id_operatore and building_manager_operatori.id_bm=building_manager.id" _
                & " AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '19000101') <= TO_CHAR (SYSDATE, 'YYYYMMDD') and building_manager_operatori.tipo_operatore=1) as operatore1," _
                & " (select (OPERATORI.cognome||' '||operatori.nome) as operatore from operatori,siscom_mi.building_manager_operatori where operatori.id=building_manager_operatori.id_operatore and building_manager_operatori.id_bm=building_manager.id" _
                & " AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '19000101') <= TO_CHAR (SYSDATE, 'YYYYMMDD') and building_manager_operatori.tipo_operatore=2 ) as operatore2" _
                & " FROM siscom_mi.building_manager" _
                & " ORDER BY filiale asc,building_manager.CODICE ASC"
            par.cmd.CommandText = Str
            Dim dt As Data.DataTable = par.getDataTableGrid(Str)
            TryCast(sender, RadGrid).DataSource = dt
            'lblRisultati.Text = "- Trovati: " & dt.Rows.Count & " risultati"
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: RisultatoBuildingManager - BindGrid - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub DataGridBM_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles DataGridBM.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il Building Manager " & Replace(dataItem("CODICE").Text, "'", "\'") & " - " & Replace(dataItem("FILIALE").Text, "'", "\'") & "';document.getElementById('hiddenID').value='" & dataItem("ID").Text & "';")
            e.Item.Attributes.Add("onDblClick", "document.getElementById('ImgModificaBM').click();")
        End If
    End Sub

    Private Sub DataGridBM_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles DataGridBM.ItemCommand
        Try
            Select Case e.CommandName
                Case "myEdit"
                    ApriBM()
                Case "Delete"
                    Try
                        connData.apri()

                        par.cmd.CommandText = "select id from siscom_mi.edifici where id_bm=" & hiddenID.Value
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader.Read Then
                            ' Response.Write("<script>alert('');</script>")
                            RadWindowManager1.RadAlert("Impossibile eliminare! Building Manager associato a complesso.", 300, 150, "Attenzione", "", "null")
                            myReader.Close()

                            connData.chiudi(False)
                            Exit Sub
                        Else
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.BUILDING_MANAGER WHERE ID = " & hiddenID.Value
                            par.cmd.ExecuteNonQuery()

                            connData.chiudi()
                            Me.TextBox1.Value = "0"
                            Me.hiddenID.Value = ""
                            'Response.Write("<script>alert('Operazione effettuata!');</script>")
                            RadWindowManager1.RadAlert("Operazione effettuata!", 300, 150, "Attenzione", "", "null")

                            DataGridBM.Rebind()
                        End If
                        myReader.Close()



                    Catch EX1 As Data.OracleClient.OracleException
                        connData.chiudi()
                        Me.lblErrore.Visible = True
                        If EX1.Code = 2292 Then
                            lblErrore.Text = "Elemento in uso. Non è possibile eliminare!"
                        Else
                            lblErrore.Text = EX1.Message
                        End If
                    Catch ex As Exception
                        connData.chiudi()
                        Me.lblErrore.Visible = True
                        lblErrore.Text = ex.Message
                    End Try
            End Select

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: RisultatoBuildingManager - DataGridBM_ItemCommand - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ApriBM()

        Try
            If hiddenID.Value <> "" Then
                connData.apri()
                par.cmd.CommandText = "SELECT BUILDING_MANAGER.codice,BUILDING_MANAGER.id_struttura,BUILDING_MANAGER_OPERATORI.*,operatori.operatore " _
                    & "FROM SISCOM_MI.BUILDING_MANAGER,SISCOM_MI.BUILDING_MANAGER_OPERATORI,operatori WHERE BUILDING_MANAGER.ID=BUILDING_MANAGER_OPERATORI.ID_BM(+) AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '19000101') <= TO_CHAR (SYSDATE, 'YYYYMMDD') and operatori.id(+)=building_manager_operatori.id_operatore AND BUILDING_MANAGER.ID = " & hiddenID.Value

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    For Each row As Data.DataRow In dt.Rows
                        txtCodice.Text = par.IfNull(row.Item("CODICE"), "")
                        cmbfiliale.SelectedValue = par.IfNull(row.Item("ID_STRUTTURA"), -1)
                        If par.IfNull(row.Item("tipo_operatore"), "") = "1" Then
                            cmbOperatore1.SelectedValue = par.IfNull(row.Item("ID_OPERATORE"), -1)
                            txtInizio1.Text = par.FormattaData(par.IfNull(row.Item("INIZIO_VALIDITA"), ""))

                        End If
                        If par.IfNull(row.Item("tipo_operatore"), "") = "2" Then
                            cmbOperatore2.SelectedValue = par.IfNull(row.Item("ID_OPERATORE"), -1)
                            txtInizio2.Text = par.FormattaData(par.IfNull(row.Item("INIZIO_VALIDITA"), ""))

                        End If
                    Next
                End If
                connData.chiudi()
                Me.TextBox1.Value = "2"
                Dim script As String = "function f(){$find(""" + RadWindowBM.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            Else
                Me.TextBox1.Value = "0"
                Response.Write("<script>alert('Attenzione, Nessun elemento selezionato!');</script>")
            End If
        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try

    End Sub

    Private Sub btnModifica_Click(sender As Object, e As EventArgs) Handles btnModifica.Click
        ApriBM()
    End Sub

    Private Sub btnEliminaBM_Click(sender As Object, e As EventArgs) Handles btnEliminaBM.Click
        EliminaBuildingManager()

    End Sub

    Private Sub EliminaBuildingManager(Optional ByVal idDaEliminare As String = "-1")
        Try
            connData.apri()
            If idDaEliminare <> "-1" Then
                hiddenID.Value = idDaEliminare
            End If
            par.cmd.CommandText = "select id from siscom_mi.edifici where id_bm=" & hiddenID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                ' Response.Write("<script>alert('');</script>")
                RadWindowManager1.RadAlert("Impossibile eliminare! Building Manager associato a complesso.", 300, 150, "Attenzione", "", "null")
                myReader.Close()

                connData.chiudi(False)
                Exit Sub
            Else
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.BUILDING_MANAGER WHERE ID = " & hiddenID.Value
                par.cmd.ExecuteNonQuery()

                connData.chiudi()
                Me.TextBox1.Value = "0"
                Me.hiddenID.Value = ""
                'Response.Write("<script>alert('Operazione effettuata!');</script>")
                RadWindowManager1.RadAlert("Operazione effettuata!", 300, 150, "Attenzione", "", "null")

                DataGridBM.Rebind()
            End If
            myReader.Close()



        Catch EX1 As Data.OracleClient.OracleException
            connData.chiudi()
            Me.lblErrore.Visible = True
            If EX1.Code = 2292 Then
                lblErrore.Text = "Elemento in uso. Non è possibile eliminare!"
            Else
                lblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Private Sub EliminaBuildingManagerOperatore(ByVal idDaEliminare)
        Try
            connData.apri()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = " & hiddenID.Value _
                    & " AND ID_OPERATORE = " & idDaEliminare
            par.cmd.ExecuteNonQuery()



        Catch EX1 As Data.OracleClient.OracleException
            connData.chiudi()
            Me.lblErrore.Visible = True
            If EX1.Code = 2292 Then
                lblErrore.Text = "Elemento in uso. Non è possibile eliminare!"
            Else
                lblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            connData.chiudi()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub
End Class
