
Partial Class ANAUT_Dett_Reddito
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        'SettaFunzioniJS()
        If IsPostBack = False Then
            lIdDichiarazione = Request.QueryString("IDDICH")
            inModifica = Request.QueryString("MOD")
            idRedd = Request.QueryString("IDREDD")
            vIdConnessione = Request.QueryString("IDCONN")

            RicavaDateValidita()

            If inModifica = "0" Then
                par.caricaComboBox("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & lIdDichiarazione & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", True)

                CaricaDettReddDipend()
                CaricaDettReddAuton()
                CaricaDettReddPens()
                CaricaDettReddPensEs()
                CaricaDettReddNoIsee()
                SettaFunzioniJS()
            Else
                If cmbComponente.SelectedValue = "-1" Then
                    chkTipiReddito.Enabled = False
                Else
                    chkTipiReddito.Enabled = True
                End If
                cmbComponente.Enabled = False
                par.caricaComboBox("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & lIdDichiarazione & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", True)

                CaricaCompSelezion()

                SettaFunzioniJS()
            End If
        End If
        'Dim CTRL As Control
        'For Each CTRL In Me.Controls
        '    If TypeOf CTRL Is TextBox Then
        '        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='2';document.getElementById('H1').value='0';")
        '    ElseIf TypeOf CTRL Is DropDownList Then
        '        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='2';document.getElementById('H1').value='0';")
        '    ElseIf TypeOf CTRL Is CheckBox Then
        '        DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='2';document.getElementById('H1').value='0';")
        '    End If
        'Next
        SettaControlModifiche(Me)
        SettaFunzioniJS()

    End Sub


    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBoxList Then
                DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
    End Sub

    Public Property inModifica() As String
        Get
            If Not (ViewState("par_inModifica") Is Nothing) Then
                Return CStr(ViewState("par_inModifica"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_inModifica") = value
        End Set
    End Property

    Public Property reddPresente() As Boolean
        Get
            If Not (ViewState("par_reddPresente") Is Nothing) Then
                Return CBool(ViewState("par_reddPresente"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_reddPresente") = value
        End Set

    End Property

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property

    Private Sub SettaFunzioniJS()

        chkTipiReddito.Attributes.Add("onclick", "javascript:VisualizzaDiv();")
        txtTotImporto.Attributes.Add("ontextchanged", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);SostPuntVirg(event, this);SvuotaTextBox();")
        txtImportoA.Attributes.Add("ontextchanged", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);SostPuntVirg(event, this);SvuotaTextBox();")
        txtImportoP.Attributes.Add("ontextchanged", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);SostPuntVirg(event, this);SvuotaTextBox();")
        txtImportoPes.Attributes.Add("ontextchanged", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);SostPuntVirg(event, this);SvuotaTextBox();")
        txtImportoNoIsee.Attributes.Add("ontextchanged", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);SostPuntVirg(event, this);SvuotaTextBox();")


        Dim i As Integer = 0
        Dim di As DataGridItem
        For i = 0 To DataGridDipend.Items.Count - 1
            di = Me.DataGridDipend.Items(i)

            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);CalcolaTotale(this);")
            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
        Next

        For i = 0 To DataGridAuton.Items.Count - 1
            di = Me.DataGridAuton.Items(i)

            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);CalcolaTotale(this);")
            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
        Next

        For i = 0 To DataGridPens.Items.Count - 1
            di = Me.DataGridPens.Items(i)

            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);CalcolaTotale(this);")
            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
        Next

        For i = 0 To DataGridPensEs.Items.Count - 1
            di = Me.DataGridPensEs.Items(i)

            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);CalcolaTotale(this);")
            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
        Next

        For i = 0 To DataGridNoIsee.Items.Count - 1
            di = Me.DataGridNoIsee.Items(i)

            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers'); AutoDecimal2(this);CalcolaTotale(this);")
            CType(di.Cells(3).FindControl("txtImporto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event, this);")
        Next

    End Sub

    Public Property valoreDipend() As Decimal
        Get
            If Not (ViewState("par_valoreDipend") Is Nothing) Then
                Return CDec(ViewState("par_valoreDipend"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_valoreDipend") = value
        End Set

    End Property

    Public Property valoreAuton() As Decimal
        Get
            If Not (ViewState("par_valoreAuton") Is Nothing) Then
                Return CDec(ViewState("par_valoreAuton"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_valoreAuton") = value
        End Set

    End Property

    Public Property valorePens() As Decimal
        Get
            If Not (ViewState("par_valorePens") Is Nothing) Then
                Return CDec(ViewState("par_valorePens"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_valorePens") = value
        End Set

    End Property

    Public Property valorePensEs() As Decimal
        Get
            If Not (ViewState("par_valorePensEs") Is Nothing) Then
                Return CDec(ViewState("par_valorePensEs"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_valorePensEs") = value
        End Set

    End Property

    Public Property valoreNoIsee() As Decimal
        Get
            If Not (ViewState("par_valoreNoIsee") Is Nothing) Then
                Return CDec(ViewState("par_valoreNoIsee"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_valoreNoIsee") = value
        End Set

    End Property

    Public Property dataInizioValid() As String
        Get
            If Not (ViewState("par_dataInizioValid") Is Nothing) Then
                Return CStr(ViewState("par_dataInizioValid"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataInizioValid") = value
        End Set
    End Property

    Public Property dataFineValid() As String
        Get
            If Not (ViewState("par_dataFineValid") Is Nothing) Then
                Return CStr(ViewState("par_dataFineValid"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataFineValid") = value
        End Set
    End Property

    Private Sub RicavaDateValidita()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                dataInizioValid = par.IfNull(myReader("DATA_INIZIO_VAL"), "")
                dataFineValid = par.IfNull(myReader("DATA_FINE_VAL"), "")
            End If
            myReader.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>RicavaDateValidita" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaCompSelezion()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.* FROM DOMANDE_REDDITI_VSA,COMP_NUCLEO_VSA WHERE DOMANDE_REDDITI_VSA.ID_COMPONENTE=COMP_NUCLEO_VSA.ID AND DOMANDE_REDDITI_VSA.ID=" & idRedd
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                cmbComponente.SelectedValue = par.IfNull(myReader("ID"), "-1")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID=" & idRedd & " AND ID_DOMANDA=" & lIdDichiarazione
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                If par.IfNull(myReader1("DIPENDENTE"), 0) <> 0 Then
                    dipendente.Value = "1"
                    'chkTipiReddito.SelectedValue = "DIPEND"

                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "DIPEND" Then
                            Items.Selected = True
                        End If
                    Next
                    valoreDipend = par.IfNull(myReader1("DIPENDENTE"), 0)
                    CaricaImportiDIPEND(valoreDipend)
                End If
                If par.IfNull(myReader1("PENSIONE"), 0) <> 0 Then
                    pensione.Value = "1"

                    'chkTipiReddito.SelectedValue = "PENS"

                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "PENS" Then
                            Items.Selected = True
                        End If
                    Next
                    valorePens = par.IfNull(myReader1("PENSIONE"), 0)
                    CaricaImportiPENS(valorePens)
                End If
                If par.IfNull(myReader1("PENS_ESENTE"), 0) <> 0 Then
                    pens_esente.Value = "1"

                    'chkTipiReddito.SelectedValue = "PENS"

                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "PENS2" Then
                            Items.Selected = True
                        End If
                    Next
                    valorePensEs = par.IfNull(myReader1("PENS_ESENTE"), 0)
                    CaricaImportiPENSEs(valorePensEs)
                End If

                If par.IfNull(myReader1("NON_IMPONIBILI"), 0) <> 0 Then
                    pens_esente.Value = "1"
                    'chkTipiReddito.SelectedValue = "AUTON"

                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "PENS2" Then
                            Items.Selected = True
                        End If
                    Next
                    valorePensEs = par.IfNull(myReader1("NON_IMPONIBILI"), 0)
                    CaricaImportiPENSEs(valorePensEs)
                End If

                If par.IfNull(myReader1("AUTONOMO"), 0) <> 0 Then
                    autonomo.Value = "1"
                    'chkTipiReddito.SelectedValue = "AUTON"
                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "AUTON" Then
                            Items.Selected = True
                        End If
                    Next
                    valoreAuton = par.IfNull(myReader1("AUTONOMO"), 0)
                    CaricaImportiAUTON(valoreAuton)
                End If

                If par.IfNull(myReader1("DOM_AG_FAB"), 0) <> 0 Then
                    autonomo.Value = "1"
                    'chkTipiReddito.SelectedValue = "AUTON"
                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "AUTON" Then
                            Items.Selected = True
                        End If
                    Next
                    valoreAuton = par.IfNull(myReader1("DOM_AG_FAB"), 0)
                    CaricaImportiAUTON(valoreAuton)
                End If
                If par.IfNull(myReader1("OCCASIONALI"), 0) <> 0 Then
                    autonomo.Value = "1"
                    'chkTipiReddito.SelectedValue = "AUTON"
                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "AUTON" Then
                            Items.Selected = True
                        End If
                    Next
                    valoreAuton = par.IfNull(myReader1("OCCASIONALI"), 0)
                    CaricaImportiAUTON(valoreAuton)
                End If
                If par.IfNull(myReader1("ONERI"), 0) <> 0 Then
                    'chkTipiReddito.SelectedValue = "NOISEE"
                    noIsee.Value = "1"
                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "NOISEE" Then
                            Items.Selected = True
                        End If
                    Next
                    valoreNoIsee = par.IfNull(myReader1("ONERI"), 0)
                    CaricaImportiNOISEE(valoreNoIsee)
                End If
                If par.IfNull(myReader1("NO_ISEE"), 0) <> 0 Then
                    'chkTipiReddito.SelectedValue = "NOISEE"
                    noIsee.Value = "1"
                    For Each Items As ListItem In chkTipiReddito.Items
                        If Items.Value = "NOISEE" Then
                            Items.Selected = True
                        End If
                    Next
                    valoreNoIsee = par.IfNull(myReader1("NO_ISEE"), 0)
                    CaricaImportiNOISEE(valoreNoIsee)
                End If
                'DIPENDENTE	
                'PENSIONE
                'AUTONOMO
                'NON_IMPONIBILI
                'DOM_AG_FAB
                'OCCASIONALI
                'ONERI

            End While
            myReader1.Close()

            If dipendente.Value = "0" Then
                CaricaDettReddDipend()
            End If
            If pens_esente.Value = "0" Then
                CaricaDettReddPensEs()
            End If
            If pensione.Value = "0" Then
                CaricaDettReddPens()
            End If
            If autonomo.Value = "0" Then
                CaricaDettReddAuton()
            End If
            If noIsee.Value = "0" Then
                CaricaDettReddNoIsee()
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaCompSelezion" & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Public Property idRedd() As Long
        Get
            If Not (ViewState("par_idRedd") Is Nothing) Then
                Return CLng(ViewState("par_idRedd"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idRedd") = value
        End Set

    End Property

    Public Property lIdDichiarazione() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property

    Public Property dtReddDipend() As Data.DataTable
        Get
            If Not (ViewState("dtReddDipend") Is Nothing) Then
                Return ViewState("dtReddDipend")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtReddDipend") = value
        End Set
    End Property

    Public Property dtDipendModif() As Data.DataTable
        Get
            If Not (ViewState("dtDipendModif") Is Nothing) Then
                Return ViewState("dtDipendModif")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtDipendModif") = value
        End Set
    End Property

    Public Property dtAutonModif() As Data.DataTable
        Get
            If Not (ViewState("dtAutonModif") Is Nothing) Then
                Return ViewState("dtAutonModif")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtAutonModif") = value
        End Set
    End Property

    Public Property dtPensModif() As Data.DataTable
        Get
            If Not (ViewState("dtPensModif") Is Nothing) Then
                Return ViewState("dtPensModif")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtPensModif") = value
        End Set
    End Property

    Public Property dtPensEsModif() As Data.DataTable
        Get
            If Not (ViewState("dtPensEsModif") Is Nothing) Then
                Return ViewState("dtPensEsModif")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtPensEsModif") = value
        End Set
    End Property

    Public Property dtNoIseeModif() As Data.DataTable
        Get
            If Not (ViewState("dtNoIseeModif") Is Nothing) Then
                Return ViewState("dtNoIseeModif")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtNoIseeModif") = value
        End Set
    End Property

    Public Property dtReddAuton() As Data.DataTable
        Get
            If Not (ViewState("dtReddAuton") Is Nothing) Then
                Return ViewState("dtReddAuton")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtReddAuton") = value
        End Set
    End Property

    Public Property dtReddPens() As Data.DataTable
        Get
            If Not (ViewState("dtReddPens") Is Nothing) Then
                Return ViewState("dtReddPens")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtReddPens") = value
        End Set
    End Property

    Public Property dtReddPensEs() As Data.DataTable
        Get
            If Not (ViewState("dtReddPensEs") Is Nothing) Then
                Return ViewState("dtReddPensEs")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtReddPensEs") = value
        End Set
    End Property

    Public Property dtReddNoIsee() As Data.DataTable
        Get
            If Not (ViewState("dtReddNoIsee") Is Nothing) Then
                Return ViewState("dtReddNoIsee")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtReddNoIsee") = value
        End Set
    End Property

    Private Sub CaricaDettReddDipend()
        Try
            Dim rowDT As System.Data.DataRow
            Dim importoTotale As Decimal = 0
            dtReddDipend = New Data.DataTable

            dtReddDipend.Columns.Add("id")
            dtReddDipend.Columns.Add("DESCRIZIONE")
            dtReddDipend.Columns.Add("IMPORTO")
            dtReddDipend.Columns.Add("NUM_GG")
            dtReddDipend.Columns.Add("IDIMPORTI")

            Dim condQuery As String = ""

            If dataFineValid < "20120101" Then
                condQuery = condQuery & " WHERE ID<>7"
            End If

            par.cmd.CommandText = "select * from VSA_REDDITI_DIPENDENTE" & condQuery & " ORDER BY ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtReddDipend.NewRow()
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""
                    rowDT.Item("IDIMPORTI") = ""
                    'importoTotale = importoTotale + par.IfNull(row.Item("IMPORTO"), 0)
                    dtReddDipend.Rows.Add(rowDT)
                Next
            Else
                rowDT = dtReddDipend.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("DESCRIZIONE") = "&nbsp"
                rowDT.Item("IMPORTO") = 0
                rowDT.Item("NUM_GG") = "&nbsp"
                rowDT.Item("IDIMPORTI") = ""
                dtReddDipend.Rows.Add(rowDT)
            End If

            'CType(di.Cells(4).FindControl("txtPaga"), TextBox).BackColor = Drawing.Color.DodgerBlue
            'CType(di.Cells(4).FindControl("txtPaga"), TextBox).ForeColor = Drawing.Color.White

            DataGridDipend.DataSource = dtReddDipend
            DataGridDipend.DataBind()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddDipend" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaDettReddAuton()
        Try
            Dim rowDT As System.Data.DataRow
            dtReddAuton = New Data.DataTable
            Dim importoTotale As Decimal = 0
            Dim condQuery As String = ""

            dtReddAuton.Columns.Add("id")
            dtReddAuton.Columns.Add("DESCRIZIONE")
            dtReddAuton.Columns.Add("IMPORTO")
            dtReddAuton.Columns.Add("NUM_GG")
            dtReddAuton.Columns.Add("IDIMPORTI")

            If dataInizioValid > "20111231" Then
                condQuery = condQuery & " WHERE ID<>5"
            End If

            par.cmd.CommandText = "select * from VSA_REDDITI_AUTONOMO" & condQuery & " ORDER BY ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtReddAuton.NewRow()
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""
                    rowDT.Item("IDIMPORTI") = ""
                    dtReddAuton.Rows.Add(rowDT)
                Next
            Else
                rowDT = dtReddAuton.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("DESCRIZIONE") = "&nbsp"
                rowDT.Item("IMPORTO") = 0
                rowDT.Item("NUM_GG") = "&nbsp"
                rowDT.Item("IDIMPORTI") = ""
                dtReddAuton.Rows.Add(rowDT)
            End If

            DataGridAuton.DataSource = dtReddAuton
            DataGridAuton.DataBind()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddAuton" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaDettReddPens()
        Try
            Dim rowDT As System.Data.DataRow
            dtReddPens = New Data.DataTable
            Dim importoTotale As Decimal = 0

            dtReddPens.Columns.Add("id")
            dtReddPens.Columns.Add("DESCRIZIONE")
            dtReddPens.Columns.Add("IMPORTO")
            dtReddPens.Columns.Add("NUM_GG")
            dtReddPens.Columns.Add("IDIMPORTI")

            par.cmd.CommandText = "select * from VSA_REDDITI_PENSIONE ORDER BY ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtReddPens.NewRow()
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""
                    rowDT.Item("IDIMPORTI") = ""
                    dtReddPens.Rows.Add(rowDT)
                Next
            Else
                rowDT = dtReddPens.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("DESCRIZIONE") = "&nbsp"
                rowDT.Item("IMPORTO") = 0
                rowDT.Item("NUM_GG") = "&nbsp"
                rowDT.Item("IDIMPORTI") = ""
                dtReddPens.Rows.Add(rowDT)
            End If

            DataGridPens.DataSource = dtReddPens
            DataGridPens.DataBind()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddPens" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaDettReddPensEs()
        Try
            Dim rowDT As System.Data.DataRow
            dtReddPensEs = New Data.DataTable
            Dim importoTotale As Decimal = 0

            dtReddPensEs.Columns.Add("id")
            dtReddPensEs.Columns.Add("DESCRIZIONE")
            dtReddPensEs.Columns.Add("IMPORTO")
            dtReddPensEs.Columns.Add("NUM_GG")
            dtReddPensEs.Columns.Add("IDIMPORTI")

            par.cmd.CommandText = "select * from VSA_REDDITI_PENS_ESENTI ORDER BY ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtReddPensEs.NewRow()
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""
                    rowDT.Item("IDIMPORTI") = ""
                    dtReddPensEs.Rows.Add(rowDT)
                Next
            Else
                rowDT = dtReddPensEs.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("DESCRIZIONE") = "&nbsp"
                rowDT.Item("IMPORTO") = 0
                rowDT.Item("NUM_GG") = "&nbsp"
                rowDT.Item("IDIMPORTI") = ""
                dtReddPensEs.Rows.Add(rowDT)
            End If

            DataGridPensEs.DataSource = dtReddPensEs
            DataGridPensEs.DataBind()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddPensEs" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaDettReddNoIsee()
        Try
            Dim rowDT As System.Data.DataRow
            Dim importoTotale As Decimal = 0
            dtReddNoIsee = New Data.DataTable

            dtReddNoIsee.Columns.Add("id")
            dtReddNoIsee.Columns.Add("DESCRIZIONE")
            dtReddNoIsee.Columns.Add("IMPORTO")
            dtReddNoIsee.Columns.Add("NUM_GG")
            dtReddNoIsee.Columns.Add("IDIMPORTI")

            par.cmd.CommandText = "select * from VSA_REDDITI_NO_ISEE ORDER BY ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtReddNoIsee.NewRow()
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""
                    rowDT.Item("IDIMPORTI") = ""
                    dtReddNoIsee.Rows.Add(rowDT)
                Next
            Else
                rowDT = dtReddNoIsee.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("DESCRIZIONE") = "&nbsp"
                rowDT.Item("IMPORTO") = 0
                rowDT.Item("NUM_GG") = "&nbsp"
                rowDT.Item("IDIMPORTI") = ""
                dtReddNoIsee.Rows.Add(rowDT)
            End If


            'CType(di.Cells(4).FindControl("txtPaga"), TextBox).BackColor = Drawing.Color.DodgerBlue
            'CType(di.Cells(4).FindControl("txtPaga"), TextBox).ForeColor = Drawing.Color.White

            DataGridNoIsee.DataSource = dtReddNoIsee
            DataGridNoIsee.DataBind()


        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddNoIsee" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaImportiDIPEND(ByVal dipendente As Decimal)
        Try
            Dim rowDT As System.Data.DataRow
            Dim importoTotale As Decimal = 0
            dtDipendModif = New Data.DataTable

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            dtDipendModif.Columns.Add("id")
            dtDipendModif.Columns.Add("DESCRIZIONE")
            dtDipendModif.Columns.Add("IMPORTO")
            dtDipendModif.Columns.Add("NUM_GG")
            dtDipendModif.Columns.Add("IDIMPORTI")

            par.cmd.CommandText = "select VSA_REDDITI_DIPENDENTE.*,VSA_REDD_DIPEND_IMPORTI.ID AS IDIMPORTI,VSA_REDD_DIPEND_IMPORTI.NUM_GG,VSA_REDD_DIPEND_IMPORTI.IMPORTO from VSA_REDDITI_DIPENDENTE,VSA_REDD_DIPEND_IMPORTI,DOMANDE_REDDITI_VSA where VSA_REDDITI_DIPENDENTE.id=VSA_REDD_DIPEND_IMPORTI.ID_REDD_DIPENDENTE(+) AND DOMANDE_REDDITI_VSA.ID=VSA_REDD_DIPEND_IMPORTI.ID_REDD_TOT AND ID_COMPONENTE=" & cmbComponente.SelectedValue
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtDipendModif.NewRow()
                    reddPresente = True
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = par.IfNull(row.Item("IMPORTO"), "")
                    rowDT.Item("NUM_GG") = par.IfNull(row.Item("NUM_GG"), "")
                    rowDT.Item("IDIMPORTI") = par.IfNull(row.Item("IDIMPORTI"), "")
                    importoTotale = importoTotale + par.IfNull(row.Item("IMPORTO"), 0)
                    dtDipendModif.Rows.Add(rowDT)
                Next
            Else
                'rowDT = dtDipendModif.NewRow()
                'rowDT.Item("ID") = "-1"
                'rowDT.Item("DESCRIZIONE") = ""
                'rowDT.Item("IMPORTO") = 0
                'rowDT.Item("NUM_GG") = ""
                'dtDipendModif.Rows.Add(rowDT)
            End If

            'If importoTotale <> 0 Then
            Dim Elenco As String = ""
            For Each r1 As Data.DataRow In dtDipendModif.Rows
                Elenco = Elenco & r1.Item("ID") & ","
            Next

            If Elenco <> "" Then
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
            Else
                Elenco = "-1"
            End If

            par.cmd.CommandText = "select * from VSA_REDDITI_DIPENDENTE where id not in " & Elenco
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery1 As New Data.DataTable

            da1.Fill(dtQuery1)
            da1.Dispose()
            If dtQuery1.Rows.Count > 0 Then
                For Each r1 As Data.DataRow In dtQuery1.Rows
                    rowDT = dtDipendModif.NewRow()

                    rowDT.Item("ID") = r1.Item("ID")
                    rowDT.Item("DESCRIZIONE") = r1.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""

                    dtDipendModif.Rows.Add(rowDT)
                Next
            End If
            'End If

            'CType(di.Cells(4).FindControl("txtPaga"), TextBox).BackColor = Drawing.Color.DodgerBlue
            'CType(di.Cells(4).FindControl("txtPaga"), TextBox).ForeColor = Drawing.Color.White
            txtTotImporto.Text = FormatNumber(dipendente, 2)

            DataGridDipend.DataSource = dtDipendModif
            DataGridDipend.DataBind()

            If dtQuery.Rows.Count = 0 Then
                CType(DataGridDipend.Items(0).Cells(3).FindControl("txtImporto"), TextBox).Text = FormatNumber(dipendente, 2)
            End If

            If importoTotale = 0 Then
                importoDip.Value = 0
            Else
                importoDip.Value = par.IfEmpty(txtTotImporto.Text, 0) - importoTotale
            End If

            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddDipend" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaImportiPENS(ByVal pensione As Decimal)
        Try
            Dim rowDT As System.Data.DataRow
            dtPensModif = New Data.DataTable
            Dim importoTotale As Decimal = 0

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            dtPensModif.Columns.Add("id")
            dtPensModif.Columns.Add("DESCRIZIONE")
            dtPensModif.Columns.Add("IMPORTO")
            dtPensModif.Columns.Add("NUM_GG")
            dtPensModif.Columns.Add("IDIMPORTI")

            par.cmd.CommandText = "select VSA_REDDITI_PENSIONE.*,VSA_REDD_PENS_IMPORTI.ID AS IDIMPORTI,VSA_REDD_PENS_IMPORTI.NUM_GG,VSA_REDD_PENS_IMPORTI.IMPORTO from VSA_REDDITI_PENSIONE,VSA_REDD_PENS_IMPORTI,DOMANDE_REDDITI_VSA where VSA_REDDITI_PENSIONE.id=VSA_REDD_PENS_IMPORTI.ID_REDD_PENSIONE(+) AND DOMANDE_REDDITI_VSA.ID=VSA_REDD_PENS_IMPORTI.ID_REDD_TOT AND ID_COMPONENTE=" & cmbComponente.SelectedValue
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtPensModif.NewRow()
                    reddPresente = True
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = par.IfNull(row.Item("IMPORTO"), "")
                    rowDT.Item("NUM_GG") = par.IfNull(row.Item("NUM_GG"), "")
                    rowDT.Item("IDIMPORTI") = par.IfNull(row.Item("IDIMPORTI"), "")
                    importoTotale = importoTotale + par.IfNull(row.Item("IMPORTO"), 0)
                    dtPensModif.Rows.Add(rowDT)
                Next
            Else
                'rowDT = dtPensModif.NewRow()
                'rowDT.Item("ID") = "-1"
                'rowDT.Item("DESCRIZIONE") = "&nbsp"
                'rowDT.Item("IMPORTO") = 0
                'rowDT.Item("NUM_GG") = "&nbsp"
                'dtPensModif.Rows.Add(rowDT)
            End If

            Dim Elenco As String = ""
            For Each r1 As Data.DataRow In dtPensModif.Rows
                Elenco = Elenco & r1.Item("ID") & ","
            Next

            If Elenco <> "" Then
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
            Else
                Elenco = "-1"
            End If

            par.cmd.CommandText = "select * from VSA_REDDITI_PENSIONE where id not in " & Elenco
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery1 As New Data.DataTable

            da1.Fill(dtQuery1)
            da1.Dispose()
            If dtQuery1.Rows.Count > 0 Then
                For Each r1 As Data.DataRow In dtQuery1.Rows
                    rowDT = dtPensModif.NewRow()

                    rowDT.Item("ID") = r1.Item("ID")
                    rowDT.Item("DESCRIZIONE") = r1.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""

                    dtPensModif.Rows.Add(rowDT)
                Next
            End If

            txtImportoP.Text = FormatNumber(pensione, 2)
            DataGridPens.DataSource = dtPensModif
            DataGridPens.DataBind()

            If dtQuery.Rows.Count = 0 Then
                CType(DataGridPens.Items(0).Cells(3).FindControl("txtImporto"), TextBox).Text = FormatNumber(pensione, 2)
            End If
            'CERCO L'IMPORTO SINGOLO DI REDDITO
            'If importoTotale = 0 Then
            '    par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID=" & idRedd
            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader.Read Then
            '        importoTotale = importoTotale + par.IfNull(myReader("PENSIONE"), 0)
            '    End If
            '    myReader.Close()

            '    txtImportoP.Text = FormatNumber(importoTotale, 2)
            'End If

            If importoTotale = 0 Then
                importoPens.Value = 0
            Else
                importoPens.Value = par.IfEmpty(txtImportoP.Text, 0) - importoTotale
            End If

            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddPens" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaImportiAUTON(ByVal autonomo As Decimal)
        Try
            Dim rowDT As System.Data.DataRow
            dtAutonModif = New Data.DataTable
            Dim importoTotale As Decimal = 0

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            dtAutonModif.Columns.Add("id")
            dtAutonModif.Columns.Add("DESCRIZIONE")
            dtAutonModif.Columns.Add("IMPORTO")
            dtAutonModif.Columns.Add("NUM_GG")
            dtAutonModif.Columns.Add("IDIMPORTI")

            par.cmd.CommandText = "select VSA_REDDITI_AUTONOMO.*,VSA_REDD_AUTONOMO_IMPORTI.ID AS IDIMPORTI,VSA_REDD_AUTONOMO_IMPORTI.NUM_GG,VSA_REDD_AUTONOMO_IMPORTI.IMPORTO from VSA_REDDITI_AUTONOMO,VSA_REDD_AUTONOMO_IMPORTI,DOMANDE_REDDITI_VSA where VSA_REDDITI_AUTONOMO.id=VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_AUTONOMO(+) AND DOMANDE_REDDITI_VSA.ID=VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT AND ID_COMPONENTE=" & cmbComponente.SelectedValue
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtAutonModif.NewRow()
                    reddPresente = True
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = par.IfNull(row.Item("IMPORTO"), "")
                    rowDT.Item("NUM_GG") = par.IfNull(row.Item("NUM_GG"), "")
                    rowDT.Item("IDIMPORTI") = par.IfNull(row.Item("IDIMPORTI"), "")
                    importoTotale = importoTotale + par.IfNull(row.Item("IMPORTO"), 0)
                    dtAutonModif.Rows.Add(rowDT)
                Next
            Else
                'rowDT = dtAutonModif.NewRow()
                'rowDT.Item("ID") = "-1"
                'rowDT.Item("DESCRIZIONE") = "&nbsp"
                'rowDT.Item("IMPORTO") = 0
                'rowDT.Item("NUM_GG") = "&nbsp"
                'dtAutonModif.Rows.Add(rowDT)
            End If

            Dim Elenco As String = ""
            For Each r1 As Data.DataRow In dtAutonModif.Rows
                Elenco = Elenco & r1.Item("ID") & ","
            Next

            If Elenco <> "" Then
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
            Else
                Elenco = "-1"
            End If

            par.cmd.CommandText = "select * from VSA_REDDITI_AUTONOMO where id not in " & Elenco
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery1 As New Data.DataTable

            da1.Fill(dtQuery1)
            da1.Dispose()
            If dtQuery1.Rows.Count > 0 Then
                For Each r1 As Data.DataRow In dtQuery1.Rows
                    rowDT = dtAutonModif.NewRow()

                    rowDT.Item("ID") = r1.Item("ID")
                    rowDT.Item("DESCRIZIONE") = r1.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""

                    dtAutonModif.Rows.Add(rowDT)
                Next
            End If


            txtImportoA.Text = FormatNumber(autonomo, 2)
            DataGridAuton.DataSource = dtAutonModif
            DataGridAuton.DataBind()

            If dtQuery.Rows.Count = 0 Then
                CType(DataGridAuton.Items(0).Cells(3).FindControl("txtImporto"), TextBox).Text = FormatNumber(autonomo, 2)
            End If
            'CERCO L'IMPORTO SINGOLO DI REDDITO
            'If importoTotale = 0 Then
            '    par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID=" & idRedd
            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader.Read Then
            '        importoTotale += FormatNumber(par.IfNull(myReader("AUTONOMO"), 0), 2)
            '        importoTotale += FormatNumber(par.IfNull(myReader("NON_IMPONIBILI"), 0), 2)
            '        importoTotale += FormatNumber(par.IfNull(myReader("DOM_AG_FAB"), 0), 2)
            '        importoTotale += FormatNumber(par.IfNull(myReader("OCCASIONALI"), 0), 2)
            '    End If
            '    myReader.Close()

            '    txtImportoA.Text = FormatNumber(importoTotale, 2)
            'End If

            If importoTotale = 0 Then
                importoAuton.Value = 0
            Else
                importoAuton.Value = par.IfEmpty(txtImportoA.Text, 0) - importoTotale
            End If

            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddPens" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaImportiPENSEs(ByVal pensEsente As Decimal)
        Try
            Dim rowDT As System.Data.DataRow
            dtPensEsModif = New Data.DataTable
            Dim importoTotale As Decimal = 0

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            dtPensEsModif.Columns.Add("id")
            dtPensEsModif.Columns.Add("DESCRIZIONE")
            dtPensEsModif.Columns.Add("IMPORTO")
            dtPensEsModif.Columns.Add("NUM_GG")
            dtPensEsModif.Columns.Add("IDIMPORTI")

            par.cmd.CommandText = "select VSA_REDDITI_PENS_ESENTI.*,VSA_REDD_PENS_ES_IMPORTI.ID AS IDIMPORTI,VSA_REDD_PENS_ES_IMPORTI.NUM_GG,VSA_REDD_PENS_ES_IMPORTI.IMPORTO from VSA_REDDITI_PENS_ESENTI,VSA_REDD_PENS_ES_IMPORTI,DOMANDE_REDDITI_VSA where VSA_REDDITI_PENS_ESENTI.id=VSA_REDD_PENS_ES_IMPORTI.ID_REDD_PENS_ESENTI(+) AND DOMANDE_REDDITI_VSA.ID=VSA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT AND ID_COMPONENTE=" & cmbComponente.SelectedValue
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtPensEsModif.NewRow()
                    reddPresente = True
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = par.IfNull(row.Item("IMPORTO"), "")
                    rowDT.Item("NUM_GG") = par.IfNull(row.Item("NUM_GG"), "")
                    rowDT.Item("IDIMPORTI") = par.IfNull(row.Item("IDIMPORTI"), "")
                    importoTotale = importoTotale + par.IfNull(row.Item("IMPORTO"), 0)
                    dtPensEsModif.Rows.Add(rowDT)
                Next
            Else
                'rowDT = dtPensEsModif.NewRow()
                'rowDT.Item("ID") = "-1"
                'rowDT.Item("DESCRIZIONE") = "&nbsp"
                'rowDT.Item("IMPORTO") = 0
                'rowDT.Item("NUM_GG") = "&nbsp"
                'dtPensEsModif.Rows.Add(rowDT)
            End If

            Dim Elenco As String = ""
            For Each r1 As Data.DataRow In dtPensEsModif.Rows
                Elenco = Elenco & r1.Item("ID") & ","
            Next

            If Elenco <> "" Then
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
            Else
                Elenco = "-1"
            End If

            par.cmd.CommandText = "select * from VSA_REDDITI_PENS_ESENTI where id not in " & Elenco
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery1 As New Data.DataTable

            da1.Fill(dtQuery1)
            da1.Dispose()
            If dtQuery1.Rows.Count > 0 Then
                For Each r1 As Data.DataRow In dtQuery1.Rows
                    rowDT = dtPensEsModif.NewRow()

                    rowDT.Item("ID") = r1.Item("ID")
                    rowDT.Item("DESCRIZIONE") = r1.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""

                    dtPensEsModif.Rows.Add(rowDT)
                Next
            End If

            txtImportoPes.Text = FormatNumber(pensEsente, 2)
            DataGridPensEs.DataSource = dtPensEsModif
            DataGridPensEs.DataBind()

            If dtQuery.Rows.Count = 0 Then
                CType(DataGridPensEs.Items(0).Cells(3).FindControl("txtImporto"), TextBox).Text = FormatNumber(pensEsente, 2)
            End If
            'CERCO L'IMPORTO SINGOLO DI REDDITO
            'If importoTotale = 0 Then
            '    par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID=" & idRedd
            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader.Read Then
            '        importoTotale = importoTotale + par.IfNull(myReader("PENS_ESENTE"), 0)
            '    End If
            '    myReader.Close()

            '    txtImportoPes.Text = FormatNumber(importoTotale, 2)

            'End If

            If importoTotale = 0 Then
                importoPensEs.Value = 0
            Else
                importoPensEs.Value = par.IfEmpty(txtImportoPes.Text, 0) - importoTotale
            End If

            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddPens" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaImportiNOISEE(ByVal noIsee As Decimal)
        Try
            Dim rowDT As System.Data.DataRow
            dtNoIseeModif = New Data.DataTable
            Dim importoTotale As Decimal = 0

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            dtNoIseeModif.Columns.Add("id")
            dtNoIseeModif.Columns.Add("DESCRIZIONE")
            dtNoIseeModif.Columns.Add("IMPORTO")
            dtNoIseeModif.Columns.Add("NUM_GG")
            dtNoIseeModif.Columns.Add("IDIMPORTI")

            par.cmd.CommandText = "select VSA_REDDITI_NO_ISEE.*,VSA_REDD_NO_ISEE_IMPORTI.ID AS IDIMPORTI,VSA_REDD_NO_ISEE_IMPORTI.NUM_GG,VSA_REDD_NO_ISEE_IMPORTI.IMPORTO from VSA_REDDITI_NO_ISEE,VSA_REDD_NO_ISEE_IMPORTI,DOMANDE_REDDITI_VSA where VSA_REDDITI_NO_ISEE.id=VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_NO_ISEE(+) AND DOMANDE_REDDITI_VSA.ID=VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_TOT AND ID_COMPONENTE=" & cmbComponente.SelectedValue
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable

            da.Fill(dtQuery)
            da.Dispose()

            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtNoIseeModif.NewRow()
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("DESCRIZIONE") = row.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = par.IfNull(row.Item("IMPORTO"), "")
                    rowDT.Item("NUM_GG") = par.IfNull(row.Item("NUM_GG"), "")
                    rowDT.Item("IDIMPORTI") = par.IfNull(row.Item("IDIMPORTI"), "")
                    importoTotale = importoTotale + par.IfNull(row.Item("IMPORTO"), 0)
                    dtNoIseeModif.Rows.Add(rowDT)
                Next
            Else
                'rowDT = dtNoIseeModif.NewRow()
                'rowDT.Item("ID") = "-1"
                'rowDT.Item("DESCRIZIONE") = "&nbsp"
                'rowDT.Item("IMPORTO") = 0
                'rowDT.Item("NUM_GG") = "&nbsp"
                'dtNoIseeModif.Rows.Add(rowDT)
            End If

            Dim Elenco As String = ""
            For Each r1 As Data.DataRow In dtNoIseeModif.Rows
                Elenco = Elenco & r1.Item("ID") & ","
            Next

            If Elenco <> "" Then
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
            Else
                Elenco = "-1"
            End If

            par.cmd.CommandText = "select * from VSA_REDDITI_NO_ISEE where id not in " & Elenco
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery1 As New Data.DataTable

            da1.Fill(dtQuery1)
            da1.Dispose()
            If dtQuery1.Rows.Count > 0 Then
                For Each r1 As Data.DataRow In dtQuery1.Rows
                    rowDT = dtNoIseeModif.NewRow()

                    rowDT.Item("ID") = r1.Item("ID")
                    rowDT.Item("DESCRIZIONE") = r1.Item("DESCRIZIONE")
                    rowDT.Item("IMPORTO") = ""
                    rowDT.Item("NUM_GG") = ""

                    dtNoIseeModif.Rows.Add(rowDT)
                Next
            End If

            txtImportoNoIsee.Text = FormatNumber(noIsee, 2)
            DataGridNoIsee.DataSource = dtNoIseeModif
            DataGridNoIsee.DataBind()

            If dtQuery.Rows.Count = 0 Then
                CType(DataGridNoIsee.Items(0).Cells(3).FindControl("txtImporto"), TextBox).Text = FormatNumber(noIsee, 2)
            End If
            'If importoTotale = 0 Then
            '    par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID=" & idRedd
            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader.Read Then
            '        importoTotale = importoTotale + par.IfNull(myReader("ONERI"), 0)
            '        importoTotale = importoTotale + par.IfNull(myReader("NO_ISEE"), 0)
            '    End If
            '    myReader.Close()

            '    txtImportoNoIsee.Text = FormatNumber(importoTotale, 2)

            'End If

            If importoTotale = 0 Then
                importoNoIsee.Value = 0
            Else
                importoNoIsee.Value = par.IfEmpty(txtImportoNoIsee.Text, 0) - importoTotale
            End If

            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CaricaDettReddPens" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConfirm.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim redditoScritto As Integer = 0

            salvaRedditi.Value = "1"

            'LAVORO DIPENDENTE
            For i = 0 To DataGridDipend.Items.Count - 1

                di = Me.DataGridDipend.Items(i)
                Dim idDipend As Long = 0
                Dim idImporti As Long = 0

                If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                    redditoScritto = 1
                    'SCRIVI RECORD IMPORTO IN DIPENDENTE
                    If dtReddDipend.Rows.Count > 0 Then
                        idDipend = dtReddDipend.Rows(i).Item(0)
                    Else
                        idDipend = dtDipendModif.Rows(i).Item(0)
                        idImporti = par.IfNull(dtDipendModif.Rows(i).Item(4), 0)
                    End If
                    ScriviReddito(CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text, par.IfEmpty(CType(di.Cells(2).FindControl("txtNumGG"), TextBox).Text, 0), idDipend, "1", idImporti)
                End If
            Next

            For i = 0 To DataGridAuton.Items.Count - 1
                di = Me.DataGridAuton.Items(i)
                Dim idAuton As Long = 0
                Dim idImporti As Long = 0

                If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                    redditoScritto = 1
                    If dtReddAuton.Rows.Count > 0 Then
                        idAuton = dtReddAuton.Rows(i).Item(0)
                    Else
                        idAuton = dtAutonModif.Rows(i).Item(0)
                        idImporti = par.IfNull(dtAutonModif.Rows(i).Item(4), 0)
                    End If

                    ScriviReddito(CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text, par.IfEmpty(CType(di.Cells(2).FindControl("txtNumGG"), TextBox).Text, 0), idAuton, "2", idImporti)
                End If
            Next

            For i = 0 To DataGridPens.Items.Count - 1
                di = Me.DataGridPens.Items(i)
                Dim idPens As Long = 0
                Dim idImporti As Long = 0

                If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                    redditoScritto = 1
                    If dtReddPens.Rows.Count > 0 Then
                        idPens = dtReddPens.Rows(i).Item(0)
                    Else
                        idPens = dtPensModif.Rows(i).Item(0)
                        idImporti = par.IfNull(dtPensModif.Rows(i).Item(4), 0)
                    End If
                    ScriviReddito(CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text, par.IfEmpty(CType(di.Cells(2).FindControl("txtNumGG"), TextBox).Text, 0), idPens, "3", idImporti)
                End If
            Next

            For i = 0 To DataGridPensEs.Items.Count - 1
                di = Me.DataGridPensEs.Items(i)
                Dim idPensEs As Long = 0
                Dim idImporti As Long = 0

                If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                    redditoScritto = 1
                    If dtReddPensEs.Rows.Count > 0 Then
                        idPensEs = dtReddPensEs.Rows(i).Item(0)
                    Else
                        idPensEs = dtPensEsModif.Rows(i).Item(0)
                        idImporti = par.IfNull(dtPensEsModif.Rows(i).Item(4), 0)
                    End If
                    ScriviReddito(CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text, par.IfEmpty(CType(di.Cells(2).FindControl("txtNumGG"), TextBox).Text, 0), idPensEs, "4", idImporti)
                End If
            Next

            For i = 0 To DataGridNoIsee.Items.Count - 1
                di = Me.DataGridNoIsee.Items(i)
                Dim idNoIsee As Long = 0
                Dim idImporti As Long = 0

                If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                    redditoScritto = 1
                    If dtReddNoIsee.Rows.Count > 0 Then
                        idNoIsee = dtReddNoIsee.Rows(i).Item(0)
                    Else
                        idNoIsee = dtNoIseeModif.Rows(i).Item(0)
                        idImporti = par.IfNull(dtNoIseeModif.Rows(i).Item(4), 0)
                    End If

                    ScriviReddito(CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text, par.IfEmpty(CType(di.Cells(2).FindControl("txtNumGG"), TextBox).Text, 0), idNoIsee, "5", idImporti)
                End If
            Next
            If redditoScritto = 1 Then
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaRedditi.Value & ");", True)

            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Nessun importo da salvare!')", True)

            End If

            'Response.Write("<script>window.close();</script>")

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>btnSalva()" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub ScriviReddito(ByVal importo As Decimal, ByVal numGG As Integer, ByVal idReddDett As Integer, ByVal tipoRedd As String, ByVal idImporti As Long)
        Try
            'Dim reddPresente As Boolean = False

            Select Case tipoRedd
                Case "1"
                    Dim impTOTDipend As Decimal = 0
                    Dim idUtenzaRedd1 As Long = 0
                    Dim impDiffer As Decimal = 0
                    Dim impSomma As Decimal = 0

                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_DIPEND_IMPORTI WHERE ID=" & idImporti
                    Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderI.Read Then
                        If par.IfNull(myReaderI("IMPORTO"), 0) = importo Then
                            importo = 0
                        Else
                            If par.IfNull(myReaderI("IMPORTO"), 0) > importo Then
                                impDiffer = par.IfNull(myReaderI("IMPORTO"), 0) - importo
                                importo = 0
                            Else
                                impSomma = importo - par.IfNull(myReaderI("IMPORTO"), 0)
                                importo = 0
                            End If
                        End If
                    End If
                    myReaderI.Close()

                    'If importo <> 0 Then

                    Dim reddImportato1 As Integer = 0
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA,VSA_REDD_DIPEND_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_DIPEND_IMPORTI.ID_REDD_TOT and NVL(DIPENDENTE,0)<>0 and ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader0.Read() Then
                        reddPresente = True
                        idUtenzaRedd1 = myReader0(0)
                        impTOTDipend = par.IfNull(myReader0("DIPENDENTE"), 0)
                    Else
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                        Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCon.Read Then
                            reddImportato1 = 1
                            idUtenzaRedd1 = myReaderCon(0)
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                                & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazione & "," & cmbComponente.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "SELECT SEQ_DOMANDE_REDDITI_VSA.CURRVAL FROM DUAL"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                idUtenzaRedd1 = myReader(0)
                            End If
                            myReader.Close()
                        End If
                        myReaderCon.Close()
                    End If
                    myReader0.Close()

                    impTOTDipend = (impTOTDipend + importo + impSomma) - impDiffer

                    Dim importoNew As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_DIPEND_IMPORTI WHERE ID=" & idImporti
                    myReader0 = par.cmd.ExecuteReader()
                    If myReader0.Read Then
                        If importo <> 0 Or (impSomma <> 0 Or impDiffer <> 0) Then
                            If impSomma <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) + impSomma
                            End If
                            If impDiffer <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) - impDiffer
                            End If

                            If importoNew <> 0 Then
                                importo = importoNew
                            End If

                            If importo = 0 Then
                                par.cmd.CommandText = "DELETE FROM VSA_REDD_DIPEND_IMPORTI WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "UPDATE VSA_REDD_DIPEND_IMPORTI SET IMPORTO=" & par.VirgoleInPunti(importo) & ",NUM_GG=" & numGG & " WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            End If

                        Else
                            par.cmd.CommandText = "UPDATE VSA_REDD_DIPEND_IMPORTI SET NUM_GG=" & numGG & " WHERE ID=" & idImporti
                            par.cmd.ExecuteNonQuery()
                        End If
                    Else
                        If importo <> 0 Then
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_DIPEND_IMPORTI (ID, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                            & "VALUES (SEQ_VSA_REDD_DIPEND_IMPORTI.NEXTVAL," & idReddDett & "," & par.VirgoleInPunti(importo) & "," & numGG & "," & idUtenzaRedd1 & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If
                    myReader0.Close()

                    'End If
                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET DIPENDENTE=" & par.VirgoleInPunti(impTOTDipend) & " WHERE ID=" & idUtenzaRedd1
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        If reddImportato1 = 1 Then
                            If reddPresente = False Then
                                par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impTOTDipend) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                par.cmd.ExecuteNonQuery()
                            Else
                                If importo <> 0 Then
                                    If impDiffer <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        If impSomma <> 0 Then
                                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impSomma + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                            par.cmd.ExecuteNonQuery()
                                        Else
                                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                    End If
                                Else
                                    If impDiffer <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            End If
                        Else
                            If importo <> 0 Then
                                If impDiffer <> 0 Then
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    If impSomma <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impSomma + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            Else
                                If impDiffer <> 0 Then
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                        End If
                        If impTOTDipend <> 0 Then
                            reddPresente = True
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(impTOTDipend) & ")"
                        par.cmd.ExecuteNonQuery()
                        If impTOTDipend <> 0 Then
                            reddPresente = True
                        End If
                    End If
                    myReaderR.Close()

                Case "2"
                    Dim impTOTAuton As Decimal = 0
                    Dim idUtenzaRedd2 As Long = 0
                    Dim impDiffer As Decimal = 0
                    Dim impSomma As Decimal = 0

                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID=" & idImporti
                    Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderI.Read Then
                        If par.IfNull(myReaderI("IMPORTO"), 0) = importo Then
                            importo = 0
                        Else
                            If par.IfNull(myReaderI("IMPORTO"), 0) > importo Then
                                impDiffer = par.IfNull(myReaderI("IMPORTO"), 0) - importo
                                importo = 0
                            Else
                                impSomma = importo - par.IfNull(myReaderI("IMPORTO"), 0)
                                importo = 0
                            End If
                        End If
                    End If
                    myReaderI.Close()

                    'If importo <> 0 Then
                    Dim reddImportato2 As Integer = 0
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA,VSA_REDD_AUTONOMO_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT and NVL(AUTONOMO,0)<>0 AND ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader0.Read() Then
                        idUtenzaRedd2 = myReader0(0)
                        impTOTAuton = par.IfNull(myReader0("AUTONOMO"), 0)
                        reddPresente = True
                    Else
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                        Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCon.Read Then
                            reddImportato2 = 1
                            idUtenzaRedd2 = myReaderCon(0)
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                            & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazione & "," & cmbComponente.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "SELECT SEQ_DOMANDE_REDDITI_VSA.CURRVAL FROM DUAL"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                idUtenzaRedd2 = myReader(0)
                            End If
                            myReader.Close()
                        End If
                        myReaderCon.Close()
                    End If
                    myReader0.Close()

                    impTOTAuton = (impTOTAuton + importo + impSomma) - impDiffer

                    Dim importoNew As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID=" & idImporti
                    myReader0 = par.cmd.ExecuteReader()
                    If myReader0.Read Then
                        If importo <> 0 Or (impSomma <> 0 Or impDiffer <> 0) Then
                            If impSomma <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) + impSomma
                            End If
                            If impDiffer <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) - impDiffer
                            End If

                            If importoNew <> 0 Then
                                importo = importoNew
                            End If

                            If importo = 0 Then
                                par.cmd.CommandText = "DELETE FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "UPDATE VSA_REDD_AUTONOMO_IMPORTI SET IMPORTO=" & par.VirgoleInPunti(importo) & ",NUM_GG=" & numGG & " WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            End If
                        Else
                            par.cmd.CommandText = "UPDATE VSA_REDD_AUTONOMO_IMPORTI SET NUM_GG=" & numGG & " WHERE ID=" & idImporti
                            par.cmd.ExecuteNonQuery()
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO VSA_REDD_AUTONOMO_IMPORTI (ID, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                        & "VALUES (SEQ_VSA_REDD_AUTON_IMPORTI.NEXTVAL," & idReddDett & "," & par.VirgoleInPunti(importo) & "," & numGG & "," & idUtenzaRedd2 & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader0.Close()
                    'End If

                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET AUTONOMO=" & par.VirgoleInPunti(impTOTAuton) & ",DOM_AG_FAB=0,OCCASIONALI=0 WHERE ID=" & idUtenzaRedd2
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        If reddImportato2 = 1 Then
                            If reddPresente = False Then
                                par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impTOTAuton) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                par.cmd.ExecuteNonQuery()
                            Else
                                If importo <> 0 Then
                                    If impDiffer <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        If impSomma <> 0 Then
                                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impSomma + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                            par.cmd.ExecuteNonQuery()
                                        Else
                                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                    End If
                                Else
                                    If impDiffer <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            End If
                        Else
                            If importo <> 0 Then
                                If impDiffer <> 0 Then
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    If impSomma <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impSomma + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            Else
                                If impDiffer <> 0 Then
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                        End If
                        If impTOTAuton <> 0 Then
                            reddPresente = True
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(impTOTAuton) & ")"
                        par.cmd.ExecuteNonQuery()
                        If impTOTAuton <> 0 Then
                            reddPresente = True
                        End If
                    End If
                    myReaderR.Close()

                Case "3"
                    Dim impTOTPens As Decimal = 0
                    Dim idUtenzaRedd3 As Long = 0

                    Dim impDiffer As Decimal = 0
                    Dim impSomma As Decimal = 0

                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI WHERE ID=" & idImporti
                    Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderI.Read Then
                        If par.IfNull(myReaderI("IMPORTO"), 0) = importo Then
                            importo = 0
                        Else
                            If par.IfNull(myReaderI("IMPORTO"), 0) > importo Then
                                impDiffer = par.IfNull(myReaderI("IMPORTO"), 0) - importo
                                importo = 0
                            Else
                                impSomma = importo - par.IfNull(myReaderI("IMPORTO"), 0)
                                importo = 0
                            End If
                        End If
                    End If
                    myReaderI.Close()

                    'If importo <> 0 Then
                    Dim reddImportato3 As Integer = 0
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA,VSA_REDD_PENS_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_PENS_IMPORTI.ID_REDD_TOT and NVL(PENSIONE,0)<>0 and ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader0.Read() Then
                        idUtenzaRedd3 = myReader0(0)
                        impTOTPens = par.IfNull(myReader0("PENSIONE"), 0)
                        reddPresente = True
                    Else
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                        Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCon.Read Then
                            reddImportato3 = 1
                            idUtenzaRedd3 = myReaderCon(0)
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                            & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazione & "," & cmbComponente.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "SELECT SEQ_DOMANDE_REDDITI_VSA.CURRVAL FROM DUAL"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                idUtenzaRedd3 = myReader(0)
                            End If
                            myReader.Close()
                        End If
                        myReaderCon.Close()
                    End If
                    myReader0.Close()

                    impTOTPens = (impTOTPens + importo + impSomma) - impDiffer

                    Dim importoNew As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI WHERE ID=" & idImporti
                    myReader0 = par.cmd.ExecuteReader()
                    If myReader0.Read Then
                        If importo <> 0 Or (impSomma <> 0 Or impDiffer <> 0) Then
                            If impSomma <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) + impSomma
                            End If
                            If impDiffer <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) - impDiffer
                            End If

                            If importoNew <> 0 Then
                                importo = importoNew
                            End If
                            If importo = 0 Then
                                par.cmd.CommandText = "DELETE FROM VSA_REDD_PENS_IMPORTI WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "UPDATE VSA_REDD_PENS_IMPORTI SET IMPORTO=" & par.VirgoleInPunti(importo) & ",NUM_GG=" & numGG & " WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            End If

                        Else
                            par.cmd.CommandText = "UPDATE VSA_REDD_PENS_IMPORTI SET NUM_GG=" & numGG & " WHERE ID=" & idImporti
                            par.cmd.ExecuteNonQuery()
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_IMPORTI (ID, ID_REDD_PENSIONE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                            & "VALUES (SEQ_VSA_REDD_PENS_IMPORTI.NEXTVAL," & idReddDett & "," & par.VirgoleInPunti(importo) & "," & numGG & "," & idUtenzaRedd3 & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader0.Close()

                    'End If
                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET PENSIONE=" & par.VirgoleInPunti(impTOTPens) & " WHERE ID=" & idUtenzaRedd3
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        If reddImportato3 = 1 Then
                            If reddPresente = False Then
                                par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impTOTPens) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                par.cmd.ExecuteNonQuery()
                            Else
                                If importo <> 0 Then
                                    If impDiffer <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        If impSomma <> 0 Then
                                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impSomma + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                            par.cmd.ExecuteNonQuery()
                                        Else
                                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                    End If
                                Else
                                    If impDiffer <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            End If
                        Else
                            If importo <> 0 Then
                                If impDiffer <> 0 Then
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    If impSomma <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impSomma + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            Else
                                If impDiffer <> 0 Then
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                        End If
                        If impTOTPens <> 0 Then
                            reddPresente = True
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(impTOTPens) & ")"
                        par.cmd.ExecuteNonQuery()
                        If impTOTPens <> 0 Then
                            reddPresente = True
                        End If
                    End If
                    myReaderR.Close()

                Case "4"
                    Dim impTOTPensEs As Decimal = 0
                    Dim idUtenzaRedd4 As Long = 0

                    Dim impDiffer As Decimal = 0
                    Dim impSomma As Decimal = 0

                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID=" & idImporti
                    Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderI.Read Then
                        If par.IfNull(myReaderI("IMPORTO"), 0) = importo Then
                            importo = 0
                        Else
                            If par.IfNull(myReaderI("IMPORTO"), 0) > importo Then
                                impDiffer = par.IfNull(myReaderI("IMPORTO"), 0) - importo
                                importo = 0
                            Else
                                impSomma = importo - par.IfNull(myReaderI("IMPORTO"), 0)
                                importo = 0
                            End If
                        End If
                    End If
                    myReaderI.Close()

                    'If importo <> 0 Then
                    Dim reddImportato4 As Integer = 0
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA,VSA_REDD_PENS_ES_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT and NVL(PENS_ESENTE,0)<>0 and ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader0.Read() Then
                        idUtenzaRedd4 = myReader0(0)
                        impTOTPensEs = par.IfNull(myReader0("PENS_ESENTE"), 0)
                        reddPresente = True
                    Else
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                        Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCon.Read Then
                            reddImportato4 = 1
                            idUtenzaRedd4 = myReaderCon(0)
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                                & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazione & "," & cmbComponente.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "SELECT SEQ_DOMANDE_REDDITI_VSA.CURRVAL FROM DUAL"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                idUtenzaRedd4 = myReader(0)
                            End If
                            myReader.Close()
                        End If
                        myReaderCon.Close()
                    End If
                    myReader0.Close()

                    impTOTPensEs = (impTOTPensEs + importo + impSomma) - impDiffer

                    Dim importoNew As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID=" & idImporti
                    myReader0 = par.cmd.ExecuteReader
                    If myReader0.Read Then
                        If importo <> 0 Or (impSomma <> 0 Or impDiffer <> 0) Then
                            If impSomma <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) + impSomma
                            End If
                            If impDiffer <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) - impDiffer
                            End If

                            If importoNew <> 0 Then
                                importo = importoNew
                            End If

                            If importo = 0 Then
                                par.cmd.CommandText = "DELETE FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "UPDATE VSA_REDD_PENS_ES_IMPORTI SET IMPORTO=" & par.VirgoleInPunti(importo) & ",NUM_GG=" & numGG & " WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            End If

                        Else
                            par.cmd.CommandText = "UPDATE VSA_REDD_PENS_ES_IMPORTI SET NUM_GG=" & numGG & " WHERE ID=" & idImporti
                            par.cmd.ExecuteNonQuery()
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_ES_IMPORTI (ID, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                        & "VALUES (SEQ_VSA_REDD_PENS_IMPORTI.NEXTVAL," & idReddDett & "," & par.VirgoleInPunti(importo) & "," & numGG & "," & idUtenzaRedd4 & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader0.Close()
                    'End If


                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET PENS_ESENTE=" & par.VirgoleInPunti(impTOTPensEs) & ",NON_IMPONIBILI = 0 WHERE ID=" & idUtenzaRedd4
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        If reddImportato4 = 1 Then
                            If reddPresente = False Then
                                par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impTOTPensEs) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                par.cmd.ExecuteNonQuery()
                            Else
                                If importo <> 0 Then
                                    If impDiffer <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        If impSomma <> 0 Then
                                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impSomma + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                            par.cmd.ExecuteNonQuery()
                                        Else
                                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                    End If
                                Else
                                    If impDiffer <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            End If
                        Else
                            If importo <> 0 Then
                                If impDiffer <> 0 Then
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    If impSomma <> 0 Then
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(impSomma + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            Else
                                If impDiffer <> 0 Then
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - impDiffer) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.VirgoleInPunti(importo + par.IfNull(myReaderR("REDDITO_IRPEF"), 0)) & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                        End If
                        If impTOTPensEs <> 0 Then
                            reddPresente = True
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(impTOTPensEs) & ")"
                        par.cmd.ExecuteNonQuery()
                        If impTOTPensEs <> 0 Then
                            reddPresente = True
                        End If
                    End If
                    myReaderR.Close()

                Case "5"
                    Dim impTOTNoIsee As Decimal = 0
                    Dim idUtenzaRedd5 As Long = 0

                    Dim impDiffer As Decimal = 0
                    Dim impSomma As Decimal = 0

                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID=" & idImporti
                    Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderI.Read Then
                        If par.IfNull(myReaderI("IMPORTO"), 0) = importo Then
                            importo = 0
                        Else
                            If par.IfNull(myReaderI("IMPORTO"), 0) > importo Then
                                impDiffer = par.IfNull(myReaderI("IMPORTO"), 0) - importo
                                importo = 0
                            Else
                                impSomma = importo - par.IfNull(myReaderI("IMPORTO"), 0)
                                importo = 0
                            End If
                        End If
                    End If
                    myReaderI.Close()

                    'If importo <> 0 Then
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA,VSA_REDD_NO_ISEE_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_TOT and NVL(NO_ISEE,0)<>0 AND ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader0.Read Then
                        idUtenzaRedd5 = myReader0(0)
                        impTOTNoIsee = par.IfNull(myReader0("NO_ISEE"), 0)
                        reddPresente = True
                    Else
                        par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & lIdDichiarazione & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                        Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCon.Read Then
                            idUtenzaRedd5 = myReaderCon(0)
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                                & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazione & "," & cmbComponente.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "SELECT SEQ_DOMANDE_REDDITI_VSA.CURRVAL FROM DUAL"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                idUtenzaRedd5 = myReader(0)
                            End If
                            myReader.Close()
                        End If
                        myReaderCon.Close()
                    End If
                    myReader0.Close()

                    impTOTNoIsee = (impTOTNoIsee + importo + impSomma) - impDiffer

                    Dim importoNew As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID=" & idImporti
                    myReader0 = par.cmd.ExecuteReader
                    If myReader0.Read Then
                        If importo <> 0 Or (impSomma <> 0 Or impDiffer <> 0) Then
                            If impSomma <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) + impSomma
                            End If
                            If impDiffer <> 0 Then
                                importoNew = par.IfNull(myReader0("IMPORTO"), 0) - impDiffer
                            End If

                            If importoNew <> 0 Then
                                importo = importoNew
                            End If
                            If importo = 0 Then
                                par.cmd.CommandText = "DELETE FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "UPDATE VSA_REDD_NO_ISEE_IMPORTI SET IMPORTO=" & par.VirgoleInPunti(importo) & ",NUM_GG=" & numGG & " WHERE ID=" & idImporti
                                par.cmd.ExecuteNonQuery()
                            End If

                        Else
                            par.cmd.CommandText = "UPDATE VSA_REDD_NO_ISEE_IMPORTI SET NUM_GG=" & numGG & " WHERE ID=" & idImporti
                            par.cmd.ExecuteNonQuery()
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO VSA_REDD_NO_ISEE_IMPORTI (ID, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                        & "VALUES (SEQ_VSA_REDD_NO_ISEE_IMP.NEXTVAL," & idReddDett & "," & par.VirgoleInPunti(importo) & "," & numGG & "," & idUtenzaRedd5 & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader0.Close()
                    'End If
                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET NO_ISEE=" & par.VirgoleInPunti(impTOTNoIsee) & ",ONERI=0 WHERE ID=" & idUtenzaRedd5
                    par.cmd.ExecuteNonQuery()
            End Select


        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ScriviReddito" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub cmbComponente_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbComponente.SelectedIndexChanged
        If cmbComponente.SelectedValue = "-1" Then
            chkTipiReddito.Enabled = False
        Else
            chkTipiReddito.Enabled = True
        End If
    End Sub

    Protected Sub calcolaTot_btn_Click(sender As Object, e As System.EventArgs) Handles calcolaTot_btn.Click
        Try
            If svuotaTxt.Value <> "1" Then
                Dim var1 As Decimal = 0
                Dim importo1 As Decimal = 0
                If importoDip.Value <> 0 Then
                    importo1 = importoDip.Value
                End If
                Dim i As Integer = 0
                Dim di As DataGridItem
                Dim contaTotale1 As Boolean = True

                'LAVORO DIPENDENTE
                For i = 0 To DataGridDipend.Items.Count - 1
                    di = Me.DataGridDipend.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = "" And importo1 = 0 Then
                        contaTotale1 = False
                    End If
                Next

                For i = 0 To DataGridDipend.Items.Count - 1
                    di = Me.DataGridDipend.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                        If var1 = 0 Then
                            var1 = CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        Else
                            var1 = var1 + CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        End If
                    End If
                Next

                Dim diff1 As Decimal = 0
                Dim var01 As Decimal = 0
                If var1 <> 0 Or importo1 <> 0 Then
                    If contaTotale1 = False Then
                        txtTotImporto.Text = FormatNumber(var1, 2)
                    Else
                        txtTotImporto.Text = FormatNumber(importo1 + var1, 2)
                    End If
                Else

                    txtTotImporto.Text = 0
                End If

                'LAVORO AUTONOMO
                Dim var2 As Decimal = 0
                Dim importo2 As Decimal = 0
                If importoAuton.Value <> 0 Then
                    importo2 = importoAuton.Value
                End If
                Dim contaTotale2 As Boolean = True

                For i = 0 To DataGridAuton.Items.Count - 1
                    di = Me.DataGridAuton.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = "" And importo2 = 0 Then
                        contaTotale2 = False
                    End If
                Next

                For i = 0 To DataGridAuton.Items.Count - 1
                    di = Me.DataGridAuton.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                        If var2 = 0 Then
                            var2 = CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        Else
                            var2 = var2 + CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        End If
                    End If
                Next
                If var2 <> 0 Or importo2 <> 0 Then
                    If contaTotale2 = False Then
                        txtImportoA.Text = FormatNumber(var2, 2)
                    Else
                        txtImportoA.Text = FormatNumber(var2 + importo2, 2)
                    End If
                Else
                    txtImportoA.Text = 0
                End If


                'PENSIONE
                Dim var3 As Decimal = 0
                Dim importo3 As Decimal = 0
                If importoPens.Value <> 0 Then
                    importo3 = importoPens.Value
                End If
                Dim contaTotale3 As Boolean = True

                For i = 0 To DataGridPens.Items.Count - 1
                    di = Me.DataGridPens.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = "" And importo3 = 0 Then
                        contaTotale3 = False
                    End If
                Next

                For i = 0 To DataGridPens.Items.Count - 1
                    di = Me.DataGridPens.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                        If var3 = 0 Then
                            var3 = CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        Else
                            var3 = var3 + CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        End If
                    End If
                Next
                If var3 <> 0 Or importo3 <> 0 Then
                    If contaTotale3 = False Then
                        txtImportoP.Text = FormatNumber(var3, 2)
                    Else
                        txtImportoP.Text = FormatNumber(var3 + importo3, 2)
                    End If
                Else
                    txtImportoP.Text = 0
                End If


                'PENS. ESENTE
                Dim var4 As Decimal = 0
                Dim importo4 As Decimal = 0
                If importoPensEs.Value <> 0 Then
                    importo4 = importoPens.Value
                End If
                Dim contaTotale4 As Boolean = True

                For i = 0 To DataGridPensEs.Items.Count - 1
                    di = Me.DataGridPensEs.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = "" And importo4 = 0 Then
                        contaTotale4 = False
                    End If
                Next

                For i = 0 To DataGridPensEs.Items.Count - 1
                    di = Me.DataGridPensEs.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                        If var4 = 0 Then
                            var4 = CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        Else
                            var4 = var4 + CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        End If
                    End If
                Next
                If var4 <> 0 Or importo4 <> 0 Then
                    If contaTotale4 = False Then
                        txtImportoPes.Text = FormatNumber(var4, 2)
                    Else
                        txtImportoPes.Text = FormatNumber(var4 + importo4, 2)
                    End If
                Else
                    txtImportoPes.Text = 0
                End If


                'REDDITO NO ISEE
                Dim var5 As Decimal = 0
                Dim importo5 As Decimal = 0
                If importoNoIsee.Value <> 0 Then
                    importo5 = importoNoIsee.Value
                End If
                Dim contaTotale5 As Boolean = True

                For i = 0 To DataGridNoIsee.Items.Count - 1
                    di = Me.DataGridNoIsee.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = "" And importo5 = 0 Then
                        contaTotale5 = False
                    End If
                Next

                For i = 0 To DataGridNoIsee.Items.Count - 1
                    di = Me.DataGridNoIsee.Items(i)

                    If CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text <> "" Then
                        If var5 = 0 Then
                            var5 = CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        Else
                            var5 = var5 + CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text
                        End If
                    End If
                Next
                If var5 <> 0 Or importo5 <> 0 Then
                    If contaTotale5 = False Then
                        txtImportoNoIsee.Text = FormatNumber(var5, 2)
                    Else
                        txtImportoNoIsee.Text = FormatNumber(var5 + importo5, 2)
                    End If
                Else
                    txtImportoNoIsee.Text = 0
                End If
            Else
                If txtTotImporto.Text = "" Then
                    Dim i As Integer = 0
                    Dim di As DataGridItem
                    For i = 0 To DataGridDipend.Items.Count - 1
                        di = Me.DataGridDipend.Items(i)
                        CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = ""
                    Next
                End If

                If txtImportoA.Text = "" Then
                    Dim i As Integer = 0
                    Dim di As DataGridItem
                    For i = 0 To DataGridAuton.Items.Count - 1
                        di = Me.DataGridAuton.Items(i)
                        CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = ""
                    Next
                End If

                If txtImportoP.Text = "" Then
                    Dim i As Integer = 0
                    Dim di As DataGridItem
                    For i = 0 To DataGridPens.Items.Count - 1
                        di = Me.DataGridPens.Items(i)
                        CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = ""
                    Next
                End If

                If txtImportoPes.Text = "" Then
                    Dim i As Integer = 0
                    Dim di As DataGridItem
                    For i = 0 To DataGridPens.Items.Count - 1
                        di = Me.DataGridPens.Items(i)
                        CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = ""
                    Next
                End If

                If txtImportoNoIsee.Text = "" Then
                    Dim i As Integer = 0
                    Dim di As DataGridItem
                    For i = 0 To DataGridNoIsee.Items.Count - 1
                        di = Me.DataGridNoIsee.Items(i)
                        CType(di.Cells(3).FindControl("txtImporto"), TextBox).Text = ""
                    Next
                End If

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>CalcolaTot()" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub

End Class
