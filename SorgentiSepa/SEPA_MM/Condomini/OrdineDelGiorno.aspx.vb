
Partial Class Condomini_OrdineDelGiorno
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property dt() As Data.DataTable
        Get
            If Not (ViewState("dt") Is Nothing) Then
                Return ViewState("dt")
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dt") = value
        End Set
    End Property
    Public Property vIdConnModale() As String
        Get
            If Not (ViewState("par_vIdConnModale") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnModale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnModale") = value
        End Set

    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Not IsPostBack Then
            If Request.QueryString("IDCON") <> "" Then
                vIdConnModale = Request.QueryString("IDCON")
            End If

            CaricaFinestra()
            If Request.QueryString("SL") <> 1 Then
                Me.btnAdd.Visible = False
                Me.btnDelete.Visible = False
                Me.btnSalva.Visible = False
            End If
        End If

    End Sub
    Private Sub CaricaFinestra()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            idSelected.Value = 0
            If String.IsNullOrEmpty(Me.lblAmministratore.Text) Then

                par.cmd.CommandText = "SELECT condomini.denominazione AS condominio,gestione_inizio,gestione_fine, cond_amministratori.cognome,cond_amministratori.nome FROM " _
                                    & "siscom_mi.cond_convocazioni, siscom_mi.condomini, siscom_mi.cond_amministrazione, siscom_mi.cond_amministratori " _
                                    & "WHERE(cond_amministrazione.id_condominio = condomini.ID) " _
                                    & "AND cond_amministrazione.id_amministratore = cond_amministratori.ID " _
                                    & "AND cond_amministrazione.data_fine IS NULL " _
                                    & "AND cond_convocazioni.id_condominio = condomini.ID " _
                                    & "AND cond_convocazioni.ID = " & Request.QueryString("IDCONV")
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                If lettore.Read Then
                    Me.lblAmministratore.Text = "Amministratore: Sig." & par.IfNull(lettore("cognome"), "") & " " & par.IfNull(lettore("nome"), "") & " - Condominio: " & par.IfNull(lettore("condominio"), "") & " - Gestione: " & Replace(par.FormattaData("2000" & lettore("GESTIONE_INIZIO").ToString), "/2000", "") & " - " & Replace(par.FormattaData("2000" & lettore("GESTIONE_FINE").ToString), "/2000", "")
                    ' Me.lblCondGestione.Text = " - Condominio: " & par.IfNull(lettore("condominio"), "") & " - Gestione: " & Replace(par.FormattaData("2000" & lettore("GESTIONE_INIZIO").ToString), "/2000", "") & "-" & Replace(par.FormattaData("2000" & lettore("GESTIONE_FINE").ToString), "/2000", "")
                    Me.gestInizio.Value = Replace(par.FormattaData("2000" & lettore("GESTIONE_INIZIO").ToString), "/2000", "")
                    Me.gestFine.Value = Replace(par.FormattaData("2000" & lettore("GESTIONE_FINE").ToString), "/2000", "")
                End If
                lettore.Close()
            End If

            par.cmd.CommandText = "select ID,ID_CONVOCAZIONE,COD_ORDINE,ANNO,(case when SI_NO is null then -1 else si_no end) as SI_NO,NOTE from siscom_mi.cond_ord_giorno where id_convocazione = " & Request.QueryString("IDCONV")
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                AggiungiRiga(dt)
            End If

            DataGridOrdGiorno.DataSource = dt
            DataGridOrdGiorno.DataBind()
            AddJavascriptFunction()


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
        Dim Ev As Boolean = False
        If dt.Select("ID = -1").Length > 0 Then
            SalvaDati(Ev)
        Else
            Ev = True
        End If
        If Ev = True Then
            AggiungiRiga(dt)
            DataGridOrdGiorno.DataSource = dt
            DataGridOrdGiorno.DataBind()
            AddJavascriptFunction()
        Else

        End If


    End Sub




    Private Sub AggiungiRiga(ByVal dt As Data.DataTable)
        Try
            Dim riga As Data.DataRow
            riga = dt.NewRow()
            riga.Item("ID") = -1
            riga.Item("ID_CONVOCAZIONE") = Request.QueryString("IDCONV")
            riga.Item("COD_ORDINE") = "-1"
            riga.Item("ANNO") = ""
            riga.Item("SI_NO") = "-1"
            riga.Item("NOTE") = ""
            dt.Rows.Add(riga)


        Catch ex As Exception

        End Try
    End Sub
    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next

        Catch ex As Exception
        End Try

        Return TrovaIndiceColonna

    End Function

    Protected Sub DataGridCondom_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridOrdGiorno.ItemCreated

        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                'CType(e.Item.FindControl("cmbOrdine"), DropDownList).Items.Clear()
                'CType(e.Item.FindControl("cmbSiNo"), DropDownList).ClearSelection()
                par.caricaComboBox("select id,descrizione from siscom_mi.tipo_ord_giorno", CType(e.Item.FindControl("cmbOrdine"), DropDownList), "ID", "DESCRIZIONE", True)

            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        'Response.Write("<script>alert('Funzione non ancora disponibile!');</script>")
        Dim Ev As Boolean = False
        SalvaDati(Ev)
        If Ev = True Then
            Response.Write("<script>alert('Operazione completata correttamente!');</script>")

        End If

    End Sub
    Private Sub SalvaDati(ByRef ev As Boolean)
        If ControllaCampi() = True Then
            Dim anno As String
            Dim codOrdine As String
            Dim SiNo As String
            Dim note As String
            Dim GestioneInizio As String = ""
            Dim GestioneFine As String = ""

            Dim eventoModifica = False
            Dim eventoInserimento = False

            For Each item As DataGridItem In DataGridOrdGiorno.Items
                anno = CType(item.FindControl("txtAnno"), TextBox).Text
                codOrdine = RitornaNullSeMenoUno(CType(item.FindControl("cmbOrdine"), DropDownList).SelectedValue)
                SiNo = RitornaNullSeMenoUno(CType(item.FindControl("cmbSiNo"), DropDownList).SelectedValue)
                note = CType(item.FindControl("txtNote"), TextBox).Text.ToUpper

                If gestInizio.Value = "01/01" AndAlso gestFine.Value = "31/12" AndAlso anno <> "" Then
                    GestioneInizio = gestInizio.Value & "/" & anno
                    GestioneFine = gestFine.Value & "/" & anno

                ElseIf anno <> "" Then
                    GestioneInizio = gestInizio.Value & "/" & anno
                    GestioneFine = gestFine.Value & "/" & anno + 1

                End If
                If item.Cells(TrovaIndiceColonna(DataGridOrdGiorno, "ID")).Text < 0 Then
                    '************ salva insert *********************
                    par.cmd.CommandText = "insert into siscom_mi.cond_ord_giorno (id,id_convocazione,cod_ordine,anno,si_no,note,data_inizio,data_fine) values " _
                                        & "(siscom_mi.SEQ_COND_ORD_GIORNO.nextval," & Request.QueryString("IDCONV") & ", " _
                                        & "" & codOrdine & ",'" & par.IfNull(anno, "") & "'," & par.IfNull(SiNo, "") & ",'" & par.PulisciStrSql(note) & "','" & par.AggiustaData(GestioneInizio) & "','" & par.AggiustaData(GestioneFine) & "')"
                    par.cmd.ExecuteNonQuery()

                    eventoInserimento = True
                    
                Else
                    '************ salva update *********************
                    par.cmd.CommandText = "update siscom_mi.cond_ord_giorno set cod_ordine = " & codOrdine & ", anno = '" & par.IfNull(anno, "") & "', si_no = " & par.IfNull(SiNo, "") & ", note = '" & par.PulisciStrSql(note) & "' " _
                                        & ",data_inizio='" & par.AggiustaData(GestioneInizio) & "',data_fine='" & par.AggiustaData(GestioneFine) & "'" _
                                        & "where id = " & item.Cells(TrovaIndiceColonna(DataGridOrdGiorno, "ID")).Text
                    par.cmd.ExecuteNonQuery()

                    eventoModifica = True

                    
                End If

                ev = True
            Next

            If eventoInserimento = True Then
                eventoInserimento = False
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOM") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F36','INSERIMENTO ORDINE DEL GIORNO')"
                par.cmd.ExecuteNonQuery()
            ElseIf eventoModifica = True Then
                eventoModifica = False
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOM") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F36','MODIFICA ORDINE DEL GIORNO')"
                par.cmd.ExecuteNonQuery()
            End If

            CaricaFinestra()
        End If

    End Sub
    Private Function ControllaCampi() As Boolean
        ControllaCampi = True

        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            Dim lista As New Generic.SortedList(Of Integer, datiObbligatori)

            par.cmd.CommandText = "select * from siscom_mi.tipo_ord_giorno "
            Dim dicControllo As New Generic.Dictionary(Of Integer, String)
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                Dim ob As New datiObbligatori
                ob.id = par.IfNull(lettore("id"), -1)
                ob.annoObb = par.IfNull(lettore("ANNO"), 0) = 1
                ob.SiNoObb = par.IfNull(lettore("SI_NO"), 0) = 1
                lista.Add(ob.id, ob)
            End While
            lettore.Close()

            For Each i As DataGridItem In DataGridOrdGiorno.Items

                Dim id As Integer = CType(i.FindControl("cmbOrdine"), DropDownList).SelectedValue
                If id <> -1 Then

                    If lista.ContainsKey(id) Then
                        Dim ob As New datiObbligatori

                        ob = lista(id)
                        If ob.SiNoObb Then
                            If CType(i.FindControl("cmbSiNo"), DropDownList).SelectedValue = "-1" Then
                                ControllaCampi = False
                                Response.Write("<script>alert('ATTENZIONE!Avvalorare il campo Si/No!');</script>")
                                Exit For
                            End If
                        End If
                        If ob.annoObb Then
                            If String.IsNullOrEmpty(CType(i.FindControl("txtAnno"), TextBox).Text) Then
                                ControllaCampi = False
                                Response.Write("<script>alert('ATTENZIONE!Avvalorare il campo Anno!');</script>")
                                Exit For
                            End If

                        End If
                    End If
                Else
                    ControllaCampi = False
                    Response.Write("<script>alert('ATTENZIONE!Avvalorare il campo Ordine del Giorno!');</script>")
                    Exit For

                End If

            Next



        Catch ex As Exception

        End Try
        Return ControllaCampi
    End Function
    Private Function RitornaNullSeMenoUno(ByVal i As Integer) As String
        RitornaNullSeMenoUno = i

        Try

            If i = -1 Then
                RitornaNullSeMenoUno = "null"
            End If


        Catch ex As Exception

        End Try


    End Function

    Protected Sub DataGridOrdGiorno_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridOrdGiorno.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#E7E7FF'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='white'}Selezionato=this;this.style.backgroundColor='red';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='#E7E7FF'}Selezionato=this;this.style.backgroundColor='red';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';")
        End If

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        EliminaRiga()
    End Sub
    Private Sub EliminaRiga()
        Try
            If idSelected.Value > 0 Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "delete from siscom_mi.cond_ord_giorno where id = " & idSelected.Value
                par.cmd.ExecuteNonQuery()

                CaricaFinestra()
                idSelected.Value = 0
            Else
                Response.Write("<script>alert('Seleziona la riga per eliminarla!');</script>")

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub AddJavascriptFunction()
        Try
            For Each item As DataGridItem In DataGridOrdGiorno.Items
                CType(item.FindControl("txtAnno"), TextBox).Attributes.Add("onkeyup", "javascript:valid(this,'onlynumbers');")
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class


Class datiObbligatori
    Public Property id As Integer
    Public Property annoObb As Boolean
    Public Property SiNoObb As Boolean


End Class