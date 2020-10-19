
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VociNPl
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim lstservizi As System.Collections.Generic.List(Of Mario.VociServizi)

    Protected Sub btn_InserisciAppalti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_InserisciAppalti.Click
        If ControlloCampiAppalti() = False Then
            txtAppareP.Value = "1"
            Exit Sub
        End If
        '*************PEPPE MODIFY 05/10/2010 ore 15:50
        If par.IfEmpty(Me.txtIDS.Value, "-1") = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaServizi()
            txtAppareP.Value = "1"
            '*******cancello i campi
            'Me.cmbservizio.SelectedValue = "-1"
            AggiornaVociServizi()
            'Me.cmbvoce.SelectedValue = "-1"
            Me.txtimportocorpo.Text = ""
            Me.txtimportoconsumo.Text = ""
            Me.txtscontoconsumo.Text = ""
            Me.txtscontocorpo.Text = ""
            Me.txtOnerconsumo.Text = ""
            Me.perconsumo.Value = ""

        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateAppalti()
            txtAppareP.Value = "0"
            Me.txtIDS.Value = ""
            '*******cancello i campi
            AggiornaVociServizi()

            Me.cmbvoce.SelectedValue = "-1"
            'Me.cmbvoce.SelectedValue = "-1"
            Me.txtimportocorpo.Text = ""
            Me.txtimportoconsumo.Text = ""
            Me.txtscontoconsumo.Text = ""
            Me.txtscontocorpo.Text = ""
            Me.txtOnerconsumo.Text = ""
            Me.perconsumo.Value = ""
            Me.txtpercanone.Text = ""
            Me.txtperconsumo.Text = ""

        End If
        If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
            FrmSolaLettura()
        End If

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        txtSelAppalti.Text = ""
        txtIdComponente.Value = ""
        txtIdComponente1.Value = ""

        CalcolaImpContrattuale()
        CalcolaResiduo()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)

    End Sub
    Function ControlloCampiAppalti() As Boolean

        ControlloCampiAppalti = True

        If cmbvoce.SelectedValue = "-1" Then
            Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            RadWindowManager1.RadAlert("Impossibile salvare se non si seleziona almeno una voce di servizio o non ci sono voci disponibili!", 300, 150, "Attenzione", "", "null")
            'Response.Write("<script>alert('Impossibile salvare se non si seleziona almeno una voce di servizio o non ci sono voci disponibili!');</script>")
            ControlloCampiAppalti = False
            Exit Function
        End If


    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lstservizi = CType(HttpContext.Current.Session.Item("LSTSERVIZI"), System.Collections.Generic.List(Of Mario.VociServizi))

        If Not IsPostBack Then

            Me.txtscontoconsumo.Attributes.Add("onBlur", "javascript:AutoDecimalPercentage(this);")
            Me.txtscontocorpo.Attributes.Add("onBlur", "javascript:AutoDecimalPercentage(this);")
            Me.txtivaconsumo.Attributes.Add("onBlur", "javascript:AutoDecimalPercentage2(this);")
            Me.txtivacorpo.Attributes.Add("onBlur", "javascript:AutoDecimalPercentage2(this);")

            Me.txtimportocorpo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Me.txtimportoconsumo.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(document.getElementById('Tab_VociNPl1_RadWindowServizi_C_txtOnerconsumo'),this.value,Tab_VociNPl1_RadWindowServizi_C_txtperconsumo);AutoDecimal(this,2);return false;")

            Me.txtscontoconsumo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Me.txtscontocorpo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

            '*************PEPPE MODIFY ON 30/04/2011***************
            'Me.txtOneriCan2.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtOnerconsumo.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this,document.getElementById('Tab_VociNPl1_RadWindowServizi_C_txtimportoconsumo').value,Tab_VociNPl1_RadWindowServizi_C_txtperconsumo);AutoDecimal(this,2);return false;")

            Me.txtivaconsumo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")



            lstservizi.Clear()

            DataGrid3.Rebind()
            If CType(Me.Page, Object).vIdAppalti > 0 Then
                CalcolaImpContrattuale()
            End If



        End If
        If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
            FrmSolaLettura()
        End If

    End Sub
    Private Sub FrmSolaLettura()
        Try

            'Me.btnAggAppalti.Visible = False

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
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmbvoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbvoce.SelectedIndexChanged
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            'par.cmd.CommandText = "SELECT PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE ,SISCOM_MI.PF_VOCI_IMPORTO.IVA_CANONE, SISCOM_MI.PF_VOCI_IMPORTO.IVA_CONSUMO FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_IMPORTO WHERE SISCOM_MI.PF_VOCI.ID = SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE AND SISCOM_MI.PF_VOCI_IMPORTO.ID=" & cmbvoce.SelectedValue
            par.cmd.CommandText = "SELECT PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE, SISCOM_MI.PF_VOCI_STRUTTURA.IVA FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PF_VOCI_STRUTTURA WHERE PF_VOCI.ID = PF_VOCI_STRUTTURA.ID_VOCE AND PF_VOCI_STRUTTURA.ID_VOCE = " & cmbvoce.SelectedValue

            Dim myReaderiva As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderiva.Read Then
                Me.txtivacorpo.Text = par.IfNull(myReaderiva("IVA"), "")
                Me.txtivaconsumo.Text = par.IfNull(myReaderiva("IVA"), "")
                Me.DescPF = par.IfNull(myReaderiva("CODICE"), "") & " - " & par.IfNull(myReaderiva("DESCRIZIONE"), "")
            Else
                Me.txtivacorpo.Text = 0
                Me.txtivaconsumo.Text = 0

            End If
            myReaderiva.Close()
            If par.IfEmpty(Me.perconsumo.Value, 0) > 0 Then
                Me.txtperconsumo.Text = Me.perconsumo.Value
            End If
            Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub AggiornaVociServizi()
        '****************************
        '****NUOVA VERSIONE**********
        '****************************
        '****************************
        Try

            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            Me.cmbvoce.Enabled = True
            Me.cmbvoce.Items.Clear()
            cmbvoce.Items.Add(New RadComboBoxItem(" ", -1))

            If CType(Me.Page, Object).vIdAppalti > 0 Then
                '****************************
                '****AGGIUNTA SERVIZI IN UPDATE**********




                par.cmd.CommandText = "SELECT ID,(codice ||' - '||descrizione) AS descrizione FROM siscom_mi.PF_VOCI WHERE  codice LIKE '1.%' AND id_piano_finanziario = " & CType(Me.Page, Object).vIdEsercizio _
                    & " and ID NOT IN (SELECT id_voce_madre FROM siscom_mi.PF_VOCI WHERE id_voce_madre IS NOT NULL) " _
                    & " and id not in (select id_pf_voce from siscom_mi.appalti_voci_pf where id_appalto = " & CType(Me.Page, Object).vIdAppalti & ")" _
                    & " AND ID IN ( SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_STRUTTURA = " & Session.Item("ID_STRUTTURA") _
                    & " AND (VALORE_LORDO + nvl(ASSESTAMENTO_VALORE_LORDO,0) + nvl(VARIAZIONI,0))>0) "







            Else 'AGGIUNTA SERVIZI TOGLIENDO QUELLI GIA' CARICATI NELLA CLASSE
                '****************************
                '****AGGIUNTA PRIMA INSERT**********
                Dim servizi As String
                Dim Condizione As String = ""
                servizi = ""
                For Each gen As Mario.VociServizi In lstservizi

                    If servizi = "" Then
                        servizi = gen.ID_PF_VOCE_IMPORTO
                    Else
                        servizi = servizi & "," & gen.ID_PF_VOCE_IMPORTO
                    End If
                Next

                If servizi <> "" Then
                    Condizione = " AND SISCOM_MI.PF_VOCI.ID not in " & "(" & servizi & ") "
                Else
                    Condizione = ""
                End If


                par.cmd.CommandText = "SELECT ID,(codice ||' - '||descrizione) AS descrizione FROM siscom_mi.PF_VOCI WHERE  codice LIKE '1.%' AND id_piano_finanziario = " & CType(Me.Page, Object).vIdEsercizio _
                                    & " " & Condizione & " AND ID NOT IN (SELECT id_voce_madre FROM siscom_mi.PF_VOCI WHERE id_voce_madre IS NOT NULL) " _
                                    & " AND ID IN ( SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_STRUTTURA = " & Session.Item("ID_STRUTTURA") _
                                    & " AND (VALORE_LORDO + nvl(ASSESTAMENTO_VALORE_LORDO,0) + nvl(VARIAZIONI,0))>0) "





            End If
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read()
                cmbvoce.Items.Add(New RadComboBoxItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
            End While
            myReader2.Close()
            cmbvoce.SelectedValue = -1

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"

        End Try

    End Sub
    Private Property DescPF() As String
        Get
            If Not (ViewState("par_DescPf") Is Nothing) Then
                Return CStr(ViewState("par_DescPf"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DescPf") = value
        End Set

    End Property

#Region "Salva & Update"
    Private Sub SalvaServizi()

        Try


            If CType(Me.Page, Object).vIdAppalti = 0 Then
                Dim gen As Mario.VociServizi

                gen = New Mario.VociServizi(lstservizi.Count, 0, 0, 0, Me.cmbvoce.SelectedValue,
                                            par.PulisciStrSql(Me.cmbvoce.SelectedItem.Text), par.IfEmpty(Me.txtimportocorpo.Text, 0),
                                            par.IfEmpty(Me.txtOneriCanone.Text, 0), par.PuntiInVirgole(par.IfEmpty(Me.txtscontocorpo.Text, 0)),
                                            par.PuntiInVirgole(par.IfEmpty(Me.txtivacorpo.Text, 0)), 0, par.IfEmpty(Me.txtimportoconsumo.Text, 0),
                                            par.IfEmpty(Me.txtOnerconsumo.Text, 0), par.PuntiInVirgole(par.IfEmpty(Me.txtscontoconsumo.Text, 0)),
                                            par.PuntiInVirgole(par.IfEmpty(Me.txtivaconsumo.Text, 0)), DescPF)

                DataGrid3.DataSource = Nothing
                lstservizi.Add(gen)
                gen = Nothing

                DataGrid3.DataSource = lstservizi
                DataGrid3.DataBind()


            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA FORNITORE

                '*********************************
                'CALCOLO: INPUT
                '       1) gen.IMPORTO_CONSUMO  txtimportoconsumo
                '       2) gen.SCONTO_CONSUMO   txtscontoconsumo
                '       3) gen.IVA_CONSUMO      txtivaconsumo
                '       4) oneri=PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0)

                'RESIDUO_CONSUMO= Importo al consumo -  %sconto al consumo  + % IVA Consumo + ( Oneri + % iva consumo)  

                'Dim Residuo As Decimal
                'Residuo = CalcolaResiduo(PAR.IfEmpty(Me.txtimportoconsumo.Text, 0), PAR.IfEmpty(Me.txtscontoconsumo.Text, 0), PAR.IfEmpty(Me.txtivaconsumo.Text, 0), PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0))
                '**************************************


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                ' RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                Dim PercOnSicConsumo As Decimal = 0
                If par.IfEmpty(txtimportoconsumo.Text, 0) > 0 Then
                    PercOnSicConsumo = (par.IfEmpty(par.VirgoleInPunti(Me.txtOnerconsumo.Text.Replace(".", "")), 0) / par.IfEmpty(par.VirgoleInPunti(Me.txtimportoconsumo.Text.Replace(".", "")), "0")) * 100
                    PercOnSicConsumo = Math.Round(PercOnSicConsumo, 4)

                End If


                par.cmd.CommandText = "insert into SISCOM_MI.APPALTI_VOCI_PF " _
                                            & " (ID_APPALTO,ID_PF_VOCE," _
                                            & "  IMPORTO_CANONE,SCONTO_CANONE,ONERI_SICUREZZA_CANONE,IVA_CANONE," _
                                            & "  IMPORTO_CONSUMO,SCONTO_CONSUMO,ONERI_SICUREZZA_CONSUMO,IVA_CONSUMO,PERC_ONERI_SIC_CON)" _
                              & " values (" & CType(Me.Page, Object).vIdAppalti & "," & Me.cmbvoce.SelectedValue & "," _
                                        & par.IfEmpty(par.VirgoleInPunti(Me.txtimportocorpo.Text.Replace(".", "")), "Null") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtscontocorpo.Text), "Null") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtOneriCanone.Text.Replace(".", "")), "Null") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtivacorpo.Text), "Null") & "," _
                                        & par.IfEmpty(par.VirgoleInPunti(Me.txtimportoconsumo.Text.Replace(".", "")), "Null") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtscontoconsumo.Text), "Null") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtOnerconsumo.Text.Replace(".", "")), "Null") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtivaconsumo.Text), "Null") & "," & par.VirgoleInPunti(PercOnSicConsumo) & ")"
                par.cmd.ExecuteNonQuery()

                '**** Ricavo ID VOCE SERVIZIO PER UTILIZZARLO IN SEGUITO
                'PAR.cmd.CommandText = " select SISCOM_MI.SEQ_APPALTI_VOCI_PF.ID_PF_VOCE_IMPORTO  from SISCOM_MI._APPALTI_VOCI_PF where SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE_IMPORTO=" & cmbvoce.SelectedValue
                'Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                'If myReaderI.Read Then
                '    Me.txtIDS.Text = myReaderI("ID_VOCE_SERVIZIO")
                'End If
                'myReaderI.Close()
                'PAR.cmd.CommandText = ""
                '**********

                DataGrid3.Rebind()

                '*** EVENTI_APPALTI
                InserisciEvento(par.cmd, CType(Me.Page, Object).vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - Inserimento voce servizio")

            End If

            'CALCOLO SOMMA IMPORTI
            somma()

            'CALCOLA PERCENTUALE
            percentuale()

            'RESIDUO
            'CalcolaResiduo()
            CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            If CType(Me.Page, Object).vIdAppalti <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"

        End Try

    End Sub
    Sub percentuale()

        'Try



        '    'per evitare divisioni per zero in caso togliessi tutti gli importi
        '    If CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastacanone"), TextBox).Text = "0,00" Then
        '        CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtpercanone"), TextBox).Text = ""
        '        CType(Page.FindControl("Tab_ImportiNP1").FindControl("canone"), HiddenField).Value = ""

        '    End If

        '    If CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastaconsumo"), TextBox).Text = "0,00" Then
        '        CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtperconsumo"), TextBox).Text = ""
        '        CType(Page.FindControl("Tab_ImportiNP1").FindControl("consumo"), HiddenField).Value = ""

        '    End If

        '    Dim percentualecanone As Decimal = 0
        '    Dim percentualeconsumo As Decimal = 0
        '    'canone
        '    If Val(Replace(Replace((CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastacanone"), TextBox).Text), ".", ""), ",", ".")) > 0 And Val(Replace(Replace((CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtonericanone"), TextBox).Text), ".", ""), ",", ".")) > 0 Then
        '        percentualecanone = (100 * Val(Replace(Replace((CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtonericanone"), TextBox).Text), ".", ""), ",", "."))) / Val(Replace(Replace((CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastacanone"), TextBox).Text), ".", ""), ",", "."))
        '        CType(Page.FindControl("Tab_ImportiNP1").FindControl("canone"), HiddenField).Value = percentualecanone
        '        CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtpercanone"), TextBox).Text = percentualecanone
        '    End If
        '    'consumo
        '    If Val(Replace(Replace((CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastaconsumo"), TextBox).Text), ".", ""), ",", ".")) > 0 And Val(Replace(Replace((CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtonericonsumo"), TextBox).Text), ".", ""), ",", ".")) > 0 Then
        '        percentualeconsumo = (100 * Val(Replace(Replace((CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtonericonsumo"), TextBox).Text), ".", ""), ",", "."))) / Val(Replace(Replace((CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastaconsumo"), TextBox).Text), ".", ""), ",", "."))
        '        CType(Page.FindControl("Tab_ImportiNP1").FindControl("consumo"), HiddenField).Value = percentualeconsumo
        '        CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtperconsumo"), TextBox).Text = percentualeconsumo
        '    End If
        'Catch ex As Exception

        '    CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
        '    CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"

        'End Try


    End Sub

    Private Sub UpdateAppalti()

        Try

            If CType(Me.Page, Object).vIdAppalti = 0 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA FORNITORE


                lstservizi(txtIdComponente0.Value).ID_LOTTO = 0
                lstservizi(txtIdComponente0.Value).ID_SERVIZIO = 0
                lstservizi(txtIdComponente0.Value).ID_PF_VOCE_IMPORTO = Me.cmbvoce.SelectedValue.ToString
                lstservizi(txtIdComponente0.Value).DESCRIZIONE = Me.cmbvoce.SelectedItem.Text
                lstservizi(txtIdComponente0.Value).IMPORTO_CANONE = Me.txtimportocorpo.Text
                lstservizi(txtIdComponente0.Value).SCONTO_CANONE = par.PuntiInVirgole(Me.txtscontocorpo.Text)
                lstservizi(txtIdComponente0.Value).IVA_CANONE = par.PuntiInVirgole(Me.txtivacorpo.Text)
                lstservizi(txtIdComponente0.Value).IMPORTO_CONSUMO = Me.txtimportoconsumo.Text
                lstservizi(txtIdComponente0.Value).SCONTO_CONSUMO = par.PuntiInVirgole(Me.txtscontoconsumo.Text)
                lstservizi(txtIdComponente0.Value).IVA_CONSUMO = par.PuntiInVirgole(Me.txtivaconsumo.Text)
                lstservizi(txtIdComponente0.Value).ONERI_SICUREZZA_CONSUMO = par.IfEmpty(Me.txtOnerconsumo.Text, 0)


                DataGrid3.DataSource = lstservizi
                DataGrid3.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA FORNITORE

                '*********************************
                'CALCOLO: INPUT
                '       1) gen.IMPORTO_CONSUMO  txtimportoconsumo
                '       2) gen.SCONTO_CONSUMO   txtscontoconsumo
                '       3) gen.IVA_CONSUMO      txtivaconsumo
                '       4) oneri=PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0)

                'RESIDUO_CONSUMO= Importo al consumo -  %sconto al consumo  + % IVA Consumo + ( Oneri + % iva consumo)  

                'Dim Residuo As Decimal
                'Residuo = CalcolaResiduo(PAR.IfEmpty(Me.txtimportoconsumo.Text, 0), PAR.IfEmpty(Me.txtscontoconsumo.Text, 0), PAR.IfEmpty(Me.txtivaconsumo.Text, 0), PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0))
                '**************************************


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                ' RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                Dim PercOnSicConsumo As Decimal = 0
                If txtimportoconsumo.Text > 0 Then
                    PercOnSicConsumo = (par.IfEmpty(par.VirgoleInPunti(Me.txtOnerconsumo.Text.Replace(".", "")), 0) / par.IfEmpty(par.VirgoleInPunti(Me.txtimportoconsumo.Text.Replace(".", "")), "0")) * 100
                    PercOnSicConsumo = Math.Round(PercOnSicConsumo, 4)
                End If


                par.cmd.CommandText = "update SISCOM_MI.APPALTI_VOCI_PF set " _
                                            & "ID_PF_VOCE=" & par.PulisciStrSql(Me.cmbvoce.SelectedValue) & "," _
                                            & "IMPORTO_CANONE=" & par.IfEmpty(par.VirgoleInPunti(Me.txtimportocorpo.Text.Replace(".", "")), "Null") & "," _
                                            & "SCONTO_CANONE=" & par.IfEmpty(par.VirgoleInPunti(Me.txtscontocorpo.Text.Replace(".", "")), "Null") & "," _
                                            & "IVA_CANONE=" & par.IfEmpty(par.VirgoleInPunti(Me.txtivacorpo.Text.Replace(".", "")), "Null") & "," _
                                            & "IMPORTO_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(Me.txtimportoconsumo.Text.Replace(".", "")), "Null") & "," _
                                            & "SCONTO_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(Me.txtscontoconsumo.Text.Replace(".", "")), "Null") & "," _
                                            & "ONERI_SICUREZZA_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(Me.txtOnerconsumo.Text.Replace(".", "")), "Null") & "," _
                                            & "IVA_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(Me.txtivaconsumo.Text.Replace(".", "")), "Null") & ", " _
                                            & "PERC_ONERI_SIC_CON = " & par.VirgoleInPunti(PercOnSicConsumo) _
                                            & " WHERE ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " and ID_PF_VOCE=" & Me.txtIDS.Value

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()


                DataGrid3.Rebind()

                '*** EVENTI_FORNITORI
                InserisciEvento(par.cmd, CType(Me.Page, Object).vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica dati voci servizio appalto")

            End If



            'CALCOLO SOMMA IMPORTI
            somma()

            'CALCOLA PERCENTUALE
            percentuale()

            'RESIDUO

            CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

            Me.divVisible.Value = 0
            'DEVO FARE UPDATE DELLE VOCI DI SERVIZI A CANONE E A CONSUMO SULLE QUALI é POSSIBILE INSERIRE UNA VARIAZIONE!
            'SE IL CONTRATTO è ATTIVO ED è POSSIBILE MODIFICARE I SERVIZI. SE QUESTO NON é POSSIBILE NON SERVE AGGIORNARE NULLA!

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            If CType(Me.Page, Object).vIdAppalti <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"

        End Try

    End Sub

#End Region

    'Public Sub BindGrid_Servizi()
    '    Dim StringaSql As String

    '    Try

    '        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
    '        ' RIPRENDO LA CONNESSIONE
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)
    '        ' RIPRENDO LA TRANSAZIONE
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '        ‘‘par.cmd.Transaction = par.myTrans

    '        ' I NOMI DEI CAMPI DEVONO CORRISPONDERE CON QUELLI NELLA DATAGRID

    '        StringaSql = "SELECT ROWNUM AS ""ID"",'0' as id_lotto,'' as ID_SERVIZIO,'' as SERVIZIO ," _
    '                    & "SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE as id_pf_voce_importo," _
    '                    & "TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.IMPORTO_CANONE,'9G999G999G999G999G990D99')) AS ""IMPORTO_CANONE"", " _
    '                    & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.SCONTO_CANONE,'9G999G999G999G999G990D99'))AS ""SCONTO_CANONE"",SISCOM_MI.APPALTI_VOCI_PF.IVA_CANONE, " _
    '                    & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.IMPORTO_CONSUMO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUMO"", TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.SCONTO_CONSUMO,'9G999G999G999G999G990D99'))AS ""SCONTO_CONSUMO""," _
    '                    & " SISCOM_MI.APPALTI_VOCI_PF.IVA_CONSUMO,(PF_VOCI.CODICE ||' '|| PF_VOCI.DESCRIZIONE) AS DESCRIZIONE,'' AS DESC_PF,TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.ONERI_SICUREZZA_CONSUMO,'9G999G999G999G999G990D99')) AS ""ONERI_SICUREZZA_CONSUMO""" _
    '                    & " FROM SISCOM_MI.APPALTI_VOCI_PF,SISCOM_MI.PF_VOCI " _
    '                    & " WHERE SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID    AND SISCOM_MI.APPALTI_VOCI_PF.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti


    '        par.cmd.CommandText = StringaSql

    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim ds As New Data.DataSet()

    '        da.Fill(ds, "APPALTI_VOCI_PF")


    '        DataGrid3.DataSource = ds
    '        DataGrid3.DataBind()

    '        ds.Dispose()
    '        AggiornaVociServizi()

    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
    '    End Try

    'End Sub
    Public Sub CalcolaResiduo()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If CType(Me.Page, Object).vIdAppalti > 0 Then
                Dim STANZIATO_CONUSMO As Decimal = 0
                Dim STANZIATO_CANONE As Decimal = 0
                Dim RESIDUO As Decimal = 0
                'Dim OnConsumo As Decimal = 0
                'Dim OnCanone As Decimal = 0

                ''*-*******DEFINIZIONE DEGLI IMPORTI NETTI A CANONE E A CONSUMO COMPRENSIVI DI ONERI
                'par.cmd.CommandText = "SELECT ROUND(SUM ((Importo_consumo - (Importo_consumo*(sconto_consumo/100)))+(Importo_consumo - (Importo_consumo*(sconto_consumo/100)))*(APPALTI_VOCI_PF.iva_consumo/100) ),8) AS TOT_IMP_CONSUMO" _
                '                    & ",ROUND(SUM(IMPORTO_CONSUMO),8) AS TOT_LORDO_CONSUMO, " _
                '                    & "ONERI_SICUREZZA_CONSUMO, " _
                '                    & "ROUND(SUM ((Importo_canone - (Importo_canone*(sconto_canone/100)))+(Importo_canone - (Importo_canone*(sconto_canone/100)))*(APPALTI_VOCI_PF.iva_canone/100) ),8) AS  TOT_IMP_CANONE, ONERI_SICUREZZA_CANONE, " _
                '                    & "ROUND(SUM(IMPORTO_CANONE),8) AS TOT_LORDO_CANONE " _
                '                    & "FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_VOCI_PF WHERE " _
                '                    & "ID_APPALTO = APPALTI.ID AND ID = " & CType(Me.Page, Object).vIdAppalti & " AND ID_APPALTO = APPALTI.ID GROUP BY ONERI_SICUREZZA_CONSUMO,ONERI_SICUREZZA_CANONE"
                'Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'If myreader.Read Then
                '    STANZIATO_CONUSMO = par.IfNull(myreader("TOT_IMP_CONSUMO"), 0) + par.IfNull(myreader("ONERI_SICUREZZA_CONSUMO"), 0)
                '    STANZIATO_CANONE = par.IfNull(myreader("TOT_IMP_CANONE"), 0) + par.IfNull(myreader("ONERI_SICUREZZA_CANONE"), 0)
                '    OnConsumo = par.IfNull(myreader("ONERI_SICUREZZA_CONSUMO"), 0)
                '    OnCanone = par.IfNull(myreader("ONERI_SICUREZZA_CANONE"), 0)
                'End If
                'myreader.Close()
                STANZIATO_CONUSMO = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpTotPlusOneriCon"), TextBox).Text, 0)
                '******************CALCOLO RESIDUO A CONSUMO*************************
                Dim ResidRevers As Double = 0
                par.cmd.CommandText = "SELECT ROUND(SUM(IMPORTO_TOT),8) AS TOT_MANUTENZIONI FROM SISCOM_MI.MANUTENZIONI " _
                                    & "WHERE (STATO = 1 OR STATO = 2 OR STATO = 4)AND MANUTENZIONI.id_appalto in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " ))"
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                myreader = par.cmd.ExecuteReader
                '******DETTAGLI
                '1). Il RESIDUO = STANZIATO_NETTO + ONERI - SOMMA(IMPORTI EMESSI SULLE MANUTENZIONI) nb:gli importi emessi sulle manutenzioni sono comprensivi di oneri!☺
                '2). Il ResiduoReversibile = SOMMA((IMPORTI EMESSI SULLE MANUTENZIONI)* %REVERSIBILITA)

                If myreader.Read Then
                    RESIDUO = STANZIATO_CONUSMO - par.IfNull(myreader("TOT_MANUTENZIONI"), 0)
                End If
                myreader.Close()

                ''*****DETTAGLI
                ''1). SELEZIONO LA PERCENTUALE DI REVERSIBILITA NEGLI IMPORTI EMESSI SULLE MANUTENZIONI
                'par.cmd.CommandText = "SELECT IMPORTO_TOT AS TOT_MANUTENZIONI , PERC_REVERSIBILITA FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE (STATO = 1 OR STATO = 2) AND MANUTENZIONI.ID_PF_VOCE_IMPORTO = PF_VOCI_IMPORTO.ID AND MANUTENZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                'myreader = par.cmd.ExecuteReader
                'While myreader.Read
                '    ResidRevers = ResidRevers + ((par.IfNull(myreader("TOT_MANUTENZIONI"), 0) * par.IfNull(myreader("PERC_REVERSIBILITA"), 0)) / 100)
                'End While
                'myreader.Close()


                'Dim TOT_REV_CONSUMO As Double = 0
                'Dim SCONTO_CON As Double = 0
                'Dim IVA_CON As Double = 0

                'par.cmd.CommandText = "SELECT id_appalto,PF_VOCI_STRUTTURA.ID_voce , ROUND(importo_consumo,4) as IMPORTO_CONSUMO, " _
                '                    & "ROUND(sconto_consumo,4)AS sconto_consumo,ROUND(APPALTI_VOCI_PF.iva_consumo,4)AS iva_consumo, ROUND((ONERI_SICUREZZA_CONSUMO/(SELECT COUNT(ID_APPALTO)FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO =  " & CType(Me.Page, Object).vIdAppalti & ")),4) AS PARTE_ONERI " _
                '                    & "FROM siscom_mi.APPALTI_VOCI_PF, SISCOM_MI.PF_VOCI_STRUTTURA, SISCOM_MI.APPALTI " _
                '                    & "WHERE  APPALTI.ID = APPALTI_VOCI_PF.ID_APPALTO AND APPALTI_VOCI_PF.ID_PF_VOCE = PF_VOCI_STRUTTURA.ID_voce AND id_appalto= " & CType(Me.Page, Object).vIdAppalti
                'myreader = par.cmd.ExecuteReader

                'While myreader.Read
                '    SCONTO_CON = (par.IfNull(myreader("importo_consumo"), 0) * par.IfNull(myreader("sconto_consumo"), 0)) / 100
                '    IVA_CON = ((par.IfNull(myreader("importo_consumo"), 0) - SCONTO_CON) * par.IfNull(myreader("iva_consumo"), 0)) / 100
                '    TOT_REV_CONSUMO = TOT_REV_CONSUMO + (((par.IfNull(myreader("importo_consumo"), 0) - SCONTO_CON + IVA_CON) + par.IfNull(myreader("PARTE_ONERI"), 0)))

                'End While
                'myreader.Close()
                'ResidRevers = TOT_REV_CONSUMO - ResidRevers

                DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtresiduoConsumo"), TextBox).Text = Format(RESIDUO, "##,##0.00")
                '-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*--*-*-*****************************************----------------------------
                'Residuo canone
                RESIDUO = 0
                ResidRevers = 0
                '*-*-*-*-*-*-*--*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-FINE-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*


                '******************CALCOLO RESIDUO A CANONE

                'PEPPE MODIFY 02/11/2010 
                ' MODIFICO LA QUERY DELLE PRENOTAZIONI IN MODO DA NON PRENDERE GLI IMPORTI PRENOTATI PER L'ANNO SUCCESSIOVO ...O A SEGUIRE!!

                'par.cmd.CommandText = "SELECT ROUND(SUM(IMPORTO_PRENOTATO),8) AS PRENOTAZIONI FROM SISCOM_MI.PRENOTAZIONI WHERE TIPO_PAGAMENTO = 6 AND ID_PAGAMENTO IS NULL AND ID_STATO = 0 AND ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti

                ''par.cmd.CommandText = "SELECT SUM(SUM(IMPORTO_PRENOTATO)) AS PRENOTAZIONI " _
                ''                    & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.APPALTI_VOCI_PF " _
                ''                    & "WHERE PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO AND APPALTI_VOCI_PF.ID_APPALTO = PRENOTAZIONI.ID_APPALTO " _
                ''                    & "AND PRENOTAZIONI.ID_APPALTO =" & CType(Me.Page, Object).vIdAppalti& " AND TIPO_PAGAMENTO = 6 AND ID_PAGAMENTO IS NULL AND ID_STATO = 0 " _
                ''                    & "GROUP BY PERC_REVERSIBILITA,IMPORTO_PRENOTATO,APPALTI_VOCI_PF.IMPORTO_CANONE,SCONTO_CANONE,APPALTI_VOCI_PF.IVA_CANONE"

                'myreader = par.cmd.ExecuteReader
                'If myreader.Read Then
                '    RESIDUO = STANZIATO_CANONE - par.IfNull(myreader("PRENOTAZIONI"), 0)
                'End If
                'myreader.Close()


                ''*****SELEZIONE DELLE QUOTE REVERSIBILI PRENOTATE
                'par.cmd.CommandText = "SELECT ROUND(IMPORTO_PRENOTATO,4)AS IMPORTO_PRENOTATO,ROUND(PERC_REVERSIBILITA,4) AS PERC_REVERSIBILITA " _
                '                    & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO " _
                '                    & "WHERE  PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID AND TIPO_PAGAMENTO = 6 AND ID_PAGAMENTO IS NULL AND ID_STATO = 0 " _
                '                    & "AND PRENOTAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                'myreader = par.cmd.ExecuteReader
                'While myreader.Read
                '    ResidRevers = ResidRevers + ((par.IfNull(myreader("IMPORTO_PRENOTATO"), 0) * par.IfNull(myreader("PERC_REVERSIBILITA"), 0)) / 100)
                'End While
                'myreader.Close()



                'Dim TOT_REV_CANONE As Double = 0
                'Dim SCONTO_CAN As Double = 0
                'Dim IVA_CAN As Double = 0
                'par.cmd.CommandText = "SELECT id_appalto,PF_VOCI_STRUTTURA.ID_voce , ROUND(importo_CANONE,4)AS importo_CANONE, " _
                '                    & "ROUND(sconto_CANONE,4) AS sconto_CANONE,ROUND(APPALTI_VOCI_PF.iva_CANONE,4) AS iva_CANONE , ROUND((ONERI_SICUREZZA_CANONE/(SELECT COUNT(ID_APPALTO)FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO =  " & CType(Me.Page, Object).vIdAppalti & ")),4) AS PARTE_ONERI " _
                '                    & "FROM siscom_mi.APPALTI_VOCI_PF, SISCOM_MI.PF_VOCI_STRUTTURA, SISCOM_MI.APPALTI " _
                '                    & "WHERE  APPALTI.ID = APPALTI_VOCI_PF.ID_APPALTO AND APPALTI_VOCI_PF.ID_PF_VOCE = PF_VOCI_STRUTTURA.ID_voce AND id_appalto= " & CType(Me.Page, Object).vIdAppalti
                'myreader = par.cmd.ExecuteReader

                'While myreader.Read
                '    SCONTO_CAN = (par.IfNull(myreader("IMPORTO_CANONE"), 0) * par.IfNull(myreader("sconto_canone"), 0)) / 100
                '    IVA_CAN = ((par.IfNull(myreader("IMPORTO_CANONE"), 0) - SCONTO_CAN) * par.IfNull(myreader("iva_canone"), 0)) / 100
                '    TOT_REV_CANONE = TOT_REV_CANONE + ((par.IfNull(myreader("IMPORTO_CANONE"), 0) - SCONTO_CAN + IVA_CAN + par.IfNull(myreader("PARTE_ONERI"), 0)))
                'End While
                'myreader.Close()

                'ResidRevers = TOT_REV_CANONE - ResidRevers

                'par.cmd.CommandText = "SELECT ROUND(SUM(IMPORTO_APPROVATO),8) AS PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE  TIPO_PAGAMENTO = 6 AND ID_STATO > 0 AND PRENOTAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                'myreader = par.cmd.ExecuteReader
                'If myreader.Read Then
                '    RESIDUO = RESIDUO - par.IfNull(myreader("PAGAMENTO"), 0)
                'End If
                'myreader.Close()
                'Dim ReversibPagati As Double = 0
                'par.cmd.CommandText = "SELECT ROUND(IMPORTO_APPROVATO,4)AS IMPORTO_APPROVATO,ROUND(PERC_REVERSIBILITA,4) AS PERC_REVERSIBILITA " _
                '                    & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO " _
                '                    & "WHERE  PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID AND TIPO_PAGAMENTO = 6 AND ID_STATO > 0 " _
                '                    & "AND PRENOTAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                'myreader = par.cmd.ExecuteReader
                'While myreader.Read
                '    ReversibPagati = ReversibPagati + ((par.IfNull(myreader("IMPORTO_APPROVATO"), 0) * par.IfNull(myreader("PERC_REVERSIBILITA"), 0)) / 100)
                'End While
                'myreader.Close()
                'ResidRevers = ResidRevers - ReversibPagati


                DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtResiduoCanone"), TextBox).Text = Format(RESIDUO, "##,##0.00")

            Else
                DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtresiduoConsumo"), TextBox).Text = Format(0, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtResiduoCanone"), TextBox).Text = Format(0, "##,##0.00")

            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
        End Try
    End Sub

    Public Sub CalcolaImpContrattuale()
        Try
            If CType(Me.Page, Object).vIdAppalti > 0 Then

                '*******************RICHIAMO LA CONNESSIONECONNANP
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'l'iva è da aggiungere all'importo comprensivo di sconto

                'par.cmd.CommandText = "SELECT ROUND(SUM(SUM ((Importo_consumo - (Importo_consumo*(sconto_consumo/100)))+(Importo_consumo - (Importo_consumo*(sconto_consumo/100)))*(APPALTI_VOCI_PF.iva_consumo/100) )),8) AS ImpContConsumo, " _
                '                    & "ROUND(SUM(SUM ((Importo_canone - (Importo_canone*(sconto_canone/100)))+(Importo_canone - (Importo_canone*(sconto_canone/100)))*(APPALTI_VOCI_PF.iva_canone/100) ) ),8)AS ImpContCanone " _
                '                    & "FROM  siscom_mi.APPALTI_VOCI_PF " _
                '                    & "WHERE  id_appalto = " & CType(Me.Page, Object).vIdAppalti & " GROUP BY ID_APPALTO"
                Dim Myreader As Oracle.DataAccess.Client.OracleDataReader
                ''If Myreader.Read Then
                ''    DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpContCanone"), TextBox).Text = Format(par.IfNull(Myreader("ImpContCanone"), 0), "##,##0.00")
                ''    DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpContConsumo"), TextBox).Text = Format(par.IfNull(Myreader("ImpContConsumo"), 0), "##,##0.00")

                ''End If

                par.cmd.CommandText = "SELECT APPALTI_VOCI_PF.*,APPALTI_VOCI_PF.PERC_ONERI_SIC_CAN,APPALTI_VOCI_PF.PERC_ONERI_SIC_CON FROM siscom_mi.APPALTI_VOCI_PF,siscom_mi.appalti " _
                    & " WHERE Appalti.ID = APPALTI_VOCI_PF.id_appalto And id_appalto = " & CType(Me.Page, Object).vIdAppalti
                Myreader = par.cmd.ExecuteReader

                '******CALCOLO PERCENTUALE ONERI IN BASE AL TESTO SCRITTO
                'Dim PercOneriCon As Decimal = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtperconsumo"), TextBox).Text, 0)
                'Dim PercOneriCan As Decimal = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtpercanone"), TextBox).Text, 0)
                Dim TOTNettoCan As Decimal = 0
                Dim TOTNettoCons As Decimal = 0
                'Dim lettoreForOneri As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'While lettoreForOneri.Read
                '    TOTNettoCons = TOTNettoCons + (par.IfNull(lettoreForOneri("IMPORTO_CONSUMO"), 0) - (par.IfNull(lettoreForOneri("IMPORTO_CONSUMO"), 0) * (par.IfNull(lettoreForOneri("SCONTO_CONSUMO"), 0) / 100)))
                '    TOTNettoCan = TOTNettoCan + (par.IfNull(lettoreForOneri("IMPORTO_CANONE"), 0) - (par.IfNull(lettoreForOneri("IMPORTO_CANONE"), 0) * (par.IfNull(lettoreForOneri("SCONTO_CANONE"), 0) / 100)))
                'End While
                'lettoreForOneri.Close()
                'PercOneriCan = (par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtonericanone"), TextBox).Text, 0) * 100) / TOTNettoCan
                'PercOneriCon = (par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtonericonsumo"), TextBox).Text, 0) * 100) / TOTNettoCons

                Dim TotOneri As Decimal = 0

                If Myreader.HasRows = True Then
                    Dim ImpContConsumo As Decimal = 0
                    Dim ImpContCanone As Decimal = 0
                    Dim Netto As Decimal = 0
                    Dim NettoPlusOneri As Decimal = 0
                    Dim TotNetto As Decimal = 0
                    While Myreader.Read

                        'importo contrattuale CONSUMO
                        Netto = par.IfNull(Myreader("IMPORTO_CONSUMO"), 0) - ((par.IfNull(Myreader("IMPORTO_CONSUMO"), 0) * par.IfNull(Myreader("SCONTO_CONSUMO"), 0)) / 100)
                        NettoPlusOneri = Netto + (par.IfNull(Myreader("ONERI_SICUREZZA_CONSUMO"), 0))
                        ImpContConsumo = ImpContConsumo + (NettoPlusOneri + ((NettoPlusOneri * par.IfNull(Myreader("IVA_CONSUMO"), 0)) / 100))

                        TotNetto = TotNetto + Netto
                        TotOneri = TotOneri + par.IfNull(Myreader("ONERI_SICUREZZA_CONSUMO"), 0)
                        ''importo contrattuale CANONE
                        'Netto = par.IfNull(Myreader("IMPORTO_CANONE"), 0) - ((par.IfNull(Myreader("IMPORTO_CANONE"), 0) * par.IfNull(Myreader("SCONTO_CANONE"), 0)) \ 100)
                        'NettoPlusOneri = Netto + ((Netto * (PercOneriCan)) \ 100)
                        'ImpContCanone = ImpContCanone + (NettoPlusOneri + ((NettoPlusOneri * par.IfNull(Myreader("IVA_CANONE"), 0)) \ 100))


                    End While
                    Myreader.Close()

                    DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpContConsumo"), TextBox).Text = Format(TotNetto, "##,##0.00")
                    DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpTotPlusOneriCon"), TextBox).Text = Format(ImpContConsumo, "##,##0.00")
                    DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpContCanone"), TextBox).Text = Format(ImpContCanone, "##,##0.00")

                Else
                    DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpContCanone"), TextBox).Text = Format(0, "##,##0.00")
                    DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpContConsumo"), TextBox).Text = Format(0, "##,##0.00")

                End If

                DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtonericonsumo"), TextBox).Text = Format(TotOneri, "##,##0.00")

            Else


                Dim ImpContConsumo As Double = 0
                Dim TOTNettoCons As Decimal = 0
                Dim NettoPlusOneri As Decimal = 0
                Dim TotOneri As Decimal = 0
                'Dim TOTNettoCan As Decimal = 0
                'Dim PercOneriCon As Decimal = 0
                'Dim PercOneriCan As Decimal = 0
                Dim N As Decimal = 0
                Dim lstservizi As System.Collections.Generic.List(Of Mario.VociServizi)
                lstservizi = CType(HttpContext.Current.Session.Item("LSTSERVIZI"), System.Collections.Generic.List(Of Mario.VociServizi))
                For Each gen As Mario.VociServizi In lstservizi
                    TOTNettoCons = TOTNettoCons + (gen.IMPORTO_CONSUMO - (gen.IMPORTO_CONSUMO * (gen.SCONTO_CONSUMO / 100)))
                    'TOTNettoCan = TOTNettoCan + (gen.IMPORTO_CANONE - (gen.IMPORTO_CANONE * (gen.SCONTO_CANONE / 100)))
                    TotOneri = TotOneri + par.IfEmpty(gen.ONERI_SICUREZZA_CONSUMO.Replace(".", ""), 0)
                Next

                If par.IfEmpty(TOTNettoCons, 0) > 0 Then
                    'PercOneriCon = (par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtonericonsumo"), TextBox).Text, 0) * 100) / TOTNettoCons

                    DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpContConsumo"), TextBox).Text = Format(TOTNettoCons, "##,##0.00")

                    For Each gen As Mario.VociServizi In lstservizi
                        '******-*SEZIONE CANONE
                        'N = (gen.IMPORTO_CANONE - (gen.IMPORTO_CANONE * (gen.SCONTO_CANONE / 100)))
                        'NettoPlusOneri = N + ((N * PercOneriCan) \ 100)

                        'N = 0
                        'NettoPlusOneri = 0

                        'ImpContConsumo = ImpContConsumo + ((gen.IMPORTO_CONSUMO - (gen.IMPORTO_CONSUMO * (gen.SCONTO_CONSUMO / 100))) + ((gen.IMPORTO_CONSUMO - (gen.IMPORTO_CONSUMO * (gen.SCONTO_CONSUMO / 100))) * (gen.IVA_CONSUMO / 100)))
                        'ImpContCanone = ImpContCanone + ((gen.IMPORTO_CANONE - (gen.IMPORTO_CANONE * (gen.SCONTO_CANONE / 100))) + ((gen.IMPORTO_CANONE - (gen.IMPORTO_CANONE * (gen.SCONTO_CANONE / 100))) * (gen.IVA_CANONE / 100)))

                        'ImpContCanone = ImpContCanone + (gen.IMPORTO_CANONE - (gen.IMPORTO_CANONE - ((gen.IMPORTO_CANONE * gen.SCONTO_CANONE) / 100) + ((gen.IMPORTO_CANONE * gen.IVA_CANONE) / 100)))
                        '*****-*SEZIONE CONSUMO
                        N = (gen.IMPORTO_CONSUMO - (gen.IMPORTO_CONSUMO * (gen.SCONTO_CONSUMO / 100)))

                        'NettoPlusOneri = N + ((N * PercOneriCon) \ 100)
                        NettoPlusOneri = N + par.IfEmpty(gen.ONERI_SICUREZZA_CONSUMO.Replace(".", ""), 0)

                        ImpContConsumo = ImpContConsumo + NettoPlusOneri + ((NettoPlusOneri * gen.IVA_CONSUMO) / 100)


                    Next


                End If

                ''****SET ROUND PRECISIONE TO PRESERVE OVERFLOW EXCEPTION
                'PercOneriCan = Math.Round(PercOneriCan, 4)
                'PercOneriCon = Math.Round(PercOneriCon, 4)
                DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpTotPlusOneriCon"), TextBox).Text = Format(ImpContConsumo, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtonericonsumo"), TextBox).Text = Format(TotOneri, "##,##0.00")

                'DirectCast(Me.Page.FindControl("Tab_ImportiNP1").FindControl("txtImpContCanone"), TextBox).Text = Format(ImpContCanone, "##,##0.00")

            End If


            CalcolaResiduo()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
        End Try
    End Sub




    Public Function InserisciEvento(ByVal MyPar As Oracle.DataAccess.Client.OracleCommand, ByVal vIdFornitore As Long, ByVal vIdOperatore As Long, ByVal Tab_Eventi As Integer, ByRef Motivazione As String) As Boolean

        Try

            InserisciEvento = False

            MyPar.Parameters.Clear()
            If InStr(Motivazione, "Modifica") Then
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI(ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & CType(Me.Page, Object).vIdAppalti & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F0" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            Else
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI(ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & CType(Me.Page, Object).vIdAppalti & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            End If
            MyPar.ExecuteNonQuery()
            MyPar.CommandText = ""
            MyPar.Parameters.Clear()


        Catch ex As Exception
            InserisciEvento = False
            MyPar.Parameters.Clear()
        End Try

    End Function
    Sub somma()
        Try
            Dim sommacanone As Decimal = 0
            Dim sommaconsumo As Decimal = 0

            Dim i As Integer

            For i = 0 To Me.DataGrid3.Items.Count - 1
                If Me.DataGrid3.Items(i).Cells(9).Text = "&nbsp;" Then Me.DataGrid3.Items(i).Cells(9).Text = ""
                sommacanone = sommacanone + par.IfEmpty(Me.DataGrid3.Items(i).Cells(7).Text, 0)

                If Me.DataGrid3.Items(i).Cells(12).Text = "&nbsp;" Then Me.DataGrid3.Items(i).Cells(12).Text = ""
                sommaconsumo = sommaconsumo + par.IfEmpty(Me.DataGrid3.Items(i).Cells(10).Text, 0)
            Next


            'ASSEGNO AL CAMPO DI DESTINAZIONE I RISULTATI FORMATTATI
            If sommacanone <> 0 Then
                CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastacanone"), TextBox).Text = IsNumFormat(sommacanone, "", "##,##0.00")
            End If

            If sommaconsumo <> 0 Then
                CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastaconsumo"), TextBox).Text = IsNumFormat(sommaconsumo, "", "##,##0.00")
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"

        End Try

    End Sub

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
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
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
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


    Function IsNumFormatClasse(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If v = "Null" Then
            IsNumFormatClasse = S
        Else
            IsNumFormatClasse = Format(CDbl(v), Precision)
        End If
    End Function

    'Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then


    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VociNPl1_txtIdComponente').value='" & e.Item.Cells(5).Text & "';document.getElementById('Tab_VociNPl1_txtIdComponente0').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_VociNPl1_txtIdComponente1').value='" & e.Item.Cells(2).Text & "';")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then


    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_VociNPl1_txtIdComponente').value='" & e.Item.Cells(5).Text & "';document.getElementById('Tab_VociNPl1_txtIdComponente0').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_VociNPl1_txtIdComponente1').value='" & e.Item.Cells(2).Text & "';")

    '    End If

    'End Sub

    Protected Sub btnApriAppalti_Click(sender As Object, e As System.EventArgs) Handles btnApriAppalti.Click
        Try
            ApriServizio()

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            par.myTrans.Rollback()
            par.OracleConn.Close()

            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
        End Try
    End Sub

    Private Sub ApriServizio()
        If txtIdComponente.Value = "" Then
            'Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            txtAppareP.Value = "0"

        Else

            Me.cmbvoce.Enabled = True

            Me.txtimportoconsumo.Enabled = True
            Me.txtscontoconsumo.Enabled = True

            If CType(Me.Page, Object).vIdAppalti = 0 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA APPALTO
                Me.txtIDS.Value = lstservizi(txtIdComponente0.Value).ID_PF_VOCE_IMPORTO

                '*******************RICHIAMO LA CONNESSIONECONNANP
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans


                cmbvoce.Items.Clear()
                'par.cmd.CommandText = "select SISCOM_MI.PF_VOCI_IMPORTO.ID, SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE  FROM SISCOM_MI.PF_VOCI_IMPORTO where SISCOM_MI.PF_VOCI_IMPORTO.ID not in (select SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_LOTTO=" & idLotti & " and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_SERVIZIO=" & Me.cmbservizio.SelectedValue & ") and SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbservizio.SelectedValue
                par.cmd.CommandText = "select ID, DESCRIZIONE  FROM SISCOM_MI.PF_VOCI where ID = " & Me.txtIDS.Value
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read
                    cmbvoce.Items.Add(New RadComboBoxItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                End While
                myReader2.Close()


                Me.cmbvoce.SelectedItem.Text = lstservizi(txtIdComponente0.Value).DESCRIZIONE
                Me.txtimportocorpo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).IMPORTO_CANONE, 0), "", "##,##0.00")
                Me.txtscontocorpo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).SCONTO_CANONE, 0), "", "##,##0.0000") 'PAR.IfNull(lstservizi(txtIdComponente0.Text).SCONTO_CORPO, "")
                Me.txtivacorpo.Text = par.IfNull(lstservizi(txtIdComponente0.Value).IVA_CANONE, "")
                Me.txtimportoconsumo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).IMPORTO_CONSUMO, 0), "", "##,##0.00")
                Me.txtscontoconsumo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).SCONTO_CONSUMO, 0), "", "##,##0.000") 'PAR.IfNull(lstservizi(txtIdComponente0.value).SCONTO_CONSUMO, "")
                Me.txtivaconsumo.Text = par.IfNull(lstservizi(txtIdComponente0.Value).IVA_CONSUMO, "")
                Me.txtOnerconsumo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).ONERI_SICUREZZA_CONSUMO, 0), "", "##,##0.00")

                If Me.txtimportoconsumo.Text > 0 Then
                    Me.txtperconsumo.Text = Math.Round((txtOnerconsumo.Text.Replace(".", "") / txtimportoconsumo.Text.Replace(".", "")) * 100, 4)
                    perconsumo.Value = Me.txtperconsumo.Text
                Else
                    txtperconsumo.Text = 0
                    perconsumo.Value = ""
                End If


            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA APPALTO

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    '*******************RICHIAMO LA CONNESSIONECONNANP
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                End If

                Dim Str1 As String
                Str1 = "select pf_voci.id, pf_voci.descrizione from siscom_mi.pf_voci where id = " & txtIdComponente.Value
                par.cmd.CommandText = Str1

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader1.Read Then
                    cmbvoce.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End If
                myReader1.Close()
                Str1 = " SELECT APPALTI_VOCI_PF.*  FROM SISCOM_MI.APPALTI_VOCI_PF WHERE SISCOM_MI.APPALTI_VOCI_PF.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " AND SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE=" & txtIdComponente.Value

                par.cmd.CommandText = Str1

                myReader1 = par.cmd.ExecuteReader

                If myReader1.Read Then
                    Me.txtIDS.Value = par.IfNull(myReader1("ID_PF_VOCE"), -1)
                    Me.txtimportocorpo.Text = IsNumFormat(par.IfNull(myReader1("IMPORTO_CANONE"), 0), "", "##,##0.00")
                    Me.txtscontocorpo.Text = IsNumFormat(par.IfNull(myReader1("SCONTO_CANONE"), 0), "", "0.000") 'PAR.IfNull(myReader1("SCONTO_CORPO"), "")
                    Me.txtivacorpo.Text = par.IfNull(par.IfNull(myReader1("IVA_CANONE"), 0), "")
                    Me.txtimportoconsumo.Text = IsNumFormat(par.IfNull(myReader1("IMPORTO_CONSUMO"), 0), "", "##,##0.00")
                    Me.txtscontoconsumo.Text = IsNumFormat(par.IfNull(myReader1("SCONTO_CONSUMO"), 0), "", "0.000") 'PAR.IfNull(myReader1("SCONTO_CONSUMO"), "")
                    Me.txtivaconsumo.Text = par.IfNull(par.IfNull(myReader1("IVA_CONSUMO"), 0), "")
                    Me.cmbvoce.SelectedValue = par.IfNull(myReader1("ID_PF_VOCE"), -1)
                    Me.txtOnerconsumo.Text = IsNumFormat(par.IfNull(myReader1("ONERI_SICUREZZA_CONSUMO"), 0), "", "##,##0.00")
                    Me.txtperconsumo.Text = IsNumFormat(par.IfNull(myReader1("PERC_ONERI_SIC_CON"), 0), "", "##,##0.0000")
                End If
                myReader1.Close()

                par.cmd.CommandText = ""

            End If
        End If
        Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Me.cmbvoce.Enabled = False
    End Sub

    Private Sub EliminaServizio()
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

        Try


            Dim sottraicanone As Decimal
            Dim sottraiconsumo As Decimal
            If CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastacanone"), TextBox).Text <> "" Then
                sottraicanone = CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastacanone"), TextBox).Text
            End If

            If CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastaconsumo"), TextBox).Text <> "" Then
                sottraiconsumo = CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastaconsumo"), TextBox).Text
            End If

            If txtannullo.Value = "1" Then

                If txtIdComponente.Value = "" Then
                    'Response.Write("<script>alert('Nessuna riga selezionata!')</script>") messaggio visibile in confermaannulloappalti del file .aspx
                    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                    txtAppareP.Value = "0"

                Else
                    If CType(Me.Page, Object).vIdAppalti = 0 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA APPALTO

                        sottraicanone = sottraicanone - Val((Replace(Replace(lstservizi(txtIdComponente0.Value).IMPORTO_CANONE, ".", ""), ",", ".")))
                        sottraiconsumo = sottraiconsumo - Val((Replace(Replace(lstservizi(txtIdComponente0.Value).IMPORTO_CONSUMO, ".", ""), ",", ".")))


                        lstservizi.RemoveAt(txtIdComponente0.Value)

                        Dim indice As Integer = 0
                        For Each griglia As Mario.VociServizi In lstservizi
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid3.DataSource = lstservizi
                        DataGrid3.DataBind()

                    Else
                        ''*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA FORNITORE
                        'If par.OracleConn.State = Data.ConnectionState.Open Then
                        '    Response.Write("IMPOSSIBILE VISUALIZZARE")
                        '    Exit Sub
                        'Else
                        '*******************RICHIAMO LA CONNESSIONECONNANP
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)

                        '*******************RICHIAMO LA TRANSAZIONE*********************
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        '‘par.cmd.Transaction = par.myTrans

                        'controllo che gli importi a consumo non siano legati a delle manutenzioni
                        par.cmd.CommandText = "select * from siscom_mi.manutenzioni where  stato>=1  and stato <=4 and id_appalto = " & CType(Me.Page, Object).vIdAppalti

                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Response.Write("<script>alert('Impossibile eliminare la voce perchè il servizio è già legato ad una manutenzione!');</script>")
                            myReader1.Close()
                            Exit Sub
                        End If
                        myReader1.Close()



                        'TROVA IMPORTO CORRISPONDENTE
                        par.cmd.CommandText = "select IMPORTO_CANONE, IMPORTO_CONSUMO from SISCOM_MI.APPALTI_VOCI_PF where SISCOM_MI.APPALTI_VOCI_PF.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " and SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE=" & txtIdComponente.Value
                        Dim myReadersomma As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReadersomma.Read Then
                            sottraicanone = sottraicanone - Val(par.VirgoleInPunti(myReadersomma("IMPORTO_CANONE")))
                            sottraiconsumo = sottraiconsumo - Val(par.VirgoleInPunti(myReadersomma("IMPORTO_CONSUMO")))
                        End If
                        myReadersomma.Close()

                        'ELIMINA APPALTO DA LOTTO
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " and ID_PF_VOCE = " & Me.txtIdComponente.Value

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                        par.cmd.Parameters.Clear()


                        DataGrid3.Rebind()

                        '*** EVENTI_FORNITORI
                        InserisciEvento(par.cmd, CType(Me.Page, Object).vIdAppalti, Session.Item("ID_OPERATORE"), 56, "Elimina voce servizio da appalto")

                    End If
                End If
                txtSelAppalti.Text = ""
                txtIdComponente.Value = ""
                txtIdComponente1.Value = ""

            End If



            CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastacanone"), TextBox).Text = IsNumFormat(sottraicanone, "", "##,##0.00")
            CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtastaconsumo"), TextBox).Text = IsNumFormat(sottraiconsumo, "", "##,##0.00")


            CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

            'End If



            'CALCOLA SOMMA
            somma()
            'CALCOLA PERCENTUALE
            percentuale()

            AggiornaVociServizi()
            CalcolaImpContrattuale()


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            If CType(Me.Page, Object).vIdAppalti <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"

        End Try
    End Sub

    Protected Sub btn_ChiudiAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiAppalti.Click
        Try


            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

            If CType(Page.FindControl("Tab_ImportiNP1").FindControl("canone"), HiddenField).Value <> "" Then
                CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtpercanone"), TextBox).Text = CType(Page.FindControl("Tab_ImportiNP1").FindControl("canone"), HiddenField).Value
            End If
            If CType(Page.FindControl("Tab_ImportiNP1").FindControl("consumo"), HiddenField).Value <> "" Then
                CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtperconsumo"), TextBox).Text = CType(Page.FindControl("Tab_ImportiNP1").FindControl("consumo"), HiddenField).Value
            End If

            txtSelAppalti.Text = ""
            txtIdComponente.Value = ""
            txtIdComponente1.Value = ""
            Me.txtOnerconsumo.Text = ""
            Me.txtperconsumo.Text = ""
            Me.perconsumo.Value = ""
            Me.txtivaconsumo.Text = ""
            '*******cancello i campi
            Me.cmbvoce.ClearSelection()
            AggiornaVociServizi()

            Me.txtimportocorpo.Text = ""
            Me.txtimportoconsumo.Text = ""
            Me.txtscontoconsumo.Text = ""
            Me.txtscontocorpo.Text = ""
            Me.perconsumo.Value = ""
            Me.txtpercanone.Text = ""
            Me.txtperconsumo.Text = ""

            CalcolaImpContrattuale()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
        End Try
    End Sub

    Protected Sub DataGrid3_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('Tab_VociNPl1_txtIdComponente').value='" & dataItem("ID_PF_VOCE_IMPORTO").Text & "';document.getElementById('Tab_VociNPl1_txtIdComponente0').value='" & dataItem("ID").Text & "';document.getElementById('Tab_VociNPl1_txtIdComponente1').value='" & dataItem("ID_SERVIZIO").Text & "';")
            e.Item.Attributes.Add("onDblClick", "document.getElementById('Tab_VociNPl1_btnApriAppalti').click();")
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
                dataItem("DeleteColumn").Enabled = False
                dataItem("modificaServizio").Enabled = False
                DataGrid3.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            End If
        End If
    End Sub

    Protected Sub DataGrid3_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid3.NeedDataSource
        Dim StringaSql As String

        Try

            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            ' RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            ' I NOMI DEI CAMPI DEVONO CORRISPONDERE CON QUELLI NELLA DATAGRID

            StringaSql = "SELECT ROWNUM AS ""ID"",'0' as id_lotto,'' as ID_SERVIZIO,'' as SERVIZIO ," _
                        & "SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE as id_pf_voce_importo," _
                        & "TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.IMPORTO_CANONE,'9G999G999G999G999G990D99')) AS ""IMPORTO_CANONE"", " _
                        & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.SCONTO_CANONE,'9G999G999G999G999G990D99'))AS ""SCONTO_CANONE"",SISCOM_MI.APPALTI_VOCI_PF.IVA_CANONE, " _
                        & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.IMPORTO_CONSUMO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUMO"", TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.SCONTO_CONSUMO,'9G999G999G999G999G990D99'))AS ""SCONTO_CONSUMO""," _
                        & " SISCOM_MI.APPALTI_VOCI_PF.IVA_CONSUMO,(PF_VOCI.CODICE ||' '|| PF_VOCI.DESCRIZIONE) AS DESCRIZIONE,'' AS DESC_PF,TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.ONERI_SICUREZZA_CONSUMO,'9G999G999G999G999G990D99')) AS ""ONERI_SICUREZZA_CONSUMO""" _
                        & " FROM SISCOM_MI.APPALTI_VOCI_PF,SISCOM_MI.PF_VOCI " _
                        & " WHERE SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID    AND SISCOM_MI.APPALTI_VOCI_PF.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti


            par.cmd.CommandText = StringaSql
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "APPALTI_VOCI_PF")



            ds.Dispose()
            AggiornaVociServizi()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
        End Try

    End Sub

    Private Sub DataGrid3_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles DataGrid3.ItemCommand
        Try
            Select Case e.CommandName
                Case "myEdit"
                    ApriServizio()
                Case "Delete"
                    EliminaServizio()
            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Public Sub BindGrid_Servizi()
        Dim StringaSql As String

        Try

            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            ' RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            ' I NOMI DEI CAMPI DEVONO CORRISPONDERE CON QUELLI NELLA DATAGRID

            StringaSql = "SELECT ROWNUM AS ""ID"",'0' as id_lotto,'' as ID_SERVIZIO,'' as SERVIZIO ," _
                        & "SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE as id_pf_voce_importo," _
                        & "TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.IMPORTO_CANONE,'9G999G999G999G999G990D99')) AS ""IMPORTO_CANONE"", " _
                        & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.SCONTO_CANONE,'9G999G999G999G999G990D99'))AS ""SCONTO_CANONE"",SISCOM_MI.APPALTI_VOCI_PF.IVA_CANONE, " _
                        & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.IMPORTO_CONSUMO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUMO"", TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.SCONTO_CONSUMO,'9G999G999G999G999G990D99'))AS ""SCONTO_CONSUMO""," _
                        & " SISCOM_MI.APPALTI_VOCI_PF.IVA_CONSUMO,(PF_VOCI.CODICE ||' '|| PF_VOCI.DESCRIZIONE) AS DESCRIZIONE,'' AS DESC_PF,TRIM(TO_CHAR(SISCOM_MI.APPALTI_VOCI_PF.ONERI_SICUREZZA_CONSUMO,'9G999G999G999G999G990D99')) AS ""ONERI_SICUREZZA_CONSUMO""" _
                        & " FROM SISCOM_MI.APPALTI_VOCI_PF,SISCOM_MI.PF_VOCI " _
                        & " WHERE SISCOM_MI.APPALTI_VOCI_PF.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID    AND SISCOM_MI.APPALTI_VOCI_PF.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti


            par.cmd.CommandText = StringaSql
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            DataGrid3.DataSource = dt
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "APPALTI_VOCI_PF")



            ds.Dispose()
            AggiornaVociServizi()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabVociNPl"
        End Try
    End Sub
End Class
