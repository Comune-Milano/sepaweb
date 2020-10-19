'GESTIONE LOTTI SCAMBIO COMPLESSI

Imports System.Collections

Partial Class LOTTI_GestioneLotto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreFiliale As String
    Public sValoreServizi As String
    Public sValoreServiziDettaglio As String
    Public sValoreEsercizio As String
    Public formPadre As String


    Dim lstListaComplessi1 As System.Collections.Generic.List(Of Epifani.ListaGenerale)
    Dim lstListaComplessi2 As System.Collections.Generic.List(Of Epifani.ListaGenerale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        lstListaComplessi1 = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))
        lstListaComplessi2 = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE2"), System.Collections.Generic.List(Of Epifani.ListaGenerale))


        If Not IsPostBack Then

            Try

                lstListaComplessi1.Clear()
                lstListaComplessi2.Clear()

                'VALORI PROVENIENTI DALLA RICERCA
                sValoreFiliale = UCase(Request.QueryString("FI"))
                sValoreServizi = UCase(Request.QueryString("SE"))
                sValoreServiziDettaglio = UCase(Request.QueryString("DT"))
                sValoreEsercizio = UCase(Request.QueryString("EF"))

                formPadre = UCase(Request.QueryString("TIPO"))  'RICERCA NORMALE o PER LE VARIAZIONI
                '****************************************************

                vId = 0
                vId = Session.Item("ID")

                vId2 = 0

                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                End If

                Me.txtEvento1.Value = ""
                Me.txtEvento2.Value = ""

                Me.txtID_Complessi1.Value = ""
                Me.txtID_Complessi2.Value = ""  'NON PIU' UTILIZZATO


                Me.txtSTATO.Value = 0
                Me.txtTipoScambio.Value = 0 '0 LOTTI_PATRIMONIO (No SOldi STANZIATI), 1=LOTTI_PATRIMONIO_IMPORTI (Stanziati i SOLDI)

                If vId <> 0 Then
                    Me.btnINDIETRO.Visible = True
                    VisualizzaDati()
                    txtindietro.Text = 0
                Else
                    Me.btnINDIETRO.Visible = False
                    txtindietro.Text = 1
                End If


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        DirectCast(CTRL, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next


                Me.txtConsumo1.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtConsumo1.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtConsumo1EXP.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtConsumo1EXP.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")
                Me.txtConsumo1EXP.Attributes.Add("onChange", "javascript:AggiornaImporti(1);")

                Me.txtConsumo1_FINALE.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtConsumo1_FINALE.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")


                Me.txtCanone1.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtCanone1.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtCanone1EXP.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtCanone1EXP.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")
                Me.txtCanone1EXP.Attributes.Add("onChange", "javascript:AggiornaImporti(2);")

                Me.txtCanone1_FINALE.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtCanone1_FINALE.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")



                Me.txtConsumo2.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtConsumo2.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtConsumo2IMP.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtConsumo2IMP.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtConsumo2_FINALE.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtConsumo2_FINALE.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")


                Me.txtCanone2.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtCanone2.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtCanone2IMP.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtCanone2IMP.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtCanone2_FINALE.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtCanone2_FINALE.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")



                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")

                'If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 7, 1) = 0 Then
                '    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                '    FrmSolaLettura()
                'End If

            Catch ex As Exception
                Me.lblErrore.Visible = True
                par.OracleConn.Close()
                lblErrore.Text = ex.Message

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

            End Try
        End If

    End Sub

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

    Public Property vId() As Long
        Get
            If Not (ViewState("par_idLotto") Is Nothing) Then
                Return CLng(ViewState("par_idLotto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idLotto") = value
        End Set

    End Property

    Public Property vId2() As Long
        Get
            If Not (ViewState("par_idLotto2") Is Nothing) Then
                Return CLng(ViewState("par_idLotto2"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idLotto2") = value
        End Set

    End Property


    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader

        Try
            sValoreServizi = UCase(Request.QueryString("SE"))
            sValoreServiziDettaglio = UCase(Request.QueryString("DT"))


            Me.cmbLotto1.Items.Clear()
            Me.cmbLotto1.Items.Add(New ListItem(" ", -1))

            Me.cmbLotto1.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            Me.cmbLotto1.Items(1).Selected = True
            Me.cmbLotto1.Enabled = False


            Me.cmbLotto2.Items.Clear()
            Me.cmbLotto2.Items.Add(New ListItem(" ", -1))

            'ESERCIZIO FINANZIARIO
            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                               & " where ID=" & par.IfNull(myReader1("ID_ESERCIZIO_FINANZIARIO"), -1)


            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                Me.txtEsercizio.Text = par.IfNull(myReaderT("INIZIO") & "-" & myReaderT("FINE"), " ")
            End If
            myReaderT.Close()


            'FILIALE
            par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI " _
                               & " where SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID " _
                               & "   and SISCOM_MI.TAB_FILIALI.ID=" & par.IfNull(myReader1("ID_FILIALE"), -1)

            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                Me.txtFiliale.Text = par.IfNull(myReaderT("NOME"), " ") & "  -  " & par.IfNull(myReaderT("DESCRIZIONE"), "") & " " & par.IfNull(myReaderT("CIVICO"), "") & " " & par.IfNull(myReaderT("LOCALITA"), "")
            End If
            myReaderT.Close()


            'SERVIZIO
            par.cmd.CommandText = "select  SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE " _
                               & " from SISCOM_MI.TAB_SERVIZI " _
                               & " where SISCOM_MI.TAB_SERVIZI.ID = " & sValoreServizi

            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                Me.txtServizi.Text = par.IfNull(myReaderT("DESCRIZIONE"), " ")
                Me.txtID_Servizio.value = par.IfNull(myReaderT("ID"), "-1")
            End If
            myReaderT.Close()


            'SERVIZIO DETTAGLIO
            par.cmd.CommandText = "select  SISCOM_MI.TAB_SERVIZI_VOCI.ID, SISCOM_MI.TAB_SERVIZI_VOCI.DESCRIZIONE " _
                               & " from SISCOM_MI.TAB_SERVIZI_VOCI " _
                               & " where SISCOM_MI.TAB_SERVIZI_VOCI.ID = " & sValoreServiziDettaglio

            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                Me.txtServiziDettaglio.Text = par.IfNull(myReaderT("DESCRIZIONE"), " ")
                Me.txtID_ServizioDettaglio.Value = par.IfNull(myReaderT("ID"), "-1")
            End If
            myReaderT.Close()



            'COMPLESSI LOTTO1
            Me.lstcomplessi1.Items.Clear()
            lstListaComplessi1.Clear()

            par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                               & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                               & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI.ID_COMPLESSO " _
                                                                            & " from  SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI " _
                                                                            & " where ID_LOTTO=" & vId _
                                                                            & "   and ID_VOCE_IMPORTO in (select ID " _
                                                                                                      & " from  SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                      & " where ID_LOTTO=" & vId _
                                                                                                      & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio & ")) " _
                               & " order by DENOMINAZIONE ASC"


            myReaderT = par.cmd.ExecuteReader()
            While myReaderT.Read
                lstcomplessi1.Items.Add(New ListItem(par.IfNull(myReaderT("DENOMINAZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))

                Dim gen As Epifani.ListaGenerale

                gen = New Epifani.ListaGenerale(par.IfNull(myReaderT("ID"), -1), par.IfNull(myReaderT("DENOMINAZIONE"), " "))
                lstListaComplessi1.Add(gen)
                gen = Nothing
                txtTipoScambio.Value = 1
            End While
            myReaderT.Close()


            If txtTipoScambio.Value = 0 Then
                'IMPORTI INSERITI A MANO
                par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                   & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO " _
                                                                                & " from  SISCOM_MI.LOTTI_PATRIMONIO " _
                                                                                & " where SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=" & vId & ") " _
                                   & " order by DENOMINAZIONE ASC"


                myReaderT = par.cmd.ExecuteReader()
                While myReaderT.Read
                    lstcomplessi1.Items.Add(New ListItem(par.IfNull(myReaderT("DENOMINAZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))

                    Dim gen As Epifani.ListaGenerale

                    gen = New Epifani.ListaGenerale(par.IfNull(myReaderT("ID"), -1), par.IfNull(myReaderT("DENOMINAZIONE"), " "))
                    lstListaComplessi1.Add(gen)
                    gen = Nothing

                End While
                myReaderT.Close()

                'LOTTO 2
                par.cmd.CommandText = "select  SISCOM_MI.LOTTI.* " _
                                   & " from SISCOM_MI.LOTTI " _
                                   & " where SISCOM_MI.LOTTI.ID_FILIALE=" & par.IfNull(myReader1("ID_FILIALE"), -1) _
                                   & "   and SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO=" & par.IfNull(myReader1("ID_ESERCIZIO_FINANZIARIO"), -1) _
                                   & "   and SISCOM_MI.LOTTI.ID<>" & par.IfNull(myReader1("ID"), -1) _
                                   & "   and SISCOM_MI.LOTTI.ID not in (select ID_LOTTO from SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI)"

                myReaderT = par.cmd.ExecuteReader()
                While myReaderT.Read()
                    Me.cmbLotto2.Items.Add(New ListItem(par.IfNull(myReaderT("DESCRIZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))
                End While
                myReaderT.Close()

            Else

                'LOTTO 2
                par.cmd.CommandText = "select  SISCOM_MI.LOTTI.* " _
                                   & " from SISCOM_MI.LOTTI " _
                                   & " where SISCOM_MI.LOTTI.ID_FILIALE=" & par.IfNull(myReader1("ID_FILIALE"), -1) _
                                   & "   and SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO=" & par.IfNull(myReader1("ID_ESERCIZIO_FINANZIARIO"), -1) _
                                   & "   and SISCOM_MI.LOTTI.ID in (select ID_LOTTO from SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI " _
                                                                & " where ID_LOTTO<>" & par.IfNull(myReader1("ID"), -1) _
                                                                & "   and ID_VOCE_IMPORTO in (select ID " _
                                                                                          & " from  SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                          & " where ID_LOTTO<>" & par.IfNull(myReader1("ID"), -1) _
                                                                                          & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio & ")) "

                myReaderT = par.cmd.ExecuteReader()
                While myReaderT.Read()
                    Me.cmbLotto2.Items.Add(New ListItem(par.IfNull(myReaderT("DESCRIZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))
                End While
                myReaderT.Close()

            End If


            'IMPORTI
            par.cmd.CommandText = "select  * " _
                               & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                               & " where ID_LOTTO = " & vId _
                               & "  and  ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio

            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                Me.txtCanone1.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CANONE"), 0), "", "##,##0.00")
                Me.txtConsumo1.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CONSUMO"), 0), "", "##,##0.00")
            End If
            myReaderT.Close()


        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try

            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vId <> 0 Then

                par.cmd.CommandText = "select * from SISCOM_MI.LOTTI  where ID=" & vId & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")
            End If


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Lotto visualizzata da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If


                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = "select * from SISCOM_MI.LOTTI where ID=" & vId
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                Me.txtSTATO.Value = 0
                FrmSolaLettura()

            Else
                par.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub



    Public Function ControlloCampi() As Boolean

        ControlloCampi = True
        lblErrore.Visible = False


        If Me.cmbLotto2.SelectedValue = -1 Then
            Response.Write("<script>alert('Selezionare il secondo lotto!');</script>")
            ControlloCampi = False
            cmbLotto2.Focus()
            Exit Function
        End If

        If lstcomplessi1.Items.Count = 0 Then
            Response.Write("<script>alert('Attenzione...Il lotto di origine deve avere almeno un complesso!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If lstcomplessi2.Items.Count = 0 Then
            Response.Write("<script>alert('Attenzione...Il lotto di destinazione deve avere almeno un complesso!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If txtTipoScambio.Value = 0 Then
            If Val(Me.txtConsumo1EXP.Text) = 0 Then
                Response.Write("<script>alert('Attenzione...Inserire l\'importo a consumo da trasferire nel lotto di destinazione!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtTipoScambio.Value = 0 Then
            If Val(Me.txtCanone1EXP.Text) = 0 Then
                Response.Write("<script>alert('Attenzione...Inserire l\'importo a canone da trasferire nel lotto di destinazione!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

    End Function

    Private Sub Update()
        Dim i As Integer
        Dim pos As Integer

        Dim sStr1, sStr2 As String

        Try
            sValoreServiziDettaglio = UCase(Request.QueryString("DT"))


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans



            If Me.txtTipoScambio.Value = 0 Then
                'IMPORTI INSERITI A MANO
                'IMPORTI LOTTO1
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " update SISCOM_MI.PF_VOCI_IMPORTO " _
                                    & " set VALORE_CANONE=:val_canone,VALORE_CONSUMO=:val_consumo " _
                                    & " where ID_LOTTO = " & vId _
                                    & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("val_canone", strToNumber(Me.txtCanone1_TMP.Value.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("val_consumo", strToNumber(Me.txtConsumo1_TMP.Value.Replace(".", ""))))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()


                'IMPORTI LOTTO2
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " update SISCOM_MI.PF_VOCI_IMPORTO " _
                                    & " set VALORE_CANONE=:val_canone,VALORE_CONSUMO=:val_consumo " _
                                    & " where ID_LOTTO = " & vId2 _
                                    & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("val_canone", strToNumber(Me.txtCanone2_TMP.Value.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("val_consumo", strToNumber(Me.txtConsumo2_TMP.Value.Replace(".", ""))))

                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()

            Else
                'IMPORTI DA LOTTI_PATRIMONIO_IMPORTI
                If Me.txtID_Complessi1.Value <> "" Then
                    sStr1 = Me.txtID_Complessi1.Value

                    pos = sStr1.IndexOf("&&&")

                    Do While pos >= 0
                        sStr2 = sStr1.Substring(0, pos)

                        If EffettuaScambio(vId, Me.cmbLotto2.SelectedValue, Val(sStr2)) = False Then Exit Sub

                        sStr1 = sStr1.Substring(pos + 3)
                        pos = sStr1.IndexOf("&&&")
                    Loop

                    If Strings.Len(Strings.Trim(sStr1)) > 0 Then
                        If EffettuaScambio(vId, Me.cmbLotto2.SelectedValue, Val(sStr1)) = False Then Exit Sub
                    End If
                End If
            End If


            ''LOTTI_PATRIMONIO 1
            par.cmd.CommandText = "delete from SISCOM_MI.LOTTI_PATRIMONIO where ID_LOTTO = " & vId
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""


            For i = 0 To lstcomplessi1.Items.Count - 1

                par.cmd.CommandText = " insert into SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_COMPLESSO, ID_EDIFICIO)" _
                                    & " values(" & vId & "," & Me.lstcomplessi1.Items(i).Value & ",NULL)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
            Next
            '*********************************************



            ''LOTTI_PATRIMONIO 2
            par.cmd.CommandText = "delete from SISCOM_MI.LOTTI_PATRIMONIO where ID_LOTTO = " & Me.cmbLotto2.SelectedValue
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""


            For i = 0 To lstcomplessi2.Items.Count - 1

                par.cmd.CommandText = " insert into SISCOM_MI.LOTTI_PATRIMONIO(ID_LOTTO, ID_COMPLESSO, ID_EDIFICIO)" _
                                    & " values(" & Me.cmbLotto2.SelectedValue & "," & Me.lstcomplessi2.Items(i).Value & ",NULL)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
            Next
            '**********************************************+


            'EVENTI
            If Me.txtEvento1.Value <> "" Then
                sStr1 = Me.txtEvento1.Value

                pos = sStr1.IndexOf("&&&")

                Do While pos >= 0
                    sStr2 = sStr1.Substring(0, pos)
                    'INSERT sstr2

                    ''****Scrittura evento LOTTO1
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','SPOSTATO NEL LOTTO: " & par.PulisciStrSql(Me.cmbLotto2.SelectedItem.ToString) & " il complesso: " & par.PulisciStrSql(sStr2) & "')"

                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','SPOSTATO NEL LOTTO: " & par.PulisciStrSql(Me.cmbLotto2.SelectedItem.ToString & " l'importo a canone di euro: " & Me.txtCanone1EXP_TMP.Value) & "')"

                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','SPOSTATO NEL LOTTO: " & par.PulisciStrSql(Me.cmbLotto2.SelectedItem.ToString & " l'importo a consumo di euro: " & Me.txtConsumo1EXP_TMP.Value) & "')"

                    par.cmd.ExecuteNonQuery()
                    '****************************************************

                    ''****Scrittura evento LOTTO2
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & Me.cmbLotto2.SelectedValue & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','IMPORTATO DAL LOTTO: " & par.PulisciStrSql(Me.cmbLotto1.SelectedItem.ToString) & " il complesso: " & par.PulisciStrSql(sStr2) & "')"

                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & Me.cmbLotto2.SelectedValue & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','IMPORTATO DAL LOTTO: " & par.PulisciStrSql(Me.cmbLotto1.SelectedItem.ToString & " l'importo a canone di euro: " & Me.txtCanone1EXP_TMP.Value) & "')"

                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & Me.cmbLotto2.SelectedValue & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','IMPORTATO DAL LOTTO: " & par.PulisciStrSql(Me.cmbLotto1.SelectedItem.ToString & " l'importo a consumo di euro: " & Me.txtConsumo1EXP_TMP.Value) & "')"

                    par.cmd.ExecuteNonQuery()
                    '****************************************************

                    sStr1 = sStr1.Substring(pos + 3)
                    pos = sStr1.IndexOf("&&&")
                Loop

                If Strings.Len(Strings.Trim(sStr1)) > 0 Then

                    ''****Scrittura evento LOTTO1
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','SPOSTATO NEL LOTTO: " & par.PulisciStrSql(Me.cmbLotto2.SelectedItem.ToString) & " il complesso: " & par.PulisciStrSql(sStr1) & "')"

                    par.cmd.ExecuteNonQuery()
                    '****************************************************

                    ''****Scrittura evento LOTTO2
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & Me.cmbLotto2.SelectedValue & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','IMPORTATO DAL LOTTO: " & par.PulisciStrSql(Me.cmbLotto1.SelectedItem.ToString) & " il complesso: " & par.PulisciStrSql(sStr1) & "')"

                    par.cmd.ExecuteNonQuery()
                    '****************************************************

                End If
            End If


            If Me.txtEvento2.Value <> "" Then
                sStr1 = Me.txtEvento2.Value

                pos = sStr1.IndexOf("&&&")

                Do While pos >= 0
                    sStr2 = sStr1.Substring(0, pos)
                    'INSERT sstr2

                    ''****Scrittura evento LOTTO2
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','IMPORTATO DAL LOTTO: " & par.PulisciStrSql(Me.cmbLotto2.SelectedItem.ToString) & " il complesso: " & par.PulisciStrSql(sStr2) & "')"

                    par.cmd.ExecuteNonQuery()
                    '****************************************************

                    ''****Scrittura evento LOTTO2
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & Me.cmbLotto2.SelectedValue & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','SPOSTATO NEL LOTTO: " & par.PulisciStrSql(Me.cmbLotto1.SelectedItem.ToString) & " il complesso: " & par.PulisciStrSql(sStr2) & "')"

                    par.cmd.ExecuteNonQuery()
                    '****************************************************

                    sStr1 = sStr1.Substring(pos + 3)
                    pos = sStr1.IndexOf("&&&")
                Loop

                If Strings.Len(Strings.Trim(sStr1)) > 0 Then

                    ''****Scrittura evento LOTTO1
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','IMPORTATO DAL LOTTO: " & par.PulisciStrSql(Me.cmbLotto2.SelectedItem.ToString) & " il complesso: " & par.PulisciStrSql(sStr1) & "')"

                    par.cmd.ExecuteNonQuery()
                    '****************************************************

                    ''****Scrittura evento LOTTO2
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & Me.cmbLotto2.SelectedValue & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                       & "', 'F02','SPOSTATO NEL LOTTO: " & par.PulisciStrSql(Me.cmbLotto1.SelectedItem.ToString) & " il complesso: " & par.PulisciStrSql(sStr1) & "')"

                    par.cmd.ExecuteNonQuery()
                    '****************************************************

                End If
            End If
            '*********************************



            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            Response.Write("<SCRIPT>alert('Attenzione...! Ricordati di variare i relativi contratti!');</SCRIPT>")


            'IMPORTI
            par.cmd.CommandText = "select  * " _
                               & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                               & " where ID_LOTTO = " & vId _
                               & "  and  ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio
            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                Me.txtCanone1.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CANONE"), 0), "", "##,##0.00")
                Me.txtConsumo1.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CONSUMO"), 0), "", "##,##0.00")

                Me.txtCanone1EXP.Text = ""
                Me.txtConsumo1EXP.Text = ""

                Me.txtCanone1_FINALE.Text = ""
                Me.txtConsumo1_FINALE.Text = ""

            End If
            myReaderT.Close()


            par.cmd.CommandText = "select  * " _
                   & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                   & " where ID_LOTTO = " & vId2 _
                   & "  and  ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio

            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                Me.txtCanone2.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CANONE"), 0), "", "##,##0.00")
                Me.txtConsumo2.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CONSUMO"), 0), "", "##,##0.00")

                Me.txtCanone2IMP.Text = ""
                Me.txtConsumo2IMP.Text = ""

                Me.txtCanone2_FINALE.Text = ""
                Me.txtConsumo2_FINALE.Text = ""

            End If
            myReaderT.Close()


            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"

            Me.txtSTATO.Value = 1

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        If ControlloCampi() = False Then
            If Val(Me.txtConsumo1EXP.Text) > 0 Then
                CalcolaImporti(1)
            End If

            If Val(Me.txtCanone1EXP.Text) > 0 Then
                CalcolaImporti(2)
            End If
            Exit Sub
        End If

        Me.Update()

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Dim FlagConnessione As Boolean
        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader

        Try

            If Me.txtAnnulla.Text = "1" Then

                sValoreServiziDettaglio = UCase(Request.QueryString("DT"))

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If



                'RIPRISTINO COMPLESSI LOTTO 1
                Me.lstcomplessi1.Items.Clear()
                lstListaComplessi1.Clear()


                par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                   & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI.ID_COMPLESSO " _
                                                                                & " from  SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI " _
                                                                                & " where ID_LOTTO=" & vId _
                                                                                & "   and ID_VOCE_IMPORTO in (select ID " _
                                                                                                          & " from  SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                          & " where ID_LOTTO=" & vId _
                                                                                                          & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio & ")) " _
                                   & " order by DENOMINAZIONE ASC"


                myReaderT = par.cmd.ExecuteReader()
                While myReaderT.Read
                    lstcomplessi1.Items.Add(New ListItem(par.IfNull(myReaderT("DENOMINAZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))


                    Dim gen As Epifani.ListaGenerale

                    gen = New Epifani.ListaGenerale(par.IfNull(myReaderT("ID"), -1), par.IfNull(myReaderT("DENOMINAZIONE"), " "))
                    lstListaComplessi1.Add(gen)
                    gen = Nothing
                    txtTipoScambio.Value = 1
                End While
                myReaderT.Close()


                If txtTipoScambio.Value = 0 Then
                    par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                       & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                       & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO " _
                                                                                    & " from  SISCOM_MI.LOTTI_PATRIMONIO " _
                                                                                    & " where SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=" & vId & ") " _
                                       & " order by DENOMINAZIONE ASC"


                    myReaderT = par.cmd.ExecuteReader()
                    While myReaderT.Read
                        lstcomplessi1.Items.Add(New ListItem(par.IfNull(myReaderT("DENOMINAZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))


                        Dim gen As Epifani.ListaGenerale

                        gen = New Epifani.ListaGenerale(par.IfNull(myReaderT("ID"), -1), par.IfNull(myReaderT("DENOMINAZIONE"), " "))
                        lstListaComplessi1.Add(gen)
                        gen = Nothing

                    End While
                    myReaderT.Close()


                    Me.txtCanone1EXP.ReadOnly = False
                    Me.txtConsumo1EXP.ReadOnly = False
                Else

                    Me.txtCanone1EXP.ReadOnly = True
                    Me.txtConsumo1EXP.ReadOnly = True
                End If


                'RIPRISTINO COMPLESSI LOTTO 2
                lstListaComplessi2.Clear()
                Me.lstcomplessi2.Items.Clear()

                If txtTipoScambio.Value = 0 Then
                    par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                       & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                       & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO " _
                                                                                    & " from  SISCOM_MI.LOTTI_PATRIMONIO " _
                                                                                    & " where SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=" & cmbLotto2.SelectedValue & ") " _
                                       & " order by DENOMINAZIONE ASC"


                Else
                    par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                       & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                       & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI.ID_COMPLESSO " _
                                                                                    & " from  SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI " _
                                                                                    & " where ID_LOTTO=" & cmbLotto2.SelectedValue _
                                                                                    & "   and ID_VOCE_IMPORTO in (select ID " _
                                                                                                              & " from  SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                              & " where ID_LOTTO=" & cmbLotto2.SelectedValue _
                                                                                                              & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio & ")) " _
                                       & " order by DENOMINAZIONE ASC"

                End If




                myReaderT = par.cmd.ExecuteReader()
                While myReaderT.Read
                    lstcomplessi2.Items.Add(New ListItem(par.IfNull(myReaderT("DENOMINAZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))

                    Dim gen As Epifani.ListaGenerale

                    gen = New Epifani.ListaGenerale(par.IfNull(myReaderT("ID"), -1), par.IfNull(myReaderT("DENOMINAZIONE"), " "))
                    lstListaComplessi2.Add(gen)
                    gen = Nothing

                End While
                myReaderT.Close()

                Me.txtSTATO.Value = 1

                Me.txtEvento1.Value = ""
                Me.txtEvento2.Value = ""

                Me.txtID_Complessi1.Value = ""
                Me.txtID_Complessi2.Value = ""

                Me.txtConsumo1EXP.Text = ""
                Me.txtCanone1EXP.Text = ""

                Me.txtConsumo1_FINALE.Text = ""
                Me.txtCanone1_FINALE.Text = ""

                Me.txtConsumo2IMP.Text = ""
                Me.txtCanone2IMP.Text = ""

                Me.txtConsumo2_FINALE.Text = ""
                Me.txtCanone2_FINALE.Text = ""


                'IMPORTI LOTTO1
                par.cmd.CommandText = "select  * " _
                                   & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                                   & " where ID_LOTTO = " & vId _
                                   & "  and  ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio

                myReaderT = par.cmd.ExecuteReader()

                If myReaderT.Read Then
                    Me.txtCanone1.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CANONE"), 0), "", "##,##0.00")
                    Me.txtConsumo1.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CONSUMO"), 0), "", "##,##0.00")
                End If
                myReaderT.Close()

                'IMPORTI LOTTO2
                par.cmd.CommandText = "select  * " _
                                   & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                                   & " where ID_LOTTO = " & cmbLotto2.SelectedValue _
                                   & "  and  ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio

                myReaderT = par.cmd.ExecuteReader()

                If myReaderT.Read Then
                    Me.txtCanone2.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CANONE"), 0), "", "##,##0.00")
                    Me.txtConsumo2.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CONSUMO"), 0), "", "##,##0.00")
                End If
                myReaderT.Close()

                Me.cmbLotto2.Enabled = True

            Else
                CType(Me.Page.FindControl("txtAnnulla"), TextBox).Text = "0"
            End If


        Catch ex As Exception

            If FlagConnessione = True Then par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()


            Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub

    Protected Sub btnINDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            sValoreEsercizio = UCase(Request.QueryString("EF"))
            sValoreFiliale = UCase(Request.QueryString("FI"))
            sValoreServizi = UCase(Request.QueryString("SE"))
            sValoreServiziDettaglio = UCase(Request.QueryString("DT"))

            formPadre = UCase(Request.QueryString("TIPO"))


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")


            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
            Else
                Response.Write("<script>location.replace('RisultatiLotti.aspx?EF=" & sValoreEsercizio _
                                                                          & "&FI=" & sValoreFiliale _
                                                                          & "&SE=" & sValoreServizi _
                                                                          & "&DT=" & sValoreServiziDettaglio _
                                                                          & "&TIPO=" & formPadre & "');</script>")
            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

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

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                End If
            Next


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    'LOTTO2
    Protected Sub cmbLotto2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLotto2.SelectedIndexChanged
        Dim FlagConnessione As Boolean

        Try
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            sValoreServiziDettaglio = UCase(Request.QueryString("DT"))

            vId2 = 0

            If cmbLotto2.SelectedValue <> "-1" Then

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If


                vId2 = Me.cmbLotto2.SelectedValue

                'COMPLESSI LOTTO1

                'lstListaComplessi2 = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE2"), System.Collections.Generic.List(Of Epifani.ListaGenerale))
                lstListaComplessi2.Clear()

                Me.lstcomplessi2.Items.Clear()

                If txtTipoScambio.Value = 0 Then

                    par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                       & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                       & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO " _
                                                                                    & " from  SISCOM_MI.LOTTI_PATRIMONIO " _
                                                                                    & " where SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=" & vId2 & ") " _
                                       & " order by DENOMINAZIONE ASC"

                    Me.txtCanone1EXP.ReadOnly = False
                    Me.txtConsumo1EXP.ReadOnly = False

                Else
                    par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO) as DENOMINAZIONE " _
                                       & " from  SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                       & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID in (select SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI.ID_COMPLESSO " _
                                                                                    & " from  SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI " _
                                                                                    & " where ID_LOTTO=" & vId2 _
                                                                                    & "   and ID_VOCE_IMPORTO in (select ID " _
                                                                                                              & " from  SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                              & " where ID_LOTTO=" & vId2 _
                                                                                                              & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio & ")) " _
                                       & " order by DENOMINAZIONE ASC"

                    Me.txtCanone1EXP.ReadOnly = True
                    Me.txtConsumo1EXP.ReadOnly = True
                End If

                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                myReaderT = par.cmd.ExecuteReader()
                While myReaderT.Read
                    lstcomplessi2.Items.Add(New ListItem(par.IfNull(myReaderT("DENOMINAZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))

                    Dim gen As Epifani.ListaGenerale

                    gen = New Epifani.ListaGenerale(par.IfNull(myReaderT("ID"), -1), par.IfNull(myReaderT("DENOMINAZIONE"), " "))
                    lstListaComplessi2.Add(gen)
                    gen = Nothing

                End While
                myReaderT.Close()


                'IMPORTI
                par.cmd.CommandText = "select  * " _
                                   & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                                   & " where ID_LOTTO = " & vId2 _
                                   & "  and  ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio

                myReaderT = par.cmd.ExecuteReader()

                If myReaderT.Read Then
                    Me.txtCanone2.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CANONE"), 0), "", "##,##0.00")
                    Me.txtConsumo2.Text = IsNumFormat(par.IfNull(myReaderT("VALORE_CONSUMO"), 0), "", "##,##0.00")
                End If
                myReaderT.Close()

            End If



        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If FlagConnessione = True Then par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub


    Protected Sub btn_Destra_1_2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Destra_1_2.Click
        'TRASFERISCE I COMPLESSI SELEZIONATI DAL LOTTO1 AL LOTTO2
        Dim i, j As Integer
        Dim FlagConnessione As Boolean

        Dim ComplessoTrasferito As Boolean
        Dim TuttiTrasferiti As Boolean

        Dim Imp_ConsumoLordo1 As Decimal
        Dim Imp_CanoneLordo1 As Decimal


        Try

            'CONTROLLI
            ComplessoTrasferito = False
            TuttiTrasferiti = True

            If Me.cmbLotto2.SelectedValue = -1 Then
                Response.Write("<script>alert('Selezionare il secondo lotto!');</script>")
                Me.cmbLotto2.Focus()
                Exit Sub
            End If

            For i = 0 To lstcomplessi1.Items.Count - 1
                If lstcomplessi1.Items(i).Selected = True And Str(lstcomplessi1.Items(i).Value) > -1 Then
                    ComplessoTrasferito = True
                Else
                    TuttiTrasferiti = False
                End If

            Next i

            If ComplessoTrasferito = False Then
                Response.Write("<script>alert('Attenzione...Selezionare almeno un complesso da trasferire!');</script>")
                Me.cmbLotto2.Focus()
                Exit Sub
            End If

            If TuttiTrasferiti = True Then
                Response.Write("<script>alert('Attenzione...Impossibile trasferire tutti i complessi, almeno uno deve restare al lotto di origine!');</script>")
                Me.cmbLotto2.Focus()
                Exit Sub
            End If
            '*******************************+


            'ELABORAZIONE
            sValoreServiziDettaglio = UCase(Request.QueryString("DT"))
            Dim gen As Epifani.ListaGenerale

            For i = 0 To lstcomplessi1.Items.Count - 1
                If lstcomplessi1.Items(i).Selected = True And Str(lstcomplessi1.Items(i).Value) > -1 Then

                    ComplessoTrasferito = True
                    'ELIMINO IL COMPLESSO SELEZIONATO DALLA LISTA1
                    j = 0
                    For Each gen In lstListaComplessi1
                        If gen.ID = lstcomplessi1.Items(i).Value Then

                            lstListaComplessi1.RemoveAt(j)
                            If Me.txtEvento1.Value = "" Then
                                Me.txtEvento1.Value = gen.STR & " &&& "
                                Me.txtID_Complessi1.Value = gen.ID & " &&& "
                            Else
                                Me.txtEvento1.Value = Me.txtEvento1.Value & gen.STR & " &&& "
                                Me.txtID_Complessi1.Value = Me.txtID_Complessi1.Value & gen.ID & " &&& "
                            End If

                            Exit For
                        End If
                        j = j + 1
                    Next
                    gen = Nothing


                    'AGGIUNGO IL COMPLESSO SELEZIONATO NELLA LISTA 2
                    Dim gen2 As Epifani.ListaGenerale
                    gen2 = New Epifani.ListaGenerale(lstcomplessi1.Items(i).Value, lstcomplessi1.Items(i).Text)
                    lstListaComplessi2.Add(gen2)
                    gen2 = Nothing

                    If txtTipoScambio.Value = 1 Then
                        FlagConnessione = False
                        If par.OracleConn.State = Data.ConnectionState.Closed Then
                            par.OracleConn.Open()
                            par.SettaCommand(par)

                            FlagConnessione = True
                        End If

                        'par.cmd.CommandText = "select * from SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI " _
                        '    & " where ID_LOTTO=" & vId _
                        '    & "   and ID_COMPLESSO = " & lstcomplessi1.Items(i).Value _
                        '    & "   and ID_VOCE_IMPORTO in (select ID " _
                        '                              & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                        '                              & " where ID_LOTTO=" & vId _
                        '                              & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio & ")"


                        par.cmd.CommandText = "select SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI.*,SISCOM_MI.PF_VOCI_IMPORTO.*  " _
                                            & " from SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI,SISCOM_MI.PF_VOCI_IMPORTO " _
                                            & " where LOTTI_PATRIMONIO_IMPORTI.ID_LOTTO=" & vId _
                                            & "   and LOTTI_PATRIMONIO_IMPORTI.ID_COMPLESSO = " & lstcomplessi1.Items(i).Value _
                                            & "   and LOTTI_PATRIMONIO_IMPORTI.ID_VOCE_IMPORTO in (select ID  " _
                                                                                               & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                               & " where ID_LOTTO =" & vId _
                                                                                               & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio & ")" _
                                            & "   and LOTTI_PATRIMONIO_IMPORTI.ID_VOCE_IMPORTO=PF_VOCI_IMPORTO.ID (+) "

                        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                        If myReaderT.Read Then

                            'CALCOLO CONSUMO 
                            '   lotto 1
                            Imp_ConsumoLordo1 = ((par.IfNull(myReaderT("IMPORTO_CONSUMO_LORDO"), 0) * 100) / (100 + par.IfNull(myReaderT("IVA_CONSUMO"), 0)))

                            Imp_ConsumoLordo1 = par.IfEmpty(Me.txtConsumo1EXP.Text, 0) + Imp_ConsumoLordo1
                            Me.txtConsumo1EXP.Text = IsNumFormat(Imp_ConsumoLordo1, "", "##,##0.00")
                            'CALCOLO CONSUMO 
                            '   lotto 2
                            ' NOTA: si presuppone che la % di IVA_CONSUMO del lotto2 di PF_VOCI_IMPORTO sia uguale al lotto1
                            'Imp_ValoreCONSUMO2 = par.IfNull(myReaderT("VALORE_CONSUMO"), 0) + Imp_ConsumoLordo1


                            'CALCOLO CANONE
                            '   lotto 1
                            Imp_CanoneLordo1 = ((par.IfNull(myReaderT("IMPORTO_CANONE_LORDO"), 0) * 100) / (100 + par.IfNull(myReaderT("IVA_CANONE"), 0)))


                            Imp_CanoneLordo1 = par.IfEmpty(Me.txtCanone1EXP.Text, 0) + Imp_CanoneLordo1
                            Me.txtCanone1EXP.Text = IsNumFormat(Imp_CanoneLordo1, "", "##,##0.00")

                            ' Imp_ValoreCANONE1 = par.IfNull(myReaderT("VALORE_CANONE"), 0) - Imp_ConsumoLordo1

                            'CALCOLO CONSUMO 
                            '   lotto 2
                            ' NOTA: si presuppone che la % di VALORE_CANONE del lotto2 di PF_VOCI_IMPORTO sia uguale al lotto1
                            'Imp_ValoreCANONE2 = par.IfNull(myReaderT("VALORE_CANONE"), 0) + Imp_CanoneLordo1

                            CalcolaImporti(1)
                            CalcolaImporti(2)
                        End If
                        myReaderT.Close()
                    End If

                End If
            Next



            'RIAGGIORNO LA LISTA
            Me.lstcomplessi1.Items.Clear()
            For Each gen In lstListaComplessi1
                lstcomplessi1.Items.Add(New ListItem(par.IfNull(gen.STR, " "), gen.ID))
            Next
            gen = Nothing


            Me.lstcomplessi2.Items.Clear()

            For Each gen In lstListaComplessi2
                lstcomplessi2.Items.Add(New ListItem(par.IfNull(gen.STR, " "), gen.ID))
            Next
            gen = Nothing

            Me.txtSTATO.Value = 2
            Me.cmbLotto2.Enabled = False


        Catch ex As Exception
            If FlagConnessione = True Then par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)

            Session.Item("LAVORAZIONE") = "0"
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try

    End Sub

    'Protected Sub btn_Sinistra_2_1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Sinistra_2_1.Click
    '    'TRASFERISCE I COMPLESSI SELEZIONATI DAL LOTTO2 AL LOTTO1
    '    Dim i, j As Integer

    '    Try
    '        If Me.cmbLotto2.SelectedValue = -1 Then
    '            Response.Write("<script>alert('Selezionare il secondo lotto!');</script>")
    '            cmbLotto2.Focus()
    '            Exit Sub
    '        End If

    '        Dim gen As Epifani.ListaGenerale


    '        For i = 0 To lstcomplessi2.Items.Count - 1
    '            If lstcomplessi2.Items(i).Selected = True And Str(lstcomplessi2.Items(i).Value) > -1 Then

    '                'ELIMINO IL COMPLESSO SELEZIONATO DALLA LISTA2
    '                j = 0
    '                For Each gen In lstListaComplessi2
    '                    If gen.ID = lstcomplessi2.Items(i).Value Then

    '                        lstListaComplessi2.RemoveAt(j)

    '                        If Me.txtEvento2.Value = "" Then
    '                            Me.txtEvento2.Value = gen.STR & " &&& "
    '                            Me.txtID_Complessi2.Value = gen.ID & " &&& "
    '                        Else
    '                            Me.txtEvento2.Value = Me.txtEvento2.Value & gen.STR & " &&& "
    '                            Me.txtID_Complessi2.Value = Me.txtID_Complessi2.Value & gen.ID & " &&& "
    '                        End If

    '                        Exit For
    '                    End If
    '                    j = j + 1
    '                Next
    '                gen = Nothing


    '                'AGGIUNGO IL COMPLESSO SELEZIONATO NELLA LISTA 1
    '                Dim gen2 As Epifani.ListaGenerale
    '                gen2 = New Epifani.ListaGenerale(lstcomplessi2.Items(i).Value, lstcomplessi2.Items(i).Text)
    '                lstListaComplessi1.Add(gen2)
    '                gen2 = Nothing

    '            End If
    '        Next

    '        'RIAGGIORNO LE LISTE
    '        Me.lstcomplessi1.Items.Clear()
    '        For Each gen In lstListaComplessi1
    '            lstcomplessi1.Items.Add(New ListItem(par.IfNull(gen.STR, " "), gen.ID))
    '        Next
    '        gen = Nothing


    '        Me.lstcomplessi2.Items.Clear()
    '        For Each gen In lstListaComplessi2
    '            lstcomplessi2.Items.Add(New ListItem(par.IfNull(gen.STR, " "), gen.ID))
    '        Next
    '        gen = Nothing

    '        Me.txtSTATO.Value = 2


    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)

    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message

    '    End Try

    'End Sub


    Function EffettuaScambio(ByVal ID_LOTTO1 As Long, ByVal ID_LOTTO2 As Long, ByVal ID_COMPLESSO As Long) As Boolean
        Dim sSQL1, sSQL2 As String

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        Dim Imp_ConsumoLordo1 As Decimal = 0
        Dim Imp_CanoneLordo1 As Decimal = 0

        Dim Imp_ValoreCONSUMO1 As Decimal = 0
        Dim Imp_ValoreCONSUMO2 As Decimal = 0

        Dim Imp_ValoreCANONE1 As Decimal = 0
        Dim Imp_ValoreCANONE2 As Decimal = 0

        Try

            sValoreServiziDettaglio = UCase(Request.QueryString("DT"))
            EffettuaScambio = True

            '' RIPRENDO LA CONNESSIONE
            'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            'par.SettaCommand(par)

            ''RIPRENDO LA TRANSAZIONE
            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans


            'LOTTO1
            sSQL1 = "select SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI.*,SISCOM_MI.PF_VOCI_IMPORTO.*  " _
                & " from SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI,SISCOM_MI.PF_VOCI_IMPORTO " _
                & " where LOTTI_PATRIMONIO_IMPORTI.ID_LOTTO=" & ID_LOTTO1 _
                & "   and LOTTI_PATRIMONIO_IMPORTI.ID_COMPLESSO = " & ID_COMPLESSO _
                & "   and LOTTI_PATRIMONIO_IMPORTI.ID_VOCE_IMPORTO in (select ID  " _
                                                                   & " from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                   & " where ID_LOTTO =" & ID_LOTTO1 _
                                                                   & "   and ID_VOCE_SERVIZIO=" & sValoreServiziDettaglio & ")" _
                & "   and LOTTI_PATRIMONIO_IMPORTI.ID_VOCE_IMPORTO=PF_VOCI_IMPORTO.ID (+) "

            par.cmd.CommandText = sSQL1
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then

                'LOTTO2     PF_VOCI_IMPORTO   myReader3
                sSQL2 = "select * from SISCOM_MI.PF_VOCI_IMPORTO " _
                              & " where ID_VOCE_SERVIZIO=" & par.IfNull(myReader1("ID_VOCE_SERVIZIO"), -1) _
                              & "   and ID_LOTTO=" & ID_LOTTO2

                par.cmd.CommandText = sSQL2
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then

                    'CALCOLO CONSUMO 
                    '   lotto 1
                    Imp_ConsumoLordo1 = ((par.IfNull(myReader1("IMPORTO_CONSUMO_LORDO"), 0) * 100) / (100 + par.IfNull(myReader1("IVA_CONSUMO"), 0)))
                    Imp_ValoreCONSUMO1 = par.IfNull(myReader1("VALORE_CONSUMO"), 0) - Imp_ConsumoLordo1

                    '   lotto 2
                    ' NOTA: si presuppone che la % di IVA_CONSUMO del lotto2 di PF_VOCI_IMPORTO sia uguale al lotto1
                    Imp_ValoreCONSUMO2 = par.IfNull(myReader2("VALORE_CONSUMO"), 0) + Imp_ConsumoLordo1


                    'CALCOLO CANONE
                    '   lotto 1
                    Imp_CanoneLordo1 = ((par.IfNull(myReader1("IMPORTO_CANONE_LORDO"), 0) * 100) / (100 + par.IfNull(myReader1("IVA_CANONE"), 0)))
                    Imp_ValoreCANONE1 = par.IfNull(myReader1("VALORE_CANONE"), 0) - Imp_CanoneLordo1

                    '   lotto 2
                    ' NOTA: si presuppone che la % di VALORE_CANONE del lotto2 di PF_VOCI_IMPORTO sia uguale al lotto1
                    Imp_ValoreCANONE2 = par.IfNull(myReader2("VALORE_CANONE"), 0) + Imp_CanoneLordo1


                    'PF_VOCI_IMPORTO di LOTTO1
                    par.cmd.CommandText = " update SISCOM_MI.PF_VOCI_IMPORTO set " _
                                                & " VALORE_CANONE=" & par.VirgoleInPunti(Format(Imp_ValoreCANONE1, "0.00")) & "," _
                                                & " VALORE_CONSUMO=" & par.VirgoleInPunti(Format(Imp_ValoreCONSUMO1, "0.00")) _
                                        & " where ID=" & par.IfNull(myReader1("ID"), -1)

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    '********************************

                    'PF_VOCI_IMPORTO di LOTTO2
                    par.cmd.CommandText = " update SISCOM_MI.PF_VOCI_IMPORTO set " _
                                                & " VALORE_CANONE=" & par.VirgoleInPunti(Format(Imp_ValoreCANONE2, "0.00")) & "," _
                                                & " VALORE_CONSUMO=" & par.VirgoleInPunti(Format(Imp_ValoreCONSUMO2, "0.00")) _
                                        & " where ID=" & par.IfNull(myReader2("ID"), -1)

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    '********************************

                    par.cmd.CommandText = " update SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI set " _
                                                & " ID_LOTTO=" & ID_LOTTO2 & "," _
                                                & " ID_VOCE_IMPORTO=" & par.IfNull(myReader2("ID"), -1) _
                                        & " where ID_LOTTO=" & ID_LOTTO1 _
                                        & "   and ID_COMPLESSO=" & ID_COMPLESSO _
                                        & "   and ID_VOCE_IMPORTO=" & par.IfNull(myReader1("ID_VOCE_IMPORTO"), -1)


                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    '********************************

                End If
                myReader2.Close()

            End If
            myReader1.Close()


        Catch ex As Exception

            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            EffettuaScambio = False
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")


        End Try


    End Function

    Sub CalcolaImporti(ByVal campo As Integer)

        Dim Prezzo1, Prezzo2, Risultato1 As Decimal

        If campo = 1 Then

            'CONSUMO1
            Prezzo1 = par.IfEmpty(Me.txtConsumo1.Text, 0)
            Prezzo2 = par.IfEmpty(Me.txtConsumo1EXP.Text, 0)

            Risultato1 = Prezzo1 - Prezzo2

            Me.txtConsumo2IMP.Text = IsNumFormat(Prezzo2, "", "##,##0.00")
            Me.txtConsumo1EXP_TMP.Value = IsNumFormat(Prezzo2, "", "##,##0.00")

            Me.txtConsumo1_FINALE.Text = IsNumFormat(Risultato1, "", "##,##0.00")
            Me.txtConsumo1_TMP.Value = IsNumFormat(Risultato1, "", "##,##0.00")

            'CONSUMO2
            Prezzo1 = Me.txtConsumo2.Text
            Prezzo2 = Me.txtConsumo2IMP.Text

            Risultato1 = Prezzo1 + Prezzo2

            Me.txtConsumo2_FINALE.Text = IsNumFormat(Risultato1, "", "##,##0.00")
            Me.txtConsumo2_TMP.Value = IsNumFormat(Risultato1, "", "##,##0.00")

        Else
            'CANONE 1
            Prezzo1 = par.IfEmpty(Me.txtCanone1.Text, 0)
            Prezzo2 = par.IfEmpty(Me.txtCanone1EXP.Text, 0)

            Risultato1 = Prezzo1 - Prezzo2

            Me.txtCanone2IMP.Text = IsNumFormat(Prezzo2, "", "##,##0.00")
            Me.txtCanone1EXP_TMP.Value = IsNumFormat(Prezzo2, "", "##,##0.00")

            Me.txtCanone1_FINALE.Text = IsNumFormat(Risultato1, "", "##,##0.00")
            Me.txtCanone1_TMP.Value = IsNumFormat(Risultato1, "", "##,##0.00")

            'CANONE 2
            Prezzo1 = par.IfEmpty(Me.txtCanone2.Text, 0)
            Prezzo2 = par.IfEmpty(Me.txtCanone2IMP.Text, 0)

            Risultato1 = Prezzo1 + Prezzo2

            Me.txtCanone2_FINALE.Text = IsNumFormat(Risultato1, "", "##,##0.00")
            Me.txtCanone2_TMP.Value = IsNumFormat(Risultato1, "", "##,##0.00")
        End If

    End Sub

End Class
