Imports System.Collections
Imports Telerik.Web.UI

Partial Class Tab_IBAN

    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Private Property vIdFornitori() As Long
        Get
            If Not (ViewState("par_idFornitori") Is Nothing) Then
                Return CLng(ViewState("par_idFornitori"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idFornitori") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            '---IN SOLA LETTURA NASCONDO I BOTTONI---'
            If Session.Item("BP_CC_L") = "1" Then
                'btnAggIBAN.Visible = False
                'btnApriIBAN.Visible = False
                'btnEliminaIBAN.Visible = False
            End If
            '----------------------------------------'

            If Session.Item("ERROREMOD") <> 1 Then
                Try
                    '------IMPOSTO IDCONNESSIONE E IDFORNITORE-------'
                    vIdFornitori = CType(Me.Page.FindControl("IDFornitore"), HiddenField).Value
                    idFORNITORE2.Value = vIdFornitori


                    '---SE SONO IN MODALITà DI SOLA LETTURA CARICO GLI IBAN IN SOLA LETTURA
                    '---APRO E CHIUDO CONNESSIONE
                    If CType(Me.Page.FindControl("modalitaSOLALETTURA"), HiddenField).Value = 1 Then
                        caricaIBANSolaLettura()
                    ElseIf CType(Me.Page.FindControl("daInserimento"), HiddenField).Value = 1 Then
                        'NON FACCIO ALCUNA OPERAZIONE
                        'NON DEVO CARICARE NESSUN INDIRIZZO
                    ElseIf CType(Me.Page.FindControl("daInserimento"), HiddenField).Value = 2 Then
                        'PROVENGO DA UN INSERIMENTO QUINDI CARICO LA STRUTTURA PER
                        'L'INSERIMENTO DI INDIRIZZI ED IBAN
                        lIdConnessione = CType(Me.Page.FindControl("IDConnessione"), HiddenField).Value
                        idCONNESSIONE2.Value = lIdConnessione
                        caricaIBAN(0)
                    Else
                        'STO ENTRANDO IN VISUALIZZAZIONE/MODIFICA
                        lIdConnessione = CType(Me.Page.FindControl("IDConnessione"), HiddenField).Value
                        idCONNESSIONE2.Value = lIdConnessione
                        caricaIBAN(0)
                    End If

                Catch ex As Exception
                    chiudiConnessione()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                    Response.Redirect("../../../Errore.aspx")
                End Try
            End If
            If Not IsNothing(Session.Item("ERROREMOD")) Then
                Session.Remove("ERROREMOD")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub chiudiConnessione()
        If Not IsNothing(Session.Item("LAVORAZIONE")) Then
            Session.Remove("LAVORAZIONE")
        End If

        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONEFORNITORI" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        If Not IsNothing(par.myTrans) Then
            '‘par.cmd.Transaction = par.myTrans
            par.myTrans.Rollback()
            par.myTrans.Dispose()
        End If
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Protected Sub riprendiConnessione()

        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

    End Sub

    Protected Sub riprendiTransazione()

        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONEFORNITORI" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans

    End Sub

    Protected Sub caricaIBAN(ByVal sl As Integer)
        Dim OpenNow As Boolean = False
        If sl = 0 And vIdFornitori > 0 Then
            riprendiConnessione()
            riprendiTransazione()
        Else
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            OpenNow = True
        End If

        dt.Columns.Clear()
        dt.Clear()

        '--------CARICO GLI IBAN DEL FORNITORE------'
        par.cmd.CommandText = "SELECT SISCOM_MI.FORNITORI_IBAN.ID AS IDB, SISCOM_MI.FORNITORI_IBAN.IBAN AS IB,SISCOM_MI.FORNITORI_IBAN.FL_ATTIVO AS FLA,SISCOM_MI.FORNITORI_IBAN.METODO_PAGAMENTO AS MP,SISCOM_MI.FORNITORI_IBAN.TIPO_PAGAMENTO AS TP FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_IBAN WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.FORNITORI_IBAN.ID_FORNITORE AND SISCOM_MI.FORNITORI.ID='" & vIdFornitori & "'"
        par.cmd.ExecuteNonQuery()
        dt.Columns.Add("ID")
        dt.Columns.Add("IBAN")
        dt.Columns.Add("FL_ATTIVO")
        dt.Columns.Add("METODO_PAGAMENTO")
        dt.Columns.Add("TIPO_PAGAMENTO")

        Dim RIGA As System.Data.DataRow

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read()
            RIGA = dt.NewRow()
            RIGA.Item("ID") = myReader("IDB")
            RIGA.Item("IBAN") = par.IfNull(myReader("IB"), "")
            If par.IfNull(myReader("FLA"), 0) = 1 Then
                RIGA.Item("FL_ATTIVO") = "Attivo"
            Else
                RIGA.Item("FL_ATTIVO") = "Non attivo"
            End If

            RIGA.Item("METODO_PAGAMENTO") = par.IfNull(myReader("MP"), "")
            RIGA.Item("TIPO_PAGAMENTO") = par.IfNull(myReader("TP"), "")
            dt.Rows.Add(RIGA)
        End While

        myReader.Close()
        DataGrid3.DataSource = dt
        DataGrid3.DataBind()
        '--------------------------------------'

        If sl = 0 Then

            If Session.Item("BP_CC_L") = 1 Then

                '---NASCONDO BOTTONI AGGIUNGI, MODIFICA, ELIMINA----'
                'btnAggIBAN.Visible = False
                'btnApriIBAN.Visible = False
                'btnEliminaIBAN.Visible = False
                '--------------------------------------'

            Else

                '---MOSTRO BOTTONI AGGIUNGI, MODIFICA, ELIMINA----'
                'btnAggIBAN.Visible = True
                'btnApriIBAN.Visible = True
                'btnEliminaIBAN.Visible = True
                '--------------------------------------'

            End If

        End If
        par.cmd.Dispose()
        If OpenNow = True Then
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End If
    End Sub

    Protected Sub caricaIBANSolaLettura()

        par.OracleConn.Open()
        par.cmd = par.OracleConn.CreateCommand

        dt.Columns.Clear()
        dt.Clear()

        '--------CARICO GLI IBAN DEL FORNITORE------'
        par.cmd.CommandText = "SELECT SISCOM_MI.FORNITORI_IBAN.ID AS IDB, SISCOM_MI.FORNITORI_IBAN.IBAN AS IB,SISCOM_MI.FORNITORI_IBAN.FL_ATTIVO AS FLA,SISCOM_MI.FORNITORI_IBAN.METODO_PAGAMENTO AS MP,SISCOM_MI.FORNITORI_IBAN.TIPO_PAGAMENTO AS TP FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_IBAN WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.FORNITORI_IBAN.ID_FORNITORE AND SISCOM_MI.FORNITORI.ID='" & vIdFornitori & "'"
        par.cmd.ExecuteNonQuery()
        dt.Columns.Add("ID")
        dt.Columns.Add("IBAN")
        dt.Columns.Add("FL_ATTIVO")
        dt.Columns.Add("METODO_PAGAMENTO")
        dt.Columns.Add("TIPO_PAGAMENTO")

        Dim RIGA As System.Data.DataRow

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read()
            RIGA = dt.NewRow()
            RIGA.Item("ID") = myReader("IDB")
            RIGA.Item("IBAN") = par.IfNull(myReader("IB"), "")
            If par.IfNull(myReader("FLA"), 0) = 1 Then
                RIGA.Item("FL_ATTIVO") = "Attivo"
            Else
                RIGA.Item("FL_ATTIVO") = "Non attivo"
            End If

            RIGA.Item("METODO_PAGAMENTO") = par.IfNull(myReader("MP"), "")
            RIGA.Item("TIPO_PAGAMENTO") = par.IfNull(myReader("TP"), "")
            dt.Rows.Add(RIGA)
        End While

        myReader.Close()
        DataGrid3.DataSource = dt
        DataGrid3.DataBind()
        '--------------------------------------'

        '---NASCONDO BOTTONI AGGIUNGI, MODIFICA, ELIMINA----'
        'btnAggIBAN.Visible = False
        'btnApriIBAN.Visible = False
        'btnEliminaIBAN.Visible = False
        '--------------------------------------'

        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    'Protected Sub btnEliminaIBAN_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaIBAN.Click
    '    Try
    '        '------CONTROLLO CONFERMA ELIMINAZIONE------'
    '        If confermaEliminazione.Value = "1" Then

    '            If IBANselezionato.Value <> "-1" Then

    '                '--------ELIMINAZIONE-------'
    '                riprendiConnessione()
    '                riprendiTransazione()
    '                par.cmd.CommandText = "DELETE FROM SISCOM_MI.FORNITORI_IBAN WHERE SISCOM_MI.FORNITORI_IBAN.ID='" & IBANselezionato.Value & "'"
    '                par.cmd.ExecuteNonQuery()
    '                par.cmd.Dispose()
    '                CType(Me.Page.FindControl("modificheEffettuate"), HiddenField).Value = "1"
    '                caricaIBAN(0)
    '            End If

    '        End If
    '        '-------------------------------------------'
    '    Catch exOracle As Oracle.DataAccess.Client.OracleException
    '        If exOracle.Number = 2292 Then
    '            'VINCOLO INTEGRITà REFERENZIALE
    '            '---MESSAGGIO---'
    '            Response.Write("<script>alert('L\'IBAN selezionato non può essere eliminato');</script>")

    '        End If
    '    Catch ex As Exception
    '        chiudiConnessione()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Redirect("../../../Errore.aspx")
    '    End Try


    'End Sub

    'Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound

    '    If CType(Me.Page.FindControl("modalitaSOLALETTURA"), HiddenField).Value <> 1 Then

    '        If e.Item.ItemType = ListItemType.Item Then
    '            '---------------------------------------------------         
    '            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '            '---------------------------------------------------         
    '            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer'")
    '            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
    '            e.Item.Attributes.Add("onclick", "if (Selezionato3) {Selezionato3.style.backgroundColor='';}Selezionato3=this;this.style.backgroundColor='red';document.getElementById('Tab_IBAN_txtRisIBAN').value='IBAN selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_IBAN_IBANselezionato').value='" & Replace(e.Item.Cells(0).Text, "'", "\'") & "';")

    '        End If
    '        If e.Item.ItemType = ListItemType.AlternatingItem Then
    '            '---------------------------------------------------         
    '            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '            '---------------------------------------------------         
    '            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer'")
    '            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
    '            e.Item.Attributes.Add("onclick", "if (Selezionato3) {Selezionato3.style.backgroundColor='';}Selezionato3=this;this.style.backgroundColor='red';document.getElementById('Tab_IBAN_txtRisIBAN').value='IBAN selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_IBAN_IBANselezionato').value='" & Replace(e.Item.Cells(0).Text, "'", "\'") & "';")

    '        End If

    '    End If

    'End Sub
    Protected Sub DataGrid3_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid3.ItemDataBound
        If CType(Me.Page.FindControl("modalitaSOLALETTURA"), HiddenField).Value <> 1 Then
            If e.Item.ItemType = ListItemType.Item Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                e.Item.Attributes.Add("onclick", "document.getElementById('Tab_IBAN_txtRisIBAN').value='IBAN selezionato: " & Replace(e.Item.Cells(par.IndRDGC(DataGrid3, "IBAN")).Text, "'", "\'") & "';document.getElementById('Tab_IBAN_IBANselezionato').value='" & Replace(e.Item.Cells(par.IndRDGC(DataGrid3, "ID")).Text, "'", "\'") & "';")
            End If
        End If
    End Sub

    Protected Sub DataGrid3_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid3.NeedDataSource
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand

            par.cmd.CommandText = "SELECT SISCOM_MI.FORNITORI_IBAN.ID AS IDB, SISCOM_MI.FORNITORI_IBAN.IBAN AS IB,SISCOM_MI.FORNITORI_IBAN.FL_ATTIVO AS FLA,SISCOM_MI.FORNITORI_IBAN.METODO_PAGAMENTO AS MP,SISCOM_MI.FORNITORI_IBAN.TIPO_PAGAMENTO AS TP FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_IBAN WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.FORNITORI_IBAN.ID_FORNITORE AND SISCOM_MI.FORNITORI.ID='" & vIdFornitori & "'"


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")
        End Try
    End Sub
End Class
