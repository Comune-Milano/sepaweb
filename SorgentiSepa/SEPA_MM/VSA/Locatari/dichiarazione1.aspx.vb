
Partial Class VSA_Locatari_dichiarazione1
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim annoReddito As String = ""
    Dim stringaDatiRedd As String = ""

    'Dim dataPr As String = ""
    'Dim anni As String = ""

    Private Sub GestioneSegnalazione()
        Dim TipoRichiesta As Integer = 0

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Select Case tipoRich.Value
                Case "3" 'revisione canone
                    TipoRichiesta = 1045
                Case "2"
                    TipoRichiesta = 1026
                Case "4", "5" 'cambio
                    TipoRichiesta = 1048
                Case "0" 'cambio intestazione
                    TipoRichiesta = 1014
                Case "7" 'spitalita
                    TipoRichiesta = 1012
                Case "1" 'variazione intestazione
                    TipoRichiesta = 1026
            End Select

            idSegnalazione = "null"
            par.cmd.CommandText = "select * from siscom_mi.segnalazioni where SEGNALAZIONI.ID_TIPO_SEGN_LIVELLO_1=" & TipoRichiesta & " and id_contratto=" & id_contr.Value & " and id_stato in (0,6) order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                idSegnalazione = myReader("id")
                If par.IfNull(myReader("id_Stato"), 0) = 0 Then
                par.cmd.CommandText = "update siscom_mi.segnalazioni set id_stato=6 where id=" & myReader("id")
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI (ID_SEGNALAZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE,VALORE_OLD,VALORE_NEW) VALUES (" & myReader("id") & ", " & Session.Item("ID_OPERATORE") & ", " & Format(Now, "yyyyMMddHHmmss") & ", 'F287', 'CAMBIO STATO SEGNALAZIONE','APERTA','IN CORSO')"
                par.cmd.ExecuteNonQuery()

            End If
                If IsNumeric(new_id_dom) AndAlso new_id_dom > 0 Then
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET ID_sEGNALAZIONE=" & idSegnalazione & " WHERE ID=" & new_id_dom
                    par.cmd.ExecuteNonQuery()
                End If
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CodContratto.Value = Request.QueryString("COD")
            id_intestatario.Value = Request.QueryString("INTEST")
            ModRichiesta.Value = Request.QueryString("ModR")
            id_contr.Value = Request.QueryString("ID")
            tipoRich.Value = Request.QueryString("T")
            causale.Value = Request.QueryString("CAUS")
            dataPr = Request.QueryString("DATA")
            anni = Request.QueryString("ANNI")
            idSind = Request.QueryString("IDSIND")
            altro = Request.QueryString("ALTRO")

            intestNewRU.Value = Request.QueryString("INTEST2")
            dataEvento = Request.QueryString("DATAEVE")

            dataInizio = Request.QueryString("INIZ")
            dataFine = Request.QueryString("FINE")

            codContrSc = Request.QueryString("CODCONTR2")

            If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" Then
                SoloLettura.Value = "1"
            End If
            visualizzaAU()
            If tipoRich.Value = "3" Then
                CercaReca()
            End If
        End If

    End Sub

    Private Sub visualizzaAU()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            If tipoRich.Value = "2" Then
                lblAmpl.Visible = True
                imgAlert.Visible = True
            End If

            Dim iDanag As Long = 0

            Select Case tipoRich.Value
                Case "2", "3"
                    par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA,ID FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                                        & "WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & id_contr.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                    Dim myReaderCF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderCF.Read Then
                        codFisc.Value = par.IfNull(myReaderCF("COD_FISCALE"), "")
                        iDanag = par.IfNull(myReaderCF("ID"), "")
                        intestatario.Value = par.IfNull(myReaderCF("NOMINATIVO"), "")
                    End If
                    myReaderCF.Close()
                Case Else
                    par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA,ID FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                                        & "WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & id_contr.Value & " AND ID_ANAGRAFICA = " & id_intestatario.Value
                    Dim myReaderCF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderCF.Read Then
                        codFisc.Value = par.IfNull(myReaderCF("COD_FISCALE"), "")
                        intestatario.Value = par.IfNull(myReaderCF("NOMINATIVO"), "")
                        iDanag = par.IfNull(myReaderCF("ID"), "")
                    End If
                    myReaderCF.Close()
            End Select

            If Request.QueryString("COMPEX") = "1" Then
                par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA FROM SISCOM_MI.ANAGRAFICA " _
                                    & "WHERE ANAGRAFICA.ID=" & id_intestatario.Value & " ORDER BY ID DESC"
                Dim myReaderCF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCF.Read Then
                    codFisc.Value = par.IfNull(myReaderCF("COD_FISCALE"), "")
                    iDanag = par.IfNull(myReaderCF("ID"), "")
                    intestatario.Value = par.IfNull(myReaderCF("NOMINATIVO"), "")
                End If
                myReaderCF.Close()
            End If

            If codFisc.Value <> "" Then
                If par.ControllaCF(codFisc.Value) = False Then
                    'Response.Write("<script>alert('Attenzione! Codice fiscale presente nel sistema errato!!')</script>")
                    lblErr.Visible = True
                    lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                    errore = True
                End If
            Else
                Response.Write("<script>alert('Attenzione! Codice fiscale non presente nel sistema!!')</script>")
            End If

            par.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(ID) AS STATO,COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & CodContratto.Value & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                    lblIntest.Text = " Cod. Contratto: <b>" & CodContratto.Value & "</b> <br/>Stato del contratto: <b>" & myReader0("STATO") & "</b>"
                Else
                    lblIntest.Text = "Componente: <b>" & intestatario.Value & "</b>&nbspCod. Contratto: <b>" & CodContratto.Value & "</b> <br/>Stato del contratto: <b>" & myReader0("STATO") & "</b>"
                End If
                sTipoContratto = myReader0("COD_TIPOLOGIA_CONTR_LOC")
            End If
            myReader0.Close()

            If tipoRich.Value = "3" Then
                If dataInizio = "" Or dataFine = "" Then
                    lblErr.Visible = True
                    lblErr.Text = "Attenzione! Date di validità non valorizzate! Si prega di ripetere l'operazione!"
                    errore = True
                End If
            End If

            If tipoRich.Value = "3" Or tipoRich.Value = "4" Then
                dataDaConsiderare = dataInizio
            Else
                dataDaConsiderare = dataEvento
            End If

            Dim id_dichia0 As Long = 0
            Dim id_dichia1 As Long = 0
            Dim data_Fine As String = ""
            Dim data_Fine1 As String = ""
            Dim numComponImport As Integer = 0
            Dim numComponRU As Integer = 0

            Dim MESSAGGIO As String = "<br/>Al momento non risultano esserci dichiarazioni di Anagrafe Utenza, domande di bando ERP o <br/>precedenti istanze di gestione locatari.<br/><br/>Clicca su Procedi per importare la situazione anagrafica attualmente presente nel rapporto."
            Dim MESSAGGIO1 As String = ""
            Dim MESSAGGIO2 As String = ""
            Dim LINK As String = ""
            Dim LINK1 As String = ""
            Dim tipoProvenienza As String = ""

            Dim condAUapplicata As String = ""
            If sTipoContratto = "ERP" Then
                condAUapplicata = " and exists (Select id_dichiarazione from siscom_mi.canoni_ec,siscom_mi.rapporti_utenza where id_canoni_ec=canoni_ec.id and canoni_ec.cod_contratto=rapporto and id_dichiarazione=utenza_dichiarazioni.id)"
            End If

            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE /*nvl(UTENZA_DICHIARAZIONI.DATA_FINE_VAL, DATA_INIZIO_VAL)>='" & dataDaConsiderare & "'" _
                & " AND*/ NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                & "AND RAPPORTO='" & CodContratto.Value & "'" & condAUapplicata & " ORDER BY ID_BANDO DESC"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                'tipoProvenienza = myReader2("id_tipo_provenienza")
                id_dichia1 = myReader2("ID")
                data_Fine1 = par.IfNull(myReader2("DATA_FINE_VAL"), Format(Now, "yyyyMMdd"))
                LINK1 = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../../ANAUT/DichAUnuova.aspx?TORNA=0&CHIUDI=1&LE=1&ID=" & id_dichia1 & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">Clicca qui per visualizzare la dichiarazione</a>"
                MESSAGGIO1 = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " corrisponde alla domanda <b>""" & myReader2("NOME_BANDO") & """</b> (redditi " & myReader2("ANNO_ISEE") & ").<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."
                Tipo_Domanda = "2"

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia1 & " AND COD_FISCALE ='" & codFisc.Value & "'"
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read = False Then
                    lblErr.Visible = True

                    par.cmd.CommandText = "select * from UTENZA_COMP_NUCLEO where COGNOME||' '||NOME ='" & UCase(par.PulisciStrSql(intestatario.Value)) & "' AND ID_DICHIARAZIONE=" & id_dichia
                    Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderControllo.Read Then
                        If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
                            lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                            errore = True
                        End If
                    Else
                        lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
                        errore = True
                    End If
                    myReaderControllo.Close()

                Else
                    lblErr.Visible = False
                    errore = False
                End If
                myReader3.Close()
            End If
            myReader2.Close()


            par.cmd.CommandText = "SELECT T_MOTIVO_DOMANDA_VSA.*,DOMANDE_BANDO_VSA.ID AS IDDOM,DOMANDE_BANDO_VSA.*,DATA_INIZIO_VAL,DATA_FINE_VAL FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA," _
                & "DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE And DICHIARAZIONI_VSA.ID_STATO <> 2 /*And nvl(DICHIARAZIONI_VSA.DATA_FINE_VAL, DATA_INIZIO_VAL)>='" & dataDaConsiderare & "'*/" _
                & " And nvl(DICHIARAZIONI_VSA.DATA_FINE_VAL, DATA_INIZIO_VAL)<='29991231' AND (DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE=1 OR ID_STATO_ISTANZA=5) AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & CodContratto.Value & "' ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("fl_autorizzazione"), 0) = 1 Or par.IfNull(myReader1("id_stato_istanza"), 0) = 5 Then
                    Tipo_Domanda = "1"
                    id_dichia0 = myReader1("ID_DICHIARAZIONE")
                    data_Fine = par.IfNull(myReader1("DATA_FINE_VAL"), myReader1("data_evento"))

                    If data_Fine = "29991231" Then data_Fine = par.IfNull(myReader1("DATA_INIZIO_VAL"), myReader1("data_evento")) 'Format(Now, "yyyyMMdd")
                    LINK = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & id_dichia0 & "&CH=2&LE=1','DichVSA','top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">Clicca qui per visualizzare la dichiarazione</a>"
                    MESSAGGIO = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " corrisponde alla domanda di <b>""" & myReader1("DESCRIZIONE") & """</b> presentata in data " & par.FormattaData(myReader1("DATA_presentazione")) & ".<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."

                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia0 & " AND COD_FISCALE ='" & codFisc.Value & "'"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read = False Then
                        lblErr.Visible = True
                        'lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"

                        par.cmd.CommandText = "select * from comp_nucleo_vsa where COGNOME||' '||NOME ='" & UCase(par.PulisciStrSql(intestatario.Value)) & "' AND ID_DICHIARAZIONE=" & id_dichia
                        Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderControllo.Read Then
                            If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
                                lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                                errore = True
                            End If
                        Else
                            If sTipoContratto <> "EQC392" Then
                                lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
                                errore = True
                            End If
                        End If
                        myReaderControllo.Close()

                    Else
                        lblErr.Visible = False
                        errore = False
                    End If
                    myReader3.Close()
                End If
            End If
            myReader1.Close()

            If data_Fine = "" And data_Fine1 = "" Then
                par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE /*nvl(UTENZA_DICHIARAZIONI.DATA_FINE_VAL, DATA_INIZIO_VAL)>='" & dataDaConsiderare & "'" _
                & " AND*/ NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                & "AND RAPPORTO='" & CodContratto.Value & "' ORDER BY ID_BANDO DESC"
                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    'tipoProvenienza = myReader2("id_tipo_provenienza")
                    id_dichia1 = myReader2("ID")
                    data_Fine1 = par.IfNull(myReader2("DATA_FINE_VAL"), Format(Now, "yyyyMMdd"))
                    LINK1 = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../../ANAUT/DichAUnuova.aspx?TORNA=0&CHIUDI=1&LE=1&ID=" & id_dichia1 & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">Clicca qui per visualizzare la dichiarazione</a>"
                    MESSAGGIO1 = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " corrisponde alla domanda <b>""" & myReader2("NOME_BANDO") & """</b> (redditi " & myReader2("ANNO_ISEE") & ").<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."
                    Tipo_Domanda = "2"

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia1 & " AND COD_FISCALE ='" & codFisc.Value & "'"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read = False Then
                        lblErr.Visible = True

                        par.cmd.CommandText = "select * from UTENZA_COMP_NUCLEO where COGNOME||' '||NOME ='" & UCase(par.PulisciStrSql(intestatario.Value)) & "' AND ID_DICHIARAZIONE=" & id_dichia
                        Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderControllo.Read Then
                            If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
                                lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                                errore = True
                            End If
                        Else
                            If sTipoContratto <> "EQC392" Then
                            lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
                            errore = True
                        End If
                        End If
                        myReaderControllo.Close()

                    Else
                        lblErr.Visible = False
                        errore = False
                    End If
                    myReader3.Close()
                End If
                myReader2.Close()
            End If

            If data_Fine = "" And data_Fine1 = "" Then
                'importRedd.Value = "2"
                lblMsg.Text = MESSAGGIO
                lblLink.Text = LINK
                btnSi.Visible = False
                btnNo.Visible = False
                btnEsci.Visible = False
                btnProcedi.Visible = True
                btnEsci2.Visible = True
                lblLink.Visible = False
                icona.Visible = False
                Tipo_Domanda = 4
            Else
                If data_Fine >= data_Fine1 Then
                    importRedd.Value = "1"
                    lblMsg.Text = MESSAGGIO
                    lblLink.Text = LINK
                    id_dichia = id_dichia0
                Else
                    importRedd.Value = "0"
                    id_dichia = id_dichia1
                    lblMsg.Text = MESSAGGIO1
                    lblLink.Text = LINK1
                End If
            End If


            par.cmd.CommandText = "SELECT MAX(ID) AS ID_BANDO FROM BANDI_VSA WHERE STATO = 1"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderB.Read Then
                id_bando = myReaderB("ID_BANDO")
            End If
            myReaderB.Close()

            If tipoRich.Value = "0" Then
                par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id_tipo = 4 and importo_totale <> NVL(importo_pagato,0) and id_contratto in (select id from siscom_mi.rapporti_utenza where cod_contratto='" & CodContratto.Value & "')"
                Dim lettMorosita As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettMorosita.Read Then
                    lblAmpl.Visible = True
                    lblAmpl.Text = "Attenzione! Risultano esserci delle morosità per questo rapporto di utenza."
                    imgAlert.Visible = True
                End If
                lettMorosita.Close()
            End If

            If tipoRich.Value = "0" Or tipoRich.Value = "10" Then
                If Request.QueryString("DEB") > 0 Then
                    lblAmpl.Visible = True
                    imgAlert.Visible = True
                    'lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere." & vbCrLf & "<a href=" & Chr(34) & "javascript:document.getElementById('stampaDeb').value='1';document.location.reload();" & Chr(34) & " onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&RICH=" & intestatario.Value & "&IMP= " & Request.QueryString("IMP") & "&INTEST=" & intestatario.Value & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></b>"
                    lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere." & vbCrLf & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&RICH=" & Replace(intestatario.Value, "'", "\'") & "&IMP= " & Request.QueryString("IMP") & "&NRA=" & Request.QueryString("NRA") & "&INTEST=" & Replace(intestatario.Value, "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></b>"
                End If
            End If

            If tipoRich.Value = "5" Then
                If Request.QueryString("DEB") > 0 And Request.QueryString("DEB2") > 0 Then
                    lblAmpl.Visible = True
                    imgAlert.Visible = True
                    'lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere." & vbCrLf & "<a href=" & Chr(34) & "javascript:document.getElementById('stampaDeb').value='1';document.location.reload();" & Chr(34) & " onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&RICH=" & intestatario.Value & "&IMP= " & Request.QueryString("IMP") & "&INTEST=" & intestatario.Value & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></b>"
                    lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere.<br/><ul><li>" & vbCrLf & intestatario.Value & " <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&IMP= " & Request.QueryString("IMP") & "&NRA=" & Request.QueryString("NRA") & "&INTEST=" & Replace(intestatario.Value, "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></li><li>" & Request.QueryString("INTEST2") & " <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & Request.QueryString("CODCONTR2") & "&IMP= " & Request.QueryString("IMP2") & "&NRA=" & Request.QueryString("NRA2") & "&INTEST=" & Replace(Request.QueryString("INTEST2"), "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO 2</a></li></ul></b>"
                ElseIf Request.QueryString("DEB") > 0 Then
                    lblAmpl.Visible = True
                    imgAlert.Visible = True

                    lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere.<br/><ul><li>" & vbCrLf & intestatario.Value & " <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&IMP= " & Request.QueryString("IMP") & "&NRA=" & Request.QueryString("NRA") & "&INTEST=" & Replace(intestatario.Value, "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></li></ul></b>"
                ElseIf Request.QueryString("DEB2") > 0 Then
                    lblAmpl.Visible = True
                    imgAlert.Visible = True

                    lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere.<br/><ul><li>" & vbCrLf & Request.QueryString("INTEST2") & " <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & Request.QueryString("CODCONTR2") & "&IMP= " & Request.QueryString("IMP2") & "&NRA=" & Request.QueryString("NRA2") & "&INTEST=" & Replace(Request.QueryString("INTEST2"), "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO 2</a></li></ul></b>"
                End If
            End If



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If errore = True Then
                btnSi.Enabled = False
                btnNo.Enabled = False
                btnProcedi.Enabled = False
            End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property sTipoContratto() As String
        Get
            If Not (ViewState("par_sTipoContratto") Is Nothing) Then
                Return CStr(ViewState("par_sTipoContratto"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sTipoContratto") = value
        End Set

    End Property

    Public Property idSegnalazione() As String
        Get
            If Not (ViewState("idSegnalazione") Is Nothing) Then
                Return CStr(ViewState("idSegnalazione"))
            Else
                Return "null"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("idSegnalazione") = value
        End Set

    End Property

    Public Property dataDaConsiderare() As String
        Get
            If Not (ViewState("par_dataDaConsiderare") Is Nothing) Then
                Return CStr(ViewState("par_dataDaConsiderare"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataDaConsiderare") = value
        End Set

    End Property

    Public Property errore() As Boolean
        Get
            If Not (ViewState("par_errore") Is Nothing) Then
                Return CLng(ViewState("par_errore"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_errore") = value
        End Set

    End Property

    Public Property cambioCons() As Boolean
        Get
            If Not (ViewState("par_cambioCons") Is Nothing) Then
                Return CLng(ViewState("par_cambioCons"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_cambioCons") = value
        End Set

    End Property

    Public Property piurecente() As Boolean
        Get
            If Not (ViewState("par_piurecente") Is Nothing) Then
                Return CLng(ViewState("par_piurecente"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_piurecente") = value
        End Set

    End Property

    Public Property idSind() As String
        Get
            If Not (ViewState("par_idSind") Is Nothing) Then
                Return CStr(ViewState("par_idSind"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_idSind") = value
        End Set

    End Property

    Public Property altro() As String
        Get
            If Not (ViewState("par_altro") Is Nothing) Then
                Return CStr(ViewState("par_altro"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_altro") = value
        End Set

    End Property

    Public Property codContrSc() As String
        Get
            If Not (ViewState("par_codContrSc") Is Nothing) Then
                Return CStr(ViewState("par_codContrSc"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_codContrSc") = value
        End Set

    End Property

    Public Property anni() As String
        Get
            If Not (ViewState("par_anni") Is Nothing) Then
                Return CStr(ViewState("par_anni"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_anni") = value
        End Set

    End Property

    Public Property dataPr() As String
        Get
            If Not (ViewState("par_dataPr") Is Nothing) Then
                Return CStr(ViewState("par_dataPr"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataPr") = value
        End Set

    End Property

    Public Property dataEvento() As String
        Get
            If Not (ViewState("par_dataEvento") Is Nothing) Then
                Return CStr(ViewState("par_dataEvento"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataEvento") = value
        End Set

    End Property

    Public Property dataInizio() As String
        Get
            If Not (ViewState("par_dataInizio") Is Nothing) Then
                Return CStr(ViewState("par_dataInizio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataInizio") = value
        End Set

    End Property

    Public Property dataFine() As String
        Get
            If Not (ViewState("par_dataFine") Is Nothing) Then
                Return CStr(ViewState("par_dataFine"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataFine") = value
        End Set

    End Property

    Public Property Tipo_Domanda() As Long
        Get
            If Not (ViewState("par_Tipo_Domanda") Is Nothing) Then
                Return CLng(ViewState("par_Tipo_Domanda"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_Tipo_Domanda") = value
        End Set

    End Property

    Public Property id_dichia() As Long
        Get
            If Not (ViewState("par_id_dichia") Is Nothing) Then
                Return CLng(ViewState("par_id_dichia"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_id_dichia") = value
        End Set

    End Property

    Public Property id_dom() As Long
        Get
            If Not (ViewState("par_id_dom") Is Nothing) Then
                Return CLng(ViewState("par_id_dom"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_id_dom") = value
        End Set

    End Property

    Public Property id_bando() As Long
        Get
            If Not (ViewState("par_id_bando") Is Nothing) Then
                Return CLng(ViewState("par_id_bando"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_id_bando") = value
        End Set

    End Property

    Public Property new_idDichia() As Long
        Get
            If Not (ViewState("par_new_idDichia") Is Nothing) Then
                Return CLng(ViewState("par_new_idDichia"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_new_idDichia") = value
        End Set

    End Property

    Public Property id_compon() As Long
        Get
            If Not (ViewState("par_id_compon") Is Nothing) Then
                Return CLng(ViewState("par_id_compon"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_id_compon") = value
        End Set

    End Property

    Public Property new_id_dom() As Long
        Get
            If Not (ViewState("par_new_id_dom") Is Nothing) Then
                Return CLng(ViewState("par_new_id_dom"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_new_id_dom") = value
        End Set

    End Property

    Public Property new_id_compon() As Long
        Get
            If Not (ViewState("par_new_id_compon") Is Nothing) Then
                Return CLng(ViewState("par_new_id_compon"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_new_id_compon") = value
        End Set

    End Property

    Public Property id_intest() As Long
        Get
            If Not (ViewState("par_id_intest") Is Nothing) Then
                Return CLng(ViewState("par_id_intest"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_id_intest") = value
        End Set

    End Property

    Public Property idLUOGOnasc() As Long
        Get
            If Not (ViewState("par_idLUOGOnasc") Is Nothing) Then
                Return CLng(ViewState("par_idLUOGOnasc"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idLUOGOnasc") = value
        End Set

    End Property

    Public Property idLUOGOres() As Long
        Get
            If Not (ViewState("par_idLUOGOres") Is Nothing) Then
                Return CLng(ViewState("par_idLUOGOres"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idLUOGOres") = value
        End Set

    End Property

    Public Property idTIPOres() As Long
        Get
            If Not (ViewState("par_idTIPOres") Is Nothing) Then
                Return CLng(ViewState("par_idTIPOres"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idTIPOres") = value
        End Set

    End Property

    Public Property indirizzoRes() As String
        Get
            If Not (ViewState("par_indirizzoRes") Is Nothing) Then
                Return CStr(ViewState("par_indirizzoRes"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_indirizzoRes") = value
        End Set

    End Property

    Public Property civicoRes() As String
        Get
            If Not (ViewState("par_civicoRes") Is Nothing) Then
                Return CStr(ViewState("par_civicoRes"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_civicoRes") = value
        End Set

    End Property

    Public Property capRes() As String
        Get
            If Not (ViewState("par_capRes") Is Nothing) Then
                Return CStr(ViewState("par_capRes"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_capRes") = value
        End Set

    End Property

    Public Property telefono() As String
        Get
            If Not (ViewState("par_telefono") Is Nothing) Then
                Return CStr(ViewState("par_telefono"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_telefono") = value
        End Set

    End Property

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
            lblMsg.Text = ex.Message
        End Try

    End Function

    'Verifica se per il bando AU, di cui fa parte il componente selezionato, esistono domande fatte in precedenza 
    'da cui poter importare i redditi
    Protected Sub visualizzaAUOLD()
        Dim iDanag As Long = 0
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            If tipoRich.Value = "2" Then
                lblAmpl.Visible = True
                imgAlert.Visible = True
            End If



            Select Case tipoRich.Value
                Case "2", "3"
                    par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA,ID FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                                        & "WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & id_contr.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                    Dim myReaderCF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderCF.Read Then
                        codFisc.Value = par.IfNull(myReaderCF("COD_FISCALE"), "")
                        iDanag = par.IfNull(myReaderCF("ID"), "")
                        intestatario.Value = par.IfNull(myReaderCF("NOMINATIVO"), "")
                    End If
                    myReaderCF.Close()
                Case Else
                    par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA,ID FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                                        & "WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & id_contr.Value & " AND ID_ANAGRAFICA = " & id_intestatario.Value
                    Dim myReaderCF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderCF.Read Then
                        codFisc.Value = par.IfNull(myReaderCF("COD_FISCALE"), "")
                        intestatario.Value = par.IfNull(myReaderCF("NOMINATIVO"), "")
                        iDanag = par.IfNull(myReaderCF("ID"), "")
                    End If
                    myReaderCF.Close()
            End Select

            If Request.QueryString("COMPEX") = "1" Then
                par.cmd.CommandText = "SELECT ID,COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,DATA_NASCITA FROM SISCOM_MI.ANAGRAFICA " _
                                    & "WHERE ANAGRAFICA.ID=" & id_intestatario.Value & " ORDER BY ID DESC"
                Dim myReaderCF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCF.Read Then
                    codFisc.Value = par.IfNull(myReaderCF("COD_FISCALE"), "")
                    iDanag = par.IfNull(myReaderCF("ID"), "")
                    intestatario.Value = par.IfNull(myReaderCF("NOMINATIVO"), "")
                End If
                myReaderCF.Close()
            End If

            If codFisc.Value <> "" Then
                If par.ControllaCF(codFisc.Value) = False Then
                    'Response.Write("<script>alert('Attenzione! Codice fiscale presente nel sistema errato!!')</script>")
                    lblErr.Visible = True
                    lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                    errore = True
                End If
            Else
                Response.Write("<script>alert('Attenzione! Codice fiscale non presente nel sistema!!')</script>")
            End If

            par.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(ID) AS STATO,COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & CodContratto.Value & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                    lblIntest.Text = " Cod. Contratto: <b>" & CodContratto.Value & "</b> <br/>Stato del contratto: <b>" & myReader0("STATO") & "</b>"
                Else
                    lblIntest.Text = "Componente: <b>" & intestatario.Value & "</b>&nbspCod. Contratto: <b>" & CodContratto.Value & "</b> <br/>Stato del contratto: <b>" & myReader0("STATO") & "</b>"
                End If
                sTipoContratto = myReader0("COD_TIPOLOGIA_CONTR_LOC")
            End If
            myReader0.Close()


            ''********* 28/05/2012 IMPORTO SOLO L'AU DEL 2009 *********
            'If Request.QueryString("CAUS") = "28" Then
            '    par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL<='" & dataDaConsiderare & "' AND UTENZA_DICHIARAZIONI.DATA_FINE_VAL>='" & dataDaConsiderare & "' AND NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
            '    & "AND RAPPORTO='" & CodContratto.Value & "' ORDER BY ID_BANDO DESC"
            '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader2.Read Then
            '        id_dom = myReader2("ID")
            '        id_dichia = myReader2("ID")
            '        Tipo_Domanda = 2
            '        icona.Visible = True
            '        lblMsg.Text = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " corrisponde alla domanda <b>""" & myReader2("NOME_BANDO") & """</b> (redditi " & myReader2("ANNO_ISEE") & ").<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."
            '        lblLink.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../../ANAUT/max.aspx?TORNA=0&CHIUDI=1&LE=1&ID=" & id_dichia & "','Dettagli','height=540,top=200,left=350,width=670');" & Chr(34) & ">Clicca qui per visualizzare la dichiarazione</a>"

            '        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND COD_FISCALE ='" & codFisc.Value & "'"
            '        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myReader3.Read = False Then
            '            lblErr.Visible = True

            '            par.cmd.CommandText = "select * from UTENZA_COMP_NUCLEO where COGNOME||' '||NOME ='" & UCase(intestatario.Value) & "' AND ID_DICHIARAZIONE=" & id_dichia
            '            Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '            If myReaderControllo.Read Then
            '                If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
            '                    lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
            '                    errore = True
            '                End If
            '            Else
            '                lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
            '                errore = True
            '            End If
            '            myReaderControllo.Close()

            '        Else
            '            lblErr.Visible = False
            '            errore = False
            '        End If
            '        myReader3.Close()
            '    End If
            'End If

            If tipoRich.Value = "3" Then
                If dataInizio = "" Or dataFine = "" Then
                    lblErr.Visible = True
                    lblErr.Text = "Attenzione! Date di validità non valorizzate! Si prega di ripetere l'operazione!"
                    errore = True
                End If
            End If

            If tipoRich.Value = "3" Or tipoRich.Value = "4" Then
                dataDaConsiderare = dataInizio
            Else
                dataDaConsiderare = dataEvento
            End If

            If id_dichia = 0 Then

                If causale.Value = "29" Then
                    par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL<='" & dataDaConsiderare & "' AND UTENZA_DICHIARAZIONI.DATA_FINE_VAL>='" & dataDaConsiderare & "' AND NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                        & "AND RAPPORTO='" & CodContratto.Value & "' ORDER BY ID_BANDO DESC"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        id_dom = myReader2("ID")
                        id_dichia = myReader2("ID")
                        Tipo_Domanda = 2
                        icona.Visible = True
                        lblMsg.Text = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " corrisponde alla domanda <b>""" & myReader2("NOME_BANDO") & """</b> (redditi " & myReader2("ANNO_ISEE") & ").<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."
                        lblLink.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../../ANAUT/DichAUnuova.aspx?TORNA=0&CHIUDI=1&LE=1&ID=" & id_dichia & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">Clicca qui per visualizzare la dichiarazione</a>"

                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND COD_FISCALE ='" & codFisc.Value & "'"
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader3.Read = False Then
                            lblErr.Visible = True

                            par.cmd.CommandText = "select * from UTENZA_COMP_NUCLEO where COGNOME||' '||NOME ='" & UCase(par.PulisciStrSql(intestatario.Value)) & "' AND ID_DICHIARAZIONE=" & id_dichia
                            Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderControllo.Read Then
                                If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
                                    lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                                    errore = True
                                End If
                            Else
                                lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
                                errore = True
                            End If
                            myReaderControllo.Close()

                        Else
                            lblErr.Visible = False
                            errore = False
                        End If
                        myReader3.Close()
                    Else

                        lblMsg.Text = "<br/>Al momento non risultano esserci dichiarazioni di Anagrafe Utenza, domande di bando ERP o <br/>precedenti istanze di gestione locatari.<br/><br/>Clicca su Procedi per importare la situazione anagrafica attualmente presente nel rapporto."
                        btnSi.Visible = False
                        btnNo.Visible = False
                        btnEsci.Visible = False
                        btnProcedi.Visible = True
                        btnEsci2.Visible = True
                        lblLink.Visible = False
                        icona.Visible = False
                        Tipo_Domanda = 4

                    End If
                    myReader2.Close()
                Else
                    Dim dataFineVal As String = ""
                    If Request.QueryString("COMPEX") = "" Then
                        par.cmd.CommandText = "SELECT T_MOTIVO_DOMANDA_VSA.*,DOMANDE_BANDO_VSA.ID AS IDDOM,DOMANDE_BANDO_VSA.*,DATA_FINE_VAL FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID_STATO <> 2 AND nvl(DICHIARAZIONI_VSA.DATA_FINE_VAL, DATA_INIZIO_VAL)>='" & dataDaConsiderare & "' and nvl(DICHIARAZIONI_VSA.DATA_FINE_VAL, DATA_INIZIO_VAL)<='29991231' AND (DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE=1 OR ID_STATO_ISTANZA=5) AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & CodContratto.Value & "' ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            importRedd.Value = "1"
                            id_dom = myReader1("IDDOM")
                            dataFineVal = myReader1("DATA_FINE_VAL")
                            id_dichia = myReader1("ID_DICHIARAZIONE")
                            Tipo_Domanda = 1
                            icona.Visible = True
                            'lblMsg.Text = "<br/>Ha presentato domanda di richiesta ""<b>" & myReader1("DESCRIZIONE") & "</b>"" in data " & par.FormattaData(myReader1("DATA_PG")) & ".<br/>Si desidera importare i redditi?"
                            lblMsg.Text = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " corrisponde alla domanda di <b>""" & myReader1("DESCRIZIONE") & """</b> presentata in data " & par.FormattaData(myReader1("DATA_presentazione")) & ".<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."
                            lblLink.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & id_dichia & "&CH=2&LE=1','DichVSA','top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">Clicca qui per visualizzare la dichiarazione</a>"

                            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia & " AND COD_FISCALE ='" & codFisc.Value & "'"
                            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader3.Read = False Then
                                lblErr.Visible = True
                                'lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"

                                par.cmd.CommandText = "select * from comp_nucleo_vsa where COGNOME||' '||NOME ='" & UCase(par.PulisciStrSql(intestatario.Value)) & "' AND ID_DICHIARAZIONE=" & id_dichia
                                Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderControllo.Read Then
                                    If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
                                        lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                                        errore = True
                                    End If
                                Else
                                    If sTipoContratto <> "EQC392" Then
                                        lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
                                        errore = True
                                    End If
                                End If
                                myReaderControllo.Close()

                            Else
                                lblErr.Visible = False
                                errore = False
                            End If
                            myReader3.Close()

                        Else
                            'MariaTeresa Modify 16/02/2012 per prendere anche le domande AU il cui bando è chiuso
                            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL<='" & dataDaConsiderare & "' AND UTENZA_DICHIARAZIONI.DATA_FINE_VAL>='" & dataDaConsiderare & "' AND NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                            & "AND RAPPORTO='" & CodContratto.Value & "' ORDER BY ID_BANDO DESC"
                            'par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                            '& "AND RAPPORTO='" & CodContratto.Value & "' AND STATO = 1 ORDER BY ID_BANDO DESC"
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                id_dom = myReader2("ID")
                                id_dichia = myReader2("ID")
                                Tipo_Domanda = 2
                                icona.Visible = True
                                'lblMsg.Text = "<br/>Ha presentato domanda nel bando Anagrafe Utenza <b>""" & Right(myReader2("NOME_BANDO"), 4) & """</b>. Si desidera importare i redditi?"
                                lblMsg.Text = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " corrisponde alla domanda <b>""" & myReader2("NOME_BANDO") & """</b> (redditi " & myReader2("ANNO_ISEE") & ").<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."
                                lblLink.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../../ANAUT/DichAUnuova.aspx?TORNA=0&CHIUDI=1&LE=1&ID=" & id_dichia & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">Clicca qui per visualizzare la dichiarazione</a>"

                                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND COD_FISCALE ='" & codFisc.Value & "'"
                                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader3.Read = False Then
                                    lblErr.Visible = True

                                    par.cmd.CommandText = "select * from UTENZA_COMP_NUCLEO where COGNOME||' '||NOME ='" & UCase(par.PulisciStrSql(intestatario.Value)) & "' AND ID_DICHIARAZIONE=" & id_dichia
                                    Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderControllo.Read Then
                                        If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
                                            lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                                            errore = True
                                        End If
                                    Else
                                        If sTipoContratto <> "EQC392" Then
                                            lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
                                            errore = True
                                        End If
                                    End If
                                    myReaderControllo.Close()

                                Else
                                    lblErr.Visible = False
                                    errore = False
                                End If
                                myReader3.Close()
                            Else

                                par.cmd.CommandText = "SELECT T_MOTIVO_DOMANDA_VSA.*,DOMANDE_BANDO_VSA.ID AS IDDOM,DOMANDE_BANDO_VSA.* FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID_STATO <> 2 AND nvl(DICHIARAZIONI_VSA.DATA_FINE_VAL, DATA_INIZIO_VAL)<='29991231' AND (DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE=1 OR ID_STATO_ISTANZA=5) AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & CodContratto.Value & "' ORDER BY DATA_FINE_VAL DESC,DOMANDE_BANDO_VSA.DATA_PG DESC"
                                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderX.Read Then
                                    importRedd.Value = "1"
                                    id_dom = myReaderX("IDDOM")
                                    id_dichia = myReaderX("ID_DICHIARAZIONE")
                                    Tipo_Domanda = 1
                                    icona.Visible = True
                                    'lblMsg.Text = "<br/>Ha presentato domanda di richiesta ""<b>" & myReader1("DESCRIZIONE") & "</b>"" in data " & par.FormattaData(myReader1("DATA_PG")) & ".<br/>Si desidera importare i redditi?"
                                    lblMsg.Text = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " corrisponde alla domanda di <b>""" & myReaderX("DESCRIZIONE") & """</b> presentata in data " & par.FormattaData(myReaderX("DATA_presentazione")) & ".<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."
                                    lblLink.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & id_dichia & "&CH=2&LE=1','DichVSA','top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">Clicca qui per visualizzare la dichiarazione</a>"

                                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia & " AND COD_FISCALE ='" & codFisc.Value & "'"
                                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReader3.Read = False Then
                                        lblErr.Visible = True

                                        'lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"

                                        par.cmd.CommandText = "select * from comp_nucleo_vsa where COGNOME||' '||NOME ='" & UCase(par.PulisciStrSql(intestatario.Value)) & "' AND ID_DICHIARAZIONE=" & id_dichia
                                        Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                        If myReaderControllo.Read Then
                                            If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
                                                lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                                                errore = True
                                            End If
                                        Else
                                            If sTipoContratto <> "EQC392" Then
                                                lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
                                                errore = True
                                            End If
                                        End If
                                        myReaderControllo.Close()

                                    Else
                                        lblErr.Visible = False
                                        errore = False
                                    End If
                                    myReader3.Close()
                                Else


                                    par.cmd.CommandText = "SELECT DOMANDE_BANDO.*,bandi.descrizione,BANDI.ANNO_ISEE FROM BANDI,DOMANDE_BANDO WHERE BANDI.ID=DOMANDE_BANDO.ID_BANDO AND  CONTRATTO_NUM='" & CodContratto.Value & "'"
                                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReader3.Read Then
                                        importRedd.Value = "2"
                                        id_dom = myReader3("ID")
                                        id_dichia = myReader3("ID_DICHIARAZIONE")
                                        Tipo_Domanda = 3
                                        lblMsg.Text = "<br/>La situazione anagrafica e reddituale alla data del " & par.FormattaData(dataDaConsiderare) & " non è stata trovata. L'ultima situazione disponibile corrisponde alla domanda di bando ERP <b>""" & myReader3("DESCRIZIONE") & """</b> (redditi " & myReader3("ANNO_ISEE") & ").<br/><br/>Premere SI per importare la situazione REDDITUALE E ANAGRAFICA, premere NO per importare la sola situazione ANAGRAFICA."
                                        icona.Visible = False

                                        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND COD_FISCALE ='" & codFisc.Value & "'"
                                        Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                        If myReader4.Read = False Then
                                            lblErr.Visible = True

                                            par.cmd.CommandText = "select * from COMP_NUCLEO where COGNOME||' '||NOME ='" & UCase(par.PulisciStrSql(intestatario.Value)) & "' AND ID_DICHIARAZIONE=" & id_dichia
                                            Dim myReaderControllo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                            If myReaderControllo.Read Then
                                                If par.IfNull(myReaderControllo("COD_FISCALE"), "") <> codFisc.Value Then
                                                    lblErr.Text = "Il cod. fiscale del dichiarante presente nel sistema NON è corretto. Per procedere modificare prima il dato in anagrafica!"
                                                    errore = True
                                                End If
                                            Else
                                                If sTipoContratto <> "EQC392" Then
                                                    lblErr.Text = "Attenzione! La domanda non può essere intestata al componente scelto. Si prega di ripetere la procedura selezionando un altro componente!"
                                                    errore = True
                                                End If
                                            End If
                                            myReaderControllo.Close()

                                        Else
                                            lblErr.Visible = False
                                            errore = False
                                        End If
                                        myReader4.Close()
                                    Else
                                        lblMsg.Text = "<br/>Al momento non risultano esserci dichiarazioni di Anagrafe Utenza, domande di bando ERP o <br/>precedenti istanze di gestione locatari.<br/><br/>Clicca su Procedi per importare la situazione anagrafica attualmente presente nel rapporto."
                                        btnSi.Visible = False
                                        btnNo.Visible = False
                                        btnEsci.Visible = False
                                        btnProcedi.Visible = True
                                        btnEsci2.Visible = True
                                        lblLink.Visible = False
                                        icona.Visible = False
                                        Tipo_Domanda = 4
                                    End If
                                    myReader3.Close()
                                End If
                                myReaderX.Close()

                            End If
                            myReader2.Close()

                        End If
                        myReader1.Close()
                    Else
                        lblMsg.Text = "<br/>Al momento non risultano esserci dichiarazioni di Anagrafe Utenza, domande di bando ERP o <br/>precedenti istanze di gestione locatari.<br/><br/>Clicca su Procedi per importare la situazione anagrafica attualmente presente nel rapporto."
                        btnSi.Visible = False
                        btnNo.Visible = False
                        btnEsci.Visible = False
                        btnProcedi.Visible = True
                        btnEsci2.Visible = True
                        lblLink.Visible = False
                        icona.Visible = False
                        Tipo_Domanda = 4
                    End If
                End If
            End If

            par.cmd.CommandText = "SELECT MAX(ID) AS ID_BANDO FROM BANDI_VSA WHERE STATO = 1"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderB.Read Then
                id_bando = myReaderB("ID_BANDO")
            End If
            myReaderB.Close()

            If tipoRich.Value = "0" Then
                par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id_tipo = 4 and importo_totale <> NVL(importo_pagato,0) and id_contratto in (select id from siscom_mi.rapporti_utenza where cod_contratto='" & CodContratto.Value & "')"
                Dim lettMorosita As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettMorosita.Read Then
                    lblAmpl.Visible = True
                    lblAmpl.Text = "Attenzione! Risultano esserci delle morosità per questo rapporto di utenza."
                    imgAlert.Visible = True
                End If
                lettMorosita.Close()
            End If

            If tipoRich.Value = "0" Or tipoRich.Value = "10" Then
                If Request.QueryString("DEB") > 0 Then
                    lblAmpl.Visible = True
                    imgAlert.Visible = True
                    'lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere." & vbCrLf & "<a href=" & Chr(34) & "javascript:document.getElementById('stampaDeb').value='1';document.location.reload();" & Chr(34) & " onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&RICH=" & intestatario.Value & "&IMP= " & Request.QueryString("IMP") & "&INTEST=" & intestatario.Value & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></b>"
                    lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere." & vbCrLf & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&RICH=" & Replace(intestatario.Value, "'", "\'") & "&IMP= " & Request.QueryString("IMP") & "&NRA=" & Request.QueryString("NRA") & "&INTEST=" & Replace(intestatario.Value, "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></b>"
                End If
            End If

            If tipoRich.Value = "5" Then
                If Request.QueryString("DEB") > 0 And Request.QueryString("DEB2") > 0 Then
                    lblAmpl.Visible = True
                    imgAlert.Visible = True
                    'lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere." & vbCrLf & "<a href=" & Chr(34) & "javascript:document.getElementById('stampaDeb').value='1';document.location.reload();" & Chr(34) & " onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&RICH=" & intestatario.Value & "&IMP= " & Request.QueryString("IMP") & "&INTEST=" & intestatario.Value & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></b>"
                    lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere.<br/><ul><li>" & vbCrLf & intestatario.Value & " <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&IMP= " & Request.QueryString("IMP") & "&NRA=" & Request.QueryString("NRA") & "&INTEST=" & Replace(intestatario.Value, "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></li><li>" & Request.QueryString("INTEST2") & " <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & Request.QueryString("CODCONTR2") & "&IMP= " & Request.QueryString("IMP2") & "&NRA=" & Request.QueryString("NRA2") & "&INTEST=" & Replace(Request.QueryString("INTEST2"), "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO 2</a></li></ul></b>"
                ElseIf Request.QueryString("DEB") > 0 Then
                    lblAmpl.Visible = True
                    imgAlert.Visible = True

                    lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere.<br/><ul><li>" & vbCrLf & intestatario.Value & " <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & CodContratto.Value & "&IMP= " & Request.QueryString("IMP") & "&NRA=" & Request.QueryString("NRA") & "&INTEST=" & Replace(intestatario.Value, "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO</a></li></ul></b>"
                ElseIf Request.QueryString("DEB2") > 0 Then
                    lblAmpl.Visible = True
                    imgAlert.Visible = True

                    lblAmpl.Text = "<b>Attenzione! Stampare la comunicazione di accettazione del debito prima di procedere.<br/><ul><li>" & vbCrLf & Request.QueryString("INTEST2") & " <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../StampeDoc.aspx?IDDICHIARAZ=" & id_dichia & "&NUMCONT=" & Request.QueryString("CODCONTR2") & "&IMP= " & Request.QueryString("IMP2") & "&NRA=" & Request.QueryString("NRA2") & "&INTEST=" & Replace(Request.QueryString("INTEST2"), "'", "\'") & "&TIPO=ComDEB','');" & Chr(34) & ">STAMPA MODELLO 2</a></li></ul></b>"
                End If
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If errore = True Then
                btnSi.Enabled = False
                btnNo.Enabled = False
                btnProcedi.Enabled = False
            End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub


    '24/12/2014 Funzione per controllare la presenza di un ReCa per lo stesso anno di reddito
    Private Sub CercaReca()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND ID_MOTIVO_DOMANDA=3 AND DICHIARAZIONI_VSA.ID_STATO <> 2 AND DICHIARAZIONI_VSA.ANNO_SIT_ECONOMICA=" & anni & " AND DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE=1 AND CONTRATTO_NUM='" & CodContratto.Value & "' ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC,DOMANDE_BANDO_VSA.PG DESC"
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderB.Read Then
                lblMsgReca.Visible = True
                lblMsgReca.Text = "Attenzione...esiste già una pratica di ReCa per l'anno di reddito " & anni & "!"
            End If
            myReaderB.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    '24/12/2014 Fine funzione


    'Crea una nuova dichiarazione vuota (carica solo i componenti del nucleo) nel caso in cui non risultino esserci domande fatte in precedenza
    Protected Sub CaricaNOdichiarazione(Optional ByRef codFiscale As String = "", Optional ByRef nomeIntest As String = "", Optional ByRef codContr2 As String = "", Optional ByRef idCont As Long = 0, Optional ByRef PGDomcollegato As String = "", Optional ByRef PGDichcollegato As String = "")

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim num_comp_nucleo As Integer
        Dim prog As Integer
        Dim prog_int As Integer
        Dim prog_comp As Integer
        'Dim anno As Integer
        Dim strScript As String


        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            valorePG = Format(lValoreCorrente, "0000000000")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans



            par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.NEXTVAL FROM DUAL"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                new_idDichia = myReader(0)
            End If
            myReader.Close()


            If codFiscale <> "" Then
                codFisc.Value = codFiscale
            End If
            If nomeIntest <> "" Then
                intestatario.Value = nomeIntest
            End If
            If idCont <> 0 Then
                id_contr.Value = idCont
            End If
            If codContr2 <> "" Then
                CodContratto.Value = codContr2
                cambioCons = True
            End If

            If cambioCons = True Then
                par.cmd.CommandText = "UPDATE DICHIARAZIONI_VSA SET PG_COLLEGATO = '" & valorePG & "' WHERE pg =" & PGDichcollegato
                par.cmd.ExecuteNonQuery()
            End If

            If Request.QueryString("ANNI") <> "" Then
                annoReddito = Request.QueryString("ANNI")
            Else
                annoReddito = Year(Now)
            End If

            '************* 13/03/2012 CERCO L'ULTIMA DICHIARAZIONE DA CUI IMPORTARE GLI INDIRIZZI AGGIORNATI

            piurecente = CercaUltimeDich(idLUOGOres, idTIPOres, indirizzoRes, civicoRes, capRes, telefono)

            par.cmd.CommandText = "SELECT COUNT(ID_ANAGRAFICA) AS NUM_COMP_NUCLEO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO = " & id_contr.Value & " AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                num_comp_nucleo = myReader("NUM_COMP_NUCLEO")
            End If
            myReader.Close()

            If Request.QueryString("COMPEX") <> "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE " _
                & "(ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & id_contr.Value & " AND " _
                & "(ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
            End If
            Dim myReaderAnagr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderAnagr.Read Then
                id_intest = myReaderAnagr("ID")

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD ='" & par.IfNull(myReaderAnagr("COD_COMUNE_NASCITA"), "") & "'" _
                    & " OR NOME ='" & par.IfNull(myReaderAnagr("COD_COMUNE_NASCITA"), "") & "'"
                Dim myReaderIDluogoNAS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderIDluogoNAS.Read Then
                    idLUOGOnasc = par.IfNull(myReaderIDluogoNAS("ID"), "")
                End If
                myReaderIDluogoNAS.Close()

                If piurecente = False Then
                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReaderAnagr("COMUNE_RESIDENZA"), "") & "'"
                    Dim myReaderIDluogoRES As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderIDluogoRES.Read Then
                        idLUOGOres = par.IfNull(myReaderIDluogoRES("ID"), "")
                    End If
                    myReaderIDluogoRES.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO WHERE DESCRIZIONE =  '" & RicavaVial(par.IfNull(myReaderAnagr("INDIRIZZO_RESIDENZA"), "")) & "'"
                    Dim myReaderIDTipoRES As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderIDTipoRES.Read Then
                        idTIPOres = myReaderIDTipoRES("COD")
                    End If
                    myReaderIDTipoRES.Close()
                End If

                If piurecente = False Then
                    par.cmd.CommandText = "INSERT INTO Dichiarazioni_VSA (ID,ID_CAF,ID_BANDO,PG,DATA_PG,LUOGO,DATA,ID_STATO,ID_LUOGO_NAS_DNTE,TELEFONO_DNTE,ID_LUOGO_RES_DNTE," _
                    & " ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,CAP_RES_DNTE,MOD_PRESENTAZIONE,N_COMP_NUCLEO,PROGR_DNTE,ANNO_SIT_ECONOMICA,DATA_FINE_VAL,DATA_INIZIO_VAL,ID_SINDACATO_VSA,MOD_PRES_ALTRO,PG_COLLEGATO)" _
                    & " VALUES (" & new_idDichia & "," & Session.Item("ID_CAF") & "," & id_bando & ",'" & valorePG & "','" & Format(Now, "yyyyMMdd") & "'," _
                    & " 'Milano','" & Format(Now, "yyyyMMdd") & "',0," & idLUOGOnasc & ",'" & par.IfNull(myReaderAnagr("TELEFONO"), "") & "'," _
                    & "'" & idLUOGOres & "','" & idTIPOres & "','" & par.PulisciStrSql(par.IfNull(myReaderAnagr("INDIRIZZO_RESIDENZA"), "")) & "','" & par.IfNull(myReaderAnagr("CIVICO_RESIDENZA"), "") & "'," _
                    & "'" & par.IfNull(myReaderAnagr("CAP_RESIDENZA"), "") & "'," & ModRichiesta.Value & "," & num_comp_nucleo & ",0," & annoReddito & ",'" & dataFine & "','" & dataInizio & "','" & idSind & "','" & altro & "','" & PGDichcollegato & "')"
                Else
                    par.cmd.CommandText = "INSERT INTO Dichiarazioni_VSA (ID,ID_CAF,ID_BANDO,PG,DATA_PG,LUOGO,DATA,ID_STATO,ID_LUOGO_NAS_DNTE,TELEFONO_DNTE,ID_LUOGO_RES_DNTE," _
                    & " ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,CAP_RES_DNTE,MOD_PRESENTAZIONE,N_COMP_NUCLEO,PROGR_DNTE,ANNO_SIT_ECONOMICA,DATA_FINE_VAL,DATA_INIZIO_VAL,ID_SINDACATO_VSA,MOD_PRES_ALTRO,PG_COLLEGATO)" _
                    & " VALUES (" & new_idDichia & "," & Session.Item("ID_CAF") & "," & id_bando & ",'" & valorePG & "','" & Format(Now, "yyyyMMdd") & "'," _
                    & " 'Milano','" & Format(Now, "yyyyMMdd") & "',0," & idLUOGOnasc & ",'" & telefono & "'," _
                    & "'" & idLUOGOres & "','" & idTIPOres & "','" & par.PulisciStrSql(indirizzoRes) & "','" & civicoRes & "'," _
                    & "'" & capRes & "'," & ModRichiesta.Value & "," & num_comp_nucleo & ",0," & annoReddito & ",'" & dataFine & "','" & dataInizio & "','" & idSind & "','" & altro & "','" & PGDichcollegato & "')"
                End If
                par.cmd.ExecuteNonQuery()
            End If
            myReaderAnagr.Close()

            If sTipoContratto <> "EQC392" Then
                If Request.QueryString("COMPEX") <> "" Then
                    par.cmd.CommandText = "SELECT * FROM siscom_mi.anagrafica WHERE ID =" & id_intest
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & id_contr.Value & " AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"
                End If
                Dim myReaderComNucleo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderComNucleo.Read
                    id_compon = myReaderComNucleo("ID")
                    prog = 1
                    If id_intest = id_compon Then
                        prog = 0
                    Else
                        prog_int = prog_int + 1
                    End If
                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.NEXTVAL FROM DUAL"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        new_id_compon = myReader1(0)
                    End If
                    myReader1.Close()

                    If Not prog = 0 Then
                        prog_comp = prog_int
                    Else
                        prog_comp = prog
                    End If

                    par.cmd.CommandText = "SELECT TIPOLOGIA_PARENTELA.* FROM SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD(+)" _
                        & " AND SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=" & id_compon
                    Dim myReaderParente As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderParente.Read Then
                        par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PROGR,PERC_INVAL,INDENNITA_ACC) " _
                        & "VALUES (" & new_id_compon & "," & new_idDichia & ",'" & par.IfNull(myReaderComNucleo("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReaderComNucleo("COGNOME"), "")) & "'," _
                        & "'" & par.PulisciStrSql(par.IfNull(myReaderComNucleo("NOME"), "")) & "','" & par.IfNull(myReaderComNucleo("SESSO"), "") & "','" & par.IfNull(myReaderComNucleo("DATA_NASCITA"), "") & "'," _
                        & par.IfNull(myReaderParente("ID_SEPA"), "") & "," & prog_comp & ",0,'0')"
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PROGR,PERC_INVAL,INDENNITA_ACC) " _
                            & "VALUES (" & new_id_compon & "," & new_idDichia & ",'" & par.IfNull(myReaderComNucleo("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReaderComNucleo("COGNOME"), "")) & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReaderComNucleo("NOME"), "")) & "','" & par.IfNull(myReaderComNucleo("SESSO"), "") & "','" & par.IfNull(myReaderComNucleo("DATA_NASCITA"), "") & "'," _
                            & "NULL," & prog_comp & ",0,'0')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderParente.Close()

                End While
                myReaderComNucleo.Close()
            Else
                par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.NEXTVAL FROM DUAL"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    new_id_compon = myReader1(0)
                End If
                myReader1.Close()

                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PROGR,PERC_INVAL,INDENNITA_ACC) " _
                           & "VALUES (" & new_id_compon & "," & new_idDichia & ",'',''," _
                           & "'','',''," _
                           & "NULL," & prog_comp & ",0,'0')"
                par.cmd.ExecuteNonQuery()

            End If

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaNOdomanda(PGDomcollegato)

            If PGDomcollegato = "" And tipoRich.Value <> "3" And tipoRich.Value <> "2" Then
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','DichVSA','top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                & "else{window.close();}</script>"
                Response.Write(strScript)
            End If
            If tipoRich.Value = "2" And causale.Value = "30" Then
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','DichVSA','top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                & "else{window.close();}</script>"
                Response.Write(strScript)
            End If

            'Response.Write("<script>alert('Operazione effettuata con successo!');location.replace('nuova_domanda.aspx');</script>")

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    'Crea una nuova domanda sempre nel caso in cui non risultino esserci domande fatte in precedenza 
    Protected Sub CaricaNOdomanda(Optional ByRef pgDomCollegato As String = "")

        'Dim new_id_dom As Long
        Dim iDluogo As Long
        Dim codIndir As Integer
        Dim sup_netta As Decimal
        Dim ascens As Integer
        Dim lValoreCorrenteDom As Long
        Dim valorePGdom As String
        Dim tipoBando As Long
        Dim anno As Integer

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrenteDom = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrenteDom & ")"
            par.cmd.ExecuteNonQuery()
            valorePGdom = Format(lValoreCorrenteDom, "0000000000")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans



            par.cmd.CommandText = "SELECT * FROM BANDI_VSA WHERE ID=" & id_bando
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                tipoBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
                anno = par.IfNull(myReader("ANNO_ISEE"), "-1")
            End If
            myReader.Close()



            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO ='" & CodContratto.Value & "'"
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    id_contratto = myReader("ID")
            'End If
            'myReader.Close()
            If cambioCons = True Then
                codContrSc = Request.QueryString("COD")
                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET PG_COLLEGATO = '" & valorePGdom & "' WHERE pg =" & pgDomCollegato
                par.cmd.ExecuteNonQuery()
                Tipo_Domanda = 4
            End If

            If Request.QueryString("COMPEX") <> "" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.COD_CONTRATTO ='" & CodContratto.Value & "'"
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.COD_CONTRATTO ='" & CodContratto.Value & "' AND (ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
            End If
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & myReader("LUOGO_COR") & "'"
                Dim myReaderIDluogo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderIDluogo.Read Then
                    iDluogo = myReaderIDluogo("ID")
                End If
                myReaderIDluogo.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE DESCRIZIONE='" & par.PulisciStrSql(myReader("TIPO_COR")) & "'"
                Dim myReaderTipoIndir As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderTipoIndir.Read Then
                    codIndir = myReaderTipoIndir("COD")
                End If
                myReaderTipoIndir.Close()

                par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_VSA.NEXTVAL FROM DUAL"
                Dim myReaderNew As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderNew.Read() Then
                    new_id_dom = myReaderNew(0)
                End If
                myReaderNew.Close()

                If dataPr = "" Then
                    dataPr = Format(Now, "yyyyMMdd")
                End If

                'If myReader("COD_TIPOLOGIA_OCCUPANTE") = "INTE" Then
                '    progr = 0
                'Else
                '    progr = 1
                'End If

                'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SCALE_EDIFICI WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO " _
                '    & "AND SCALE_EDIFICI.ID = UNITA_IMMOBILIARI.ID_SCALA AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA AND ID_CONTRATTO =" & id_contr.Value
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.id_unita_principale is null and UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA AND ID_CONTRATTO =" & id_contr.Value
                Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderUI.Read Then

                    If piurecente = False Then
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA (ID,ID_BANDO,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA," _
                        & "ID_DICHIARAZIONE,PROGR_COMPONENTE,PG,DATA_PG,PRESSO_REC_DNTE,IND_REC_DNTE,ID_LUOGO_REC_DNTE,TELEFONO_REC_DNTE," _
                        & "ID_TIPO_IND_REC_DNTE,CIVICO_REC_DNTE,CAP_REC_DNTE,ID_MOTIVO_DOMANDA," _
                        & "DATA_PRESENTAZIONE,TIPO_ALLOGGIO,FL_MOROSITA,FL_PROFUGO,ANNO_RIF_CANONE,IMPORTO_CANONE,ANNO_RIF_SPESE_ACC,IMPORTO_SPESE_ACC," _
                        & "ID_PARA_0,ID_PARA_1,ID_PARA_2,ID_PARA_3,ID_PARA_4,ID_PARA_5,ID_PARA_6,ID_PARA_7,ID_PARA_8,ID_PARA_9,ID_PARA_10,ID_PARA_11,ID_PARA_12," _
                        & "ID_PARA_13,ID_PARA_14,ID_PARA_15,REQUISITO1,REQUISITO2,REQUISITO3,REQUISITO4,REQUISITO5,REQUISITO6,REQUISITO7,REQUISITO8,REQUISITO9," _
                        & "ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E,FL_ISTRUTTORIA_COMPLETA,FL_COMPLETA,FL_ESAMINATA,FL_PROPOSTA,FL_CONTROLLA_REQUISITI," _
                        & "FL_INVITO,CONTRATTO_NUM,CONTRATTO_DATA,NUM_ALLOGGIO,FL_RINNOVO,PERIODO_RES,CONTRATTO_DATA_DEC," _
                        & "FL_ASS_ESTERNA,FL_FATTA_AU,FL_FATTA_ERP,FL_FAI_ERP,REQUISITO10,REQUISITO11,REQUISITO12,REQUISITO13,REQUISITO14,REQUISITO15,REQUISITO16," _
                        & "FL_VERIFICA_CONCLUSA,REQUISITO17,REQUISITO18,REQUISITO19,FL_NATO_ESTERO,ACCOLTA,TIPO_U,ID_CAUSALE_DOMANDA,DATA_EVENTO,TIPO_D_IMPORT,ID_D_IMPORT,COD_CONTRATTO_SCAMBIO,PG_COLLEGATO,ID_INTEST_NEW_RU) " _
                        & "VALUES (" & new_id_dom & "," & id_bando & ",'0','1','0','0','" & tipoBando & "'," & new_idDichia & ",0," _
                        & "'" & valorePGdom & "','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfNull(myReader("PRESSO_COR"), "")) & "'," _
                        & "'" & par.PulisciStrSql(par.IfNull(myReader("VIA_COR"), "")) & "','" & iDluogo & "','" & par.IfNull(myReader("TELEFONO"), "") & "','" & codIndir & "'," _
                        & "'" & par.IfNull(myReader("CIVICO_COR"), "") & "','" & par.IfNull(myReader("CAP_COR"), "") & "'," & tipoRich.Value & "," _
                        & "'" & dataPr & "','0','1','0','" & anno & "',0,'" & anno & "',0,-1,-1,-1,-1,-1,-1,-1,-1," _
                        & "-1,-1,-1,-1,-1,-1,-1,-1,'1','1','1','1','1','1','1','1','1',0,0,0,0,0,0,'0','0','0','0',NULL,'0','" & CodContratto.Value & "','" _
                        & par.IfNull(myReader("DATA_STIPULA"), "") & "','" & myReaderUI("COD_UNITA_IMMOBILIARE") & "','0',-1,'" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "'," _
                        & "'1','1','1','0','1','1','1','1','1','1','1','0','1','1','1','0','0','0','" & causale.Value & "','" & dataEvento & "'," & Tipo_Domanda & "," & id_contr.Value & ",'" & codContrSc & "','" & pgDomCollegato & "'," & par.IfEmpty(intestNewRU.Value, "NULL") & ")"
                    Else
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA (ID,ID_BANDO,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA," _
                        & "ID_DICHIARAZIONE,PROGR_COMPONENTE,PG,DATA_PG,PRESSO_REC_DNTE,IND_REC_DNTE,ID_LUOGO_REC_DNTE,TELEFONO_REC_DNTE," _
                        & "ID_TIPO_IND_REC_DNTE,CIVICO_REC_DNTE,CAP_REC_DNTE,ID_MOTIVO_DOMANDA," _
                        & "DATA_PRESENTAZIONE,TIPO_ALLOGGIO,FL_MOROSITA,FL_PROFUGO,ANNO_RIF_CANONE,IMPORTO_CANONE,ANNO_RIF_SPESE_ACC,IMPORTO_SPESE_ACC," _
                        & "ID_PARA_0,ID_PARA_1,ID_PARA_2,ID_PARA_3,ID_PARA_4,ID_PARA_5,ID_PARA_6,ID_PARA_7,ID_PARA_8,ID_PARA_9,ID_PARA_10,ID_PARA_11,ID_PARA_12," _
                        & "ID_PARA_13,ID_PARA_14,ID_PARA_15,REQUISITO1,REQUISITO2,REQUISITO3,REQUISITO4,REQUISITO5,REQUISITO6,REQUISITO7,REQUISITO8,REQUISITO9," _
                        & "ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E,FL_ISTRUTTORIA_COMPLETA,FL_COMPLETA,FL_ESAMINATA,FL_PROPOSTA,FL_CONTROLLA_REQUISITI," _
                        & "FL_INVITO,CONTRATTO_NUM,CONTRATTO_DATA,NUM_ALLOGGIO,FL_RINNOVO,PERIODO_RES,CONTRATTO_DATA_DEC," _
                        & "FL_ASS_ESTERNA,FL_FATTA_AU,FL_FATTA_ERP,FL_FAI_ERP,REQUISITO10,REQUISITO11,REQUISITO12,REQUISITO13,REQUISITO14,REQUISITO15,REQUISITO16," _
                        & "FL_VERIFICA_CONCLUSA,REQUISITO17,REQUISITO18,REQUISITO19,FL_NATO_ESTERO,ACCOLTA,TIPO_U,ID_CAUSALE_DOMANDA,DATA_EVENTO,TIPO_D_IMPORT,ID_D_IMPORT,COD_CONTRATTO_SCAMBIO,PG_COLLEGATO,ID_INTEST_NEW_RU) " _
                        & "VALUES (" & new_id_dom & "," & id_bando & ",'0','1','0','0','" & tipoBando & "'," & new_idDichia & ",0," _
                        & "'" & valorePGdom & "','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfNull(myReader("PRESSO_COR"), "")) & "'," _
                        & "'" & par.PulisciStrSql(indirizzoRes) & "','" & idLUOGOres & "','" & telefono & "','" & idTIPOres & "'," _
                        & "'" & civicoRes & "','" & capRes & "'," & tipoRich.Value & "," _
                        & "'" & dataPr & "','0','1','0','" & anno & "',0,'" & anno & "',0,-1,-1,-1,-1,-1,-1,-1,-1," _
                        & "-1,-1,-1,-1,-1,-1,-1,-1,'1','1','1','1','1','1','1','1','1',0,0,0,0,0,0,'0','0','0','0',NULL,'0','" & CodContratto.Value & "','" _
                        & par.IfNull(myReader("DATA_STIPULA"), "") & "','" & myReaderUI("COD_UNITA_IMMOBILIARE") & "','0',-1,'" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "'," _
                        & "'1','1','1','0','1','1','1','1','1','1','1','0','1','1','1','0','0','0','" & causale.Value & "','" & dataEvento & "'," & Tipo_Domanda & "," & id_contr.Value & ",'" & codContrSc & "','" & pgDomCollegato & "'," & par.IfEmpty(intestNewRU.Value, "NULL") & ")"
                    End If
                    par.cmd.ExecuteNonQuery()

                    'Campi non inseriti: REDDITO_ISEE,ISR_ERP,ISP_ERP,ISE_ERP,MINORI_CARICO,PSE,VSE,CARTA_I,CARTA_I_DATA,PERMESSO_SOGG_N,
                    'PERMESSO_SOGG_DATA, PERMESSO_SOGG_SCADE, PERMESSO_SOGG_RINNOVO, CARTA_SOGG_N, CARTA_SOGG_DATA, CARTA_I_RILASCIATA

                    par.cmd.CommandText = "INSERT INTO COMP_NAS_RES_VSA (ID_COMPONENTE,ID_LUOGO_NAS_DNTE,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE," _
                    & "CIVICO_RES_DNTE,TELEFONO_DNTE,CAP_RES) VALUES (" & id_intest & ",'" & idLUOGOnasc & "'," & idTIPOres & "," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReader("INDIRIZZO_RESIDENZA"), "")) & "','" & par.IfNull(myReader("CIVICO_RESIDENZA"), "") & "'," _
                    & "'" & par.IfNull(myReader("TELEFONO"), "") & "','" & par.IfNull(myReader("CAP_RESIDENZA"), "") & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & par.IfNull(myReaderUI("ID"), "") & " AND COD_TIPOLOGIA='SUP_NETTA'"
                    Dim myReaderMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderMQ.Read Then
                        sup_netta = par.IfNull(myReaderMQ("VALORE"), "")
                    End If
                    myReaderMQ.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID =" & par.IfNull(myReaderUI("ID_INDIRIZZO"), "")
                    Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderInd.Read Then

                        'query per verificare la presenza o meno dell'ascensore
                        par.cmd.CommandText = "SELECT IMPIANTI.* FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.SCALE_EDIFICI WHERE IMPIANTI.ID = IMPIANTI_SCALE.ID_IMPIANTO " _
                        & "AND SCALE_EDIFICI.ID = IMPIANTI_SCALE.ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = '" & myReaderUI("ID_EDIFICIO") & "' AND SCALE_EDIFICI.DESCRIZIONE = '" & par.IfNull(myReaderUI("SCALA"), "") & "' AND IMPIANTI.COD_TIPOLOGIA = 'SO'"
                        Dim myReaderAsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderAsc.Read Then
                            ascens = 1
                        Else
                            ascens = 0
                        End If
                        myReaderAsc.Close()

                        par.cmd.CommandText = "INSERT INTO DOMANDE_VSA_ALLOGGIO (ID_DOMANDA,COMUNE,CAP,INDIRIZZO,CIVICO,ID_TIPO_GESTORE," _
                        & "SCALA,PIANO,INTERNO,NUM_CONTRATTO,DEC_CONTRATTO,ASS_TEMPORANEA,ID_TIPO_CONTRATTO,COD_UNITA_IMMOBILIARE,SUP_NETTA,ASCENSORE)" _
                        & "VALUES (" & new_id_dom & ",'" & par.IfNull(myReaderInd("LOCALITA"), "") & "','" & par.IfNull(myReaderInd("CAP"), "") & "'," _
                        & "'" & par.PulisciStrSql(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "','" & par.IfNull(myReaderInd("CIVICO"), "") & "','9','" & par.IfNull(myReaderUI("SCALA"), "") & "'," _
                        & "'" & par.IfNull(myReaderUI("COD_TIPO_LIVELLO_PIANO"), "") & "','" & par.IfNull(myReaderUI("INTERNO"), "") & "','" & par.IfNull(myReader("COD_CONTRATTO"), "") & "'," _
                        & "'" & par.IfNull(myReader("DATA_DECORRENZA"), "") & "','0','0','" & par.IfNull(myReaderUI("COD_UNITA_IMMOBILIARE"), "") & "','" & sup_netta & "','" & ascens & "')"
                        par.cmd.ExecuteNonQuery()

                    End If
                    myReaderInd.Close()

                    par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                        & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & id_contr.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                    Dim myReaderIntest As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderIntest.Read Then
                        par.cmd.CommandText = "INSERT INTO INTESTATARI_CONTRATTI_VSA (ID_DOMANDA,COGNOME,NOME,SESSO,COD_FISCALE,ID_LUOGO_NAS_DNTE,DATA_NASCITA_DNTE) VALUES " _
                        & "(" & new_id_dom & ",'" & par.PulisciStrSql(par.IfNull(myReaderIntest("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderIntest("NOME"), "")) & "'," _
                        & "'" & par.IfNull(myReaderIntest("SESSO"), "") & "','" & par.IfNull(myReaderIntest("COD_FISCALE"), "") & "'," _
                        & "'" & idLUOGOnasc & "','" & par.IfNull(myReaderIntest("DATA_NASCITA"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderIntest.Close()

                End If
                myReaderUI.Close()

            End If
            myReader.Close()

            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & new_id_dom & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
            & "','F190','','I')"
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    'Crea una nuova dichiarazione dando all'utente la possibilità di scegliere se importare i redditi (SI) in quanto risultano già caricati
    'per una domanda fatta in precedenza
    Protected Sub CaricaDichiarazione()

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim i As Integer = 1
        Dim strScript As String

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            valorePG = Format(lValoreCorrente, "0000000000")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans


            If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND PROGR=0"
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    id_intest = myReader0("ID")
                End If
                myReader0.Close()
            Else
                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND (COD_FISCALE ='" & codFisc.Value & "' OR COGNOME||' '||NOME LIKE '" & par.PulisciStrSql(intestatario.Value) & "%')"
                myReader = par.cmd.ExecuteReader()
                If myReader.HasRows Then
                    While myReader.Read
                        If par.IfNull(myReader("COD_FISCALE"), "") = codFisc.Value Then
                            id_intest = myReader("ID") 'ottengo l'id dell'intestatario che intende presentare la domanda
                        End If
                    End While
                    myReader.Close()
                Else
                    'Response.Write("<script>alert('Attenzione...la domanda non può essere intestata automaticamente al componente scelto!\nSi procederà al caricamento dei dati di un componente del nucleo presente nel sistema.')</script>")
                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND PROGR=0"
                    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader0.Read Then
                        id_intest = myReader0("ID")
                    End If
                    myReader0.Close()
                End If
            End If

            par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_VSA.NEXTVAL FROM DUAL"
            Dim myReaderNew As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderNew.Read() Then
                new_id_dom = myReaderNew(0)
            End If
            myReaderNew.Close()

            Dim idLuogoNascita As Integer = 0
            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE = UTENZA_DICHIARAZIONI.ID AND UTENZA_DICHIARAZIONI.ID = " & id_dichia & " AND UTENZA_COMP_NUCLEO.COD_FISCALE='" & codFisc.Value & "' AND UTENZA_COMP_NUCLEO.PROGR =0"
            Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCodF.Read = False Then
                If codFisc.Value <> "" Then
                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD = '" & codFisc.Value.Substring(11, 4) & "'"
                    Dim myReaderCom As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReaderCom.Read Then
                        idLuogoNascita = par.IfNull(myReaderCom("ID"), "")
                    End If
                    myReaderCom.Close()
                End If
            End If
            myReaderCodF.Close()


            '************* 13/03/2012 CERCO L'ULTIMA DICHIARAZIONE DA CUI IMPORTARE GLI INDIRIZZI AGGIORNATI

            piurecente = CercaUltimeDich(idLUOGOres, idTIPOres, indirizzoRes, civicoRes, capRes, telefono)

            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID = " & id_dichia
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If idLuogoNascita = 0 Then
                    idLuogoNascita = par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "")
                End If
                If piurecente = False Then
                    idLUOGOres = par.IfNull(myReader("ID_LUOGO_RES_DNTE"), 0)
                    idTIPOres = par.IfNull(myReader("ID_TIPO_IND_RES_DNTE"), 0)
                    indirizzoRes = par.IfNull(myReader("IND_RES_DNTE"), "")
                    civicoRes = par.IfNull(myReader("CIVICO_RES_DNTE"), "")
                    capRes = par.IfNull(myReader("CAP_RES_DNTE"), "")
                    telefono = par.IfNull(myReader("TELEFONO_DNTE"), "")
                End If
                par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.NEXTVAL FROM DUAL"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    new_idDichia = myReader1(0)
                End If
                myReader1.Close()

                'If LBLrisp.Value = "1" Then
                If Request.QueryString("ANNI") <> "" Then
                    annoReddito = Request.QueryString("ANNI")
                Else
                    annoReddito = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                End If
                'Else
                '    annoReddito = "2010"
                'End If


                par.cmd.CommandText = "INSERT INTO Dichiarazioni_VSA (ID,ID_CAF,ID_BANDO,PG,DATA_PG,LUOGO,DATA,ID_STATO,ID_LUOGO_NAS_DNTE,TELEFONO_DNTE,ID_LUOGO_RES_DNTE," _
                    & " ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,N_COMP_NUCLEO,N_INV_100_CON,N_INV_100_SENZA,N_INV_100_66,ID_TIPO_CAT_AB,ANNO_SIT_ECONOMICA,LUOGO_INT_ERP," _
                    & " DATA_INT_ERP,LUOGO_S,DATA_S,PROGR_DNTE,FL_GIA_TITOLARE,CAP_RES_DNTE,FL_UBICAZIONE,POSSESSO_UI,FL_APPLICA_36,MINORI_CARICO,ISEE,ISE_ERP,ISR_ERP,ISP_ERP,PSE,VSE,MOD_PRESENTAZIONE,DATA_INIZIO_VAL,DATA_FINE_VAL,ID_SINDACATO_VSA,MOD_PRES_ALTRO) " _
                    & " VALUES (" & new_idDichia & "," & Session.Item("ID_CAF") & "," & id_bando & ",'" & valorePG & "','" & Format(Now, "yyyyMMdd") & "'," _
                    & " 'Milano','" & Format(Now, "yyyyMMdd") & "',0," & idLuogoNascita & ",'" & telefono & "','" _
                    & idLUOGOres & "'," & idTIPOres & ",'" & par.PulisciStrSql(indirizzoRes) & "','" _
                    & civicoRes & "'," & par.IfNull(myReader("N_COMP_NUCLEO"), "''") & "," & par.IfNull(myReader("N_INV_100_CON"), "''") & "," _
                    & par.IfNull(myReader("N_INV_100_SENZA"), "''") & "," & par.IfNull(myReader("N_INV_100_66"), "''") & "," & par.IfNull(myReader("ID_TIPO_CAT_AB"), "''") & "," _
                    & annoReddito & ",'" & par.IfNull(myReader("LUOGO_INT_ERP"), "") & "','" & par.IfNull(myReader("DATA_INT_ERP"), "") & "','" _
                    & par.IfNull(myReader("LUOGO_S"), "") & "','" & par.IfNull(myReader("DATA_S"), "") & "'," & par.IfNull(myReader("PROGR_DNTE"), "''") & ",'" _
                    & par.IfNull(myReader("FL_GIA_TITOLARE"), "") & "','" & capRes & "','" & par.IfNull(myReader("FL_UBICAZIONE"), "") & "'," _
                    & par.IfNull(myReader("POSSESSO_UI"), "''") & ",'" & par.IfNull(myReader("FL_APPLICA_36"), "") & "','" & par.IfNull(myReader("MINORI_CARICO"), "") & "','" _
                    & par.IfNull(myReader("ISEE"), "") & "','" & par.IfNull(myReader("ISE_ERP"), "") & "','" & par.IfNull(myReader("ISR_ERP"), "") & "','" _
                    & par.IfNull(myReader("ISP_ERP"), "") & "','" & par.IfNull(myReader("PSE"), "") & "','" & par.IfNull(myReader("VSE"), "") & "','" & ModRichiesta.Value & "','" & dataInizio & "','" & dataFine & "','" & idSind & "','" & altro & "')"
                par.cmd.ExecuteNonQuery()

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia
            myReader = par.cmd.ExecuteReader()
            While myReader.Read '-------------- CICLO SUI COMPONENTI DEL NUCLEO FAMILIARE --------------'
                id_compon = myReader("ID")

                par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    new_id_compon = myReader1(0)
                End If
                myReader1.Close()

                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC,TIPO_INVAL,NATURA_INVAL) " _
                    & "VALUES (" & new_id_compon & "," & new_idDichia & "," & par.IfNull(myReader("PROGR"), "") & ",'" & par.IfNull(myReader("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader("COGNOME"), "")) & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReader("NOME"), "")) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "'," _
                    & par.IfNull(myReader("PERC_INVAL"), "") & ",'" & par.IfNull(myReader("INDENNITA_ACC"), "") & "','" & par.IfNull(myReader("TIPO_INVAL"), "") & "','" & par.IfNull(myReader("NATURA_INVAL"), "") & "')"
                par.cmd.ExecuteNonQuery()

                If id_compon = id_intest Then
                    id_intest = new_id_compon
                End If

                Dim distanzaKm As Decimal = 0
                Dim idRedditoTot As Long = 0

                If LBLrisp.Value = "1" Then 'se clicca SI

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read

                        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "'"
                        Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCC.Read() Then
                            distanzaKm = par.IfNull(myReaderCC("DISTANZA_KM"), 0)
                        End If
                        myReaderCC.Close()

                        par.cmd.CommandText = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,REND_CATAST_DOMINICALE,ID_TIPO_PROPRIETA,DISTANZA_KM) " _
                            & "VALUES (SEQ_COMP_PATR_IMMOB_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                            & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "'," _
                            & "'" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "','" & par.IfNull(myReader2("N_VANI"), "") & "'," _
                            & "'" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), 0) & "," _
                            & par.IfNull(myReader2("REND_CATAST_DOMINICALE"), 0) & "," & par.IfNull(myReader2("ID_TIPO_PROPRIETA"), "NULL") & "," & distanzaKm & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        par.cmd.CommandText = "INSERT INTO COMP_PATR_MOB_VSA (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO,IBAN,ID_TIPO,PERC_PROPRIETA) " _
                            & "VALUES (SEQ_COMP_PATR_MOB_VSA.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ",'" & par.IfNull(myReader3("IBAN"), "") & "'," & par.IfNull(myReader3("ID_TIPO"), "NULL") & "," & par.IfNull(myReader3("PERC_PROPRIETA"), 1) & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader3.Close()

                    Dim idReddComp As Long = 0
                    Dim reddAltro As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader4.Read
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                            & "VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & new_id_compon & "," & par.VirgoleInPunti(par.IfNull(myReader4("REDDITO_IRPEF"), 0)) & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_COMP_REDDITO_VSA.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idReddComp = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader5.Read
                            reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReader5.Close()
                    End While
                    myReader4.Close()

                    'par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                    'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'While myReader5.Read
                    '    par.cmd.CommandText = "INSERT INTO COMP_ALTRI_REDDITI_VSA (ID,ID_COMPONENTE,IMPORTO) " _
                    '    & "VALUES (SEQ_COMP_ALTRI_REDDITI_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader5("IMPORTO"), "") & ")"
                    '    par.cmd.ExecuteNonQuery()
                    'End While
                    'myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader6.Read
                        par.cmd.CommandText = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                        & "VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader6.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader7.Read
                        par.cmd.CommandText = "INSERT INTO COMP_DETRAZIONI_VSA (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                        & "VALUES (SEQ_COMP_DETRAZIONI_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader7.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_REDDITI WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader8.Read
                        par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID,ID_DOMANDA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI,PENS_ESENTE,NO_ISEE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & new_idDichia & "," & new_id_compon & ",'" _
                        & par.IfNull(myReader8("CONDIZIONE"), "0") & "','" & par.IfNull(myReader8("PROFESSIONE"), "0") & "','" _
                        & par.IfNull(myReader8("DIPENDENTE"), "0") & "','" & par.IfNull(myReader8("PENSIONE"), "0") & "','" _
                        & par.IfNull(myReader8("AUTONOMO"), "0") & "','" & par.IfNull(myReader8("NON_IMPONIBILI"), "0") & "','" _
                        & par.IfNull(myReader8("DOM_AG_FAB"), "0") & "','" & par.IfNull(myReader8("OCCASIONALI"), "0") & "','" & par.IfNull(myReader8("ONERI"), "0") & "','" & par.IfNull(myReader8("PENS_ESENTE"), "0") & "','" & par.IfNull(myReader8("NO_ISEE"), "0") & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_UTENZA_REDDITI.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idRedditoTot = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_AUTONOMO_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddA.Read
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_AUTONOMO_IMPORTI (SELECT seq_VSA_REDD_AUTON_IMPORTI.nextval, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_AUTONOMO_IMPORTI WHERE ID=" & par.IfNull(myReaderReddA("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddA.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddD.Read
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_DIPEND_IMPORTI (SELECT seq_VSA_REDD_DIPEND_IMPORTI.nextval, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_DIPEND_IMPORTI WHERE ID=" & par.IfNull(myReaderReddD("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddD.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddN.Read
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_NO_ISEE_IMPORTI (SELECT seq_VSA_REDD_NO_ISEE_IMP.nextval, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_NO_ISEE_IMPORTI WHERE ID=" & par.IfNull(myReaderReddN("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddN.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP.Read
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_ES_IMPORTI (SELECT seq_VSA_REDD_PENS_ES_IMP.nextval, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_PENS_ES_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP2.Read
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_IMPORTI (SELECT seq_VSA_REDD_PENS_IMPORTI.nextval, ID_REDD_PENSIONE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM UTENZA_REDD_PENS_IMPORTI WHERE ID=" & par.IfNull(myReaderReddP2("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP2.Close()

                    End While
                    myReader8.Close()

                End If

            End While
            myReader.Close()

            par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = 0 WHERE ID =" & id_intest
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID <> " & id_intest & " AND ID_DICHIARAZIONE = " & new_idDichia & " ORDER BY PROGR ASC FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = " & i & " WHERE ID =" & myReader("ID")
                par.cmd.ExecuteNonQuery()
                i = i + 1
            End While
            myReader.Close()

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaDomanda()

            If tipoRich.Value <> "3" And tipoRich.Value <> "2" Then
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                 & "else{window.close();}</script>"
                Response.Write(strScript)
            End If
            If tipoRich.Value = "2" And causale.Value = "30" Then
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                & "else{window.close();}</script>"
                Response.Write(strScript)
            End If


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub CaricaDomanda()

        'Dim idDomanda As Long
        Dim iDluogo As Long
        Dim codIndir As Integer
        Dim sup_netta As Decimal
        Dim ascens As Integer
        Dim lValoreCorrenteDom As Long
        Dim valorePGdom As String
        Dim tipoBando As Long
        Dim cod_UI As String = ""

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrenteDom = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrenteDom & ")"
            par.cmd.ExecuteNonQuery()
            valorePGdom = Format(lValoreCorrenteDom, "0000000000")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans



            par.cmd.CommandText = "SELECT * FROM BANDI_VSA WHERE ID=" & id_bando
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                tipoBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DISTINCT(UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE) FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_CONTRATTUALE.id_unita_principale is null and  UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND ID_CONTRATTO=(SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & CodContratto.Value & "')"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                cod_UI = par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "0")
            End If
            myReader.Close()

            '**************** RICAVO INFO DOCUMENTO DI RICONOSCIMENTO QUANDO IL RICHIEDENTE E' DIVERSO DA QUELLO VECCHIO ************
            Dim infoDocumento As Boolean = False
            Dim numDoc As String = ""
            Dim dataDoc As String = ""
            Dim rilascioDoc As String = ""
            Dim docSoggiorno As String = ""

            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE = UTENZA_DICHIARAZIONI.ID AND UTENZA_DICHIARAZIONI.ID = " & id_dichia & " AND UTENZA_COMP_NUCLEO.COD_FISCALE='" & codFisc.Value & "' AND UTENZA_COMP_NUCLEO.PROGR =0"
            Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCodF.Read = False Then
                par.cmd.CommandText = "SELECT SISCOM_MI.ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.COD_CONTRATTO ='" & CodContratto.Value & "' AND (ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
                Dim myReaderCI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCI.Read Then
                    infoDocumento = True
                    numDoc = par.IfNull(myReaderCI("NUM_DOC"), "")
                    dataDoc = par.IfNull(myReaderCI("DATA_DOC"), "")
                    rilascioDoc = par.IfNull(myReaderCI("RILASCIO_DOC"), "")
                    docSoggiorno = par.IfNull(myReaderCI("DOC_SOGGIORNO"), "")
                End If
                myReaderCI.Close()
            End If
            myReaderCodF.Close()


            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID = " & id_dichia
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                If infoDocumento = False Then
                    numDoc = par.IfNull(myReader("CARTA_I"), "")
                    dataDoc = par.IfNull(myReader("CARTA_I_DATA"), "")
                    rilascioDoc = par.IfNull(myReader("CARTA_I_RILASCIATA"), "")
                    docSoggiorno = par.IfNull(myReader("PERMESSO_SOGG_N"), "")
                End If

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO ='" & CodContratto.Value & "'"
                Dim myReaderContr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderContr.Read Then

                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & myReader("LUOGO").ToString.ToUpper & "'"
                    Dim myReaderIDluogo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderIDluogo.Read Then
                        iDluogo = myReaderIDluogo("ID")
                    End If
                    myReaderIDluogo.Close()

                    par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE DESCRIZIONE='" & par.PulisciStrSql(myReaderContr("TIPO_COR")) & "'"
                    Dim myReaderTipoIndir As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderTipoIndir.Read Then
                        codIndir = myReaderTipoIndir("COD")
                    End If
                    myReaderTipoIndir.Close()

                    'par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_VSA.NEXTVAL FROM DUAL"
                    'Dim myReaderNew As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderNew.Read() Then
                    '    idDomanda = myReaderNew(0)
                    'End If
                    'myReaderNew.Close()

                    If dataPr = "" Then
                        dataPr = Format(Now, "yyyyMMdd")
                    End If

                    '------ inserimento nuova domanda ------' 
                    par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA (ID,ID_BANDO,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA," _
                        & "ID_DICHIARAZIONE,PROGR_COMPONENTE,PG,DATA_PG,PRESSO_REC_DNTE,IND_REC_DNTE,ID_LUOGO_REC_DNTE,TELEFONO_REC_DNTE," _
                        & "ID_TIPO_IND_REC_DNTE,CIVICO_REC_DNTE,REDDITO_ISEE,ISR_ERP,ISP_ERP,ISE_ERP,CAP_REC_DNTE,MINORI_CARICO,PSE,VSE,ID_MOTIVO_DOMANDA," _
                        & "DATA_PRESENTAZIONE,TIPO_ALLOGGIO,FL_MOROSITA,FL_PROFUGO,ANNO_RIF_CANONE,IMPORTO_CANONE,ANNO_RIF_SPESE_ACC,IMPORTO_SPESE_ACC," _
                        & "ID_PARA_0,ID_PARA_1,ID_PARA_2,ID_PARA_3,ID_PARA_4,ID_PARA_5,ID_PARA_6,ID_PARA_7,ID_PARA_8,ID_PARA_9,ID_PARA_10,ID_PARA_11,ID_PARA_12," _
                        & "ID_PARA_13,ID_PARA_14,ID_PARA_15,REQUISITO1,REQUISITO2,REQUISITO3,REQUISITO4,REQUISITO5,REQUISITO6,REQUISITO7,REQUISITO8,REQUISITO9," _
                        & "ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E,FL_ISTRUTTORIA_COMPLETA,FL_COMPLETA,FL_ESAMINATA,FL_PROPOSTA,FL_CONTROLLA_REQUISITI," _
                        & "FL_INVITO,CONTRATTO_NUM,CONTRATTO_DATA,NUM_ALLOGGIO,FL_RINNOVO,PERIODO_RES,CONTRATTO_DATA_DEC," _
                        & "FL_ASS_ESTERNA,CARTA_I,CARTA_I_DATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,PERMESSO_SOGG_SCADE,PERMESSO_SOGG_RINNOVO,FL_FATTA_AU,FL_FATTA_ERP," _
                        & "FL_FAI_ERP,CARTA_SOGG_N,CARTA_SOGG_DATA,CARTA_I_RILASCIATA,REQUISITO10,REQUISITO11,REQUISITO12,REQUISITO13,REQUISITO14,REQUISITO15,REQUISITO16," _
                        & "FL_VERIFICA_CONCLUSA,REQUISITO17,REQUISITO18,REQUISITO19,FL_NATO_ESTERO,ACCOLTA,TIPO_U,ID_CAUSALE_DOMANDA,DATA_EVENTO,TIPO_D_IMPORT,ID_D_IMPORT,TIPO_DOCUMENTO,COD_CONTRATTO_SCAMBIO,ID_INTEST_NEW_RU) " _
                            & "VALUES (" & new_id_dom & "," & id_bando & ",'0','1','0','0','" & tipoBando & "'," & new_idDichia & "," _
                            & myReader("PROGR_DNTE") & ",'" & valorePGdom & "','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfNull(myReaderContr("PRESSO_COR"), "")) & "'," _
                            & "'" & par.PulisciStrSql(indirizzoRes) & "','" & idLUOGOres & "','" & telefono & "','" & idTIPOres & "'," _
                            & "'" & civicoRes & "'," & CDec(par.VirgoleInPunti(par.IfNull(myReader("ISEE"), "0"))) & "," & par.IfNull(par.VirgoleInPunti(myReader("ISR_ERP")), "''") & "," _
                            & "" & par.IfNull(par.VirgoleInPunti(myReader("ISP_ERP")), "''") & ", " & par.IfNull(par.VirgoleInPunti(myReader("ISE_ERP")), "''") & ",'" & capRes & "','" _
                            & " " & par.IfNull(myReader("MINORI_CARICO"), "") & "','" & par.IfNull(myReader("PSE"), "") & "','" & par.IfNull(myReader("VSE"), "") & "'," & tipoRich.Value & "," _
                            & "'" & dataPr & "','0','1','0','" & par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "") & "',0,'" & par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "") & "'," _
                            & "0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'1','1','1','1','1','1','1','1','1',0,0,0,0,0,0,'0','0','0','0',NULL,'0','" & CodContratto.Value & "','" _
                            & par.IfNull(myReaderContr("DATA_STIPULA"), "") & "','" & cod_UI & "','0',-1,'" & par.IfNull(myReaderContr("DATA_DECORRENZA"), "") & "'," _
                            & "'1','" & numDoc & "','" & dataDoc & "','" & docSoggiorno & "','" _
                            & par.IfNull(myReader("PERMESSO_SOGG_DATA"), "") & "','" & par.IfNull(myReader("PERMESSO_SOGG_SCADE"), "") & "','" & par.IfNull(myReader("PERMESSO_SOGG_RINNOVO"), "") & "'," _
                            & "'1','1','0','" & par.IfNull(myReader("CARTA_SOGG_N"), "") & "'," & "'" & par.IfNull(myReader("CARTA_SOGG_DATA"), "") & "','" & par.PulisciStrSql(rilascioDoc) & "'," _
                            & "'1','1','1','1','1','1','1','0','1','1','1','0','0','0','" & causale.Value & "','" & dataEvento & "'," & Tipo_Domanda & "," & id_dichia & "," & par.IfNull(myReader("TIPO_DOCUMENTO"), 0) & ",'" & codContrSc & "'," & par.IfEmpty(intestNewRU.Value, "NULL") & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO COMP_NAS_RES_VSA (ID_COMPONENTE,ID_LUOGO_NAS_DNTE,ID_LUOGO_RES_DNTE,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE," _
                        & "CIVICO_RES_DNTE,TELEFONO_DNTE,CAP_RES) VALUES (" & id_intest & ",'" & par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "") & "'," _
                        & "'" & par.IfNull(myReader("ID_LUOGO_RES_DNTE"), "") & "','" & par.IfNull(myReader("ID_TIPO_IND_RES_DNTE"), "") & "','" _
                        & par.PulisciStrSql(par.IfNull(myReader("IND_RES_DNTE"), "")) & "','" & par.IfNull(myReader("CIVICO_RES_DNTE"), "") & "'," _
                        & "'" & par.IfNull(myReader("TELEFONO_DNTE"), "") & "','" & par.IfNull(myReader("CAP_RES_DNTE"), "") & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SCALE_EDIFICI WHERE COD_UNITA_IMMOBILIARE='" & cod_UI & "' AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA"
                    Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderUI.Read() Then

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & par.IfNull(myReaderUI("ID"), "") & " AND COD_TIPOLOGIA='SUP_NETTA'"
                        Dim myReaderMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderMQ.Read Then
                            sup_netta = par.IfNull(myReaderMQ("VALORE"), "")
                        End If
                        myReaderMQ.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID =" & par.IfNull(myReaderUI("ID_INDIRIZZO"), "")
                        Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderInd.Read Then

                            'query per verificare la presenza o meno dell'ascensore
                            par.cmd.CommandText = "SELECT IMPIANTI.* FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.SCALE_EDIFICI WHERE IMPIANTI.ID = IMPIANTI_SCALE.ID_IMPIANTO " _
                                & "AND SCALE_EDIFICI.ID = IMPIANTI_SCALE.ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = '" & myReaderUI("ID_EDIFICIO") & "' AND SCALE_EDIFICI.DESCRIZIONE = '" & myReaderUI("DESCRIZIONE") & "' AND IMPIANTI.COD_TIPOLOGIA = 'SO'"
                            Dim myReaderAsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderAsc.Read Then
                                ascens = 1
                            Else
                                ascens = 0
                            End If
                            myReaderAsc.Close()

                            par.cmd.CommandText = "INSERT INTO DOMANDE_VSA_ALLOGGIO (ID_DOMANDA,COMUNE,CAP,INDIRIZZO,CIVICO,ID_TIPO_GESTORE," _
                                & "SCALA,PIANO,INTERNO,NUM_CONTRATTO,DEC_CONTRATTO,ASS_TEMPORANEA,ID_TIPO_CONTRATTO," _
                                & "COD_UNITA_IMMOBILIARE,SUP_NETTA,ASCENSORE) VALUES (" & new_id_dom & ",'" & par.IfNull(myReaderInd("LOCALITA"), "") & "','" & par.IfNull(myReaderInd("CAP"), "") & "'," _
                                & "'" & par.PulisciStrSql(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "','" & par.IfNull(myReaderInd("CIVICO"), "") & "','9','" & par.IfNull(myReader("SCALA"), "") & "'," _
                                & "'" & par.IfNull(myReaderUI("COD_TIPO_LIVELLO_PIANO"), "") & "','" & par.IfNull(myReaderUI("INTERNO"), "") & "','" & par.IfNull(myReader("RAPPORTO"), "") & "'," _
                                & "'" & par.IfNull(myReaderContr("DATA_DECORRENZA"), "") & "','0','0','" & cod_UI & "','" & sup_netta & "','" & ascens & "')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                                & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & myReaderContr("ID") & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                            Dim myReaderIntest As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderIntest.Read Then

                                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD ='" & myReaderIntest("COD_COMUNE_NASCITA") & "'"
                                Dim myReaderLuogoNasc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderLuogoNasc.Read Then
                                    idLUOGOnasc = myReaderLuogoNasc("ID")
                                End If
                                myReaderLuogoNasc.Close()

                                par.cmd.CommandText = "INSERT INTO INTESTATARI_CONTRATTI_VSA (ID_DOMANDA,COGNOME,NOME,SESSO,COD_FISCALE,ID_LUOGO_NAS_DNTE,DATA_NASCITA_DNTE) VALUES " _
                                    & "(" & new_id_dom & ",'" & par.PulisciStrSql(par.IfNull(myReaderIntest("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderIntest("NOME"), "")) & "'," _
                                    & "'" & par.IfNull(myReaderIntest("SESSO"), "") & "','" & par.IfNull(myReaderIntest("COD_FISCALE"), "") & "','" & idLUOGOnasc & "','" & par.IfNull(myReaderIntest("DATA_NASCITA"), "") & "')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReaderIntest.Close()

                        End If
                        myReaderInd.Close()

                    End If
                    myReaderUI.Close()

                End If
                myReaderContr.Close()

            End If
            myReader.Close()

            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & new_id_dom & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
            & "','F190','','I')"
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub CaricaDomVSA()

        'Dim idDomanda As Long
        Dim sup_netta As Decimal
        Dim ascens As Integer
        Dim lValoreCorrenteDom As Long
        Dim valorePGdom As String
        Dim tipoBando As Long
        Dim cod_UI As String = ""

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrenteDom = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrenteDom & ")"
            par.cmd.ExecuteNonQuery()
            valorePGdom = Format(lValoreCorrenteDom, "0000000000")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * FROM BANDI_VSA WHERE ID=" & id_bando
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                tipoBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DISTINCT(UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE) FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_CONTRATTUALE.id_unita_principale is null and UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND ID_CONTRATTO=(SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & CodContratto.Value & "')"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                cod_UI = par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "0")
            End If
            myReader.Close()

            If dataPr = "" Then
                dataPr = Format(Now, "yyyyMMdd")
            End If

            '**************** RICAVO INFO DOCUMENTO DI RICONOSCIMENTO QUANDO IL RICHIEDENTE E' DIVERSO DA QUELLO VECCHIO ************
            Dim infoDocumento As Boolean = False
            Dim numDoc As String = ""
            Dim dataDoc As String = ""
            Dim rilascioDoc As String = ""
            Dim docSoggiorno As String = ""

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND DICHIARAZIONI_VSA.ID = " & id_dichia & " AND COMP_NUCLEO_VSA.COD_FISCALE='" & codFisc.Value & "' AND COMP_NUCLEO_VSA.PROGR =0"
            Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCodF.Read = False Then
                par.cmd.CommandText = "SELECT SISCOM_MI.ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.COD_CONTRATTO ='" & CodContratto.Value & "' AND (ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
                Dim myReaderCI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCI.Read Then
                    infoDocumento = True
                    numDoc = par.IfNull(myReaderCI("NUM_DOC"), "")
                    dataDoc = par.IfNull(myReaderCI("DATA_DOC"), "")
                    rilascioDoc = par.IfNull(myReaderCI("RILASCIO_DOC"), "")
                    docSoggiorno = par.IfNull(myReaderCI("DOC_SOGGIORNO"), "")
                End If
                myReaderCI.Close()
            End If
            myReaderCodF.Close()


            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & id_dichia
            Dim myReaderBandoVSA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderBandoVSA.Read Then

                If infoDocumento = False Then
                    numDoc = par.IfNull(myReaderBandoVSA("CARTA_I"), "")
                    dataDoc = par.IfNull(myReaderBandoVSA("CARTA_I_DATA"), "")
                    rilascioDoc = par.IfNull(myReaderBandoVSA("CARTA_I_RILASCIATA"), "")
                    docSoggiorno = par.IfNull(myReaderBandoVSA("PERMESSO_SOGG_N"), "")
                End If

                '------ inserimento nuova domanda ------' 
                par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA (ID,ID_BANDO,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA," _
                    & "ID_DICHIARAZIONE,PROGR_COMPONENTE,PG,DATA_PG,PRESSO_REC_DNTE,IND_REC_DNTE,ID_LUOGO_REC_DNTE,TELEFONO_REC_DNTE," _
                    & "ID_TIPO_IND_REC_DNTE,CIVICO_REC_DNTE,REDDITO_ISEE,ISR_ERP,ISP_ERP,ISE_ERP,CAP_REC_DNTE,MINORI_CARICO,PSE,VSE,ID_MOTIVO_DOMANDA," _
                    & "DATA_PRESENTAZIONE,TIPO_ALLOGGIO,FL_MOROSITA,FL_PROFUGO,ANNO_RIF_CANONE,IMPORTO_CANONE,ANNO_RIF_SPESE_ACC,IMPORTO_SPESE_ACC," _
                    & "ID_PARA_0,ID_PARA_1,ID_PARA_2,ID_PARA_3,ID_PARA_4,ID_PARA_5,ID_PARA_6,ID_PARA_7,ID_PARA_8,ID_PARA_9,ID_PARA_10,ID_PARA_11,ID_PARA_12," _
                    & "ID_PARA_13,ID_PARA_14,ID_PARA_15,REQUISITO1,REQUISITO2,REQUISITO3,REQUISITO4,REQUISITO5,REQUISITO6,REQUISITO7,REQUISITO8,REQUISITO9," _
                    & "ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E,FL_ISTRUTTORIA_COMPLETA,FL_COMPLETA,FL_ESAMINATA,FL_PROPOSTA,FL_CONTROLLA_REQUISITI," _
                    & "FL_INVITO,CONTRATTO_NUM,CONTRATTO_DATA,NUM_ALLOGGIO,FL_RINNOVO,PERIODO_RES,CONTRATTO_DATA_DEC," _
                    & "FL_ASS_ESTERNA,CARTA_I,CARTA_I_DATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,PERMESSO_SOGG_SCADE,PERMESSO_SOGG_RINNOVO,FL_FATTA_AU,FL_FATTA_ERP," _
                    & "FL_FAI_ERP,CARTA_SOGG_N,CARTA_SOGG_DATA,CARTA_I_RILASCIATA,REQUISITO10,REQUISITO11,REQUISITO12,REQUISITO13,REQUISITO14,REQUISITO15,REQUISITO16," _
                    & "FL_VERIFICA_CONCLUSA,REQUISITO17,REQUISITO18,REQUISITO19,FL_NATO_ESTERO,ACCOLTA,TIPO_U,ID_CAUSALE_DOMANDA,DATA_EVENTO,TIPO_D_IMPORT,ID_D_IMPORT,TIPO_DOCUMENTO,COD_CONTRATTO_SCAMBIO,ID_INTEST_NEW_RU) " _
                        & "VALUES (" & new_id_dom & "," & id_bando & ",'0','1','0','0','" & tipoBando & "'," & new_idDichia & "," _
                        & myReaderBandoVSA("PROGR_COMPONENTE") & ",'" & valorePGdom & "','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfNull(myReaderBandoVSA("PRESSO_REC_DNTE"), "")) & "'," _
                        & "'" & par.PulisciStrSql(indirizzoRes) & "','" & idLUOGOres & "','" & telefono & "','" & idTIPOres & "'," _
                        & "'" & civicoRes & "'," & CDec(par.VirgoleInPunti(par.IfNull(myReaderBandoVSA("REDDITO_ISEE"), "0"))) & "," & par.IfNull(par.VirgoleInPunti(myReaderBandoVSA("ISR_ERP")), "''") & "," _
                        & "" & par.IfNull(par.VirgoleInPunti(myReaderBandoVSA("ISP_ERP")), "''") & ", " & par.IfNull(par.VirgoleInPunti(myReaderBandoVSA("ISE_ERP")), "''") & ",'" & capRes & "','" _
                        & " " & par.IfNull(myReaderBandoVSA("MINORI_CARICO"), "") & "','" & par.IfNull(myReaderBandoVSA("PSE"), "") & "','" & par.IfNull(myReaderBandoVSA("VSE"), "") & "'," & tipoRich.Value & "," _
                        & "'" & dataPr & "','0','1','0','" & par.IfNull(myReaderBandoVSA("ANNO_RIF_CANONE"), "") & "',0,'" & par.IfNull(myReaderBandoVSA("ANNO_RIF_SPESE_ACC"), "") & "'," _
                        & "0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'1','1','1','1','1','1','1','1','1',0,0,0,0,0,0,'0','0','0','0',NULL,'0','" & CodContratto.Value & "','" _
                        & par.IfNull(myReaderBandoVSA("CONTRATTO_DATA"), "") & "','" & cod_UI & "','0',-1,'" & par.IfNull(myReaderBandoVSA("CONTRATTO_DATA_DEC"), "") & "'," _
                        & "'1','" & numDoc & "','" & dataDoc & "','" & docSoggiorno & "','" _
                        & par.IfNull(myReaderBandoVSA("PERMESSO_SOGG_DATA"), "") & "','" & par.IfNull(myReaderBandoVSA("PERMESSO_SOGG_SCADE"), "") & "','" & par.IfNull(myReaderBandoVSA("PERMESSO_SOGG_RINNOVO"), "") & "'," _
                        & "'1','1','0','" & par.IfNull(myReaderBandoVSA("CARTA_SOGG_N"), "") & "','" & par.IfNull(myReaderBandoVSA("CARTA_SOGG_DATA"), "") & "','" & par.PulisciStrSql(rilascioDoc) & "'," _
                        & "'1','1','1','1','1','1','1','0','1','1','1','0','0','0','" & causale.Value & "','" & dataEvento & "'," & Tipo_Domanda & "," & id_dichia & "," & par.IfNull(myReaderBandoVSA("TIPO_DOCUMENTO"), 0) & ",'" & codContrSc & "'," & par.IfEmpty(intestNewRU.Value, "NULL") & ")"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SCALE_EDIFICI WHERE COD_UNITA_IMMOBILIARE='" & cod_UI & "' AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA"
                Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderUI.Read() Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & par.IfNull(myReaderUI("ID"), "") & " AND COD_TIPOLOGIA='SUP_NETTA'"
                    Dim myReaderMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderMQ.Read Then
                        sup_netta = par.IfNull(myReaderMQ("VALORE"), "")
                    End If
                    myReaderMQ.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID =" & par.IfNull(myReaderUI("ID_INDIRIZZO"), "")
                    Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderInd.Read Then

                        'query per verificare la presenza o meno dell'ascensore
                        par.cmd.CommandText = "SELECT IMPIANTI.* FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.SCALE_EDIFICI WHERE IMPIANTI.ID = IMPIANTI_SCALE.ID_IMPIANTO " _
                            & "AND SCALE_EDIFICI.ID = IMPIANTI_SCALE.ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = '" & myReaderUI("ID_EDIFICIO") & "' AND SCALE_EDIFICI.DESCRIZIONE = '" & myReaderUI("DESCRIZIONE") & "' AND IMPIANTI.COD_TIPOLOGIA = 'SO'"
                        Dim myReaderAsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderAsc.Read Then
                            ascens = 1
                        Else
                            ascens = 0
                        End If
                        myReaderAsc.Close()

                        par.cmd.CommandText = "INSERT INTO DOMANDE_VSA_ALLOGGIO (ID_DOMANDA,COMUNE,CAP,INDIRIZZO,CIVICO,ID_TIPO_GESTORE," _
                            & "SCALA,PIANO,INTERNO,NUM_CONTRATTO,DEC_CONTRATTO,ASS_TEMPORANEA,ID_TIPO_CONTRATTO," _
                            & "COD_UNITA_IMMOBILIARE,SUP_NETTA,ASCENSORE) VALUES (" & new_id_dom & ",'" & par.IfNull(myReaderInd("LOCALITA"), "") & "','" & par.IfNull(myReaderInd("CAP"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "','" & par.IfNull(myReaderInd("CIVICO"), "") & "','9','" & par.IfNull(myReaderUI("descrizione"), "") & "'," _
                            & "'" & par.IfNull(myReaderUI("COD_TIPO_LIVELLO_PIANO"), "") & "','" & par.IfNull(myReaderUI("INTERNO"), "") & "','" & CodContratto.Value & "'," _
                            & "'" & par.IfNull(myReaderBandoVSA("CONTRATTO_DATA"), "") & "','0','0','" & cod_UI & "','" & sup_netta & "','" & ascens & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                            & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & id_contr.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                        Dim myReaderIntest As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderIntest.Read Then

                            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD ='" & myReaderIntest("COD_COMUNE_NASCITA") & "'"
                            Dim myReaderLuogoNasc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderLuogoNasc.Read Then
                                idLUOGOnasc = myReaderLuogoNasc("ID")
                            End If
                            myReaderLuogoNasc.Close()

                            par.cmd.CommandText = "INSERT INTO INTESTATARI_CONTRATTI_VSA (ID_DOMANDA,COGNOME,NOME,SESSO,COD_FISCALE,ID_LUOGO_NAS_DNTE,DATA_NASCITA_DNTE) VALUES " _
                                & "(" & new_id_dom & ",'" & par.PulisciStrSql(par.IfNull(myReaderIntest("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderIntest("NOME"), "")) & "'," _
                                & "'" & par.IfNull(myReaderIntest("SESSO"), "") & "','" & par.IfNull(myReaderIntest("COD_FISCALE"), "") & "','" & idLUOGOnasc & "','" & par.IfNull(myReaderIntest("DATA_NASCITA"), "") & "')"
                            par.cmd.ExecuteNonQuery()

                        End If
                        myReaderIntest.Close()

                    End If
                    myReaderInd.Close()

                End If
                myReaderUI.Close()

            End If
            myReaderBandoVSA.Close()


            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & new_id_dom & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
            & "','F190','','I')"
            par.cmd.ExecuteNonQuery()


            '##### Tabella COMP_NAS_RES  #####
            If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia & " AND PROGR=0"
            Else
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia & " AND (COD_FISCALE ='" & codFisc.Value & "' OR COGNOME||' '||NOME LIKE '" & par.PulisciStrSql(intestatario.Value) & "%')"
            End If
            Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderI.Read Then

                par.cmd.CommandText = "SELECT * FROM COMP_NAS_RES_VSA WHERE ID_COMPONENTE=" & myReaderI("ID")
                Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderComp.Read Then

                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE PROGR= 0 AND ID_DICHIARAZIONE = " & new_idDichia
                    Dim myReaderIDComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderIDComp.Read Then
                        par.cmd.CommandText = "INSERT INTO COMP_NAS_RES_VSA (ID_COMPONENTE,ID_LUOGO_NAS_DNTE,ID_LUOGO_RES_DNTE,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE," _
                            & "CIVICO_RES_DNTE,TELEFONO_DNTE,CAP_RES) VALUES (" & myReaderIDComp("ID") & ",'" & par.IfNull(myReaderComp("ID_LUOGO_NAS_DNTE"), "") & "'," _
                            & "'" & par.IfNull(myReaderComp("ID_LUOGO_RES_DNTE"), "") & "','" & par.IfNull(myReaderComp("ID_TIPO_IND_RES_DNTE"), "") & "','" _
                            & par.PulisciStrSql(par.IfNull(myReaderComp("IND_RES_DNTE"), "")) & "','" & par.IfNull(myReaderComp("CIVICO_RES_DNTE"), "") & "'," _
                            & "'" & par.IfNull(myReaderComp("TELEFONO_DNTE"), "") & "','" & par.IfNull(myReaderComp("CAP_RES"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderIDComp.Close()

                End If
                myReaderComp.Close()

            End If
            myReaderI.Close()
            '##### fine COMP_NAS_RES #####


            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try


    End Sub

    Protected Sub CaricaReddVSA()

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim i As Integer = 1
        Dim strScript As String

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            valorePG = Format(lValoreCorrente, "0000000000")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans


            If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia & " AND PROGR=0"
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    id_intest = myReader0("ID")
                End If
                myReader0.Close()
            Else
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia & " AND (COD_FISCALE ='" & codFisc.Value & "' OR COGNOME||' '||NOME LIKE '" & par.PulisciStrSql(intestatario.Value) & "%')"
                myReader = par.cmd.ExecuteReader()
                If myReader.HasRows Then
                    While myReader.Read
                        If par.IfNull(myReader("COD_FISCALE"), "") = codFisc.Value Then
                            id_intest = myReader("ID") 'ottengo l'id dell'intestatario che intende presentare la domanda
                        End If
                    End While
                    myReader.Close()
                Else
                    Response.Write("<script>alert('Attenzione...la domanda non può essere intestata automaticamente al componente scelto!\nSi procederà al caricamento dei dati di un componente del nucleo presente nel sistema.')</script>")
                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia & " AND PROGR=0"
                    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader0.Read Then
                        id_intest = myReader0("ID")
                    End If
                    myReader0.Close()
                End If
            End If

            par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_VSA.NEXTVAL FROM DUAL"
            Dim myReaderNew As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderNew.Read() Then
                new_id_dom = myReaderNew(0)
            End If
            myReaderNew.Close()

            par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.NEXTVAL FROM DUAL"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                new_idDichia = myReader1(0)
            End If
            myReader1.Close()

            Dim idLuogoNascita As Integer = 0
            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND DICHIARAZIONI_VSA.ID = " & id_dichia & " AND COMP_NUCLEO_VSA.COD_FISCALE='" & codFisc.Value & "' AND COMP_NUCLEO_VSA.PROGR =0"
            Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCodF.Read = False Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD = '" & codFisc.Value.Substring(11, 4) & "'"
                Dim myReaderCom As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderCom.Read Then
                    idLuogoNascita = par.IfNull(myReaderCom("ID"), "")
                End If
                myReaderCom.Close()
            End If
            myReaderCodF.Close()


            '************* 13/03/2012 CERCO L'ULTIMA DICHIARAZIONE DA CUI IMPORTARE GLI INDIRIZZI AGGIORNATI

            piurecente = CercaUltimeDich(idLUOGOres, idTIPOres, indirizzoRes, civicoRes, capRes, telefono)


            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID = " & id_dichia
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If idLuogoNascita = 0 Then
                    idLuogoNascita = par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "")
                End If
                If piurecente = False Then
                    idLUOGOres = par.IfNull(myReader("ID_LUOGO_RES_DNTE"), 0)
                    idTIPOres = par.IfNull(myReader("ID_TIPO_IND_RES_DNTE"), 0)
                    indirizzoRes = par.IfNull(myReader("IND_RES_DNTE"), "")
                    civicoRes = par.IfNull(myReader("CIVICO_RES_DNTE"), "")
                    capRes = par.IfNull(myReader("CAP_RES_DNTE"), "")
                    telefono = par.IfNull(myReader("TELEFONO_DNTE"), "")
                End If
                'If LBLrisp.Value = "1" Then
                If Request.QueryString("ANNI") <> "" Then
                    annoReddito = Request.QueryString("ANNI")
                Else
                    annoReddito = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                End If
                'Else
                '    annoReddito = "2010"
                'End If

                par.cmd.CommandText = "INSERT INTO DICHIARAZIONI_VSA (ID,ID_CAF,ID_BANDO,PG,DATA_PG,LUOGO,DATA,ID_STATO,ID_LUOGO_NAS_DNTE,TELEFONO_DNTE,ID_LUOGO_RES_DNTE," _
                    & " ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,N_COMP_NUCLEO,N_INV_100_CON,N_INV_100_SENZA,N_INV_100_66,ID_TIPO_CAT_AB,ANNO_SIT_ECONOMICA,LUOGO_INT_ERP," _
                    & " DATA_INT_ERP,LUOGO_S,DATA_S,PROGR_DNTE,FL_GIA_TITOLARE,CAP_RES_DNTE,FL_UBICAZIONE,POSSESSO_UI,FL_APPLICA_36,MINORI_CARICO,ISEE,ISE_ERP,ISR_ERP,ISP_ERP,PSE,VSE,MOD_PRESENTAZIONE,DATA_INIZIO_VAL,DATA_FINE_VAL,ID_SINDACATO_VSA,MOD_PRES_ALTRO) " _
                    & " VALUES (" & new_idDichia & "," & Session.Item("ID_CAF") & "," & id_bando & ",'" & valorePG & "','" & Format(Now, "yyyyMMdd") & "'," _
                    & " 'Milano','" & Format(Now, "yyyyMMdd") & "',0," & idLuogoNascita & ",'" & telefono & "','" _
                    & idLUOGOres & "'," & idTIPOres & ",'" & par.PulisciStrSql(indirizzoRes) & "','" _
                    & civicoRes & "'," & par.IfNull(myReader("N_COMP_NUCLEO"), "''") & "," & par.IfNull(myReader("N_INV_100_CON"), "''") & "," _
                    & par.IfNull(myReader("N_INV_100_SENZA"), "''") & "," & par.IfNull(myReader("N_INV_100_66"), "''") & "," & par.IfNull(myReader("ID_TIPO_CAT_AB"), "''") & "," _
                    & annoReddito & ",'" & par.IfNull(myReader("LUOGO_INT_ERP"), "") & "','" & par.IfNull(myReader("DATA_INT_ERP"), "") & "','" _
                    & par.IfNull(myReader("LUOGO_S"), "") & "','" & par.IfNull(myReader("DATA_S"), "") & "'," & par.IfNull(myReader("PROGR_DNTE"), "''") & ",'" _
                    & par.IfNull(myReader("FL_GIA_TITOLARE"), "") & "','" & capRes & "','" & par.IfNull(myReader("FL_UBICAZIONE"), "") & "'," _
                    & par.IfNull(myReader("POSSESSO_UI"), "''") & ",'" & par.IfNull(myReader("FL_APPLICA_36"), "") & "','" & par.IfNull(myReader("MINORI_CARICO"), "") & "','" _
                    & par.IfNull(myReader("ISEE"), "") & "','" & par.IfNull(myReader("ISE_ERP"), "") & "','" & par.IfNull(myReader("ISR_ERP"), "") & "','" _
                    & par.IfNull(myReader("ISP_ERP"), "") & "','" & par.IfNull(myReader("PSE"), "") & "','" & par.IfNull(myReader("VSE"), "") & "','" & ModRichiesta.Value & "','" & dataInizio & "','" & dataFine & "','" & idSind & "','" & altro & "')"
                par.cmd.ExecuteNonQuery()

            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE = " & id_dichia
            myReader = par.cmd.ExecuteReader()
            While myReader.Read '-------------- CICLO SUI COMPONENTI DEL NUCLEO FAMILIARE --------------'
                id_compon = myReader("ID")


                par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read Then
                    new_id_compon = myReaderN(0)
                End If
                myReaderN.Close()


                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC,TIPO_INVAL,NATURA_INVAL) " _
                    & "VALUES (" & new_id_compon & "," & new_idDichia & "," & par.IfNull(myReader("PROGR"), "") & ",'" & par.IfNull(myReader("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader("COGNOME"), "")) & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReader("NOME"), "")) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "','" _
                    & par.IfNull(myReader("PERC_INVAL"), "") & "','" & par.IfNull(myReader("INDENNITA_ACC"), "") & "','" & par.IfNull(myReader("TIPO_INVAL"), "") & "','" & par.IfNull(myReader("NATURA_INVAL"), "") & "')"
                par.cmd.ExecuteNonQuery()

                If id_compon = id_intest Then
                    id_intest = new_id_compon
                End If

                Dim distanzaKm As Decimal = 0
                Dim idRedditoTot As Long = 0

                If LBLrisp.Value = "1" Then 'se clicca SI

                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "'"
                        Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCC.Read() Then
                            distanzaKm = par.IfNull(myReaderCC("DISTANZA_KM"), 0)
                        End If
                        myReaderCC.Close()

                        par.cmd.CommandText = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,REND_CATAST_DOMINICALE,ID_TIPO_PROPRIETA,DISTANZA_KM) " _
                            & "VALUES (SEQ_COMP_PATR_IMMOB_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                            & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "'," _
                            & "'" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "','" & par.IfNull(myReader2("N_VANI"), "") & "'," _
                            & "'" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), 0) & "," _
                            & par.IfNull(myReader2("REND_CATAST_DOMINICALE"), 0) & "," & par.IfNull(myReader2("ID_TIPO_PROPRIETA"), "NULL") & "," & distanzaKm & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        par.cmd.CommandText = "INSERT INTO COMP_PATR_MOB_VSA (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO,IBAN,ID_TIPO,PERC_PROPRIETA) " _
                            & "VALUES (SEQ_COMP_PATR_MOB_VSA.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ",'" & par.IfNull(myReader3("IBAN"), "") & "'," & par.IfNull(myReader3("ID_TIPO"), "NULL") & "," & par.IfNull(myReader3("PERC_PROPRIETA"), 1) & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader3.Close()

                    Dim idReddComp As Long = 0
                    Dim reddAltro As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader4.Read
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                            & "VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & new_id_compon & "," & par.VirgoleInPunti(par.IfNull(myReader4("REDDITO_IRPEF"), 0)) & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_COMP_REDDITO_VSA.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idReddComp = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader5.Read
                            reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReader5.Close()
                    End While
                    myReader4.Close()

                    'par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                    'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'While myReader5.Read
                    '    par.cmd.CommandText = "INSERT INTO COMP_ALTRI_REDDITI_VSA (ID,ID_COMPONENTE,IMPORTO) " _
                    '    & "VALUES (SEQ_COMP_ALTRI_REDDITI_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader5("IMPORTO"), "") & ")"
                    '    par.cmd.ExecuteNonQuery()
                    'End While
                    'myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader6.Read
                        par.cmd.CommandText = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                        & "VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader6.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader7.Read
                        par.cmd.CommandText = "INSERT INTO COMP_DETRAZIONI_VSA (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                        & "VALUES (SEQ_COMP_DETRAZIONI_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader7.Close()

                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader8.Read
                        par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID,ID_DOMANDA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI,PENS_ESENTE,NO_ISEE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & new_idDichia & "," & new_id_compon & ",'" _
                        & par.IfNull(myReader8("CONDIZIONE"), "0") & "','" & par.IfNull(myReader8("PROFESSIONE"), "0") & "','" _
                        & par.IfNull(myReader8("DIPENDENTE"), "0") & "','" & par.IfNull(myReader8("PENSIONE"), "0") & "','" _
                        & par.IfNull(myReader8("AUTONOMO"), "0") & "','" & par.IfNull(myReader8("NON_IMPONIBILI"), "0") & "','" _
                        & par.IfNull(myReader8("DOM_AG_FAB"), "0") & "','" & par.IfNull(myReader8("OCCASIONALI"), "0") & "','" & par.IfNull(myReader8("ONERI"), "0") & "','" & par.IfNull(myReader8("PENS_ESENTE"), "0") & "','" & par.IfNull(myReader8("NO_ISEE"), "0") & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_UTENZA_REDDITI.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idRedditoTot = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddA.Read

                            par.cmd.CommandText = "INSERT INTO VSA_REDD_AUTONOMO_IMPORTI (SELECT seq_VSA_REDD_AUTON_IMPORTI.nextval, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID =" & par.IfNull(myReaderReddA("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddA.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddD.Read

                            par.cmd.CommandText = "INSERT INTO VSA_REDD_DIPEND_IMPORTI (SELECT seq_VSA_REDD_DIPEND_IMPORTI.nextval, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_DIPEND_IMPORTI WHERE ID =" & par.IfNull(myReaderReddD("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddD.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddN.Read
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_NO_ISEE_IMPORTI (SELECT seq_VSA_REDD_NO_ISEE_IMP.nextval, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID =" & par.IfNull(myReaderReddN("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddN.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP.Read
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_ES_IMPORTI (SELECT seq_VSA_REDD_PENS_ES_IMP.nextval, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID =" & par.IfNull(myReaderReddP("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & par.IfNull(myReader8("ID"), "0")
                        Dim myReaderReddP2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReaderReddP2.Read
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_IMPORTI (SELECT seq_VSA_REDD_PENS_IMPORTI.nextval, ID_REDD_PENSIONE, IMPORTO, NUM_GG, " & idRedditoTot & " FROM VSA_REDD_PENS_IMPORTI WHERE ID =" & par.IfNull(myReaderReddP2("ID"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReaderReddP2.Close()
                    End While
                    myReader8.Close()

                End If

            End While
            myReader.Close()

            par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = 0 WHERE ID =" & id_intest
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID <> " & id_intest & " AND ID_DICHIARAZIONE = " & new_idDichia & " ORDER BY PROGR ASC FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = " & i & " WHERE ID =" & myReader("ID")
                par.cmd.ExecuteNonQuery()
                i = i + 1
            End While
            myReader.Close()

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaDomVSA()

            If tipoRich.Value <> "3" And tipoRich.Value <> "2" Then
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                 & "else{window.close();}</script>"
                Response.Write(strScript)
            End If
            If tipoRich.Value = "2" And causale.Value = "30" Then
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                & "else{window.close();}</script>"
                Response.Write(strScript)
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub CaricaDaBando()

        Dim lValoreCorrente As Long
        Dim valorePG As String
        Dim i As Integer = 1
        Dim strScript As String

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            valorePG = Format(lValoreCorrente, "0000000000")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans


            If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND PROGR=0"
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    id_intest = myReader0("ID")
                End If
                myReader0.Close()
            Else
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND (COD_FISCALE ='" & codFisc.Value & "' OR COGNOME||' '||NOME LIKE '" & par.PulisciStrSql(intestatario.Value) & "%')"
                myReader = par.cmd.ExecuteReader()
                If myReader.HasRows Then
                    While myReader.Read
                        If par.IfNull(myReader("COD_FISCALE"), "") = codFisc.Value Then
                            id_intest = myReader("ID") 'ottengo l'id dell'intestatario che intende presentare la domanda
                        End If
                    End While
                    myReader.Close()
                Else
                    Response.Write("<script>alert('Attenzione...la domanda non può essere intestata automaticamente al componente scelto!\nSi procederà al caricamento dei dati di un componente del nucleo presente nel sistema.')</script>")
                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND PROGR=0"
                    Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader0.Read Then
                        id_intest = myReader0("ID")
                    End If
                    myReader0.Close()
                End If
            End If


            par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO_VSA.NEXTVAL FROM DUAL"
            Dim myReaderNew As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderNew.Read() Then
                new_id_dom = myReaderNew(0)
            End If
            myReaderNew.Close()

            par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.NEXTVAL FROM DUAL"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                new_idDichia = myReader1(0)
            End If
            myReader1.Close()

            Dim idLuogoNascita As Integer = 0
            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI,COMP_NUCLEO WHERE COMP_NUCLEO.ID_DICHIARAZIONE = DICHIARAZIONI.ID AND DICHIARAZIONI.ID = " & id_dichia & " AND COMP_NUCLEO.COD_FISCALE='" & codFisc.Value & "' AND COMP_NUCLEO.PROGR =0"
            Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCodF.Read = False Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD = '" & codFisc.Value.Substring(11, 4) & "'"
                Dim myReaderCom As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderCom.Read Then
                    idLuogoNascita = par.IfNull(myReaderCom("ID"), "")
                End If
                myReaderCom.Close()
            End If
            myReaderCodF.Close()

            '************* 13/03/2012 CERCO L'ULTIMA DICHIARAZIONE DA CUI IMPORTARE GLI INDIRIZZI AGGIORNATI

            piurecente = CercaUltimeDich(idLUOGOres, idTIPOres, indirizzoRes, civicoRes, capRes, telefono)


            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID = " & id_dichia
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If idLuogoNascita = 0 Then
                    idLuogoNascita = par.IfNull(myReader("ID_LUOGO_NAS_DNTE"), "")
                End If
                If piurecente = False Then
                    idLUOGOres = par.IfNull(myReader("ID_LUOGO_RES_DNTE"), 0)
                    idTIPOres = par.IfNull(myReader("ID_TIPO_IND_RES_DNTE"), 0)
                    indirizzoRes = par.IfNull(myReader("IND_RES_DNTE"), "")
                    civicoRes = par.IfNull(myReader("CIVICO_RES_DNTE"), "")
                    capRes = par.IfNull(myReader("CAP_RES_DNTE"), "")
                    telefono = par.IfNull(myReader("TELEFONO_DNTE"), "")
                End If
                'If LBLrisp.Value = "1" Then
                If Request.QueryString("ANNI") <> "" Then
                    annoReddito = Request.QueryString("ANNI")
                Else
                    annoReddito = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                End If
                'Else
                '    annoReddito = "2010"
                'End If

                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID_DICHIARAZIONE = " & id_dichia
                Dim myReaderDomB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderDomB.Read Then

                    par.cmd.CommandText = "INSERT INTO DICHIARAZIONI_VSA (ID,ID_CAF,ID_BANDO,PG,DATA_PG,LUOGO,DATA,ID_STATO,ID_LUOGO_NAS_DNTE,TELEFONO_DNTE,ID_LUOGO_RES_DNTE," _
                        & " ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,N_COMP_NUCLEO,N_INV_100_CON,N_INV_100_SENZA,N_INV_100_66,ID_TIPO_CAT_AB,ANNO_SIT_ECONOMICA,LUOGO_INT_ERP," _
                        & " DATA_INT_ERP,LUOGO_S,DATA_S,PROGR_DNTE,FL_GIA_TITOLARE,CAP_RES_DNTE,MINORI_CARICO,ISEE,ISE_ERP,ISR_ERP,ISP_ERP,PSE,VSE,MOD_PRESENTAZIONE,DATA_INIZIO_VAL,DATA_FINE_VAL,ID_SINDACATO_VSA,MOD_PRES_ALTRO) " _
                        & " VALUES (" & new_idDichia & "," & Session.Item("ID_CAF") & "," & id_bando & ",'" & valorePG & "','" & Format(Now, "yyyyMMdd") & "'," _
                        & " 'Milano','" & Format(Now, "yyyyMMdd") & "',0," & idLuogoNascita & ",'" & telefono & "','" _
                        & idLUOGOres & "'," & idTIPOres & ",'" & par.PulisciStrSql(indirizzoRes) & "','" _
                        & civicoRes & "'," & par.IfNull(myReader("N_COMP_NUCLEO"), "''") & "," & par.IfNull(myReader("N_INV_100_CON"), "''") & "," _
                        & par.IfNull(myReader("N_INV_100_SENZA"), "''") & "," & par.IfNull(myReader("N_INV_100_66"), "''") & "," & par.IfNull(myReader("ID_TIPO_CAT_AB"), "''") & "," _
                        & annoReddito & ",'" & par.IfNull(myReader("LUOGO_INT_ERP"), "") & "','" & par.IfNull(myReader("DATA_INT_ERP"), "") & "','" _
                        & par.IfNull(myReader("LUOGO_S"), "") & "','" & par.IfNull(myReader("DATA_S"), "") & "'," & par.IfNull(myReader("PROGR_DNTE"), "''") & ",'" _
                        & par.IfNull(myReader("FL_GIA_TITOLARE"), "") & "','" & capRes & "'," _
                        & "'" & par.IfNull(myReaderDomB("MINORI_CARICO"), "") & "','" _
                        & par.IfNull(myReaderDomB("REDDITO_ISEE"), "") & "','" & par.IfNull(myReaderDomB("ISE_ERP"), "") & "','" & par.IfNull(myReaderDomB("ISR_ERP"), "") & "','" _
                        & par.IfNull(myReaderDomB("ISP_ERP"), "") & "','" & par.IfNull(myReaderDomB("PSE"), "") & "','" & par.IfNull(myReaderDomB("VSE"), "") & "','" & ModRichiesta.Value & "','" & dataInizio & "','" & dataFine & "','" & idSind & "','" & altro & "')"
                    par.cmd.ExecuteNonQuery()

                End If
                myReaderDomB.Close()

            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia
            myReader = par.cmd.ExecuteReader()
            While myReader.Read '-------------- CICLO SUI COMPONENTI DEL NUCLEO FAMILIARE --------------'
                id_compon = myReader("ID")

                par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.NEXTVAL FROM DUAL" 'Prendo il nuovo id_componente
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read Then
                    new_id_compon = myReaderN(0)
                End If
                myReaderN.Close()

                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COD_FISCALE,COGNOME,NOME,SESSO,DATA_NASCITA,GRADO_PARENTELA,PERC_INVAL,INDENNITA_ACC) " _
                    & "VALUES (" & new_id_compon & "," & new_idDichia & "," & par.IfNull(myReader("PROGR"), "") & ",'" & par.IfNull(myReader("COD_FISCALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader("COGNOME"), "")) & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(myReader("NOME"), "")) & "','" & par.IfNull(myReader("SESSO"), "") & "','" & par.IfNull(myReader("DATA_NASCITA"), "") & "','" & par.IfNull(myReader("GRADO_PARENTELA"), "") & "'," _
                    & par.IfNull(myReader("PERC_INVAL"), "") & ",'" & par.IfNull(myReader("INDENNITA_ACC"), "") & "')"
                par.cmd.ExecuteNonQuery()

                If id_compon = id_intest Then
                    id_intest = new_id_compon
                End If

                Dim distanzaKm As Decimal = 0

                If LBLrisp.Value = "1" Then 'se clicca SI

                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read

                        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "'"
                        Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCC.Read() Then
                            distanzaKm = par.IfNull(myReaderCC("DISTANZA_KM"), 0)
                        End If
                        myReaderCC.Close()

                        par.cmd.CommandText = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,DISTANZA_KM) " _
                            & "VALUES (SEQ_COMP_PATR_IMMOB_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader2("ID_TIPO"), "") & ",'" & par.IfNull(myReader2("PERC_PATR_IMMOBILIARE"), "") & "'," _
                            & "'" & par.IfNull(myReader2("VALORE"), "") & "','" & par.IfNull(myReader2("MUTUO"), "") & "','" & par.IfNull(myReader2("F_RESIDENZA"), "") & "'," _
                            & "'" & par.IfNull(myReader2("CAT_CATASTALE"), "") & "','" & par.PulisciStrSql(par.IfNull(myReader2("COMUNE"), "")) & "','" & par.IfNull(myReader2("N_VANI"), "") & "'," _
                            & "'" & par.IfNull(myReader2("SUP_UTILE"), "") & "','" & par.IfNull(myReader2("FL_70KM"), "") & "'," & par.IfNull(myReader2("PIENA_PROPRIETA"), 0) & "," & distanzaKm & "" _
                            & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        par.cmd.CommandText = "INSERT INTO COMP_PATR_MOB_VSA (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) " _
                            & "VALUES (SEQ_COMP_PATR_MOB_VSA.NEXTVAL," & new_id_compon & ",'" & par.IfNull(myReader3("COD_INTERMEDIARIO"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReader3("INTERMEDIARIO"), "")) & "'," & par.IfNull(myReader3("IMPORTO"), "") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader3.Close()

                    Dim idReddComp As Long = 0
                    Dim reddAltro As Decimal = 0
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader4.Read
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) " _
                            & "VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & new_id_compon & "," & par.VirgoleInPunti(par.IfNull(myReader4("REDDITO_IRPEF"), 0)) & "," & par.IfNull(myReader4("PROV_AGRARI"), 0) & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SEQ_COMP_REDDITO_VSA.currval from dual"
                        Dim myReaderCV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCV.Read Then
                            idReddComp = myReaderCV(0)
                        End If
                        myReaderCV.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader5.Read
                            reddAltro = reddAltro + par.IfNull(myReader5("IMPORTO"), "")
                            par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET REDDITO_IRPEF=" & par.IfNull(myReader4("REDDITO_IRPEF"), "") + reddAltro & " WHERE ID=" & idReddComp
                            par.cmd.ExecuteNonQuery()
                        End While
                        myReader5.Close()
                    End While
                    myReader4.Close()

                    'par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE = " & id_compon
                    'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'While myReader5.Read
                    '    par.cmd.CommandText = "INSERT INTO COMP_ALTRI_REDDITI_VSA (ID,ID_COMPONENTE,IMPORTO) " _
                    '    & "VALUES (SEQ_COMP_ALTRI_REDDITI_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader5("IMPORTO"), "") & ")"
                    '    par.cmd.ExecuteNonQuery()
                    'End While
                    'myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader6.Read
                        par.cmd.CommandText = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) " _
                        & "VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader6("IMPORTO"), "") & ",'" & par.IfNull(myReader6("DESCRIZIONE"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader6.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader7.Read
                        par.cmd.CommandText = "INSERT INTO COMP_DETRAZIONI_VSA (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) " _
                        & "VALUES (SEQ_COMP_DETRAZIONI_VSA.NEXTVAL," & new_id_compon & "," & par.IfNull(myReader7("ID_TIPO"), "") & "," & par.IfNull(myReader7("IMPORTO"), "0") & ")"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader7.Close()

                    par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI WHERE ID_COMPONENTE = " & id_compon
                    Dim myReader8 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader8.Read
                        par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID,ID_DOMANDA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,DOM_AG_FAB,OCCASIONALI,ONERI) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & new_idDichia & "," & new_id_compon & ",'" _
                        & par.IfNull(myReader8("CONDIZIONE"), "0") & "','" & par.IfNull(myReader8("PROFESSIONE"), "0") & "','" _
                        & par.IfNull(myReader8("DIPENDENTE"), "0") & "','" & par.IfNull(myReader8("PENSIONE"), "0") & "','" _
                        & par.IfNull(myReader8("AUTONOMO"), "0") & "','" & par.IfNull(myReader8("NON_IMPONIBILI"), "0") & "','" _
                        & par.IfNull(myReader8("DOM_AG_FAB"), "0") & "','" & par.IfNull(myReader8("OCCASIONALI"), "0") & "','" & par.IfNull(myReader8("ONERI"), "0") & "')"
                        par.cmd.ExecuteNonQuery()
                    End While
                    myReader8.Close()

                End If

            End While
            myReader.Close()

            par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = 0 WHERE ID =" & id_intest
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID <> " & id_intest & " AND ID_DICHIARAZIONE = " & new_idDichia & " ORDER BY PROGR ASC FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader
            While myReader.Read
                par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET PROGR = " & i & " WHERE ID =" & myReader("ID")
                par.cmd.ExecuteNonQuery()
                i = i + 1
            End While
            myReader.Close()

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaDomBando()

            If tipoRich.Value <> "3" And tipoRich.Value <> "2" Then
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                 & "else{window.close();}</script>"
                Response.Write(strScript)
            End If
            If tipoRich.Value = "2" And causale.Value = "30" Then
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                & "else{window.close();}</script>"
                Response.Write(strScript)
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try



    End Sub

    Protected Sub CaricaDomBando()

        'Dim idDomanda As Long
        Dim iDluogo As Long
        Dim codIndir As Integer
        Dim sup_netta As Decimal
        Dim ascens As Integer
        Dim lValoreCorrenteDom As Long
        Dim valorePGdom As String
        Dim tipoBando As Long
        Dim cod_UI As String = ""

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrenteDom = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrenteDom & ")"
            par.cmd.ExecuteNonQuery()
            valorePGdom = Format(lValoreCorrenteDom, "0000000000")
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans



            par.cmd.CommandText = "SELECT * FROM BANDI_VSA WHERE ID=" & id_bando
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                tipoBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DISTINCT(UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE) FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_CONTRATTUALE.id_unita_principale is null and  UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID AND ID_CONTRATTO=(SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & CodContratto.Value & "')"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                cod_UI = par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "0")
            End If
            myReader.Close()


            If dataPr = "" Then
                dataPr = Format(Now, "yyyyMMdd")
            End If


            Dim infoDocumento As Boolean = False
            Dim numDoc As String = ""
            Dim dataDoc As String = ""
            Dim rilascioDoc As String = ""
            Dim docSoggiorno As String = ""

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI,COMP_NUCLEO WHERE COMP_NUCLEO.ID_DICHIARAZIONE =DICHIARAZIONI.ID AND DICHIARAZIONI.ID = " & id_dichia & " AND COMP_NUCLEO.COD_FISCALE='" & codFisc.Value & "' AND COMP_NUCLEO.PROGR =0"
            Dim myReaderCodF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCodF.Read = False Then
                par.cmd.CommandText = "SELECT SISCOM_MI.ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND RAPPORTI_UTENZA.COD_CONTRATTO ='" & CodContratto.Value & "' AND (ANAGRAFICA.COD_FISCALE='" & codFisc.Value & "' OR COGNOME||' '||NOME ='" & par.PulisciStrSql(intestatario.Value) & "')"
                Dim myReaderCI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCI.Read Then
                    infoDocumento = True
                    numDoc = par.IfNull(myReaderCI("NUM_DOC"), "")
                    dataDoc = par.IfNull(myReaderCI("DATA_DOC"), "")
                    rilascioDoc = par.IfNull(myReaderCI("RILASCIO_DOC"), "")
                    docSoggiorno = par.IfNull(myReaderCI("DOC_SOGGIORNO"), "")
                End If
                myReaderCI.Close()
            End If
            myReaderCodF.Close()


            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID_DICHIARAZIONE=" & id_dichia
            Dim myReaderBando As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderBando.Read Then

                If infoDocumento = False Then
                    numDoc = par.IfNull(myReaderBando("CARTA_I"), "")
                    dataDoc = par.IfNull(myReaderBando("CARTA_I_DATA"), "")
                    rilascioDoc = par.IfNull(myReaderBando("CARTA_I_RILASCIATA"), "")
                    docSoggiorno = par.IfNull(myReaderBando("PERMESSO_SOGG_N"), "")
                End If

                '------ inserimento nuova domanda ------' 
                par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA (ID,ID_BANDO,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA," _
                    & "ID_DICHIARAZIONE,PROGR_COMPONENTE,PG,DATA_PG,PRESSO_REC_DNTE,IND_REC_DNTE,ID_LUOGO_REC_DNTE,TELEFONO_REC_DNTE," _
                    & "ID_TIPO_IND_REC_DNTE,CIVICO_REC_DNTE,REDDITO_ISEE,ISR_ERP,ISP_ERP,ISE_ERP,CAP_REC_DNTE,MINORI_CARICO,PSE,VSE,ID_MOTIVO_DOMANDA," _
                    & "DATA_PRESENTAZIONE,TIPO_ALLOGGIO,FL_MOROSITA,FL_PROFUGO,ANNO_RIF_CANONE,IMPORTO_CANONE,ANNO_RIF_SPESE_ACC,IMPORTO_SPESE_ACC," _
                    & "ID_PARA_0,ID_PARA_1,ID_PARA_2,ID_PARA_3,ID_PARA_4,ID_PARA_5,ID_PARA_6,ID_PARA_7,ID_PARA_8,ID_PARA_9,ID_PARA_10,ID_PARA_11,ID_PARA_12," _
                    & "ID_PARA_13,ID_PARA_14,ID_PARA_15,REQUISITO1,REQUISITO2,REQUISITO3,REQUISITO4,REQUISITO5,REQUISITO6,REQUISITO7,REQUISITO8,REQUISITO9," _
                    & "ISBAR, ISBARC, ISBARC_R, DISAGIO_F, DISAGIO_A, DISAGIO_E,FL_ISTRUTTORIA_COMPLETA,FL_COMPLETA,FL_ESAMINATA,FL_PROPOSTA,FL_CONTROLLA_REQUISITI," _
                    & "FL_INVITO,CONTRATTO_NUM,CONTRATTO_DATA,NUM_ALLOGGIO,FL_RINNOVO,PERIODO_RES,CONTRATTO_DATA_DEC," _
                    & "FL_ASS_ESTERNA,CARTA_I,CARTA_I_DATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,PERMESSO_SOGG_SCADE,PERMESSO_SOGG_RINNOVO,FL_FATTA_AU,FL_FATTA_ERP," _
                    & "FL_FAI_ERP,CARTA_SOGG_N,CARTA_SOGG_DATA,CARTA_I_RILASCIATA,REQUISITO10,REQUISITO11,REQUISITO12,REQUISITO13,REQUISITO14,REQUISITO15,REQUISITO16," _
                    & "FL_VERIFICA_CONCLUSA,REQUISITO17,REQUISITO18,REQUISITO19,FL_NATO_ESTERO,ACCOLTA,TIPO_U,ID_CAUSALE_DOMANDA,DATA_EVENTO,TIPO_D_IMPORT,ID_D_IMPORT,COD_CONTRATTO_SCAMBIO,ID_INTEST_NEW_RU) " _
                        & "VALUES (" & new_id_dom & "," & id_bando & ",'0','1','0','0','" & tipoBando & "'," & new_idDichia & "," _
                        & myReaderBando("PROGR_COMPONENTE") & ",'" & valorePGdom & "','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfNull(myReaderBando("PRESSO_REC_DNTE"), "")) & "'," _
                        & "'" & par.PulisciStrSql(indirizzoRes) & "','" & idLUOGOres & "','" & telefono & "','" & idTIPOres & "'," _
                        & "'" & civicoRes & "'," & CDec(par.VirgoleInPunti(par.IfNull(myReaderBando("REDDITO_ISEE"), "0"))) & "," & par.IfNull(par.VirgoleInPunti(myReaderBando("ISR_ERP")), "''") & "," _
                        & "" & par.IfNull(par.VirgoleInPunti(myReaderBando("ISP_ERP")), "''") & ", " & par.IfNull(par.VirgoleInPunti(myReaderBando("ISE_ERP")), "''") & ",'" & capRes & "','" _
                        & " " & par.IfNull(myReaderBando("MINORI_CARICO"), "") & "','" & par.IfNull(myReaderBando("PSE"), "") & "','" & par.IfNull(myReaderBando("VSE"), "") & "'," & tipoRich.Value & "," _
                        & "'" & dataPr & "','0','1','0','" & par.IfNull(myReaderBando("ANNO_RIF_CANONE"), "") & "',0,'" & par.IfNull(myReaderBando("ANNO_RIF_SPESE_ACC"), "") & "'," _
                        & "0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'1','1','1','1','1','1','1','1','1',0,0,0,0,0,0,'0','0','0','0',NULL,'0','" & CodContratto.Value & "','" _
                        & par.IfNull(myReaderBando("CONTRATTO_DATA"), "") & "','" & par.IfNull(myReaderBando("NUM_ALLOGGIO"), "") & "','0',-1,'" & par.IfNull(myReaderBando("CONTRATTO_DATA_DEC"), "") & "'," _
                        & "'1','" & numDoc & "','" & dataDoc & "','" & docSoggiorno & "','" _
                        & par.IfNull(myReaderBando("PERMESSO_SOGG_DATA"), "") & "','" & par.IfNull(myReaderBando("PERMESSO_SOGG_SCADE"), "") & "','" & par.IfNull(myReaderBando("PERMESSO_SOGG_RINNOVO"), "") & "'," _
                        & "'1','1','0','" & par.IfNull(myReaderBando("CARTA_SOGG_N"), "") & "','" & par.IfNull(myReaderBando("CARTA_SOGG_DATA"), "") & "','" & par.PulisciStrSql(rilascioDoc) & "'," _
                        & "'1','1','1','1','1','1','1','0','1','1','1','0','0','0','" & causale.Value & "','" & dataEvento & "'," & Tipo_Domanda & "," & id_dichia & ",'" & codContrSc & "'," & par.IfEmpty(intestNewRU.Value, "NULL") & ")"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SCALE_EDIFICI WHERE COD_UNITA_IMMOBILIARE='" & cod_UI & "' AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA"
                Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderUI.Read() Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & par.IfNull(myReaderUI("ID"), "") & " AND COD_TIPOLOGIA='SUP_NETTA'"
                    Dim myReaderMQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderMQ.Read Then
                        sup_netta = par.IfNull(myReaderMQ("VALORE"), "")
                    End If
                    myReaderMQ.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID =" & par.IfNull(myReaderUI("ID_INDIRIZZO"), "")
                    Dim myReaderInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderInd.Read Then

                        'query per verificare la presenza o meno dell'ascensore
                        par.cmd.CommandText = "SELECT IMPIANTI.* FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.SCALE_EDIFICI WHERE IMPIANTI.ID = IMPIANTI_SCALE.ID_IMPIANTO " _
                            & "AND SCALE_EDIFICI.ID = IMPIANTI_SCALE.ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = '" & myReaderUI("ID_EDIFICIO") & "' AND SCALE_EDIFICI.DESCRIZIONE = '" & myReaderUI("DESCRIZIONE") & "' AND IMPIANTI.COD_TIPOLOGIA = 'SO'"
                        Dim myReaderAsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderAsc.Read Then
                            ascens = 1
                        Else
                            ascens = 0
                        End If
                        myReaderAsc.Close()

                        par.cmd.CommandText = "INSERT INTO DOMANDE_VSA_ALLOGGIO (ID_DOMANDA,COMUNE,CAP,INDIRIZZO,CIVICO,ID_TIPO_GESTORE," _
                            & "SCALA,PIANO,INTERNO,NUM_CONTRATTO,DEC_CONTRATTO,ASS_TEMPORANEA,ID_TIPO_CONTRATTO," _
                            & "COD_UNITA_IMMOBILIARE,SUP_NETTA,ASCENSORE) VALUES (" & new_id_dom & ",'" & par.IfNull(myReaderInd("LOCALITA"), "") & "','" & par.IfNull(myReaderInd("CAP"), "") & "'," _
                            & "'" & par.PulisciStrSql(par.IfNull(myReaderInd("DESCRIZIONE"), "")) & "','" & par.IfNull(myReaderInd("CIVICO"), "") & "','9','" & par.IfNull(myReaderUI("DESCRIZIONE"), "") & "'," _
                            & "'" & par.IfNull(myReaderUI("COD_TIPO_LIVELLO_PIANO"), "") & "','" & par.IfNull(myReaderUI("INTERNO"), "") & "','" & CodContratto.Value & "'," _
                            & "'" & par.IfNull(myReaderBando("CONTRATTO_DATA"), "") & "','0','0','" & par.IfNull(myReaderBando("NUM_ALLOGGIO"), "") & "','" & sup_netta & "','" & ascens & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                            & "SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & id_contr.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                        Dim myReaderIntest As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderIntest.Read Then

                            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD ='" & myReaderIntest("COD_COMUNE_NASCITA") & "'"
                            Dim myReaderLuogoNasc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderLuogoNasc.Read Then
                                idLUOGOnasc = myReaderLuogoNasc("ID")
                            End If
                            myReaderLuogoNasc.Close()

                            par.cmd.CommandText = "INSERT INTO INTESTATARI_CONTRATTI_VSA (ID_DOMANDA,COGNOME,NOME,SESSO,COD_FISCALE,ID_LUOGO_NAS_DNTE,DATA_NASCITA_DNTE) VALUES " _
                                & "(" & new_id_dom & ",'" & par.PulisciStrSql(par.IfNull(myReaderIntest("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderIntest("NOME"), "")) & "'," _
                                & "'" & par.IfNull(myReaderIntest("SESSO"), "") & "','" & par.IfNull(myReaderIntest("COD_FISCALE"), "") & "','" & idLUOGOnasc & "','" & par.IfNull(myReaderIntest("DATA_NASCITA"), "") & "')"
                            par.cmd.ExecuteNonQuery()

                        End If
                        myReaderIntest.Close()

                    End If
                    myReaderInd.Close()

                End If
                myReaderUI.Close()

            End If
            myReaderBando.Close()


            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & new_id_dom & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
            & "','F190','','I')"
            par.cmd.ExecuteNonQuery()


            '##### prova #####
            If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND PROGR=0"
            Else
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE = " & id_dichia & " AND (COD_FISCALE ='" & codFisc.Value & "' OR COGNOME||' '||NOME LIKE '" & par.PulisciStrSql(intestatario.Value) & "%')"
            End If
            Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderI.Read Then

                par.cmd.CommandText = "SELECT * FROM COMP_NAS_RES WHERE ID_COMPONENTE=" & myReaderI("ID")
                Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderComp.Read Then

                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE PROGR= 0 AND ID_DICHIARAZIONE = " & new_idDichia
                    Dim myReaderIDComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderIDComp.Read Then
                        par.cmd.CommandText = "INSERT INTO COMP_NAS_RES_VSA (ID_COMPONENTE,ID_LUOGO_NAS_DNTE,ID_LUOGO_RES_DNTE,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE," _
                            & "CIVICO_RES_DNTE,TELEFONO_DNTE,CAP_RES) VALUES (" & myReaderIDComp("ID") & ",'" & par.IfNull(myReaderComp("ID_LUOGO_NAS_DNTE"), "") & "'," _
                            & "'" & par.IfNull(myReaderComp("ID_LUOGO_RES_DNTE"), "") & "','" & par.IfNull(myReaderComp("ID_TIPO_IND_RES_DNTE"), "") & "','" _
                            & par.PulisciStrSql(par.IfNull(myReaderComp("IND_RES_DNTE"), "")) & "','" & par.IfNull(myReaderComp("CIVICO_RES_DNTE"), "") & "'," _
                            & "'" & par.IfNull(myReaderComp("TELEFONO_DNTE"), "") & "','" & par.IfNull(myReaderComp("CAP_RES"), "") & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderIDComp.Close()

                End If
                myReaderComp.Close()

            End If
            myReaderI.Close()
            '##### fine prova #####


            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnSi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSi.Click

        If importRedd.Value = "0" And errore = False Then
            CaricaDichiarazione()
        ElseIf importRedd.Value = "1" And errore = False Then
            CaricaReddVSA()
        ElseIf importRedd.Value = "2" And errore = False Then
            CaricaDaBando()
        End If
        If causale.Value <> 30 Then
            If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                RicavaSituazionePRE()
            End If
            If tipoRich.Value = 5 Then
                NuovaDomandaCambio(new_id_dom, new_idDichia)
            End If
        End If
        GestioneSegnalazione()
    End Sub

    Protected Sub btnNo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNo.Click
        If importRedd.Value = "0" And errore = False Then
            CaricaDichiarazione()
        ElseIf importRedd.Value = "1" And errore = False Then
            CaricaReddVSA()
        ElseIf importRedd.Value = "2" And errore = False Then
            CaricaDaBando()
        End If
        If causale.Value <> 30 Then
            If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                RicavaSituazionePRE()
            End If
            If tipoRich.Value = 5 Then
                NuovaDomandaCambio(new_id_dom, new_idDichia)
            End If
        End If
        GestioneSegnalazione()
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If errore = False Then
            CaricaNOdichiarazione()
        End If
        If causale.Value <> 30 Then
            If tipoRich.Value = 2 Or tipoRich.Value = 3 Then
                RicavaSituazionePRE()
            End If
            If tipoRich.Value = 5 Then
                NuovaDomandaCambio(new_id_dom, new_idDichia)
            End If
        End If
        GestioneSegnalazione()
    End Sub


    '************* 13/03/2012 FUNZIONE PER CERCARE L'ULTIMA DICHIARAZIONE DA CUI IMPORTARE GLI INDIRIZZI AGGIORNATI
    Private Function CercaUltimeDich(ByRef idLuoRes As Long, ByRef idTipoIND As Long, ByRef indRes As String, ByRef numcivicoRes As String, ByRef capResid As String, ByRef numTel As String) As Boolean
        Dim trovata As Boolean = False

        Try
            par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.* FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA WHERE DOMANDE_BANDO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND CONTRATTO_NUM='" & CodContratto.Value & "' ORDER BY DICHIARAZIONI_VSA.DATA_PG DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                idLuoRes = par.IfNull(myReader1("ID_LUOGO_RES_DNTE"), 0)
                idTipoIND = par.IfNull(myReader1("ID_TIPO_IND_RES_DNTE"), 0)
                indRes = par.IfNull(myReader1("IND_RES_DNTE"), "")
                numcivicoRes = par.IfNull(myReader1("CIVICO_RES_DNTE"), "")
                capResid = par.IfNull(myReader1("CAP_RES_DNTE"), "")
                numTel = par.IfNull(myReader1("TELEFONO_DNTE"), "")
                trovata = True
            Else
                par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.* FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
                & "AND RAPPORTO='" & CodContratto.Value & "' ORDER BY ID_BANDO DESC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    idLuoRes = par.IfNull(myReader2("ID_LUOGO_RES_DNTE"), 0)
                    idTipoIND = par.IfNull(myReader2("ID_TIPO_IND_RES_DNTE"), 0)
                    indRes = par.IfNull(myReader2("IND_RES_DNTE"), "")
                    numcivicoRes = par.IfNull(myReader2("CIVICO_RES_DNTE"), "")
                    capResid = par.IfNull(myReader2("CAP_RES_DNTE"), "")
                    numTel = par.IfNull(myReader2("TELEFONO_DNTE"), "")
                    trovata = True
                Else
                    par.cmd.CommandText = "SELECT DICHIARAZIONI.* FROM DICHIARAZIONI,DOMANDE_BANDO WHERE DICHIARAZIONI.ID = DOMANDE_BANDO.ID_DICHIARAZIONE AND CONTRATTO_NUM='" & CodContratto.Value & "' ORDER BY DICHIARAZIONI.DATA_PG DESC"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        idLuoRes = par.IfNull(myReader3("ID_LUOGO_RES_DNTE"), 0)
                        idTipoIND = par.IfNull(myReader3("ID_TIPO_IND_RES_DNTE"), 0)
                        indRes = par.IfNull(myReader3("IND_RES_DNTE"), "")
                        numcivicoRes = par.IfNull(myReader3("CIVICO_RES_DNTE"), "")
                        capResid = par.IfNull(myReader3("CAP_RES_DNTE"), "")
                        numTel = par.IfNull(myReader3("TELEFONO_DNTE"), "")
                        trovata = True
                    End If
                    myReader3.Close()

                End If
                myReader2.Close()

            End If
            myReader1.Close()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

        Return trovata

    End Function


    Private Sub NuovaDomandaCambio(ByVal idDom1 As Long, ByVal idDich1 As Long)

        Dim idcont As Long = 0
        Dim nomeIntest As String = ""
        Dim codFisc As String = ""
        Dim codContr2 As String = ""
        Dim pgDomCollegato1 As String = ""
        Dim pgDichCollegato1 As String = ""
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            codContr2 = Request.QueryString("CODCONTR2")

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID =" & idDom1
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                pgDomCollegato1 = par.IfNull(myReader("PG"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID =" & idDich1
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                pgDichCollegato1 = par.IfNull(myReader("PG"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT COGNOME||' '||NOME AS NOMINATIVO,COD_FISCALE,RAPPORTI_UTENZA.ID AS IDC from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA where " _
                & " RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND COD_CONTRATTO='" & codContr2 & "'"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                idcont = par.IfNull(myReader("IDC"), -1)
                nomeIntest = par.IfNull(myReader("NOMINATIVO"), "")
                codFisc = par.IfNull(myReader("COD_FISCALE"), "")
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaNOdichiarazione(codFisc, nomeIntest, codContr2, idcont, pgDomCollegato1, pgDichCollegato1)

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub RicavaSituazionePRE()

        Dim IMPORTO As Double
        Dim NuovoTransit As Double
        Dim LOCATIVO As String = ""
        Dim comunicazioni As String = ""
        Dim AreaEconomica As Integer
        Dim sISEE As String = ""
        Dim sISE As String = ""
        Dim sISR As String = ""
        Dim sISP As String = ""
        Dim sVSE As String = ""
        Dim sREDD_DIP As String = ""
        Dim sREDD_ALT As String = ""
        Dim sLimitePensione As String = ""
        Dim sPER_VAL_LOC As String = ""
        Dim sPERC_INC_MAX_ISE_ERP As String = ""
        Dim sCANONE_MIN As String = ""
        Dim sISE_MIN As String = ""
        Dim sCanone As String = ""
        Dim sNOTE As String = ""
        Dim sDEM As String = ""
        Dim sSUPCONVENZIONALE As String = ""
        Dim sCOSTOBASE As String = ""
        Dim sZONA As String = ""
        Dim sPIANO As String = ""
        Dim sCONSERVAZIONE As String = ""
        Dim sVETUSTA As String = ""
        Dim sPSE As String = ""
        Dim sINCIDENZAISE As String = ""
        Dim sCOEFFFAM As String = ""
        Dim sSOTTOAREA As String = ""
        Dim sMOTIVODECADENZA As String = ""
        Dim sNUMCOMP As String = ""
        Dim sNUMCOMP66 As String = ""
        Dim sNUMCOMP100 As String = ""
        Dim sNUMCOMP100C As String = ""
        Dim sPREVDIP As String = ""
        Dim sDETRAZIONI As String = ""
        Dim sMOBILIARI As String = ""
        Dim sIMMOBILIARI As String = ""
        Dim sCOMPLESSIVO As String = ""
        Dim sDETRAZIONEF As String = ""
        Dim sANNOCOSTRUZIONE As String = ""
        Dim sLOCALITA As String = ""
        Dim sASCENSORE As String = ""
        Dim sDESCRIZIONEPIANO As String = ""
        Dim sSUPNETTA As String = ""
        Dim sALTRESUP As String = ""
        Dim sMINORI15 As String = ""
        Dim sMAGGIORI65 As String = ""
        Dim sSUPACCESSORI As String = ""
        Dim sVALORELOCATIVO As String = ""
        Dim sCANONESOPP As String = ""
        Dim sVALOCIICI As String = ""
        Dim sALLOGGIOIDONEO As String = ""
        Dim sISTAT As String = ""
        Dim sCANONECLASSE As String = ""
        Dim sCANONECLASSEISTAT As String = ""
        Dim sANNOINIZIOVAL As String = ""
        Dim sANNOFINEVAL As String = ""
        Dim parte1 As String = ""
        Dim parte2 As String = ""
        Dim parte3 As String = ""
        Dim parte4 As String = ""
        Dim IDdich As String = ""
        Dim dataInizioValidita As String = ""
        Dim I As Integer
        Dim Prov As Integer
        Dim IDUNITA As Long
        Dim ANNO_INIZIO As Integer = 0
        Dim PER_ANNI As Integer = 0
        Dim CanonePREreca As Decimal = 0
        Dim idDichCan_EC As Long = 0
        Dim idDOMCan_EC As Long = 0
        'Dim istat2009 As String = "2,025"
        Dim importoTrovato As Boolean = True
        Dim parte2new As String = ""
        Dim parte3new As String = ""
        Dim ID_AU As Long
        Dim annotazioni As String = ""
        Dim idContratto As Long = 0
        Dim canoneIniziale As Decimal = 0
        Dim tipoContrattoLoc As String = ""
        'Dim ANNICONGUAGLIO As Integer = 0
        Try
            Select Case importRedd.Value
                Case 0
                    Prov = 0
                Case 1
                    Prov = 3
                Case 2
                    Prov = 1
            End Select

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID,ID_UNITA,RAPPORTI_UTENZA.ID_AU,RAPPORTI_UTENZA.IMP_CANONE_INIZIALE,COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,siscom_mi.unita_immobiliari WHERE UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND unita_contrattuale.id_unita = unita_immobiliari.ID AND UNITA_CONTRATTUALE.id_unita_principale IS NULL AND ID_CONTRATTO=" & id_contr.Value
            Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX1.Read Then
                IDUNITA = myReaderX1("ID_UNITA")
                ID_AU = par.IfNull(myReaderX1("ID_AU"), 0)
                idContratto = par.IfNull(myReaderX1("ID"), -1)
                canoneIniziale = par.IfNull(myReaderX1("IMP_CANONE_INIZIALE"), 0)
                tipoContrattoLoc = par.IfNull(myReaderX1("COD_TIPOLOGIA_CONTR_LOC"), "")
            End If
            myReaderX1.Close()

            If IDUNITA <> 0 Then

                par.cmd.CommandText = "SELECT DATA_EVENTO,DATA_INIZIO_VAL,DATA_FINE_VAL,DICHIARAZIONI_VSA.ID_STATO,DICHIARAZIONI_VSA.ID,DOMANDE_BANDO_VSA.REDDITI_PRE_RECA FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID=" & new_id_dom
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    ANNO_INIZIO = CInt(Mid(par.IfNull(myReader("DATA_INIZIO_VAL"), Year(Now)), 1, 4))

                    dataInizioValidita = par.IfNull(myReader("DATA_INIZIO_VAL"), "")
                    IDdich = par.IfNull(myReader("ID"), "")

                    If par.IfNull(myReader("DATA_FINE_VAL"), "") = "29991231" Then
                        dataFine = Year(Now) & "1231"
                    Else
                        dataFine = par.IfNull(myReader("DATA_FINE_VAL"), "")
                    End If
                    PER_ANNI = DateDiff(DateInterval.Year, CDate(par.FormattaData(myReader("DATA_INIZIO_VAL"))), CDate(par.FormattaData(dataFine)))

                    'ANNICONGUAGLIO = ANNO_INIZIO + PER_ANNI

                    'If causale.Value = "28" Then
                    '    ANNICONGUAGLIO = Year(Now)
                    'End If
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & ANNO_INIZIO & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & ANNO_INIZIO & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                Dim myReaderCEC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCEC.HasRows = True Then
                    If myReaderCEC.Read Then
                        If par.IfNull(myReaderCEC("tipo_provenienza"), 0) = 1 Then
                            Prov = 3
                        Else
                            Prov = 0
                        End If
                        id_dom = par.IfNull(myReaderCEC("id_dichiarazione"), 0)
                    End If
                End If
                myReaderCEC.Close()

                For I = ANNO_INIZIO To ANNO_INIZIO + PER_ANNI
                    CanonePREreca = 0
                    parte2 = ""
                    parte3 = ""
                    parte4 = ""

                    'If id_dom <> 0 And I = ANNO_INIZIO Then
                    '    par.CalcolaCanone27RECA(id_dom, Prov, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                    '    parte3new = parte3
                    '    parte2new = parte2
                    'End If

                    LOCATIVO = ""
                    comunicazioni = ""

                    sISEE = ""
                    sISE = ""
                    sISR = ""
                    sISP = ""
                    sVSE = ""
                    sREDD_DIP = ""
                    sREDD_ALT = ""
                    sLimitePensione = ""
                    sPER_VAL_LOC = ""
                    sPERC_INC_MAX_ISE_ERP = ""
                    sCANONE_MIN = ""
                    sISE_MIN = ""
                    sCanone = ""
                    sNOTE = ""
                    sDEM = ""
                    sSUPCONVENZIONALE = ""
                    sCOSTOBASE = ""
                    sZONA = ""
                    sMOTIVODECADENZA = ""

                    If I = 2008 Or I = 2009 Then
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.RAPPORTI_UTENZA_EXTRA where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "')"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.Read Then
                            parte4 = vbCrLf & vbCrLf & "CANONE ANNO " & I & vbCrLf

                            CanonePREreca = par.IfNull(myReaderRX("IMP_ANN_CANONE_A_REGIME_" & I & ""), 0)
                            'TOLTO COME DA ISTRUZIONI 11/06/2012
                            'If I = 2009 Then
                            '    If par.IfNull(myReaderRX("FASCIA_ECONOMICA_2009_LR36"), "") >= 12 And par.IfNull(myReaderRX("FASCIA_ECONOMICA_2009_LR36"), "") < 27 Then
                            '        CanonePREreca = CanonePREreca + ((CanonePREreca * CDbl(istat2009)) / 100)
                            '    End If
                            'End If
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(CanonePREreca), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_CANONE_A_REGIME_" & I & ""), 0) / 12), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE TRANSITORIO:..........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_CANONE_TRANSITORIO"), 0)), "##,##0.00")
                            If I = 2008 Then
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE GRADUATO " & I & ":........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_PRIMO_ANNO"), 0)), "##,##0.00")
                            Else
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE GRADUATO " & I & ":........................." & Format(CDec(par.IfNull(myReaderRX("IMP_ANN_SECONDO_ANNO"), 0)), "##,##0.00")
                            End If
                        Else
                            importoTrovato = False
                        End If
                        myReaderRX.Close()

                        If I <= 2012 Then
                            par.cmd.CommandText = "SELECT CANONE_COMPETENZA_" & I & " FROM SISCOM_MI.ELABORAZIONE_CONGUAGLI WHERE COD_CONTRATTO='" & CodContratto.Value & "'"
                            Dim myReaderCanone As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderCanone.Read Then
                                If par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0) <> 0 Then
                                    parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderCanone("CANONE_COMPETENZA_" & I & ""), 0)), "##,##0.00")
                                End If
                            End If
                            myReaderCanone.Close()
                        End If

                    End If


                    If I = 2010 Or I = 2011 Then
                        'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.HasRows = True Then


                            If myReaderRX.Read Then
                                parte4 = ""
                                idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                                'If idDichCan_EC <> 0 Then
                                'If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 2 Then
                                '    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                'Else
                                '    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                '    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderID.Read Then
                                '        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                '    End If
                                '    myReaderID.Close()

                                '    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                '    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                '    & "525,10001,10002,30003,530," _
                                '    & "30075,1,10072,10087,10125," _
                                '    & "10135,20003,20019,20020," _
                                '    & "20023,20096,20097,553," _
                                '    & "10075,10128,20021,10127," _
                                '    & "10126,512,10074,534,10073," _
                                '    & "604,30071,603,30068,506," _
                                '    & "647,653,599,648,30080,622," _
                                '    & "30123,30124,508,10160,509," _
                                '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                '    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderComp.Read Then
                                '        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                '    End If
                                '    myReaderComp.Close()

                                'End If
                                'CanonePREreca = IMPORTO
                                'Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                If parte3new = "" Then
                                    parte3new = parte3new & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP REDDITI " & I & "" & vbCrLf
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. ................................................." & par.IfNull(myReaderRX("NUM_COMP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & par.IfNull(myReaderRX("MINORI_15"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & par.IfNull(myReaderRX("MAGGIORI_65"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & par.IfNull(myReaderRX("NUM_COMP_66"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & par.IfNull(myReaderRX("NUM_COMP_100"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & par.IfNull(myReaderRX("NUM_COMP_100_CON"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI_FRAGILITA"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDec(par.IfNull(myReaderRX("REDD_MOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDec(par.IfNull(myReaderRX("REDD_IMMOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDec(par.IfNull(myReaderRX("REDD_COMPLESSIVO"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(CDec(par.IfNull(myReaderRX("ISEE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISR:......................................................" & par.IfNull(myReaderRX("ISR"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "ISP:......................................................" & par.IfNull(myReaderRX("ISP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "VSE:......................................................" & par.IfNull(myReaderRX("VSE"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_DIP"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_ATRI"), 0)), "##,##0.00")
                                    If par.IfNull(myReaderRX("REDD_PREV_DIP"), 0) = 0 Then
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                                    Else
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                                    End If
                                    parte3new = parte3new & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(CDec(par.IfNull(myReaderRX("LIMITE_PENSIONI"), 0)), "##,##0.00")
                                End If
                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                                'End If
                            Else
                                'parte4 = ""
                                'If ID_AU <> 0 Then
                                '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)
                                '    CanonePREreca = IMPORTO
                                'Else
                                '    importoTrovato = False
                                'End If
                            End If
                        Else
                            CanonePREreca = canoneIniziale

                            parte4 = ""
                            parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")


                            'par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                            '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                            '    & "525,10001,10002,30003,530," _
                            '    & "30075,1,10072,10087,10125," _
                            '    & "10135,20003,20019,20020," _
                            '    & "20023,20096,20097,553," _
                            '    & "10075,10128,20021,10127," _
                            '    & "10126,512,10074,534,10073," _
                            '    & "604,30071,603,30068,506," _
                            '    & "647,653,599,648,30080,622," _
                            '    & "30123,30124,508,10160,509," _
                            '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                            '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                            '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                            'Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'If myReaderComp.Read Then
                            '    parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                            'End If
                            'myReaderComp.Close()

                        End If
                        myReaderRX.Close()
                    End If



                    If I = 2012 Or I = 2013 Then
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.HasRows = True Then
                            If myReaderRX.Read Then
                                parte4 = ""
                                idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                                'If idDichCan_EC <> 0 Then
                                'If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 5 Then
                                '    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                'Else
                                '    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                '    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderID.Read Then
                                '        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                '    End If
                                '    myReaderID.Close()
                                '    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)

                                '    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                '    & "525,10001,10002,30003,530," _
                                '    & "30075,1,10072,10087,10125," _
                                '    & "10135,20003,20019,20020," _
                                '    & "20023,20096,20097,553," _
                                '    & "10075,10128,20021,10127," _
                                '    & "10126,512,10074,534,10073," _
                                '    & "604,30071,603,30068,506," _
                                '    & "647,653,599,648,30080,622," _
                                '    & "30123,30124,508,10160,509," _
                                '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                '    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderComp.Read Then
                                '        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                '    End If
                                '    myReaderComp.Close()
                                'End If
                                'CanonePREreca = IMPORTO
                                'parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                If parte3new = "" Then
                                    parte3new = parte3new & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP REDDITI " & I & "" & vbCrLf
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. ................................................." & par.IfNull(myReaderRX("NUM_COMP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & par.IfNull(myReaderRX("MINORI_15"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & par.IfNull(myReaderRX("MAGGIORI_65"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & par.IfNull(myReaderRX("NUM_COMP_66"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & par.IfNull(myReaderRX("NUM_COMP_100"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & par.IfNull(myReaderRX("NUM_COMP_100_CON"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI_FRAGILITA"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDec(par.IfNull(myReaderRX("REDD_MOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDec(par.IfNull(myReaderRX("REDD_IMMOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDec(par.IfNull(myReaderRX("REDD_COMPLESSIVO"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(CDec(par.IfNull(myReaderRX("ISEE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISR:......................................................" & par.IfNull(myReaderRX("ISR"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "ISP:......................................................" & par.IfNull(myReaderRX("ISP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "VSE:......................................................" & par.IfNull(myReaderRX("VSE"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_DIP"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_ATRI"), 0)), "##,##0.00")
                                    If par.IfNull(myReaderRX("REDD_PREV_DIP"), 0) = 0 Then
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                                    Else
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                                    End If
                                    parte3new = parte3new & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(CDec(par.IfNull(myReaderRX("LIMITE_PENSIONI"), 0)), "##,##0.00")
                                End If

                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                                'End If
                            Else
                                'parte4 = ""
                                'If ID_AU <> 0 Then
                                '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                                '    CanonePREreca = IMPORTO
                                '    parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                '    importoTrovato = False
                                'End If
                            End If
                        Else
                            CanonePREreca = canoneIniziale
                            parte4 = ""
                            parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                            'If ID_AU <> 0 Then
                            'par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                            par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                & "525,10001,10002,30003,530," _
                                & "30075,1,10072,10087,10125," _
                                & "10135,20003,20019,20020," _
                                & "20023,20096,20097,553," _
                                & "10075,10128,20021,10127," _
                                & "10126,512,10074,534,10073," _
                                & "604,30071,603,30068,506," _
                                & "647,653,599,648,30080,622," _
                                & "30123,30124,508,10160,509," _
                                & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                            Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderComp.Read Then
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                            End If
                            myReaderComp.Close()

                            'Else
                            'importoTrovato = False
                            'End If
                        End If
                        myReaderRX.Close()
                    End If

                    If I = 2014 Or I = 2015 Then
                        'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.HasRows = True Then
                            If myReaderRX.Read Then
                                parte4 = ""
                                idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                                'If idDichCan_EC <> 0 Then
                                'If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 8 Then
                                '    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                'Else
                                '    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                '    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderID.Read Then
                                '        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                '    End If
                                '    myReaderID.Close()
                                '    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)

                                '    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                '    & "525,10001,10002,30003,530," _
                                '    & "30075,1,10072,10087,10125," _
                                '    & "10135,20003,20019,20020," _
                                '    & "20023,20096,20097,553," _
                                '    & "10075,10128,20021,10127," _
                                '    & "10126,512,10074,534,10073," _
                                '    & "604,30071,603,30068,506," _
                                '    & "647,653,599,648,30080,622," _
                                '    & "30123,30124,508,10160,509," _
                                '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                '    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderComp.Read Then
                                '        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                '    End If
                                '    myReaderComp.Close()
                                'End If
                                'CanonePREreca = IMPORTO
                                'parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")

                                If parte3new = "" Then
                                    parte3new = parte3new & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP REDDITI " & I & "" & vbCrLf
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. ................................................." & par.IfNull(myReaderRX("NUM_COMP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & par.IfNull(myReaderRX("MINORI_15"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & par.IfNull(myReaderRX("MAGGIORI_65"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & par.IfNull(myReaderRX("NUM_COMP_66"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & par.IfNull(myReaderRX("NUM_COMP_100"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & par.IfNull(myReaderRX("NUM_COMP_100_CON"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI_FRAGILITA"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDec(par.IfNull(myReaderRX("REDD_MOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDec(par.IfNull(myReaderRX("REDD_IMMOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDec(par.IfNull(myReaderRX("REDD_COMPLESSIVO"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(CDec(par.IfNull(myReaderRX("ISEE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISR:......................................................" & par.IfNull(myReaderRX("ISR"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "ISP:......................................................" & par.IfNull(myReaderRX("ISP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "VSE:......................................................" & par.IfNull(myReaderRX("VSE"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_DIP"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_ATRI"), 0)), "##,##0.00")
                                    If par.IfNull(myReaderRX("REDD_PREV_DIP"), 0) = 0 Then
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                                    Else
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                                    End If
                                    parte3new = parte3new & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(CDec(par.IfNull(myReaderRX("LIMITE_PENSIONI"), 0)), "##,##0.00")
                                End If

                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                                'End If
                            Else
                                'parte4 = ""
                                'If ID_AU <> 0 Then
                                '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                                '    CanonePREreca = IMPORTO
                                '    parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                '    importoTrovato = False
                                'End If
                            End If
                        Else
                            CanonePREreca = canoneIniziale
                            parte4 = ""
                            parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                            'If ID_AU <> 0 Then
                            '''''''''par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                            par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                & "525,10001,10002,30003,530," _
                                & "30075,1,10072,10087,10125," _
                                & "10135,20003,20019,20020," _
                                & "20023,20096,20097,553," _
                                & "10075,10128,20021,10127," _
                                & "10126,512,10074,534,10073," _
                                & "604,30071,603,30068,506," _
                                & "647,653,599,648,30080,622," _
                                & "30123,30124,508,10160,509," _
                                & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                            Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderComp.Read Then
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                            End If
                            myReaderComp.Close()

                            'Else
                            'importoTrovato = False
                            'End If
                        End If
                        myReaderRX.Close()
                    End If

                    If I >= 2016 Then
                        'par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') and ID_BANDO_AU = 2 AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        par.cmd.CommandText = "SELECT * from SISCOM_MI.CANONI_EC where ID_CONTRATTO IN (SELECT ID from SISCOM_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "') AND SUBSTR(INIZIO_VALIDITA_CAN,1,4)<='" & I & "' AND SUBSTR(FINE_VALIDITA_CAN,1,4)>='" & I & "' AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1) ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderRX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRX.HasRows = True Then
                            If myReaderRX.Read Then
                                parte4 = ""
                                idDichCan_EC = par.IfNull(myReaderRX("ID_DICHIARAZIONE"), 0)
                                'If idDichCan_EC <> 0 Then
                                'If par.IfNull(myReaderRX("TIPO_PROVENIENZA"), "") = 10 Then
                                '    par.CalcolaCanone27RECA(idDichCan_EC, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                                'Else
                                '    par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & idDichCan_EC
                                '    Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderID.Read Then
                                '        idDOMCan_EC = par.IfNull(myReaderID("ID"), -1)
                                '    End If
                                '    myReaderID.Close()
                                '    par.CalcolaCanone27RECA(idDOMCan_EC, 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)

                                '    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                '    & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                '    & "525,10001,10002,30003,530," _
                                '    & "30075,1,10072,10087,10125," _
                                '    & "10135,20003,20019,20020," _
                                '    & "20023,20096,20097,553," _
                                '    & "10075,10128,20021,10127," _
                                '    & "10126,512,10074,534,10073," _
                                '    & "604,30071,603,30068,506," _
                                '    & "647,653,599,648,30080,622," _
                                '    & "30123,30124,508,10160,509," _
                                '    & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                '    & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                '    & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                                '    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                '    If myReaderComp.Read Then
                                '        parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                                '    End If
                                '    myReaderComp.Close()
                                'End If
                                'CanonePREreca = IMPORTO
                                'parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                                Select Case par.IfNull(myReaderRX("ID_AREA_ECONOMICA"), -1)
                                    Case 1
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                                    Case 2
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                                    Case 3
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                                    Case 4
                                        parte4 = parte4 & vbCrLf & vbTab & "Area:.....................................................DECADENZA"
                                End Select
                                parte4 = parte4 & vbCrLf & vbTab & "Fascia:..................................................." & par.IfNull(myReaderRX("SOTTO_AREA"), "")
                                parte4 = parte4 & vbCrLf & vbTab & "ISEE-ERP L.R 27:.........................................." & Format(CDec(par.IfNull(myReaderRX("ISEE_27"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "ISE-ERP L.R 27:..........................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "PERCENTUALE DEL VALORE LOCATIVO:.........................." & par.IfNull(myReaderRX("PERC_VAL_LOC"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "INCIDENZA PERCENTUALE MASSIMA SU ISE-ERP:................." & par.IfNull(myReaderRX("INC_MAX"), 0) & "%"
                                parte4 = parte4 & vbCrLf & vbTab & "VALORE INCIDENZA SU ISE-ERP:.............................." & Format(CDec(par.IfNull(myReaderRX("INCIDENZA_ISE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "COEFFICIENTE PER NUCLEI FAMILIARI:........................" & par.IfNull(myReaderRX("COEFF_NUCLEO_FAM"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE MINIMO MENSILE....................................:" & Format(CDec(par.IfNull(myReaderRX("CANONE_MINIMO_AREA"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE:............................................" & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "% ISTAT APPLICATA CANONE CLASSE:.........................." & par.IfNull(myReaderRX("PERC_ISTAT_APPLICATA"), 0)
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE CLASSE CON ISTAT:.................................." & Format(CDec(par.IfNull(myReaderRX("CANONE_CLASSE_ISTAT"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")
                                parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(CDec(par.IfNull(myReaderRX("CANONE"), 0) / 12), "##,##0.00")
                                CanonePREreca = Format(CDec(par.IfNull(myReaderRX("CANONE"), 0)), "##,##0.00")


                                If parte3new = "" Then
                                    parte3new = parte3new & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP REDDITI " & I & "" & vbCrLf
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. ................................................." & par.IfNull(myReaderRX("NUM_COMP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & par.IfNull(myReaderRX("MINORI_15"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & par.IfNull(myReaderRX("MAGGIORI_65"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & par.IfNull(myReaderRX("NUM_COMP_66"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & par.IfNull(myReaderRX("NUM_COMP_100"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & par.IfNull(myReaderRX("NUM_COMP_100_CON"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDec(par.IfNull(myReaderRX("DETRAZIONI_FRAGILITA"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDec(par.IfNull(myReaderRX("REDD_MOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDec(par.IfNull(myReaderRX("REDD_IMMOBILIARI"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDec(par.IfNull(myReaderRX("REDD_COMPLESSIVO"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(CDec(par.IfNull(myReaderRX("ISEE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(CDec(par.IfNull(myReaderRX("ISE"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "ISR:......................................................" & par.IfNull(myReaderRX("ISR"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "ISP:......................................................" & par.IfNull(myReaderRX("ISP"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "VSE:......................................................" & par.IfNull(myReaderRX("VSE"), 0)
                                    parte3new = parte3new & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_DIP"), 0)), "##,##0.00")
                                    parte3new = parte3new & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(CDec(par.IfNull(myReaderRX("REDDITI_ATRI"), 0)), "##,##0.00")
                                    If par.IfNull(myReaderRX("REDD_PREV_DIP"), 0) = 0 Then
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                                    Else
                                        parte3new = parte3new & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                                    End If
                                    parte3new = parte3new & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(CDec(par.IfNull(myReaderRX("LIMITE_PENSIONI"), 0)), "##,##0.00")
                                End If

                                annotazioni = par.IfNull(myReaderRX("ANNOTAZIONI"), "")
                                If par.IfNull(myReaderRX("ANNOTAZIONI"), "") <> "" Then
                                    parte4 = parte4 & vbCrLf & vbCrLf & vbTab & "ANNOTAZIONI:"
                                    parte4 = parte4 & vbCrLf & vbTab & Replace(par.IfNull(myReaderRX("ANNOTAZIONI"), ""), "/", vbCrLf)
                                End If
                                'End If
                            Else
                                'parte4 = ""
                                'If ID_AU <> 0 Then
                                '    par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2011)
                                '    CanonePREreca = IMPORTO
                                '    parte4 = Replace(parte4, Mid(parte4, 36, 4), I)
                                'Else
                                '    importoTrovato = False
                                'End If
                            End If
                        Else
                            CanonePREreca = canoneIniziale
                            parte4 = ""
                            parte4 = parte4 & vbCrLf & vbCrLf & "DETERMINAZIONE DEL CANONE ANNO " & I & vbCrLf
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO ANN.CANONE A REGIME:.............................." & Format(par.IfNull(CanonePREreca, 0), "##,##0.00")
                            parte4 = parte4 & vbCrLf & vbTab & "IMPORTO MENSILE CANONE A REGIME:.........................." & Format(par.IfNull(CanonePREreca, 0) / 12, "##,##0.00")

                            'If ID_AU <> 0 Then
                            '''''''''par.CalcolaCanone27RECA(ID_AU, 0, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransit, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, 2010)

                            par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID " _
                                & "AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.ID IN (" _
                                & "525,10001,10002,30003,530," _
                                & "30075,1,10072,10087,10125," _
                                & "10135,20003,20019,20020," _
                                & "20023,20096,20097,553," _
                                & "10075,10128,20021,10127," _
                                & "10126,512,10074,534,10073," _
                                & "604,30071,603,30068,506," _
                                & "647,653,599,648,30080,622," _
                                & "30123,30124,508,10160,509," _
                                & "10161,10162,30081,575,650,686,687,688,689,690,691,36,10003,701,702,703,704,705) " _
                                & "AND RIFERIMENTO_DA<='" & I & "1231" & "' AND RIFERIMENTO_A>='" & I & "0101" & "' AND ID_TIPO<>5 AND ID_TIPO<>4 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0)) " _
                                & "AND ID_CONTRATTO=" & idContratto & " ORDER BY RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC"
                            Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderComp.Read Then
                                parte4 = parte4 & vbCrLf & vbTab & "CANONE COMPETENZA " & I & ":..................................." & Format(CDec(par.IfNull(myReaderComp("imp_EMESSO"), 0)), "##,##0.00")
                            End If
                            myReaderComp.Close()

                            'Else
                            'importoTrovato = False
                            'End If
                        End If
                        myReaderRX.Close()
                    End If

                    Dim numparte As String = ""
                    Dim testo As String = ""
                    For j As Integer = 0 To 3
                        numparte = j + 1
                        Select Case j
                            Case 0
                                testo = parte1
                            Case 1
                                testo = parte2new
                            Case 2
                                If parte3new = "" Then
                                    If idDichCan_EC = 0 Then
                                        If annotazioni <> "" Then
                                            parte3new = "<< Dati reddituali non importati per " & LCase(par.PulisciStrSql(annotazioni)) & " >>"
                                        Else
                                            parte3new = "<< Dati reddituali non importati da precedenti istanze >>"
                                        End If
                                    Else
                                        parte3new = "<< Dati reddituali non importati da precedenti istanze >>"
                                    End If
                                End If
                                testo = parte3new
                            Case 3
                                testo = parte4
                        End Select

                        If importoTrovato = True Then
                            par.cmd.CommandText = "INSERT INTO CANONI_PRE_RECA (ID_DOMANDA,ANNO_RIFERIMENTO,TESTO_CANONE,NUM_PARTE,IMPORTO) VALUES (" & new_id_dom & "," & I & ",'" & par.PulisciStrSql(testo) & "','" & numparte & "'," & par.VirgoleInPunti(Format(CanonePREreca, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    Next
                Next
            End If

            Dim strScript As String = ""
            If tipoContrattoLoc = "ERP" Then
                par.cmd.CommandText = "SELECT * FROM CANONI_PRE_RECA WHERE ID_DOMANDA = " & new_id_dom
                Dim myReaderDelete As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderDelete.Read = False Then
                    par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA WHERE ID =" & new_id_dom
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "DELETE FROM DICHIARAZIONI_VSA WHERE ID =" & new_idDichia
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Impossibile procedere. Nessuna situazione pre-reca è stata memorizzata!')</script>")
                Else
                    strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                    & "else{window.close();}</script>"
                    Response.Write(strScript)
                End If
                myReaderDelete.Close()
            Else
                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');window.close();if (conf){window.open('../NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & new_idDichia & "&CH=1&ANNI=" & anni & "','Dettagli','top=200,left=350,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');}" _
                    & "else{window.close();}</script>"
                Response.Write(strScript)
            End If



            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET CANONE_PRE_RECA = " & par.VirgoleInPunti(Format(CanonePREreca, "0.00")) & " WHERE ID = " & new_id_dom
            'par.cmd.ExecuteNonQuery()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub


    Private Function RicavaStampaPreRECA() As String

        Dim StringaFile As String = ""
        Dim sNUMCOMP As String = ""
        Dim sMINORI15 As String = ""
        Dim sMAGGIORI65 As String = ""
        Dim sNUMCOMP66 As String = ""
        Dim sNUMCOMP100 As String = ""
        Dim sNUMCOMP100C As String = ""
        Dim sDETRAZIONI As String = ""
        Dim sDETRAZIONIF As String = ""
        Dim sMOBILIARI As String = ""
        Dim sIMMOBILIARI As String = ""
        Dim sCOMPLESSIVO As String = ""

        Dim ISEE As Double = 0
        Dim VSE As Double = 0
        Dim ISE As Double = 0
        Dim ISR As Double = 0
        Dim ISP As Double = 0
        Dim REDD_DIP As Double = 0
        Dim REDD_ALT As Double = 0

        Dim DEM As Double = 0
        Dim SUP_CONVENZIONALE As Double = 0
        Dim COSTO_BASE_MC As Double = 0
        Dim ZONA As Double = 0
        Dim PIANO As Double = 0
        Dim CONSERVAZIONE As Double = 0
        Dim VETUSTA As Double = 0
        Dim VALORECONVENZIONALE As Double = 0

        Dim UnitaVenduta As Boolean = False

        Dim PSE As Double = 0
        Dim totS As Double = 0

        Dim TotDetrazioni As Double = 0
        Dim TotMobiliari As Double = 0
        Dim TotImmobiliari As Double = 0
        Dim REDDITO_COMPLESSIVO As Double = 0
        Dim DETRAZIONI_F As Double = 0

        Dim NumComponenti As Integer = 0
        Dim IdDichiarazione As Long
        Dim IdDomanda As Long
        Dim Minori65 As Boolean = False
        Dim Note As String = ""
        Dim BUONO As Boolean = True

        Dim COEF_NUCLEO_FAM As Double = 0
        Dim Decadenza As String = "0"
        Dim Ise_erp_new As Double = 0
        Dim Disabilita66 As Boolean = False
        Dim DataDecorrenzaContratto As String = ""
        Dim StatoDichiarazione As Integer = 1

        Dim NUM66 As Integer = 0
        Dim NUM99 As Integer = 0
        Dim NUM100 As Integer = 0
        Dim NUM100C As Integer = 0

        Dim ANNO_SIT_ECONOMICA As Integer
        Dim sANNOINIZIOVAL As String = ""
        Dim sANNOFINEVAL As String = ""
        Dim DATA_EVENTO As String = ""
        Dim MINORI15 As Integer = 0
        Dim MAGGIORI15 As Integer = 0
        Dim MAGGIORI65 As Integer = 0
        Dim sREDD_DIP As Double
        Dim sREDD_ALT As Double

        Dim sISEE As String = ""
        Dim sISE As String = ""
        Dim sISR As String = ""
        Dim sISP As String = ""
        Dim sVSE As String = ""
        Dim sPSE As String = ""

        Dim sMOTIVODECADENZA As String = ""
        Dim sALLOGGIOIDONEO As String = ""
        Dim InizioUltimaArea As String = ""
        Dim sSOTTOAREA As String = ""
        Dim sVALOCIICI As String = ""
        Dim LimitePensioni As Double = 0
        Dim codContratto As String = ""

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Select Case importRedd.Value
                Case 0 'ANAGRAFE UTENZA
                    par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & id_dichia
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        IdDichiarazione = par.IfNull(myReaderX("ID"), 0)
                        'IdDomanda = par.IfNull(myReaderX("ID"), 0)
                        codContratto = par.IfNull(myReaderX("RAPPORTO"), "")
                        ISEE = par.IfNull(myReaderX("ISEE"), 0)
                        ISE = par.IfNull(myReaderX("ISE_ERP"), 0)
                        ISR = par.IfNull(myReaderX("ISR_ERP"), 0)
                        ISP = par.IfNull(myReaderX("ISP_ERP"), 0)
                        PSE = par.IfNull(myReaderX("PSE"), 1)
                        VSE = par.IfNull(myReaderX("VSE"), 1)

                        'If ANNO_RIFERIMENTO <> 0 Then
                        '    DATA_EVENTO = ANNO_RIFERIMENTO
                        'Else
                        DATA_EVENTO = par.IfNull(myReaderX("DATA_INIZIO_VAL"), Format(Now, "yyyyMMdd"))
                        'End If

                    End If
                    myReaderX.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & IdDichiarazione
                    myReaderX = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        ANNO_SIT_ECONOMICA = par.IfNull(myReaderX("ANNO_SIT_ECONOMICA"), "2009")
                        sANNOINIZIOVAL = par.IfNull(myReaderX("data_inizio_val"), "2010")
                        sANNOFINEVAL = par.IfNull(myReaderX("data_fine_val"), "2011")
                        StatoDichiarazione = par.IfNull(myReaderX("id_stato"), 1)
                    End If
                    myReaderX.Close()


                    par.cmd.CommandText = "select sum(dipendente+non_imponibili+pensione) from sepa.UTENZA_REDDITI where ID_UTENZA=" & IdDichiarazione
                    Dim myReaderW As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_DIP = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    par.cmd.CommandText = "select sum(autonomo+dom_ag_fab+occasionali) from sepa.UTENZA_REDDITI where ID_UTENZA=" & IdDichiarazione
                    myReaderW = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_ALT = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    Disabilita66 = False
                    Minori65 = False

                    par.cmd.CommandText = "select * from SEPA.UTENZA_COMP_NUCLEO where id_DICHIARAZIONE=" & IdDichiarazione
                    myReaderW = par.cmd.ExecuteReader()
                    Do While myReaderW.Read
                        NumComponenti = NumComponenti + 1
                        If Minori65 = False Then
                            If par.RicavaEta(par.IfNull(myReaderW("data_nascita"), "")) < 65 Then
                                Minori65 = True
                            End If
                        End If

                        If par.RicavaEtaChiusura(par.FormattaData(par.IfNull(myReaderW("data_nascita"), "")), ANNO_SIT_ECONOMICA & "1231") < 15 Then
                            MINORI15 = MINORI15 + 1
                        End If

                        If par.RicavaEtaChiusura(par.FormattaData(par.IfNull(myReaderW("data_nascita"), "")), ANNO_SIT_ECONOMICA & "1231") > 65 Then
                            MAGGIORI65 = MAGGIORI65 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) > 66 Then
                            Disabilita66 = True
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) >= 66 And par.IfNull(myReaderW("perc_inval"), 0) <= 99 Then
                            NUM66 = NUM66 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) = 100 And par.IfNull(myReaderW("INDENNITA_ACC"), "0") = "0" Then
                            NUM100 = NUM100 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) = 100 And par.IfNull(myReaderW("INDENNITA_ACC"), "0") = "1" Then
                            NUM100C = NUM100C + 1
                        End If


                        par.cmd.CommandText = "select sum(valore) from SEPA.utenza_comp_patr_immob where id_componente=" & myReaderW("id") & " and id_tipo=0"
                        Dim myReaderQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderQ.Read Then
                            totS = totS + par.IfNull(myReaderQ(0), 0)
                        End If
                        myReaderQ.Close()


                        par.cmd.CommandText = "select SUM(importo) from SEPA.UTENZA_COMP_DETRAZIONI where id_componente=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        If myReaderQ.Read Then
                            TotDetrazioni = TotDetrazioni + par.IfNull(myReaderQ(0), 0)
                        End If
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            TotMobiliari = TotMobiliari + par.IfNull(myReaderQ("IMPORTO"), 0)
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            TotImmobiliari = TotImmobiliari + (par.IfNull(myReaderQ("VALORE"), 0) - par.IfNull(myReaderQ("MUTUO"), 0))
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderQ("REDDITO_IRPEF"), 0) + par.IfNull(myReaderQ("PROV_AGRARI"), 0)
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderQ("IMPORTO"), 0)
                        End While
                        myReaderQ.Close()


                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        If myReaderQ.HasRows Then
                            While myReaderQ.Read
                                DETRAZIONI_F = DETRAZIONI_F + par.IfNull(myReaderQ("IMPORTO"), 0)
                            End While

                        Else
                            If par.IfNull(myReaderW("indennita_acc"), 0) = "1" Then
                                DETRAZIONI_F = DETRAZIONI_F + 10000
                            End If

                        End If
                        myReaderQ.Close()

                        If DETRAZIONI_F < 10000 And NUM100C > 0 Then
                            DETRAZIONI_F = 10000
                        End If

                    Loop
                    myReaderW.Close()

                    DETRAZIONI_F = DETRAZIONI_F + (NUM100 * 3000) + (NUM66 * 1500)

                    sREDD_DIP = REDD_DIP
                    sREDD_ALT = REDD_ALT

                Case 1 'GESTIONE LOCATARI
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.* FROM DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND ID_DICHIARAZIONE=" & id_dichia
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        IdDichiarazione = par.IfNull(myReaderX("ID_DICHIARAZIONE"), 0)
                        IdDomanda = par.IfNull(myReaderX("ID"), 0)
                        codContratto = par.IfNull(myReaderX("CONTRATTO_NUM"), "")
                        ISEE = par.IfNull(myReaderX("REDDITO_ISEE"), 0)
                        ISE = par.IfNull(myReaderX("ISE_ERP"), 0)
                        ISR = par.IfNull(myReaderX("ISR_ERP"), 0)
                        ISP = par.IfNull(myReaderX("ISP_ERP"), 0)
                        PSE = par.IfNull(myReaderX("PSE"), 1)
                        VSE = par.IfNull(myReaderX("VSE"), 1)

                        'If ANNO_RIFERIMENTO <> 0 Then
                        '    DATA_EVENTO = ANNO_RIFERIMENTO
                        'Else
                        DATA_EVENTO = par.IfNull(myReaderX("DATA_EVENTO"), Format(Now, "yyyyMMdd"))
                        'End If

                    End If
                    myReaderX.Close()

                    par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & IdDichiarazione
                    myReaderX = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        ANNO_SIT_ECONOMICA = par.IfNull(myReaderX("ANNO_SIT_ECONOMICA"), "2009")
                        sANNOINIZIOVAL = par.IfNull(myReaderX("data_inizio_val"), "2010")
                        sANNOFINEVAL = par.IfNull(myReaderX("data_fine_val"), "2011")
                        StatoDichiarazione = par.IfNull(myReaderX("id_stato"), 1)
                    End If
                    myReaderX.Close()


                    par.cmd.CommandText = "select sum(dipendente+non_imponibili+pensione) from sepa.DOMANDE_REDDITI_VSA where ID_DOMANDA=" & IdDichiarazione
                    Dim myReaderW As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_DIP = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    par.cmd.CommandText = "select sum(autonomo+dom_ag_fab+occasionali) from sepa.DOMANDE_REDDITI_VSA where ID_DOMANDA=" & IdDichiarazione
                    myReaderW = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_ALT = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    Disabilita66 = False
                    Minori65 = False

                    par.cmd.CommandText = "select * from SEPA.COMP_NUCLEO_VSA where id_DICHIARAZIONE=" & IdDichiarazione
                    myReaderW = par.cmd.ExecuteReader()
                    Do While myReaderW.Read
                        NumComponenti = NumComponenti + 1
                        If Minori65 = False Then
                            If par.RicavaEta(par.IfNull(myReaderW("data_nascita"), "")) < 65 Then
                                Minori65 = True
                            End If
                        End If

                        If par.RicavaEtaChiusura(par.FormattaData(par.IfNull(myReaderW("data_nascita"), "")), ANNO_SIT_ECONOMICA & "1231") < 15 Then
                            MINORI15 = MINORI15 + 1
                        End If

                        If par.RicavaEtaChiusura(par.FormattaData(par.IfNull(myReaderW("data_nascita"), "")), ANNO_SIT_ECONOMICA & "1231") > 65 Then
                            MAGGIORI65 = MAGGIORI65 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) > 66 Then
                            Disabilita66 = True
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) >= 66 And par.IfNull(myReaderW("perc_inval"), 0) <= 99 Then
                            NUM66 = NUM66 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) = 100 And par.IfNull(myReaderW("INDENNITA_ACC"), "0") = "0" Then
                            NUM100 = NUM100 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) = 100 And par.IfNull(myReaderW("INDENNITA_ACC"), "0") = "1" Then
                            NUM100C = NUM100C + 1
                        End If


                        par.cmd.CommandText = "select sum(valore) from SEPA.comp_patr_immob_VSA where id_componente=" & myReaderW("id") & " and id_tipo=0"
                        Dim myReaderQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderQ.Read Then
                            totS = totS + par.IfNull(myReaderQ(0), 0)
                        End If
                        myReaderQ.Close()


                        par.cmd.CommandText = "select SUM(importo) from SEPA.COMP_DETRAZIONI_VSA where id_componente=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        If myReaderQ.Read Then
                            TotDetrazioni = TotDetrazioni + par.IfNull(myReaderQ(0), 0)
                        End If
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            TotMobiliari = TotMobiliari + par.IfNull(myReaderQ("IMPORTO"), 0)
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_VSA WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            TotImmobiliari = TotImmobiliari + (par.IfNull(myReaderQ("VALORE"), 0) - par.IfNull(myReaderQ("MUTUO"), 0))
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderQ("REDDITO_IRPEF"), 0) + par.IfNull(myReaderQ("PROV_AGRARI"), 0)
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderQ("IMPORTO"), 0)
                        End While
                        myReaderQ.Close()


                        par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        If myReaderQ.HasRows Then
                            While myReaderQ.Read
                                DETRAZIONI_F = DETRAZIONI_F + par.IfNull(myReaderQ("IMPORTO"), 0)
                            End While

                        Else
                            If par.IfNull(myReaderW("indennita_acc"), 0) = "1" Then
                                DETRAZIONI_F = DETRAZIONI_F + 10000
                            End If

                        End If
                        myReaderQ.Close()

                        If DETRAZIONI_F < 10000 And NUM100C > 0 Then
                            DETRAZIONI_F = 10000
                        End If

                    Loop
                    myReaderW.Close()

                    DETRAZIONI_F = DETRAZIONI_F + (NUM100 * 3000) + (NUM66 * 1500)

                    sREDD_DIP = REDD_DIP
                    sREDD_ALT = REDD_ALT


                Case 2 'BANDO ERP
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO.* FROM DOMANDE_BANDO,DICHIARAZIONI WHERE DOMANDE_BANDO.ID_DICHIARAZIONE = DICHIARAZIONI.ID AND ID_DICHIARAZIONE=" & id_dichia
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        IdDichiarazione = par.IfNull(myReaderX("ID_DICHIARAZIONE"), 0)
                        IdDomanda = par.IfNull(myReaderX("ID"), 0)
                        codContratto = par.IfNull(myReaderX("CONTRATTO_NUM"), "")
                        ISEE = par.IfNull(myReaderX("REDDITO_ISEE"), 0)
                        ISE = par.IfNull(myReaderX("ISE_ERP"), 0)
                        ISR = par.IfNull(myReaderX("ISR_ERP"), 0)
                        ISP = par.IfNull(myReaderX("ISP_ERP"), 0)
                        PSE = par.IfNull(myReaderX("PSE"), 1)
                        VSE = par.IfNull(myReaderX("VSE"), 1)

                        DATA_EVENTO = Format(Now, "yyyyMMdd")
                    End If
                    myReaderX.Close()

                    par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & IdDichiarazione
                    myReaderX = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        ANNO_SIT_ECONOMICA = par.IfNull(myReaderX("ANNO_SIT_ECONOMICA"), "2009")
                        sANNOINIZIOVAL = "20100101" 'par.IfNull(myReaderX("data_inizio_val"), "2009")
                        sANNOFINEVAL = "20111231" ' par.IfNull(myReaderX("data_fine_val"), "2009")

                        StatoDichiarazione = par.IfNull(myReaderX("id_stato"), 1)
                    End If
                    myReaderX.Close()


                    par.cmd.CommandText = "select sum(dipendente+non_imponibili+pensione) from sepa.DOMANDE_REDDITI where ID_DOMANDA=" & ID
                    Dim myReaderW As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_DIP = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    par.cmd.CommandText = "select sum(autonomo+dom_ag_fab+occasionali) from sepa.DOMANDE_REDDITI where ID_DOMANDA=" & ID
                    myReaderW = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_ALT = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    Disabilita66 = False
                    Minori65 = False

                    par.cmd.CommandText = "select * from SEPA.COMP_NUCLEO where id_DICHIARAZIONE=" & IdDichiarazione
                    myReaderW = par.cmd.ExecuteReader()
                    Do While myReaderW.Read
                        NumComponenti = NumComponenti + 1
                        If Minori65 = False Then
                            If par.RicavaEta(par.IfNull(myReaderW("data_nascita"), "")) < 65 Then
                                Minori65 = True
                            End If
                        End If

                        If par.RicavaEtaChiusura(par.FormattaData(par.IfNull(myReaderW("data_nascita"), "")), ANNO_SIT_ECONOMICA & "1231") < 15 Then
                            MINORI15 = MINORI15 + 1
                        End If

                        If par.RicavaEtaChiusura(par.FormattaData(par.IfNull(myReaderW("data_nascita"), "")), ANNO_SIT_ECONOMICA & "1231") > 65 Then
                            MAGGIORI65 = MAGGIORI65 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) > 66 Then
                            Disabilita66 = True
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) >= 66 And par.IfNull(myReaderW("perc_inval"), 0) <= 99 Then
                            NUM66 = NUM66 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) = 100 And par.IfNull(myReaderW("INDENNITA_ACC"), "0") = "0" Then
                            NUM100 = NUM100 + 1
                        End If

                        If par.IfNull(myReaderW("perc_inval"), 0) = 100 And par.IfNull(myReaderW("INDENNITA_ACC"), "0") = "1" Then
                            NUM100C = NUM100C + 1
                        End If


                        par.cmd.CommandText = "select sum(valore) from SEPA.comp_patr_immob where id_componente=" & myReaderW("id") & " and id_tipo=0"
                        Dim myReaderQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderQ.Read Then
                            totS = totS + par.IfNull(myReaderQ(0), 0)
                        End If
                        myReaderQ.Close()


                        par.cmd.CommandText = "select SUM(importo) from SEPA.COMP_DETRAZIONI where id_componente=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        If myReaderQ.Read Then
                            TotDetrazioni = TotDetrazioni + par.IfNull(myReaderQ(0), 0)
                        End If
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            TotMobiliari = TotMobiliari + par.IfNull(myReaderQ("IMPORTO"), 0)
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            TotImmobiliari = TotImmobiliari + (par.IfNull(myReaderQ("VALORE"), 0) - par.IfNull(myReaderQ("MUTUO"), 0))
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_REDDITO WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderQ("REDDITO_IRPEF"), 0) + par.IfNull(myReaderQ("PROV_AGRARI"), 0)
                        End While
                        myReaderQ.Close()

                        par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        While myReaderQ.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderQ("IMPORTO"), 0)
                        End While
                        myReaderQ.Close()


                        par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReaderW("id")
                        myReaderQ = par.cmd.ExecuteReader()
                        If myReaderQ.HasRows Then
                            While myReaderQ.Read
                                DETRAZIONI_F = DETRAZIONI_F + par.IfNull(myReaderQ("IMPORTO"), 0)
                            End While

                        Else
                            If par.IfNull(myReaderW("indennita_acc"), 0) = "1" Then
                                DETRAZIONI_F = DETRAZIONI_F + 10000
                            End If

                        End If
                        myReaderQ.Close()

                        If DETRAZIONI_F < 10000 And NUM100C > 0 Then
                            DETRAZIONI_F = 10000
                        End If

                    Loop
                    myReaderW.Close()

                    DETRAZIONI_F = DETRAZIONI_F + (NUM100 * 3000) + (NUM66 * 1500)

                    sREDD_DIP = REDD_DIP
                    sREDD_ALT = REDD_ALT


            End Select

            sMINORI15 = MINORI15
            sMAGGIORI65 = MAGGIORI65

            sNUMCOMP = NumComponenti
            sNUMCOMP66 = NUM66
            sNUMCOMP100 = NUM100
            sNUMCOMP100C = NUM100C

            sDETRAZIONI = TotDetrazioni
            sMOBILIARI = TotMobiliari
            sIMMOBILIARI = TotImmobiliari
            sCOMPLESSIVO = REDDITO_COMPLESSIVO
            sDETRAZIONIF = DETRAZIONI_F

            sISEE = ISEE
            sISE = ISE
            sISR = ISR
            sISP = ISP
            sVSE = VSE
            sPSE = PSE

            If IdDomanda <> -1 Then

                StringaFile = StringaFile & vbCrLf & vbCrLf & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP" & vbCrLf

                StringaFile = StringaFile & vbCrLf & vbTab & "N. COMP. ................................................." & sNUMCOMP
                StringaFile = StringaFile & vbCrLf & vbTab & "N. COMP. MINORI 15 ANNI..................................." & sMINORI15
                StringaFile = StringaFile & vbCrLf & vbTab & "N. COMP. MAGGIORI 65 ANNI................................." & sMAGGIORI65
                StringaFile = StringaFile & vbCrLf & vbTab & "N. COMP. INVALIDI 66%-99%................................." & sNUMCOMP66
                StringaFile = StringaFile & vbCrLf & vbTab & "N. COMP. INVALIDI 100%...................................." & sNUMCOMP100
                StringaFile = StringaFile & vbCrLf & vbTab & "N. COMP. INVALIDI 100% CON IND. ACC......................." & sNUMCOMP100C
                StringaFile = StringaFile & vbCrLf & vbTab & "DETRAZIONI................................................" & Format(CDbl(sDETRAZIONI), "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "DETRAZIONI PER FRAGILITA'................................." & Format(CDbl(sDETRAZIONIF), "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "VALORI MOBILIARI.........................................." & Format(CDbl(sMOBILIARI), "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "VALORI IMMOBILIARI........................................" & Format(CDbl(sIMMOBILIARI), "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "REDDITO COMPLESSIVO......................................." & Format(CDbl(sCOMPLESSIVO), "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "ISEE ERP EFF.............................................." & Format(ISEE, "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "ISE ERP EFF..............................................." & Format(ISE, "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "ISR:......................................................" & Format(ISR, "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "ISP:......................................................" & Format(ISP, "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "VSE:......................................................" & Format(VSE, "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "Redditi Dipendenti o Assimilati:.........................." & Format(REDD_DIP, "##,##0.00")
                StringaFile = StringaFile & vbCrLf & vbTab & "Altri tipi di reddito Imponibili:........................." & Format(REDD_ALT, "##,##0.00")
            Else
                Decadenza = "1"
            End If


            If IdDomanda <> -1 Then
                Select Case importRedd.Value
                    Case 0 'ANAGRAFE UTENZA
                        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE (perc_patr_immobiliare=100 or piena_proprieta=1) and (FL_70KM=1 OR UPPER(COMUNE) IN (SELECT NOME FROM COMUNI_NAZIONI WHERE SIGLA='MI')) AND id_componente IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE id_dichiarazione=" & IdDichiarazione & ") "
                    Case 2 'BANDO ERP
                        par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB WHERE (perc_patr_immobiliare=100 or piena_proprieta=1) and (FL_70KM=1 OR UPPER(COMUNE) IN (SELECT NOME FROM COMUNI_NAZIONI WHERE SIGLA='MI')) AND id_componente IN (SELECT ID FROM COMP_NUCLEO WHERE id_dichiarazione=" & IdDichiarazione & ") "
                    Case 1 'GESTIONE LOCATARI
                        par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_VSA WHERE (perc_patr_immobiliare=100 or piena_proprieta=1) and (FL_70KM=1 OR UPPER(COMUNE) IN (SELECT NOME FROM COMUNI_NAZIONI WHERE SIGLA='MI')) AND id_componente IN (SELECT ID FROM COMP_NUCLEO_VSA WHERE id_dichiarazione=" & IdDichiarazione & ") "
                End Select

                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.HasRows = True Then


                    Do While myReaderA.Read
                        If Mid(par.IfNull(myReaderA("cat_catastale"), "X"), 1, 1) = "A" Then
                            Select Case NumComponenti
                                Case 1, 2
                                    If par.IfNull(myReaderA("sup_utile"), 0) >= 54 Then
                                        Decadenza = "1"
                                        sMOTIVODECADENZA = sMOTIVODECADENZA & "ALLOGGIO IDONEO (" & par.IfNull(myReaderA("sup_utile"), 0) & "mq) PER 1-2 COMP./"
                                        sALLOGGIOIDONEO = "1"
                                        ISEE = InizioUltimaArea
                                        sSOTTOAREA = "D9"
                                        Exit Do
                                    End If
                                Case 3, 4
                                    If par.IfNull(myReaderA("sup_utile"), 0) >= 74 Then
                                        Decadenza = "1"
                                        sMOTIVODECADENZA = sMOTIVODECADENZA & "ALLOGGIO IDONEO (" & par.IfNull(myReaderA("sup_utile"), 0) & "mq) PER 3-4 COMP./"
                                        sALLOGGIOIDONEO = "1"
                                        ISEE = InizioUltimaArea
                                        sSOTTOAREA = "D9"
                                        Exit Do
                                    End If
                                Case 5, 6
                                    If par.IfNull(myReaderA("sup_utile"), 0) >= 90 Then
                                        Decadenza = "1"
                                        sMOTIVODECADENZA = sMOTIVODECADENZA & "ALLOGGIO IDONEO (" & par.IfNull(myReaderA("sup_utile"), 0) & "mq) PER 5-6 COMP./"
                                        sALLOGGIOIDONEO = "1"
                                        ISEE = InizioUltimaArea
                                        sSOTTOAREA = "D9"
                                        Exit Do
                                    End If
                                Case Is > 7
                                    If par.IfNull(myReaderA("sup_utile"), 0) >= 114 Then
                                        Decadenza = "1"
                                        sMOTIVODECADENZA = sMOTIVODECADENZA & "ALLOGGIO IDONEO (" & par.IfNull(myReaderA("sup_utile"), 0) & "mq) PER 7 O + COMP./"
                                        sALLOGGIOIDONEO = "1"
                                        ISEE = InizioUltimaArea
                                        sSOTTOAREA = "D9"
                                        Exit Do
                                    End If
                            End Select
                        End If
                    Loop
                End If
                myReaderA.Close()

                Select Case importRedd.Value
                    Case 0 ' ANAGRAFE UTENZA
                        par.cmd.CommandText = "SELECT SUM(VALORE) FROM UTENZA_COMP_PATR_IMMOB WHERE (perc_patr_immobiliare=100 or piena_proprieta=1) and (FL_70KM=1 OR UPPER(COMUNE) IN (SELECT NOME FROM COMUNI_NAZIONI WHERE SIGLA='MI')) AND id_componente IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE id_dichiarazione=" & IdDichiarazione & ") "
                    Case 2 'BANDO ERP
                        par.cmd.CommandText = "SELECT SUM(VALORE) FROM COMP_PATR_IMMOB WHERE (perc_patr_immobiliare=100 or piena_proprieta=1) and (FL_70KM=1 OR UPPER(COMUNE) IN (SELECT NOME FROM COMUNI_NAZIONI WHERE SIGLA='MI')) AND id_componente IN (SELECT ID FROM COMP_NUCLEO WHERE id_dichiarazione=" & IdDichiarazione & ") "
                    Case 1 'GESTIONE LOCATARI
                        par.cmd.CommandText = "SELECT SUM(VALORE) FROM COMP_PATR_IMMOB_VSA WHERE (perc_patr_immobiliare=100 or piena_proprieta=1) and (FL_70KM=1 OR UPPER(COMUNE) IN (SELECT NOME FROM COMUNI_NAZIONI WHERE SIGLA='MI')) AND id_componente IN (SELECT ID FROM COMP_NUCLEO_VSA WHERE id_dichiarazione=" & IdDichiarazione & ") "
                End Select


                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.HasRows = True Then
                    Do While myReaderA.Read
                        Select Case NumComponenti
                            Case 1, 2
                                If par.IfNull(myReaderA(0), 0) >= 42026.25 Then
                                    Decadenza = "1"
                                    sMOTIVODECADENZA = sMOTIVODECADENZA & "VALORE ICI ALLOGGIO (" & par.IfNull(myReaderA(0), 0) & ") SUPERIORE PER 1-2 COMP./"
                                    sVALOCIICI = "1"
                                    ISEE = InizioUltimaArea
                                    sSOTTOAREA = "D8"
                                    Exit Do
                                End If
                            Case 3, 4
                                If par.IfNull(myReaderA(0), 0) >= 54634.13 Then
                                    Decadenza = "1"
                                    sMOTIVODECADENZA = sMOTIVODECADENZA & "VALORE ICI ALLOGGIO (" & par.IfNull(myReaderA(0), 0) & ") SUPERIORE  PER 3-4 COMP./"
                                    sVALOCIICI = "1"
                                    ISEE = InizioUltimaArea
                                    sSOTTOAREA = "D8"
                                    Exit Do
                                End If
                            Case 5, 6
                                If par.IfNull(myReaderA(0), 0) >= 58836.75 Then
                                    Decadenza = "1"
                                    sMOTIVODECADENZA = sMOTIVODECADENZA & "VALORE ICI ALLOGGIO (" & par.IfNull(myReaderA(0), 0) & ") SUPERIORE PER 5-6 COMP./"
                                    sVALOCIICI = "1"
                                    ISEE = InizioUltimaArea
                                    sSOTTOAREA = "D8"
                                    Exit Do
                                End If
                            Case Is > 7
                                If par.IfNull(myReaderA(0), 0) >= 75647.25 Then
                                    Decadenza = "1"
                                    sMOTIVODECADENZA = sMOTIVODECADENZA & "VALORE ICI ALLOGGIO (" & par.IfNull(myReaderA(0), 0) & ") SUPERIORE PER 7 O + COMP./"
                                    sVALOCIICI = "1"
                                    ISEE = InizioUltimaArea
                                    sSOTTOAREA = "D8"
                                    Exit Do
                                End If
                        End Select
                    Loop
                End If
                myReaderA.Close()


                par.cmd.CommandText = "select COD_TIPOLOGIA_CONTR_LOC from siscom_MI.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "'"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    If par.IfNull(myReaderA("COD_TIPOLOGIA_CONTR_LOC"), "") = "NONE" Then
                        Decadenza = "1"
                        sMOTIVODECADENZA = sMOTIVODECADENZA & "TIPOLOGIA RAPPORTO ABUSIVO/"
                        ISEE = InizioUltimaArea
                        sSOTTOAREA = "D7"
                    End If
                End If
                myReaderA.Close()
            End If


            Select Case Mid(DATA_EVENTO, 1, 4)
                Case "2006"
                    LimitePensioni = 10520
                Case "2007"
                    LimitePensioni = 10731
                Case "2008"
                    LimitePensioni = 10903
                Case "2009"
                    LimitePensioni = 11274
                Case "2010"
                    LimitePensioni = 11343
                Case "2011"
                    LimitePensioni = 11501
                Case "2012"
                    LimitePensioni = 11824
            End Select


            If Decadenza = "0" Then
                StringaFile = StringaFile & vbCrLf & vbTab & "Limite 2 pensioni INPS, minima + sociale:................." & Format(LimitePensioni, "##,##0.00")
                'VERIFICA SE IL REDDITO E' PREVALENTEMENTE DIPENDENTE O NO
                Dim prev_dip As Boolean
                prev_dip = False

                If ISEE = 0 Then
                    Beep()
                End If

                If REDD_DIP > ((REDD_ALT + REDD_DIP) * 80) / 100 Then
                    prev_dip = True
                    StringaFile = StringaFile & vbCrLf & vbTab & "Prevalentemente dipendente:...............................SI"
                Else
                    'If ISEE <> 0 Then
                    StringaFile = StringaFile & vbCrLf & vbTab & "Prevalentemente dipendente:...............................NO"
                    'End If
                End If
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

        Return StringaFile


    End Function

End Class
