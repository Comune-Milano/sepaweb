Imports System.Collections

Partial Class Tab_Appalto
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    'Dim lstAppalti As System.Collections.Generic.List(Of Mario.Appalti)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            If Not IsPostBack Then


                '  lstAppalti.Clear()

                vIdFornitori = CType(Me.Page.FindControl("txtIdFornitore"), HiddenField).Value
                idfornitore.Value = vIdFornitori 'per passarlo alla showdialog

                ' CONNESSIONE DB
                IdConnessione = CType(Me.Page.FindControl("txtConnessione"), HiddenField).Value

                If PAR.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    PAR.SettaCommand(PAR)
                End If
                ''''''''''''''''''''''''''

                'BindGrid_Appalti()

            End If

            vIdFornitori = CType(Me.Page.FindControl("txtIdFornitore"), HiddenField).Value

            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                FrmSolaLettura()
            End If


            BindGrid_Appalti() ' devo aggiornare la tabella quando chiudo la showdialog

        Catch ex As Exception

        End Try
    End Sub


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

    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_Connessione") Is Nothing) Then
                Return CStr(ViewState("par_Connessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Connessione") = value
        End Set

    End Property


    'APPALTI GRID3
    Private Sub BindGrid_Appalti()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        ' I NOMI DEI CAMPI DEVONO CORRISPONDERE CON QUELLI NELLA DATAGRID
        '       StringaSql = "  select SISCOM_MI.APPALTI.ID, SISCOM_MI.APPALTI.NUM_REPERTORIO, TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_REPERTORIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_REPERTORIO"", SISCOM_MI.APPALTI.ID_TIPOLOGIA AS ""ID_TIPO"", SISCOM_MI.TIPOLOGIA_APPALTI.DESCRIZIONE AS ""TIPO"", (CASE (APPALTI.SAL) WHEN 0 THEN 'NO' ELSE 'SI' END) AS ""SAL"", SISCOM_MI.APPALTI.DESCRIZIONE, SISCOM_MI.APPALTI.ANNO_INIZIO, SISCOM_MI.APPALTI.DURATA_MESI AS ""DURATA"", TRIM(TO_CHAR(SISCOM_MI.APPALTI.BASE_ASTA_CANONE,'9G999G990D99')) AS ""ASTA_CANONE"", TRIM(TO_CHAR(SISCOM_MI.APPALTI.BASE_ASTA_CONSUMO,'9G999G990D99')) AS ""ASTA_CONSUMO"", " _
        '       & "TRIM(TO_CHAR(SISCOM_MI.APPALTI.ONERI_SICUREZZA_CANONE,'9G999G990D99')) AS ""ONERI_CANONE"", TRIM(TO_CHAR(SISCOM_MI.APPALTI.ONERI_SICUREZZA_CONSUMO,'9G999G990D99')) AS ""ONERI_CONSUMO"", SISCOM_MI.APPALTI.PERC_ONERI_SICUREZZA AS ""ONERI"", SISCOM_MI.APPALTI.PENALI, SISCOM_MI.APPALTI.ANNO_RIF_INIZIO AS ""RIFINIZIO"", SISCOM_MI.APPALTI.ANNO_RIF_FINE AS ""RIFINE"", SISCOM_MI.APPALTI.IVA_CANONE, SISCOM_MI.APPALTI.IVA_CONSUMO, TRIM(TO_CHAR(SISCOM_MI.APPALTI.COSTO_GRADO_GIORNO,'9G999G990D99')) AS ""COSTO"", " _
        '       & "SISCOM_MI.LOTTI_SERVIZI.ID AS ""ID_LOTTO"", SISCOM_MI.LOTTI_SERVIZI.DESCRIZIONE AS ""DESCRIZIONE_LOTTO"", SISCOM_MI.TAB_SERVIZI.DESCRIZIONE AS ""SERVIZIO"", SISCOM_MI.APPALTI.PERC_SCONTO_CANONE AS ""SCONTO_CANONE"", SISCOM_MI.APPALTI.PERC_SCONTO_CONSUMO AS ""SCONTO_CONSUMO""" _
        '                   & " from SISCOM_MI.APPALTI, SISCOM_MI.LOTTI_SERVIZI, SISCOM_MI.TIPOLOGIA_APPALTI,SISCOM_MI.TAB_SERVIZI" _
        '   & " where SISCOM_MI.APPALTI.ID_FORNITORE = " & vIdFornitori & " and " _
        ' & "SISCOM_MI.APPALTI.ID=SISCOM_MI.LOTTI_SERVIZI.ID_APPALTO (+) AND " _
        '& "SISCOM_MI.APPALTI.ID_TIPOLOGIA=SISCOM_MI.TIPOLOGIA_APPALTI.ID (+) AND " _
        '& " SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
        '& "order by SISCOM_MI.APPALTI.NUM_REPERTORIO "


        StringaSql = "  select  distinct(SISCOM_MI.APPALTI.ID), SISCOM_MI.APPALTI.ID_FORNITORE, SISCOM_MI.APPALTI.NUM_REPERTORIO, " _
        & " TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_REPERTORIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_REPERTORIO"", (CASE (APPALTI.SAL) WHEN 0 THEN 'NO' ELSE 'SI' END) AS ""SAL"", " _
        & " SISCOM_MI.APPALTI.DESCRIZIONE, TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_INIZIO"", TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_FINE,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_FINE"", SISCOM_MI.APPALTI.DURATA_MESI AS ""DURATA"", " _
        & " TRIM(TO_CHAR(SISCOM_MI.APPALTI.BASE_ASTA_CANONE,'9G999G999G999G999G990D99')) AS ""ASTA_CANONE"", " _
        & " TRIM(TO_CHAR(SISCOM_MI.APPALTI.BASE_ASTA_CONSUMO,'9G999G999G999G999G990D99')) AS ""ASTA_CONSUMO"", " _
        & " TRIM(TO_CHAR(SISCOM_MI.APPALTI.ONERI_SICUREZZA_CANONE,'9G999G999G999G999G990D99')) AS ""ONERI_CANONE"", " _
        & "TRIM(TO_CHAR(SISCOM_MI.APPALTI.ONERI_SICUREZZA_CONSUMO,'9G999G999G999G999G990D99')) AS ""ONERI_CONSUMO"", " _
        & "SISCOM_MI.APPALTI.PERC_ONERI_SIC_CAN, SISCOM_MI.APPALTI.PERC_ONERI_SIC_CON, SISCOM_MI.APPALTI.PENALI, SISCOM_MI.APPALTI.ANNO_RIF_INIZIO AS ""RIFINIZIO"", " _
        & " SISCOM_MI.APPALTI.ANNO_RIF_FINE AS ""RIFINE"", '' AS ""COSTO"", " _
        & " SISCOM_MI.LOTTI.DESCRIZIONE AS ""DESCRIZIONE_LOTTO"", SISCOM_MI.LOTTI.ID AS ""ID_LOTTO"" " _
        & " from SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_LOTTI_SERVIZI, SISCOM_MI.LOTTI " _
        & " where SISCOM_MI.APPALTI.ID_FORNITORE = " & vIdFornitori & "" _
        & " AND SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) AND " _
        & "SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
        & " ORDER BY SISCOM_MI.APPALTI.NUM_REPERTORIO ASC, DATA_REPERTORIO ASC"


        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "APPALTI")


        DataGrid3.DataSource = ds
        DataGrid3.DataBind()

        ds.Dispose()
    End Sub



    Function ControlloCampiAppalti() As Boolean

        ControlloCampiAppalti = True


        'If PAR.IfEmpty(Me.txtnumero.Text, "Null") = "Null" Then
        '    Response.Write("<script>alert('Inserire il numero dell\'appalto!');</script>")
        '    ControlloCampiAppalti = False
        '    txtnumero.Focus()
        '    Exit Function
        'End If

        'If PAR.IfEmpty(Me.txtdescrizione.Text, "Null") = "Null" Then
        '    Response.Write("<script>alert('Inserire la descrizione dell\'appalto!');</script>")
        '    ControlloCampiAppalti = False
        '    txtdescrizione.Focus()
        '    Exit Function
        'End If


        'If Me.txtAnnoRealizzazioneP.Text = "dd/mm/YYYY" Then
        '    Me.txtAnnoRealizzazioneP.Text = ""
        'End If

    End Function

    Protected Sub btnApriAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriAppalti.Click
        Try

            'If txtIdComponente.Text = "" Then
            '    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            '    txtAppareP.Text = "0"
            'End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


    End Sub

    Protected Sub btnEliminaAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaAppalti.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    'Response.Write("<script>alert('Nessuna riga selezionata!')</script>") messaggio visibile in confermaannulloappalti del file .aspx
                    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                    txtAppareP.Text = "0"

                    ' If vIdFornitori = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA FORNITORE

                    'lstAppalti.RemoveAt(txtIdComponente.Text)

                    'Dim indice As Integer = 0
                    'For Each griglia As Mario.Appalti In lstAppalti
                    '    griglia.ID = indice
                    '    indice += 1
                    'Next

                    'DataGrid3.DataSource = lstAppalti
                    'DataGrid3.DataBind()

                Else

                    '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    'ELIMINA APPALTO
                    PAR.cmd.CommandText = "delete from SISCOM_MI.APPALTI where ID = " & txtIdComponente.Text
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = ""



                    BindGrid_Appalti()

                    '*** EVENTI_FORNITORI
                    InserisciEvento(PAR.cmd, vIdFornitori, Session.Item("ID_OPERATORE"), 56, "Elimina appalto")

                End If
                txtSelAppalti.Text = ""
                txtIdComponente.Text = ""

            End If
            CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdFornitori <> -1 Then PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnAggAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggAppalti.Click
        Try

            If btnAggAppalti.Enabled = False Then
                Response.Write("<script>alert('Salvare il fornitore per poter associargli un appalto!')</script>")
            End If

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub


    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function


    Private Sub FrmSolaLettura()
        Try

            Me.btnAggAppalti.Visible = False
            Me.btnEliminaAppalti.Visible = False
            Me.btnApriAppalti.Visible = False


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound
        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Appalto_txtSelAppalti').value='Hai selezionato appalto: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Appalto_txtIdComponente').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Appalto_idlotto').value='" & e.Item.Cells(20).Text & "'")

        'End If
        'If e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
        '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Appalto_txtSelAppalti').value='Hai selezionato appalto: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Appalto_txtIdComponente').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Appalto_idlotto').value='" & e.Item.Cells(20).Text & "'")

        'End If
    End Sub

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function


    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Public Function InserisciEvento(ByVal MyPar As Oracle.DataAccess.Client.OracleCommand, ByVal vIdFornitore As Long, ByVal vIdOperatore As Long, ByVal Tab_Eventi As Integer, ByRef Motivazione As String) As Boolean

        Try

            InserisciEvento = False

            MyPar.Parameters.Clear()
            If InStr(Motivazione, "Modifica") Then
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_FORNITORI (ID_FORNITORE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & vIdFornitore & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F0" & Tab_Eventi & "','" & PAR.PulisciStrSql(Motivazione) & "')"
            Else
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_FORNITORI (ID_FORNITORE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & vIdFornitore & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F" & Tab_Eventi & "','" & PAR.PulisciStrSql(Motivazione) & "')"
            End If
            MyPar.ExecuteNonQuery()
            MyPar.CommandText = ""
            MyPar.Parameters.Clear()


        Catch ex As Exception
            InserisciEvento = False
            MyPar.Parameters.Clear()
        End Try

    End Function

End Class
