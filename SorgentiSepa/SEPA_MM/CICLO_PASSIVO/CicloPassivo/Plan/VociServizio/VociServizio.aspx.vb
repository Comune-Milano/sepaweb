Imports System.Collections

Partial Class CicloPassivo_CicloPassivo_APPALTI_VociServizio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Response.Expires = 0

            If Not IsPostBack Then



                'vedo da dove ricevo id servizio
                idservizio = 0
                If UCase(Request.QueryString("IDS")) <> "" Then
                    idservizio = UCase(Request.QueryString("IDS")) 'altrimenti dal form selezione dei servizi
                End If



                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                Me.txtConnessione.Text = CStr(lIdConnessione)
                Me.txtidservizio.Text = "-1"
                'Me.txtidservizio.Text = idservizio

                idPiano = RicavaPianoF()

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                End If

                SOLO_LETTURA.Value = "0"


                'ricavo il servizio
                If UCase(Request.QueryString("SE")) <> "" Then
                    txtservizio.Text = UCase(Request.QueryString("SE")) 'passaggio da form selezione servizio
                End If


                'MODIFICA IVA
                Dim queryIVA As String = "SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC"
                par.caricaComboBox(queryIVA, cmbivacorpo, "VALORE", "VALORE", True, "-1", " ")
                par.caricaComboBox(queryIVA, cmbivaconsumo, "VALORE", "VALORE", True, "-1", " ")


                BindGrid_voci()

                'modifica marco 18/12/2102
                '********************************************************************************************************
                par.cmd.CommandText = "select * from siscom_mi.pf_categorie "
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                DropDownListCategorie.Items.Clear()
                While lettore.Read
                    DropDownListCategorie.Items.Add(New ListItem(par.IfNull(lettore(1), "") & " - " & par.IfNull(lettore(2), ""), par.IfNull(lettore(0), 22)))
                End While
                lettore.Close()
                DropDownListCategorie.ClearSelection()
                '********************************************************************************************************


                par.cmd.CommandText = "select * from operatori where id=" & Session.Item("ID_OPERATORE")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    If par.IfNull(myReader1("BP_VOCI_SERVIZI_L"), "1") = "1" Then
                        'SOLO LETTURA GENERALE, DEVONO SPARIRE I PULSANTI DI AGGIUNGI, MODIFICA E ELIMINA
                        'ORDINAMENTO CORRETTO
                        btnAgg1.Visible = False
                        btnApri1.Visible = False
                        btnElimina1.Visible = False
                    End If
                End If
                myReader1.Close()

                par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_piano_finanziario=" & idPiano & " AND (CODICE='2.02.01' OR CODICE='2.02.02' OR CODICE='2.02.03' OR CODICE='2.04.01' OR CODICE='2.04.02' OR CODICE='2.04.03' OR CODICE='2.04.04') order by codice asc"
                myReader1 = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    cmbVoci.Items.Add(New ListItem(par.IfNull(myReader1("codice"), "") & "-" & par.IfNull(myReader1("descrizione"), ""), par.IfNull(myReader1("id"), "")))
                Loop
                myReader1.Close()
                cmbVoci.Items.Add(New ListItem("-", "-1"))
                Me.cmbVoci.ClearSelection()
                cmbVoci.Items.FindByText("-").Selected = True

            End If


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next



        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")
        End Try

    End Sub


    Private Function RicavaPianoF() As Long
        Try

            RicavaPianoF = 0
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim idStato As Integer = -1
            Dim myReaderW As Oracle.DataAccess.Client.OracleDataReader

            If Request.QueryString("IDES") <> "" Then
                RicavaPianoF = Request.QueryString("IDES")
                par.cmd.CommandText = "select * from siscom_mi.pf_main where ID_ESERCIZIO_FINANZIARIO = " & RicavaPianoF
                myReaderW = par.cmd.ExecuteReader()
                If myReaderW.Read Then
                    RicavaPianoF = par.IfNull(myReaderW("ID"), 0)
                    idStato = par.IfNull(myReaderW("id_stato"), -1)
                End If
                myReaderW.Close()

            Else
                par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN where id_stato=1 order by ID desc"
                myReaderW = par.cmd.ExecuteReader()
                If myReaderW.Read Then
                    RicavaPianoF = par.IfNull(myReaderW("ID"), 0)
                    idStato = par.IfNull(myReaderW("id_stato"), -1)
                End If
                myReaderW.Close()


            End If

            If idStato <> 1 Then
                Me.btnAgg1.Visible = False
                Me.btnApri1.Visible = False
                Me.btnElimina1.Visible = False

            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()

        End Try
    End Function

    Public Property IdConnessione() As String
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

    Public Property idservizio() As Long
        Get
            If Not (ViewState("par_idservizio") Is Nothing) Then
                Return CLng(ViewState("par_idservizio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idservizio") = value
        End Set

    End Property

    Public Property idPiano() As Long
        Get
            If Not (ViewState("par_idPiano") Is Nothing) Then
                Return CLng(ViewState("par_idPiano"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idPiano") = value
        End Set

    End Property

    'INTERVENTI GRID1
    Private Sub BindGrid_voci()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '‘‘par.cmd.Transaction = par.myTrans

        '(CASE (TAB_SERVIZI_VOCI.QUOTA_PREVENTIVA) WHEN 0 THEN 'NO' ELSE 'SI' END) AS ""QUOTA_PREVENTIVA""
        StringaSql = " select pf_voci.codice,SISCOM_MI.TAB_SERVIZI_VOCI.ID,SISCOM_MI.TAB_SERVIZI_VOCI.DESCRIZIONE," _
            & " SISCOM_MI.TAB_SERVIZI_VOCI.PERC_REVERSIBILITA,SISCOM_MI.TAB_SERVIZI_VOCI.IVA_CANONE,SISCOM_MI.TAB_SERVIZI_VOCI.IVA_CONSUMO," _
            & " DECODE(TAB_SERVIZI_VOCI.QUOTA_PREVENTIVA,0,'NO',1,'SI',Null,'') AS ""QUOTA_PREVENTIVA""," _
            & " SISCOM_MI.TAB_SERVIZI_VOCI.NO_MOD,PF_CATEGORIE.GRUPPO||'-'||PF_CATEGORIE.DESCRIZIONE AS CATEGORIA" _
            & " from siscom_mi.pf_voci,SISCOM_MI.TAB_SERVIZI_VOCI,SISCOM_MI.PF_cATEGORIE " _
            & " where TAB_SERVIZI_VOCI.ID_voce=pf_voci.id (+) " _
            & " and SISCOM_MI.TAB_SERVIZI_VOCI.ID_SERVIZIO=" & idservizio _
            & " and PF_CATEGORIE.ID=TAB_SERVIZI_VOCI.ID_CATEGORIA " _
            & " and pf_voci.id_piano_finanziario=" & idPiano _
            & " order by SISCOM_MI.TAB_SERVIZI_VOCI.DESCRIZIONE asc"

        par.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "TAB_SERVIZI_VOCI")


        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        ds.Dispose()





    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtSel1').value='Hai selezionato: " & Replace(Mid(e.Item.Cells(1).Text, 1, 80), "'", "\'") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtnomod').value='" & e.Item.Cells(8).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtSel1').value='Hai selezionato: " & Replace(Mid(e.Item.Cells(1).Text, 1, 80), "'", "\'") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtnomod').value='" & e.Item.Cells(8).Text & "';")

        End If
    End Sub


    Protected Sub btn_InserisciVoce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciVoce.Click
        If ControlloCampi() = False Then
            txtAppare2.Value = "1"
            Exit Sub
        End If

        If Me.txtIDV.Value = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaVoce()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateVoce()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSel1.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Function ControlloCampi() As Boolean

        ControlloCampi = True


        If par.IfEmpty(Me.txtdescrizione.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire descrizione voce servizio!');</script>")
            ControlloCampi = False
            txtdescrizione.Focus()
            Exit Function
        End If

        If par.IfEmpty(Me.txtreversibilita.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la percentuale di reversibilità!');</script>")
            ControlloCampi = False
            txtdescrizione.Focus()
            Exit Function
        End If

        If cmbVoci.SelectedItem.Value = "-1" Then
            Response.Write("<script>alert('Inserire voce associata!');</script>")
            ControlloCampi = False
            txtdescrizione.Focus()
            Me.cmbVoci.ClearSelection()
            cmbVoci.Items.FindByText("-").Selected = True
            Exit Function
        End If

    End Function


    Private Sub SalvaVoce()

        Try

            '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)


            par.cmd.CommandText = "SELECT SISCOM_MI.TAB_SERVIZI_VOCI.DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE SISCOM_MI.TAB_SERVIZI_VOCI.DESCRIZIONE LIKE '" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 2000)) & "' AND SISCOM_MI.TAB_SERVIZI_VOCI.ID_SERVIZIO=" & idservizio
            Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myRec.Read Then
                Response.Write("<script>alert('Attenzione...Voce già presente per quel servizio.');</script>")
                myRec.Close()
                Exit Sub
            End If
            myRec.Close()

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            par.cmd.Parameters.Clear()



            ' '' Ricavo vIdFornitori
            par.cmd.CommandText = " select SISCOM_MI.SEQ_TAB_SERVIZI_VOCI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                txtIDV.Value = myReader1(0)
                'Session.Item("ID") = vIdFornitori
                'Session.Add("IDF", vIdFornitori)
            End If

            par.cmd.Parameters.Clear()

            par.cmd.CommandText = "insert into SISCOM_MI.TAB_SERVIZI_VOCI " _
                                        & " (ID, ID_SERVIZIO, DESCRIZIONE, PERC_REVERSIBILITA, IVA_CANONE, IVA_CONSUMO, QUOTA_PREVENTIVA,id_voce,id_categoria) " _
                                & "values (:id,:id_servizio,:descrizione,:perc_reversibilita,:iva_CANONE,:iva_consumo,:quota_preventiva,:id_voce,:id_categoria) "

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", strToNumber(Me.txtIDV.Value)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_servizio", idservizio))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStringaInvio(Me.txtdescrizione.Text, 2000)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perc_reversibilita", strToNumber(Me.txtreversibilita.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_CANONE", Me.cmbivacorpo.SelectedValue))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumo", Me.cmbivaconsumo.SelectedValue))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quota_preventiva", Me.cmbquota.SelectedValue))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", cmbVoci.SelectedItem.Value))

            'modifica marco 18/12/2102'
            '********************************************************************************************************************
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_categoria", DropDownListCategorie.SelectedValue))
            '********************************************************************************************************************

            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()

            ' COMMIT
            par.myTrans.Commit()

            Response.Write("<SCRIPT>alert('Voce inserita correttamente!');</SCRIPT>")

            BindGrid_voci()


            '*** EVENTI_MANUTENZIONE
            'par.InserisciEventoManu(par.cmd, idservizio, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLI_MANUTENZIONE, "Consuntivo")


            ''* BLOCCO LA SCHEDA (STESSA COSA CHE ACCADE IN APRI RICERCA)
            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID = " & txtIDV.Text & " FOR UPDATE NOWAIT"
            'myReader1 = par.cmd.ExecuteReader()
            'myReader1.Close()

            'par.myTrans = par.OracleConn.BeginTransaction()
            'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            'Response.Write("<SCRIPT>alert('Voce servizio inserita correttamente!');</SCRIPT>")


            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"




        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If idservizio <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub UpdateVoce()

        Try

            If idservizio <> -1 Then

                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA MANUTENZIONI

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                'par.cmd.CommandText = "update  SISCOM_MI.TAB_SERVIZI_VOCI  set " _
                '                            & "ID_SERVIZIO=:id_servizio,DESCRIZIONE=:descrizione," _
                '                            & "PERC_REVERSIBILITA=:perc_reversibilita," _
                '                            & "IVA_CORPO=:iva_corpo,IVA_CONSUMO=:iva_consumo,QUOTA_PREVENTIVA=:quota_preventiva " _
                '                    & " where ID=" & Me.txtIDV.Text


                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_servizio", idservizio))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStringaInvio(Me.txtdescrizione.Text, 2000)))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perc_reversibilita", strToNumber(Me.txtreversibilita.Text)))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_corpo", Me.cmbivacorpo.SelectedValue))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumo", Me.cmbivaconsumo.SelectedValue))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quota_preventiva", Me.cmbquota.SelectedValue))

                par.cmd.CommandText = "UPDATE SISCOM_MI.TAB_SERVIZI_VOCI SET " _
                    & "ID_SERVIZIO=" & idservizio & "," _
                    & "DESCRIZIONE='" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 2000)) & "'," _
                    & "PERC_REVERSIBILITA=" & par.IfEmpty(Me.txtreversibilita.Text, "Null") & "," _
                    & "IVA_CANONE=" & Me.cmbivacorpo.SelectedValue & "," _
                    & "IVA_CONSUMO=" & Me.cmbivaconsumo.SelectedValue & "," _
                    & "QUOTA_PREVENTIVA=" & Me.cmbquota.SelectedValue & ", " _
                    & "id_categoria=" & Me.DropDownListCategorie.SelectedValue & ", " _
                    & "ID_VOCE=" & cmbVoci.SelectedItem.Value _
                    & " WHERE ID=" & Me.txtIDV.Value
                'modifica marco 18/12/2012 aggiunta la categoria


                par.cmd.ExecuteNonQuery()



                par.myTrans.Commit() 'COMMIT


                'par.myTrans = par.OracleConn.BeginTransaction()
                'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Response.Write("<SCRIPT>alert('Voce aggiornata correttamente!');</SCRIPT>")
                BindGrid_voci()
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtModificato.Text = "0"

                '*** EVENTI_MANUTENZIONE
                'par.InserisciEventoManu(par.cmd, idservizio, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLI_MANUTENZIONE, "Consuntivo")


            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If txtIDV.Value <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnAgg1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgg1.Click
        Try

            Me.txtIDV.Value = -1

            Me.txtdescrizione.Text = ""
            Me.txtreversibilita.Text = ""
            Me.cmbivacorpo.ClearSelection()
            Me.cmbivaconsumo.ClearSelection()
            Me.cmbquota.ClearSelection()
            Me.DropDownListCategorie.ClearSelection()
            cmbquota.Items.FindByValue("0").Selected = True
            Me.cmbVoci.ClearSelection()
            cmbVoci.Items.FindByValue("-1").Selected = True
            'cmbVoci.ClearSelection()
            'cmbVoci.Items.FindByValue("1").Selected = True


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btn_ChiudiVoce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiVoce.Click

        'riabilito i campi
        Me.txtdescrizione.Enabled = True
        Me.txtreversibilita.Enabled = True
        Me.cmbivacorpo.Enabled = True
        Me.cmbivaconsumo.Enabled = True
        Me.cmbquota.Enabled = True
        Me.btn_InserisciVoce.Visible = True
        cmbVoci.Enabled = True

        If Me.txtIDV.Value <> "-1" Then


            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.myTrans.Rollback()


        End If


        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSel1.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Protected Sub btnApri1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApri1.Click
        Try

            If txtIdComponente.Text = "" Then
                'Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare2.Value = "0"
            Else

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans
                    HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                par.cmd.CommandText = "select * from SISCOM_MI.TAB_SERVIZI_VOCI where ID=" & Me.txtIdComponente.Text & " FOR UPDATE NOWAIT"

                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                If txtnomod.Value = "1" Then 'se inserito a mano sul db non posso modificare 
                    txtdescrizione.Enabled = False
                    txtreversibilita.Enabled = False
                End If



                Session.Add("LAVORAZIONE", "1")

            End If



        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                Dim scriptblock As String
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Voce servizio aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID = " & txtIdComponente.Text
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiCampi(myReader1)
                End While
                myReader1.Close()


                SOLO_LETTURA.Value = "1"
                FrmSolaLettura()

            Else
                par.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")
            End If



        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If txtIDV.Value <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)

        Me.txtIDV.Value = par.IfNull(myReader1("ID"), -1)
        Me.txtdescrizione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        Me.txtreversibilita.Text = Replace(par.IfNull(myReader1("PERC_REVERSIBILITA"), ""), ",", ".")
        Me.cmbivacorpo.SelectedValue = par.IfNull(myReader1("IVA_CANONE"), 0)
        Me.cmbivaconsumo.SelectedValue = par.IfNull(myReader1("IVA_CONSUMO"), 0)
        Me.cmbquota.SelectedValue = par.IfNull(myReader1("QUOTA_PREVENTIVA"), 0)
        Me.DropDownListCategorie.SelectedValue = par.IfNull(myReader1("ID_CATEGORIA"), 22)
        If par.IfNull(myReader1("ID_VOCE"), "0") <> "0" Then
            cmbVoci.ClearSelection()
            cmbVoci.SelectedValue = par.IfNull(myReader1("ID_VOCE"), 0)
        End If

    End Sub


    Protected Sub btnElimina1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina1.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    ' Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppare2.Value = "0"

                Else
                    If idservizio <> -1 Then
                        If par.OracleConn.State = Data.ConnectionState.Open Then
                            Response.Write("IMPOSSIBILE VISUALIZZARE")
                            Exit Sub
                        Else

                            ' RIPRENDO LA CONNESSIONE
                            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)

                            'vedo se è bloccata da un altro utente
                            par.cmd.CommandText = "select * from SISCOM_MI.TAB_SERVIZI_VOCI where ID=" & Me.txtIdComponente.Text & " FOR UPDATE NOWAIT"
                            Dim myReaderblocco As Oracle.DataAccess.Client.OracleDataReader
                            myReaderblocco = par.cmd.ExecuteReader()
                            myReaderblocco.Close()




                            par.cmd.CommandText = "SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & txtIdComponente.Text & "" _
                            & "UNION SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.MANUTENZIONI WHERE SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO =" & txtIdComponente.Text
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                Response.Write("<SCRIPT>alert('Impossibile eliminare. Voce di servizio legata ad un appalto o ad una manutenzione!');</SCRIPT>")
                                myReader1.Close()
                                Exit Sub
                            End If
                            myReader1.Close()

                            If txtnomod.Value = "0" Then 'se inserito a mano sul db non posso eliminare


                                'RIPRENDO LA TRANSAZIONE
                                par.myTrans = par.OracleConn.BeginTransaction()
                                ‘‘par.cmd.Transaction = par.myTrans
                                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                                par.cmd.CommandText = "delete from SISCOM_MI.TAB_SERVIZI_VOCI where ID = " & txtIdComponente.Text
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""

                                ' COMMIT
                                par.myTrans.Commit()

                                Response.Write("<script>alert('Voce servizio eliminata!')</script>")

                                BindGrid_voci()


                            Else
                                Response.Write("<script>alert('Impossibile eliminare questa voce servizio!')</script>")
                            End If

                            '*** EVENTI_MANUTENZIONE
                            'par.InserisciEventoManu(par.cmd, idservizio, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLI_MANUTENZIONE, "Consuntivo")


                        End If
                    End If
                    txtSel1.Text = ""
                    txtIdComponente.Text = ""

                End If
                CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            End If


        Catch EX1 As Oracle.DataAccess.Client.OracleException 'gestione blocco in eliminazione
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                Dim scriptblock As String
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Voce servizio aperta da un altro utente. Eliminazione non possibile in questo momento!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                txtSel1.Text = ""
                txtIdComponente.Text = ""

                Exit Sub

            Else
                If EX1.Number = 2292 Then
                    par.myTrans.Rollback()
                    par.OracleConn.Close()
                    Dim scriptblock As String
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Voce utilizzata. Eliminazione non possibile in questo momento!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                    End If

                    txtSel1.Text = ""
                    txtIdComponente.Text = ""

                    Exit Sub
                Else
                    par.OracleConn.Close()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                    Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")
                End If
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If idservizio <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")

        End Try

    End Sub


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
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



    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click

        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            ' par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            'par.myTrans.Rollback()
            par.OracleConn.Close()



            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")

            Response.Write("<script>document.location.href=""../../../pagina_home.aspx""</script>")

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


    Private Sub FrmSolaLettura()
        Try

            Me.btn_InserisciVoce.Visible = False
            'Me.btnElimina1.Visible = False
            'Me.btnApri1.Visible = False


            Me.txtdescrizione.Enabled = False
            Me.txtreversibilita.Enabled = False
            Me.cmbivacorpo.Enabled = False
            Me.cmbivaconsumo.Enabled = False
            Me.cmbquota.Enabled = False
            cmbVoci.Enabled = False

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
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

    Protected Sub btnIndietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            ' par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            'par.myTrans.Rollback()
            par.OracleConn.Close()



            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")

            Response.Write("<script>document.location.href=""SceltaServizio.aspx""</script>")

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub

    

End Class
