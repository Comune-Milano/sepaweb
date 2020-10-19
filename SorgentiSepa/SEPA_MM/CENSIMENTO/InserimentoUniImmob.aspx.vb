
Partial Class CENSIMENTO_InserimentoUniImmob
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public classetab As String = ""
    Public classetabSpRev As String
    Public tabdefault1 As String = ""
    Public tabdefault2 As String = ""
    Public tabdefault3 As String = ""
    Public tabdefault4 As String = ""
    Public tabdefault5 As String = ""
    Public tabdefault6 As String = ""
    Public tabdefault7 As String = ""
    Public tabdefault8 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)
            If vId <> 0 Then
                classetab = "tabbertab"
                If Session.Item("FL_SPESE_REVERSIBILI") = 1 Then
                    classetabSpRev = "tabbertab"
                Else
                    classetabSpRev = "tabbertabhide"
                End If
            Else
                classetab = "tabbertabhide"

                classetabSpRev = "tabbertabhide"
            End If
            Me.TxtInterno.Attributes.Add("onBlur", "javascript:valid(this,'special');valid(this,'quotes');")

            txtDataEsclusione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            If Not IsPostBack Then
                Response.Flush()
                Me.Connessione.Value = Request.QueryString("F")
                If (IsNumeric(Request.QueryString("ID")) = False Or Len(Request.QueryString("ID")) = 17) And Not String.IsNullOrEmpty(CStr(Request.QueryString("ID"))) Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT  ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & Request.QueryString("ID") & "'"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        vId = myReaderA(0)
                    End If
                    myReaderA.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()

                    If vId = 0 Then
                        Response.Write("<script>alert('Non è possibile visualizzare questa unità!');window.close();</script>")
                        'HttpContext.Current.Session.Remove("TRANSAZIONE")
                        'HttpContext.Current.Session.Remove("CONNESSIONE")
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                Else
                    If Request.QueryString("ID") = "" Then
                        vId = 0
                    Else
                        vId = Request.QueryString("ID")
                    End If
                End If


                If vId <> 0 Then
                    classetab = "tabbertab"
                    If Session.Item("FL_SPESE_REVERSIBILI") = 1 Then
                        classetabSpRev = "tabbertab"
                    Else
                        classetabSpRev = "tabbertabhide"
                    End If
                Else
                    classetab = "tabbertabhide"
                    classetabSpRev = "tabbertabhide"
                End If

                'Controllo modifica campi nel form
                Dim CTRL As Control
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    End If
                Next
                'FINE DEL CICLO

                If Session.Item("ID_CAF") = "6" Then
                    operatoreComune.Value = "1"
                Else
                    operatoreComune.Value = "0"
                End If

                If vId <> 0 Then
                    AttivaCampiPulsanti()
                    Me.Riempicampi()
                    ApriRicerca()
                Else
                    Me.Riempicampi()
                    If Request.QueryString("C") = "EdificiUI" Then
                        IndirizzoRiscaldFromEdificio()
                        DrlSc.Items.Clear()
                        scala()
                        TrovaFogSez()
                        ComplessoAssociato()
                        Me.caricaPertinenze()
                        LivelloPiano()
                    End If
                    'Apro la connessione che resterà valida per tutti i metodi delle sottofinestre e del salva
                    'Me.TxtCivicoKilo.Enabled = False
                    'Me.TxtDescrInd.Enabled = False
                    'Me.DrLTipoInd.Enabled = False
                    'Me.DrLComune.Enabled = False
                    'Me.TxtCap.Enabled = False
                    'Me.TxtLocalità.Enabled = False
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

                    'DisattivaCampiPulsanti()
                    'Me.salvainiziale()
                End If

            End If

            If vId = 0 Then
                Lbllocativo.Visible = False
            Else
                If DrLTipUnita.SelectedItem.Text = "Alloggio" Then
                    'Lbllocativo.Attributes.Add("onclick", "javascript:window.open('Locativo.aspx?ID=" & vId & "','Locativo','');")
                    Lbllocativo.Text = "<a href=" & Chr(34) & "Locativo.aspx?ID=" & vId & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Valore Locativo</a>"
                Else
                    Lbllocativo.Visible = False
                End If

            End If


            'max 16/04/2018
            If Session("PED2_SOLOLETTURA") = "1" Or Request.QueryString("LE") = "1" Then
                FrmSolaLettura()
                maxSLE.Value = "1"
            Else
                modUI.Value = VerificaUIModificabile()
                If modUI.Value = "0" Then
                    FrmSolaLettura()
                    maxSLE.Value = "1"
                End If
            End If
            maxPed2SL.Value = Session("PED2_SOLOLETTURA")

            'passaggio parametro per aprire il form in sola lettura, e anche i sottoform
            If Request.QueryString("X") = "1" Then
                Me.ImageButton1.Visible = False
                Me.chkPertinenze.Enabled = False
            End If
            If Not IsNothing(Request.QueryString("LE")) Then
                maxLE.Value = Request.QueryString("LE")
            End If

            '*** 17/06/2015 Nuova funzione sola lettura da rilievo ***
            If Request.QueryString("LET") = "1" Then
                SolaLetturaDaRilievo()
            End If
            '*** 17/06/2015 Fine Nuova funzione sola lettura da rilievo ***

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try


        'TxtDataAcquisiz.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        'TxtDataFineVal.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        ''*********************************************************************************************
        'TxtDataAcquisiz.Attributes.Add("onfocus", "javascript:selectText(this);")
        'TxtDataFineVal.Attributes.Add("onfocus", "javascript:selectText(this);")
        ''*********************************************************************************************
        'TxtDataAcquisiz.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        'TxtDataFineVal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        'Se vengono effettuate modifiche nei sotto-form questo manda il messaggio in casa di uscita senza salvataggio
        'If Session.Item("MODIFICASOTTOFORM") = 1 Then
        '    Me.txtModificato.Text = 1
        '    Session.Item("MODIFICASOTTOFORM") = 0

        'End If
        VerificaModificheSottoform()
        Me.txtindietro.Value = txtindietro.Value - 1
        If Session("ID_CAF") <> "6" Then
            NascondiCampi() 'Questa sub non fa più nulla ho lasciato il controllo perchè potrebbe servire a qualcosa...
        End If

        If Me.DrLDisponib.SelectedValue = "VEND" Then
            Session("PED2_SOLOLETTURA") = "1"
            maxPed2SL.Value = Session("PED2_SOLOLETTURA")
            FrmSolaLettura()
            visualizzaSM.Value = 0
            'Session.Add("SLE", 1)

        End If
        VeririfcaOSMI()
    End Sub
