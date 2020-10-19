
Partial Class Condomini_DettMorInquilini
    Inherits PageSetIdMode
    Dim par As New CM.Global

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
    Public Property vIdMorosita() As String
        Get
            If Not (ViewState("par_vIdMorosita") Is Nothing) Then
                Return CStr(ViewState("par_vIdMorosita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdMorosita") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Not IsPostBack Then
            Try

                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                End If

                If Request.QueryString("IDCON") <> "" Then
                    vIdConnModale = Request.QueryString("IDCON")
                End If

                If Request.QueryString("DATADOC") <> "" Then
                    txtDataDocumentazione.Text = Request.QueryString("DATADOC")
                    ControllaPresenzaDataDoc(txtDataDocumentazione.Text)
                End If


                If Request.QueryString("IDMOROSITA") <> "" Then
                    vIdMorosita = Request.QueryString("IDMOROSITA")
                End If

                RicercaDettagliMorosita()
                ElencoInquilini()

                If Request.QueryString("SL") = 1 Then
                    Me.btnSalvaDettMorInquilini.Visible = False
                    Me.btnDeleteInquilini.Visible = False
                    Me.ReadOnlyxMoro.Value = 1

                End If
            Catch ex As Exception
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Redirect("..\Errore.aspx")
            End Try
        End If
    End Sub


    Private Sub SettaFrmReadOnly()
        Try

            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridDettMorosita.Items.Count - 1
                di = Me.DataGridDettMorosita.Items(i)
                DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Enabled = False
                DirectCast(di.Cells(1).FindControl("txtNote"), TextBox).Enabled = False
            Next

            btnDeleteInquilini.Enabled = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub


    Private Sub RicercaDettagliMorosita()
        Try

            If vIdMorosita <> "" Then

                Dim dataDoc As String = par.FormatoDataDB(Request.QueryString("DATADOC"))

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "SELECT (SELECT POSIZIONE_BILANCIO FROM SISCOM_MI.COND_UI WHERE ID_UI = cond_morosita_inquilini_det.ID_UI AND ID_CONDOMINIO =" & Request.QueryString("IDCONDOMINIO") & ") AS POSIZIONE_BILANCIO,COND_MOROSITA_INQUILINI_DET.ID_INTESTATARIO, COND_MOROSITA_INQUILINI_DET.ID_MOROSITA, COND_MOROSITA_INQUILINI_DET.ID_UI, trim(TO_CHAR(COND_MOROSITA_INQUILINI_DET.IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO, COND_MOROSITA_INQUILINI_DET.NOTE, ANAGRAFICA.ID, (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET, SISCOM_MI.ANAGRAFICA WHERE COND_MOROSITA_INQUILINI_DET.ID_MOROSITA=" & Request.QueryString("IDMOROSITA") & " AND ANAGRAFICA.ID=COND_MOROSITA_INQUILINI_DET.ID_INTESTATARIO AND COND_MOROSITA_INQUILINI_DET.DATA='" & dataDoc & "'"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()
                da.Fill(dt)
                DataGridDettMorosita.DataSource = dt
                DataGridDettMorosita.DataBind()

                AddJavascriptFunction()

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Private Sub ControllaPresenzaDataDoc(ByVal data As String)
        Try


            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE COND_MOROSITA_INQUILINI_DET.ID_MOROSITA=" & Request.QueryString("IDMOROSITA") & " AND COND_MOROSITA_INQUILINI_DET.DATA='" & data & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader.HasRows Then
                Dim funzione As String = " var Conferma; " _
                                                    & "Conferma = window.confirm('Attenzione. Esistono già degli inquilini per questa data. Continuare con la modifica dei dati esistenti?');" _
                                                    & "if (Conferma == false) { self.close(); }"

                If myReader.HasRows Then
                    Response.Write("<script>" & funzione & "</script>")
                End If

            End If
            myReader.Close()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Protected Sub DataGridDettMorosita_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDettMorosita.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato " & e.Item.Cells(6).Text.Replace("'", "\'") & "';document.getElementById('txtidIntestatario').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato " & e.Item.Cells(6).Text.Replace("'", "\'") & "';document.getElementById('txtidIntestatario').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub


    Private Sub ElencoInquilini()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UI,SCALE_EDIFICI.DESCRIZIONE AS SCALA,UNITA_IMMOBILIARI.INTERNO,PIANI.DESCRIZIONE AS PIANO, " _
                                & "COND_UI.POSIZIONE_BILANCIO,COND_UI.ID_INTESTARIO as id_intestario, /*(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END)*/ ''  AS NOMINATIVO " _
                                & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.PIANI " _
                                & "WHERE UNITA_IMMOBILIARI.ID_PIANO = PIANI.ID(+) AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD(+) AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND  COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio=" & Request.QueryString("IDCONDOMINIO") & ") AND ID_INTESTARIO " _
                                & "IS NOT NULL AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO =" & Request.QueryString("IDCONDOMINIO") & ") "

            If vIdMorosita <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " AND UNITA_IMMOBILIARI.ID NOT IN (SELECT ID_UI FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_MOROSITA =" & vIdMorosita & " AND DATA = '" & par.FormatoDataDB(Request.QueryString("DATADOC")) & "') AND " _
                                                          & " UNITA_CONTRATTUALE.ID_CONTRATTO = (SELECT MAX (ID_CONTRATTO) FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_UNITA= UNITA_IMMOBILIARI.ID)"
            End If
            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY POSIZIONE_BILANCIO ASC, NOMINATIVO ASC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            DataGridElencoInquilini.DataSource = dt
            DataGridElencoInquilini.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try

    End Sub


    Protected Sub Aggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Aggiungi.Click
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim di2 As DataGridItem
            Dim descEvent As String = ""

            '**********QUESTO IF CONSENTE DI AGGIORNARE EVENTUALI IMPORTI MODIFICATI SULLA TABELLA 
            '**********PRIMA DI AVER CLICCATO SUL TASTO +
            '**********BISOGNA CONTROLLARE CHE SE L'IMPORTO è STATO CAMBIATO ALLORA è NECESSARIO SCRIVERE L'EVENTO DI MODIFICA DELL'IMPORTO
            If Me.DataGridDettMorosita.Items.Count > 0 Then
                Dim IdIntestatario As String = ""
                Dim IdUi As String = ""
                Dim Importo As String = ""
                Dim ImportoVecchio As String = ""
                Dim Note As String = ""
                Dim NoteVecchie As String = ""
                Dim Diverso As Boolean = False
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_MOROSITA = " & vIdMorosita
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                For i = 0 To Me.DataGridDettMorosita.Items.Count - 1
                    di = Me.DataGridDettMorosita.Items(i)
                    IdIntestatario = Me.DataGridDettMorosita.Items(i).Cells(0).Text
                    IdUi = Me.DataGridDettMorosita.Items(i).Cells(2).Text
                    Importo = DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text
                    Note = DirectCast(di.Cells(1).FindControl("txtNote"), TextBox).Text
                    For Each row As Data.DataRow In dt.Rows
                        If IdIntestatario = row.Item("ID_INTESTATARIO") AndAlso IdUi = row.Item("ID_UI") AndAlso Importo <> par.IfNull(row.Item("IMPORTO"), "") Then
                            Diverso = True
                            ImportoVecchio = par.IfNull(row.Item("IMPORTO"), "")
                            NoteVecchie = par.IfNull(row.Item("NOTE"), "")
                            Exit For
                        End If
                    Next
                    If Not String.IsNullOrEmpty(Importo) And Diverso = True Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA_INQUILINI_DET  SET IMPORTO =  " & par.IfEmpty(par.VirgoleInPunti(Importo.Replace(".", "")), "Null") & ",NOTE ='" & par.IfEmpty(Note, "") & "' " _
                        & " WHERE ID_MOROSITA=" & vIdMorosita & " AND ID_INTESTATARIO=" & IdIntestatario & " AND ID_UI= " & IdUi
                        par.cmd.ExecuteNonQuery()

                        '****************MYEVENT*****************
                        descEvent = "MOROSITA DAL " & par.IfEmpty(Request.QueryString("TXTDATARIFDA"), "--") & " AL " & par.IfEmpty(Request.QueryString("TXTDATARIFA"), "--") & ""
                        descEvent = descEvent & " CON DATA DOCUMENTAZIONE  " & txtDataDocumentazione.Text
                        descEvent = descEvent & " INQUILINO " & (di.Cells(3).Text)
                        descEvent = descEvent & " MODIFICATO IMPORTO DA " & ImportoVecchio & " A " & Importo
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F190','" & par.PulisciStrSql(descEvent) & "')"
                        par.cmd.ExecuteNonQuery()
                        '****************FINE MYEVENT*****************


                    End If
                    Diverso = False
                Next


            End If
            '**********FINE IF DI CONTROLLO


            '**********INZIO CON LA SCRITTURA IN COND_MOROSITA_INQUILINI_DET DELLE NUOVE MOROSITA SELEZIONATE DALLA LISTA DI CHECKBOX*********
            i = 0
            di = Nothing
            di2 = Nothing

            For i = 0 To Me.DataGridElencoInquilini.Items.Count - 1
                di = Me.DataGridElencoInquilini.Items(i)
                If DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True Then

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_MOROSITA_INQUILINI_DET (ID_MOROSITA, ID_INTESTATARIO,ID_UI,DATA) VALUES " _
                                        & "(" & vIdMorosita & ", " & DirectCast(di.FindControl("cmbInquilino"), DropDownList).SelectedValue & ", " & Me.DataGridElencoInquilini.Items(i).Cells(0).Text & ", '" & par.FormatoDataDB(Request.QueryString("DATADOC")) & "')"
                    par.cmd.ExecuteNonQuery()

                    '****************MYEVENT*****************
                    descEvent = "MOROSITA DAL " & par.IfEmpty(Request.QueryString("TXTDATARIFDA"), "--") & " AL " & par.IfEmpty(Request.QueryString("TXTDATARIFA"), "--") & ""
                    descEvent = descEvent & " CON DATA DOCUMENTAZIONE  " & txtDataDocumentazione.Text
                    descEvent = descEvent & " AGGIUNTO INQUILINO " & (di.Cells(3).Text)
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F190','" & par.PulisciStrSql(descEvent) & "')"
                    par.cmd.ExecuteNonQuery()
                    '****************FINE MYEVENT*****************

                End If
            Next
            '**********FINE SCRITTURA IN COND_MOROSITA_INQUILINI DELLE NUOVE MOROSITA SELEZIONATE DALLA LISTA DI CHECKBOX**********
            Session("MODIFYMODAL") = 1
            RicercaDettagliMorosita()
            ElencoInquilini()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub



    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridDettMorosita.Items.Count - 1
                di = Me.DataGridDettMorosita.Items(i)
                DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try

    End Sub


    Protected Sub btnSalvaDettMorInquilini_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaDettMorInquilini.Click
        Try

            aggiungiDettMorInquilini()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Private Sub aggiungiDettMorInquilini()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim descEvent As String = ""
            Dim erroreImporto As Boolean = False
            Dim importo As String = ""
            Dim importoVecchio As String = ""
            Dim nuovoInquilino = False


            '**********INZIO CON LA SCRITTURA IN COND_MOROSITA_INQUILINI DELLE NUOVE MOROSITA SELEZIONATE DALLA LISTA DI CHECKBOX*********
            i = 0
            di = Nothing
            For i = 0 To Me.DataGridDettMorosita.Items.Count - 1
                di = Me.DataGridDettMorosita.Items(i)

                par.cmd.CommandText = "SELECT COND_MOROSITA_INQUILINI.ID_MOROSITA FROM SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_MOROSITA = " & vIdMorosita & " AND ID_INTESTATARIO= " & Me.DataGridDettMorosita.Items(i).Cells(0).Text & " AND ID_UI = " & Me.DataGridDettMorosita.Items(i).Cells(2).Text & " "
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.HasRows Then
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_MOROSITA_INQUILINI(ID_MOROSITA, ID_INTESTATARIO, ID_UI) VALUES (" & vIdMorosita & ", " & Me.DataGridDettMorosita.Items(i).Cells(0).Text & ", " & Me.DataGridDettMorosita.Items(i).Cells(2).Text & ")"
                    par.cmd.ExecuteNonQuery()
                    nuovoInquilino = True
                End If
                myReader.Close()

                importo = DirectCast(DataGridDettMorosita.Items(i).Cells(4).FindControl("txtImporto"), TextBox).Text

                par.cmd.CommandText = "SELECT trim(TO_CHAR(COND_MOROSITA_INQUILINI_DET.IMPORTO,'9G999G999G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_MOROSITA = " & vIdMorosita & " AND ID_INTESTATARIO= " & Me.DataGridDettMorosita.Items(i).Cells(0).Text & " AND ID_UI = " & Me.DataGridDettMorosita.Items(i).Cells(2).Text & ""
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader2.HasRows Then
                    If myReader2.Read Then
                        importoVecchio = par.IfNull(myReader2.Item("IMPORTO"), "")
                    End If
                End If
                myReader2.Close()
                If (DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text <> "") Then

                    If (DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text > 0) Then
                        'If importoVecchio <> importo Then                ''☺♫☼ Puccia 08/01/2013 controllo errato e non pertinente

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA_INQUILINI_DET SET IMPORTO = " & par.VirgoleInPunti((DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text.Replace(".", ""))) & ",NOTE= '" & par.VirgoleInPunti(par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtNote"), TextBox).Text, "")) & "' " _
                                            & " WHERE DATA = '" & par.FormatoDataDB(Request.QueryString("DATADOC")) & "' AND ID_MOROSITA = " & vIdMorosita & " AND ID_INTESTATARIO= " & Me.DataGridDettMorosita.Items(i).Cells(0).Text & " AND ID_UI = " & Me.DataGridDettMorosita.Items(i).Cells(2).Text
                        par.cmd.ExecuteNonQuery()

                        If nuovoInquilino = False Then
                            '****************MYEVENT*****************
                            descEvent = "MOROSITA DAL " & par.IfEmpty(Request.QueryString("TXTDATARIFDA"), "--") & " AL " & par.IfEmpty(Request.QueryString("TXTDATARIFA"), "--") & ""
                            descEvent = descEvent & " CON DATA DOCUMENTAZIONE  " & txtDataDocumentazione.Text
                            descEvent = descEvent & " AGGIORNATO IMPORTO INQUILINO " & (di.Cells(3).Text)
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F190','" & par.PulisciStrSql(descEvent) & "')"
                            par.cmd.ExecuteNonQuery()
                            '****************FINE MYEVENT*****************
                        End If

                        nuovoInquilino = False

                        ''☺♫☼ Puccia 08/01/2013 controllo errato e non pertinenteEnd If
                    Else
                        Response.Write("<script>alert('Inserire un importo superiore a 0 per l\'inquilino " + Me.DataGridDettMorosita.Items(i).Cells(6).Text + "!');</script>")
                        Exit Sub
                    End If

                Else
                    Response.Write("<script>alert('Inserire un importo per l\'inquilino " + Me.DataGridDettMorosita.Items(i).Cells(6).Text + "!');</script>")
                    Exit Sub
                End If

            Next


            morositaTotaleInquilino()

            Session("MODIFYMODAL") = 1
            Response.Write("<script>window.close();</script>")

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub


    Private Sub morositaTotaleInquilino()

        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            '****27/02/2013  AGGIUNTO RAGGRUPPAMENTO PER ID_UI PER PRESENZA DI UN INTESTATARIO SU DUE ALLOGGI DEL CONDOMINIO
            par.cmd.CommandText = "SELECT ID_UI,ID_INTESTATARIO, ID_MOROSITA, SUM (importo) AS IMPORTO FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_MOROSITA = " & vIdMorosita & " GROUP BY ID_INTESTATARIO, ID_MOROSITA,ID_UI"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader.Read
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA_INQUILINI SET IMPORTO = " & par.VirgoleInPunti(myReader("IMPORTO")) & " WHERE ID_INTESTATARIO= " & myReader("ID_INTESTATARIO") & " AND ID_MOROSITA = " & myReader("ID_MOROSITA") & " AND ID_UI = " & myReader("ID_UI")
                par.cmd.ExecuteNonQuery()
            End While
            myReader.Close()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try

    End Sub


    Protected Sub btnDeleteInquilini_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeleteInquilini.Click
        Try

            If Me.txtidIntestatario.Value <> 0 Then
                If txtConfElimina.Value = 1 Then
                    eliminaDettMorInquilini()
                Else
                    txtConfElimina.Value = 0
                End If
            Else
                Response.Write("<script>alert('Selezionare un elemento da eliminare!');</script>")
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub


    Private Sub eliminaDettMorInquilini()
        Try

            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim descEvent As String = ""
            i = 0
            di = Nothing
            di = Me.DataGridDettMorosita.Items(i)


            If Me.txtidIntestatario.Value <> 0 Then

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_INTESTATARIO = " & Me.txtidIntestatario.Value & " AND ID_MOROSITA = " & vIdMorosita & " AND DATA = '" & par.FormatoDataDB(Request.QueryString("DATADOC")) & "'"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT COND_MOROSITA_INQUILINI_DET.ID_INTESTATARIO FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_INTESTATARIO= " & txtidIntestatario.Value & " AND ID_MOROSITA = " & vIdMorosita & ""
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.HasRows Then
                Else
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_INTESTATARIO = " & Me.txtidIntestatario.Value & " AND ID_MOROSITA = " & vIdMorosita & ""
                    par.cmd.ExecuteNonQuery()
                End If

                '****************MYEVENT*****************
                descEvent = "MOROSITA DAL " & par.IfEmpty(par.FormatoDataDB(Request.QueryString("TXTDATARIFDA")), "--") & " AL " & par.IfEmpty(par.FormatoDataDB(Request.QueryString("TXTDATARIFA")), "--") & ""
                descEvent = descEvent & " CON DATA DOCUMENTAZIONE  " & txtDataDocumentazione.Text
                descEvent = descEvent & " ELIMINATO INQUILINO  " & (di.Cells(3).Text)
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F190','" & par.PulisciStrSql(descEvent) & "')"
                par.cmd.ExecuteNonQuery()
                '****************FINE MYEVENT*****************

                Session("MODIFYMODAL") = 1
                RicercaDettagliMorosita()
                ElencoInquilini()

                txtidIntestatario.Value = 0
                txtConfElimina.Value = 0
            Else

                Response.Write("<script>alert('Selezionare un elemento da eliminare!');</script>")

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub


    Protected Sub DataGridElencoInquilini_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridElencoInquilini.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            par.caricaComboBox("SELECT id_anagrafica," _
                                & "       (CASE" _
                                & "           WHEN ANAGRAFICA.ragione_sociale IS NOT NULL" _
                                & "              THEN ragione_sociale" _
                                & "           ELSE RTRIM (LTRIM (cognome || ' ' || nome))" _
                                & "        END" _
                                & "       ) AS nominativo" _
                                & "  FROM siscom_mi.SOGGETTI_CONTRATTUALI, siscom_mi.ANAGRAFICA" _
                                & " WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.id_anagrafica" _
                                & "   AND id_contratto in (SELECT id_contratto" _
                                & "                         FROM siscom_mi.UNITA_CONTRATTUALE" _
                                & "                        WHERE id_unita = " & e.Item.Cells(par.IndDGC(DataGridElencoInquilini, "ID_UI")).Text & ")" _
                                & "   AND cod_tipologia_occupante = 'INTE'", CType(e.Item.FindControl("cmbInquilino"), DropDownList), "ID_ANAGRAFICA", "NOMINATIVO")


            If Not IsNothing(CType(e.Item.FindControl("cmbInquilino"), DropDownList).Items.FindByValue(e.Item.Cells(par.IndDGC(DataGridElencoInquilini, "id_intestario")).Text)) Then
                CType(e.Item.FindControl("cmbInquilino"), DropDownList).SelectedValue = (e.Item.Cells(par.IndDGC(DataGridElencoInquilini, "id_intestario")).Text)
            Else
                Dim nome As String = ""
                par.cmd.CommandText = "SELECT  (CASE" _
                                & "           WHEN ANAGRAFICA.ragione_sociale IS NOT NULL" _
                                & "              THEN ragione_sociale" _
                                & "           ELSE RTRIM (LTRIM (cognome || ' ' || nome))" _
                                & "        END" _
                                & "       ) AS nominativo FROM siscom_mi.ANAGRAFICA WHERE ID= " & e.Item.Cells(par.IndDGC(DataGridElencoInquilini, "id_intestario")).Text
                nome = par.cmd.ExecuteScalar

                CType(e.Item.FindControl("cmbInquilino"), DropDownList).Items.Add(New ListItem(nome, e.Item.Cells(par.IndDGC(DataGridElencoInquilini, "id_intestario")).Text))

                CType(e.Item.FindControl("cmbInquilino"), DropDownList).SelectedValue = (e.Item.Cells(par.IndDGC(DataGridElencoInquilini, "id_intestario")).Text)
            End If

            'par.caricaComboBox("SELECT ID, 'Avv. ' || RTRIM(LTRIM(INITCAP(COGNOME))) || ' ' || RTRIM(LTRIM(INITCAP(NOME))) AS AVVOCATO FROM MOROSITA_LEGALI ORDER BY COGNOME, NOME ASC", CType(e.Item.FindControl("ddlAvvocatoSceltaDirezione"), DropDownList), "ID", "AVVOCATO", True)

        End If
    End Sub

    Protected Sub DataGridElencoInquilini_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridElencoInquilini.PageIndexChanged
        For Each di As DataGridItem In Me.DataGridElencoInquilini.Items
            If DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True Then
                Response.Write("<script>alert('Aggiungere quanto selezionato prima di cambiare pagina!');</script>")
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "divInq", "OpInquil.toggle();", True)
                Exit Sub

            End If
        Next

        If e.NewPageIndex >= 0 Then
            DataGridElencoInquilini.CurrentPageIndex = e.NewPageIndex
            ElencoInquilini()
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "divInq", "OpInquil.toggle();", True)
    End Sub
End Class
