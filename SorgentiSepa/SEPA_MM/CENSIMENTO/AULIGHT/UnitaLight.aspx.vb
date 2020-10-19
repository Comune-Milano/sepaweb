
Partial Class CENSIMENTO_AULIGHT_UnitaLight
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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Request.QueryString("ID") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim ValorePassato As String = par.DeCriptaMolto(Request.QueryString("ID"))
        If Mid(ValorePassato, 1, 10) <> Format(Now, "yyyyMMddHH") Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)

            classetab = "tabbertab"
            classetabSpRev = "tabbertab"

            If Not IsPostBack Then

                Dim CodiceUnita As String = Mid(ValorePassato, InStr(ValorePassato, "#") + 1, 17)
                Dim SessioneLavoro As String = Trim(Mid(ValorePassato, InStr(ValorePassato, "@") + 1, 100))

                If Len(CodiceUnita) = 17 Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT * FROM AU_LIGHT_CONCESSIONI WHERE CODICE='" & SessioneLavoro & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = False Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Redirect("~/AccessoNegato.htm", True)
                        Exit Sub
                    Else
                        par.cmd.CommandText = "DELETE FROM AU_LIGHT_CONCESSIONI WHERE CODICE='" & SessioneLavoro & "'"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT  ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & CodiceUnita & "'"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        vId = myReaderA(0)
                    End If
                    myReaderA.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                If vId = 0 Then
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Redirect("~/AccessoNegato.htm", True)
                    Exit Sub
                Else
                    Response.Flush()
                    Me.Riempicampi()
                    ApriRicerca()
                End If
            End If

            FrmSolaLettura()
            maxSLE.Value = "1"
            Me.chkPertinenze.Enabled = False
            ChkAscensori.Enabled = False
            ChkRiscaldamento.Enabled = False
            ChkSpGenerali.Enabled = False

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

        VerificaModificheSottoform()
        Me.txtindietro.Value = txtindietro.Value - 1
       

        If Me.DrLDisponib.SelectedValue = "VEND" Then

            visualizzaSM.Value = 0
        End If

    End Sub

    Public Sub VerificaModificheSottoform()
        'Se vengono effettuate modifiche nei sotto-form questo manda il messaggio in casa di uscita senza salvataggio
        If Session.Item("MODIFICASOTTOFORM") = 1 Then
            Me.txtModificato.Value = 1
            Session.Item("MODIFICASOTTOFORM") = 0
        End If

    End Sub

    Private Sub SolaLetturaDaRilievo()
        Try

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
            Session.Add("SLE", 1)
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub FrmSolaLettura()
        Try
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

            Session.Add("SLE", 1)
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
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

            par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            cmbComplesso.SelectedValue = "-1"

            
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


            Me.CaricaEdifici()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try

        'Dim IDED As String = Request.QueryString("IDED")
        'If IDED > 0 Then
        '    Me.DrLEdificio.SelectedValue = IDED
        '    DrlSc.Items.Clear()
        '    scala()
        '    LivelloPiano()
        '    'TrovaFogSez()
        '    Me.DrLEdificio.Enabled = False
        '    ComplessoAssociato()
        '    Me.cmbComplesso.Enabled = False
        '    TrovaFogSez()
        'End If



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
                'If Session.Item("CONT_DISDETTE") = "1" Then
                '    'ImgBtnVerStatManut.Visible = True
                '    visualizzaSM.value = "1"
                'Else
                '    'ImgBtnVerStatManut.Visible = False
                '    visualizzaSM.value = "0"

                'End If

                visualizzaSM.Value = "0"
                ''SE SI PROVIENE DA UNA RICERCA DOPO AVER RIEMPITO I CAMPI SETTO LA MIA CONNESSIONE CHE RESTERà VALIDA PER TUTTA LA DURATA DI APERTURA DEL FORM
                'If Request.QueryString("LE") <> "1" And Session("PED2_SOLOLETTURA") <> "1" Then
                '    STRAPPOGGIO = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                'Else
                '    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId
                '    'par.OracleConn.Close()
                '    ApriFrmWithDBLock()
                '    Exit Sub
                'End If

                par.OracleConn.Open()
                par.SettaCommand(par)
                STRAPPOGGIO = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & vId
                par.cmd.CommandText = STRAPPOGGIO
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)


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
                        DisattivaCampiCatastali()
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
                    'testoTabella = testoTabella _
                    '            & "<tr>" _
                    '            & "<td style='height: 19px'>" _
                    '            & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&PERT=1&ID=" & par.IfNull(myReader2("cod_unita_immobiliare"), "") & "','Pertin" & myReader2("cod_unita_immobiliare") & "','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & par.IfNull(myReader2("cod_unita_immobiliare"), "") & "</a></span></td>" _
                    '            & "<td style='height: 19px'>" _
                    '            & "</td>" _
                    '            & "</tr>"
                    testoTabella = testoTabella _
                               & "<tr>" _
                               & "<td style='height: 19px'>" _
                               & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("cod_unita_immobiliare"), "") & "</span></td>" _
                               & "<td style='height: 19px'>" _
                               & "</td>" _
                               & "</tr>"
                Loop
                myReader2.Close()
                LblPertinenze.Text = testoTabella & "</table>"
                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                'Try
                '    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Then
                '        Me.btnFoto.Visible = True
                '        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                '    Else
                '        Me.btnFoto.Visible = True
                '        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                '    End If
                'Catch ex As Exception
                '    Me.LblErrore.Visible = True
                '    LblErrore.Text = "ATTENZIONE!Verificare il percorso delle foto e delle planimetrie!"
                'End Try
                '*********************FINE CONTROLLO PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********


                'Apro una nuova transazione
                'Session.Item("LAVORAZIONE") = "1"
                'par.myTrans = par.OracleConn.BeginTransaction()
                'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Catch ex As Exception
                par.OracleConn.Close()
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try
        End If

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

    Private Sub AttivaCampiPulsanti()
        visualizzaSM.Value = "0"
    End Sub

    Private Sub DisattivaCampiPulsanti()

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

    Private Sub FiltraEdifici()
        Try
            Dim connopennow As Boolean = False

            If Me.cmbComplesso.SelectedValue <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    'Richiamo la Transazione
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans
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

    Private Sub caricaPertinenze()
        Try
            Dim Apertadesso As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                Apertadesso = True
            End If
            Me.cmbPertinenza.Items.Clear()
            cmbPertinenza.Items.Add(New ListItem(" ", -1))
            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString
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


                'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.RAPPORTI_UTENZA WHERE ID_UNITA =" & vId & " AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO ORDER BY DATA_DECORRENZA DESC"
                'Dim myReaderPepp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReaderPepp.Read Then
                '    LblDatiContratt.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderPepp("ID") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">Dati Contrattuali</a>"
                'Else
                '    'LblDatiContratt.Text = "<a href='DatiContratto.aspx?ID=" & vId & "&UI=" & par.IfNull(dt.Rows(0).Item("COD_UNITA_IMMOBILIARE"), "") & "' target='_blank'>Dati Contrattuali</a>"
                '    LblDatiContratt.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "alert('Nessun Contratto stipulato su questa unità!');" & Chr(34) & ">Dati Contrattuali</a>"
                'End If
                'myReaderPepp.Close()

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
                FrmSolaLettura()
                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                'Try
                '    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/UI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodUnitImm.Text) & "*.*").Count > 0 Then
                '        Me.btnFoto.Visible = True
                '        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                '    Else
                '        Me.btnFoto.Visible = True
                '        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                '    End If
                'Catch ex As Exception
                '    Me.LblErrore.Visible = True
                '    LblErrore.Text = "ATTENZIONE!Verificare il percorso delle foto e delle planimetrie!"
                'End Try
                '*********************FINE CONTROLLO PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
               
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try
        End If
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

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        tabdefault1 = ""
        tabdefault2 = ""
        tabdefault3 = ""
        tabdefault4 = ""
        tabdefault5 = ""
        'tabdefault6 = ""
        'tabdefault7 = ""

        Select Case txttab.Value
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
        End Select
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

    Protected Sub ImButEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImButEsci.Click
        Try
            If Session.Item("SLE") = "1" Then
                Session.Remove("SLE")
            End If
            Response.Write("<script>self.close();</script>")
        Catch ex As Exception

        End Try
    End Sub
End Class
