Imports Telerik.Web.UI

Partial Class Tab_Indirizzi

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
                'btnAggInd.Visible = False
                btnApriInd.Visible = False
                'btnEliminaInd.Visible = False
            End If
            '------------------------------------------'

            If Session.Item("ERROREMOD") <> 1 Then
                Try
                    '------IMPOSTO IDCONNESSIONE E IDFORNITORE-------'
                    vIdFornitori = CType(Me.Page.FindControl("IDFornitore"), HiddenField).Value
                    idFORNITORE1.Value = vIdFornitori



                    '---SE SONO IN MODALITà DI SOLA LETTURA CARICO GLI INDIRIZZI IN SOLA LETTURA
                    '---APRO E CHIUDO CONNESSIONE
                    If CType(Me.Page.FindControl("modalitaSOLALETTURA"), HiddenField).Value = 1 Then
                        caricaIndirizziSolaLettura()
                    ElseIf CType(Me.Page.FindControl("daInserimento"), HiddenField).Value = 1 Then
                        'NON FACCIO ALCUNA OPERAZIONE
                        'NON DEVO CARICARE NESSUN INDIRIZZO
                    ElseIf CType(Me.Page.FindControl("daInserimento"), HiddenField).Value = 2 Then
                        'PROVENGO DA UN INSERIMENTO QUINDI CARICO LA STRUTTURA PER
                        'L'INSERIMENTO DI INDIRIZZI ED IBAN COME VISUALIZZAZIONE/MODIFICA
                        lIdConnessione = CType(Me.Page.FindControl("IDConnessione"), HiddenField).Value
                        idCONNESSIONE1.Value = lIdConnessione
                        caricaIndirizzi(0)
                    Else
                        'STO ENTRANDO IN VISUALIZZAZIONE/MODIFICA
                        lIdConnessione = CType(Me.Page.FindControl("IDConnessione"), HiddenField).Value
                        idCONNESSIONE1.Value = lIdConnessione
                        caricaIndirizzi(0)
                    End If

                Catch ex As Exception
                    chiudiConnessione()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                    Response.Redirect("../../../Errore.aspx")
                End Try
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

    Protected Sub caricaIndirizzi(ByVal sl As Integer)
        Dim OpenNow As Boolean = False
        If sl = 0 And vIdFornitori > 0 Then
            riprendiConnessione()
            riprendiTransazione()
        Else
            '---SOLA LETTURA, LA CONNESSIONE è CHIUSA---'
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            OpenNow = True
        End If
        dt.Columns.Clear()
        dt.Clear()

        '--------CARICO GLI INDIRIZZI DEL FORNITORE------'
        par.cmd.CommandText = "SELECT SISCOM_MI.FORNITORI_INDIRIZZI.* FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_INDIRIZZI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.FORNITORI_INDIRIZZI.ID_FORNITORE AND SISCOM_MI.FORNITORI.ID='" & vIdFornitori & "'"
        par.cmd.ExecuteNonQuery()

        dt.Columns.Add("ID")
        dt.Columns.Add("INDIRIZZO")
        dt.Columns.Add("CAP")
        dt.Columns.Add("COMUNE")
        dt.Columns.Add("PR")
        dt.Columns.Add("TELEFONO_1")
        dt.Columns.Add("TELEFONO_2")
        dt.Columns.Add("FAX")
        dt.Columns.Add("EMAIL")

        Dim RIGA As System.Data.DataRow

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read()
            RIGA = dt.NewRow()
            RIGA.Item("ID") = myReader("ID")
            RIGA.Item("INDIRIZZO") = par.IfNull(myReader("INDIRIZZO"), "")
            RIGA.Item("CAP") = par.IfNull(myReader("CAP"), "")
            RIGA.Item("COMUNE") = par.IfNull(myReader("COMUNE"), "")
            RIGA.Item("PR") = par.IfNull(myReader("PR"), "")
            RIGA.Item("TELEFONO_1") = par.IfNull(myReader("TELEFONO_1"), "")
            RIGA.Item("TELEFONO_2") = par.IfNull(myReader("TELEFONO_2"), "")
            RIGA.Item("FAX") = par.IfNull(myReader("FAX"), "")
            RIGA.Item("EMAIL") = par.IfNull(myReader("EMAIL"), "")
            dt.Rows.Add(RIGA)
        End While
        myReader.Close()
        DataGrid3.DataSource = dt
        DataGrid3.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        If sl = 0 Then
            If Session.Item("BP_CC_L") = 1 Then
                '---NASCONDO BOTTONI AGGIUNGI, MODIFICA, ELIMINA----'
                'btnAggInd.Visible = False
                btnApriInd.Visible = False
                'btnEliminaInd.Visible = False
                '--------------------------------------'
            Else
                '---MOSTRO BOTTONI AGGIUNGI, MODIFICA, ELIMINA----'
                'btnAggInd.Visible = True
                btnApriInd.Visible = True
                'btnEliminaInd.Visible = True
                '--------------------------------------'
            End If
        End If
        par.cmd.Dispose()
        If OpenNow = True Then
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End If
    End Sub

    Protected Sub caricaIndirizziSolaLettura()
        par.OracleConn.Open()
        par.cmd = par.OracleConn.CreateCommand
        dt.Columns.Clear()
        dt.Clear()
        '--------CARICO GLI INDIRIZZI DEL FORNITORE------'
        par.cmd.CommandText = "SELECT SISCOM_MI.FORNITORI_INDIRIZZI.* FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_INDIRIZZI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.FORNITORI_INDIRIZZI.ID_FORNITORE AND SISCOM_MI.FORNITORI.ID='" & vIdFornitori & "'"
        par.cmd.ExecuteNonQuery()
        dt.Columns.Add("ID")
        dt.Columns.Add("INDIRIZZO")
        dt.Columns.Add("CAP")
        dt.Columns.Add("COMUNE")
        dt.Columns.Add("PR")
        dt.Columns.Add("TELEFONO_1")
        dt.Columns.Add("TELEFONO_2")
        dt.Columns.Add("FAX")
        dt.Columns.Add("EMAIL")

        Dim RIGA As System.Data.DataRow
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read()
            RIGA = dt.NewRow()
            RIGA.Item("ID") = myReader("ID")
            RIGA.Item("INDIRIZZO") = par.IfNull(myReader("INDIRIZZO"), "")
            RIGA.Item("CAP") = par.IfNull(myReader("CAP"), "")
            RIGA.Item("COMUNE") = par.IfNull(myReader("COMUNE"), "")
            RIGA.Item("PR") = par.IfNull(myReader("PR"), "")
            RIGA.Item("TELEFONO_1") = par.IfNull(myReader("TELEFONO_1"), "")
            RIGA.Item("TELEFONO_2") = par.IfNull(myReader("TELEFONO_2"), "")
            RIGA.Item("FAX") = par.IfNull(myReader("FAX"), "")
            RIGA.Item("EMAIL") = par.IfNull(myReader("EMAIL"), "")
            dt.Rows.Add(RIGA)
        End While

        myReader.Close()
        DataGrid3.DataSource = dt
        DataGrid3.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        '---NASCONDO BOTTONI AGGIUNGI, MODIFICA, ELIMINA----'
        'btnAggInd.Visible = False
        btnApriInd.Visible = False
        'btnEliminaInd.Visible = False
        '--------------------------------------'
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

 

  
  

    'Protected Sub btnEliminaInd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaInd.Click
    '    Try
    '        '---CONTROLLO CONFERMA ELIMINAZIONE---'
    '        If confermaEliminazioneIndirizzo.Value = "1" Then
    '            If INDIRIZZOselezionato.Value <> "-1" Then
    '                '---ELIMINAZIONE---'
    '                riprendiConnessione()
    '                riprendiTransazione()
    '                par.cmd.CommandText = "DELETE FROM SISCOM_MI.FORNITORI_INDIRIZZI WHERE SISCOM_MI.FORNITORI_INDIRIZZI.ID='" & INDIRIZZOselezionato.Value & "'"
    '                par.cmd.ExecuteNonQuery()
    '                par.cmd.Dispose()
    '                CType(Me.Page.FindControl("modificheEffettuate"), HiddenField).Value = "1"
    '                caricaIndirizzi(0)
    '            End If
    '        End If
    '    Catch exOracle As Oracle.DataAccess.Client.OracleException
    '        If exOracle.Number = 2292 Then
    '            'VINCOLO INTEGRITà REFERENZIALE
    '            '---MESSAGGIO---'
    '            'ELIMINAZIONE NON POSSIBILE PER I VINCOLI DI INTEGRITà REFERENZIALE
    '            Response.Write("<script>alert('L\'indirizzo selezionato non può essere eliminato');</script>")

    '        End If
    '    Catch ex As Exception
    '        chiudiConnessione()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Redirect("../../../Errore.aspx")
    '    End Try

    'End Sub

    Protected Sub DataGrid3_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('Tab_Indirizzi_txtRisIndirizzi').value='Indirizzo selezionato: " & Replace(e.Item.Cells(par.IndRDGC(DataGrid3, "INDIRIZZO")).Text, "'", "\'") & "';document.getElementById('Tab_Indirizzi_INDIRIZZOselezionato').value='" & Replace(e.Item.Cells(par.IndRDGC(DataGrid3, "ID")).Text, "'", "\'") & "';")

        End If
    End Sub

    Protected Sub btnApriInd_Click(sender As Object, e As System.EventArgs) Handles btnApriInd.Click
        DataGrid3.Rebind()
    End Sub

    Protected Sub DataGrid3_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid3.NeedDataSource
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
        
            par.cmd.CommandText = "SELECT SISCOM_MI.FORNITORI_INDIRIZZI.* FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_INDIRIZZI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.FORNITORI_INDIRIZZI.ID_FORNITORE AND SISCOM_MI.FORNITORI.ID='" & vIdFornitori & "'"
       
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")
        End Try
    End Sub

    Private Sub ApriIndirizzo()
        Throw New NotImplementedException
    End Sub

End Class
