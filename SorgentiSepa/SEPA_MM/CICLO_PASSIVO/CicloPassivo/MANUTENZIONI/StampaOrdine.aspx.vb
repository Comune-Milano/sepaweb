Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class StampaOrdine
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sUBICAZIONE As String = ""
        Dim sUBICAZIONE2 As String = ""
        Dim sModello As String = ""

        Dim sODL As String = ""
        Dim sODL_DATA As String = ""
        Dim sODL_EXTRA As String = ""

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            '$ODL                   MANUTENZIONI.ODL ||'/'|| MANUTENZIONI.ANNO
            '$ODL_DATA              MANUTENZIONI.DATA_INIZIO_ORDINE   
            '$$ODL_EXTRA$$          MANUTENZIONI.ODL ||'/'|| MANUTENZIONI.ANNO ||' del '|| MANUTENZIONI.DATA_INIZIO_ORDINE (della manutenzione INTEGRATA)
            '                       oppure(ANNULLATO)

            '$REPERTORIO            APPALTI.NUM_REPERTORIO
            '$DATA_REPERTORIO       APPALTI.DATA_REPERTORIO
            '$DESCRIZIONE_APPALTO   APPALTI.DESCRIZIONE as ""APPALTO""

            '$FORNITORE             FORNITORI.RAGIONE_SOCIALE,FORNITORI.COGNOME,FORNITORI.NOME " _
            '$FORNITORI_INDIRIZZI$  FORNITORI_INDIRIZZI.TIPO FORNITORI_INDIRIZZI.INDIRIZZ FORNITORI_INDIRIZZI.CIVICO FORNITORI_INDIRIZZI.CAP  ||' - '|| FORNITORI_INDIRIZZI.COMUNE


            '$UBICAZIONE_INDIRIZZO  COMPLESSO o (EDICIIO e/0 SCALA) (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP) AS INDIRIZZO,
            '$UBICAZIONE_INDIRIZZO2 INDIRIZZI.CAP ||' - '|| INDIRIZZI.LOCALITA

            '$DESCRIZIONE_MANUTENZIONI$ MANUTENZIONI.DESCRIZIONE

            '$FRASE_MODELLO$        'frase + MANUTENZIONI.DATA_FINE_INTERVENTO


            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloOrdineRRS2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            'par.cmd.CommandText = "select data_inizio_INTERVENTO from siscom_mi.manutenzioni where id=" & Request.QueryString("COD")
            'Dim data_inizio_ordine As String = par.IfNull(par.cmd.ExecuteScalar, "")
            Dim condizioneDataOrdine As String = ""
            'If data_inizio_ordine <> "" Then
            condizioneDataOrdine = " INIZIO_VALIDITA<='" & Format(Now, "yyyyMMdd") & "' AND FINE_VALIDITA>='" & Format(Now, "yyyyMMdd") & "' AND "
            'End If
            par.cmd.CommandText = "SELECT (CASE WHEN COGNOME IS NOT NULL THEN COGNOME||' 'ELSE NULL END )||" _
                & "(CASE WHEN NOME IS NOT NULL THEN NOME||'; ' ELSE NULL END)||" _
                & "(CASE WHEN (select max(contatto_telefonico) from siscom_mi.building_manager_operatori where " & condizioneDataOrdine & " building_manager_operatori.id_operatore=operatori.id and TIPO_OPERATORE=1) IS NOT NULL THEN (select max(contatto_telefonico) from siscom_mi.building_manager_operatori where " & condizioneDataOrdine & " building_manager_operatori.id_operatore=operatori.id and TIPO_OPERATORE=1)||';' ELSE NULL END)|| " _
                & "(CASE WHEN (select max(contatto_mail) from siscom_mi.building_manager_operatori where " & condizioneDataOrdine & " building_manager_operatori.id_operatore=operatori.id and TIPO_OPERATORE=1) IS NOT NULL THEN (select max(contatto_mail) from siscom_mi.building_manager_operatori where " & condizioneDataOrdine & " building_manager_operatori.id_operatore=operatori.id and TIPO_OPERATORE=1) ELSE NULL END) " _
                & " FROM SEPA.OPERATORI WHERE ID IN (" _
                & " SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE " _
                & condizioneDataOrdine _
                & " TIPO_OPERATORE=1" _
                & " AND ID_BM IN (" _
                & " SELECT DISTINCT ID_BM FROM SISCOM_MI.EDIFICI WHERE ID IN (" _
                & " SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID_COMPLESSO FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_COMPLESSO IS NOT NULL AND ID_MANUTENZIONE=" & Request.QueryString("COD") & ")" _
                & " UNION " _
                & " SELECT ID_EDIFICIO FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_EDIFICIO IS NOT NULL AND ID_MANUTENZIONE=" & Request.QueryString("COD") & "" _
                & " UNION " _
                & " SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_UNITA_IMMOBILIARE IS NOT NULL AND ID_MANUTENZIONE=" & Request.QueryString("COD") & ")" _
                & " UNION" _
                & " SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_COMUNI WHERE ID IN (SELECT ID_UNITA_COMUNE FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_UNITA_COMUNE IS NOT NULL AND ID_MANUTENZIONE=" & Request.QueryString("COD") & ")" _
                & " UNION " _
                & " SELECT ID_EDIFICIO FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_SCALA IS NOT NULL AND ID_MANUTENZIONE=" & Request.QueryString("COD") & ")" _
                & " UNION " _
                & " SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID IN (SELECT IMPIANTI_UI.ID_UNITA FROM SISCOM_MI.IMPIANTI_UI WHERE ID_IMPIANTO IN (SELECT ID FROM SISCOM_MI.IMPIANTI WHERE COD_TIPOLOGIA<>'SO' AND ID IN (SELECT ID_IMPIANTO FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_IMPIANTO IS NOT NULL AND ID_MANUTENZIONE=" & Request.QueryString("COD") & ")))" _
                & ")))"
            Dim buildingManager As String = ""
            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore2.Read
                buildingManager &= par.IfNull(lettore2(0), "") & "<br>"
            End While
            lettore2.Close()



            par.cmd.CommandText = "select MANUTENZIONI.ID,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO,MANUTENZIONI.DESCRIZIONE," _
                & " MANUTENZIONI.ID_COMPLESSO,MANUTENZIONI.ID_EDIFICIO,MANUTENZIONI.ID_SCALA," _
                & " MANUTENZIONI.DATA_INIZIO_ORDINE,MANUTENZIONI.DATA_INIZIO_INTERVENTO,MANUTENZIONI.DATA_FINE_INTERVENTO," _
                & " MANUTENZIONI.STATO,MANUTENZIONI.ID_FIGLIO,SISCOM_MI.TAB_SERVIZI.DESCRIZIONE as ""TIPO_SERVIZIO"",MANUTENZIONI.DANNEGGIANTE,MANUTENZIONI.DANNEGGIATO," _
                & " SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.DATA_REPERTORIO,SISCOM_MI.APPALTI.DESCRIZIONE as ""APPALTO"", " _
                & " FORNITORI.ID as ""ID_FORNITORE"",(CASE WHEN MANUTENZIONI.ID_FORNITORE_STAMPA IS NOT NULL THEN MANUTENZIONI.ID_FORNITORE_STAMPA ELSE APPALTI.ID_FORNITORE END) as id_fornitore_stampa,FORNITORI.RAGIONE_SOCIALE,FORNITORI.COGNOME,FORNITORI.NOME,FORNITORI.COD_FORNITORE, " _
                & " manutenzioni.operatore_autorizzazione, " _
                & "(select fornitori.ragione_sociale from siscom_mi.fornitori where fornitori.id=(CASE WHEN MANUTENZIONI.ID_FORNITORE_STAMPA IS NOT NULL THEN MANUTENZIONI.ID_FORNITORE_STAMPA ELSE APPALTI.ID_FORNITORE END)) as ragione_sociale_app," _
                & "(select fornitori.COD_FORNITORE from siscom_mi.fornitori where fornitori.id=(CASE WHEN MANUTENZIONI.ID_FORNITORE_STAMPA IS NOT NULL THEN MANUTENZIONI.ID_FORNITORE_STAMPA ELSE APPALTI.ID_FORNITORE END)) as cod_fornitore_app," _
                & "(select fornitori.COGNOME from siscom_mi.fornitori where fornitori.id=(CASE WHEN MANUTENZIONI.ID_FORNITORE_STAMPA IS NOT NULL THEN MANUTENZIONI.ID_FORNITORE_STAMPA ELSE APPALTI.ID_FORNITORE END)) as cognome_app," _
                & "(select fornitori.NOME from siscom_mi.fornitori where fornitori.id=(CASE WHEN MANUTENZIONI.ID_FORNITORE_STAMPA IS NOT NULL THEN MANUTENZIONI.ID_FORNITORE_STAMPA ELSE APPALTI.ID_FORNITORE END)) as nome_app " _
                & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI,SISCOM_MI.TAB_SERVIZI " _
                & " where MANUTENZIONI.ID=" & Request.QueryString("cod") _
                & " and  MANUTENZIONI.ID_APPALTO=APPALTI.ID (+) " _
                & " /*AND (CASE WHEN MANUTENZIONI.ID_FORNITORE_STAMPA IS NOT NULL THEN MANUTENZIONI.ID_FORNITORE_STAMPA ELSE APPALTI.ID_FORNITORE END)=FORNITORI.ID*/ " _
                & " and  MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)" _
                & " and  APPALTI.ID_FORNITORE=FORNITORI.ID (+)"


            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then

                If par.IfNull(myReaderA("STATO"), 0) = 3 Then
                    'MANUTENZIONE INTEGRATA
                    Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select PROGR,ANNO,DATA_INIZIO_ORDINE from SISCOM_MI.MANUTENZIONI where ID=" & par.IfNull(myReaderA("ID_FIGLIO"), "-1")
                    myReaderTMP = par.cmd.ExecuteReader()

                    If myReaderTMP.Read Then

                        sODL = par.IfNull(myReaderA("PROGR"), "") & "/" & par.IfNull(myReaderA("ANNO"), "")
                        sODL_DATA = par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_ORDINE"), ""))
                        sODL_EXTRA = " INTEGRATO DELL'ODL " & par.IfNull(myReaderTMP("PROGR"), "") & "/" & par.IfNull(myReaderTMP("ANNO"), "") & " del " & par.FormattaData(par.IfNull(myReaderTMP("DATA_INIZIO_ORDINE"), ""))
                    End If
                    myReaderTMP.Close()

                ElseIf par.IfNull(myReaderA("STATO"), 0) = 5 Then
                    'ANNULLATO
                    sODL = par.IfNull(myReaderA("PROGR"), "") & "/" & par.IfNull(myReaderA("ANNO"), "")
                    sODL_DATA = par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_ORDINE"), ""))
                    sODL_EXTRA = "( ANNULLATO )"

                Else
                    Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select ID,PROGR,ANNO,DATA_INIZIO_ORDINE from SISCOM_MI.MANUTENZIONI where ID_FIGLIO=" & par.IfNull(myReaderA("ID"), "-1")
                    myReaderTMP = par.cmd.ExecuteReader()

                    If myReaderTMP.Read Then
                        'MANUTENZIONE INTEGRAZIONE 

                        sODL = par.IfNull(myReaderA("PROGR"), "") & "/" & par.IfNull(myReaderA("ANNO"), "")
                        sODL_DATA = par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_ORDINE"), ""))
                        sODL_EXTRA = " INTEGRAZIONE DELL'ODL " & par.IfNull(myReaderTMP("PROGR"), "") & "/" & par.IfNull(myReaderTMP("ANNO"), "") & " del " & par.FormattaData(par.IfNull(myReaderTMP("DATA_INIZIO_ORDINE"), ""))

                    End If
                    myReaderTMP.Close()

                End If

                If sODL = "" Then
                    sODL = par.IfNull(myReaderA("PROGR"), "") & "/" & par.IfNull(myReaderA("ANNO"), "")
                    sODL_DATA = par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_ORDINE"), ""))
                End If


                Dim firmaResponsabile As String = "<span class=""style13"">.................................................................................</span>"
                Dim operatoreAutorizzazione As Integer = par.IfNull(myReaderA("operatore_autorizzazione"), 0)
                par.cmd.CommandText = "SELECT FIRMA,COGNOME,NOME FROM SISCOM_MI.FIRME_OPERATORI,SEPA.OPERATORI WHERE OPERATORI.ID=ID_OPERATORE AND ID_OPERATORE=" & operatoreAutorizzazione
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    If IO.File.Exists(Server.MapPath("../../../ALLEGATI/FIRME/" & par.IfNull(lettore("FIRMA"), ""))) Then
                        firmaResponsabile = "<table><tr><td>" & par.IfNull(lettore("NOME"), "") & " " & par.IfNull(lettore("COGNOME"), "") & "</td></tr><tr><td><img alt="""" src=""" & par.IfNull(lettore("FIRMA"), "") & """ width=""200"" heigth=""80"" /></td></tr></table>"
                    End If
                End If
                lettore.Close()
                contenuto = Replace(contenuto, "$firmaResponsabile$", firmaResponsabile)

                contenuto = Replace(contenuto, "$ODL$", sODL)

                contenuto = Replace(contenuto, "$ODL$", sODL)
                contenuto = Replace(contenuto, "$ODL_DATA$", sODL_DATA)
                contenuto = Replace(contenuto, "$ODL_EXTRA$", sODL_EXTRA)


                contenuto = Replace(contenuto, "$REPERTORIO$", par.IfNull(myReaderA("NUM_REPERTORIO"), ""))
                contenuto = Replace(contenuto, "$DATA_REPERTORIO$", par.FormattaData(par.IfNull(myReaderA("DATA_REPERTORIO"), "")))
                contenuto = Replace(contenuto, "$DESCRIZIONE_APPALTO$", par.IfNull(myReaderA("APPALTO"), ""))

                'DATI FORNITORE APPALTO
                Dim fornitoreStampa As String = ""
                If par.IfNull(myReaderA("RAGIONE_SOCIALE_APP"), "") = "" Then
                    If par.IfNull(myReaderA("COD_FORNITORE"), "") = "" Then
                        fornitoreStampa = par.IfNull(myReaderA("COGNOME_APP"), "") & " " & par.IfNull(myReaderA("NOME_APP"), "")
                    Else
                        fornitoreStampa = par.IfNull(myReaderA("COD_FORNITORE_APP"), "") & " - " & par.IfNull(myReaderA("COGNOME_APP"), "") & " " & par.IfNull(myReaderA("NOME_APP"), "")
                    End If
                Else
                    If par.IfNull(myReaderA("COD_FORNITORE_APP"), "") = "" Then
                        fornitoreStampa = par.IfNull(myReaderA("RAGIONE_SOCIALE_APP"), "")
                    Else
                        fornitoreStampa = par.IfNull(myReaderA("COD_FORNITORE_APP"), "") & " - " & par.IfNull(myReaderA("RAGIONE_SOCIALE_APP"), "")
                    End If
                End If

                'DATI FORNITORE
                Dim fornitoreAppalto As String = ""
                If par.IfNull(myReaderA("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReaderA("COD_FORNITORE"), "") = "" Then
                        fornitoreAppalto = par.IfNull(myReaderA("COGNOME"), "") & " " & par.IfNull(myReaderA("NOME"), "")
                    Else
                        fornitoreAppalto = par.IfNull(myReaderA("COD_FORNITORE"), "") & " - " & par.IfNull(myReaderA("COGNOME"), "") & " " & par.IfNull(myReaderA("NOME"), "")
                    End If
                Else
                    If par.IfNull(myReaderA("COD_FORNITORE"), "") = "" Then
                        fornitoreAppalto = par.IfNull(myReaderA("RAGIONE_SOCIALE"), "")
                    Else
                        fornitoreAppalto = par.IfNull(myReaderA("COD_FORNITORE"), "") & " - " & par.IfNull(myReaderA("RAGIONE_SOCIALE"), "")
                    End If
                End If

               


                'INDIRIZZO FORNITORE APPALTO
                Dim sIndirizzoFornitore1 As String = ""
                Dim sIndirizzoFornitore2 As String = ""

                par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                    & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                    & " where ID_FORNITORE=" & par.IfNull(myReaderA("ID_FORNITORE"), 0)

                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                myReaderT = par.cmd.ExecuteReader
                While myReaderT.Read

                    sIndirizzoFornitore1 = par.IfNull(myReaderT("TIPO"), "") _
                                   & " " & par.IfNull(myReaderT("INDIRIZZO"), "") _
                                   & " " & par.IfNull(myReaderT("CIVICO"), "")

                    sIndirizzoFornitore2 = par.IfNull(myReaderT("CAP"), "") _
                                & " - " & par.IfNull(myReaderT("COMUNE"), "")

                End While
                myReaderT.Close()

                'INDIRIZZO FORNITORE APPALTO
                Dim sIndirizzoFornitore1stampa As String = ""
                Dim sIndirizzoFornitore2stampa As String = ""

                par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                    & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                    & " where ID_FORNITORE=" & par.IfNull(myReaderA("ID_FORNITORE_STAMPA"), 0)

                myReaderT = par.cmd.ExecuteReader
                While myReaderT.Read

                    sIndirizzoFornitore1stampa = par.IfNull(myReaderT("TIPO"), "") _
                                   & " " & par.IfNull(myReaderT("INDIRIZZO"), "") _
                                   & " " & par.IfNull(myReaderT("CIVICO"), "")

                    sIndirizzoFornitore2stampa = par.IfNull(myReaderT("CAP"), "") _
                                & " - " & par.IfNull(myReaderT("COMUNE"), "")

                End While
                myReaderT.Close()

                fornitoreAppalto &= "<br />" & sIndirizzoFornitore1 & "<br />" & sIndirizzoFornitore2
                fornitoreStampa &= "<br />" & sIndirizzoFornitore1stampa & "<br />" & sIndirizzoFornitore2stampa

                'contenuto = Replace(contenuto, "$FORNITORE_INDIRIZZO$", sIndirizzoFornitore1)
                'contenuto = Replace(contenuto, "$FORNITORE_INDIRIZZO2$", sIndirizzoFornitore2)
                contenuto = Replace(contenuto, "$FORNITORE_INDIRIZZO$", "")
                contenuto = Replace(contenuto, "$FORNITORE_INDIRIZZO2$", "")



                If fornitoreAppalto = fornitoreStampa Then
                    contenuto = Replace(contenuto, "$FORNITORE$", fornitoreAppalto)
                Else
                    contenuto = Replace(contenuto, "$FORNITORE$", fornitoreAppalto & "<br /><br />" & fornitoreStampa)
                End If


                '**********************************************



                If par.IfNull(myReaderA("DANNEGGIANTE"), "") <> "" Then
                    contenuto = Replace(contenuto, "$DANNEGGIANTE$", "Danneggiante " & par.IfNull(myReaderA("DANNEGGIANTE"), ""))
                Else
                    contenuto = Replace(contenuto, "$DANNEGGIANTE$", "")
                End If

                If par.IfNull(myReaderA("DANNEGGIATO"), "") <> "" Then
                    contenuto = Replace(contenuto, "$DANNEGGIATO$", "Danneggiato " & par.IfNull(myReaderA("DANNEGGIATO"), ""))
                Else
                    contenuto = Replace(contenuto, "$DANNEGGIATO$", "")
                End If


                ' UBICAZIONE
                If par.IfNull(myReaderA("ID_COMPLESSO"), "-1") <> "-1" Then

                    Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select * from SISCOM_MI.INDIRIZZI where ID=(select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & myReaderA("ID_COMPLESSO") & ")"
                    myReaderTMP = par.cmd.ExecuteReader()
                    If myReaderTMP.Read Then

                        sUBICAZIONE = "COMPLESSO in " & par.IfNull(myReaderTMP("DESCRIZIONE"), "") & " N. " & par.IfNull(myReaderTMP("CIVICO"), "")
                        sUBICAZIONE2 = par.IfNull(myReaderTMP("CAP"), "") & " - " & par.IfNull(myReaderTMP("LOCALITA"), "")
                    End If
                    myReaderTMP.Close()

                ElseIf par.IfNull(myReaderA("ID_EDIFICIO"), "-1") <> "-1" Then

                    Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select * from SISCOM_MI.INDIRIZZI where ID=(select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI where ID=" & myReaderA("ID_EDIFICIO") & ")"
                    myReaderTMP = par.cmd.ExecuteReader()
                    If myReaderTMP.Read Then

                        sUBICAZIONE = "EDIFICIO in " & par.IfNull(myReaderTMP("DESCRIZIONE"), "") & " N. " & par.IfNull(myReaderTMP("CIVICO"), "")
                        sUBICAZIONE2 = par.IfNull(myReaderTMP("CAP"), "") & " - " & par.IfNull(myReaderTMP("LOCALITA"), "")
                    End If
                    myReaderTMP.Close()

                    If par.IfNull(myReaderA("ID_SCALA"), "-1") <> "-1" Then

                        par.cmd.CommandText = "select DESCRIZIONE  from SISCOM_MI.SCALE_EDIFICI where ID= " & par.IfNull(myReaderA("ID_SCALA"), "-1")
                        myReaderTMP = par.cmd.ExecuteReader()

                        If myReaderTMP.Read Then
                            sUBICAZIONE = sUBICAZIONE & " SCALA=" & par.IfNull(myReaderTMP("DESCRIZIONE"), "")
                        End If
                        myReaderTMP.Close()
                    End If
                End If
                '****************************


                contenuto = Replace(contenuto, "$UBICAZIONE_INDIRIZZO$", sUBICAZIONE)
                contenuto = Replace(contenuto, "$UBICAZIONE_INDIRIZZO2$", sUBICAZIONE2)

                contenuto = Replace(contenuto, "$DESCRIZIONE_MANUTENZIONI$", par.IfNull(myReaderA("DESCRIZIONE"), ""))


                Dim building As String = ""
                If buildingManager <> "" Then
                    building = "Building Manager: " & buildingManager
                End If

                sModello = "Attenersi agli obblighi in materia di tutela della salute e della sicurezza nei luoghi di lavoro Titolo 4° DLgs 81/2008 E s.m.i. <br><br>" & vbCrLf _
                    & "Programma attività: " _
                    & "<br> " _
                    & "-inizio il " & par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_INTERVENTO"), "")) _
                    & "<br> " _
                    & "-fine il " & par.FormattaData(par.IfNull(myReaderA("DATA_FINE_INTERVENTO"), "")) _
                    & "<br> " _
                    & building

                '& " Intervento da eseguirsi entro il " & par.FormattaData(par.IfNull(myReaderA("DATA_FINE_INTERVENTO"), "")) & " oltre appl.art.11 C.S.p.1 "

                contenuto = Replace(contenuto, "$FRASE_MODELLO$", sModello)

                'contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(Format(Now, "yyyyMMdd")))
                contenuto = Replace(contenuto, "$data_stampa$", "")

            End If

            myReaderA.Close()

            Dim sSQL_DettaglioIMPIANTO As String
            sSQL_DettaglioIMPIANTO = "(CASE IMPIANTI.COD_TIPOLOGIA " _
                                        & " WHEN 'AN' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'CF' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'CI' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'EL' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'GA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'ID' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'ME' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'SO' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||" _
                                                & "(select  (CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                                & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                        & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END) " _
                                                & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                        & " WHEN 'TA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'TE' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'TR' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'TU' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'TV' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " ELSE '' " _
                                    & " END) as DETTAGLIO "

            '*** MANUTENZIONI_INTERVENTI
            par.cmd.CommandText = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                       & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                       & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                       & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & Request.QueryString("cod") _
                       & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
                 & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                       & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                       & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                       & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & Request.QueryString("cod") _
                       & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                 & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                       & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                       & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                       & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & Request.QueryString("cod") _
                       & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
                 & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                       & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                       & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                       & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & Request.QueryString("cod") _
                       & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                 & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                       & "       SISCOM_MI.EDIFICI.DENOMINAZIONE||' - Scala '||SCALE_EDIFICI.descrizione  AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                       & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI " _
                       & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & Request.QueryString("cod") _
                       & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='SCALA' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and SCALE_EDIFICI.ID_EDIFICIO=EDIFICI.ID " _
                & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                       & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                 & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                      & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & Request.QueryString("cod") & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) "





            Dim TestoGrigliaM As String
            TestoGrigliaM = "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
            TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                      & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TIPOLOGIA </td>" _
                                      & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DETTAGLIO </td>" _
                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                      & "</tr>"


            myReaderA = par.cmd.ExecuteReader()
            While myReaderA.Read

                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                      & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderA("TIPOLOGIA"), "") & "</td>" _
                                      & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderA("DETTAGLIO"), "") & "</td>" _
                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                      & "</tr>"


            End While
            myReaderA.Close()

            contenuto = Replace(contenuto, "$TITOLO_OGGETTO_MANUTENZIONI$", "OGGETTO INTERVENTI:")
            contenuto = Replace(contenuto, "$OGGETTO_MANUTENZIONI$", TestoGrigliaM)



            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            'STAMPA PDF
            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False


            'Dim NomeFile1 As String = "Ordine_" & Request.QueryString("cod") & "_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            'pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & NomeFile1, Server.MapPath("..\..\..\NuoveImm\"))
            'Response.Write("<script>window.open('../../../FileTemp/" & NomeFile1 & "','stampa','');self.close();</script>")

            'Dim NomeFile1 As String = "Ordine_" & Request.QueryString("cod") & "_" & Format(Now, "yyyyMMddHHmmss") '& ".pdf"
            'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".htm", False, System.Text.Encoding.Default)
            'sr.WriteLine(contenuto)
            'sr.Close()

            'pdfConverter1.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".pdf")
            'If IO.File.Exists(Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".htm") Then
            '    IO.File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".htm")
            'End If
            'Response.Write("<script>window.open('../../../FileTemp/" & NomeFile1 & ".pdf','stampa','');self.close();</script>")

            If IO.File.Exists(Server.MapPath("../../../NuoveImm/MM_113_84.png")) Then
                If Not Directory.Exists(Server.MapPath("../../../ALLEGATI/FIRME")) Then
                    Directory.CreateDirectory(Server.MapPath("../../../ALLEGATI/FIRME"))
                End If
                If File.Exists(Server.MapPath("../../../ALLEGATI/FIRME/MM_113_84.png")) Then
                    'IO.File.Delete(Server.MapPath("../../../ALLEGATI/FIRME/MM_113_84.png"))
                Else
                    IO.File.Copy(Server.MapPath("../../../NuoveImm/MM_113_84.png"), Server.MapPath("../../../ALLEGATI/FIRME/MM_113_84.png"))
                End If
            End If

            Dim NomeFile1 As String = "Ordine_" & Request.QueryString("cod") & "_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            NomeFile1 = par.NomeFileManut("MAN", Request.QueryString("cod")) & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & NomeFile1, Server.MapPath("..\..\..\ALLEGATI\FIRME\"))
            Response.Write("<script>window.open('../../../FileTemp/" & NomeFile1 & "','stampa','');self.close();</script>")


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Stampa Ordine Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
