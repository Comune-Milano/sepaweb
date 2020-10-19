Imports System.IO
Imports System.Collections.Generic

Partial Class Contratti_TrasformaContratto2
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public percentuale As Long = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Not IsPostBack Then
            'idDich.Value = Request.QueryString("IDDICH")
            idContratto.Value = Request.QueryString("IDCONT")
            cercaDich392()
            RicavaAreaEconomica() 

            If ControllaEsisteRU() = False Then
                CercaBollette()
            Else
                btnStampaDoc.Visible = False
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey2A", "alert('Attenzione! La procedura di trasformazione contratto risulta completa!');", True)
            End If

        End If
        If confermaRifiuto.Visible = True Then
            If cmbConfermaRifiuto.SelectedValue = "1" Then
                confermaRifiuto.Value = "1"
            Else
                indennita.Value = "0"
                confermaRifiuto.Value = "0"
            End If
        End If
        Me.txtImportoAnnuo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
        Me.txtImportoAnnuo.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);")
        Me.txtDataSloggio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Public Property vIdBolletta() As String
        Get
            If Not (ViewState("par_IdBolletta") Is Nothing) Then
                Return CStr(ViewState("par_IdBolletta"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdBolletta") = value
        End Set

    End Property

    Private Function ControllaEsisteRU() As Boolean
        Dim esisteRU As Boolean = False
        Try
            connData.apri()

            par.cmd.CommandText = "SELECT * FROM siscom_mi.UNITA_ASSEGNATE WHERE " _
                                & "id_dichiarazione=" & idDich.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                esisteRU = True
            End If
            lettore.Close()

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", Page.Title & " CercaBollette - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return esisteRU

    End Function

    Private Sub cercaDich392()
        Try
            connData.apri()
            par.cmd.CommandText = "select id_dichiarazione from UTENZA_DICH_CANONI_EC where id_Contratto=" & idContratto.Value & " order by id desc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                idDich.Value = par.IfNull(myReader1("id_dichiarazione"), 0)
            End If
            myReader1.Close()

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", Page.Title & " CercaBollette - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CercaBollette()
        Try
            Dim saldo As Decimal = 0
            Dim credito As Decimal = 0
            Dim debito As Decimal = 0

            debito = par.CalcolaSaldoAttuale(idContratto.Value)
            
            connData.apri()

            par.cmd.CommandText = "select sum(importo_totale) AS SALDOGEST from siscom_mi.bol_bollette_gest where tipo_Applicazione='N' and id_tipo<>55 and id_Contratto=" & idContratto.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            credito = par.IfNull(par.cmd.ExecuteScalar, 0)
           
            saldo = debito + credito

            lblSaldo.Text = Format(saldo, "##,##0.00") & "€."

            If saldo < 0 Then
                lblDebitoAccTitolo.Visible = False
                cmbDebitoAccettato.Visible = False
                btnStampaDoc.Items(0).ChildItems.RemoveAt(0)
                'lblSaldo.Text = "0.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Credito pari a " & Format(credito, "##,##0.00") & "€."
                btnNewContr.Style.Value = "visibility: visible;"
                accettazioneDeb.Value = "1"
                lblDataSl.Style.Value = "visibility: visible;"
                txtDataSloggio.Style.Value = "visibility: visible;"
            End If
            If saldo = 0 Then
                lblDebitoAccTitolo.Visible = False
                cmbDebitoAccettato.Visible = False
                btnStampaDoc.Items(0).ChildItems.RemoveAt(0)
                lblSaldo.Text = "0"
                btnNewContr.Style.Value = "visibility: visible;"
                accettazioneDeb.Value = "1"
                lblDataSl.Style.Value = "visibility: visible;"
                txtDataSloggio.Style.Value = "visibility: visible;"
            End If

            par.cmd.CommandText = "SELECT bol_bollette.ID " _
                                & "FROM SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette " _
                                & "WHERE BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) " _
                                & "AND bol_bollette.id_contratto=" & idContratto.Value & "  " _
                                & "AND bol_bollette.fl_annullata = 0 " _
                                & "AND bol_bollette.id_bolletta_ric IS NULL " _
                                & "AND BOL_BOLLETTE.ID_TIPO <> 5 " _
                                & "AND IMPORTO_RUOLO = 0 " _
                                & "AND nvl(bol_bollette.importo_totale,0) >nvl(bol_bollette.importo_pagato,0) " _
                                & "AND bol_bollette.id_rateizzazione is null " _
                                & "ORDER BY bol_bollette.data_emissione DESC,BOL_BOLLETTE.ANNO " _
                                & "DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.ID DESC"
            Dim dt As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            da.Dispose()
            Dim primo As Boolean = True
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    If primo = True Then
                        vIdBolletta = row.Item("ID")
                        primo = False
                    Else
                        vIdBolletta = vIdBolletta & "," & row.Item("ID")
                    End If
                Next
                'Else
                '    lblDebitoAccTitolo.Visible = False
                '    cmbDebitoAccettato.Visible = False
                '    btnStampaDoc.Items(0).ChildItems.RemoveAt(0)
                '    lblSaldo.Text = "Nessun debito presente"
                '    btnNewContr.Style.Value = "visibility: visible;"
                '    accettazioneDeb.Value = "1"

            End If
            Session.Add("IDBOLLETTE", vIdBolletta)
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", Page.Title & " CercaBollette - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnNewContr_Click(sender As Object, e As System.EventArgs) Handles btnNewContr.Click

        Select Case accettazioneDeb.Value
            Case "-1"
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey2A", "alert('Attenzione! Valorizzare l\'accettazione del debito!')", True)
            Case "0"
                'Calcola 200% valore locativo
                CalcolaCanoneST()
            Case "1"
                If RicavaAreaEconomica() = 4 Then

                    lblImportAnnuo.Visible = True
                    txtImportoAnnuo.Style.Value = "visibility: visible;"
                    contratto431.Value = "1"
                    btnL43198.Visible = True

                    lblDebitoAccTitolo.Visible = False
                    cmbDebitoAccettato.Visible = False
                    lblSaldo.Visible = False
                    lblTitoloSaldo.Visible = False
                    btnNewContr.Visible = False
                    lblDataSl.Style.Value = "visibility: hidden;"
                    txtDataSloggio.Style.Value = "visibility: hidden;"
                Else
                    If conferma.Value = "1" Then
                        CreaNuovoContratto()
                        '  ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "confST", "CloseModal(" & dataRicons.Value & ");", True)
                    End If
                End If
        End Select


    End Sub

    Private Function RicavaAreaEconomica() As Integer
        Try
            connData.apri()
            par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_calcolo desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                idAreaEconomica.Value = par.IfNull(myReader("id_area_economica"), "")
            End If
            myReader.Close()

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", Page.Title & " RicavaAreaEconomica - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

        Return idAreaEconomica.Value
    End Function

    Private Sub CalcolaCanoneST()
        Try
            Dim unitaContratto As Long = 0
            Dim canone As Decimal = 0

            connData.apri(False)

            par.cmd.CommandText = "SELECT ID_UNITA,imp_Canone_iniziale FROM SISCOM_MI.UNITA_CONTRATTUALE,siscom_mi.rapporti_utenza WHERE UNITA_CONTRATTUALE.id_contratto=rapporti_utenza.id and ID_CONTRATTO=" & idContratto.Value & " and id_unita_principale is null"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                unitaContratto = par.IfNull(lettore("ID_UNITA"), "")
                canone = par.IfNull(lettore("imp_Canone_iniziale"), 0)
            End If
            lettore.Close()

            connData.chiudi(False)

            ' par.CalcolaCanoneAbusivi(unitaContratto, canone, 1)

            lblTitoloCanone.Visible = True
            lblCanone.Visible = True
            indennita.Value = canone
            lblCanone.Text = Format(canone, "##,##0.00") & "€."

            lblDebitoAccTitolo.Visible = False
            cmbDebitoAccettato.Visible = False
            lblSaldo.Visible = False
            lblTitoloSaldo.Visible = False
            btnNewContr.Visible = False

            lblConfermaRifiuto.Visible = True
            cmbConfermaRifiuto.Visible = True
            btnTrasformainST.Visible = True

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", Page.Title & " CreaNuovoContrattoERP - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CreaNuovoContratto()
        Try

            Dim DESTINAZIONE As String = ""

            Dim Risoluzione As Boolean = False
            Dim importoRisoluzione As Double = 0
            Dim IMPORTOINTERESSI As Double = 0
            Dim sAggiunta As String = ""
            Dim DataCalcolo As String = ""
            Dim DataInizio As String = ""

            Dim tasso As Double = 0
            Dim baseCalcolo As Double = 0

            Dim Giorni As Integer = 0
            Dim GiorniAnno As Integer = 0
            Dim dataPartenza As String = ""

            Dim Totale As Double = 0
            Dim TotalePeriodo As Double = 0
            Dim indice As Long = 0
            Dim DataFine As String = ""
            Dim num_bolletta As String = ""
            Dim importobolletta As Double = 0
            Dim I As Integer = 0

            Dim importodanni As Double = 0
            Dim importotrasporto As Double = 0

            Dim Interessi As New SortedDictionary(Of Integer, Double)

            Dim RIASSUNTO_BOLLETTA As String = ""
            Dim lIdConnessione As String = ""

            Dim sNuovoCodiceRapporto As String = ""
            Dim lNuovoIdRapporto As String = ""
            Dim UnitaContratto As Long

            'Dim RinnovoDataChiusura As String = ""
            Dim RinnovoDataPG As String = ""
            Dim RinnovoNumeroPG As String = ""
            'Dim RinnovoCanone As String = ""
            Dim FaiCambioBox As String = "1"
            Dim CFNuovoIntest As String = ""
            Dim CognomeNome As String = ""
            Dim TipoIndir As String = ""
            Dim Indirizzo As String = ""
            Dim Civico As String = ""
            Dim LuogoRec As String = ""
            Dim Sigla As String = ""
            Dim Cap As String = ""
            Dim INDICE_CONTRATTO As Long = 0
            Dim codContratto As String = ""
            Dim canoneAnnuo As Decimal = 0
            Dim codFis As String = ""
            Dim dataSloggio392 As String = txtDataSloggio.Text


            connData.apri(True)

            par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_calcolo desc"
            Dim myReaderEC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderEC.Read Then
                canoneAnnuo = par.IfNull(myReaderEC("CANONE"), "0")
                codContratto = par.IfNull(myReaderEC("cod_contratto"), "")
            End If
            myReaderEC.Close()

            par.cmd.CommandText = "select * from UTENZA_COMP_NUCLEO where id_dichiarazione=" & idDich.Value & " AND PROGR=0"
            Dim myReaderUD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderUD.Read Then
                codFis = par.IfNull(myReaderUD("COD_FISCALE"), "0")
            End If
            myReaderUD.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & codContratto & "'"
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            da1.Fill(dt1)
            RinnovoDataPG = par.IfNull(dt1.Rows(0).Item("DATA_DELIBERA"), "")
            RinnovoNumeroPG = par.IfNull(dt1.Rows(0).Item("DELIBERA"), "")


            Dim aggiornamento_istat As Double = 0
            Dim AltriAdeguamenti As Double = 0

            par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo=2 and ID_CONTRATTO=" & idContratto.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                aggiornamento_istat = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo<>2 and ID_CONTRATTO=" & idContratto.Value
            lettore = par.cmd.ExecuteReader()
            If lettore.Read Then
                AltriAdeguamenti = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContratto.Value & " and id_unita_principale is null"
            lettore = par.cmd.ExecuteReader()
            If lettore.Read Then
                UnitaContratto = par.IfNull(lettore("ID_UNITA"), "")
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT * FROM utenza_dichiarazioni,utenza_comp_nucleo WHERE utenza_dichiarazioni.ID = utenza_comp_nucleo.ID_DICHIARAZIONE AND utenza_dichiarazioni.ID=" & idDich.Value & " AND PROGR=0"
            lettore = par.cmd.ExecuteReader()
            If lettore.Read Then

                CFNuovoIntest = par.IfNull(lettore("COD_FISCALE"), "")

                CognomeNome = Trim(par.IfNull(lettore("COGNOME"), "")) & " " & Trim(par.IfNull(lettore("NOME"), ""))

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID='" & par.IfNull(lettore("ID_LUOGO_RES_DNTE"), "") & "'"
                Dim myReaderIDluogoRES As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderIDluogoRES.Read Then
                    Sigla = par.IfNull(myReaderIDluogoRES("SIGLA"), "")
                    LuogoRec = par.PulisciStrSql(par.IfNull(myReaderIDluogoRES("NOME"), ""))
                End If
                myReaderIDluogoRES.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD='" & par.IfNull(lettore("ID_TIPO_IND_RES_DNTE"), "") & "'"
                Dim myReaderTipoIndir As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderTipoIndir.Read Then
                    TipoIndir = par.IfNull(myReaderTipoIndir("DESCRIZIONE"), "")
                End If
                myReaderTipoIndir.Close()

                Indirizzo = par.PulisciStrSql(par.IfNull(lettore("IND_RES_DNTE"), ""))
                Civico = par.PulisciStrSql(par.IfNull(lettore("CIVICO_RES_DNTE"), ""))
                Cap = par.IfNull(lettore("CAP_RES_DNTE"), "")
            End If
            lettore.Close()


            'If data_riconsegna <> "" Then

            Dim IdAnagRichiedente As String = ""
            par.cmd.CommandText = "SELECT ID_ANAGRAFICA FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE id_contratto=" & idContratto.Value & " and cod_tipologia_occupante='INTE'"
            Dim myReaderIdA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderIdA.Read Then
                IdAnagRichiedente = myReaderIdA("ID_ANAGRAFICA")
            End If
            myReaderIdA.Close()

            Dim progressivo As Integer
            par.cmd.CommandText = "select MAX(TO_NUMBER(TRANSLATE(SUBSTR(cod_contratto,18,2),'A','0'))) from SISCOM_MI.rapporti_utenza,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari where id_unita=" & UnitaContratto & " and rapporti_utenza.id=unita_contrattuale.id_contratto and unita_contrattuale.id_unita=siscom_mi.unita_immobiliari.id"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                progressivo = par.IfNull(myReader1(0), 0) + 1
            Else
                progressivo = 1
            End If
            myReader1.Close()
            Dim NuovoCodiceContratto As String = Mid(codContratto, 1, 17) & Format((progressivo), "00")

            Dim nome As String = ""
            Dim cognome As String = ""
            Dim CF As String = ""
            Dim idAnagr As Long = 0
            Dim provenienza As String = ""
            Dim provenienzaAss As String = ""
            Dim codTipoContr As String = ""
            Dim destUSO As String = ""
            Dim durataAnni As Integer = 0
            Dim durataRinnovo As Integer = 0

            If contratto431.Value = "1" Then
                provenienza = "W"
                provenienzaAss = "6"
                codTipoContr = "L43198"
                canoneAnnuo = CDec(txtImportoAnnuo.Text)
                destUSO = "0"
                durataAnni = 1
                durataRinnovo = 0
            Else
                provenienza = "U"
                provenienzaAss = "1"
                codTipoContr = "ERP"
                destUSO = "R"
                durataAnni = 4
                durataRinnovo = 4
            End If

            'If codFis = "-1" Then
            '    par.cmd.CommandText = "select anagrafica.* from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND soggetti_contrattuali.id_CONTRATTO=" & idContratto.Value
            'Else
            '    par.cmd.CommandText = "select anagrafica.* from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND anagrafica.cod_fiscale='" & UCase(par.PulisciStrSql(codFis)) & "'"
            'End If
            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader ' = par.cmd.ExecuteReader()

            'If myReaderX.Read = True Then
            '    If par.IfNull(myReaderX("ragione_sociale"), "") = "" Then
            '        Cognome = par.IfNull(myReaderX("cognome"), "")
            '        nome = par.IfNull(myReaderX("nome"), "")
            '    Else
            '        Cognome = par.IfNull(myReaderX("ragione_sociale"), "")
            '        nome = ""
            '    End If
            '    If par.IfNull(myReaderX("partita_iva"), "") = "" Then
            '        CF = par.IfNull(myReaderX("cod_fiscale"), "")
            '    Else
            '        CF = par.IfNull(myReaderX("partita_iva"), "")
            '    End If


            '    par.cmd.CommandText = "Insert into siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, " _
            '                        & "GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,CANONE,ID_ANAGRAFICA,PROVVEDIMENTO,DATA_PROVVEDIMENTO) " _
            '                        & " Values " _
            '                        & "(-1, " & UnitaContratto & ", '" & RinnovoDataPG & "', 0, " & idDich.Value & ", " _
            '                        & "'" & par.PulisciStrSql(Cognome) & "', '" & par.PulisciStrSql(nome) _
            '                        & "', '" & par.PulisciStrSql(CF) & "', '" & provenienza & "', 0," & par.VirgoleInPunti(canoneAnnuo) & "," & par.IfNull(myReaderX("id"), "0") & ",'" & RinnovoNumeroPG & "','" & RinnovoDataPG & "')"
            '    par.cmd.ExecuteNonQuery()
            'End If
            'myReaderX.Close()

            Dim proxBolletta As String = ""
            Dim numRegistr As String = ""
            Dim serieReistr As String = ""
            Dim dataRegistr As String = ""
            Dim dataScadenza As String = ""
            Dim dataScadenza2 As String = ""
            Dim durataMesi As Integer = 0
            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA FROM SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID=" & idContratto.Value
            myReaderX = par.cmd.ExecuteReader()
            If myReaderX.Read = True Then
                If codTipoContr = "ERP" Then
                    dataScadenza = par.AggiustaData(Date.Parse(dataSloggio392, New System.Globalization.CultureInfo("it-IT", False)).AddYears(4).ToString("dd/MM/yyyy"))
                    dataScadenza = par.AggiustaData(Date.Parse(par.FormattaData(dataScadenza), New System.Globalization.CultureInfo("it-IT", False)).AddDays(-1).ToString("dd/MM/yyyy"))

                    dataScadenza2 = par.AggiustaData(Date.Parse(par.FormattaData(dataScadenza), New System.Globalization.CultureInfo("it-IT", False)).AddYears(4).ToString("dd/MM/yyyy"))
                Else
                    dataScadenza = par.AggiustaData(Date.Parse(dataSloggio392, New System.Globalization.CultureInfo("it-IT", False)).AddMonths(18).ToString("dd/MM/yyyy"))
                    dataScadenza = par.AggiustaData(Date.Parse(par.FormattaData(dataScadenza), New System.Globalization.CultureInfo("it-IT", False)).AddDays(-1).ToString("dd/MM/yyyy"))

                    dataScadenza2 = dataScadenza
                    durataMesi = 18
                End If

                Dim origineContratto As String = ""

                If codTipoContr = "ERP" Then
                    origineContratto = "2"
                Else
                    origineContratto = "NULL"
                End If

                par.cmd.CommandText = "Insert into SISCOM_MI.RAPPORTI_UTENZA    (ID, COD_CONTRATTO_GIMI, COD_CONTRATTO, COD_TIPOLOGIA_RAPP_CONTR, COD_TIPOLOGIA_CONTR_LOC," _
                                    & "DURATA_ANNI, DURATA_MESI, DURATA_GIORNI, DATA_DECORRENZA, DATA_DECORRENZA_AE, DATA_SCADENZA,     DATA_DISDETTA_LOCATARIO, NUM_REGISTRAZIONE, SERIE_REGISTRAZIONE, " _
                                    & "IMP_CANONE_INIZIALE, IMP_DEPOSITO_CAUZ,     COD_FASCIA_REDDITO, MESSA_IN_MORA, PRATICA_AL_LEGALE, ISCRIZIONE_RUOLO, RATEIZZAZIONI_IN_CORSO,     " _
                                    & "DECADENZA, SFRATTO, NOTE, DATA_REG, COD_UFFICIO_REG,     DATA_STIPULA, DATA_CONSEGNA, DATA_SCADENZA_RINNOVO, DURATA_RINNOVO, MESI_DISDETTA,     " _
                                    & "DATA_RICONSEGNA, DELIBERA, DATA_DELIBERA, NRO_RATE, FREQ_VAR_ISTAT,     VERSAMENTO_TR, PER_BANDO, TIPO_COR, LUOGO_COR, VIA_COR,     " _
                                    & "NOTE_COR, CIVICO_COR, SIGLA_COR, CAP_COR, PRESSO_COR,     BOZZA, INTERESSI_CAUZIONE, NRO_REPERTORIO, DATA_REPERTORIO, NRO_ASSEGNAZIONE_PG,     " _
                                    & "DATA_ASSEGNAZIONE_PG, INIZIO_PERIODO, LIBRETTO_DEPOSITO, ID_DEST_RATE, INVIO_BOLLETTA,     FL_CONGUAGLIO, PERC_RINNOVO_CONTRATTO, PERC_ISTAT, " _
                                    & "IMP_CANONE_ATTUALE, PROVENIENZA_ASS,     INTERESSI_RIT_PAG, IMPORTO_ANTICIPO, ID_DOMANDA, ID_AU, ID_ISEE,     ID_COMMISSARIATO, REG_TELEMATICA, " _
                                    & "DEST_USO, DESCR_DEST_USO, DATA_INVIO_RIC_DISDETTA,MOTIVO_REC_FORZOSO, FL_STAMPATO, BOLLO, N_OFFERTA, DATA_NOTIFICA_DISDETTA,     " _
                                    & "MITTENTE_DISDETTA, DATA_CONVALIDA_SFRATTO, DATA_ESECUZIONE_SFRATTO, DATA_RINVIO_SFRATTO, DATA_CONFERMA_FP, SINDACATO,ID_ORIGINE_CONTRATTO) " _
                                    & "Values   (SISCOM_MI.SEQ_RAPPORTI_UTENZA.NEXTVAL, '', '" & NuovoCodiceContratto & "', '" _
                                    & "LEGIT', '" & codTipoContr & "'" _
                                    & "," & durataAnni & ", " & durataMesi & " " _
                                    & ", NULL, '" & par.AggiustaData(dataSloggio392) & "','" & par.AggiustaData(dataSloggio392) & "' ,'" & dataScadenza & "',  '' , '" & numRegistr & "', '" & serieReistr & "', " & par.VirgoleInPunti(canoneAnnuo) & ",0,  NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, '" & dataRegistr & "', '" _
                                    & "" & par.IfNull(myReaderX("COD_UFFICIO_REG"), "TNP") & "',  '" & par.AggiustaData(dataSloggio392) & "', '" & par.AggiustaData(dataSloggio392) & "', '" & dataScadenza2 & "', " & durataRinnovo & "" _
                                    & ", " & par.IfNull(myReaderX("MESI_DISDETTA"), "6") & ",     NULL, '" & RinnovoNumeroPG _
                                    & "', '" & RinnovoDataPG & "', 12, '" & par.IfNull(myReaderX("FREQ_VAR_ISTAT"), "") _
                                    & "' ,'" & par.IfNull(myReaderX("VERSAMENTO_TR"), "NULL") & "', NULL, '" & par.IfNull(myReaderX("TIPO_COR"), "VIA") _
                                    & "', '" & par.PulisciStrSql(par.IfNull(myReaderX("LUOGO_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("VIA_COR"), "")) _
                                    & "', NULL, '" & par.PulisciStrSql(par.IfNull(myReaderX("CIVICO_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("SIGLA_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("CAP_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("PRESSO_COR"), "")) & "',1, 1, NULL, NULL, NULL,     NULL, '" _
                                    & par.IfNull(myReaderX("PROSSIMA_BOLLETTA"), "") & "', '" & par.IfNull(myReaderX("LIBRETTO_DEPOSITO"), "") _
                                    & "', " & par.IfNull(myReaderX("ID_DEST_RATE"), "1") & ", " & par.IfNull(myReaderX("INVIO_BOLLETTA"), "1") _
                                    & ",'" & par.IfNull(myReaderX("FL_CONGUAGLIO"), "1") & "', " & par.IfNull(myReaderX("PERC_RINNOVO_CONTRATTO"), "0") _
                                    & ", " & par.IfNull(myReaderX("PERC_ISTAT"), "0") & ", " & par.VirgoleInPunti(canoneAnnuo) _
                                    & ", " & provenienzaAss & " ,     '1', 0, " & par.IfNull(myReaderX("ID_DOMANDA"), "NULL") _
                                    & ", " & par.IfNull(myReaderX("ID_AU"), "NULL") & ", " & par.IfNull(myReaderX("ID_ISEE"), "NULL") _
                                    & "," & par.IfNull(myReaderX("ID_COMMISSARIATO"), "1") & ", NULL, '" & destUSO _
                                    & "', '" & par.PulisciStrSql(par.IfNull(myReaderX("DESCR_DEST_USO"), "0")) _
                                    & "', NULL, 4, 0, NULL, 0, NULL,     -1, NULL, NULL, NULL, NULL," & par.IfNull(myReaderX("SINDACATO"), "NULL") & "," & origineContratto & ")"
                par.cmd.ExecuteNonQuery()

                proxBolletta = par.IfNull(myReaderX("PROSSIMA_BOLLETTA"), "")
            End If
            myReaderX.Close()


            par.cmd.CommandText = "select siscom_mi.seq_RAPPORTI_UTENZA.currval from dual"
            Dim myReaderX2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX2.Read Then
                INDICE_CONTRATTO = myReaderX2(0)
            End If
            myReaderX2.Close()

            idnuovoContratto.Value = INDICE_CONTRATTO

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL (ID_CONTRATTO,PROSSIMA_BOLLETTA) VALUES (" & INDICE_CONTRATTO & ",'" & proxBolletta & "')"
            par.cmd.ExecuteNonQuery()


            Dim s As String
            Dim comunicazioni As String = ""
            Dim LBLNOMEFILECANONE As String = ""
            Dim fileName As String = NuovoCodiceContratto & ".txt"

            codContr.Value = NuovoCodiceContratto
            's = par.CalcolaCanone27(lIdDomanda, 3, UnitaContratto, NuovoCodiceContratto, CanoneCorrente, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, annoSitEconom)
            'If comunicazioni <> "" Then
            '    Response.Write("<script>alert('" & comunicazioni & "');</script>")
            'End If

            'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName, False, System.Text.Encoding.Default)
            'sr.WriteLine(s)
            'sr.Close()
            'LBLNOMEFILECANONE = Server.MapPath("..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName


            'If System.IO.File.Exists(LBLNOMEFILECANONE) = True Then

            '    Dim sr1 As StreamReader = New StreamReader(LBLNOMEFILECANONE, System.Text.Encoding.GetEncoding("iso-8859-1"))
            '    Dim contenuto As String = sr1.ReadToEnd()
            '    sr1.Close()

            '    If sNOTE = "" Then
            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, LIMITE_PENSIONI, " _
            '                            & "ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,DEM, SUPCONVENZIONALE, COSTOBASE, ZONA, PIANO, CONSERVAZIONE, VETUSTA,INCIDENZA_ISE," _
            '                            & "COEFF_NUCLEO_FAM,SOTTO_AREA,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT," _
            '                            & "NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE," _
            '                            & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91,INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN) " _
            '                            & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'','" & NuovoCodiceContratto & "'," & INDICE_CONTRATTO & ",'" & Format(Now, "yyyyMMddHHmmss") _
            '                            & "'," & AreaEconomica & ",'" & sISEE & "','" & sISE & "','" & sISR & "','" & sISP & "','" & sPSE & "','" & sVSE & "','" & sREDD_DIP & "','" _
            '                            & sREDD_ALT & "','" & sLimitePensione & "','" & sISE_MIN & "','" & sPER_VAL_LOC & "','" & sPERC_INC_MAX_ISE_ERP & "','" & sCanone & "',:TESTO,'" _
            '                            & par.PulisciStrSql(sNOTE) & "',NULL,'" & sDEM & "','" & sSUPCONVENZIONALE & "','" & sCOSTOBASE & "','" & sZONA & "','" & sPIANO & "','" _
            '                            & sCONSERVAZIONE & "','" & sVETUSTA & "','" & sINCIDENZAISE & "','" & sCOEFFFAM & "','" & sSOTTOAREA & "','" & sMOTIVODECADENZA & "'," _
            '                            & "0" & ",0," & "0" & "," & "null" _
            '                            & ",'" & "0" & "','" & "0" & "','" _
            '                            & "0" & "'," & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
            '                            & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" & par.PulisciStrSql(sLOCALITA) _
            '                            & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','0','" & sANNOINIZIOVAL & "','" & sANNOFINEVAL & "') "
            '    Else
            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI," _
            '                            & "LIMITE_PENSIONI, ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE," _
            '                            & "CANONE_ATTUALE,ADEGUAMENTO,ISTAT,NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI," _
            '                            & "REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE,LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91,INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN) " _
            '                            & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'','" & NuovoCodiceContratto & "'," & INDICE_CONTRATTO & ",'" & Format(Now, "yyyyMMddHHmmss") & "',NULL,'','','','','','','','','','','','','',:TESTO,'" & _
            '                            par.PulisciStrSql(sNOTE) & "',NULL,'" & sMOTIVODECADENZA & "',0,0,0," _
            '                            & "null" & ",'0','0" _
            '                            & "','0'," & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
            '                            & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" _
            '                            & par.PulisciStrSql(sLOCALITA) & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','0','" & sANNOINIZIOVAL & "','" & sANNOFINEVAL & "') "

            '    End If

            '    Dim objStream As Stream = File.Open(LBLNOMEFILECANONE, FileMode.Open)
            '    Dim buffer(objStream.Length) As Byte
            '    objStream.Read(buffer, 0, objStream.Length)
            '    objStream.Close()

            '    Dim parmData As New Oracle.DataAccess.Client.OracleParameter
            '    With parmData
            '        .Direction = Data.ParameterDirection.Input
            '        .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
            '        .ParameterName = "TESTO"
            '        .Value = buffer
            '    End With

            '    par.cmd.Parameters.Add(parmData)
            '    par.cmd.ExecuteNonQuery()
            '    System.IO.File.Delete(LBLNOMEFILECANONE)
            '    par.cmd.Parameters.Remove(parmData)

            '    buffer = Nothing
            '    objStream = Nothing
            'End If

            If contratto431.Value = "0" Then
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CANONI_EC ( " _
                    & " ID_CONTRATTO, DATA_CALCOLO, TESTO, " _
                    & " ID_AREA_ECONOMICA, ISEE, ISE,  " _
                    & " ISR, ISP, PSE,  " _
                    & " VSE, REDDITI_DIP, REDDITI_ATRI,  " _
                    & " LIMITE_PENSIONI, ISEE_27, PERC_VAL_LOC,  " _
                    & " INC_MAX, INCIDENZA_ISE, CANONE,  " _
                    & " ID_BANDO_AU, NOTE, DEM,  " _
                    & " SUPCONVENZIONALE, COSTOBASE, ZONA,  " _
                    & " PIANO, CONSERVAZIONE, VETUSTA,  " _
                    & " COEFF_NUCLEO_FAM, SOTTO_AREA, ANNOTAZIONI,  " _
                    & " PATRIMONIO_SUP, NON_RISPONDENTE, LIMITE_ISEE,  " _
                    & " ID_DICHIARAZIONE, CANONE_ATTUALE, ADEGUAMENTO,  " _
                    & " ISTAT, NUM_COMP, NUM_COMP_66,  " _
                    & " NUM_COMP_100, NUM_COMP_100_CON, REDD_PREV_DIP,  " _
                    & " DETRAZIONI, DETRAZIONI_FRAGILITA, REDD_MOBILIARI,  " _
                    & " REDD_IMMOBILIARI, REDD_COMPLESSIVO, ANNO_COSTRUZIONE,  " _
                    & " LOCALITA, NUMERO_PIANO, PRESENTE_ASCENSORE,  " _
                    & " SUP_NETTA, ALTRE_SUP, COD_CONTRATTO,  " _
                    & " DATA_STIPULA, MINORI_15, MAGGIORI_65,  " _
                    & " SUP_ACCESSORI, VALORE_LOCATIVO, CANONE_MINIMO_AREA,  " _
                    & " DECADENZA_ALL_ADEGUATO, DECADENZA_VAL_ICI, CANONE_CLASSE,  " _
                    & " CANONE_SOPPORTABILE, PERC_ISTAT_APPLICATA, CANONE_CLASSE_ISTAT,  " _
                    & " CANONE_91, INIZIO_VALIDITA_CAN, FINE_VALIDITA_CAN,  " _
                    & " TIPO_PROVENIENZA, SCONTO_COSTO_BASE, CANONE_1243_12,  " _
                    & " DELTA_1243_12, ESCLUSIONE_1243_12, TIPO_CANONE_APP,  " _
                    & " LETTERA, COMPETENZA, ID)  " _
                & " SELECT " _
                & " " & idnuovoContratto.Value & ", DATA_CALCOLO, TESTO, " _
                    & " ID_AREA_ECONOMICA, ISEE, ISE,  " _
                    & " ISR, ISP, PSE,  " _
                    & " VSE, REDDITI_DIP, REDDITI_ATRI,  " _
                    & " LIMITE_PENSIONI, ISEE_27, PERC_VAL_LOC,  " _
                    & " INC_MAX, INCIDENZA_ISE, CANONE,  " _
                    & " ID_BANDO_AU, NOTE, DEM,  " _
                    & " SUPCONVENZIONALE, COSTOBASE, ZONA,  " _
                    & " PIANO, CONSERVAZIONE, VETUSTA,  " _
                    & " COEFF_NUCLEO_FAM, SOTTO_AREA, ANNOTAZIONI,  " _
                    & " PATRIMONIO_SUP, NON_RISPONDENTE, LIMITE_ISEE,  " _
                    & " ID_DICHIARAZIONE, CANONE_ATTUALE, ADEGUAMENTO,  " _
                    & " ISTAT, NUM_COMP, NUM_COMP_66,  " _
                    & " NUM_COMP_100, NUM_COMP_100_CON, REDD_PREV_DIP,  " _
                    & " DETRAZIONI, DETRAZIONI_FRAGILITA, REDD_MOBILIARI,  " _
                    & " REDD_IMMOBILIARI, REDD_COMPLESSIVO, ANNO_COSTRUZIONE,  " _
                    & " LOCALITA, NUMERO_PIANO, PRESENTE_ASCENSORE,  " _
                    & " SUP_NETTA, ALTRE_SUP, COD_CONTRATTO,  " _
                    & " DATA_STIPULA, MINORI_15, MAGGIORI_65,  " _
                    & " SUP_ACCESSORI, VALORE_LOCATIVO, CANONE_MINIMO_AREA,  " _
                    & " DECADENZA_ALL_ADEGUATO, DECADENZA_VAL_ICI, CANONE_CLASSE,  " _
                    & " CANONE_SOPPORTABILE, PERC_ISTAT_APPLICATA, CANONE_CLASSE_ISTAT,  " _
                    & " CANONE_91, INIZIO_VALIDITA_CAN, FINE_VALIDITA_CAN,  " _
                    & " TIPO_PROVENIENZA, SCONTO_COSTO_BASE, CANONE_1243_12,  " _
                    & " DELTA_1243_12, ESCLUSIONE_1243_12, TIPO_CANONE_APP,  " _
                    & " LETTERA, COMPETENZA, SEQ_UTENZA_DICH_CANONI_EC.NEXTVAL  " _
                    & " FROM UTENZA_DICH_CANONI_EC WHERE ID_DICHIARAZIONE = " & idDich.Value & " AND ID_CONTRATTO = " & idContratto.Value & " and id in " _
                    & " (select max(id) from UTENZA_DICH_CANONI_EC udc where UTENZA_DICH_CANONI_EC.id_contratto=udc.id_contratto) "
                par.cmd.ExecuteNonQuery()
            End If

            Dim CAUZIONE As Double = 0
            If codTipoContr = "ERP" Then
                CAUZIONE = Format((canoneAnnuo / 12), "0.00") '*** 02/11/2017 L'importo del deposito cauzionale in UNA mensilità (Rif. segnalazione num. 1934/2017)
            Else
                CAUZIONE = Format((canoneAnnuo / 12) * 3, "0.00")
            End If

            par.cmd.CommandText = "update siscom_mi.rapporti_utenza set imp_canone_iniziale=" & par.VirgoleInPunti(canoneAnnuo) & ",IMP_DEPOSITO_CAUZ=" & par.VirgoleInPunti(CAUZIONE) & " WHERE ID=" & INDICE_CONTRATTO
            par.cmd.ExecuteNonQuery()


            If codFis <> "-1" Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET TIPO_COR='" & par.IfNull(dt1.Rows(0).Item("TIPO_COR"), "VIA") _
                                    & "',LUOGO_COR='" & par.PulisciStrSql(par.IfNull(dt1.Rows(0).Item("LUOGO_COR"), "")) _
                                    & "',VIA_COR='" & par.PulisciStrSql(par.IfNull(dt1.Rows(0).Item("VIA_COR"), "")) _
                                    & "',CIVICO_COR='" & par.PulisciStrSql(par.IfNull(dt1.Rows(0).Item("CIVICO_COR"), "")) _
                                    & "',SIGLA_COR='" & par.PulisciStrSql(par.IfNull(dt1.Rows(0).Item("SIGLA_COR"), "")) _
                                    & "',CAP_COR='" & par.PulisciStrSql(par.IfNull(dt1.Rows(0).Item("CAP_COR"), "")) _
                                    & "',PRESSO_COR='" & par.PulisciStrSql(par.IfNull(dt1.Rows(0).Item("PRESSO_COR"), "")) _
                                    & "' WHERE ID=" & INDICE_CONTRATTO
                par.cmd.ExecuteNonQuery()
            End If

            'If codFis = "-1" Then
            '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & idContratto.Value & " AND COD_TIPOLOGIA_OCCUPANTE<>'INTE' and NVL(data_fine,'29991231') >= '" & Format(Now(), "yyyyMMdd") & "'"
            '    myReaderX = par.cmd.ExecuteReader()
            '    Do While myReaderX.Read
            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO) VALUES (" _
            '                            & par.IfNull(myReaderX("ID_ANAGRAFICA"), "0") & "," & INDICE_CONTRATTO & ",'" & par.IfNull(myReaderX("COD_TIPOLOGIA_PARENTELA"), "1") _
            '                            & "','" & par.IfNull(myReaderX("COD_TIPOLOGIA_OCCUPANTE"), "") & "','" & par.IfNull(myReaderX("COD_TIPOLOGIA_TITOLO"), "") & "')"
            '        par.cmd.ExecuteNonQuery()
            '    Loop
            '    myReaderX.Close()
            'Else
            '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & idContratto.Value & " and NVL(data_fine,'29991231') >= '" & Format(Now(), "yyyyMMdd") & "'"
            '    myReaderX = par.cmd.ExecuteReader()
            '    Do While myReaderX.Read
            '        If par.IfNull(myReaderX("ID_ANAGRAFICA"), "0") = IdAnagRichiedente Then
            '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO) VALUES (" _
            '            & IdAnagRichiedente & "," & INDICE_CONTRATTO & ",'" & par.IfNull(myReaderX("COD_TIPOLOGIA_PARENTELA"), "1") _
            '            & "','INTE','" & par.IfNull(myReaderX("COD_TIPOLOGIA_TITOLO"), "") & "')"
            '        Else
            '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO) VALUES (" _
            '            & par.IfNull(myReaderX("ID_ANAGRAFICA"), "0") & "," & INDICE_CONTRATTO & ",'" & par.IfNull(myReaderX("COD_TIPOLOGIA_PARENTELA"), "1") _
            '            & "','ALTR','" & par.IfNull(myReaderX("COD_TIPOLOGIA_TITOLO"), "") & "')"
            '        End If
            '        par.cmd.ExecuteNonQuery()

            '    Loop
            '    myReaderX.Close()
            'End If
            AggiungiComponenti(idDich.Value, idnuovoContratto.Value, cognome, nome, CF, idAnagr)

            par.cmd.CommandText = "Insert into siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, " _
                                   & "GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,CANONE,ID_ANAGRAFICA,PROVVEDIMENTO,DATA_PROVVEDIMENTO) " _
                                   & " Values " _
                                   & "(-1, " & UnitaContratto & ", '" & RinnovoDataPG & "', 0, " & idDich.Value & ", " _
                                   & "'" & par.PulisciStrSql(cognome) & "', '" & par.PulisciStrSql(nome) _
                                   & "', '" & par.PulisciStrSql(CF) & "', '" & provenienza & "', 0," & par.VirgoleInPunti(canoneAnnuo) & "," & idAnagr & ",'" & RinnovoNumeroPG & "','" & RinnovoDataPG & "')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.UNITA_CONTRATTUALE (SELECT " _
                & INDICE_CONTRATTO & ", ID_UNITA, COD_UNITA_IMMOBILIARE," _
                & " TIPOLOGIA, ID_EDIFICIO, SCALA," _
                & " COD_TIPO_LIVELLO_PIANO, INTERNO, ID_UNITA_PRINCIPALE," _
                & " SEZIONE, FOGLIO, NUMERO," _
                & " SUB, COD_TIPOLOGIA_CATASTO, RENDITA," _
                & " COD_CATEGORIA_CATASTALE, COD_CLASSE_CATASTALE, COD_QUALITA_CATASTALE," _
                & " SUPERFICIE_MQ, CUBATURA, NUM_VANI," _
                & " SUPERFICIE_CATASTALE, RENDITA_STORICA, IMMOBILE_STORICO," _
                & " REDDITO_DOMINICALE, VALORE_IMPONIBILE, REDDITO_AGRARIO," _
                & " VALORE_BILANCIO, DATA_ACQUISIZIONE, DATA_FINE_VALIDITA," _
                & " DITTA, NUM_PARTITA, ESENTE_ICI," _
                & " PERC_POSSESSO, INAGIBILE, MICROZONA_CENSUARIA," _
                & " ZONA_CENSUARIA, COD_STATO_CATASTALE, INDIRIZZO," _
                & " CIVICO, CAP, LOCALITA," _
                & " COD_COMUNE, SUP_CONVENZIONALE, VAL_LOCATIVO_UNITA" _
                & " FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContratto.Value & " and id_unita_principale is null)"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTESTATARI_RAPPORTO (ID_CONTRATTO,ID_ANAGRAFICA,DATA_INIZIO,DATA_FINE) VALUES (" & INDICE_CONTRATTO & "," & IdAnagRichiedente & ",'" & RinnovoDataPG & "','29991231')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & INDICE_CONTRATTO & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F01','CAMBIO TIPOLOGIA CONTRATTUALE (vecchia posizione cod. " & codContratto & ")')"
            par.cmd.ExecuteNonQuery()


            sNuovoCodiceRapporto = NuovoCodiceContratto
            lNuovoIdRapporto = INDICE_CONTRATTO


            par.cmd.CommandText = "SELECT cond_edifici.* FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & UnitaContratto & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (2," & UnitaContratto & ",'" & Format(Now, "yyyyMMdd") & "',0," & myReader("ID_CONDOMINIO") & "," & idContratto.Value & ")"
                par.cmd.ExecuteNonQuery()
            Loop
            myReader.Close()


            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET PRESSO_COR='" & par.PulisciStrSql(CognomeNome) & "',TIPO_COR='" & TipoIndir & "'," _
            & "LUOGO_COR='" & LuogoRec & "',VIA_COR='" & Indirizzo & "',CIVICO_COR='" & Civico & "',CAP_COR='" & Cap & "' WHERE ID=" & INDICE_CONTRATTO
            par.cmd.ExecuteNonQuery()

            '19/01/2017 Riporto la voce del sindacato dal vecchio al nuovo contratto (come richiesto da MM)
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE,IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) " _
                & " select siscom_mi.seq_bol_schema.nextval," & INDICE_CONTRATTO & "," & UnitaContratto & "," & par.RicavaEsercizioCorrente & ",ID_VOCE,IMPORTO,DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO " _
                & " from siscom_mi.bol_schema where anno = (select max(anno) from siscom_mi.bol_schema bls where bls.id_contratto=bol_schema.id_contratto) and (id_voce in (select id from siscom_mi.t_voci_bolletta where gruppo=5) or id_voce in (300,302,303)) and " & CDec(Mid(proxBolletta, 5, 2)) & " >= da_rata And (" & CDec(Mid(proxBolletta, 5, 2)) & "- da_rata) <= (per_rate - 1) and id_contratto=" & idContratto.Value & ""
            par.cmd.ExecuteNonQuery()
            'End If

            Dim verificaStMan As Boolean = False
            par.cmd.CommandText = "select * from siscom_mi.unita_stato_manutentivo where id_unita=" & UnitaContratto
            Dim myReaderUST As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderUST.Read Then
                verificaStMan = True
            End If
            myReaderUST.Close()

            If verificaStMan = True Then
                par.cmd.CommandText = "update siscom_mi.unita_stato_manutentivo set " _
                & " Tipo_Riassegnabile='1'," _
                & " data_rilevazione='" & Format(Now, "yyyyMMdd") & "'," _
                & " riassegnabile=1," _
                & " p_blindata=0,data_PRE_S='" & par.AggiustaData(dataSloggio392) & "',DATA_PRE_SLOGGIO='" & par.AggiustaData(dataSloggio392) & "'" _
                & " where id_unita=" & UnitaContratto
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "Insert into siscom_mi.UNITA_STATO_MANUTENTIVO " _
                        & "   (ID_UNITA, DATA_S, RILEVAZIONE, RIASSEGNABILE, P_BLINDATA,  " _
                        & "     SOL_GP, SOL_GF, SOL_PB, SOL_LA, SOL_AL,  " _
                        & "     HANDICAP, DATA_PRE_S, DATA_PRE_SLOGGIO, NOTE, ALLARME,  " _
                        & "     MOTIVAZIONI, ZONA, NUM_LOCALI, NUM_SERVIZI, TIPO_ALLOGGIO,  " _
                        & "     DATA_CONSEGNA_CHIAVI, CONSEGNATE_A, DATA_RIPRESA_CHIAVI, ID_QUARTIERE, DATA_RILEVAZIONE,  " _
                        & "     NOTEGRTP, REC_GRTP, FINE_LAVORI, SOL_PB1, SOL_PB2,  " _
                        & "     SOL_PB3, SOL_LA1, TIPO_RIASSEGNABILE, DATA_Q, IMPORTO_DANNI,  " _
                        & "     IMPORTO_TRASPORTO, NOTE_DANNI, FL_AUTORIZZATI_IMP, FL_ABUSIVO, NOTE_TIPO_PORTA,  " _
                        & "     NOTE_SICUREZZA, ST_DEP_CHIAVI, ID_STRUTTURA_COMP, DATA_CONSEGNA_STR, DATA_RIPRESA_STR,  " _
                        & "     ID_MANUTENZIONE/*, ID_TIPO_ALLOGGIO, DATA_DISPONIBILITA, DATA_REC_OCC_ABUSIVA, RECUPERO_OCC_ABUSIVA,  " _
                        & "     INT_PORTA_BLINDATA, INT_SOST_SERRANDA, INT_LASTRA_PRIMO_INGRESSO, INT_LASTRA_FINESTRE, DATA_CONS_UFF_CHIAVI_SLOGGIO,  " _
                        & "     NOTE_UFF_NORD_EST, NOTE_UFF_SUD_OVEST, CHIAVE_CONSEGNATE_A_RESE, CHIAVE_CONSEGANTE_A_RESE_DATA, STATO_ALLOGGIO_LAVORI,  " _
                        & "     DATA_POSSESSO_CHIAVI_LAVORI, DATA_INIZIO_LAVORI, DATA_CONS_CHIAVI_DITTA_LAVORI, DATA_PRESUNTA_DISP_UI_LAVORI, FINE_LAVORI_STRUTTURA,  " _
                        & "     DOC_LAVORI_IDRICO, DOC_LAVORI_ELETTRICO, DOC_LAVORI_GAS, DOC_LAVORI_CANNA_FUM, NOTE_LAVORI,  " _
                        & "     SLOGGIO_TECNICO, DATA_SLOGGIO_TECNICO, DATA_CENSIMENTO_TECNICO, STATO_ALLOGGIO_TECNICO, STATO_UI_TECNICO,  " _
                        & "     LIV_MANUT_TECNICO, NOTE_TECNICO*/) " _
                        & " Values " _
                        & " (" & UnitaContratto & ", NULL, 0, 1, 0,  " _
                        & " 0, 0, 0, 0, 0,  " _
                        & " 0, '" & par.AggiustaData(dataSloggio392) & "', '" & par.AggiustaData(dataSloggio392) & "', NULL, 0,  " _
                        & " NULL, NULL, NULL, NULL, NULL,  " _
                        & " NULL, NULL, NULL, NULL, NULL,  " _
                        & " NULL, 0, 1, 0, 0,  " _
                        & " 0, 0, '1', NULL, 0,  " _
                        & " 0, NULL, '0', 0, NULL,  " _
                        & " NULL, NULL, NULL, NULL, NULL,  " _
                        & " NULL/*, NULL, NULL, NULL,  " _
                        & " NULL, NULL, NULL, NULL, NULL,  " _
                        & " NULL, NULL, NULL, NULL, NULL,  " _
                        & " NULL, NULL, NULL, NULL, NULL,  " _
                        & " NULL, NULL, NULL, NULL, NULL,  " _
                        & " NULL, NULL, NULL, NULL, NULL,  " _
                        & " NULL, NULL*/)"
                par.cmd.ExecuteNonQuery()
            End If

            CercaAssegnazione()

            dataRicons.Value = txtDataSloggio.Text
            dataRicons.Value = DateAdd("d", -1, dataRicons.Value)


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F18','" & dataRicons.Value & "')"
            par.cmd.ExecuteNonQuery()


            If Not IsNothing(Session.Item("lIdConnessione")) Then
                Dim par1 As New CM.Global
                par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleConnection)
                par1.cmd = par1.OracleConn.CreateCommand()
                par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleTransaction)

                'If dataDisdLocatario <> "" Then
                par1.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET" _
               & " data_riconsegna='" & par.AggiustaData(dataRicons.Value) & "'" _
               & " WHERE ID=" & idContratto.Value
                par1.cmd.ExecuteNonQuery()

                par1.myTrans.Commit()
                par1.myTrans = par1.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessione"), par1.myTrans)
                par1.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            connData.chiudi(True)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", Page.Title & " CreaNuovoContrattoERP - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ConfrontaComponente(ByVal r As Data.DataRow, ByVal idAnagrafico As Long)

        Dim cittadinanza As String = ""
        Dim residenza As String = ""
        Dim comuresid As String = ""
        Dim provresid As String = ""
        Dim indirizzresid As String = ""
        Dim idrecapito As String = ""
        Dim idindrecapito As String = ""
        Dim codComuRecap As String = ""

        par.cmd.CommandText = "select * from comuni_nazioni where cod='" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            If par.IfNull(lettore("cod"), "").ToString.Contains("Z") Then
                cittadinanza = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
            Else
                cittadinanza = "ITALIA"
            End If
        End If
        lettore.Close()

        par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(r.Item("id_luogo_res_dnte"), "")
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            comuresid = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
            provresid = par.IfNull(lettore("sigla"), "")
            residenza = comuresid & " (" & provresid & ") CAP " & par.IfNull(r.Item("cap_res_dnte"), "") & " "
        End If
        lettore.Close()
        par.cmd.CommandText = "select * from t_tipo_indirizzo where cod='" & par.IfNull(r.Item("id_tipo_ind_res_dnte"), "") & "'"
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            indirizzresid = par.PulisciStrSql(par.IfNull(lettore("descrizione"), "")) & " " & par.PulisciStrSql(par.IfNull(r.Item("ind_res_dnte"), ""))
            residenza &= indirizzresid & ", " & par.IfNull(r.Item("civico_res_dnte"), "")
        End If
        lettore.Close()

        par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA SET " _
            & "COGNOME= '" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("COGNOME"), "")))) & "'," _
            & "NOME= '" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("NOME"), "")))) & "'," _
            & "COD_FISCALE= '" & par.PulisciStrSql(par.IfNull(r.Item("cod_fiscale"), "")) & "'," _
            & "CITTADINANZA= '" & cittadinanza & "'," _
            & "RESIDENZA= '" & residenza & "'," _
            & "DATA_NASCITA= '" & par.IfNull(r.Item("DATA_NASCITA"), "") & "'," _
            & "COD_COMUNE_NASCITA= '" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'," _
            & "SESSO= '" & par.PulisciStrSql(par.IfNull(r.Item("sesso"), "")) & "'," _
            & "ID_INDIRIZZO_RECAPITO = '" & idrecapito & "'," _
            & "COMUNE_RESIDENZA = '" & comuresid & "'," _
            & "PROVINCIA_RESIDENZA= '" & provresid & "'," _
            & "INDIRIZZO_RESIDENZA= '" & indirizzresid & "'," _
            & "CIVICO_RESIDENZA= '" & par.IfNull(r.Item("civico_res_dnte"), "") & "'," _
            & "CAP_RESIDENZA= '" & par.IfNull(r.Item("cap_res_dnte"), "") & "'," _
            & "TIPO_R=0 " _
            & "WHERE ID= " & idAnagrafico
        par.cmd.ExecuteNonQuery()


    End Sub


    Private Sub AggiungiComponenti(ByVal idDichiarazione As Long, ByVal idContr As Long, ByRef cognome As String, ByRef nome As String, ByRef codF As String, ByRef idAnagIntest As Long)

        par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.ID,UTENZA_COMP_NUCLEO.PROGR,id_dichiarazione,cod_fiscale,cognome,nome,sesso,data_nascita,perc_inval,indennita_acc,grado_parentela," _
            & "UTENZA_DICHIARAZIONI.id_luogo_res_dnte,UTENZA_DICHIARAZIONI.id_tipo_ind_res_dnte,UTENZA_DICHIARAZIONI.ind_res_dnte,UTENZA_DICHIARAZIONI.civico_res_dnte,UTENZA_DICHIARAZIONI.cap_res_dnte " _
            & "FROM UTENZA_COMP_NUCLEO,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.id_dichiarazione " _
            & "AND id_dichiarazione = " & idDichiarazione
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        If dt.Rows.Count > 0 Then
            For Each row As Data.DataRow In dt.Rows

                par.cmd.CommandText = "select anagrafica.id from siscom_mi.anagrafica,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE anagrafica.id=SOGGETTI_CONTRATTUALI.id_anagrafica and id_contratto=" & idContratto.Value & " and cod_fiscale = '" & par.IfNull(row.Item("COD_FISCALE"), "") & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim idAnagrafico As Long = 0
                Dim codParentela As String = ""
                If myReader.Read Then
                    idAnagrafico = par.IfNull(myReader("ID"), 0)
                Else
                    par.cmd.CommandText = "select anagrafica.id from siscom_mi.anagrafica WHERE cod_fiscale = '" & par.IfNull(row.Item("COD_FISCALE"), "") & "'"
                    Dim myReaderAA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReaderAA.Read Then
                        idAnagrafico = par.IfNull(myReaderAA("ID"), 0)
                    End If
                    myReaderAA.Close()
                End If
                myReader.Close()

                If idAnagrafico = 0 Then
                    idAnagrafico = InserInAnagrafica(row, idContr)
                End If

                par.cmd.CommandText = "select * from t_tipo_parentela where cod=" & par.IfNull(row.Item("grado_parentela"), "")
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    codParentela = par.IfNull(myReader("cod_siscom_mi"), "")
                End If
                myReader.Close()

                If row.Item("PROGR") = "0" Then

                    par.cmd.CommandText = "select * from siscom_mi.anagrafica where id=" & idAnagrafico
                    Dim myReaderAnag As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReaderAnag.Read Then
                        cognome = par.IfNull(myReaderAnag("cognome"), "")
                        nome = par.IfNull(myReaderAnag("nome"), "")
                        codF = par.IfNull(myReaderAnag("cod_fiscale"), "")
                        idAnagIntest = idAnagrafico
                    End If
                    myReaderAnag.Close()

                    par.cmd.CommandText = "insert into SISCOM_MI.soggetti_contrattuali " _
                            & "(id_anagrafica,id_contratto,cod_tipologia_parentela,cod_tipologia_occupante,cod_tipologia_titolo) values" _
                            & "(" & idAnagrafico & "," & idContr & ",'" & codParentela & "','INTE','LEGIT')"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "insert into SISCOM_MI.soggetti_contrattuali " _
                            & "(id_anagrafica,id_contratto,cod_tipologia_parentela,cod_tipologia_occupante,cod_tipologia_titolo) values" _
                            & "(" & idAnagrafico & "," & idContr & ",'" & codParentela & "','ALTR','LEGIT')"
                    par.cmd.ExecuteNonQuery()
                End If

                ConfrontaComponente(row, idAnagrafico)
            Next
        End If

    End Sub

    Private Function InserInAnagrafica(ByVal r As Data.DataRow, ByVal idContratto As Long) As String
        Dim IdAna As String = ""
        Dim cittadinanza As String = ""
        Dim residenza As String = ""
        Dim comuresid As String = ""
        Dim provresid As String = ""
        Dim indirizzresid As String = ""
        Dim idrecapito As String = ""
        Dim idindrecapito As String = ""
        Dim codComuRecap As String = ""


        '*************** campo CITTADINANZA **************
        par.cmd.CommandText = "select * from comuni_nazioni where cod='" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            If par.IfNull(lettore("cod"), "").ToString.Contains("Z") Then
                cittadinanza = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
            Else
                cittadinanza = "ITALIA"
            End If
        End If
        lettore.Close()
        '*************** fine CITTADINANZA **********


        '*************** campo RESIDENZA **************
        par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(r.Item("id_luogo_res_dnte"), "")
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            comuresid = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
            provresid = par.IfNull(lettore("sigla"), "")
            residenza = comuresid & " (" & provresid & ") CAP " & par.IfNull(r.Item("cap_res_dnte"), "") & " "
        End If
        lettore.Close()
        par.cmd.CommandText = "select * from t_tipo_indirizzo where cod='" & par.IfNull(r.Item("id_tipo_ind_res_dnte"), "") & "'"
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            indirizzresid = par.PulisciStrSql(par.IfNull(lettore("descrizione"), "")) & " " & par.PulisciStrSql(par.IfNull(r.Item("ind_res_dnte"), ""))
            residenza &= indirizzresid & ", " & par.IfNull(r.Item("civico_res_dnte"), "")
        End If
        lettore.Close()
        '*************** fine RESIDENZA **************


        '********* campo ID_INDIRIZZO_RECAPITO **********
        par.cmd.CommandText = "select SISCOM_MI.SEQ_INDIRIZZI.nextval from dual"
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            idindrecapito = lettore(0)
        End If
        lettore.Close()

        par.cmd.CommandText = "select * from SISCOM_MI.rapporti_utenza where id=" & idContratto
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            par.cmd.CommandText = "select cod from comuni_nazioni where nome = '" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "'"
            Dim lettoreCod As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreCod.Read Then
                codComuRecap = par.IfNull(lettoreCod("cod"), "")
            End If
            lettoreCod.Close()

            par.cmd.CommandText = "insert into SISCOM_MI.indirizzi_anagrafica (id,descrizione,civico,cap,localita," _
                & "cod_comune) values (" & idindrecapito & ",'" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "','" & par.IfNull(lettore("civico_cor"), "") & "','" & par.IfNull(lettore("cap_cor"), "") & "'," _
                & "'" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "','" & codComuRecap & "') "
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "select id from SISCOM_MI.indirizzi_anagrafica where descrizione='" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "' and civico ='" & par.IfNull(lettore("civico_cor"), "") & "'"
            Dim lettoreInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreInd.Read Then
                idrecapito = par.IfNull(lettoreInd("id"), " ")
            End If
            lettoreInd.Close()
        End If
        lettore.Close()
        '************** fine ID_INDIRIZZO_RECAPITO *************



        '************* INSERIMENTO IN ANAGRAFICA **************
        par.cmd.CommandText = "select SISCOM_MI.SEQ_ANAGRAFICA.nextval from dual"
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            IdAna = lettore(0)
        End If
        lettore.Close()
        par.cmd.CommandText = "insert into SISCOM_MI.anagrafica (id,cognome,nome,data_nascita,cod_fiscale,sesso,cod_comune_nascita,cittadinanza,residenza,id_indirizzo_recapito," _
                            & "comune_residenza,provincia_residenza,indirizzo_residenza,civico_residenza,cap_residenza,tipo_r) values " _
                            & "(" & IdAna & ",'" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("COGNOME"), "")))) & "', " _
                            & "'" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("NOME"), "")))) & "', " _
                            & "'" & par.IfNull(r.Item("DATA_NASCITA"), "") & "', " _
                            & "'" & par.PulisciStrSql(par.IfNull(r.Item("cod_fiscale"), "")) & "', " _
                            & "'" & par.PulisciStrSql(par.IfNull(r.Item("sesso"), "")) & "','" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'," _
                            & "'" & cittadinanza & "','" & residenza & "','" & idrecapito & "','" & comuresid & "','" & provresid & "','" & indirizzresid & "'," _
                            & "'" & par.IfNull(r.Item("civico_res_dnte"), "") & "','" & par.IfNull(r.Item("cap_res_dnte"), "") & "',0)"
        par.cmd.ExecuteNonQuery()
        '************* fine INSERIMENTO IN ANAGRAFICA **************

        Return IdAna

    End Function


    Private Sub CercaAssegnazione()

        Dim idUI As Long = 0
        Dim idTipoContr As Long = 0
        Dim idDestinazioneUso As String = ""
        Dim idAssegnazione As Long = 0

        par.cmd.CommandText = "SELECT * " _
                    & "FROM " _
                    & "siscom_mi.UNITA_ASSEGNATE " _
                    & "WHERE " _
                    & " id_dichiarazione=" & idDich.Value
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If lettore.Read Then
            idUI = par.IfNull(lettore("ID_UNITA"), 0)
            idDestinazioneUso = par.IfNull(lettore("PROVENIENZA"), "")
        End If
        lettore.Close()

        Dim SriptJSContratto As String = "<script language='javascript'>var chiediConferma;" _
                                           & "chiediConferma = window.confirm('Operazione Effettuata!\nClicca OK per visualizzare il nuovo contratto.');" _
                                           & "if (chiediConferma) { " _
                                           & "CloseModal(-" & idnuovoContratto.Value & ");} else {self.close();}" & "" _
                                           & "</script>"

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "conf", SriptJSContratto, False)
    End Sub

    Protected Sub cmbDebitoAccettato_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbDebitoAccettato.SelectedIndexChanged
        Select Case cmbDebitoAccettato.SelectedValue
            Case "-1"
                accettazioneDeb.Value = "-1"
                btnNewContr.Visible = False

            Case "0"
                accettazioneDeb.Value = "0"
                btnNewContr.Visible = True
                btnNewContr.Style.Value = "visibility: visible;"
                'Calcola 200% valore locativo
                lblDataSl.Style.Value = "visibility: hidden;"
                txtDataSloggio.Style.Value = "visibility: hidden;"
            Case "1"
                accettazioneDeb.Value = "1"
                btnNewContr.Visible = True
                btnNewContr.Style.Value = "visibility: visible;"
                lblDataSl.Style.Value = "visibility: visible;"
                txtDataSloggio.Style.Value = "visibility: visible;"
        End Select
    End Sub

    Protected Sub btnTrasformainST_Click(sender As Object, e As System.EventArgs) Handles btnTrasformainST.Click
        If cmbConfermaRifiuto.SelectedValue = "0" Then
            btnTrasformainST.Visible = False
            lblTitoloSaldo.Visible = True
            lblSaldo.Visible = True
            cmbDebitoAccettato.Visible = True
            lblDebitoAccTitolo.Visible = True

            lblConfermaRifiuto.Visible = False
            cmbConfermaRifiuto.Visible = False
            btnTrasformainST.Visible = False
            lblCanone.Visible = False
            lblTitoloCanone.Visible = False
            cmbDebitoAccettato.SelectedValue = "-1"
        Else
            If conferma.Value = "1" Then
                btnTrasformainST.Visible = False
                Dim dtBoll As New Data.DataTable
                dtBoll = CercaFineContrDaStornare()

                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "confST", "CloseModal(" & par.VirgoleInPunti(indennita.Value) & ");", True)
            End If
        End If
    End Sub

    Protected Sub btnL43198_Click(sender As Object, e As System.EventArgs) Handles btnL43198.Click
        If txtImportoAnnuo.Text = "" Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "confST", "alert('Inserire il canone di locazione!')", True)
        Else
            btnL43198.Visible = False
            CreaNuovoContratto()
            ' ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "confST", "CloseModal(" & dataRicons.Value & ");", True)
        End If
    End Sub

    Private Function CercaFineContrDaStornare() As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            connData.apri(True)

            CercaIncassoDaGestionali()

            AnnullaStornoEriemissione()

            par.cmd.CommandText = "SELECT * from siscom_mi.bol_bollette where id_bolletta_storno is null and id_tipo<>22 " _
                    & " and NVL (id_rateizzazione, 0) = 0 AND NVL (id_bolletta_ric, 0) = 0 AND nvl(IMPORTO_RUOLO,0) = 0" _
                    & " and id_tipo=3 and id_contratto=" & idContratto.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    CreaStornoEnuovaBoll(row.Item("ID"))
                Next
            End If

            EliminaDepositoCauzionale()

            EmettiBolletteRateo()

            PagaNuoveBollette()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
               & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                   & "'F273','')"
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", Page.Title & " CreaNuovoContrattoERP - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
        Return dt
    End Function

    Private Sub CercaIncassoDaGestionali()
        Dim dt As New Data.DataTable
        par.cmd.CommandText = "select * from siscom_mi.incassi_extramav where id_Contratto=" & idContratto.Value & " and id_bolletta_gest in (select id from siscom_mi.bol_Bollette_gest where id_Contratto=" & idContratto.Value & " and id_tipo=6 and tipo_applicazione='T')"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt)
        da.Dispose()
        If dt.Rows.Count > 0 Then
            For Each row As Data.DataRow In dt.Rows
                AnnullaRipartizioneCrediti(row.Item("ID"))
            Next
        End If
    End Sub

    Private Function CtrlIncassoSuRicla(ByVal IdIncasso As Integer, ByRef idBollettaRic As Integer, ByRef errore As String) As Boolean
        CtrlIncassoSuRicla = False
        errore = "CtrlIncassoSuRicla"

        par.cmd.CommandText = "select id from siscom_mi.bol_bollette where id in (select id_bolletta from siscom_mi.bol_bollette_voci where id in (select id_voce_bolletta from siscom_mi.bol_bollette_voci_pagamenti where fl_no_report=1 and id_incasso_Extramav=" & IdIncasso & "))"
        idBollettaRic = par.IfNull(par.cmd.ExecuteScalar, 0)
        'idBollettaRic = 0

        errore = ""
        CtrlIncassoSuRicla = True
    End Function

    Private Sub AnnullaRipartizioneCrediti(ByVal IdIncasso As Long)

        If IdIncasso = 79823 Then
            Beep()
        End If


        par.cmd.CommandText = "select data_pagamento from SISCOM_MI.incassi_extramav where id = " & IdIncasso
        Dim dataPagIncasso As String = par.cmd.ExecuteScalar
        par.cmd.CommandText = "SELECT id_voce_bolletta,sum(nvl(importo_pagato,0)) as importo_pagato FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE ID_INCASSO_EXTRAMAV = " & IdIncasso & " group by id_voce_bolletta"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        For Each r As Data.DataRow In dt.Rows
            par.cmd.CommandText = "update SISCOM_MI.bol_bollette_voci set imp_pagato = (nvl(imp_pagato,0) + " & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & "),importo_riclassificato_pagato = (nvl(importo_riclassificato_pagato,0) + (case when nvl(importo_riclassificato,0) >0 then " & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & " else  0 end)) where id = " & r.Item("id_voce_bolletta")
            par.cmd.ExecuteNonQuery()

            '13/02/2015 Aggiungo condizione di annullo pagamento bolletta tipo=4 (MOROSITA)
            'CtrlIncassoSuRicla(IdIncasso, idMor, False)
            'If idMor > 0 Then
            '    par.cmd.CommandText = "UPDATE siscom_mi.bol_Bollette_voci set imp_pagato = (nvl(imp_pagato,0) + " & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & ") where id_voce = 150 and id_bolletta=" & idMor
            '    par.cmd.ExecuteNonQuery()
            'End If
            'FINE 13/02/2015 Aggiungo condizione di annullo pagamento bolletta tipo=4 (MOROSITA)
        Next

        par.cmd.CommandText = "select nvl(imp_pagato,0) as impPag FROM SISCOM_MI.BOL_BOLLETTE_VOCI where id_bolletta=-31910929"
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        dt = New Data.DataTable
        da.Fill(dt)

        par.cmd.CommandText = "select id,nvl(importo_pagato,0) as importo_pagato from SISCOM_MI.bol_bollette where id in (SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE " _
                & " ID in (select distinct id_voce_bolletta from SISCOM_MI.bol_bollette_voci_pagamenti where id_incasso_extramav  = " & IdIncasso & "))"
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        dt = New Data.DataTable
        da.Fill(dt)

        For Each rBolletta As Data.DataRow In dt.Rows
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_PAGAMENTO = null,data_valuta = null,OPERATORE_PAG = NULL,DATA_INS_PAGAMENTO = NULL,FL_PAG_PARZ = (case when NVL(FL_PAG_PARZ,0)>0 then (NVL(FL_PAG_PARZ,0)  - 1) else 0 end) WHERE ID = " & par.IfNull(rBolletta.Item("id"), 0)
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id in (select id_bolletta_ric from siscom_mi.bol_bollette b2 where b2.id= " & par.IfNull(rBolletta.Item("id"), 0) & ")"
            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable()
            da2.Fill(dt2)
            For Each rBollettaRic As Data.DataRow In dt2.Rows
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_PAGAMENTO = null,OPERATORE_PAG = '',DATA_INS_PAGAMENTO = '',FL_PAG_PARZ = (case when NVL(FL_PAG_PARZ,0)>0 then (NVL(FL_PAG_PARZ,0)  - 1) else 0 end) WHERE ID = " & par.IfNull(rBollettaRic.Item("id"), 0)
                par.cmd.ExecuteNonQuery()
            Next
        Next

        par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_voci_gest where ID_BOLLETTA_GEST in (select id_bolletta_gest from siscom_mi.incassi_extramav where id in " & IdIncasso & ")"
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_gest where id in (select id_bolletta_gest from siscom_mi.incassi_extramav where id in " & IdIncasso & ")"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "delete from siscom_mi.bol_bollette_voci_pagamenti where ID_INCASSO_EXTRAMAV = " & IdIncasso & ""
        par.cmd.ExecuteNonQuery()

    End Sub

    Private Sub EmettiBolletteRateo()

        Dim freqCanone As String = ""
        Dim dataDecorr As String = ""
        Dim dataProssimoPeriodo As String = ""
        Dim dataDisdettaAU As String = ""

        par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza where id=" & idContratto.Value
        Dim lettoreOA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreOA.Read Then
            freqCanone = par.IfNull(lettoreOA("NRO_RATE"), "")
        End If
        lettoreOA.Close()

        par.cmd.CommandText = "SELECT * FROM utenza_dichiarazioni WHERE utenza_dichiarazioni.ID=" & idDich.Value
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            dataDisdettaAU = par.IfNull(lettore("data_disdetta_392"), "")
        End If
        lettore.Close()

        Dim canoneAnnuo As Decimal = 0
        'Dim canoneAttuale As Decimal = 0

        Dim idVoceCanone As Integer = 1

        'If idAreaEconomica.Value = 4 Or dataDisdettaAU = "" Then
        par.cmd.CommandText = "select imp_canone_iniziale from siscom_mi.rapporti_utenza where id=" & idContratto.Value & ""
        Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderCC.Read Then
            canoneAnnuo = par.IfNull(myReaderCC("imp_canone_iniziale"), 0)
        End If
        myReaderCC.Close()

        'Cerco ultima bolletta per prendere la voce canone
        par.cmd.CommandText = " select id_voce from siscom_mi.bol_bollette_voci where id_bolletta in ( " _
                & " SELECT max(bol_bollette.id) " _
                & " FROM siscom_mi.bol_bollette_voci, siscom_mi.bol_bollette " _
                & " WHERE bol_bollette_voci.id_bolletta = bol_bollette.id " _
                & " AND id_tipo = 1 " _
                & " AND n_rata BETWEEN 1 AND 10 " _
                & " AND riferimento_da = " _
                & " (SELECT MAX (riferimento_da) " _
                & " FROM siscom_mi.bol_bollette bb " _
                & " WHERE  bb.id = bol_bollette.id " _
                & " AND id_tipo = 1 " _
                & " AND n_rata BETWEEN 1 AND 10 " _
                & " AND bb.id_contratto = bol_bollette.id_contratto) " _
                & " AND id_contratto =" & idContratto.Value & ")"
        Dim daBV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtBV As New Data.DataTable
        daBV.Fill(dtBV)
        daBV.Dispose()
        If dtBV.Rows.Count > 0 Then
            For Each rowBV As Data.DataRow In dtBV.Rows
                If par.IfNull(rowBV.Item("id_voce"), "0") = 1 Or par.IfNull(rowBV.Item("id_voce"), "0") = 36 Then
                    idVoceCanone = par.IfNull(rowBV.Item("id_voce"), "0")
                    Exit For
                End If
            Next
        End If
        'Else
        '    par.cmd.CommandText = "select * from UTENZA_DICH_CANONI_EC where id_dichiarazione=" & idDich.Value & " order by data_calcolo desc"
        '    Dim myReaderEC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If myReaderEC.Read Then
        '        canoneAnnuo = par.IfNull(myReaderEC("CANONE"), "0")
        '    End If
        '    myReaderEC.Close()
        'End If

        dataDecorr = par.IfEmpty(txtDataSloggio.Text, Format(Now, "dd/MM/yyyy"))
        dataDecorr = DateAdd("d", -1, dataDecorr)

        Dim numerorata As Integer = par.ProssimaRata(freqCanone, par.AggiustaData(dataDecorr), dataProssimoPeriodo)

        dataProssimoPeriodo = par.FormattaData(dataProssimoPeriodo)
        dataProssimoPeriodo = par.AggiustaData(DateAdd("m", 1, dataProssimoPeriodo))


        Dim dataMeseSucc As String = DateAdd("m", 1, CDate(dataDecorr))

        Dim giorniCanone2 As Integer = 0
        Dim giorni As Integer = 0
        Dim rateoIndennita As Decimal = 0
        Select Case Mid(dataDecorr, 4, 2)
            Case "01", "03", "05", "07", "08", "10", "12"
                giorni = DateDiff("d", dataDecorr, "31" & Mid(dataDecorr, 3, 8))
            Case "02"
                giorni = DateDiff("d", dataDecorr, "28" & Mid(dataDecorr, 3, 8)) + 3
            Case Else
                giorni = DateDiff("d", dataDecorr, "30" & Mid(dataDecorr, 3, 8)) + 1
        End Select

        Dim giorniVecchioCanone As Integer = 0
        giorniVecchioCanone = DateDiff("d", "01" & Mid(dataDecorr, 3, 8), dataDecorr)


        If CDate(par.FormattaData(dataProssimoPeriodo)) > CDate(par.FormattaData(dataMeseSucc)) Then
            'giorniCanone2 = giorni + 30 * DateDiff("m", DateAdd(DateInterval.Month, 1, CDate(dataMeseSucc)), par.FormattaData(dataProssimoPeriodo))
            giorniCanone2 = giorniVecchioCanone + giorni
            rateoIndennita = Format((canoneAnnuo / 360) * giorniCanone2, "0.00")
        End If

        Dim giorniNuovoCanone As Integer = 0
        giorniNuovoCanone = giorni

        Select Case Mid(dataMeseSucc, 4, 2)
            Case "01", "03", "05", "07", "08", "10", "12"
                giorni = DateDiff("d", dataMeseSucc, "31" & Mid(dataMeseSucc, 3, 8))
            Case "02"
                giorni = DateDiff("d", dataMeseSucc, "28" & Mid(dataMeseSucc, 3, 8)) + 3
            Case Else
                giorni = DateDiff("d", dataMeseSucc, "30" & Mid(dataMeseSucc, 3, 8)) + 1
        End Select
       
        Dim rateoCanone As Decimal = 0
        If giorniVecchioCanone > 0 Then
            rateoCanone = Format((((canoneAnnuo / freqCanone) / 30) * giorniVecchioCanone), "0.00")
        End If

        Dim rateoCanone2 As Decimal = 0
        If giorniNuovoCanone > 0 Then
            rateoCanone2 = Format((((canoneAnnuo / freqCanone) / 30) * giorniNuovoCanone), "0.00")
        End If

        If rateoCanone > 0 Then
            par.cmd.CommandText = "select * from siscom_mi.bol_bollette where substr(riferimento_da,1,6)='" & Mid(par.AggiustaData(dataDecorr), 1, 6) & "' and id_tipo=1 and id_contratto=" & idContratto.Value
            Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da0.Fill(dt0)
            da0.Dispose()
            If dt0.Rows.Count > 0 Then
                par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & idContratto.Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderS.Read Then
                    par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                        & "Values " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 99, '" & dt0.Rows(0).Item("DATA_EMISSIONE") _
                        & "', '" & dt0.Rows(0).Item("DATA_SCADENZA") & "', NULL,NULL,NULL,'IMPORTO RICALCOLATO IN SEGUITO A TRASFORMAZIONE IN S.T.'," _
                        & "" & idContratto.Value _
                        & " ," & par.RicavaEsercizioCorrente & ", " _
                        & dt0.Rows(0).Item("ID_UNITA") _
                        & ", '0', '', " & dt0.Rows(0).Item("COD_AFFITTUARIO") _
                        & ", '" & par.PulisciStrSql(par.IfNull(myReaderS("PRESSO_COR"), "")) & "', " _
                        & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                        & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                        & "', '', '" & dt0.Rows(0).Item("RIFERIMENTO_DA") _
                        & "', '" & dt0.Rows(0).Item("RIFERIMENTO_A") & "', " _
                        & "'0', " & par.IfNull(myReaderS("ID_COMPLESSO"), 0) & ", '', NULL, '', " _
                        & Year(Now) & ", '', " & par.IfNull(myReaderS("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD'," & dt0.Rows(0).Item("ID_TIPO") & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderS.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & dt0.Rows(0).Item("ID")
                Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtV As New Data.DataTable
                daV.Fill(dtV)
                daV.Dispose()

                Dim ID_BOLLETTA_NEW As Long = 0
                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    ID_BOLLETTA_NEW = myReaderA(0)
                End If
                myReaderA.Close()

                For Each rowV As Data.DataRow In dtV.Rows
                    If rowV.Item("ID_VOCE") <> 1 And rowV.Item("ID_VOCE") <> 36 And rowV.Item("ID_VOCE") <> 404 And rowV.Item("ID_VOCE") <> 562 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & rowV.Item("ID_VOCE") & "," & par.VirgoleInPunti(par.IfNull(rowV.Item("IMPORTO"), 0)) & ",'" & par.PulisciStrSql(par.IfNull(rowV.Item("NOTE"), "")) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                Next

               
                If idVoceCanone = 36 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                   & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & ",36" _
                   & "," & par.VirgoleInPunti(rateoCanone2 + rateoCanone) & ")"
                    par.cmd.ExecuteNonQuery()
                Else
                   
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                   & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & ",36" _
                   & "," & par.VirgoleInPunti(rateoCanone2) & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                   & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & idVoceCanone _
                   & "," & par.VirgoleInPunti(rateoCanone) & ")"
                    par.cmd.ExecuteNonQuery()

                End If

                par.cmd.CommandText = "delete from siscom_mi.incassi_extramav where id in (select id_incasso_extramav from siscom_mi.bol_bollette_voci_pagamenti " _
                    & " where id_bolletta = " & dt0.Rows(0).Item("ID") & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from siscom_mi.bol_bollette_voci_pagamenti where id_bolletta = " & dt0.Rows(0).Item("ID")
                par.cmd.ExecuteNonQuery()
            End If
        End If

        If indennita.Value <> 0 Then
            par.cmd.CommandText = "select * from siscom_mi.bol_bollette where riferimento_da>='" & par.AggiustaData(dataDecorr) & "' and id_tipo=1 and id_contratto=" & idContratto.Value
            Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da0.Fill(dt0)
            da0.Dispose()
            If dt0.Rows.Count > 0 Then
                par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & idContratto.Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderS.Read Then
                    par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                        & "Values " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 99, '" & dt0.Rows(0).Item("DATA_EMISSIONE") _
                        & "', '" & dt0.Rows(0).Item("DATA_SCADENZA") & "', NULL,NULL,NULL,'IMPORTO RICALCOLATO IN SEGUITO A TRASFORMAZIONE IN S.T.'," _
                        & "" & idContratto.Value _
                        & " ," & par.RicavaEsercizioCorrente & ", " _
                        & dt0.Rows(0).Item("ID_UNITA") _
                        & ", '0', '', " & dt0.Rows(0).Item("COD_AFFITTUARIO") _
                        & ", '" & par.PulisciStrSql(par.IfNull(myReaderS("PRESSO_COR"), "")) & "', " _
                        & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                        & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                        & "', '', '" & dt0.Rows(0).Item("RIFERIMENTO_DA") _
                        & "', '" & dt0.Rows(0).Item("RIFERIMENTO_A") & "', " _
                        & "'0', " & par.IfNull(myReaderS("ID_COMPLESSO"), 0) & ", '', NULL, '', " _
                        & Year(Now) & ", '', " & par.IfNull(myReaderS("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD'," & dt0.Rows(0).Item("ID_TIPO") & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderS.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & dt0.Rows(0).Item("ID")
                Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtV As New Data.DataTable
                daV.Fill(dtV)
                daV.Dispose()

                Dim ID_BOLLETTA_NEW As Long = 0
                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    ID_BOLLETTA_NEW = myReaderA(0)
                End If
                myReaderA.Close()

                For Each rowV As Data.DataRow In dtV.Rows
                    If rowV.Item("ID_VOCE") <> 1 And rowV.Item("ID_VOCE") <> 36 And rowV.Item("ID_VOCE") <> 404 And rowV.Item("ID_VOCE") <> 562 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & rowV.Item("ID_VOCE") & "," & par.VirgoleInPunti(par.IfNull(rowV.Item("IMPORTO"), 0)) & ",'" & par.PulisciStrSql(par.IfNull(rowV.Item("NOTE"), "")) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                Next

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & ",36" _
                    & "," & par.VirgoleInPunti(rateoIndennita) & ")"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "delete from siscom_mi.incassi_extramav where id in (select id_incasso_extramav from siscom_mi.bol_bollette_voci_pagamenti " _
                    & " where id_bolletta = " & dt0.Rows(0).Item("ID") & ")"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "delete from siscom_mi.bol_bollette_voci_pagamenti where id_bolletta = " & dt0.Rows(0).Item("ID")
                par.cmd.ExecuteNonQuery()
            End If
        End If

    End Sub

    Private Sub PagaNuoveBollette()

        Dim importoMorosita As Decimal = 0
        Dim impNuovoPAGATO As Decimal = 0
        Dim diffCreditoMoros As Decimal = 0
        Dim idTipoBoll As Long = 0
        Dim idIncasso As Long = 0
        Dim impCredito As Decimal = 0
        Dim impCredIniziale As Decimal = 0

        Dim dataDecorr As String = par.IfEmpty(txtDataSloggio.Text, Format(Now, "dd/MM/yyyy"))
        dataDecorr = DateAdd("d", -1, dataDecorr)

        par.cmd.CommandText = "select * from siscom_mi.bol_bollette where substr(riferimento_da,1,6)>='" & Mid(par.AggiustaData(dataDecorr), 1, 6) & "' and note like '%TRASFORMAZIONE IN S.T.%' and n_rata='99' and id_contratto=" & idContratto.Value
        Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt0 As New Data.DataTable
        da0.Fill(dt0)
        da0.Dispose()
        If dt0.Rows.Count > 0 Then
            For Each row As Data.DataRow In dt0.Rows

                par.cmd.CommandText = "select BOL_BOLLETTE_GEST.*,importo FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE BOL_BOLLETTE_GEST.ID=SISCOM_MI.BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND riferimento_da='" & row.Item("riferimento_da") & "' and riferimento_a='" & row.Item("riferimento_a") & "' and tipo_applicazione='N' and id_contratto=" & idContratto.Value
                Dim daGest As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dtGest As New Data.DataTable
                daGest = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                daGest.Fill(dtGest)
                daGest.Dispose()
                If dtGest.Rows.Count > 0 Then
                    For Each rowGest As Data.DataRow In dtGest.Rows
                        impCredIniziale = Math.Abs(par.IfNull(rowGest.Item("IMPORTO"), 0))
                        impCredito = par.IfNull(rowGest.Item("IMPORTO"), 0)

                        par.cmd.CommandText = "select distinct(bol_bollette.id),bol_bollette.*,bol_bollette_voci.*,BOL_BOLLETTE_VOCI.ID AS ID_VOCE1 from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id=" & row.Item("id") & " ORDER BY IMPORTO ASC"
                        Dim daMoros1 As Oracle.DataAccess.Client.OracleDataAdapter
                        Dim dtMoros1 As New Data.DataTable
                        daMoros1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                        daMoros1.Fill(dtMoros1)
                        daMoros1.Dispose()
                        If dtMoros1.Rows.Count > 0 Then
                            '(0)*** AGGIORNO LA VOCE degli INCASSI EXTRAMAV ***
                            par.cmd.CommandText = "INSERT INTO siscom_mi.incassi_extramav (ID, id_tipo_pag, motivo_pagamento, id_contratto,data_pagamento, riferimento_da, riferimento_a, fl_annullata,importo, id_operatore,id_bolletta_gest)" _
                                & "VALUES (siscom_mi.seq_incassi_extramav.NEXTVAL,12,'RIPARTIZ. CREDITO DA STORNO'," & idContratto.Value & ",'" & Format(Now, "yyyyMMdd") & "','','',0," & par.VirgoleInPunti(impCredito) & "," & Session.Item("ID_OPERATORE") & "," & rowGest.Item("id") & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.currval from dual"
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettore.Read Then
                                idIncasso = par.IfNull(lettore(0), "")
                            End If
                            lettore.Close()

                            For Each row1 As Data.DataRow In dtMoros1.Rows
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID=" & row1.Item("ID_BOLLETTA") & " AND DATA_PAGAMENTO IS NOT NULL"
                                lettore = par.cmd.ExecuteReader
                                If Not lettore.Read Then
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & Format(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(rowGest.Item("data_emissione")))), "yyyyMMdd") _
                                                & "',DATA_PAGAMENTO='" & par.IfNull(rowGest.Item("data_emissione"), "") & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                                & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                                    par.cmd.ExecuteNonQuery()
                                End If
                                lettore.Close()

                                If dtMoros1.Rows(0).Item("ID_TIPO") <> "4" Then
                                    importoMorosita = par.IfNull(row1.Item("IMPORTO"), 0) - par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                    If impCredito <> 0 Then
                                        diffCreditoMoros = importoMorosita - Math.Abs(impCredito)

                                        If diffCreditoMoros >= 0 Then
                                            '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO in BOL_BOLLETTE_VOCI_PAGAM ***
                                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                            & "(" & row1.Item("ID_VOCE1") & ",'" & row1.Item("DATA_PAGAMENTO") & "'," & par.VirgoleInPunti(Math.Round(impCredito, 2)) & ",4," & idIncasso & ")"
                                            par.cmd.ExecuteNonQuery()

                                            'WriteEvent(False, row1.Item("ID_VOCE1"), "F205", impCredito, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto.Value, "")

                                            impCredito = impCredito + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                            'ATTRAVERSO IL CREDITO, INIZIO AD INCASSARE LE BOLLETTE (scaduta e sollecitata) PARTENDO DALLE PIù VECCHIE

                                            '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(impCredito) & " where id=" & row1.Item("ID_VOCE1")
                                            par.cmd.ExecuteNonQuery()

                                            impCredito = 0
                                        Else
                                            impNuovoPAGATO = importoMorosita + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                            '(1)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(impNuovoPAGATO) & " where id=" & row1.Item("ID_VOCE1")
                                            par.cmd.ExecuteNonQuery()

                                            '(2)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                            If importoMorosita <> 0 Then
                                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                                & "(" & row1.Item("ID_VOCE1") & ",'" & row1.Item("DATA_PAGAMENTO") & "'," & par.VirgoleInPunti(Math.Round(importoMorosita, 2)) & ",4," & idIncasso & ")"
                                                par.cmd.ExecuteNonQuery()
                                            End If

                                            impCredito = Math.Abs(impCredito) - importoMorosita
                                        End If
                                    Else
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                        Dim importoRipartito As Decimal = 0
                        If impCredito > 0 Then
                            importoRipartito = impCredIniziale - impCredito
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST set IMPORTO_TOTALE=" & par.VirgoleInPunti(par.IfNull(impCredito * (-1), 0)) & " WHERE ID=" & rowGest.Item("id")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F204','IMPORTO STORNATO DI EURO " & Format(impCredito, "##,##0.00") & " PARI A EURO " & Format(impCredIniziale, "##,##0.00") & " DI CUI EURO " & Format(importoRipartito, "##,##0.00") & " UTILIZZATI PER RICOPRIRE LA NUOVA BOLLETTA EMESSA')"
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & rowGest.Item("id")
                            par.cmd.ExecuteNonQuery()
                        End If
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                            & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                            & "'F08','IMPORTO PARI A EURO " & Format(importoRipartito, "##,##0.00") & " PAGATO CON CREDITO DA STORNO')"
                        par.cmd.ExecuteNonQuery()
                    Next
                End If
                
            Next
        End If

    End Sub

    Private Sub CreaStornoEnuovaBoll(ByVal idbolletta As Long)

        Dim pagata As Boolean = False
        Dim dataPagamento As String = ""
        Dim dataValuta As String = ""
        Dim idAnagrafica As Long = 0
        Dim dataEmiss As String = ""
        Dim dataCompetDal As String = ""
        Dim dataCompetAl As String = ""
        Dim dataDecorr As String = ""
        Dim dataScadenza As String = ""
        Dim importoContrCalore As Decimal = 0
        Dim delta As Decimal = 0
        Dim dtV As New Data.DataTable
        Dim idBollGest As Long = 0
        par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto.Value & " AND ID=" & idbolletta
        Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt0 As New Data.DataTable
        da0.Fill(dt0)
        da0.Dispose()
        If dt0.Rows.Count > 0 Then
            If par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0 Or (par.IfNull(dt0.Rows(0).Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0) Then
                pagata = True
                dataPagamento = par.IfNull(dt0.Rows(0).Item("DATA_PAGAMENTO"), "")
                dataValuta = par.IfNull(dt0.Rows(0).Item("DATA_VALUTA"), "")
                dataEmiss = par.IfNull(dt0.Rows(0).Item("DATA_EMISSIONE"), "")
                dataCompetDal = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_DA"), "")
                dataCompetAl = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_A"), "")
            Else
                pagata = False
                dataPagamento = Format(Now, "yyyyMMdd")
                dataValuta = Format(Now, "yyyyMMdd")
                dataEmiss = par.IfNull(dt0.Rows(0).Item("DATA_EMISSIONE"), "")
                dataCompetDal = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_DA"), "")
                dataCompetAl = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_A"), "")
            End If
        End If

        Dim freqCanone As String = ""
        par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza where id='" & idContratto.Value & "'"
        Dim lettoreOA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreOA.Read Then
            dataDecorr = par.IfNull(lettoreOA("DATA_DECORRENZA"), "")
            dataScadenza = par.IfNull(lettoreOA("DATA_SCADENZA"), "")
            freqCanone = par.IfNull(lettoreOA("NRO_RATE"), "")
        End If
        lettoreOA.Close()

        Dim idAnagr As Long = 0
        par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE " _
            & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND RAPPORTI_UTENZA.ID=" & idContratto.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
        Dim lettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreDati.Read Then
            idAnagr = par.IfNull(lettoreDati("ID_ANAGRAFICA"), 0)
        End If
        lettoreDati.Close()

        Dim dataAttuale As String = ""
        Dim dataInizioCompet As String = ""
        Dim dataFineCompet As String = ""
        dataAttuale = Format(Now, "dd/MM/yyyy")
        If dataAttuale <> "" Then
            dataInizioCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & "01"
            dataFineCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & DateTime.DaysInMonth(Right(dataAttuale, 4), dataAttuale.Substring(3, 2))
        End If

        'STORNA BOLLETTA SELEZIONATA
        Dim note As String = ""
        Dim pagataParz As Boolean = False
        If pagata = True Then
            Dim importoTot As Decimal = 0

            importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_TOTALE"), 0)

            If par.IfNull(dt0.Rows(0).Item("IMPORTO_TOTALE"), 0) > par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) Then
                importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0)
                pagataParz = True
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                        & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE) " _
                        & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & idContratto.Value & "," & par.RicavaEsercizioCorrente & "," & dt0.Rows(0).Item("ID_UNITA") & "," & idAnagr & ",'" & dataInizioCompet & "','" & dataFineCompet & "'," & par.VirgoleInPunti(importoTot * -1) & "," _
                        & "'" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_VALUTA") & "',4,'N',NULL,'ECCEDENZA PER PAGAMENTO BOLLETTA STORNATA " & dt0.Rows(0).Item("NUM_BOLLETTA") & "')"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                idBollGest = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                        & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",712," & par.VirgoleInPunti(importoTot * -1) & ")"
            par.cmd.ExecuteNonQuery()
        End If

        If pagataParz = True Then
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & idbolletta
            Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            daV.Fill(dtV)
            daV.Dispose()
        Else
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & idbolletta
            Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            daV.Fill(dtV)
            daV.Dispose()
        End If

        par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto.Value & "AND ID=" & idbolletta
        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt1 As New Data.DataTable
        da1.Fill(dt1)
        da1.Dispose()
        For Each row As Data.DataRow In dt1.Rows

            note = "STORNO PER TRASFORM. CONTRATTO - NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA")

            par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                    & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                    & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                    & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                    & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                    & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, NOTE_PAGAMENTO, " _
                    & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                    & "Values " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
                    & "', '" & Format(Now, "yyyyMMdd") & "', NULL,NULL,NULL,'" & par.PulisciStrSql(note) & "'," _
                    & "" & par.IfNull(row.Item("ID_CONTRATTO"), 0) _
                    & " ," & par.RicavaEsercizioCorrente & ", " _
                    & par.IfNull(row.Item("ID_UNITA"), 0) _
                    & ", '0', '" & par.PulisciStrSql(par.IfNull(row.Item("PAGABILE_PRESSO"), "")) & "', " & par.IfNull(row.Item("COD_AFFITTUARIO"), 0) & "" _
                    & ", '" & par.PulisciStrSql(par.IfNull(row.Item("INTESTATARIO"), "")) & "', " _
                    & "'" & par.PulisciStrSql(par.IfNull(row.Item("INDIRIZZO"), "")) _
                    & "', '" & par.PulisciStrSql(par.IfNull(row.Item("CAP_CITTA"), "")) _
                    & "', '" & par.PulisciStrSql(par.IfNull(row.Item("PRESSO"), "")) & "', '" & par.IfNull(row.Item("RIFERIMENTO_DA"), "") _
                    & "', '" & par.IfNull(row.Item("RIFERIMENTO_A"), "") & "', " _
                    & "'1', " & par.IfNull(row.Item("ID_COMPLESSO"), 0) & ", '', '', " _
                    & Year(Now) & ", '', " & par.IfNull(row.Item("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD',22)"
            par.cmd.ExecuteNonQuery()
        Next

        Dim ID_BOLLETTA_STORNO As Long = 0
        par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
        Dim myReaderST As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderST.Read Then
            ID_BOLLETTA_STORNO = myReaderST(0)
        End If
        myReaderST.Close()

        Dim ID_VOCE_STORNO As Long = 0
        Dim SumImportoVOCI As Decimal = 0
        par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE.ID= " & idbolletta
        Dim daBVoci As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtBVoci As New Data.DataTable
        daBVoci.Fill(dtBVoci)
        daBVoci.Dispose()
        For Each row As Data.DataRow In dtBVoci.Rows
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI ( ID, ID_BOLLETTA, ID_VOCE, IMPORTO, NOTE, IMP_PAGATO_OLD," _
                & "IMP_PAGATO_BAK, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO, COMPETENZA_INIZIO," _
                & "COMPETENZA_FINE, FL_ACCERTATO ) VALUES ( SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL, " & ID_BOLLETTA_STORNO & ", " & row.Item("ID_VOCE") & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & ",'STORNO'," _
                & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_OLD"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_BAK"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), 0)) & "," _
                & "'" & par.IfNull(row.Item("COMPETENZA_INIZIO"), "") & "', '" & par.IfNull(row.Item("COMPETENZA_FINE"), "") & "'," & par.IfNull(row.Item("FL_ACCERTATO"), 0) & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.CURRVAL FROM DUAL"
            Dim myReaderIDV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIDV.Read Then
                ID_VOCE_STORNO = myReaderIDV(0)
            End If
            myReaderIDV.Close()

            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & " WHERE ID=" & ID_VOCE_STORNO
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) & " WHERE ID=" & row.Item("ID")
            par.cmd.ExecuteNonQuery()

            SumImportoVOCI = SumImportoVOCI + par.IfNull(row.Item("IMPORTO"), 0)
        Next

        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set ID_BOLLETTA_STORNO=" & ID_BOLLETTA_STORNO & ",FL_STAMPATO='1',DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & idbolletta
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & ID_BOLLETTA_STORNO
        par.cmd.ExecuteNonQuery()

        Dim strPagata As String = ""
        If pagata = True Then
            strPagata = "(precedentam. pagata) "
        Else
            strPagata = "(non precedentem. pagata) "
        End If
        If pagataParz = True Then
            pagata = False
            strPagata = "(parzialm. pagata) "
        End If
        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F203','NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA") & " " & strPagata & " STORNATA PER TRASFORM. CONTRATTO')"
        par.cmd.ExecuteNonQuery()


    End Sub

    Private Sub AnnullaStornoEriemissione()

        Dim dataEmissione As String = ""
        Dim dataDecorr As String = Format(Now, "dd/MM/yyyy")
        dataDecorr = DateAdd("d", -1, dataDecorr)

        par.cmd.CommandText = "select id,data_emissione,importo_totale,riferimento_da from siscom_mi.bol_bollette where note like '%STORNO PER TRASFORM. CONTRATTO%' and substr(riferimento_da,1,6)<'" & Mid(par.AggiustaData(dataDecorr), 1, 6) & "' and id_contratto=" & idContratto.Value
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            For Each r As Data.DataRow In dt.Rows
                dataEmissione = r.Item("data_emissione")

                par.cmd.CommandText = "select id_bolletta,data_pagamento,id_voce_bolletta,importo_pagato from siscom_mi.bol_bollette_voci_pagamenti where id_voce_bolletta in " _
                    & "(select bol_bollette_voci.id from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where siscom_mi.bol_bollette.id=siscom_mi.bol_bollette_voci.id_bolletta and id_bolletta_Storno=" & r.Item("id") & ")"
                Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt2 As New Data.DataTable()
                da2.Fill(dt2)
                If dt2.Rows.Count > 0 Then
                    For Each r2 As Data.DataRow In dt2.Rows
                        par.cmd.CommandText = "update SISCOM_MI.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(r2.Item("importo_pagato")) & " where id = " & r2.Item("id_voce_bolletta")
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "update SISCOM_MI.bol_bollette set data_pagamento='" & r2.Item("data_pagamento") & "' where id = " & r2.Item("id_bolletta")
                        par.cmd.ExecuteNonQuery()
                    Next
                Else
                    par.cmd.CommandText = "update SISCOM_MI.bol_bollette_voci set imp_pagato=0 where id_bolletta in (select bol_bollette.id from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci " _
                        & " where bol_bollette.id=siscom_mi.bol_bollette_voci.id_bolletta and id_bolletta_Storno=" & r.Item("id") & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "update SISCOM_MI.bol_bollette set data_pagamento='' where id_bolletta_Storno = " & r.Item("id")
                    par.cmd.ExecuteNonQuery()
                End If

                par.cmd.CommandText = "update siscom_mi.bol_bollette set id_bolletta_Storno=null where id_bolletta_Storno=" & r.Item("id")
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update siscom_mi.bol_bollette set fl_Stampato=0 where id=" & r.Item("id")
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from siscom_mi.bol_bollette where id=" & r.Item("id")
                par.cmd.ExecuteNonQuery()
            Next
        End If

        par.cmd.CommandText = "select * from siscom_mi.bol_bollette where n_rata='99' and note like '%IMPORTO RICALCOLATO IN SEGUITO A RETTIFICA PARTITA%' and data_pagamento is null and id_contratto=" & idContratto.Value
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtA As New Data.DataTable()
        da.Fill(dtA)
        If dtA.Rows.Count > 0 Then
            For Each rBollettaM As Data.DataRow In dtA.Rows
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET fl_Stampato=0 WHERE ID = " & par.IfNull(rBollettaM.Item("id"), 0)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE WHERE ID = " & par.IfNull(rBollettaM.Item("id"), 0)
                par.cmd.ExecuteNonQuery()
            Next
        End If

        par.cmd.CommandText = "select * from siscom_mi.incassi_Extramav where id_bolletta_gest in (select id from siscom_mi.bol_bollette_gest where " _
            & " id_tipo=6 and id_contratto=" & idContratto.Value & ")"
        Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt3 As New Data.DataTable()
        da3.Fill(dt3)
        If dt3.Rows.Count > 0 Then
            For Each r3 As Data.DataRow In dt3.Rows
                EliminaPagamento(r3.Item("id"))
            Next
        End If

        For Each r As Data.DataRow In dt.Rows
            par.cmd.CommandText = "select * from siscom_mi.bol_Bollette_gest where id_contratto=" & idContratto.Value & " and tipo_applicazione='N' and id_tipo=6 and riferimento_da='" & r.Item("riferimento_da") & "'"
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtB As New Data.DataTable()
            da.Fill(dtB)
            If dtB.Rows.Count > 0 Then
                For Each rBB As Data.DataRow In dtB.Rows
                    par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_voci_gest where ID_BOLLETTA_GEST = " & rBB.Item("ID")
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_gest where id = " & rBB.Item("ID")
                    par.cmd.ExecuteNonQuery()
                Next
            End If
        Next
        

        
    End Sub

    Private Sub EliminaPagamento(ByVal idIncasso As Long)
        
        par.cmd.CommandText = "select id,rif_bollettino,nvl(importo_pagato,0) as importo_pagato from SISCOM_MI.bol_bollette where id in (SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE " _
                & " ID in (select distinct id_voce_bolletta from SISCOM_MI.bol_bollette_voci_pagamenti where id_incasso_extramav  = " & idIncasso & "))"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)

        For Each rBolletta As Data.DataRow In dt.Rows
            If par.IfNull(rBolletta.Item("rif_bollettino"), 0) = 0 Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET fl_Stampato=0 WHERE ID = " & par.IfNull(rBolletta.Item("id"), 0)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE WHERE ID = " & par.IfNull(rBolletta.Item("id"), 0)
                par.cmd.ExecuteNonQuery()
            End If
        Next

        par.cmd.CommandText = "SELECT id_voce_bolletta,sum(nvl(importo_pagato,0)) as importo_pagato FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE ID_INCASSO_EXTRAMAV = " & idIncasso & " group by id_voce_bolletta"
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        dt = New Data.DataTable()
        da.Fill(dt)
        For Each r As Data.DataRow In dt.Rows
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE ID_VOCE_BOLLETTA=" & r.Item("id_voce_bolletta") & " AND ID_INCASSO_EXTRAMAV=" & idIncasso
            par.cmd.ExecuteNonQuery()
        Next

        par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_voci_gest where ID_BOLLETTA_GEST = (select id from siscom_mi.bol_bollette_gest where id_evento_pagamento = " & idIncasso & ")"
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_gest where id_evento_pagamento = " & idIncasso
        par.cmd.ExecuteNonQuery()


        par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_voci_gest where ID_BOLLETTA_GEST = (select ID_BOLLETTA_GEST from siscom_mi.incassi_extramav where id = " & idIncasso & ")"
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_gest where id = (select ID_BOLLETTA_GEST from siscom_mi.incassi_extramav where id = " & idIncasso & ")"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_voci_gest where ID_BOLLETTA_GEST in (select id from siscom_mi.bol_bollette_gest where id_tipo=6 and tipo_applicazione='N' and id_contratto=" & idContratto.Value & ")"
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_gest where id_tipo=6 and tipo_applicazione='N' and id_contratto=" & idContratto.Value
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "delete from SISCOM_MI.incassi_extramav WHERE ID = " & idIncasso
        par.cmd.ExecuteNonQuery()

    End Sub

    

    Private Sub EliminaDepositoCauzionale()
        par.cmd.CommandText = "SELECT BOL_BOLLETTE_GEST.ID,IMPORTO_TOTALE,DESCRIZIONE,ID_TIPO,UTILIZZABILE FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID_CONTRATTO=" & idContratto.Value & " AND ID_TIPO=55 AND BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID AND TIPO_APPLICAZIONE='N'"
        Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader0.Read Then

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & par.IfNull(myReader0("ID"), 0)
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & par.IfNull(myReader0("ID"), 0)
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F230','Tipo: " & par.IfNull(myReader0("DESCRIZIONE"), "") & " - Importo: euro " & par.VirgoleInPunti(Format(par.IfNull(myReader0("IMPORTO_TOTALE"), ""), "##,##0.00")) & "')"
            par.cmd.ExecuteNonQuery()

        End If
        myReader0.Close()

        par.cmd.CommandText = "delete from siscom_mi.adeguamento_interessi_voci where id_adeguamento in (select id from siscom_mi.adeguamento_interessi where id_contratto=" & idContratto.Value & ")"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "delete from siscom_mi.adeguamento_interessi where id_contratto=" & idContratto.Value
        par.cmd.ExecuteNonQuery()
    End Sub

    Protected Sub cmbConfermaRifiuto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbConfermaRifiuto.SelectedIndexChanged
        If cmbConfermaRifiuto.SelectedValue = "1" Then
            confermaRifiuto.Value = "1"
        Else
            indennita.Value = "0"
            confermaRifiuto.Value = "0"
        End If
    End Sub

    Protected Sub btnStampaDoc_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles btnStampaDoc.MenuItemClick
        Select Case btnStampaDoc.SelectedValue
            Case 1
                stampaModello.Value = 1
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey2A", "window.open('PrintLetter392.aspx?IDD=" & idDich.Value & "&RD=1&IDBOLL=" & vIdBolletta & "', 'letDebt', '');", True)
            Case 2
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey2B", "window.open('PrintLetter392.aspx?IDD=" & idDich.Value & "&IDC=" & idContratto.Value & "', 'letRisol', '');", True)
        End Select
    End Sub
End Class
