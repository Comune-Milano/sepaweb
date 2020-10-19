' RIPARTIZIONI IMPORTO CONSUNTIVATO per EDIFICI o IMPIANTI
Imports System.Collections


Partial Class Tab_SAL_Ripartizioni
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then


                'vIdAppalto = CType(Me.Page.FindControl("txtID_APPALTO"), HiddenField).Value
                'vIdPagamenti = CType(Me.Page.FindControl("txtID_PAGAMENTI"), HiddenField).Value


                '' CONNESSIONE DB
                'IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text

                'If PAR.OracleConn.State = Data.ConnectionState.Open Then
                '    Response.Write("IMPOSSIBILE VISUALIZZARE")
                '    Exit Sub
                'Else
                '    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                '    par.SettaCommand(par)
                'End If
                ''''''''''''''''''''''''''

                'BindGrid_Ripartizioni()

            End If

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    'Private Property vIdPagamenti() As Long
    '    Get
    '        If Not (ViewState("par_idPagamenti") Is Nothing) Then
    '            Return CLng(ViewState("par_idPagamenti"))
    '        Else
    '            Return 0
    '        End If
    '    End Get

    '    Set(ByVal value As Long)
    '        ViewState("par_idPagamenti") = value
    '    End Set

    'End Property

    'Private Property vIdAppalto() As Long
    '    Get
    '        If Not (ViewState("par_idAppalto") Is Nothing) Then
    '            Return CLng(ViewState("par_idAppalto"))
    '        Else
    '            Return 0
    '        End If
    '    End Get

    '    Set(ByVal value As Long)
    '        ViewState("par_idAppalto") = value
    '    End Set

    'End Property

    'Public Property IdConnessione() As String
    '    Get
    '        If Not (ViewState("par_Connessione") Is Nothing) Then
    '            Return CStr(ViewState("par_Connessione"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_Connessione") = value
    '    End Set

    'End Property


    'PRENOTAZIONI GRID1
    'Private Sub BindGrid_Ripartizioni()
    '    Dim StringaSql As String
    '    Dim myReaderTMP2 As Oracle.DataAccess.Client.OracleDataReader
    '    Dim Trovato As Boolean


    '    Try

    '        '' RIPRENDO LA CONNESSIONE
    '        'PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        'par.SettaCommand(par)

    '        ''RIPRENDO LA TRANSAZIONE
    '        'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '        '‘‘par.cmd.Transaction = par.myTrans


    '        'Trovato = False

    '        'If vIdPagamenti <> 0 Then
    '        '    If PAR.IfEmpty(CType(Me.Page.FindControl("txtTipo_LOTTO"), HiddenField).Value, "E") = "E" Then
    '        '        PAR.cmd.CommandText = "select count(ID_EDIFICIO) from SISCOM_MI.PAGAMENTI_EDIFICI where ID_PAGAMENTO=" & vIdPagamenti
    '        '    Else
    '        '        PAR.cmd.CommandText = "select count(ID_IMPIANTO) from SISCOM_MI.PAGAMENTI_IMPIANTI where ID_PAGAMENTO=" & vIdPagamenti
    '        '    End If
    '        '    myReaderTMP2 = PAR.cmd.ExecuteReader()

    '        '    If myReaderTMP2.Read Then
    '        '        If myReaderTMP2(0) > 0 Then
    '        '            Trovato = True
    '        '        End If
    '        '    Else
    '        '        Trovato = False
    '        '    End If
    '        '    myReaderTMP2.Close()
    '        'End If


    '        'If PAR.IfEmpty(CType(Me.Page.FindControl("txtTipo_LOTTO"), HiddenField).Value, "E") = "E" Then

    '        '    If Trovato = True Then
    '        '        StringaSql = "select EDIFICI.ID,trim(EDIFICI.DENOMINAZIONE) as DENOMINAZIONE," _
    '        '                        & " 'EDIFICIO' as TIPO, trim(TO_CHAR(SISCOM_MI.PAGAMENTI_EDIFICI.IMPORTO,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
    '        '                    & " from SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.PAGAMENTI_EDIFICI " _
    '        '                    & " where APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=EDIFICI.ID  " _
    '        '                    & "   and EDIFICI.ID=SISCOM_MI.PAGAMENTI_EDIFICI.ID_EDIFICIO (+) " _
    '        '                    & "   and (SISCOM_MI.PAGAMENTI_EDIFICI.ID_PAGAMENTO=" & vIdPagamenti _
    '        '                    & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & vIdAppalto _
    '        '                    & " order by DENOMINAZIONE "

    '        '    Else
    '        '        StringaSql = "select EDIFICI.ID,trim(EDIFICI.DENOMINAZIONE) as DENOMINAZIONE," _
    '        '                        & " 'EDIFICIO' as TIPO, trim(TO_CHAR(0,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
    '        '                    & " from SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
    '        '                    & " where APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=EDIFICI.ID  " _
    '        '                    & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & vIdAppalto _
    '        '                    & " order by DENOMINAZIONE "

    '        '    End If

    '        'Else
    '        '    If Trovato = True Then

    '        '        StringaSql = "select IMPIANTI.ID, (TIPOLOGIA_IMPIANTI.DESCRIZIONE|| ' - - ' || trim(IMPIANTI.DESCRIZIONE) ) AS DENOMINAZIONE," _
    '        '                        & " 'IMPIANTO' as TIPO,trim(TO_CHAR(SISCOM_MI.PAGAMENTI_IMPIANTI.IMPORTO,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
    '        '                  & " from SISCOM_MI.IMPIANTI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.PAGAMENTI_IMPIANTI " _
    '        '                  & " where APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=IMPIANTI.ID   " _
    '        '                  & "   and TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " _
    '        '                  & "   and IMPIANTI.ID=SISCOM_MI.PAGAMENTI_IMPIANTI.ID_IMPIANTO (+) " _
    '        '                  & "   and (SISCOM_MI.PAGAMENTI_IMPIANTI.ID_PAGAMENTO=" & vIdPagamenti & " or SISCOM_MI.PAGAMENTI_IMPIANTI.ID_PAGAMENTO is NUll) " _
    '        '                  & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO=" & vIdAppalto _
    '        '                  & " order by DENOMINAZIONE "

    '        '    Else
    '        '        StringaSql = "select IMPIANTI.ID, (TIPOLOGIA_IMPIANTI.DESCRIZIONE|| ' - - ' || trim(IMPIANTI.DESCRIZIONE) ) AS DENOMINAZIONE," _
    '        '                        & " 'IMPIANTO' as TIPO, trim(TO_CHAR(0,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
    '        '                    & " from SISCOM_MI.IMPIANTI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, SISCOM_MI.TIPOLOGIA_IMPIANTI " _
    '        '                    & " where APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=IMPIANTI.ID  " _
    '        '                    & "   and TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " _
    '        '                    & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & vIdAppalto _
    '        '                    & " order by DENOMINAZIONE "

    '        '    End If
    '        'End If


    '        'PAR.cmd.CommandText = StringaSql

    '        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
    '        'Dim ds As New Data.DataSet()

    '        'da.Fill(ds, "APPALTI_LOTTI_PATRIMONIO")

    '        'DataGrid1.DataSource = ds
    '        'DataGrid1.DataBind()

    '        'ds.Dispose()


    '        'Dim i As Integer = 0
    '        'Dim di As DataGridItem

    '        'For i = 0 To DataGrid1.Items.Count - 1
    '        '    di = DataGrid1.Items(i)

    '        '    CType(di.Cells(1).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

    '        '    If Trovato = True Then
    '        '        CType(Me.DataGrid1.Items(i).Cells(0).FindControl("txtImporto"), TextBox).ReadOnly = False
    '        '        Me.ChkRipartizioni.Checked = True
    '        '        Me.btnRipartisci.Visible = True
    '        '    Else
    '        '        CType(Me.DataGrid1.Items(i).Cells(0).FindControl("txtImporto"), TextBox).ReadOnly = True
    '        '        Me.ChkRipartizioni.Checked = False
    '        '        Me.btnRipartisci.Visible = False
    '        '    End If
    '        'Next




    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    '        Session.Item("LAVORAZIONE") = "0"

    '        PAR.myTrans.Rollback()
    '        PAR.OracleConn.Close()


    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try

    'End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_SAL_Ripartizioni_txtSel1').value='Hai selezionato: " & Replace(Replace(Replace(e.Item.Cells(1).Text, "'", "\'"), vbCr, " "), vbLf, " ") & "';document.getElementById('Tab_SAL_Ripartizioni_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_SAL_Ripartizioni_txtSel1').value='Hai selezionato: " & Replace(Replace(Replace(e.Item.Cells(1).Text, "'", "\'"), vbCr, " "), vbLf, " ") & "';document.getElementById('Tab_SAL_Ripartizioni_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    'Function ControlloCampiRipartizioni() As Boolean

    '    ControlloCampiRipartizioni = True

    '    If PAR.IfEmpty(Me.txtImportoRipartito.Text, "Null") = "Null" Then
    '        Response.Write("<script>alert('Inserire il prezzo da ripartire!');</script>")
    '        ControlloCampiRipartizioni = False
    '        Exit Function
    '    End If


    '    'Controllo che la somma Inserita non superi l'importo tolale - la somma di quelli già ripartiri

    '    Dim Somma1 As Decimal = 0

    '    Somma1 = Decimal.Parse(PAR.IfEmpty(Me.txtImportoRimasto.Text.Replace(".", ""), "0")) + Decimal.Parse(PAR.IfEmpty(Me.txtImportoRipartito.Text.Replace(".", ""), "0"))

    '    If Somma1 > Decimal.Parse(PAR.IfEmpty(Me.txtImportoTOT.Text.Replace(".", ""), "0")) Then
    '        Somma1 = Somma1 - Decimal.Parse(PAR.IfEmpty(Me.txtImportoTOT.Text.Replace(".", ""), "0"))
    '        Somma1 = Decimal.Parse(PAR.IfEmpty(Me.txtImportoRipartito.Text.Replace(".", ""), "0")) - Somma1

    '        Response.Write("<script>alert('Attenzione: l\'importo inserito da ripartire non deve superare " & Somma1 & " euro!');</script>")
    '        ControlloCampiRipartizioni = False
    '        Exit Function

    '    End If


    '  End Function

    'Protected Sub btn_Inserisci1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci1.Click
    '    If ControlloCampiRipartizioni() = False Then
    '        txtAppare1.Text = "1"
    '        Exit Sub
    '    End If

    '    Me.UpdateRipartizioni()

    '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


    '    txtSel1.Text = ""
    '    txtIdComponente.Text = ""

    'End Sub

    'Protected Sub btn_Chiudi1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi1.Click
    '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

    '    txtSel1.Text = ""
    '    txtIdComponente.Text = ""
    'End Sub



    'Private Sub UpdateRipartizioni()
    '    Dim sStr1 As String = ""


    '    Try

    '        If PAR.OracleConn.State = Data.ConnectionState.Open Then
    '            Response.Write("IMPOSSIBILE VISUALIZZARE")
    '            Exit Sub
    '        Else
    '            ' RIPRENDO LA CONNESSIONE
    '            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '            par.SettaCommand(par)

    '            'RIPRENDO LA TRANSAZIONE
    '            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '            ‘‘par.cmd.Transaction = par.myTrans
    '        End If



    '        PAR.cmd.Parameters.Clear()

    '        If Me.txtTIPO.Text = "EDIFICIO" Then
    '            PAR.cmd.CommandText = "update  SISCOM_MI.PAGAMENTI_EDIFICI  set " _
    '                                            & "IMPORTO=:importo" _
    '                                    & " where ID_EDIFICIO=" & Me.txtIdComponente.Text _
    '                                    & "   and ID_PAGAMENTO=" & vIdPagamenti

    '            sStr1 = "Importo ripartito dell'edificio: " & Strings.Left(Me.txtDenominazione.Text, 30) & "... da: " & IsNumFormat(Me.txtImportoRipartitoODL.Value, "", "##,##0.00") & " a: " & Me.txtImportoRipartito.Text

    '        Else
    '            PAR.cmd.CommandText = "update  SISCOM_MI.PAGAMENTI_IMPIANTI  set " _
    '                                            & "IMPORTO=:importo" _
    '                                    & " where ID_IMPIANTO=" & Me.txtIdComponente.Text _
    '                                    & "   and ID_PAGAMENTO=" & vIdPagamenti


    '            sStr1 = "Importo ripartito dell'impianto: " & Strings.Left(Me.txtDenominazione.Text, 30) & "... da: " & IsNumFormat(Me.txtImportoRipartitoODL.Value, "", "##,##0.00") & " a: " & Me.txtImportoRipartito.Text

    '        End If


    '        PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtImportoRipartito.Text.Replace(".", ""))))
    '        PAR.cmd.ExecuteNonQuery()
    '        PAR.cmd.CommandText = ""


    '        ''****Scrittura evento MODIFICA RIPARTIZIONI IMPORTI DEL PAGAMENTO
    '        PAR.cmd.Parameters.Clear()
    '        PAR.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
    '                           & " values ( " & vIdPagamenti & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','" & PAR.PulisciStrSql(Strings.Left(sStr1, 100)) & "')"
    '        PAR.cmd.ExecuteNonQuery()
    '        PAR.cmd.Parameters.Clear()
    '        '****************************************************


    '        BindGrid_Ripartizioni()

    '        CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"


    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    '        Session.Item("LAVORAZIONE") = "0"

    '        PAR.myTrans.Rollback()
    '        PAR.OracleConn.Close()

    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

    '    End Try

    'End Sub


    'Protected Sub btnApri1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRipartisci.Click
    '    Dim FlagConnessione As Boolean

    '    Try


    '        If txtIdComponente.Text = "" Then
    '            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
    '            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    '            Me.txtAppare1.Text = "0"
    '        Else

    '            FlagConnessione = False
    '            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
    '                PAR.OracleConn.Open()
    '                par.SettaCommand(par)

    '                FlagConnessione = True
    '            End If


    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
    '            Dim Somma1 As Decimal = 0
    '            Dim sRisultato As String = ""


    '            If PAR.IfEmpty(CType(Me.Page.FindControl("txtTipo_LOTTO"), HiddenField).Value, "E") = "E" Then

    '                PAR.cmd.CommandText = "select EDIFICI.ID,trim(DENOMINAZIONE) as DENOMINAZIONE," _
    '                          & " 'EDIFICIO' as TIPO, TO_CHAR(SISCOM_MI.PAGAMENTI_EDIFICI.IMPORTO) AS ""IMPORTO_RIPARTITO""" _
    '                          & " from SISCOM_MI.EDIFICI, SISCOM_MI.PAGAMENTI_EDIFICI " _
    '                          & " where EDIFICI.ID=" & Me.txtIdComponente.Text _
    '                          & "   and EDIFICI.ID=SISCOM_MI.PAGAMENTI_EDIFICI.ID_EDIFICIO (+) " _
    '                          & "   and PAGAMENTI_IMPIANTI.ID_PAGAMENTO=" & vIdPagamenti _
    '                          & " order by DENOMINAZIONE "
    '            Else
    '                PAR.cmd.CommandText = "select IMPIANTI.ID, trim(IMPIANTI.DESCRIZIONE ) AS DENOMINAZIONE," _
    '                                & " TIPOLOGIA_IMPIANTI.DESCRIZIONE as TIPO,TO_CHAR(SISCOM_MI.PAGAMENTI_IMPIANTI.IMPORTO) AS ""IMPORTO_RIPARTITO""" _
    '                          & " from SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.PAGAMENTI_IMPIANTI " _
    '                          & " where IMPIANTI.ID=" & Me.txtIdComponente.Text _
    '                          & "   and TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " _
    '                          & "   and IMPIANTI.ID=SISCOM_MI.PAGAMENTI_IMPIANTI.ID_IMPIANTO (+) " _
    '                          & "   and PAGAMENTI_IMPIANTI.ID_PAGAMENTO=" & vIdPagamenti _
    '                          & " order by DENOMINAZIONE "

    '            End If

    '            myReader1 = PAR.cmd.ExecuteReader

    '            If myReader1.Read Then

    '                Me.txtTIPO.Text = PAR.IfNull(myReader1("TIPO"), "")
    '                Me.txtDenominazione.Text = PAR.IfNull(myReader1("DENOMINAZIONE"), "")


    '                'IMPORTO TOTALE CONSUNTIVATO
    '                Me.txtImportoTOT.Text = CType(Me.Page.FindControl("Tab_SAL_Riepilogo_txtNettoOneriIVA2"), TextBox).Text


    '                'EVENTUALE IMPORTO GIA' RIPARTITO oppure 0 se non ancora ripartito
    '                sRisultato = PAR.IfNull(myReader1("IMPORTO_RIPARTITO"), "0")
    '                Somma1 = Decimal.Parse(sRisultato)
    '                Me.txtImportoRipartito.Text = IsNumFormat(Somma1, "", "##,##0.00")

    '                Me.txtImportoRipartitoODL.Value = Somma1    'IMPORTO RIPARTITO ORIGINALE


    '                'IMPORTO DA RIPARTIRE (TOTALE - SOMMA(IMPORTO) ripartito
    '                Somma1 = Me.txtImportoTOT.Text - TotalePrezzoRipartito(PAR.IfNull(myReader1("TIPO"), ""))
    '                Me.txtImportoRimasto.Text = IsNumFormat(Somma1, "", "##,##0.00")

    '            End If
    '            myReader1.Close()

    '            'If Me.txt_FL_BLOCCATO.Value = 1 Then
    '            '    Response.Write("<SCRIPT>alert('Attenzione...Non è possibile modificare la voce perchè proveniente da un ordine emesso integrativo!');</SCRIPT>")
    '            '    Me.btn_Inserisci1.Visible = False
    '            'Else
    '            '    Me.btn_Inserisci1.Visible = True
    '            'End If


    '        End If

    '    Catch ex As Exception

    '        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    '        Session.Item("LAVORAZIONE") = "0"

    '        If FlagConnessione = True Then
    '            PAR.OracleConn.Close()
    '        End If

    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try


    'End Sub


    'Public Function strToNumber(ByVal s0 As String) As Object
    '    Dim s As String = s0.Replace(".", ",")

    '    Dim d As Double

    '    If Double.TryParse(s, d) = True Then
    '        Return d
    '    Else
    '        Return DBNull.Value
    '    End If
    'End Function


    Private Sub FrmSolaLettura()
        Dim i As Integer

        Try

            Me.btnRipartisci.Visible = False

            For i = 0 To Me.DataGrid1.Items.Count - 1
                CType(Me.DataGrid1.Items(i).Cells(0).FindControl("txtImporto"), TextBox).ReadOnly = True
            Next i



        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub



    'Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
    '    Dim a As Object = DBNull.Value
    '    Try

    '        If valorepass <> -1 Then
    '            a = valorepass
    '        End If

    '    Catch ex As Exception

    '    End Try

    '    Return a
    'End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function




    'Function TotalePrezzoRipartito(ByVal Tipo As String) As Decimal
    '    'Dim myReaderTMP2 As Oracle.DataAccess.Client.OracleDataReader
    '    'Dim sRisultato As String = ""
    '    'Dim FlagConnessione As Boolean


    '    'Try

    '    '    '    TotalePrezzoRipartito = 0

    '    '    '    FlagConnessione = False
    '    '    '    If PAR.OracleConn.State = Data.ConnectionState.Closed Then
    '    '    '        PAR.OracleConn.Open()
    '    '    '        par.SettaCommand(par)

    '    '    '        FlagConnessione = True
    '    '    '    End If


    '    '    '    If Tipo = "EDIFICIO" Then
    '    '    '        PAR.cmd.CommandText = " select TO_CHAR(SUM(IMPORTO)) from SISCOM_MI.PAGAMENTI_EDIFICI " _
    '    '    '                            & " where ID_PAGAMENTO=" & vIdPagamenti

    '    '    '    Else
    '    '    '        PAR.cmd.CommandText = " select TO_CHAR(SUM(IMPORTO)) from SISCOM_MI.PAGAMENTI_IMPIANTI " _
    '    '    '                            & " where ID_PAGAMENTO=" & vIdPagamenti

    '    '    '    End If

    '    '    '    myReaderTMP2 = PAR.cmd.ExecuteReader()
    '    '    '    If myReaderTMP2.Read Then
    '    '    '        sRisultato = PAR.IfNull(myReaderTMP2(0), "0")
    '    '    '        TotalePrezzoRipartito = Decimal.Parse(sRisultato)
    '    '    '    End If
    '    '    '    myReaderTMP2.Close()

    '    '    '    PAR.cmd.CommandText = ""


    '    '    'Catch ex As Exception
    '    '    '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    '    '    '    Session.Item("LAVORAZIONE") = "0"


    '    '    '    If FlagConnessione = True Then
    '    '    '        PAR.myTrans.Rollback()
    '    '    '        PAR.OracleConn.Close()
    '    '    '    End If

    '    '    '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '    '    '    Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

    '    '    'End Try

    'End Function




    Protected Sub btnRipartisci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRipartisci.Click

        Dim i As Integer = 0
        Dim riga As Integer = 0
        Dim Somma1 As Decimal = 0
        Dim SommaDaRipartire As Decimal = 0
        Dim Totale As Decimal = 0
        Dim differenza As Decimal = 0
        Dim di As DataGridItem

        For i = 0 To DataGrid1.Items.Count - 1
            di = DataGrid1.Items(i)
            If CDbl(PAR.IfEmpty(CType(di.Cells(1).FindControl("txtImporto"), TextBox).Text, 0)) = 0 Then
                riga = riga + 1
            End If
            Somma1 = Somma1 + CDbl(PAR.IfEmpty(CType(di.Cells(1).FindControl("txtImporto"), TextBox).Text, 0))
        Next


        Totale = Decimal.Parse(PAR.IfEmpty(CType(Me.Page.FindControl("Tab_SAL_Riepilogo").FindControl("txtNettoOneriIVA2"), TextBox).Text.Replace(".", ""), "0"))


        If Somma1 > Totale Then
            Response.Write("<SCRIPT>alert('Attenzione...Impossibile ripartire gli importi perchè quelli inseriti supera la somma consuntivata!');</SCRIPT>")
            Exit Sub
        End If

        If riga > 0 Then
            differenza = Totale - Somma1
            SommaDaRipartire = differenza / riga

            For i = 0 To DataGrid1.Items.Count - 1
                di = DataGrid1.Items(i)
                If CDbl(PAR.IfEmpty(CType(di.Cells(1).FindControl("txtImporto"), TextBox).Text, 0)) = 0 Then
                    CType(di.Cells(1).FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaDaRipartire, "", "##,##0.00")
                End If
            Next

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
        End If

    End Sub


End Class