'max 20/04/2018
    Private Sub VeririfcaOSMI()
        Dim ApertaAdesso As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ApertaAdesso = True
            End If
            par.cmd.CommandText = "select descrizione from SISCOM_MI.TAB_ZONA_OSMI where id=(select id_osmi from siscom_mi.edifici where id=" & Me.DrLEdificio.SelectedValue.ToString & ")"
            Dim myReaderOSMI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderOSMI.Read Then
                lblOSMI.Visible = True
                lblOSMI.Text = "Z. OSMI: " & par.IfNull(myReaderOSMI(0), "")
            Else
                lblOSMI.Visible = False
                lblOSMI.Text = ""
            End If
            myReaderOSMI.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Function VerificaUIModificabile() As Integer
        'MAX 14/06/2019 Tutte le unità possono essere modificate (1379/2019)
        'max 20/06/2019 Tutte le unità possono essere modificate solo da chi è abilitato (1379/2019)
        If Session.Item("FL_MOD_MM_PATRIMONIO") = "1" Then
            VerificaUIModificabile = 1
        Else
            Dim newConn As Boolean = False
            VerificaUIModificabile = 0
            If operatoreComune.Value = "1" Then 'se operatore comunale, sempre modificabile
                VerificaUIModificabile = 1
            Else
                If DrLTipUnita.SelectedValue <> "AL" Then
                    If cmbPertinenza.Visible = True Then 'Se operatore MM e unità pertinenza, controllo UI principale
                        If IsNothing(cmbPertinenza.SelectedItem) = False Then
                            If par.IfEmpty(cmbPertinenza.SelectedItem.Text, " ") <> " " And chkPertinenze.Checked = True Then
                                Try
                                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                                        par.OracleConn.Open()
                                        par.SettaCommand(par)
                                        newConn = True
                                    End If
                                    par.cmd.CommandText = "select * from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & cmbPertinenza.SelectedItem.Text & "'"
                                    Dim lettoreP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If lettoreP.Read Then
                                        If lettoreP("cod_tipologia") <> "AL" Then
                                            ''Se operatore MM e unità principale NON AL, sempre modificabile
                                            Dim IDP As String = cmbPertinenza.SelectedItem.Value
                                            VerificaUIModificabile = 1
                                            DrLTipUnita.Enabled = False
                                            caricaPertinenze(True)
                                            Me.cmbPertinenza.SelectedValue = IDP
                                        Else
                                            ''Se operatore MM e unità principale AL, sempre NON modificabile
                                            VerificaUIModificabile = 0
                                        End If
                                    End If
                                    lettoreP.Close()
                                    If newConn = True Then
                                        par.OracleConn.Close()
                                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                    End If
                                Catch ex As Exception
                                    VerificaUIModificabile = 0
                                    par.OracleConn.Close()
                                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                End Try
                            Else
                                ''Se operatore MM e unità AL, sempre modificabile
                                VerificaUIModificabile = 1
                                DrLTipUnita.Enabled = False
                            End If
                        Else
                            ''Se operatore MM e unità AL, sempre modificabile
                            VerificaUIModificabile = 1
                            DrLTipUnita.Enabled = False
                        End If
                    Else
                        ''Se operatore MM e unità AL, sempre modificabile
                        VerificaUIModificabile = 1
                        DrLTipUnita.Enabled = False
                    End If
                Else
                    'Se operatore MM e unità AL, sempre NON modificabile
                    VerificaUIModificabile = 0
                End If
            End If
        End If

        
    End Function

    Public Sub VerificaModificheSottoform()
        'Se vengono effettuate modifiche nei sotto-form questo manda il messaggio in casa di uscita senza salvataggio
        If Session.Item("MODIFICASOTTOFORM") = 1 Then
            Me.txtModificato.Value = 1
            Session.Item("MODIFICASOTTOFORM") = 0
        End If

    End Sub

    Private Sub SolaLetturaDaRilievo()
        Try
            Me.ImgButSave.Visible = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                End If
            Next
            Me.ChkCatastali.Enabled = False

            CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).Items.Add(" ")
            btnFoto.Visible = False
            ImgButStampa.Visible = False
            imgAggNota.Visible = False
            ImgModifyNota.Visible = False
            Session.Add("SLE", 1)
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub FrmSolaLettura()
        Try
            Me.ImgButSave.Visible = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                End If
            Next
            Me.ChkCatastali.Enabled = False

            CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).Items.Add(" ")
            imgAggNota.Visible = False
            ImgModifyNota.Visible = False



            If Session.Item("FL_SPESE_REVERSIBILI") = 1 Then
                Me.ChkAscensori.Enabled = True
                Me.ChkRiscaldamento.Enabled = True
                Me.ChkSpGenerali.Enabled = True
                Me.ImgButSave.Visible = True
            End If
            Session.Add("SLE", 1)
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub NascondiCampi()
        'Me.Label9.Visible = False
        'Me.DrLEdificio.Visible = False
        'Me.Label40.Visible = False
        'CType(Tab_Catastali1.FindControl("TxtFoglio"),TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtNumero"),TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Visible = False
        'Me.TxtCodComun.Visible = False
        'CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).Visible = False
        'Me.CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Visible = False
        'Me.TxtSupCat.Visible = False
        'CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtCubatura), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).Visible = False
        'CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("DrlEsenzICI"),DropDownList).Visible = False
        'CType(Tab_Catastali1.FindControl("DrLInagibile"),DropDownList).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtMicrozCens"),TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtZonaCens"),TextBox).Visible = False
        'Me.TxtDataFineVal.Visible = False
        'CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Visible = False
        'Me.TxtDataAcquisiz.Visible = False
        'CType(Tab_Catastali1.FindControl("DrLStatoCatast"),DropDownList).Visible = False
        'CType(Tab_Catastali1.FindControl("TxtNote"),TextBox).Visible = False
        'Me.Label16.Visible = False
        'Me.Label20.Visible = False
        'Me.Label23.Visible = False
        'Me.Label14.Visible = False
        'Me.Label19.Visible = False
        'Me.Label22.Visible = False
        'Me.Label15.Visible = False
        'Me.Label21.Visible = False
        'Me.Label24.Visible = False
        'Me.Label17.Visible = False
        'Me.Label26.Visible = False
        'Me.Label10.Visible = False
        'Me.Label18.Visible = False
        'Me.Label27.Visible = False
        'Me.Label7.Visible = False
        'Me.Label25.Visible = False
        'Me.Label32.Visible = False
        'Me.Label33.Visible = False
        'Me.Label39.Visible = False
        'Me.Label35.Visible = False
        'Me.Label36.Visible = False
        'Me.Label30.Visible = False
        'Me.Label28.Visible = False
        'Me.Label31.Visible = False
        'Me.Label37.Visible = False
        'Me.ImgButStampa.Visible = False
    End Sub
    Private Sub Riempicampi()
        Dim ds As New Data.DataSet
        Try
            '23/02/2009 MODIFICATI TUTTI I METODI DI CARICAMENTO OGGETTI QUALI COMBO CON MYREADER
            CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).Items.Add(" ")
            CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).Items.Add("SI")
            CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).Items.Add("NO")

            CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).Items.Add(" ")
            CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).Items.Add("SI")
            CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).Items.Add("NO")

            CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).Items.Add(" ")
            CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).Items.Add("SI")
            CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).Items.Add("NO")

            DrlAscensore.Items.Add(" ")
            DrlAscensore.Items.Add(New ListItem("SI", 1))
            DrlAscensore.Items.Add(New ListItem("NO", 0))

            DrlHandicap.Items.Add(" ")
            DrlHandicap.Items.Add(New ListItem("SI", 1))
            DrlHandicap.Items.Add(New ListItem("NO", 0))
            'Apro la CONNESSIONE  con il DB PER RIEMPIRE I CAMPI (Combo, textbox...ecc...)
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'GESTIONE SOTTOSOGLIA
            lblSoglia.Text = ""
            lblSoglia.BackColor = Drawing.Color.Transparent
            par.cmd.CommandText = "SELECT NVL(VALORE,10000) FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='SOGLIA ABITABILITA'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim soglia As Integer = 0
            If lettore.Read Then
                soglia = par.IfNull(lettore(0), 10000)
            End If
            lettore.Close()
            par.cmd.CommandText = "SELECT MAX(VALORE) FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & vId & " AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA'"
            lettore = par.cmd.ExecuteReader
            Dim superficie As Decimal = 0
            If lettore.Read Then
                superficie = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()
            If superficie > 0 And superficie <= soglia Then
                lblSoglia.Text = "ALLOGGIO SOTTOSOGLIA"
                lblSoglia.BackColor = Drawing.Color.Yellow
            Else
                lblSoglia.Text = ""
                lblSoglia.BackColor = Drawing.Color.Transparent
            End If

            'CARICO COMBO COMPLESSI
            If Session("PED2_ESTERNA") = "1" Then
                'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
            Else
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "

            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                'cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            myReader1.Close()
            cmbComplesso.SelectedValue = "-1"

            'CARICO COMBO EDIFICI
            'DrLEdificio.Items.Add(New ListItem(" ", -1))
            'If gest > 0 Then
            '    par.cmd.CommandText = "SELECT distinct id,denominazione FROM SISCOM_MI.edifici  where substr(id,1,1)= " & gest & "order by denominazione asc"

            'Else
            '    par.cmd.CommandText = "SELECT distinct id,denominazione FROM SISCOM_MI.edifici order by denominazione asc"
            'End If
            'myReader1 = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            'End While
            'myReader1.Close()
            'DrLEdificio.SelectedValue = "-1"
            'da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI", par.OracleConn)
            'da.Fill(ds)
            'DrLEdificio.Items.Add(New ListItem(" ", -1))
            'DrLEdificio.DataSource = ds
            'DrLEdificio.DataTextField = "DENOMINAZIONE"
            'DrLEdificio.DataValueField = "ID"
            'DrLEdificio.DataBind()
            'Riempio l'oggetto da con quello che restituisce la select
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI"
            myReader1 = par.cmd.ExecuteReader
            DrLTipUnita.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLTipUnita.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            DrLTipUnita.Text = "-1"
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO"
            myReader1 = par.cmd.ExecuteReader
            DrLTipoLivPiano.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLTipoLivPiano.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            DrLTipoLivPiano.Text = "-1"

            myReader1.Close()

            'Ripulisco gli oggetti ds e da per riutilizzarli sulle successive combo

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.STATO_CONSERVATIVO_LG_392_78"
            myReader1 = par.cmd.ExecuteReader

            DrLStatoCons.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLStatoCons.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            DrLStatoCons.SelectedValue = "NORMA"
            myReader1.Close()

            'ANTONELLO MODIFY 05/07/2013 - AGGIUNTA FILIALE
            par.cmd.CommandText = "SELECT DISTINCT TAB_FILIALI.ID, (NOME || ' - ' || TIPOLOGIA_STRUTTURA_ALER.DESCRIZIONE) AS FILIALE FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.TIPOLOGIA_STRUTTURA_ALER WHERE TIPOLOGIA_STRUTTURA_ALER.ID(+) = TAB_FILIALI.ID_TIPO_ST order by FILIALE asc"
            myReader1 = par.cmd.ExecuteReader
            ddlFiliale.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                ddlFiliale.Items.Add(New ListItem(par.IfNull(myReader1("FILIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_DISPONIBILITA"
            myReader1 = par.cmd.ExecuteReader
            DrLDisponib.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLDisponib.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            DrLDisponib.SelectedValue = "INDEF"
            myReader1.Close()

            'Ripulisco gli oggetti ds e da per riutilizzarli sulle successive combo
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PROGRAMMAZIONE_INTERVENTI"
            myReader1 = par.cmd.ExecuteReader
            DrLStatoCens.Items.Add(New ListItem(" ", "NULL"))
            While myReader1.Read
                DrLStatoCens.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            DrLStatoCens.SelectedValue = "NULL"
            myReader1.Close()

            'Destinazione d'uso aggiunta peppe 14/12/2009
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DESTINAZIONI_USO_UI"
            myReader1 = par.cmd.ExecuteReader
            Me.DrlDestUso.Items.Add(New ListItem("", -1))
            While myReader1.Read
                DrlDestUso.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            DrlDestUso.SelectedValue = "-1"
            myReader1.Close()

            'Ripulisco gli oggetti ds e da per riutilizzarli sulle successive combo

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_CATASTO"
            myReader1 = par.cmd.ExecuteReader
            CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue = "-1"
            myReader1.Close()

            'Ripulisco gli oggetti ds e da per riutilizzarli sulle successive combo
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CATEGORIA_CATASTALE"
            myReader1 = par.cmd.ExecuteReader
            'DrLCategoria.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).SelectedValue = "000"
            myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CLASSE_CATASTALE"
            myReader1 = par.cmd.ExecuteReader
            CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).SelectedValue = "00"
            myReader1.Close()

            'Ripulisco gli oggetti ds e da per riutilizzarli sulle successive combo
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.STATO_CATASTALE"
            myReader1 = par.cmd.ExecuteReader
            CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).Text = "-1"
            myReader1.Close()
            '****************PEPPE MODIFY 05/09/2010
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO "

            DrLTipoInd.Items.Add(New ListItem(" ", -1))
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                DrLTipoInd.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT COMU_COD, COMU_DESCR FROM sepa.COMUNI order by comu_descr asc"
            myReader1 = par.cmd.ExecuteReader()
            DrLComune.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLComune.Items.Add(New ListItem(par.IfNull(myReader1("COMU_DESCR"), " "), par.IfNull(myReader1("COMU_COD"), -1)))
            End While
            Me.DrLComune.SelectedValue = "F205"
            myReader1.Close()

            par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.DESTINAZIONI_USO_RL_UI ORDER BY ID ASC"
            par.caricaComboBox(par.cmd.CommandText, ddlDestUsoRL, "ID", "DESCRIZIONE", True)

            par.cmd.CommandText = "SELECT MOD_PED2_SOLO_LETTURA FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Session.Item("PED2_SOLOLETTURA") = par.IfNull(myReader1("MOD_PED2_SOLO_LETTURA"), "0")
                If par.IfNull(myReader1("MOD_PED2_SOLO_LETTURA"), "0") = "0" Then
                    Session("SLE") = "0"
                End If
            End If
            myReader1.Close()


            Me.CaricaEdifici()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            AlloggioEscluso()



        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try



        Dim IDED As String = Request.QueryString("IDED")
        If IDED > 0 Then
            Me.DrLEdificio.SelectedValue = IDED
            DrlSc.Items.Clear()
            scala()
            LivelloPiano()
            'TrovaFogSez()
            Me.DrLEdificio.Enabled = False
            ComplessoAssociato()
            Me.cmbComplesso.Enabled = False
            TrovaFogSez()
        End If



    End Sub
    Public Property vIdIndirizzo() As Long
        Get
            If Not (ViewState("par_vIdIndirizzo") Is Nothing) Then
                Return CLng(ViewState("par_vIdIndirizzo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdIndirizzo") = value
        End Set

    End Property

    Private Sub VisualizzaAscHandicap(ByVal idunita As Long)

        '************ 04/09/2012 Aggiunta visualizz. accessibilità portatori di Handicap
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO WHERE ID_UNITA=" & idunita
        Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderInd.Read Then
            DrlHandicap.SelectedValue = par.IfNull(myReaderInd("HANDICAP"), 0)
        Else
            DrlHandicap.Items.Add(New ListItem("---", -1))
            DrlHandicap.SelectedValue = -1
        End If
        myReaderInd.Close()
        '************ 04/09/2012 FINE Aggiunta visualizz. accessibilità portatori di Handicap


        '************ 04/09/2012 Aggiunta visualizz. presenza ascensore
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.IMPIANTI_SCALE WHERE ID_SCALA =" & DrlSc.SelectedValue & " AND ID_IMPIANTO IN (select ID from SISCOM_MI.impianti where cod_tipologia = 'SO')"
        myReaderInd = par.cmd.ExecuteReader()
        If myReaderInd.Read Then
            DrlAscensore.SelectedValue = 1
        Else
            DrlAscensore.SelectedValue = 0
        End If
        myReaderInd.Close()
        '************ 04/09/2012 FINE Aggiunta visualizz. presenza ascensore


    End Sub
    Private Sub ApriRicerca()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dt As New Data.DataTable
        Dim scriptblock As String

        If vId <> -1 Then
            Try
                Dim STRAPPOGGIO As String
                If Session.Item("CONT_DISDETTE") = "1" Then
                    'ImgBtnVerStatManut.Visible = True
                    visualizzaSM.value = "1"
                Else
                    'ImgBtnVerStatManut.Visible = False
                    visualizzaSM.value = "0"

                End If
                'SE SI PROVIENE DA UNA RICERCA DOPO AVER RIEMPITO I CAMPI SETTO LA MIA CONNESSIONE CHE RESTERà VALIDA PER TUTTA LA DURATA DI APERTURA DEL FORM
                If Request.QueryString("LE") <> "1" And Session("PED2_SOLOLETTURA") <> "1" Then
                    STRAPPOGGIO = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                Else
                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId
                    'par.OracleConn.Close()
                    ApriFrmWithDBLock()
                    Exit Sub
                End If
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE
                If STRAPPOGGIO <> "" Then
                    par.cmd.CommandText = STRAPPOGGIO
                End If

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)

                'LblDatiContratt.Text = "<a href='InserimentoComplessi.aspx?ID=200000029&SLE=1' target='_blank'>Dati Contrattuali</a>"
                'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE ID_UNITA =" & vId & " AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO order by rapporti_utenza.id desc"
                par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE ID_UNITA =" & vId & " AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO order by rapporti_utenza.DATA_DECORRENZA desc"
                Dim myReaderPepp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderPepp.Read Then
                    LblDatiContratt.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?ID=" & myReaderPepp("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">Dati Contrattuali</a>"
                Else
                    'LblDatiContratt.Text = "<a href='DatiContratto.aspx?ID=" & vId & "&UI=" & par.IfNull(dt.Rows(0).Item("COD_UNITA_IMMOBILIARE"), "") & "' target='_blank'>Dati Contrattuali</a>"
                    LblDatiContratt.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "alert('Nessun Contratto stipulato su questa unità!');" & Chr(34) & ">Dati Contrattuali</a>"
                End If
                myReaderPepp.Close()

                If dt.Rows.Count > 0 Then
                    HFAscensori.Value = dt.Rows(0).Item("P_ASCENSORE")
                    HFRiscaldamento.Value = dt.Rows(0).Item("P_RISCALDAMENTO")
                    HFSpGenerali.Value = dt.Rows(0).Item("P_SERVIZI_GENERALI")

                    If HFAscensori.Value = 1 Then
                        Me.ChkAscensori.Checked = True
                    End If
                    If HFRiscaldamento.Value = 1 Then
                        Me.ChkRiscaldamento.Checked = True
                    End If
                    If HFSpGenerali.Value = 1 Then
                        Me.ChkSpGenerali.Checked = True
                    End If
                    Me.DrLTipUnita.SelectedValue = dt.Rows(0).Item("COD_TIPOLOGIA")
                    If DrLTipUnita.SelectedValue <> "AL" Then
                        lblSoglia.Text = ""
                        lblSoglia.BackColor = Drawing.Color.Transparent
                    End If
                    Me.DrLEdificio.SelectedValue = dt.Rows(0).Item("ID_EDIFICIO")
                    vIdEdificio = dt.Rows(0).Item("ID_EDIFICIO")
                    vIdIndirizzo = dt.Rows(0).Item("ID_INDIRIZZO")
                    LivelloPiano()
                    scala()
                    'CONTROLLO CHE LA DRLSC SIA PIENA, PERCHè SE COMPOSTA DA UN SOLO ELEMENTO è QUELLO VUOTO PARI A "NESSUNA"
                    If Me.DrlSc.Items.Count > 1 Then
                        Me.DrlSc.SelectedValue = (par.IfNull((dt.Rows(0).Item("id_scala").ToString), "-1"))
                        ' Me.DrlSc.Enabled = False
                    End If
                    If par.IfNull(dt.Rows(0).Item("ID_UNITA_PRINCIPALE"), 0) <> 0 Then
                        caricaPertinenze()
                        Me.chkPertinenze.Checked = True
                        Me.cmbPertinenza.Visible = True
                        Me.cmbPertinenza.SelectedValue = dt.Rows(0).Item("ID_UNITA_PRINCIPALE")
                    End If

                    Me.TxtInterno.Text = par.IfNull(dt.Rows(0).Item("INTERNO"), "")
                    Me.txtCodUnitImm.Text = par.IfNull(dt.Rows(0).Item("COD_UNITA_IMMOBILIARE"), "")
                    Me.DrLTipoLivPiano.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPO_LIVELLO_PIANO"), -1)


                    Me.DrLStatoCons.SelectedValue = (par.IfNull(dt.Rows(0).Item("COD_STATO_CONS_LG_392_78"), -1))

                    Me.DrLStatoCons.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_STATO_CONS_LG_392_78"), -1)
                    Me.DrLDisponib.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPO_DISPONIBILITA"), -1)

                    Me.DrLStatoCens.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_PRG_EVENTI"), "NULL")

                    Me.DrlDestUso.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_DESTINAZIONE_USO"), -1)
                    '*************** REGIONE ***************
                    Me.ddlDestUsoRL.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_DESTINAZIONE_USO_RL"), -1)
                    Me.ddlAlloggioEscluso.SelectedValue = par.IfNull(dt.Rows(0).Item("FL_ALLOGGIO_ESCLUSO"), "0")
                    Me.txtDataEsclusione.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_ESCLUSIONE"), ""))
                    Me.txtNrProvvedimentoEsclusione.Text = par.IfNull(dt.Rows(0).Item("NR_PROVV_ESCLUSIONE"), "")
                    AlloggioEscluso()
                    par.CaricaStoricoNote(vId, dataGridNote, "2")
                    '*************** REGIONE ***************

                    If Me.DrLDisponib.SelectedValue <> "LIBE" Then
                        Me.DrlDestUso.Enabled = False
                    Else
                        Me.DrlDestUso.Enabled = True
                    End If
                    ''****GESTIONE DELLA DISPONIBILITA DELL'UNITA IMMOBILIARE-SE NON DEFINIBILE VIENE ATTIVATA

                    If Me.DrLDisponib.SelectedValue = "INDEF" Then
                        Me.DrLDisponib.Enabled = True
                        If Not IsNothing(Me.DrLDisponib.Items.FindByValue("OCCU")) Then Me.DrLDisponib.Items.FindByValue("OCCU").Enabled = False
                        '21/01/2012 non esiste più
                        'Me.DrLDisponib.Items.FindByValue("LOCA").Enabled = False
                    Else
                        Me.DrLDisponib.Enabled = False
                    End If

                        If Me.DrlDestUso.SelectedValue = "2" Then
                            VisibilitaCanone()
                            par.cmd.CommandText = "SELECT CANONE FROM SISCOM_MI.CANONI_UI WHERE ID =" & vId
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                            If myReader.Read Then
                                Me.TxtCanoneUI.Text = myReader("CANONE")
                            End If

                        End If
                        idCatasto = par.IfNull(dt.Rows(0).Item("ID_CATASTALE"), 0)

                        '*********PEPPE MODIFY 15/09/2010 PER INDIRIZZO DELL'UNITA' EDITABILE
                        par.cmd.CommandText = "SELECT DESCRIZIONE ,CIVICO, CAP, LOCALITA, INDIRIZZI.COD_COMUNE FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI where INDIRIZZI.ID = UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID =" & vId
                        'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderInd.Read Then
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                            par.cmd.CommandText = "SELECT COD FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO WHERE DESCRIZIONE =  '" & RicavaVial(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "'"
                            myReader = par.cmd.ExecuteReader
                            If myReader.Read Then
                                Me.DrLTipoInd.SelectedValue = myReader(0)
                            End If
                            myReader.Close()
                            Me.TxtDescrInd.Text = RicavaDescVia(par.IfNull(myReaderInd("DESCRIZIONE"), ""))
                            Me.TxtCivicoKilo.Text = par.IfNull(myReaderInd("CIVICO"), "")
                            Me.TxtCap.Text = par.IfNull(myReaderInd("CAP"), "")
                            Me.TxtLocalità.Text = par.IfNull(myReaderInd("LOCALITA"), "")
                            Me.DrLComune.SelectedValue = par.IfNull(myReaderInd("COD_COMUNE"), "F205")
                            'Me.lblIndirizzo.Text = myReaderInd("DESCRIZIONE")
                        End If
                        myReaderInd.Close()

                        VisualizzaAscHandicap(vId)
                        'Aggiunta Visualizzazione della tipologia di impianto di riscaldamento
                        par.cmd.CommandText = "SELECT TIPOLOGIA_IMP_RISCALDAMENTO.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO,SISCOM_MI.EDIFICI WHERE EDIFICI.COD_TIPOLOGIA_IMP_RISCALD = TIPOLOGIA_IMP_RISCALDAMENTO.COD AND EDIFICI.ID =" & Me.DrLEdificio.SelectedValue.ToString
                        myReaderInd = par.cmd.ExecuteReader()
                        If myReaderInd.Read Then
                            Me.lblTipoRiscald.Text = myReaderInd("DESCRIZIONE")
                        End If
                        myReaderInd.Close()

                        dt = New Data.DataTable
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT * FROM SISCOM_MI.IDENTIFICATIVI_CATASTALI WHERE ID = " & idCatasto, par.OracleConn)
                        da.Fill(dt)

                        If dt.Rows.Count > 0 Then

                            Me.ChkCatastali.Checked = True
                            Me.AttivaCampiCatastali()

                            CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Text = par.IfNull(dt.Rows(0).Item("SEZIONE").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text = par.IfNull(dt.Rows(0).Item("FOGLIO").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text = par.IfNull(dt.Rows(0).Item("NUMERO").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text = par.IfNull(dt.Rows(0).Item("SUB").ToString, "")

                            CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPOLOGIA_CATASTO"), -1)


                            CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Text = par.IfNull(dt.Rows(0).Item("RENDITA").ToString, "")

                            CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).SelectedValue = par.IfNull(dt.Rows(0).Item("COD_CATEGORIA_CATASTALE"), -1)


                            CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).SelectedValue = par.IfNull(dt.Rows(0).Item("COD_CLASSE_CATASTALE"), -1)


                            CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).SelectedValue = par.IfNull(dt.Rows(0).Item("COD_STATO_CATASTALE"), -1)


                            CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text = par.IfNull(dt.Rows(0).Item("SUPERFICIE_MQ").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtCubatura"), TextBox).Text = par.IfNull(dt.Rows(0).Item("CUBATURA").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Text = par.IfNull(dt.Rows(0).Item("NUM_VANI").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text = par.IfNull(dt.Rows(0).Item("SUPERFICIE_CATASTALE").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Text = par.IfNull(dt.Rows(0).Item("RENDITA_STORICA").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Text = par.IfNull(dt.Rows(0).Item("REDDITO_DOMINICALE").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Text = par.IfNull(dt.Rows(0).Item("VALORE_IMPONIBILE").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Text = par.IfNull(dt.Rows(0).Item("REDDITO_AGRARIO").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Text = par.IfNull(dt.Rows(0).Item("VALORE_BILANCIO").ToString, "")
                            CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text = par.FormattaData(dt.Rows(0).Item("DATA_ACQUISIZIONE").ToString)
                            CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text = par.FormattaData(dt.Rows(0).Item("DATA_FINE_VALIDITA").ToString)
                            CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Text = dt.Rows(0).Item("DITTA").ToString
                            CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Text = dt.Rows(0).Item("NUM_PARTITA").ToString
                            CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text = dt.Rows(0).Item("PERC_POSSESSO").ToString
                            CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text = dt.Rows(0).Item("COD_COMUNE").ToString
                            CType(Tab_Catastali1.FindControl("TxtMicrozCens"), TextBox).Text = dt.Rows(0).Item("MICROZONA_CENSUARIA").ToString
                            CType(Tab_Catastali1.FindControl("TxtZonaCens"), TextBox).Text = dt.Rows(0).Item("ZONA_CENSUARIA").ToString
                            CType(Tab_Catastali1.FindControl("TxtNote"), TextBox).Text = dt.Rows(0).Item("NOTE").ToString

                            CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).SelectedValue = RitornaSiNoDaNum(par.IfNull((dt.Rows(0).Item("IMMOBILE_STORICO")), ""))

                            CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).SelectedValue = RitornaSiNoDaNum(par.IfNull((dt.Rows(0).Item("INAGIBILE")), ""))

                            CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).SelectedValue = RitornaSiNoDaNum(par.IfNull((dt.Rows(0).Item("ESENTE_ICI")), ""))
                        Else
                            Me.ChkCatastali.Checked = False
                            DisattivaCampiCatastali()

                        End If
                        dt = New Data.DataTable
                        da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID = " & vIdEdificio, par.OracleConn)
                        da.Fill(dt)
                        Me.cmbComplesso.SelectedValue = dt.Rows(0).Item("ID_COMPLESSO").ToString

                        Me.DrLEdificio.Enabled = False
                        Me.cmbComplesso.Enabled = False

                    'ANTONELLO MODIFY 05/07/2013 - AGGIUNTA FILIALE
                    par.cmd.CommandText = "SELECT ID_FILIALE FROM SISCOM_MI.FILIALI_UI WHERE ID_UI = " & vId & " AND FINE_VALIDITA = '30000101'"
                    Dim MyReaderFiliale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If MyReaderFiliale.Read Then
                        ddlFiliale.SelectedValue = par.IfNull(MyReaderFiliale("ID_FILIALE"), "-1")
                        idFiliale.Value = par.IfNull(MyReaderFiliale("ID_FILIALE"), "-1")
                    End If
                    MyReaderFiliale.Close()
                    End If
                Dim testoTabella As String
                testoTabella = ""
                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                            & "<tr>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.UNITA</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"
                par.cmd.CommandText = "SELECT cod_unita_immobiliare FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_UNITA_PRINCIPALE=" & vId
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader2.Read

                    testoTabella = testoTabella _
                                & "<tr>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&PERT=1&ID=" & par.IfNull(myReader2("cod_unita_immobiliare"), "") & "','Pertin" & myReader2("cod_unita_immobiliare") & "','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & par.IfNull(myReader2("cod_unita_immobiliare"), "") & "</a></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "</td>" _
                                & "</tr>"
                Loop
                myReader2.Close()
                LblPertinenze.Text = testoTabella & "</table>"
                'Controllo se il campo Dest USO RL può essere abilitato o meno
                If Session.Item("LIVELLO") = "1" Or Session.Item("RESPONSABILE") = "1" Then
                    ddlDestUsoRL.Enabled = True
                Else
                    ddlDestUsoRL.Enabled = False
                End If

                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                Try
                    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Then
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                    Else
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                    End If
                Catch ex As Exception
                    Me.LblErrore.Visible = True
                    LblErrore.Text = "ATTENZIONE!Verificare il percorso delle foto e delle planimetrie!"
                End Try
                '*********************FINE CONTROLLO PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********


                'Apro una nuova transazione
                Session.Item("LAVORAZIONE") = "1"
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    par.OracleConn.Close()
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Unità Immobiliare aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                    & "</script>"
                    visualizzaSM.Value = 0
                    ApriFrmWithDBLock()
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                    End If
                Else
                    par.OracleConn.Close()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End If

            Catch ex As Exception
                Me.ImgButSave.Visible = False
                par.OracleConn.Close()
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try
        End If

    End Sub

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgButDimens.Click
    '    Response.Write("<script>window.open('AdDimens.aspx?ID=" & vId & ",&Pas=UI','DIMENSIONI', 'resizable=no, width=520, height=220');</script>")

    'End Sub

    'Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgButVarConf.Click
    '    Response.Write("<script>window.open('AdVarConf.aspx?ID=" & vId & ",&Pas=UI','VARIAZIONI', 'resizable=no, width=530, height=220');</script>")
    '    'Me.txtindietro.Text = txtindietro.Text - 1

    'End Sub


    'Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgButAdegNorm.Click
    '    Response.Write("<script>window.open('AdNormativo.aspx?ID=" & vId & ",&Pas=UI','ADnORMA', 'resizable=no, width=560, height=250,titlebar=no,toolbar=no,statusbar=no');</script>")
    '    'Me.txtindietro.Text = txtindietro.Text - 1

    'End Sub

    Protected Sub ImButEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImButEsci.Click
        Try


            'If Request.QueryString("X") = "1" Then
            '    If Session.Item("LE") = "1" Then
            '        Session.Remove("LE")
            '    End If
            '    Response.Write("<script language='javascript'> { self.close() }</script>")

            'Else
            If txtModificato.Value <> "111" Then
                If Request.QueryString("LE") <> "1" And Session("PED2_SOLOLETTURA") <> "1" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)


                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")

                    par.OracleConn.Close()
                End If
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = 0

                If Session.Item("SLE") = "1" Then
                    Session.Remove("SLE")
                End If

                If Request.QueryString("X") = "1" Then
                    Response.Write("<script language='javascript'> { self.close() }</script>")
                Else
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                End If

            Else
                txtModificato.Value = "1"
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            End If
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Public Property vId() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Public Property idCatasto() As Long
        Get
            If Not (ViewState("par_idCatasto") Is Nothing) Then
                Return CLng(ViewState("par_idCatasto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idCatasto") = value
        End Set

    End Property
    Public Property vIdEdificio() As Long
        Get
            If Not (ViewState("par_lIdEdificio") Is Nothing) Then
                Return CLng(ViewState("par_lIdEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdEdificio") = value
        End Set
    End Property
    Private Sub IndirizzoRiscaldFromEdificio()
        Try
            If Me.DrLEdificio.SelectedValue <> "-1" Then
                Dim ApertaAdesso As Boolean = False
                'Questo metodo vuole una connessione aperte dal chiamante in caso contrario ne apre una nuova e la chiude immediatamente
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    ApertaAdesso = True
                End If
                par.cmd.CommandText = "SELECT DESCRIZIONE ,CIVICO, CAP, LOCALITA, INDIRIZZI.COD_COMUNE FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI where EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND EDIFICI.ID =" & Me.DrLEdificio.SelectedValue.ToString
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderInd.Read Then
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "SELECT COD FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO WHERE DESCRIZIONE =  '" & RicavaVial(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "'"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        Me.DrLTipoInd.SelectedValue = myReader(0)
                    End If
                    myReader.Close()
                    Me.TxtDescrInd.Text = RicavaDescVia(par.IfNull(myReaderInd("DESCRIZIONE"), ""))
                    Me.TxtCivicoKilo.Text = par.IfNull(myReaderInd("CIVICO"), "")
                    Me.TxtCap.Text = par.IfNull(myReaderInd("CAP"), "")
                    Me.TxtLocalità.Text = par.IfNull(myReaderInd("LOCALITA"), "")

                    'Me.lblIndirizzo.Text = myReaderInd("DESCRIZIONE")
                End If
                myReaderInd.Close()

                'Aggiunta Visualizzazione della tipologia di impianto di riscaldamento
                par.cmd.CommandText = "SELECT TIPOLOGIA_IMP_RISCALDAMENTO.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO,SISCOM_MI.EDIFICI WHERE EDIFICI.COD_TIPOLOGIA_IMP_RISCALD = TIPOLOGIA_IMP_RISCALDAMENTO.COD AND EDIFICI.ID =" & Me.DrLEdificio.SelectedValue.ToString
                myReaderInd = par.cmd.ExecuteReader()
                If myReaderInd.Read Then
                    Me.lblTipoRiscald.Text = myReaderInd("DESCRIZIONE")
                End If
                myReaderInd.Close()


                If ApertaAdesso = True Then
                    par.OracleConn.Close()
                End If
            Else
                Me.TxtDescrInd.Text = ""
                Me.TxtCivicoKilo.Text = ""
                Me.DrLTipoInd.SelectedValue = "-1"
                Me.lblTipoRiscald.Text = ""
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged
        If Me.DrLEdificio.SelectedValue <> -1 Then
            IndirizzoRiscaldFromEdificio()
            DrlSc.Items.Clear()
            scala()
            TrovaFogSez()
            ComplessoAssociato()
            Me.caricaPertinenze()
            LivelloPiano()
        Else
            CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Text = ""
            CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text = ""
            CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text = ""
            CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text = ""
            Me.DrlSc.Items.Clear()
            Me.cmbPertinenza.Items.Clear()
            LivelloPiano()
            Me.TxtDescrInd.Text = ""
            Me.DrLTipoInd.SelectedValue = -1
            Me.TxtCivicoKilo.Text = ""
            Me.DrLComune.SelectedValue = -1
        End If
    End Sub
    Private Sub ComplessoAssociato()
        Try
            Dim ApertAdesso As Boolean = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                ApertAdesso = True
                par.OracleConn.Open()
                par.SettaCommand(par)
                'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
            End If
            par.cmd.CommandText = "SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID =" & Me.DrLEdificio.SelectedValue.ToString

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO").ToString, "-1")
            End If
            myReader1.Close()

            If ApertAdesso = True Then
                par.OracleConn.Close()

            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message

        End Try
    End Sub
    Private Sub scala()
        Try
            Dim ApertaAdesso As Boolean = False

            'Questo metodo vuole una connessione aperte dal chiamante in caso contrario ne apre una nuova e la chiude immediatamente
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ApertaAdesso = True
            End If

            par.cmd.CommandText = "SELECT  ID, DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI where id_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Me.DrlSc.Items.Add(New ListItem("NON DEFINIBILE", -1))
            While myReader1.Read
                DrlSc.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " ").ToString.ToUpper, par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            If ApertaAdesso = True Then
                par.OracleConn.Close()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub TrovaFogSez()
        Try
            Dim APERTORA As Boolean = False
            If Me.DrLEdificio.SelectedValue <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    APERTORA = True
                End If
                par.cmd.CommandText = "SELECT  COD_COMUNE FROM SISCOM_MI.EDIFICI WHERE ID =" & Me.DrLEdificio.SelectedValue.ToString
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    'Me.CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Text = myReader1("SEZIONE").ToString
                    'CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text = myReader1("FOGLIO").ToString
                    'CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text = myReader1("NUMERO").ToString
                    CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text = myReader1("COD_COMUNE").ToString
                End If
                myReader1.Close()
            End If

            If APERTORA = True Then
                par.OracleConn.Close()
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub ImgButSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgButSave.Click
        Try
            If vId <> "0" Then
                If Me.txtUpdate.Value <> 1 Then
                    'If Session.Item("LIVELLO") <> 1 Then
                    If Session.Item("SLE") = 1 Then
                        If Session.Item("FL_SPESE_REVERSIBILI") = 1 Then
                            UpdateSpRev()
                            txtModificato.Value = "0"
                        End If
                        Exit Sub
                    End If
                    'End If

                    Me.update()
                Else
                    Response.Write("<SCRIPT>alert('Non sono state apportate modifiche!');</SCRIPT>")

                End If

            Else
                If txtUpdate.Value <> 1 Then
                    Me.salva()
                Else
                    Response.Write("<SCRIPT>alert('Nessun dato è stato salvato!');</SCRIPT>")

                End If
            End If
            'Me.txtindietro.Text = CInt(txtindietro.Text) - 1
            txtModificato.Value = "0"
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub salva()
        Dim myreader As Oracle.DataAccess.Client.OracleDataReader
        Dim nextval As String = ""
        Dim idUnitaImm As Integer
        Dim CodEdif As String = ""
        Dim CodUniIm As String = ""
        Dim IdScala As String = ""
        Dim IdCatasto As String = ""
        Dim IdIndirizzo As String = ""
        Dim DescScla As String = ""


        Try
            'Richiamo la connessione
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            Else
                par.SettaCommand(par)
            End If

            If Me.DrLEdificio.SelectedValue <> "-1" Then

                If CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text = "dd/mm/YYYY" Then
                    CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text = ""

                End If
                If CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text = "dd/mm/YYYY" Then
                    CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text = ""
                End If
                If Me.DrlSc.Items.Count > 1 Then
                    If Me.DrlSc.SelectedValue = -1 Then
                        DescScla = "000"
                    Else
                        DescScla = Me.DrlSc.SelectedItem.Text.ToString
                    End If
                End If
                '                If Me.DrLEdificio.SelectedValue <> "-1" AndAlso Me.DrLTipUnita.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtInterno.Text, "Null") <> "Null" AndAlso Me.DrLDisponib.SelectedValue <> "-1" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text, "Null") <> "Null" AndAlso Me.DrLTipoLivPiano.SelectedValue <> "-1" And CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue <> "-1" And par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text, "Null") <> "Null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text, "null") <> "null" Then

                'AndAlso CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text <> "" AndAlso CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text <> "" 
                If Me.DrLEdificio.SelectedValue <> "-1" AndAlso Me.DrLTipUnita.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtInterno.Text, "Null") <> "Null" AndAlso Me.DrLDisponib.SelectedValue <> "-1" AndAlso Me.DrLTipoLivPiano.SelectedValue <> "-1" AndAlso Me.ddlFiliale.SelectedValue.ToString <> "-1" Then
                    'If A Then
                    '****************CONTROLLO DELLA DATA DI ACUISIZIONE E DI FINE VALIDITA**************
                    If par.IfNull(CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text, "NULL") <> "NULL" AndAlso CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text <> "" Then

                        par.cmd.CommandText = "SELECT DATA_COSTRUZIONE FROM SISCOM_MI.EDIFICI WHERE ID = " & Me.DrLEdificio.SelectedValue.ToString
                        myreader = par.cmd.ExecuteReader
                        If myreader.Read Then
                            If par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text) < myreader(0) Then
                                Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in DATA ACQUISIZIONE!\nLa DATA COSTRUZIONE dell\'edificio è:" & par.FormattaData(myreader(0)) & "');</SCRIPT>")
                                myreader.Close()
                                par.cmd.CommandText = ""
                                Exit Sub
                            End If
                        End If
                    End If

                    If par.IfNull(CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text, "NULL") <> "NULL" AndAlso CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text <> "" Then
                        If par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text) < par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text) Then
                            Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in DATA ACQUISIZIONE e DATA FINE VALIDITA\'!\n                                         L\'operazione non può essere completata!');</SCRIPT>")
                            Exit Sub
                        End If
                    End If

                    par.cmd.CommandText = ""

                    '****************************FINE CONTROLLO DATE**+**********************************

                    '****************************CONTROLLO SE SCELTO DEST.USO MODERATO ALLORA SALVO CANONE**+**********************************
                    If Me.TxtCanoneUI.Visible = True Then
                        If par.IfEmpty(Me.TxtCanoneUI.Text, "Null") = "Null" Then
                            Response.Write("<SCRIPT>alert('Canone obbligatorio su destinazione d\'uso Erp Moderato!');</SCRIPT>")
                            Exit Sub
                        End If
                    End If
                    '****************************FINE CONTROLLO CANONE ERP MODERATO**+**********************************


                    '****************************CONTROLLO DATI CATASTALI********************************
                    If par.IfNull(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text, "NULL") <> "NULL" AndAlso CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text <> "" AndAlso par.IfNull(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text, "NULL") <> "NULL" AndAlso CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text <> "" Then
                        If par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text) < par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text) Then
                            Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in Superficie Catastale e Superficie Mq\'!\n                                         L\'operazione non può essere completata!');</SCRIPT>")
                            Exit Sub
                        End If
                    End If

                    '******************** ALLOGGIO ESCLUSO ********************
                    If ddlAlloggioEscluso.SelectedValue.ToString = "1" Then
                        If String.IsNullOrEmpty(Trim(txtDataEsclusione.Text)) Then
                            Response.Write("<SCRIPT>alert('ATTENZIONE! Definire la data di Esclusione!\nL\'operazione non può essere completata!');</SCRIPT>")
                            Exit Sub
                        End If
                        If String.IsNullOrEmpty(Trim(txtNrProvvedimentoEsclusione.Text)) Then
                            Response.Write("<SCRIPT>alert('ATTENZIONE! Definire il Numero di Provvedimento di Esclusione!\nL\'operazione non può essere completata!');</SCRIPT>")
                            Exit Sub
                        End If
                    End If
                    '******************** ALLOGGIO ESCLUSO ********************

                    '******************** DESTINAZIONE USO RL ********************
                    'If Session.Item("LIVELLO") = "1" Or Session.Item("RESPONSABILE") = "1" Then
                    If DrLTipUnita.SelectedItem.Value = "AL" Then
                        '        If ddlDestUsoRL.SelectedValue.ToString = "-1" Then
                        '            Response.Write("<SCRIPT>alert('ATTENZIONE! Definire la destinazione d\'uso RL del Tab REGIONE!');</SCRIPT>")
                        '            AlloggioEscluso()
                        '            Exit Sub
                        '        End If
                    Else
                        ddlDestUsoRL.SelectedIndex = -1
                        ddlDestUsoRL.Items.FindByValue("-1").Selected = True
                    End If
                    'End If
                    '******************** DESTINAZIONE USO RL ********************

                    'Apro la Transazione
                    par.myTrans = par.OracleConn.BeginTransaction()
                    '‘par.cmd.Transaction = par.myTrans
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    '*****PEPPE MODIFY 15/09/2010
                    'INSERIMENTO DELL'INDIRIZZO ASSOCIATO ALL'EDIFICIO NELLA TABELLA INDIRIZZI CON UN NUOVO ID
                    'QUESTO ID VIENE POI UTILIZZATO COME ID_INDIRIZZO DELL'UNITA IMMOBILIARE
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_INDIRIZZI.NEXTVAL FROM DUAL"
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        'IDINDIRIZZO = Mid(DdLComplesso.SelectedValue.ToString, 1, 1) & myReader(0)
                        IdIndirizzo = myreader(0)
                    End If
                    myreader.Close()
                    par.cmd.CommandText = ""
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INDIRIZZI (ID,DESCRIZIONE,CIVICO,CAP,LOCALITA,COD_COMUNE) VALUES (" & IdIndirizzo & ", '" & DrLTipoInd.SelectedItem.Text & " " & par.PulisciStrSql(TxtDescrInd.Text.ToUpper) & "','" & par.PulisciStrSql(TxtCivicoKilo.Text) & "', '" & par.PulisciStrSql(TxtCap.Text) & "', '" & par.PulisciStrSql(Me.TxtLocalità.Text) & "', '" & Me.DrLComune.SelectedValue.ToString & "')"
                    par.cmd.ExecuteNonQuery()
                    vIdIndirizzo = IdIndirizzo
                    par.cmd.CommandText = ""

                    'par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI  WHERE ID = " & Me.DrLEdificio.SelectedValue.ToString
                    'myreader = par.cmd.ExecuteReader
                    'If myreader.Read Then
                    '    IdIndirizzo = myreader(0)
                    'End If
                    'myreader.Close()
                    'par.cmd.CommandText = ""
                    '******************INSERT DEI DATI CATASTALI*******************
                    'Dati catastali non obbligatori, se non è selezionato il flag per il loro inserimento consente di salvare l'unità senza dati catastali
                    If Me.ChkCatastali.Checked = True Then
                        If CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue <> "-1" And par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text, "Null") <> "Null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text, "Null") <> "Null" Then
                            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_IDENTIFICATIVI_CATASTALI.NEXTVAL FROM DUAL"
                            myreader = par.cmd.ExecuteReader
                            If myreader.Read Then
                                IdCatasto = myreader(0)
                            End If
                            myreader.Close()
                            par.cmd.CommandText = ""
                            IdCatasto = Mid(Me.DrLEdificio.SelectedValue.ToString, 1, 1) & IdCatasto


                            '****** Il cod_qualità_catastale prvisto solo per i terreni non è gestito!!!
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.IDENTIFICATIVI_CATASTALI (ID,SEZIONE, FOGLIO, NUMERO, SUB, COD_TIPOLOGIA_CATASTO,RENDITA,COD_CATEGORIA_CATASTALE,COD_CLASSE_CATASTALE,SUPERFICIE_MQ,CUBATURA,NUM_VANI," _
                            & "SUPERFICIE_CATASTALE,RENDITA_STORICA,IMMOBILE_STORICO,REDDITO_DOMINICALE, VALORE_IMPONIBILE, REDDITO_AGRARIO, VALORE_BILANCIO, DATA_ACQUISIZIONE, DATA_FINE_VALIDITA, DITTA, NUM_PARTITA, ESENTE_ICI, PERC_POSSESSO, INAGIBILE,COD_COMUNE,MICROZONA_CENSUARIA," _
                            & " ZONA_CENSUARIA, COD_STATO_CATASTALE, NOTE) VALUES (" & IdCatasto & ", '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Text) & "' ,'" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text) & "', '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text) & "','" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text) & "', " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue.ToString) & "" _
                            & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Text), "null") & ", '" & CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).SelectedValue.ToString & "', " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).SelectedValue.ToString) & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text), "null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtCubatura"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Text), "Null") _
                            & ", " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).SelectedValue.ToString) & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Text), "Null") & ", '" & par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text) & "','" & par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text) _
                            & "','" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Text) & "', '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Text) & "', " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).SelectedValue.ToString) & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text), "") & ", " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).SelectedValue.ToString) & ", '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text) & "', '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtMicrozCens"), TextBox).Text) & "', '" _
                            & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtZonaCens"), TextBox).Text) & "', " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).SelectedValue.ToString) & ", '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNote"), TextBox).Text) & "' )"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                        Else
                            Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")
                            par.cmd.CommandText = ""

                            par.myTrans.Rollback()
                            'Blocco il UNITA per eventuali modifiche da altri utenti
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                            myreader = par.cmd.ExecuteReader
                            myreader.Close()
                            Exit Sub
                        End If

                    End If

                    '*************INSERT DELL'UNITA NELL'APPOSITA TABELLA***************** 
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_UNITA_IMMOBILIARI.NEXTVAL FROM DUAL"
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        nextval = myreader(0)
                    End If
                    myreader.Close()
                    par.cmd.CommandText = ""
                    '****22/12/2010 Rimosso il primo numero dell'id edificio che rappresentava id gestore
                    'idUnitaImm = Mid(Me.DrLEdificio.SelectedValue.ToString, 1, 1) & nextval
                    idUnitaImm = nextval

                    If Me.DrLEdificio.SelectedValue <> "-1" Then
                        par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.COD_EDIFICIO FROM SISCOM_MI.EDIFICI where id = " & Me.DrLEdificio.SelectedValue.ToString
                        myreader = par.cmd.ExecuteReader
                        If myreader.Read Then
                            CodEdif = myreader(0)
                        End If
                        myreader.Close()
                        par.cmd.CommandText = ""
                        CodUniIm = CodEdif & Me.DrLTipoLivPiano.SelectedValue.ToString

                        If Me.DrlSc.SelectedValue <> "-1" Then

                            Select Case Len(Me.DrlSc.SelectedItem.Text.ToString)
                                Case 1
                                    DescScla = "00" & Me.DrlSc.SelectedItem.Text.ToString.ToUpper
                                Case 2
                                    DescScla = "0" & Me.DrlSc.SelectedItem.Text.ToString.ToUpper
                                Case 3
                                    DescScla = Me.DrlSc.SelectedItem.Text.ToString.ToUpper

                            End Select
                        Else
                            DescScla = "000"

                        End If
                        If Me.TxtInterno.Text <> "" Then
                            Select Case Len(TxtInterno.Text)
                                Case 1
                                    TxtInterno.Text = "00" & TxtInterno.Text.ToUpper
                                Case 2
                                    TxtInterno.Text = "0" & TxtInterno.Text.ToUpper
                                    'Case 3
                                    '    TxtInterno.Text = TxtInterno.Text
                            End Select
                        Else
                            Me.TxtInterno.Text = "000"
                        End If
                        CodUniIm = CodUniIm & DescScla & Me.TxtInterno.Text
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE = '" & CodUniIm & "'"
                        myreader = par.cmd.ExecuteReader
                        If myreader.Read Then
                            Response.Write("<SCRIPT>alert('Sono stati già inseriti dati per questa Unita Immobiliare!\nVerificare la Scala, l\'Interno e il Livello Piano ');</SCRIPT>")
                            par.myTrans.Rollback()
                            ''Riapro una nuova transazione
                            'Session.Item("LAVORAZIONE") = "1"
                            'par.myTrans = par.OracleConn.BeginTransaction()
                            'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                            Exit Sub
                        End If
                        par.cmd.CommandText = ""
                        myreader.Close()

                        IdScala = RitornaNullSeMenoUno(Me.DrlSc.SelectedValue.ToString)



                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.UNITA_IMMOBILIARI (ID,COD_UNITA_IMMOBILIARE,COD_TIPOLOGIA,ID_EDIFICIO,INTERNO,ID_UNITA_PRINCIPALE, COD_TIPO_DISPONIBILITA, COD_STATO_CONS_LG_392_78,ID_CATASTALE, ID_PRG_EVENTI,ID_OPERATORE_INSERIMENTO,ID_OPERATORE_AGGIORNAMENTO,ID_SCALA, COD_TIPO_LIVELLO_PIANO,ID_INDIRIZZO,ID_DESTINAZIONE_USO, ID_DESTINAZIONE_USO_RL, FL_ALLOGGIO_ESCLUSO, DATA_ESCLUSIONE, NR_PROVV_ESCLUSIONE ) VALUES " _
                        & "(" & idUnitaImm & ", '" & CodUniIm & "', '" & Me.DrLTipUnita.SelectedValue.ToString & "', " & Me.DrLEdificio.SelectedValue.ToString & ", '" & par.PulisciStrSql(Me.TxtInterno.Text) & "', " & RitornaNullSeMenoUno(Me.cmbPertinenza.SelectedValue) & ", '" & Me.DrLDisponib.SelectedValue.ToString _
                                            & "', '" & Me.DrLStatoCons.SelectedValue.ToString & "', " & par.IfEmpty(IdCatasto, "NULL") & ", " & Me.DrLStatoCens.SelectedValue.ToString & ", '" & Session("ID_OPERATORE") & "', NULL, " & IdScala & ",'" & par.IfNull(Me.DrLTipoLivPiano.SelectedValue.ToString, "Null") & "'," & IdIndirizzo & "," & RitornaNullSeMenoUno(Me.DrlDestUso.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.ddlDestUsoRL.SelectedValue.ToString) & ", " _
                                            & par.RitornaNullSeMenoUno(ddlAlloggioEscluso.SelectedValue, False) & ", " & par.insDbValue(txtDataEsclusione.Text, True, True) & ", " & par.insDbValue(txtNrProvvedimentoEsclusione.Text, True) & ")"
                        par.cmd.ExecuteNonQuery()
                        Me.vIdIndirizzo = IdIndirizzo
                        par.cmd.CommandText = ""
                        vId = idUnitaImm
                        Me.AttivaCampiPulsanti()
                        Me.DrLEdificio.Enabled = False
                        Me.cmbComplesso.Enabled = False
                        Me.txtCodUnitImm.Text = CodUniIm

                        'ANTONELLO MODIFY 05/07/2013 - AGGIUNTA FILIALE
                        If idFiliale.Value.ToString <> ddlFiliale.SelectedValue.ToString Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.FILIALI_UI SET FINE_VALIDITA = '" & Format(Now, "yyyyMMdd") & "' WHERE ID_FILIALE = " & idFiliale.Value & " AND ID_UI = " & vId & " AND FINE_VALIDITA = '30000101'"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.FILIALI_UI (ID_FILIALE, ID_UI, INIZIO_VALIDITA, FINE_VALIDITA) VALUES (" & RitornaNullSeMenoUno(ddlFiliale.SelectedValue) & ", " & vId & ", '" & Format(Now, "yyyyMMdd") & "', '30000101')"
                            par.cmd.ExecuteNonQuery()
                            idFiliale.Value = ddlFiliale.SelectedValue
                        End If
                        'CONTROLLO SE SCELTO DEST.USO MODERATO ALLORA SALVO CANONE
                        If Me.TxtCanoneUI.Visible = True Then
                            If par.IfEmpty(Me.TxtCanoneUI.Text, "Null") <> "Null" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_UI (ID, DATA,CANONE) VALUES (" & idUnitaImm & ", '" & Format(Now, "yyyyMMdd") & "', " & par.VirgoleInPunti(Me.TxtCanoneUI.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If
                        End If


                        'Blocco l'UNITA per eventuali modifiche da altri utenti
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                        myreader = par.cmd.ExecuteReader
                        classetab = "tabbertab"
                        classetabSpRev = "tabbertab"
                        myreader.Close()
                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
                        par.cmd.ExecuteNonQuery()

                        '*******************************FINE****************************************
                        '***************************************************************************

                        '*******************************EVENTO IN COND_AVVISI****************************************
                        '***************************************************************************

                        If Me.DrLDisponib.SelectedValue = "VEND" Then
                            par.cmd.CommandText = "SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO = " & Me.DrLEdificio.SelectedValue.ToString
                            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader
                            Lettore = par.cmd.ExecuteReader
                            While Lettore.Read
                                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_UI,ID_CONDOMINIO,EVENTO,DATA,VISTO) VALUES (" & vId & "," & par.IfNull(Lettore("ID_CONDOMINIO"), 0) & ", 'UNITA IMMOBILIARE VENDUTA' ," & Format(Now, "yyyyMMdd") & ",0)"
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO) VALUES (3," & vId & ",'" & Format(Now, "yyyyMMdd") & "',0," & par.IfNull(Lettore("ID_CONDOMINIO"), 0) & ")"

                                par.cmd.ExecuteNonQuery()
                            End While
                            Lettore.Close()
                        Else
                            par.cmd.CommandText = "SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO = " & Me.DrLEdificio.SelectedValue.ToString
                            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader
                            Lettore = par.cmd.ExecuteReader
                            While Lettore.Read
                                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_UI,ID_CONDOMINIO,EVENTO,DATA,VISTO) VALUES (" & vId & "," & par.IfNull(Lettore("ID_CONDOMINIO"), 0) & ", 'UNITA IMMOBILIARE ACQUISITA' ," & Format(Now, "yyyyMMdd") & ",0)"
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO) VALUES (8," & vId & ",'" & Format(Now, "yyyyMMdd") & "',0," & par.IfNull(Lettore("ID_CONDOMINIO"), 0) & ")"

                                par.cmd.ExecuteNonQuery()
                            End While
                            Lettore.Close()

                        End If


                        'COMMIT GENERALE
                        par.myTrans.Commit()
                        '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                        If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Then
                            Me.btnFoto.Visible = True
                            Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                        Else
                            Me.btnFoto.Visible = True
                            Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                        End If
                        'Riapro una nuova transazione
                        Session.Item("LAVORAZIONE") = "1"
                        par.myTrans = par.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                        Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
                        Me.TxtCivicoKilo.Enabled = True
                        Me.TxtDescrInd.Enabled = True
                        Me.DrLTipoInd.Enabled = True
                        Me.DrLComune.Enabled = True
                        Me.TxtCap.Enabled = True
                        Me.TxtLocalità.Enabled = True
                        'Else
                        '    Response.Write("<SCRIPT>alert('Riempire tutti i campi!');</SCRIPT>")
                    End If
                    'Else
                    '    Response.Write("<SCRIPT>alert('Nella definizione dell\'edificio non sono stati inseriti i dati:\r\n-SEZIONE\r\n-FOGLIO\r\n-NUMERO\r\nModificare i dati dell\'edificio prima di procedere con il salvataggio!');</SCRIPT>")

                    'End If
                Else
                    Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")

                End If
                Else
                    Response.Write("<SCRIPT>alert('E\' necessario scegliere un edificio!');</SCRIPT>")

                End If

        Catch ex As Exception
            par.myTrans.Rollback()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try


    End Sub


    Private Sub update()
        Dim myreader As Oracle.DataAccess.Client.OracleDataReader
        Dim myreaderX As Oracle.DataAccess.Client.OracleDataReader
        Dim IdCatasto As String = ""
        Dim CodUnita As String = Me.txtCodUnitImm.Text.Substring(0, 9)
        Dim DescScla As String = ""
        Dim IndicePrgInt As String = ""

        If vId <> "0" Then
            Try
                Dim tipodisp As String = Me.DrLDisponib.SelectedValue.ToString
                If statodisp.Value <> "" Then
                    Me.DrLDisponib.SelectedIndex = -1
                    Me.DrLDisponib.Items.FindByValue(statodisp.Value).Selected = True
                    tipodisp = statodisp.Value
                    statodisp.Value = ""
                End If

                If Me.DrLEdificio.SelectedValue <> "-1" AndAlso Me.DrLTipUnita.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtInterno.Text, "Null") <> "Null" AndAlso Me.DrLDisponib.SelectedValue <> "-1" AndAlso Me.DrLTipoLivPiano.SelectedValue <> "-1" AndAlso Me.ddlFiliale.SelectedValue.ToString <> "-1" Then

                    'If Me.DrLEdificio.SelectedValue <> "-1" AndAlso Me.DrLTipUnita.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtInterno.Text, "Null") <> "Null" AndAlso Me.DrLDisponib.SelectedValue <> "-1" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text, "Null") <> "Null" AndAlso Me.DrLTipoLivPiano.SelectedValue <> "-1" And CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue <> "-1" And par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text, "Null") <> "Null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text, "null") <> "null" Then

                    '******************** ALLOGGIO ESCLUSO ********************
                    If ddlAlloggioEscluso.SelectedValue.ToString = "1" Then
                        If String.IsNullOrEmpty(Trim(txtDataEsclusione.Text)) Then
                            Response.Write("<SCRIPT>alert('ATTENZIONE! Definire la data di Esclusione!\nL\'operazione non può essere completata!');</SCRIPT>")
                            Exit Sub
                        End If
                        If String.IsNullOrEmpty(Trim(txtNrProvvedimentoEsclusione.Text)) Then
                            Response.Write("<SCRIPT>alert('ATTENZIONE! Definire il Numero di Provvedimento di Esclusione!\nL\'operazione non può essere completata!');</SCRIPT>")
                            Exit Sub
                        End If
                    End If
                    '******************** ALLOGGIO ESCLUSO ********************

                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    'Richiamo la Transazione
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    '******************** DESTINAZIONE USO RL ********************
                    'If Session.Item("LIVELLO") = "1" Or Session.Item("RESPONSABILE") = "1" Then
                    If DrLTipUnita.SelectedItem.Value = "AL" Then
                        '        If ddlDestUsoRL.SelectedValue.ToString = "-1" Then
                        '            Response.Write("<SCRIPT>alert('ATTENZIONE! Definire la destinazione d\'uso RL del Tab REGIONE!');</SCRIPT>")
                        '            AlloggioEscluso()
                        '            Exit Sub
                        '        End If
                    Else
                        ddlDestUsoRL.SelectedIndex = -1
                        ddlDestUsoRL.Items.FindByValue("-1").Selected = True
                    End If
                    'End If
                    '******************** DESTINAZIONE USO RL ********************


                    '****************CONTROLLO DELLA DATA DI ACUISIZIONE E DI FINE VALIDITA**************
                    If Me.ChkCatastali.Checked = True Then

                        If CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text <> "" Then

                            par.cmd.CommandText = "SELECT DATA_COSTRUZIONE FROM SISCOM_MI.EDIFICI WHERE ID = " & Me.DrLEdificio.SelectedValue.ToString
                            myreader = par.cmd.ExecuteReader
                            If myreader.Read Then
                                If par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text) < myreader(0) Then
                                    Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in DATA ACQUISIZIONE!\nLa DATA COSTRUZIONE dell\'edificio è:" & par.FormattaData(myreader(0)) & "');</SCRIPT>")
                                    myreader.Close()
                                    par.cmd.CommandText = ""
                                    par.myTrans.Rollback()
                                    'Blocco il UNITA per eventuali modifiche da altri utenti
                                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                                    myreader = par.cmd.ExecuteReader
                                    myreader.Close()
                                    'Riapro una nuova transazione
                                    Session.Item("LAVORAZIONE") = "1"
                                    par.myTrans = par.OracleConn.BeginTransaction()
                                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                                    Exit Sub
                                End If
                            End If
                        End If

                        If CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text <> "" Then

                            If par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text) < par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text) Then
                                Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in DATA ACQUISIZIONE e DATA FINE VALIDITA\'!\n                       L\'operazione non può essere completata!');</SCRIPT>")
                                par.myTrans.Rollback()
                                'Blocco il UNITA per eventuali modifiche da altri utenti
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                                myreader = par.cmd.ExecuteReader
                                myreader.Close()
                                'Riapro una nuova transazione
                                Session.Item("LAVORAZIONE") = "1"
                                par.myTrans = par.OracleConn.BeginTransaction()
                                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                                Exit Sub
                            End If
                        End If

                        par.cmd.CommandText = ""
                    End If

                    '****************************FINE CONTROLLO DATE**+**********************************

                    par.cmd.CommandText = "SELECT ID_PRG_EVENTI FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId
                    myreaderX = par.cmd.ExecuteReader
                    If myreaderX.Read Then
                        IndicePrgInt = par.IfNull(myreaderX(0), "")
                    End If
                    myreaderX.Close()


                    If Me.TxtInterno.Text <> "" Then
                        Select Case Len(TxtInterno.Text)
                            Case 1
                                TxtInterno.Text = "00" & TxtInterno.Text
                            Case 2
                                TxtInterno.Text = "0" & TxtInterno.Text
                                'Case 3
                                '    TxtInterno.Text = TxtInterno.Text
                        End Select
                    Else
                        Me.TxtInterno.Text = "000"
                    End If
                    CodUnita = CodUnita & Me.DrLTipoLivPiano.SelectedValue.ToString

                    If Me.DrlSc.SelectedValue <> "-1" Then

                        Select Case Len(Me.DrlSc.SelectedItem.Text.ToString)
                            Case 1
                                DescScla = "00" & Me.DrlSc.SelectedItem.Text.ToString
                            Case 2
                                DescScla = "0" & Me.DrlSc.SelectedItem.Text.ToString
                            Case 3
                                DescScla = Me.DrlSc.SelectedItem.Text.ToString

                        End Select
                    Else
                        DescScla = "000"

                    End If
                    CodUnita = CodUnita & DescScla & Me.TxtInterno.Text
                    If Me.txtCodUnitImm.Text <> CodUnita Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE = '" & CodUnita & "'"
                        myreader = par.cmd.ExecuteReader
                        If myreader.Read Then
                            Response.Write("<SCRIPT>alert('Sono stati già inseriti dati per questa Unita Immobiliare!\nVerificare la Scala, l\'Interno e il Livello Piano ');</SCRIPT>")
                            par.myTrans.Rollback()
                            'Blocco il UNITA per eventuali modifiche da altri utenti
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                            myreader = par.cmd.ExecuteReader
                            myreader.Close()
                            'Riapro una nuova transazione
                            Session.Item("LAVORAZIONE") = "1"
                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                            Exit Sub
                        End If
                        par.cmd.CommandText = ""
                        myreader.Close()
                    End If
                    If Me.txtCodUnitImm.Text <> CodUnita Then
                        Response.Write("<SCRIPT>alert('Con le modifiche apportate cambierà il codice dell\'Unita Immobiliare!\nAssicurarsi che la Scala, l\'Interno e il Livello Piano siano corretti!');</SCRIPT>")
                    End If

                    Me.txtCodUnitImm.Text = CodUnita

                    '*******************UPDATE DELL'INDIRIZZO PER UNITA IMMOBILIARE PEPPE MODIFY 15/09/2010
                    par.cmd.CommandText = "UPDATE SISCOM_MI.INDIRIZZI SET DESCRIZIONE = '" & DrLTipoInd.SelectedItem.Text.ToUpper & " " & par.PulisciStrSql(TxtDescrInd.Text.ToUpper) & "', CIVICO = '" & par.PulisciStrSql(Me.TxtCivicoKilo.Text.ToUpper) & "', CAP = '" & Me.TxtCap.Text & "', LOCALITA = '" & par.PulisciStrSql(Me.TxtLocalità.Text.ToUpper) & "', COD_COMUNE = '" & Me.DrLComune.SelectedValue.ToString & "'  WHERE ID IN (SELECT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID =" & vId & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    '*******************

                    Dim upSpGenerali As String = ""
                    If classetabSpRev = "tabbertab" Then
                        If HFAscensori.Value <> chkZeroUno(Me.ChkAscensori) Or HFRiscaldamento.Value <> chkZeroUno(Me.ChkRiscaldamento) Or HFSpGenerali.Value <> chkZeroUno(ChkSpGenerali) Then
                            'se almeno una chek è cambiata dal valore iniziale, procedo con l'update
                            upSpGenerali = " ,P_ASCENSORE = " & chkZeroUno(Me.ChkAscensori) & " ,P_RISCALDAMENTO = " & chkZeroUno(Me.ChkRiscaldamento) & ",P_SERVIZI_GENERALI = " & chkZeroUno(Me.ChkSpGenerali) & " "
                            '****************MYEVENT*****************
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F222','')"
                            par.cmd.ExecuteNonQuery()
                            '*******************************FINE****************************************
                            InsertCarature()

                            HFAscensori.Value = chkZeroUno(Me.ChkAscensori)
                            HFRiscaldamento.Value = chkZeroUno(Me.ChkRiscaldamento)
                            HFSpGenerali.Value = chkZeroUno(Me.ChkSpGenerali)


                        End If
                    End If
                    If Me.ChkCatastali.Checked = True Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_IMMOBILIARI SET COD_TIPOLOGIA = '" & Me.DrLTipUnita.SelectedValue.ToString & "' , COD_TIPO_LIVELLO_PIANO = " & RitornaNullSeMenoUno(Me.DrLTipoLivPiano.SelectedValue.ToString) & " , COD_TIPO_DISPONIBILITA = '" & tipodisp & "', COD_STATO_CONS_LG_392_78 = " & RitornaNullSeMenoUno(Me.DrLStatoCons.SelectedValue.ToString) & " ,ID_PRG_EVENTI = " & (Me.DrLStatoCens.SelectedValue.ToString) & ", ID_SCALA= " & RitornaNullSeMenoUno(Me.DrlSc.SelectedValue.ToString) _
                                            & ",INTERNO = '" & par.PulisciStrSql(Me.TxtInterno.Text) & "', ID_OPERATORE_AGGIORNAMENTO= '" & Session("ID_OPERATORE") & "',ID_UNITA_PRINCIPALE = " & RitornaNullSeMenoUno(Me.cmbPertinenza.SelectedValue) & " , COD_UNITA_IMMOBILIARE ='" & CodUnita.ToUpper & "', ID_DESTINAZIONE_USO= " & RitornaNullSeMenoUno(Me.DrlDestUso.SelectedValue.ToString) & ", ID_DESTINAZIONE_USO_RL = " & RitornaNullSeMenoUno(Me.ddlDestUsoRL.SelectedValue.ToString) & ", " _
                                            & "FL_ALLOGGIO_ESCLUSO = " & par.RitornaNullSeMenoUno(ddlAlloggioEscluso.SelectedValue, False) & ", " _
                                            & "DATA_ESCLUSIONE = " & par.insDbValue(txtDataEsclusione.Text, True, True) & ", " _
                                            & "NR_PROVV_ESCLUSIONE = " & par.insDbValue(txtNrProvvedimentoEsclusione.Text, True) & " " _
                                            & upSpGenerali _
                                            & "where ID = " & vId
                    Else
                        par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_IMMOBILIARI SET COD_TIPOLOGIA = '" & Me.DrLTipUnita.SelectedValue.ToString & "' , COD_TIPO_LIVELLO_PIANO = " & RitornaNullSeMenoUno(Me.DrLTipoLivPiano.SelectedValue.ToString) & " , COD_TIPO_DISPONIBILITA = '" & tipodisp & "', COD_STATO_CONS_LG_392_78 = " & RitornaNullSeMenoUno(Me.DrLStatoCons.SelectedValue.ToString) & " ,ID_PRG_EVENTI = " & (Me.DrLStatoCens.SelectedValue.ToString) & ", ID_SCALA= " & RitornaNullSeMenoUno(Me.DrlSc.SelectedValue.ToString) _
                                            & ",INTERNO = '" & par.PulisciStrSql(Me.TxtInterno.Text) & "', ID_OPERATORE_AGGIORNAMENTO= '" & Session("ID_OPERATORE") & "',ID_UNITA_PRINCIPALE = " & RitornaNullSeMenoUno(Me.cmbPertinenza.SelectedValue) & " , COD_UNITA_IMMOBILIARE ='" & CodUnita.ToUpper & "', ID_DESTINAZIONE_USO= " & RitornaNullSeMenoUno(Me.DrlDestUso.SelectedValue.ToString) & " ,ID_CATASTALE= NULL, ID_DESTINAZIONE_USO_RL = " & RitornaNullSeMenoUno(Me.ddlDestUsoRL.SelectedValue.ToString) & ", " _
                                            & "FL_ALLOGGIO_ESCLUSO = " & par.RitornaNullSeMenoUno(ddlAlloggioEscluso.SelectedValue, False) & ", " _
                                            & "DATA_ESCLUSIONE = " & par.insDbValue(txtDataEsclusione.Text, True, True) & ", " _
                                            & "NR_PROVV_ESCLUSIONE = " & par.insDbValue(txtNrProvvedimentoEsclusione.Text, True) & " " _
                                            & upSpGenerali _
                                            & "where ID = " & vId
                        IdCatasto = 0
                    End If

                    par.cmd.ExecuteNonQuery()
                    'ANTONELLO MODIFY 05/07/2013 - AGGIUNTA FILIALE
                    If idFiliale.Value.ToString <> ddlFiliale.SelectedValue.ToString Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.FILIALI_UI SET FINE_VALIDITA = '" & Format(Now, "yyyyMMdd") & "' WHERE ID_FILIALE = " & idFiliale.Value & " AND ID_UI = " & vId & " AND FINE_VALIDITA = '30000101'"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.FILIALI_UI (ID_FILIALE, ID_UI, INIZIO_VALIDITA, FINE_VALIDITA) VALUES (" & RitornaNullSeMenoUno(ddlFiliale.SelectedValue) & ", " & vId & ", '" & Format(Now, "yyyyMMdd") & "', '30000101')"
                        par.cmd.ExecuteNonQuery()
                        idFiliale.Value = ddlFiliale.SelectedValue
                    End If
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************

                    'CONTROLLO SE SCELTO DEST.USO MODERATO ALLORA SALVO CANONE
                    If Me.DrlDestUso.SelectedValue.ToString = "2" Then
                        If par.IfEmpty(Me.TxtCanoneUI.Text, "Null") <> "Null" Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_UI WHERE ID = " & vId
                            myreader = par.cmd.ExecuteReader

                            If myreader.Read Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.CANONI_UI SET DATA='" & Format(Now, "yyyyMMdd") & "', CANONE =" & par.VirgoleInPunti(Me.TxtCanoneUI.Text) & " WHERE ID = " & vId
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            Else
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_UI (ID, DATA,CANONE) VALUES (" & vId & ", '" & Format(Now, "yyyyMMdd") & "', " & par.VirgoleInPunti(Me.TxtCanoneUI.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If
                            myreader.Close()
                        Else
                            Response.Write("<SCRIPT>alert('Canone obbligatorio su destinazione d\'uso Erp Moderato!');</SCRIPT>")
                            par.myTrans.Rollback()
                            par.cmd.CommandText = ""
                            'Blocco il UNITA per eventuali modifiche da altri utenti
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                            myreader = par.cmd.ExecuteReader
                            myreader.Close()
                            'Riapro una nuova transazione
                            Session.Item("LAVORAZIONE") = "1"
                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                            Exit Sub
                        End If
                    End If

                    'GESTIONE DATI CATASTALI CON EVENTUALE CANCELLAZIONE DI QUELLI ESISTENTI IN CASO
                    'VENGA DESELEZIONATO IL FLAG RELATIVO AI DATI CATASTALI!!!08/09/2009
                    par.cmd.CommandText = "SELECT ID_CATASTALE FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID =" & vId
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        IdCatasto = par.IfNull(myreader(0), 0)
                    End If
                    myreader.Close()
                    par.cmd.CommandText = ""

                    If Me.ChkCatastali.Checked = True Then


                        If CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue <> "-1" And par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text, "Null") <> "Null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text, "null") <> "null" AndAlso par.IfEmpty(CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text, "Null") <> "Null" Then

                            If IdCatasto = 0 Then
                                Dim IdIndirizzo As String
                                '****************************CONTROLLO DATI CATASTALI********************************
                                If par.IfNull(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text, "NULL") <> "NULL" AndAlso CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text <> "" AndAlso par.IfNull(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text, "NULL") <> "NULL" AndAlso CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text <> "" Then
                                    'If par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text) < par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text) Then
                                    '    Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in Superficie Catastale e Superficie Mq\'!\n                                         L\'operazione non può essere completata!');</SCRIPT>")
                                    '    par.myTrans.Rollback()
                                    '    'Blocco il UNITA per eventuali modifiche da altri utenti
                                    '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                                    '    myreader = par.cmd.ExecuteReader
                                    '    myreader.Close()
                                    '    'Riapro una nuova transazione
                                    '    Session.Item("LAVORAZIONE") = "1"
                                    '    par.myTrans = par.OracleConn.BeginTransaction()
                                    '    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                                    '    Exit Sub
                                    'End If
                                End If
                                '******************INSERT DEI DATI CATASTALI*******************
                                par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI  WHERE ID = " & Me.DrLEdificio.SelectedValue.ToString
                                myreader = par.cmd.ExecuteReader
                                If myreader.Read Then
                                    IdIndirizzo = myreader(0)
                                End If
                                myreader.Close()
                                par.cmd.CommandText = ""

                                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_IDENTIFICATIVI_CATASTALI.NEXTVAL FROM DUAL"
                                myreader = par.cmd.ExecuteReader
                                If myreader.Read Then
                                    IdCatasto = myreader(0)
                                End If
                                myreader.Close()
                                par.cmd.CommandText = ""
                                IdCatasto = Mid(Me.DrLEdificio.SelectedValue.ToString, 1, 1) & IdCatasto
                                '****** Il cod_qualità_catastale prvisto solo per i terreni non è gestito!!!
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.IDENTIFICATIVI_CATASTALI (ID,SEZIONE, FOGLIO, NUMERO, SUB, COD_TIPOLOGIA_CATASTO,RENDITA,COD_CATEGORIA_CATASTALE,COD_CLASSE_CATASTALE,SUPERFICIE_MQ,CUBATURA,NUM_VANI," _
                                & "SUPERFICIE_CATASTALE,RENDITA_STORICA,IMMOBILE_STORICO,REDDITO_DOMINICALE, VALORE_IMPONIBILE, REDDITO_AGRARIO, VALORE_BILANCIO, DATA_ACQUISIZIONE, DATA_FINE_VALIDITA, DITTA, NUM_PARTITA, ESENTE_ICI, PERC_POSSESSO, INAGIBILE,COD_COMUNE,MICROZONA_CENSUARIA," _
                                & " ZONA_CENSUARIA, COD_STATO_CATASTALE, NOTE) VALUES (" & IdCatasto & ", '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Text) & "' ,'" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text) & "', '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text) & "','" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text) & "', " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue.ToString) & "" _
                                & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Text), "null") & ", '" & CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).SelectedValue.ToString & "', " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).SelectedValue.ToString) & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text), "null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtCubatura"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Text), "Null") _
                                & ", " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).SelectedValue.ToString) & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Text), "Null") & ", '" & par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text) & "','" & par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text) _
                                & "','" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Text) & "', '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Text) & "', " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).SelectedValue.ToString) & ", " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text), "") & ", " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).SelectedValue.ToString) & ", '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text) & "', '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtMicrozCens"), TextBox).Text) & "', '" _
                                & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtZonaCens"), TextBox).Text) & "', " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).SelectedValue.ToString) & ", '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNote"), TextBox).Text) & "' )"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""

                                par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_IMMOBILIARI SET ID_CATASTALE = " & IdCatasto & "where ID = " & vId
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                '****************MYEVENT*****************
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','')"
                                par.cmd.ExecuteNonQuery()
                                '*******************************FINE****************************************
                                '***************************************************************************


                                par.myTrans.Commit()



                                'Riapro una nuova transazione
                                Session.Item("LAVORAZIONE") = "1"
                                par.myTrans = par.OracleConn.BeginTransaction()
                                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

                            Else

                                '****************************CONTROLLO DATI CATASTALI********************************
                                'If par.IfNull(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text, "NULL") <> "NULL" AndAlso CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text <> "" AndAlso par.IfNull(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text, "NULL") <> "NULL" AndAlso CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text <> "" Then
                                '    If par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text) < par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text) Then
                                '        Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in Superficie Catastale e Superficie Mq\'!\n                                         L\'operazione non può essere completata!');</SCRIPT>")
                                '        par.myTrans.Rollback()
                                '        'Blocco il UNITA per eventuali modifiche da altri utenti
                                '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                                '        myreader = par.cmd.ExecuteReader
                                '        myreader.Close()
                                '        'Riapro una nuova transazione
                                '        Session.Item("LAVORAZIONE") = "1"
                                '        par.myTrans = par.OracleConn.BeginTransaction()
                                '        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                                '        Exit Sub
                                '    End If
                                'End If
                                par.cmd.CommandText = "UPDATE SISCOM_MI.IDENTIFICATIVI_CATASTALI SET FOGLIO = '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text) & "', NUMERO = '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text) & "', SUB = '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text) & "', COD_TIPOLOGIA_CATASTO = " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue.ToString) & ", RENDITA = " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Text), "Null") & ", COD_CATEGORIA_CATASTALE = '" & CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).SelectedValue.ToString & "', COD_CLASSE_CATASTALE = " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).SelectedValue.ToString) & ", SUPERFICIE_MQ = " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text), "null") & ", CUBATURA = " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtCubatura"), TextBox).Text), "Null") & ", NUM_VANI= " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Text), "Null") _
                                & ", SUPERFICIE_CATASTALE= " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text), "Null") & ", RENDITA_STORICA = " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Text), "Null") & ", IMMOBILE_STORICO= " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).SelectedValue.ToString) & ", REDDITO_DOMINICALE = " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Text), "Null") & ", VALORE_IMPONIBILE= " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Text), "Null") & ", REDDITO_AGRARIO= " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Text), "Null") & ", VALORE_BILANCIO = " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Text), "Null") & ", DATA_ACQUISIZIONE = '" & par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text) & "', DATA_FINE_VALIDITA= '" & par.AggiustaData(CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text) & "', DITTA= '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Text) _
                                & "', NUM_PARTITA = '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Text) & "', ESENTE_ICI = " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).SelectedValue.ToString) & ", PERC_POSSESSO = " & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text), "Null") _
                                & ", INAGIBILE = " & RitornaNumDaSiNo(CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).SelectedValue.ToString) & ", COD_COMUNE = '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text) & "', MICROZONA_CENSUARIA = '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtMicrozCens"), TextBox).Text) _
                                & "', ZONA_CENSUARIA= '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtZonaCens"), TextBox).Text) & "',COD_STATO_CATASTALE = " & RitornaNullSeMenoUno(CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).SelectedValue.ToString) & ", NOTE = '" & par.PulisciStrSql(CType(Tab_Catastali1.FindControl("TxtNote"), TextBox).Text) & "' WHERE ID = " & IdCatasto
                                par.cmd.ExecuteNonQuery()

                                '****************MYEVENT*****************
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','')"
                                par.cmd.ExecuteNonQuery()
                                '*******************************FINE****************************************
                                '***************************************************************************

                                '*******************************EVENTO IN COND_AVVISI****************************************
                                '***************************************************************************

                                If Me.DrLDisponib.SelectedValue = "VEND" Then
                                    par.cmd.CommandText = "SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO = " & Me.DrLEdificio.SelectedValue.ToString
                                    Dim Lettore As Oracle.DataAccess.Client.OracleDataReader
                                    Lettore = par.cmd.ExecuteReader
                                    While Lettore.Read
                                        'par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_UI,ID_CONSOMINIO,EVENTO,DATA,VISTO) VALUES (" & vId & "," & par.IfNull(Lettore("ID_CONDOMINIO"), 0) & ", 'UNITA IMMOBILIARE VENDUTA' ," & Format(Now, "yyyyMMdd") & ",0)"
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO) VALUES (3," & vId & ",'" & Format(Now, "yyyyMMdd") & "',0," & par.IfNull(Lettore("ID_CONDOMINIO"), 0) & ")"

                                        par.cmd.ExecuteNonQuery()
                                    End While
                                    Lettore.Close()
                                End If

                                If IndicePrgInt <> DrLStatoCens.SelectedItem.Value Then
                                    If DrLStatoCens.SelectedItem.Value <> "NULL" Then
                                        par.cmd.CommandText = "update siscom_mi.unita_stato_manutentivo set Tipo_Riassegnabile='0' where id_unita=" & vId
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If

                                par.myTrans.Commit()

                                'Blocco il UNITA per eventuali modifiche da altri utenti
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                                myreader = par.cmd.ExecuteReader
                                myreader.Close()

                                'Riapro una nuova transazione
                                Session.Item("LAVORAZIONE") = "1"
                                par.myTrans = par.OracleConn.BeginTransaction()
                                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


                                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
                                par.cmd.CommandText = ""
                            End If
                        Else
                            Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")
                            par.cmd.CommandText = ""
                            par.myTrans.Rollback()
                            'Blocco il UNITA per eventuali modifiche da altri utenti
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                            myreader = par.cmd.ExecuteReader
                            myreader.Close()
                            'Riapro una nuova transazione
                            Session.Item("LAVORAZIONE") = "1"
                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                            Exit Sub
                        End If
                    Else
                        If IdCatasto = 0 Then
                            '    par.myTrans.Commit()
                            '    'Riapro una nuova transazione
                            '    Session.Item("LAVORAZIONE") = "1"
                            '    par.myTrans = par.OracleConn.BeginTransaction()
                            '    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                            '    Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

                            'Else
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.IDENTIFICATIVI_CATASTALI WHERE ID=" & IdCatasto
                            par.cmd.ExecuteNonQuery()
                            ''****************MYEVENT*****************
                            'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLAZIONE IDENTIFICATIVI CATASTALI')"
                            'par.cmd.ExecuteNonQuery()
                            ''*******************************FINE****************************************
                            ''***************************************************************************

                            par.myTrans.Commit()
                            CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtCubatura"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtZonaCens"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtMicrozCens"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Text = ""
                            CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).SelectedValue = "000"
                            CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).SelectedValue = "00"
                            Me.DrLStatoCens.SelectedValue = "NULL"
                            Me.DrLStatoCons.SelectedValue = "NORMA"
                            Me.DrLDisponib.SelectedValue = "INDEF"
                            CType(Tab_Catastali1.FindControl("TxtNote"), TextBox).Text = ""
                            'Blocco il UNITA per eventuali modifiche da altri utenti
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                            myreader = par.cmd.ExecuteReader
                            myreader.Close()
                            'Riapro una nuova transazione
                            Session.Item("LAVORAZIONE") = "1"
                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
                            par.cmd.CommandText = ""
                        End If


                    End If

                    Me.AttivaCampiPulsanti()

                Else
                    Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")
                    Exit Sub
                End If

                    '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Then
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                    Else
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                    End If

            Catch ex As Exception
                par.myTrans.Rollback()
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message

            End Try



        End If
    End Sub

    Private Sub AttivaCampiPulsanti()
        'Me.ImgButAdegNorm.Visible = True
        'Me.ImgButDimens.Visible = True
        'Me.ImgButVarConf.Visible = True
        'Me.ImgBtnMillesim.Visible = True
        Me.ImgButStampa.Visible = True
        Me.btnFoto.Visible = True
        'Me.ImgBtnVerStatManut.Visible = True
        visualizzaSM.value = "1"
    End Sub

    Private Sub DisattivaCampiPulsanti()
        'Me.ImgButAdegNorm.Enabled = False
        'Me.ImgButDimens.Enabled = False
        'Me.ImgButVarConf.Enabled = False
        Me.btnFoto.Visible = False
        'CType(Tab_Catastali1.FindControl("TxtFoglio"),TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtNumero"),TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Enabled = False
        'Me.TxtCodComun.Enabled = False
        'CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).Enabled = False
        'CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).Enabled = False
        'CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).Enabled = False
        'CType(Tab_Catastali1.FindControl("DrLInagibile"),DropDownList).Enabled = False
        'CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).Enabled = False
        'CType(Tab_Catastali1.FindControl("DrLStatoCatast"),DropDownList).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtCubatura), TextBox).Enabled = False
        'Me.CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Enabled = False
        'Me.TxtDataAcquisiz.Enabled = False
        'Me.TxtDataFineVal.Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtMicrozCens"),TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Enabled = False
        'CType(Tab_Catastali1.FindControl("TxtZonaCens"),TextBox).Enabled = False
    End Sub
    Private Function RitornaNumDaSiNo(ByVal valoredapassare As String) As String
        Try
            Dim a As String = ""
            If valoredapassare = "SI" Then
                a = 1
            ElseIf valoredapassare = "NO" Then
                a = 0
            Else
                a = "Null"
            End If

            Return a
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Function

    Private Function RitornaSiNoDaNum(ByVal valoredapassare As String) As String
        Try
            Dim a As String = ""
            If valoredapassare = "1" And valoredapassare <> "" Then
                a = "SI"
            ElseIf valoredapassare = "0" Then
                a = "NO"
            End If

            Return a
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Function
    Private Function IfEmpty(ByVal Controllore As String) As String
        Try
            Dim Q As String = ""
            If Controllore = "" Then
                Q = "NULL"
            Else
                Q = Controllore
            End If
            Return Q
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Function
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Try
            Dim a As String
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If
            Return a
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Function
    'Protected Sub ImgBtnMillesim_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnMillesim.Click
    '    Response.Write("<script>window.open('Millesimali.aspx?IDED=" & vIdEdificio & ",&Pas=UI&IDUNI=" & vId & "','MILLESIMALI', 'resizable=yes, width=520, height=250');</script>")
    '    'Me.txtindietro.Text = txtindietro.Text - 1
    'End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        If Me.cmbComplesso.SelectedValue <> "-1" Then
            FiltraEdifici()
            Me.TxtDescrInd.Text = ""
            Me.DrLTipoInd.SelectedValue = -1
            Me.TxtCivicoKilo.Text = ""
            Me.DrLComune.SelectedValue = -1
        Else
            CaricaEdifici()
            Me.TxtDescrInd.Text = ""
            Me.DrLTipoInd.SelectedValue = -1
            Me.TxtCivicoKilo.Text = ""
            Me.DrLComune.SelectedValue = -1
        End If
    End Sub
    Private Sub FiltraEdifici()
        Try
            Dim connopennow As Boolean = False

            If Me.cmbComplesso.SelectedValue <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    'Richiamo la Transazione
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    connopennow = True
                End If
                Dim gest As Integer = 0
                Me.DrLEdificio.Items.Clear()
                DrLEdificio.Items.Add(New ListItem(" ", -1))


                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.cmbComplesso.SelectedValue.ToString & " order by denominazione asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

                    'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While


                myReader1.Close()

                par.OracleConn.Close()

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub CaricaEdifici()
        Try


            'Apro la CONNESSIONE  con il DB PER RIEMPIRE I CAMPI (Combo, textbox...ecc...)
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.DrLEdificio.Items.Clear()

            DrLEdificio.Items.Add(New ListItem(" ", -1))
            If Session("PED2_ESTERNA") = "1" Then
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.LOTTO > 3 order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID order by denominazione asc"

            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            myReader1.Close()
            DrLEdificio.SelectedValue = "-1"

            par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub ImgButStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgButStampa.Click
        Try
            If vId <> -1 Then
                If txtModificato.Value <> "111" Then
                    Response.Write("<script>window.open('StampaUI.aspx?ID=" & vId & "', '');</script>")
                Else
                    txtModificato.Value = "1"
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                End If
                'Me.txtindietro.Text = txtindietro.Text - 1
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub ImageButton1_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        'If Request.QueryString("V") <> "1" Then
        '    Response.Redirect(Request.QueryString("C") & ".aspx?E=" & Request.QueryString("IDED"))
        'Else
        '    Response.Write("<script>history.go(" & txtindietro.Text & ");</script>")
        'End If
        Try
            If txtModificato.Value <> "111" Then
                If Request.QueryString("LE") <> "1" And Session("PED2_SOLOLETTURA") <> "1" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.OracleConn.Close()

                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")
                End If
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = 0


                Select Case Request.QueryString("C")
                    Case "RisultatiUI"
                        Response.Redirect(Request.QueryString("C") & ".aspx?COMP=" & Request.QueryString("COMPLESSO") & "&E=" & Request.QueryString("EDIFICIO") & "&IND=" & Request.QueryString("IND") & "&IDIND=" & Request.QueryString("IDIND") & "&INT=" & Request.QueryString("INT") & "&CIVICO=" & Request.QueryString("CIVICO") & "&SCAL=" & Request.QueryString("SCAL") & "&TIPOL=" & Request.QueryString("TIPOL") & "&T=" & Request.QueryString("T") & "&DISP=" & Request.QueryString("DISP"))
                    Case "RisultatiOccupante"
                        Response.Redirect(Request.QueryString("C") & ".aspx")

                    Case Nothing
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                    Case Else
                        Response.Redirect(Request.QueryString("C") & ".aspx?E=" & Request.QueryString("EDIFICIO") & "&COMPLESSO=" & Request.QueryString("COMPLESSO"))
                End Select
            Else
                txtModificato.Value = "1"
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub chkPertinenze_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPertinenze.CheckedChanged

        Try
            If Me.chkPertinenze.Checked = True Then
                If Me.DrLEdificio.SelectedValue <> "-1" Then
                    Me.cmbPertinenza.Visible = True
                    If Session("PED2_SOLOLETTURA") = "0" And operatoreComune.Value = "0" Then
                        caricaPertinenze(True)
                    Else
                        caricaPertinenze(False)
                    End If
                Else
                    Me.chkPertinenze.Checked = False
                    Response.Write("<SCRIPT>alert('E\' necessario scegliere un edificio!');</SCRIPT>")
                End If

            ElseIf Me.chkPertinenze.Checked = False Then
                Me.cmbPertinenza.Items.Clear()
                cmbPertinenza.Items.Add(New ListItem(" ", -1))
                Me.cmbPertinenza.Visible = False

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub caricaPertinenze(Optional ByVal SoloUSD As Boolean = False)
        Try
            Dim Apertadesso As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                Apertadesso = True
            End If
            Me.cmbPertinenza.Items.Clear()
            cmbPertinenza.Items.Add(New ListItem(" ", -1))
            If SoloUSD = False Then
                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString
            Else
                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA<>'AL' AND ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString
            End If
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader = par.cmd.ExecuteReader

            While myReader.Read
                cmbPertinenza.Items.Add(New ListItem(par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), " "), par.IfNull(myReader("ID"), -1)))
            End While
            If Apertadesso = True Then
                par.OracleConn.Close()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub


    Protected Sub btnFoto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnFoto.Click
        Response.Write("<script>window.open('FotoImmobile.aspx?T=U&ID=" & vId & "&I=" & vIdIndirizzo & "', '');</script>")

    End Sub
    Private Sub LivelloPiano()
        Try
            Dim ApertaAdesso As Boolean = False

            'Questo metodo vuole una connessione aperte dal chiamante in caso contrario ne apre una nuova e la chiude immediatamente
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ApertaAdesso = True
            End If
            Dim sStrSQL As String

            Dim Entro As Integer
            Dim Fuori As Integer
            Dim mezzanini As Integer
            Dim Attico As Integer
            Dim SuperAttico As Integer
            Dim Sottotetto As Integer
            Dim Seminter As Integer
            Dim PTerra As Integer
            Dim TROVATO As Boolean
            Me.DrLTipoLivPiano.Items.Clear()


            If Me.DrLEdificio.SelectedValue.ToString = "-1" Then
                par.cmd.CommandText = "SELECT COD, DESCRIZIONE, LIVELLO FROM SISCOM_MI.TIPO_LIVELLO_PIANO"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                DrLTipoLivPiano.Items.Add(New ListItem(" ", -1))
                While myReader2.Read
                    DrLTipoLivPiano.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("COD"), -1)))
                End While
                par.cmd.CommandText = ""
                myReader2.Close()
            End If


            par.cmd.CommandText = "SELECT  NUM_PIANI_ENTRO , NUM_PIANI_FUORI,PIANO_TERRA,SEMINTERRATO,SOTTOTETTO,ATTICO,SUPERATTICO,NUM_MEZZANINI FROM SISCOM_MI.EDIFICI WHERE ID = " & Me.DrLEdificio.SelectedValue.ToString
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                Entro = par.IfNull(myReader("NUM_PIANI_ENTRO"), 0)
                Fuori = par.IfNull(myReader("NUM_PIANI_FUORI"), 0)
                mezzanini = par.IfNull(myReader("NUM_MEZZANINI"), 0)
                Attico = par.IfNull(myReader("ATTICO"), 0)
                SuperAttico = par.IfNull(myReader("SUPERATTICO"), 0)
                Sottotetto = par.IfNull(myReader("SOTTOTETTO"), 0)
                Seminter = par.IfNull(myReader("SEMINTERRATO"), 0)
                PTerra = par.IfNull(myReader("PIANO_TERRA"), 0)
            End If
            myReader.Close()
            par.cmd.CommandText = ""
            sStrSQL = "select COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO"

            If Fuori <> 0 Then
                sStrSQL = sStrSQL & " WHERE (LIVELLO <= " & Fuori
                TROVATO = True
            Else
                sStrSQL = sStrSQL & " WHERE (LIVELLO <= " & Fuori
                TROVATO = True

            End If
            If TROVATO = True Then
                sStrSQL = sStrSQL & " and "
            Else
                sStrSQL = sStrSQL & " where( "
                TROVATO = True
            End If
            sStrSQL = sStrSQL & " LIVELLO >=-" & Entro

            If TROVATO = True Then
                sStrSQL = sStrSQL & " AND (ROUND(LIVELLO,0)=LIVELLO) "
            Else
                sStrSQL = sStrSQL & " WHERE (ROUND(LIVELLO,0)=LIVELLO) "
                TROVATO = True

            End If
            If PTerra = 1 Then
                sStrSQL = sStrSQL & " )"

            Else
                sStrSQL = sStrSQL & " AND LIVELLO<>0)"

            End If
            If mezzanini <> 0 Then
                If TROVATO = True Then
                    sStrSQL = sStrSQL & "OR (LIVELLO<" & Fuori & " AND (ROUND(LIVELLO,0)<>LIVELLO)) "
                End If
            End If

            If Attico <> 0 Then
                sStrSQL = sStrSQL & " or COD = 74 "
            End If
            If SuperAttico <> 0 Then
                sStrSQL = sStrSQL & " or COD = 75 "
            End If
            If Sottotetto <> 0 Then
                sStrSQL = sStrSQL & " or COD = 73 "
            End If
            If Seminter <> 0 Then
                sStrSQL = sStrSQL & " or COD = 72 "
            End If

            'sStrSQL = sStrSQL & " ) ORDER BY COD ASC"

            par.cmd.CommandText = sStrSQL

            myReader = par.cmd.ExecuteReader

            DrLTipoLivPiano.Items.Add(New ListItem(" ", -1))
            While myReader.Read
                DrLTipoLivPiano.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("COD"), -1)))
            End While
            par.cmd.CommandText = ""
            myReader.Close()
            If ApertaAdesso = True Then
                par.OracleConn.Close()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub ApriFrmWithDBLock()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dt As New Data.DataTable
        Dim idCatasto As String

        If vId <> -1 Then
            Try
                'SE SI PROVIENE DA UNA RICERCA DOPO AVER RIEMPITO I CAMPI SETTO LA MIA CONNESSIONE CHE RESTERà VALIDA PER TUTTA LA DURATA DI APERTURA DEL FORM

                par.OracleConn.Open()
                par.SettaCommand(par)

                Session.Add("SLE", 1)

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)

                'LblDatiContratt.Text = "<a href='DatiContratto.aspx?ID=" & vId & "&UI=" & par.IfNull(dt.Rows(0).Item("COD_UNITA_IMMOBILIARE"), "") & "' target='_blank'>Dati Contrattuali</a>"
                par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE ID_UNITA =" & vId & " AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO ORDER BY DATA_DECORRENZA DESC"
                Dim myReaderPepp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderPepp.Read Then
                    LblDatiContratt.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderPepp("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">Dati Contrattuali</a>"
                Else
                    'LblDatiContratt.Text = "<a href='DatiContratto.aspx?ID=" & vId & "&UI=" & par.IfNull(dt.Rows(0).Item("COD_UNITA_IMMOBILIARE"), "") & "' target='_blank'>Dati Contrattuali</a>"
                    LblDatiContratt.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "alert('Nessun Contratto stipulato su questa unità!');" & Chr(34) & ">Dati Contrattuali</a>"
                End If
                myReaderPepp.Close()

                If dt.Rows.Count > 0 Then
                    HFAscensori.Value = dt.Rows(0).Item("P_ASCENSORE")
                    HFRiscaldamento.Value = dt.Rows(0).Item("P_RISCALDAMENTO")
                    HFSpGenerali.Value = dt.Rows(0).Item("P_SERVIZI_GENERALI")

                    If HFAscensori.Value = 1 Then
                        Me.ChkAscensori.Checked = True
                    End If
                    If HFRiscaldamento.Value = 1 Then
                        Me.ChkRiscaldamento.Checked = True
                    End If
                    If HFSpGenerali.Value = 1 Then
                        Me.ChkSpGenerali.Checked = True
                    End If
                    Me.DrLTipUnita.SelectedValue = dt.Rows(0).Item("COD_TIPOLOGIA")
                    If DrLTipUnita.SelectedValue <> "AL" Then
                        lblSoglia.Text = ""
                        lblSoglia.BackColor = Drawing.Color.Transparent
                    End If
                    Me.DrLEdificio.SelectedValue = dt.Rows(0).Item("ID_EDIFICIO")
                    vIdEdificio = dt.Rows(0).Item("ID_EDIFICIO")
                    vIdIndirizzo = dt.Rows(0).Item("ID_INDIRIZZO")
                    LivelloPiano()
                    scala()


                    'CONTROLLO CHE LA DRLSC SIA PIENA, PERCHè SE COMPOSTA DA UN SOLO ELEMENTO è QUELLO VUOTO PARI A "NESSUNA"
                    If Me.DrlSc.Items.Count > 1 Then
                        Me.DrlSc.SelectedValue = (par.IfNull((dt.Rows(0).Item("id_scala").ToString), "-1"))
                        ' Me.DrlSc.Enabled = False
                    End If
                    If par.IfNull(dt.Rows(0).Item("ID_UNITA_PRINCIPALE"), 0) <> 0 Then
                        caricaPertinenze()
                        Me.chkPertinenze.Checked = True
                        Me.cmbPertinenza.Visible = True
                        Me.cmbPertinenza.SelectedValue = dt.Rows(0).Item("ID_UNITA_PRINCIPALE")
                    End If

                    Me.TxtInterno.Text = par.IfNull(dt.Rows(0).Item("INTERNO"), "")
                    Me.txtCodUnitImm.Text = par.IfNull(dt.Rows(0).Item("COD_UNITA_IMMOBILIARE"), "")
                    Me.DrLTipoLivPiano.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPO_LIVELLO_PIANO"), -1)


                    Me.DrLStatoCons.SelectedValue = (par.IfNull(dt.Rows(0).Item("COD_STATO_CONS_LG_392_78"), -1))

                    Me.DrLStatoCons.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_STATO_CONS_LG_392_78"), -1)

                    Me.DrLDisponib.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPO_DISPONIBILITA"), -1)
                    Me.DrLDisponib.Items.FindByValue(par.IfNull(dt.Rows(0).Item("COD_TIPO_DISPONIBILITA"), -1)).Selected = True

                    Me.DrLStatoCens.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_PRG_EVENTI"), "NULL")
                    Me.DrlDestUso.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_DESTINAZIONE_USO"), -1)
                    '*************** REGIONE ***************
                    Me.ddlDestUsoRL.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_DESTINAZIONE_USO_RL"), -1)
                    Me.ddlAlloggioEscluso.SelectedValue = par.IfNull(dt.Rows(0).Item("FL_ALLOGGIO_ESCLUSO"), "0")
                    Me.txtDataEsclusione.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_ESCLUSIONE"), ""))
                    Me.txtNrProvvedimentoEsclusione.Text = par.IfNull(dt.Rows(0).Item("NR_PROVV_ESCLUSIONE"), "")
                    AlloggioEscluso()
                    par.CaricaStoricoNote(vId, dataGridNote, "2")
                    '*************** REGIONE ***************

                    'MAX 06/11/2014
                    If Me.DrlDestUso.SelectedValue = "2" Then
                        VisibilitaCanone()
                        par.cmd.CommandText = "SELECT CANONE FROM SISCOM_MI.CANONI_UI WHERE ID =" & vId
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader.Read Then
                            Me.TxtCanoneUI.Text = myReader("CANONE")
                        End If
                        TxtCanoneUI.Enabled = True
                    End If

                    idCatasto = par.IfNull(dt.Rows(0).Item("ID_CATASTALE"), 0)
                    '*********PEPPE MODIFY 15/09/2010 PER INDIRIZZO DELL'UNITA' EDITABILE
                    par.cmd.CommandText = "SELECT DESCRIZIONE ,CIVICO, CAP, LOCALITA, INDIRIZZI.COD_COMUNE FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI where INDIRIZZI.ID = UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID =" & vId
                    'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderInd.Read Then
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                        par.cmd.CommandText = "SELECT COD FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO WHERE DESCRIZIONE =  '" & RicavaVial(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "'"
                        myReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            Me.DrLTipoInd.SelectedValue = myReader(0)
                        End If
                        myReader.Close()
                        Me.TxtDescrInd.Text = RicavaDescVia(par.IfNull(myReaderInd("DESCRIZIONE"), ""))
                        Me.TxtCivicoKilo.Text = par.IfNull(myReaderInd("CIVICO"), "")
                        Me.TxtCap.Text = par.IfNull(myReaderInd("CAP"), "")
                        Me.TxtLocalità.Text = par.IfNull(myReaderInd("LOCALITA"), "")
                        'Me.lblIndirizzo.Text = myReaderInd("DESCRIZIONE")
                        Me.DrLComune.SelectedValue = par.IfNull(myReaderInd("COD_COMUNE"), "F205")
                    End If
                    myReaderInd.Close()

                    VisualizzaAscHandicap(vId)
                    'Aggiunta Visualizzazione della tipologia di impianto di riscaldamento
                    par.cmd.CommandText = "SELECT TIPOLOGIA_IMP_RISCALDAMENTO.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO,SISCOM_MI.EDIFICI WHERE EDIFICI.COD_TIPOLOGIA_IMP_RISCALD = TIPOLOGIA_IMP_RISCALDAMENTO.COD AND EDIFICI.ID =" & Me.DrLEdificio.SelectedValue.ToString
                    myReaderInd = par.cmd.ExecuteReader()
                    If myReaderInd.Read Then
                        Me.lblTipoRiscald.Text = myReaderInd("DESCRIZIONE")
                    End If
                    myReaderInd.Close()


                    dt = New Data.DataTable
                    da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT * FROM SISCOM_MI.IDENTIFICATIVI_CATASTALI WHERE ID = " & idCatasto, par.OracleConn)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then

                        Me.ChkCatastali.Checked = True

                        CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Text = par.IfNull(dt.Rows(0).Item("SEZIONE").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Text = par.IfNull(dt.Rows(0).Item("FOGLIO").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Text = par.IfNull(dt.Rows(0).Item("NUMERO").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Text = par.IfNull(dt.Rows(0).Item("SUB").ToString, "")


                        CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPOLOGIA_CATASTO"), -1)


                        CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Text = par.IfNull(dt.Rows(0).Item("RENDITA").ToString, "")

                        CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).SelectedValue = par.IfNull(dt.Rows(0).Item("COD_CATEGORIA_CATASTALE"), -1)


                        CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).SelectedValue = par.IfNull(dt.Rows(0).Item("COD_CLASSE_CATASTALE"), -1)


                        CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).SelectedValue = par.IfNull(dt.Rows(0).Item("COD_STATO_CATASTALE"), -1)


                        CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Text = par.IfNull(dt.Rows(0).Item("SUPERFICIE_MQ").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtCubatura"), TextBox).Text = par.IfNull(dt.Rows(0).Item("CUBATURA").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Text = par.IfNull(dt.Rows(0).Item("NUM_VANI").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Text = par.IfNull(dt.Rows(0).Item("SUPERFICIE_CATASTALE").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Text = par.IfNull(dt.Rows(0).Item("RENDITA_STORICA").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Text = par.IfNull(dt.Rows(0).Item("REDDITO_DOMINICALE").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Text = par.IfNull(dt.Rows(0).Item("VALORE_IMPONIBILE").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Text = par.IfNull(dt.Rows(0).Item("REDDITO_AGRARIO").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Text = par.IfNull(dt.Rows(0).Item("VALORE_BILANCIO").ToString, "")
                        CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Text = par.FormattaData(dt.Rows(0).Item("DATA_ACQUISIZIONE").ToString)
                        CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Text = par.FormattaData(dt.Rows(0).Item("DATA_FINE_VALIDITA").ToString)
                        CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Text = dt.Rows(0).Item("DITTA").ToString
                        CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Text = dt.Rows(0).Item("NUM_PARTITA").ToString
                        CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Text = dt.Rows(0).Item("PERC_POSSESSO").ToString
                        CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text = dt.Rows(0).Item("COD_COMUNE").ToString
                        CType(Tab_Catastali1.FindControl("TxtMicrozCens"), TextBox).Text = dt.Rows(0).Item("MICROZONA_CENSUARIA").ToString
                        CType(Tab_Catastali1.FindControl("TxtZonaCens"), TextBox).Text = dt.Rows(0).Item("ZONA_CENSUARIA").ToString
                        CType(Tab_Catastali1.FindControl("TxtNote"), TextBox).Text = dt.Rows(0).Item("NOTE").ToString

                        CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).SelectedValue = RitornaSiNoDaNum(par.IfNull((dt.Rows(0).Item("IMMOBILE_STORICO")), ""))

                        CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).SelectedValue = RitornaSiNoDaNum(par.IfNull((dt.Rows(0).Item("INAGIBILE")), ""))

                        CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).SelectedValue = RitornaSiNoDaNum(par.IfNull((dt.Rows(0).Item("ESENTE_ICI")), ""))
                    End If
                    dt = New Data.DataTable
                    da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID = " & vIdEdificio, par.OracleConn)
                    da.Fill(dt)
                    Me.cmbComplesso.SelectedValue = dt.Rows(0).Item("ID_COMPLESSO").ToString

                    Me.DrLEdificio.Enabled = False
                    Me.cmbComplesso.Enabled = False
                    'ANTONELLO MODIFY 05/07/2013 - AGGIUNTA FILIALE
                    par.cmd.CommandText = "SELECT ID_FILIALE FROM SISCOM_MI.FILIALI_UI WHERE ID_UI = " & vId & " AND FINE_VALIDITA = '30000101'"
                    Dim MyReaderFiliale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If MyReaderFiliale.Read Then
                        ddlFiliale.SelectedValue = par.IfNull(MyReaderFiliale("ID_FILIALE"), "-1")
                        idFiliale.Value = par.IfNull(MyReaderFiliale("ID_FILIALE"), "-1")
                    End If
                    MyReaderFiliale.Close()
                End If
                Dim testoTabella As String
                testoTabella = ""
                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                            & "<tr>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.UNITA</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"
                par.cmd.CommandText = "SELECT cod_unita_immobiliare FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_UNITA_PRINCIPALE=" & vId
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader2.Read

                    testoTabella = testoTabella _
                                & "<tr>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&PERT=1&ID=" & par.IfNull(myReader2("cod_unita_immobiliare"), "") & "','Pertinenza" & myReader2("cod_unita_immobiliare") & "','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & par.IfNull(myReader2("cod_unita_immobiliare"), "") & "</a></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "</td>" _
                                & "</tr>"
                Loop

                myReader2.Close()

                par.cmd.CommandText = "SELECT MOD_PED2_SOLO_LETTURA FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    Session.Item("PED2_SOLOLETTURA") = par.IfNull(myReader2("MOD_PED2_SOLO_LETTURA"), "0")
                    If par.IfNull(myReader2("MOD_PED2_SOLO_LETTURA"), "0") = "0" Then
                        Session("SLE") = "0"
                    End If
                End If
                myReader2.Close()

                LblPertinenze.Text = testoTabella & "</table>"
                'par.OracleConn.Close()
                CType(Tab_AdDimens1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_AdDimens1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_AdVarConf1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_AdVarConf1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_AdNormativo1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_AdNormativo1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_ValoriMilleismalil1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_ValoriMilleismalil1.FindControl("BtnElimina"), ImageButton).Visible = False
                CType(Tab_ValoriMilleismalil1.FindControl("btnModifica"), ImageButton).Visible = False


               


                'CType(Tab_ValoriMillesimali1.FindControl("BtnADD"), ImageButton).Visible = False
                'CType(Tab_ValoriMillesimali1.FindControl("BtnElimina"), ImageButton).Visible = False
                If Request.QueryString("PERT") = 1 Then
                    Me.HyLinkPertinenze.Visible = False
                    'Me.ImgBtnVerStatManut.Visible = False
                    visualizzaSM.value = "0"
                End If
                'Controllo se il campo Dest USO RL può essere abilitato o meno
                If Session.Item("LIVELLO") = "1" Or Session.Item("RESPONSABILE") = "1" Then
                    ddlDestUsoRL.Enabled = True
                Else
                    ddlDestUsoRL.Enabled = False
                End If
                FrmSolaLettura()
                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                Try
                    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Then
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                    Else
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                    End If
                Catch ex As Exception
                    Me.LblErrore.Visible = True
                    LblErrore.Text = "ATTENZIONE!Verificare il percorso delle foto e delle planimetrie!"
                End Try
                '*********************FINE CONTROLLO PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                'Me.ImgBtnMillesim.Enabled = False
                'Me.ImgButAdegNorm.Enabled = False
                'Me.ImgButDimens.Enabled = False
                'Me.ImgButVarConf.Enabled = False
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try
        End If
    End Sub


    'Protected Sub ImgBtnVerStatManut_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnVerStatManut.Click
    '    If vId <> -1 Then
    '        If txtModificato.Value <> "111" Then
    '            If Request.QueryString("LE") <> "1" And Session("PED2_SOLOLETTURA") <> "1" Then
    '                If Session.Item("SLE") = "1" Then
    '                    Response.Write("<script>window.open('VerificaSManutentivo.aspx?A=0&L=2&ID=" & vId & "', '');</script>")
    '                Else
    '                    If txtModificato.Value = "1" Then
    '                        Response.Write("<script>alert('Sono state effettuate delle modifiche. Salvare prima di richiamare il modulo!');</script>")
    '                    Else
    '                        Response.Write("<script>window.open('VerificaSManutentivo.aspx?A=1&ID=" & vId & "', '');</script>")
    '                    End If
    '                End If
    '            Else
    '                Response.Write("<script>window.open('VerificaSManutentivo.aspx?A=0&ID=" & vId & "', '');</script>")
    '            End If
    '        Else
    '            txtModificato.Value = "1"
    '            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    '        End If
    '        'Me.txtindietro.Text = txtindietro.Text - 1
    '    End If
    'End Sub


    Private Sub AttivaCampiCatastali()
        CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtCubatura"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).Enabled = True
        CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtZonaCens"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtMicrozCens"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Enabled = True
        CType(Tab_Catastali1.FindControl("TxtNote"), TextBox).Enabled = True
        Me.TrovaFogSez()
    End Sub
    Private Sub DisattivaCampiCatastali()
        CType(Tab_Catastali1.FindControl("TxtSezione"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtFoglio"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtSub"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtNumero"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("DrLTipoCat"), DropDownList).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtPercPoss"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("DrLCategoria"), DropDownList).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtMq"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtSupCat"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtVani"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtCubatura"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("DrLClasse"), DropDownList).Enabled = False
        CType(Tab_Catastali1.FindControl("DrLStatoCatast"), DropDownList).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtValImponibile"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtValBil"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("DrLImmStorico"), DropDownList).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtRendStorica"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtRendita"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("DrlEsenzICI"), DropDownList).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtRedAgrari"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtRedDomini"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("DrLInagibile"), DropDownList).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtZonaCens"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtMicrozCens"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtDataAcquisiz"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtDataFineVal"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtNumPartita"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtDitta"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtNote"), TextBox).Enabled = False
        CType(Tab_Catastali1.FindControl("TxtCodComun"), TextBox).Text = ""
    End Sub

    Protected Sub ChkCatastali_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCatastali.CheckedChanged
        If ChkCatastali.Checked = True Then
            AttivaCampiCatastali()
        Else
            DisattivaCampiCatastali()
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        tabdefault1 = ""
        tabdefault2 = ""
        tabdefault3 = ""
        tabdefault4 = ""
        tabdefault5 = ""
        tabdefault6 = ""
        tabdefault7 = ""
        tabdefault8 = ""
        Select Case txttab.Value - 1
            Case "1"
                tabdefault1 = "tabbertabdefault"
            Case "2"
                tabdefault2 = "tabbertabdefault"
            Case "3"
                tabdefault3 = "tabbertabdefault"
            Case "4"
                tabdefault4 = "tabbertabdefault"
            Case "5"
                tabdefault5 = "tabbertabdefault"
            Case "6"
                tabdefault6 = "tabbertabdefault"
            Case "7"
                tabdefault7 = "tabbertabdefault"
            Case "8"
                tabdefault8 = "tabbertabdefault"
        End Select
    End Sub


    Protected Sub DrlDestUso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrlDestUso.SelectedIndexChanged

        VisibilitaCanone()

    End Sub

    Private Sub VisibilitaCanone()
        If Me.DrlDestUso.SelectedValue.ToString = "2" Then
            Me.TxtCanoneUI.Visible = True
            Me.LblCanone.Visible = True
            Me.TxtCanoneUI.Text = ""
        Else
            Me.TxtCanoneUI.Visible = False
            Me.LblCanone.Visible = False
            Me.TxtCanoneUI.Text = ""
        End If
    End Sub

    Protected Sub DrLDisponib_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLDisponib.SelectedIndexChanged
        If Me.DrLDisponib.SelectedValue <> "LIBE" Then
            Me.DrlDestUso.Enabled = False
        Else
            Me.DrlDestUso.Enabled = True
        End If
    End Sub
    Private Function RicavaVial(ByVal indirizzo As String) As String
        Dim pos As Integer
        Dim via As String
        Try

            pos = InStr(1, indirizzo, " ")
            If pos > 0 Then
                via = Mid(indirizzo, 1, pos - 1)
                Select Case via

                    Case "CORSO", "C.SO"
                        RicavaVial = "CORSO"
                    Case "PIAZZA", "PZ.", "P.ZZA"
                        RicavaVial = "PIAZZA"
                    Case "PIAZZALE", "P.LE"
                        RicavaVial = "PIAZZALE"
                    Case "P.T"
                        RicavaVial = "PORTA"
                    Case "S.T.R.", "STRADA"
                        RicavaVial = "STRADA"
                    Case "V.", "VIA"
                        RicavaVial = "VIA"
                    Case "VIALE", "V.LE"
                        RicavaVial = "VIALE"
                    Case "LARGO"
                        RicavaVial = "LARGO"
                    Case "VICO"
                        RicavaVial = "VICO"
                    Case "VICOLO"
                        RicavaVial = "VICOLO"
                    Case "ALTRO"
                        RicavaVial = "ALTRO"
                    Case "ALZAIA"
                        RicavaVial = "ALZAIA"
                    Case "RIPA"
                        RicavaVial = "RIPA"
                    Case "CALLE"
                        RicavaVial = "CALLE"
                    Case Else
                        RicavaVial = "VIA"
                End Select

            Else
                RicavaVial = ""
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Source
        End Try
    End Function
    Private Function RicavaDescVia(ByVal indirizzo As String) As String
        Try

            Dim pos As Integer
            Dim descrizione As String

            pos = InStr(1, indirizzo, " ")
            If pos > 0 Then
                descrizione = Mid(indirizzo, pos + 1)
                RicavaDescVia = descrizione
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Source
        End Try
    End Function
    Function chkZeroUno(ByVal chk As CheckBox) As Integer
        chkZeroUno = 0
        If chk.Checked = True Then
            chkZeroUno = 1
        End If
    End Function
    Private Sub UpdateSpRev()
        If classetabSpRev = "tabbertab" Then

            If HFAscensori.Value <> chkZeroUno(Me.ChkAscensori) Or HFRiscaldamento.Value <> chkZeroUno(Me.ChkRiscaldamento) Or HFSpGenerali.Value <> chkZeroUno(ChkSpGenerali) Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then

                par.OracleConn.Open()
                par.SettaCommand(par)
                End If

                'se almeno una chek è cambiata dal valore iniziale, procedo con l'update
                par.cmd.CommandText = " UPDATE SISCOM_MI.UNITA_IMMOBILIARI SET P_ASCENSORE = " & chkZeroUno(Me.ChkAscensori) & " ,P_RISCALDAMENTO = " & chkZeroUno(Me.ChkRiscaldamento) & ",P_SERVIZI_GENERALI = " & chkZeroUno(Me.ChkSpGenerali) & " WHERE ID =  " & vId
                par.cmd.ExecuteNonQuery()
                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F222','')"
                par.cmd.ExecuteNonQuery()
                '*******************************FINE****************************************

                HFAscensori.Value = chkZeroUno(Me.ChkAscensori)
                HFRiscaldamento.Value = chkZeroUno(Me.ChkRiscaldamento)
                HFSpGenerali.Value = chkZeroUno(Me.ChkSpGenerali)

                InsertCarature()

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
        End If
        Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
    End Sub
    Protected Sub ddlAlloggioEscluso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAlloggioEscluso.SelectedIndexChanged
        Try
            AlloggioEscluso()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End If
        End Try
    End Sub
    Private Sub AlloggioEscluso()
        Try
            If ddlAlloggioEscluso.SelectedValue.ToString = "0" Then
                txtDataEsclusione.Text = ""
                txtDataEsclusione.Enabled = False
                txtNrProvvedimentoEsclusione.Text = ""
                txtNrProvvedimentoEsclusione.Enabled = False
            Else
                txtDataEsclusione.Enabled = True
                txtDataEsclusione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtNrProvvedimentoEsclusione.Enabled = True
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub imgAggNota_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgAggNota.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

        'Richiamo la Transazione
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans
        par.CaricaStoricoNote(vId, dataGridNote, "2")
    End Sub

    Protected Sub DataGridNote_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dataGridNote.ItemDataBound
        
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('idNota').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('ImgModifyNota').click();")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('idNota').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('ImgModifyNota').click();")
        End If


   
    End Sub

    Protected Sub ImgModifyNota_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgModifyNota.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

        'Richiamo la Transazione
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans
        par.CaricaStoricoNote(vId, dataGridNote, "2")
    End Sub

    Private Sub InsertCarature()
        'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        'par.SettaCommand(par)

        ''Richiamo la Transazione
        'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ''‘par.cmd.Transaction = par.myTrans

        If ChkAscensori.Checked = True Or ChkRiscaldamento.Checked = True Or ChkSpGenerali.Checked = True Then
            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.CARATURE WHERE ID_UNITA = " & vId
            Dim n As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If n = 0 Then
                '1 --> SERVIZI
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " ID, ID_UNITA, ID_TIPOLOGIA_CARATURA,  " _
                                    & " VALORE_CARATURA, INIZIO_VALIDITA, FINE_VALIDITA, " _
                                    & " MODIFICA_MANUALE, SUP_NETTA, SUP_CATASTALE)  " _
                                    & " VALUES ( " _
                                    & "SISCOM_MI.SEQ_CARATURE.NEXTVAL /* ID */, " _
                                    & vId & " /* ID_UNITA */, " _
                                    & "1 /* ID_TIPOLOGIA_CARATURA */, " _
                                    & "0 /* VALORE_CARATURA */, " _
                                    & "'19000101' /* INIZIO_VALIDITA */, " _
                                    & "'30000000' /* FINE_VALIDITA */, " _
                                    & "1 /* MODIFICA_MANUALE */, " _
                                    & "NULL /* SUP_NETTA */, " _
                                    & "NULL /* SUP_CATASTALE */ ) "
                par.cmd.ExecuteNonQuery()
                '2 --> RISCALDAMENTO
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " ID, ID_UNITA, ID_TIPOLOGIA_CARATURA,  " _
                                    & " VALORE_CARATURA, INIZIO_VALIDITA, FINE_VALIDITA, " _
                                    & " MODIFICA_MANUALE, SUP_NETTA, SUP_CATASTALE)  " _
                                    & " VALUES ( " _
                                    & "SISCOM_MI.SEQ_CARATURE.NEXTVAL /* ID */, " _
                                    & vId & " /* ID_UNITA */, " _
                                    & "2 /* ID_TIPOLOGIA_CARATURA */, " _
                                    & "0 /* VALORE_CARATURA */, " _
                                    & "'19000101' /* INIZIO_VALIDITA */, " _
                                    & "'30000000' /* FINE_VALIDITA */, " _
                                    & "1 /* MODIFICA_MANUALE */, " _
                                    & "NULL /* SUP_NETTA */, " _
                                    & "NULL /* SUP_CATASTALE */ ) "
                par.cmd.ExecuteNonQuery()
                '3 --> ASCENSORE
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " ID, ID_UNITA, ID_TIPOLOGIA_CARATURA,  " _
                                    & " VALORE_CARATURA, INIZIO_VALIDITA, FINE_VALIDITA, " _
                                    & " MODIFICA_MANUALE, SUP_NETTA, SUP_CATASTALE)  " _
                                    & " VALUES ( " _
                                    & "SISCOM_MI.SEQ_CARATURE.NEXTVAL /* ID */, " _
                                    & vId & " /* ID_UNITA */, " _
                                    & "3 /* ID_TIPOLOGIA_CARATURA */, " _
                                    & "0 /* VALORE_CARATURA */, " _
                                    & "'19000101' /* INIZIO_VALIDITA */, " _
                                    & "'30000000' /* FINE_VALIDITA */, " _
                                    & "1 /* MODIFICA_MANUALE */, " _
                                    & "NULL /* SUP_NETTA */, " _
                                    & "NULL /* SUP_CATASTALE */ ) "
                par.cmd.ExecuteNonQuery()
                '4 --> SERVIZI(EDIFICIO)
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " ID, ID_UNITA, ID_TIPOLOGIA_CARATURA,  " _
                                    & " VALORE_CARATURA, INIZIO_VALIDITA, FINE_VALIDITA, " _
                                    & " MODIFICA_MANUALE, SUP_NETTA, SUP_CATASTALE)  " _
                                    & " VALUES ( " _
                                    & "SISCOM_MI.SEQ_CARATURE.NEXTVAL /* ID */, " _
                                    & vId & " /* ID_UNITA */, " _
                                    & "4 /* ID_TIPOLOGIA_CARATURA */, " _
                                    & "0 /* VALORE_CARATURA */, " _
                                    & "'19000101' /* INIZIO_VALIDITA */, " _
                                    & "'30000000' /* FINE_VALIDITA */, " _
                                    & "1 /* MODIFICA_MANUALE */, " _
                                    & "NULL /* SUP_NETTA */, " _
                                    & "NULL /* SUP_CATASTALE */ ) "
                par.cmd.ExecuteNonQuery()
                '5 --> MONTASCALE
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " ID, ID_UNITA, ID_TIPOLOGIA_CARATURA,  " _
                                    & " VALORE_CARATURA, INIZIO_VALIDITA, FINE_VALIDITA, " _
                                    & " MODIFICA_MANUALE, SUP_NETTA, SUP_CATASTALE)  " _
                                    & " VALUES ( " _
                                    & "SISCOM_MI.SEQ_CARATURE.NEXTVAL /* ID */, " _
                                    & vId & " /* ID_UNITA */, " _
                                    & "5 /* ID_TIPOLOGIA_CARATURA */, " _
                                    & "0 /* VALORE_CARATURA */, " _
                                    & "'19000101' /* INIZIO_VALIDITA */, " _
                                    & "'30000000' /* FINE_VALIDITA */, " _
                                    & "0 /* MODIFICA_MANUALE */, " _
                                    & "NULL /* SUP_NETTA */, " _
                                    & "NULL /* SUP_CATASTALE */ ) "
                par.cmd.ExecuteNonQuery()
            End If
        End If
    End Sub
End Class

