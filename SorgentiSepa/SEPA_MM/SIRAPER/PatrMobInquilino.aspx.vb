
Partial Class SIRAPER_PatrMobInquilino
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Public Property dataTablePatrMobiliare() As Data.DataTable
        Get
            If Not (ViewState("dataTablePatrMobiliare") Is Nothing) Then
                Return ViewState("dataTablePatrMobiliare")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTablePatrMobiliare") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        idConnessione.Value = Request.QueryString("IdConnessione")
        sescon.Value = Request.QueryString("SESCON")
        If IsNothing(HttpContext.Current.Session.Item(sescon.Value & idConnessione.Value)) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item(sescon.Value & idConnessione.Value), CM.datiConnessione)
            par.cmd = par.OracleConn.CreateCommand()
        End If
        If Not IsPostBack Then
            IdInquilino.Value = Request.QueryString("ID")
            idSiraper.Value = Request.QueryString("IDS")
            idSiraperVersione.Value = Request.QueryString("IDSV")
            RiempiDati()
            If Request.QueryString("SLE").ToString = "1" Then
                btnInserisci.Visible = False
                btnDelete.Visible = False
                btnProcedi.Enabled = False
            End If
        End If
    End Sub
    Private Sub RiempiDati()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT COGNOME, NOME FROM SISCOM_MI.SIR_INQUILINO WHERE ID = " & IdInquilino.Value
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                lblTitolo.Text = "PATRIMONIO MOBILIARE INQUILINO: " & par.IfNull(MyReader("COGNOME"), "") & " " & par.IfNull(MyReader("NOME"), "")
            End If
            MyReader.Close()
            CaricaDataGrid()
            ControlliDataGrid()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - RiempiDati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaDataGrid()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT ID, ANNO_RIFERIMENTO, CODICE_GESTORE, DENOMINAZIONE_GESTORE, TRIM(TO_CHAR(IMPORTO_PATRIMONIO, '9G999G999G999G999G990D99')) AS IMPORTO_PATRIMONIO, TRIM(TO_CHAR(VALORE_PATRIMONIO, '9G999G999G999G999G990D99')) AS VALORE_PATRIMONIO " _
                                & "FROM SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE " _
                                & "WHERE ID_INQUILINO = " & IdInquilino.Value & " AND ID_SIRAPER = " & idSiraper.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dataTablePatrMobiliare = New Data.DataTable
            da.Fill(dataTablePatrMobiliare)
            da.Dispose()
            BindGrid()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - CaricaDataGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ControlliDataGrid()
        Try
            For Each Items As DataGridItem In dgvPatrMobInqui.Items
                SettaControlModifiche(Items)
                CType(Items.FindControl("txtannorif"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
                CType(Items.FindControl("txtvalore"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                CType(Items.FindControl("txtvalore"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Items.FindControl("txtimporto"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                CType(Items.FindControl("txtimporto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - ControlliDataGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dgvPatrMobInqui.DataSource = dataTablePatrMobiliare
            dgvPatrMobInqui.DataBind()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('frmModify').value='1';")
            End If
        Next
    End Sub
    Private Sub AggiustaCompSessionePatrMobiliare()
        Try
            Dim row As Data.DataRow
            For i As Integer = 0 To dgvPatrMobInqui.Items.Count - 1
                row = dataTablePatrMobiliare.Select("id = " & dgvPatrMobInqui.Items(i).Cells(0).Text)(0)
                row.Item("ANNO_RIFERIMENTO") = par.IfEmpty(CType(dgvPatrMobInqui.Items(i).FindControl("txtannorif"), TextBox).Text.ToUpper, DBNull.Value)
                row.Item("CODICE_GESTORE") = par.IfEmpty(CType(dgvPatrMobInqui.Items(i).FindControl("txtcodintermediario"), TextBox).Text.ToUpper, DBNull.Value)
                row.Item("DENOMINAZIONE_GESTORE") = par.IfEmpty(CType(dgvPatrMobInqui.Items(i).FindControl("txtdenintermediario"), TextBox).Text.ToUpper, DBNull.Value)
                row.Item("IMPORTO_PATRIMONIO") = par.IfEmpty(CType(dgvPatrMobInqui.Items(i).FindControl("txtimporto"), TextBox).Text.ToUpper, DBNull.Value)
                row.Item("VALORE_PATRIMONIO") = par.IfEmpty(CType(dgvPatrMobInqui.Items(i).FindControl("txtvalore"), TextBox).Text.ToUpper, DBNull.Value)
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - AggiustaCompSessionePatrMobiliare - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnInserisci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnInserisci.Click
        Try
            AggiustaCompSessionePatrMobiliare()
            Select Case idSiraperVersione.Value.ToString
                Case "1"
                    If ControlloDatiDT() Then
                        AggiungiRiga()
                    End If
                Case "2"
                    AggiungiRiga()
            End Select
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - btnInserisci_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function ControlloDatiDT() As Boolean
        Try
            ControlloDatiDT = False
            For Each row As Data.DataRow In dataTablePatrMobiliare.Rows
                If String.IsNullOrEmpty(par.IfNull(row.Item("ANNO_RIFERIMENTO"), "")) Or String.IsNullOrEmpty(par.IfNull(row.Item("CODICE_GESTORE"), "")) Or String.IsNullOrEmpty(par.IfNull(row.Item("DENOMINAZIONE_GESTORE"), "")) Or String.IsNullOrEmpty(par.IfNull(row.Item("IMPORTO_PATRIMONIO"), "")) Or String.IsNullOrEmpty(par.IfNull(row.Item("VALORE_PATRIMONIO"), "")) Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Completare tutti i dati prima di procedere all\'inserimento di un nuovo patrimonio mobiliare!');", True)
                    Exit Function
                Else
                    If Len(par.IfNull(row.Item("ANNO_RIFERIMENTO"), "").ToString) <> 4 Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('L\'anno di riferimento non è corretto!');", True)
                        Exit Function
                    Else
                        If par.IfNull(row.Item("ANNO_RIFERIMENTO"), "") < 1990 Then
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('L\'anno di riferimento non può essere minore del 1990!');", True)
                            Exit Function
                        Else
                            If par.IfNull(row.Item("ANNO_RIFERIMENTO"), "") > Format(Now, "yyyy") Then
                                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('L\'anno di riferimento non può essere minore del " & Format(Now, "yyyy") & "!');", True)
                                Exit Function
                            End If
                        End If
                    End If
                End If
            Next
            ControlloDatiDT = True
        Catch ex As Exception
            ControlloDatiDT = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - btnInserisci_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Sub AggiungiRiga()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            Dim idPatr As Long = 0
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SIR_INQUILINO_PATR_MOBIL.NEXTVAL FROM DUAL"
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                idPatr = par.IfNull(MyReader(0), -1)
            End If
            MyReader.Close()
            Dim row As Data.DataRow
            row = dataTablePatrMobiliare.NewRow()
            row.Item("ID") = idPatr
            row.Item("ANNO_RIFERIMENTO") = DBNull.Value
            row.Item("CODICE_GESTORE") = DBNull.Value
            row.Item("DENOMINAZIONE_GESTORE") = DBNull.Value
            row.Item("IMPORTO_PATRIMONIO") = DBNull.Value
            row.Item("VALORE_PATRIMONIO") = DBNull.Value
            dataTablePatrMobiliare.Rows.Add(row)
            BindGrid()
            ControlliDataGrid()
            frmModify.Value = 1
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - AggiungiRiga - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvPatrMobInqui_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvPatrMobInqui.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            Dim stringaselezione As String = "Hai selezionato il patrimonio mobiliare: " & par.IfEmpty(CType(e.Item.Cells(1).FindControl("txtannorif"), TextBox).Text, "").ToString.Replace("'", "\'") & ", " _
                                           & "Codice: " & par.IfEmpty(CType(e.Item.Cells(2).FindControl("txtcodintermediario"), TextBox).Text, "").ToString.Replace("'", "\'") & ", " _
                                           & "Denominazione: " & par.IfEmpty(CType(e.Item.Cells(3).FindControl("txtdenintermediario"), TextBox).Text, "").ToString.Replace("'", "\'") & ", " _
                                           & "Importo: " & par.IfEmpty(CType(e.Item.Cells(4).FindControl("txtimporto"), TextBox).Text, "").ToString.Replace("'", "\'") & ", " _
                                           & "Valore: " & par.IfEmpty(CType(e.Item.Cells(5).FindControl("txtvalore"), TextBox).Text, "").ToString.Replace("'", "\'")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('idSelected').value='" & e.Item.Cells(0).Text & "';" _
                                & "document.getElementById('txtmia').value='" & stringaselezione & "';")
            
        End If
    End Sub
    Protected Sub btnDelete_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        Try
            AggiustaCompSessionePatrMobiliare()
            CancellaRiga()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - btnDelete_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CancellaRiga()
        Try
            If ConfEliminaPatrimonio.Value = 1 Then
                If idSelected.Value <> 0 Then
                    Dim i As Integer = 0
                    For Each r As Data.DataRow In dataTablePatrMobiliare.Rows
                        If r.Item("ID") = idSelected.Value Then
                            Exit For
                        End If
                        i += 1
                    Next
                    dataTablePatrMobiliare.Rows(i).Delete()
                    If dataTablePatrMobiliare.Rows.Count > 0 Then
                        dataTablePatrMobiliare.Rows(i).AcceptChanges()
                    End If
                    BindGrid()
                    ControlliDataGrid()
                    txtmia.Text = "Nessuna Selezione"
                    idSelected.Value = 0
                    frmModify.Value = 1
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Patrimonio Eliminato!');", True)
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - CancellaRiga - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            AggiustaCompSessionePatrMobiliare()
            Select Case idSiraperVersione.Value.ToString
                Case "1"
                    If ControlloDatiDT() Then
                        SalvaDati()
                    End If
                Case "2"
                    SalvaDati()
            End Select
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - btnProcedi_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SalvaDati()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader
            If dataTablePatrMobiliare.Rows.Count = 0 Then
                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE WHERE ID_INQUILINO = " & IdInquilino.Value & " AND ID_SIRAPER = " & idSiraper.Value
                MyReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE WHERE ID_INQUILINO = " & IdInquilino.Value & " AND ID_SIRAPER = " & idSiraper.Value
                    par.cmd.ExecuteNonQuery()
                End If
                MyReader.Close()
            Else
                Dim ListaPatrimonio As String = ""
                Dim PrimoLista As Boolean = True
                For Each row As Data.DataRow In dataTablePatrMobiliare.Rows
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE WHERE ID = " & par.IfNull(row.Item("ID"), "-1") & " AND ID_INQUILINO = " & IdInquilino.Value & " AND ID_SIRAPER = " & idSiraper.Value
                    MyReader = par.cmd.ExecuteReader
                    If MyReader.Read Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE SET ANNO_RIFERIMENTO = " & par.IfNull(row.Item("ANNO_RIFERIMENTO"), "null") & ", " _
                                            & "CODICE_GESTORE = '" & par.PulisciStrSql(par.IfNull(row.Item("CODICE_GESTORE"), "")).ToUpper & "', " _
                                            & "DENOMINAZIONE_GESTORE = '" & par.PulisciStrSql(par.IfNull(row.Item("DENOMINAZIONE_GESTORE"), "")).ToUpper & "', " _
                                            & "IMPORTO_PATRIMONIO = " & NumeriNull(Math.Round(CDec(par.IfNull(row.Item("IMPORTO_PATRIMONIO").ToString.Replace(".", ""), 0)), 2)) & ", " _
                                            & "VALORE_PATRIMONIO = " & NumeriNull(Math.Round(CDec(par.IfNull(row.Item("VALORE_PATRIMONIO").ToString.Replace(".", ""), 0)), 2)) & " " _
                                            & "WHERE ID = " & par.IfNull(row.Item("ID"), "-1") & " AND ID_INQUILINO = " & IdInquilino.Value & " AND ID_SIRAPER = " & idSiraper.Value
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE (ID, ID_SIRAPER, ID_INQUILINO, ANNO_RIFERIMENTO, CODICE_GESTORE, DENOMINAZIONE_GESTORE, IMPORTO_PATRIMONIO, VALORE_PATRIMONIO) VALUES " _
                                            & "(" & par.IfNull(row.Item("ID"), "-1") & ", " & idSiraper.Value & ", " & IdInquilino.Value & ", " & par.IfNull(row.Item("ANNO_RIFERIMENTO"), "null") & ", " _
                                            & "'" & par.PulisciStrSql(par.IfNull(row.Item("CODICE_GESTORE"), "")).ToUpper & "', '" & par.PulisciStrSql(par.IfNull(row.Item("DENOMINAZIONE_GESTORE"), "")).ToUpper & "', " _
                                            & NumeriNull(Math.Round(CDec(par.IfNull(row.Item("IMPORTO_PATRIMONIO").ToString.Replace(".", ""), 0)), 2)) & ", " & NumeriNull(Math.Round(CDec(par.IfNull(row.Item("VALORE_PATRIMONIO").ToString.Replace(".", ""), 0)), 2)) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    MyReader.Close()
                    If PrimoLista Then
                        ListaPatrimonio = par.IfNull(row.Item("ID"), "-1")
                        PrimoLista = False
                    Else
                        ListaPatrimonio = ListaPatrimonio & ", " & par.IfNull(row.Item("ID"), "-1")
                    End If
                Next
                If Not String.IsNullOrEmpty(ListaPatrimonio) Then
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE WHERE ID_INQUILINO = " & IdInquilino.Value & " AND ID_SIRAPER = " & idSiraper.Value & " AND ID NOT IN (" & ListaPatrimonio & ")"
                    MyReader = par.cmd.ExecuteReader
                    If MyReader.Read Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE WHERE ID_INQUILINO = " & IdInquilino.Value & " AND ID_SIRAPER = " & idSiraper.Value & " AND ID NOT IN (" & ListaPatrimonio & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    MyReader.Close()
                End If
            End If
            Session.Add("FRMODIFY", 1)
            frmModify.Value = 0
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER_EVENTI (ID_SIRAPER, ID_OPERATORE, DATA_ORA, COD_EVENTO, DESCRIZIONE) VALUES " _
                                & "(" & idSiraper.Value & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'S03', " _
                                & "'AGGIORNAMENTO " & par.PulisciStrSql(lblTitolo.Text) & "')"
            par.cmd.ExecuteNonQuery()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Operazione Completata!');", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_PatrimonioMobInq - SalvaDati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function NumeriNull(ByVal Numero As Decimal) As String
        Try
            NumeriNull = "null"
            If String.IsNullOrEmpty(Numero.ToString) Then
                NumeriNull = "null"
            Else
                If Numero = 0 Then
                    ' NumeriNull = "null"
                    NumeriNull = Numero
                Else
                    NumeriNull = (par.VirgoleInPunti(Numero)).ToString
                End If
            End If
        Catch ex As Exception
            NumeriNull = "null"
        End Try
    End Function
End Class
