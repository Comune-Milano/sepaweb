' TAB RIEPILOGO GENERALE DELLA MANUTENZIONE

Partial Class Tab_Manu_Riepilogo
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then


            ' CONNESSIONE DB
            IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text
            Me.txtIdConnessione.Text = IdConnessione

            'If PAR.OracleConn.State = Data.ConnectionState.Open Then
            '    Response.Write("IMPOSSIBILE VISUALIZZARE")
            '    Exit Sub
            'Else
            '    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '    par.SettaCommand(par)
            'End If
            ''''''''''''''''''''''''''


            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If
    End Sub


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

    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
            txtOneriC.ReadOnly = True
            txtOneriC.Enabled = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub




    Protected Sub btnINFO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINFO.Click

        If Me.HLink_Appalto.Text <> "" Then
            Response.Write("<script>window.open('../APPALTI/Appalti.aspx?A=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value & "&IDL=-1','Appalto','height=550,width=800');</script>")
        End If
        'CType(Tab_Manu_Riepilogo.FindControl("txtAppalto"), Label).Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../APPALTI/Appalti.aspx?A=" & par.IfNull(myReader1("ID_APPALTO"), -1) & "&IDL=" & Id_Lotto & " ','Appalto','height=550,width=800');" & Chr(34) & ">" & Strings.Left(par.PulisciStrSql(par.IfNull(myReader1("APPALTO"), "")), 20) & "..." & "</a>"

    End Sub

    Protected Sub DataGridAppalti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAppalti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Manu_Riepilogo_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Manu_Riepilogo_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Manu_Riepilogo_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Manu_Riepilogo_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Manu_Dettagli_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub



    Protected Sub btn_Inserisci1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci1.Click
        Dim FlagConnessione As Boolean
        'Dim sStr1 As String

        Try

            'If Me.txtIdComponente.Text <> "-1" Then

            '    FlagConnessione = False
            '    If PAR.OracleConn.State = Data.ConnectionState.Closed Then
            '        PAR.OracleConn.Open()
            '        par.SettaCommand(par)

            '        FlagConnessione = True
            '    End If


            '    sStr1 = "select  SISCOM_MI.LOTTI.ID,SISCOM_MI.LOTTI.DESCRIZIONE " _
            '         & " from SISCOM_MI.LOTTI " _
            '         & " where ID=(select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '                       & " where ID_SERVIZIO=" & CType(Me.Page.FindControl("cmbServizio"), DropDownList).SelectedValue _
            '                       & " and   ID_PF_VOCE_IMPORTO=" & CType(Me.Page.FindControl("cmbServizioVoce"), DropDownList).SelectedValue _
            '                       & " and   ID_APPALTO=" & Me.txtIdComponente.Text & ")"


            '    PAR.cmd.CommandText = sStr1
            '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

            '    If myReader1.Read Then
            '        CType(Me.Page.FindControl("txtIdLotto"), HiddenField).Value = PAR.IfNull(myReader1("ID"), -1)
            '        Me.txtLotto.Text = PAR.IfNull(myReader1("DESCRIZIONE"), "")
            '    End If
            '    myReader1.Close()


            '    'FORNITORE 
            '    sStr1 = "select  SISCOM_MI.APPALTI.ID as ""ID_APPALTO"",SISCOM_MI.APPALTI.DESCRIZIONE as ""APPALTO"",SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.PERC_ONERI_SIC_CON,SISCOM_MI.APPALTI.FL_RIT_LEGGE, " _
            '                 & "  SISCOM_MI.FORNITORI.ID as ""ID_FORNITORE"",SISCOM_MI.FORNITORI.RAGIONE_SOCIALE,SISCOM_MI.FORNITORI.COGNOME,SISCOM_MI.FORNITORI.NOME,SISCOM_MI.FORNITORI.TIPO,SISCOM_MI.FORNITORI.COD_FISCALE,SISCOM_MI.FORNITORI.PARTITA_IVA " _
            '         & " from SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
            '         & " where SISCOM_MI.APPALTI.ID=" & Me.txtIdComponente.Text _
            '         & "  and  SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)"

            '    PAR.cmd.CommandText = sStr1
            '    myReader1 = PAR.cmd.ExecuteReader()

            '    If myReader1.Read Then
            '        If PAR.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
            '            Me.HLink_Fornitore.Text = PAR.IfNull(myReader1("COGNOME"), "") & " " & PAR.IfNull(myReader1("NOME"), "")
            '        Else
            '            Me.HLink_Fornitore.Text = PAR.IfNull(myReader1("RAGIONE_SOCIALE"), "")
            '        End If

            '        If PAR.IfNull(myReader1("TIPO"), "") = "F" Then

            '            ' CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Attributes.Add("onClick", "javascript:window.open('../APPALTI/FornitoreF.aspx?ID=" & par.IfNull(myReader1("ID_FORNITORE"), "") & "&F=1&G=0" & "&CF=" & par.IfNull(myReader1("COD_FISCALE"), "") & "&CO=" & par.IfNull(myReader1("COGNOME"), "") & "&NO=" & par.IfNull(myReader1("NOME"), "") & "','Stato','height=370,top=0,left=0,width=480,scrollbars=no');")
            '            Me.HLink_Fornitore.Attributes.Add("onClick", "javascript:window.open('../APPALTI/FornitoreF.aspx?ID=" & PAR.IfNull(myReader1("ID_FORNITORE"), "") & "','Fornitore','height=550,width=800');")

            '            'CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).NavigateUrl = "../APPALTI/FornitoreF.aspx?ID=" & par.IfNull(myReader1("ID_FORNITORE"), "") '& "&F=1&G=0" & "&CF=" & par.IfNull(myReader1("COD_FISCALE"), "") & "&CO=" & par.IfNull(myReader1("COGNOME"), "") & "&NO=" & par.IfNull(myReader1("NOME"), "") & "','Stato','height=800,top=0,left=0,width=500,scrollbars=no');"
            '            'Response.Write("<script>location.replace('RisultatiFornitoriF.aspx?F=" & f & "&G=" & g & "&CF=" & par.PulisciStrSql(par.IfEmpty(Me.txtCF.Text, "")) & "&CO=" & par.VaroleDaPassare(par.IfEmpty(Me.txtCognome.Text, "")) & "&NO=" & par.VaroleDaPassare(par.IfEmpty(Me.txtNome.Text, "")) & "');</script>")
            '        Else
            '            'CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Attributes.Add("onClick", "javascript:window.open('../APPALTI/FornitoriG.aspx?ID=" & par.IfNull(myReader1("ID_FORNITORE"), "") & "&F=0&G=1" & "&PI=" & par.IfNull(myReader1("PARTITA_IVA"), "") & "&RA=" & par.IfNull(myReader1("RAGIONE_SOCIALE"), "") & "','Stato','height=370,top=0,left=0,width=480,scrollbars=no');")
            '            Me.HLink_Fornitore.Attributes.Add("onClick", "javascript:window.open('../APPALTI/FornitoreG.aspx?ID=" & PAR.IfNull(myReader1("ID_FORNITORE"), "") & "','Fornitore','height=550,width=800');")

            '            'CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).NavigateUrl = "../APPALTI/FornitoriG.aspx?ID=" & par.IfNull(myReader1("ID_FORNITORE"), "") ' & "&F=0&G=1" & "&PI=" & par.IfNull(myReader1("PARTITA_IVA"), "") & "&RA=" & par.IfNull(myReader1("RAGIONE_SOCIALE"), "") & "','Stato','height=370,top=0,left=0,width=480,scrollbars=no');")
            '            ' Response.Write("<script>location.replace('RisultatiFornitoriG.aspx?F=" & f & "&G=" & g & "&PI=" & par.PulisciStrSql(par.IfEmpty(Me.txtPIva.Text, "")) & "&RA=" & par.VaroleDaPassare(par.IfEmpty(Me.txtRagione.Text, "")) & "');</script>")
            '        End If

            '        Me.HLink_Appalto.Text = PAR.IfNull(myReader1("NUM_REPERTORIO"), "")
            '        Me.HLink_Appalto.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Appalti.aspx?ID=" & PAR.IfNull(myReader1("ID_APPALTO"), "") & "&A=" & PAR.IfNull(myReader1("ID_APPALTO"), "") & "&IDL=-1','Appalto','height=550,width=800');")

            '        CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value = PAR.IfNull(myReader1("PERC_ONERI_SIC_CON"), 0)

            '        CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value = PAR.IfNull(myReader1("ID_APPALTO"), "")
            '        CType(Me.Page.FindControl("txtID_Fornitore"), HiddenField).Value = PAR.IfNull(myReader1("ID_FORNITORE"), -1)

            '        CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value = PAR.IfNull(myReader1("FL_RIT_LEGGE"), 0)

            '    End If
            '    myReader1.Close()



            '    'INFO IMPORTI
            '    'CREATE TABLE APPALTI_LOTTI_SERVIZI
            '    '(
            '    '  ID_APPALTO        NUMBER                      NOT NULL,
            '    '  ID_LOTTO          NUMBER                      NOT NULL,
            '    '  ID_SERVIZIO       NUMBER                      NOT NULL,
            '    '  ID_PF_VOCE_IMPORTO  NUMBER,

            '    '  IMPORTO_CORPO     NUMBER,
            '    '  SCONTO_CORPO      NUMBER(3),
            '    '  IVA_CORPO         NUMBER(2),
            '    '  IMPORTO_CONSUMO   NUMBER,
            '    '  SCONTO_CONSUMO    NUMBER(3),
            '    '  IVA_CONSUMO       NUMBER(2),
            '    '  RESIDUO_CONSUMO(NUMBER)


            '    Setta_ImportoResiduo()

            '    FlagConnessione = False
            '    If PAR.OracleConn.State = Data.ConnectionState.Closed Then
            '        PAR.OracleConn.Open()
            '        par.SettaCommand(par)

            '        FlagConnessione = True
            '    End If

            '    'ESTRAGGO per ID_PF_VOCE_IMPORTO lo sconto e iva al consumo
            '    sStr1 = "select * from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '         & " where ID_LOTTO=" & CType(Me.Page.FindControl("txtIdLotto"), HiddenField).Value _
            '           & " and ID_SERVIZIO=" & CType(Me.Page.FindControl("cmbServizio"), DropDownList).SelectedValue _
            '           & " and ID_APPALTO=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value _
            '           & " and ID_PF_VOCE_IMPORTO=" & CType(Me.Page.FindControl("cmbServizioVoce"), DropDownList).SelectedValue

            '    PAR.cmd.CommandText = sStr1
            '    myReader1 = PAR.cmd.ExecuteReader()

            '    If myReader1.Read Then
            '        CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value = PAR.IfNull(myReader1("SCONTO_CONSUMO"), 0)
            '        CType(Me.Page.FindControl("txtPercIVA"), HiddenField).Value = PAR.IfNull(myReader1("IVA_CONSUMO"), 0)

            '        'If txtSTATO.Value = 0 Then AbilitaDisabilita()
            '        'txtIdScala.Text = Me.cmbScala.SelectedValue
            '    Else
            '        'If txtSTATO.Value = 0 Then AbilitaDisabilita()
            '    End If
            '    myReader1.Close()




            '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            '    txtSel1.Text = ""
            '    txtIdComponente.Text = ""

            'End If

        Catch ex As Exception

            If FlagConnessione = True Then PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub Setta_ImportoResiduo()
        'Dim FlagConnessione As Boolean
        'Dim sStr1 As String

        'Dim SommaResiduo As Decimal = 0
        'Dim ris1, ris2 As Decimal

        'Try

        '    FlagConnessione = False
        '    If par.OracleConn.State = Data.ConnectionState.Closed Then
        '        par.OracleConn.Open()
        '        par.SettaCommand(par)

        '        FlagConnessione = True
        '    End If

        '    'CALCOLO l'IMPORTO RESIDUO dato da:

        '    '1) la somma di eventuali variazioni all'importo residuo di APPALTI_VARIAZIONI_IMPORTI
        '    sStr1 = "select SUM(IMPORTO) " _
        '         & " from   SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
        '         & " where  ID_LOTTO=" & CType(Me.Page.FindControl("txtIdLotto"), HiddenField).Value _
        '           & " and  ID_SERVIZIO=" & CType(Me.Page.FindControl("cmbServizio"), DropDownList).SelectedValue _
        '           & " and  ID_PF_VOCE_IMPORTO=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value _
        '           & " and  ID_VARIAZIONE in (select ID from SISCOM_MI.APPALTI_VARIAZIONI " _
        '                                 & " where ID_APPALTO=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value & ")"

        '    par.cmd.CommandText = sStr1
        '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        '    If myReader1.Read Then
        '        SommaResiduo = par.IfNull(myReader1(0), 0)
        '    End If
        '    myReader1.Close()


        '    '2)la somma dell'importo calcolato (IMPORTO-CONSUMO+IVA) di ID_SERVIZIO per tutte le ID_PF_VOCE_IMPORTO
        '    sStr1 = "select * from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
        '         & " where ID_LOTTO=" & CType(Me.Page.FindControl("txtIdLotto"), HiddenField).Value _
        '         & "   and ID_SERVIZIO=" & CType(Me.Page.FindControl("cmbServizio"), DropDownList).SelectedValue _
        '         & "   and ID_APPALTO=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value

        '    par.cmd.CommandText = sStr1
        '    myReader1 = par.cmd.ExecuteReader()

        '    While myReader1.Read
        '        'ris1 = PAR.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - PAR.IfNull(myReader1("SCONTO_CONSUMO"), 0)
        '        ris1 = PAR.IfNull(myReader1("IMPORTO_CONSUMO"), 0) * PAR.IfNull(myReader1("SCONTO_CONSUMO"), 0) / 100
        '        ris1 = PAR.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - ris1

        '        If par.IfNull(myReader1("IVA_CONSUMO"), 0) > 0 Then
        '            ris2 = ris1 * par.IfNull(myReader1("IVA_CONSUMO"), 0) / 100
        '        Else
        '            ris2 = 0
        '        End If
        '        SommaResiduo = SommaResiduo + ris1 + ris2
        '    End While
        '    myReader1.Close()


        '    '3)la SommaResiduo va sottratto alla somma dell'IMPORTO PRENOTATO o CONSUNTIVATO da MANUTENZIONI 
        '    sStr1 = "select SUM(IMPORTO_TOT) from SISCOM_MI.MANUTENZIONI " _
        '         & " where ID_APPALTO=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value _
        '         & "   and ID_SERVIZIO=" & CType(Me.Page.FindControl("cmbServizio"), DropDownList).SelectedValue _
        '         & "   and (STATO=1 or STATO=2)"

        '    par.cmd.CommandText = sStr1
        '    myReader1 = par.cmd.ExecuteReader()

        '    If myReader1.Read Then
        '        SommaResiduo = SommaResiduo - par.IfNull(myReader1(0), 0)
        '    End If
        '    myReader1.Close()

        '    Me.txtImportoTotale.Text = IsNumFormat(SommaResiduo, "", "##,##0.00") ' IsNumFormat(myReader1("RESIDUO_CONSUMO"), "", "##,##0.00")

        '    CType(Me.Page.FindControl("Tab_Manu_Dettagli").FindControl("txtResiduoConsumo"), HiddenField).Value = IsNumFormat(SommaResiduo, "", "##,##0.00") ' IsNumFormat(myReader1("RESIDUO_CONSUMO"), "", "##,##0.00")

        'Catch ex As Exception
        '    If FlagConnessione = True Then PAR.OracleConn.Close()

        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        'End Try

    End Sub




End Class
