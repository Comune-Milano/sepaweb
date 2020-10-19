Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CENSIMENTO_RisultatiRptUnitaImmSoglia
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim Complesso As String = ""
    Dim Edificio As String = ""
    Dim Indirizzo As String = ""
    Dim Civico As String = ""
    Dim Scala As String = ""
    Dim Interno As String = ""
    Dim Ascensore As Integer = 0
    Dim Condominio As Integer = 0
    Dim Struttura As String = ""
    Dim Tipologia As String = ""
    Dim Disponibilita As String = ""
    Dim DestUso As String = ""
    Dim ContrattiChiusi As String = ""
    Dim ContrattiAperti As String = ""
    Dim ContrattiBozza As String = ""
    Dim Quartiere As String = ""
    Dim dt As New Data.DataTable
    Dim dtContratti As New Data.DataTable
    Dim backcolor As Integer = 1
    Dim StatoContrattiChiuso As String = ""
    Dim StatoContrattiAperti As String = ""
    Dim Soglia As String = ""
    Dim filtroRicerca As String = ""

    Public Property StringaSQL() As String
        Get
            If Not (ViewState("StringaSQL") Is Nothing) Then
                Return CStr(ViewState("StringaSQL"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("StringaSQL") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
                                  & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
                                  & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
                                  & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
                                  & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
                                  & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
                                  & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
                                  & "</td></tr></table></div></div>"
            Response.Write(Loading)
            If Not IsPostBack Then
                Response.Flush()
                CaricaDati()
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
                SettaValoriRicerca()
                BindGrid()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Sub CaricaDati()
        Try
            Complesso = Request.QueryString("C")
            Edificio = Request.QueryString("E")
            Indirizzo = Request.QueryString("I")
            Civico = Request.QueryString("Ci")
            Scala = Request.QueryString("S")
            Interno = Request.QueryString("In")
            Ascensore = Request.QueryString("A")
            Condominio = Request.QueryString("Co")
            Struttura = Request.QueryString("St")
            Tipologia = Request.QueryString("T")
            Disponibilita = Request.QueryString("Di")
            DestUso = Request.QueryString("De")
            ContrattiChiusi = Request.QueryString("CC")
            ContrattiAperti = Request.QueryString("CA")
            ContrattiBozza = Request.QueryString("CB")
            Quartiere = Request.QueryString("Q")
            StatoContrattiChiuso = Request.QueryString("SCC")
            StatoContrattiAperti = Request.QueryString("SCA")
            Soglia = Request.QueryString("SOGLIA")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Sub SettaValoriRicerca()
        Try
            Dim reader As Oracle.DataAccess.Client.OracleDataReader
            Dim valoreSoglia As Decimal = 0
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='SOGLIA ABITABILITA'"
            reader = par.cmd.ExecuteReader
            If reader.Read Then
                valoreSoglia = CDec(par.IfNull(reader(0), 0))
            End If
            reader.Close()
            Dim primo As Boolean = True
            StringaSQL = "SELECT DESTINAZIONI_USO_UI.DESCRIZIONE AS DESTINAZIONE_USO,PROGRAMMAZIONE_INTERVENTI.DESCRIZIONE AS PROGR_INTERVENTI,IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB, SISCOM_MI.UNITA_IMMOBILIARI.ID AS ID_UI, " _
                       & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') AS CODICE_UI, " _
                       & "SISCOM_MI.EDIFICI.CONDOMINIO, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA, " _
                       & "SISCOM_MI.INDIRIZZI.DESCRIZIONE AS INDIRIZZO, SISCOM_MI.INDIRIZZI.CIVICO, SISCOM_MI.UNITA_IMMOBILIARI.INTERNO, SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA, " _
                       & "SISCOM_MI.PIANI.DESCRIZIONE AS PIANO, SISCOM_MI.INDIRIZZI.CAP, SEPA.COMUNI_NAZIONI.NOME AS COMUNE, SISCOM_MI.TAB_QUARTIERI.NOME AS QUARTIERE, SISCOM_MI.TAB_FILIALI.NOME AS STRUTTURA, SISCOM_MI.EDIFICI.COD_EDIFICIO, " _
                       & "SISCOM_MI.EDIFICI.DENOMINAZIONE, (CASE WHEN SISCOM_MI.EDIFICI.NUM_ASCENSORI > 0 THEN 'SI' ELSE 'NO' END) AS ASCENSORE, (CASE WHEN SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO.COD = 'AUTO' THEN 'SI' ELSE 'NO' END) AS RISCAUTO, " _
                       & "(CASE WHEN SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO.COD = 'CENT' THEN 'SI' ELSE 'NO' END) AS RISCCENT, " _
                       & "NVL((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID),0) AS SUPNETTA, " _
                       & "NVL((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_UR' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID),0) AS SUPUTILE, " _
                       & "NVL((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'BALCONI' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID),0) AS BALCONI, " _
                       & "NVL((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_COMM' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID),0) AS SUPCOMM, " _
                       & "NVL((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUSCO' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID),0) AS SUPESCLUSIVA, " _
                       & "TIPO_DISPONIBILITA.DESCRIZIONE AS STATO_UI," _
                       & "(CASE WHEN (UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL') THEN ((CASE WHEN ((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID) IS NOT NULL) THEN (CASE WHEN ((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=" & par.VirgoleInPunti(valoreSoglia) & ") THEN ('SI') ELSE ('NO') END) ELSE ('---') END)) ELSE ('---') END) AS SOTTOSOGLIA " _
                       & "FROM SISCOM_MI.DESTINAZIONI_USO_UI,SISCOM_MI.PROGRAMMAZIONE_INTERVENTI,SISCOM_MI.TIPO_DISPONIBILITA,SISCOM_MI.IDENTIFICATIVI_CATASTALI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.PIANI, " _
                       & "SEPA.COMUNI_NAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO, SISCOM_MI.TAB_QUARTIERI " _
                       & "WHERE DESTINAZIONI_USO_UI.ID (+)=UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO AND PROGRAMMAZIONE_INTERVENTI.ID (+)=UNITA_IMMOBILIARI.ID_PRG_EVENTI AND UNITA_IMMOBILIARI.ID_CATASTALE = IDENTIFICATIVI_CATASTALI.ID (+) AND SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD(+) = SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA AND " _
                       & "SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID(+) AND " _
                       & "SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA = SISCOM_MI.SCALE_EDIFICI.ID(+) AND " _
                       & "SISCOM_MI.UNITA_IMMOBILIARI.ID_PIANO = SISCOM_MI.PIANI.ID(+) AND " _
                       & "SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD(+) AND " _
                       & "SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO AND " _
                       & "SISCOM_MI.COMPLESSI_IMMOBILIARI.ID(+) = SISCOM_MI.EDIFICI.ID_COMPLESSO AND " _
                       & "SISCOM_MI.TAB_FILIALI.ID(+) = SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_FILIALE AND " _
                       & "SISCOM_MI.EDIFICI.COD_TIPOLOGIA_IMP_RISCALD = SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO.COD(+) AND " _
                       & "SISCOM_MI.TAB_QUARTIERI.ID(+) = SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_QUARTIERE AND " _
                       & "SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND " _
                       & "SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA(+)=TIPO_DISPONIBILITA.COD "
            If Complesso <> "-1" And Complesso <> "" Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = " & Complesso
                par.cmd.CommandText = "Select denominazione from siscom_mi.complessi_immobiliari where id = " & Complesso
                reader = par.cmd.ExecuteReader
                If reader.Read Then
                    filtroRicerca += "Complesso = " & par.IfNull(reader("denominazione"), "").ToString.ToUpper
                End If
                reader.Close()
            End If
            If Edificio <> "-1" And Edificio <> "" Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.EDIFICI.ID = " & Edificio
                par.cmd.CommandText = "Select denominazione from siscom_mi.edifici where id = " & Edificio
                reader = par.cmd.ExecuteReader
                If reader.Read Then
                    filtroRicerca += "<br/>Edificio = " & par.IfNull(reader("denominazione"), "").ToString.ToUpper
                End If
                reader.Close()
            End If

            If Indirizzo <> "-1" And Indirizzo <> "- - -" Then
                filtroRicerca += "<br/>Indirizzo = " & Indirizzo & ""
                StringaSQL = StringaSQL & " AND SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & par.PulisciStrSql(Indirizzo) & "'"
            End If
            If Civico <> "-1" And Civico <> "" Then
                filtroRicerca += ", civico = " & Civico & ""
                StringaSQL = StringaSQL & " AND SISCOM_MI.INDIRIZZI.CIVICO = '" & par.PulisciStrSql(Civico) & "'"
            End If


            If Scala <> "-1" And Scala <> "" Then

                StringaSQL = StringaSQL & " AND SISCOM_MI.SCALE_EDIFICI.ID = '" & Scala & "'"
                par.cmd.CommandText = "Select descrizione from siscom_mi.scale_edifici where id = " & Scala
                reader = par.cmd.ExecuteReader
                If reader.Read Then
                    filtroRicerca += "<br/>Scala = " & par.IfNull(reader("descrizione"), "").ToString.ToUpper
                End If
                reader.Close()

            End If
            If Interno <> "-1" And Interno <> "" Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.UNITA_IMMOBILIARI.INTERNO = '" & par.PulisciStrSql(Interno) & "'"
                filtroRicerca += "<br/>Interno = " & Interno
            End If
            If Ascensore = 0 Then
                StringaSQL = StringaSQL & " AND (SISCOM_MI.EDIFICI.NUM_ASCENSORI = 0 OR SISCOM_MI.EDIFICI.NUM_ASCENSORI IS NULL) "
                filtroRicerca += "<br/>Ascensore = NO"
            ElseIf Ascensore = 1 Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.EDIFICI.NUM_ASCENSORI > 0 "
                filtroRicerca += "<br/>Ascensore = SI"

            End If
            If Condominio = 1 Then
                StringaSQL = StringaSQL & " AND CONDOMINIO = " & Condominio
                filtroRicerca += "<br/>Condominio = SI"

            ElseIf Condominio = 0 Then
                StringaSQL = StringaSQL & " AND CONDOMINIO = " & Condominio
                filtroRicerca += "<br/>Condominio = NO"
            End If
            If Struttura <> "-1" And Struttura <> "" Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.TAB_FILIALI.ID = " & Struttura

                par.cmd.CommandText = "Select nome from siscom_mi.tab_filiali where id = " & Struttura
                reader = par.cmd.ExecuteReader
                If reader.Read Then
                    filtroRicerca += "<br/>Sede T. = " & par.IfNull(reader("nome"), "").ToString.ToUpper
                End If
                reader.Close()

            End If
            primo = True
            If Tipologia <> "" Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA IN ('" & Replace(Tipologia, ",", "','") & "')"

                par.cmd.CommandText = "Select descrizione from siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI where cod in ('" & Replace(Tipologia, ",", "','") & "')"
                reader = par.cmd.ExecuteReader
                filtroRicerca += "<br/>Tipologia Unità = "
                While reader.Read
                    If primo = True Then
                        filtroRicerca += par.IfNull(reader("descrizione"), "").ToString.ToUpper
                        primo = False
                    Else
                        filtroRicerca += ", " & par.IfNull(reader("descrizione"), "").ToString.ToUpper
                    End If
                End While
                reader.Close()
            End If
            primo = True

            If Disponibilita <> "" Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA IN ('" & Replace(Disponibilita, ",", "','") & "')"

                par.cmd.CommandText = "Select descrizione from siscom_mi.TIPO_DISPONIBILITA where cod IN ('" & Replace(Disponibilita, ",", "','") & "')"
                reader = par.cmd.ExecuteReader
                filtroRicerca += "<br/>Disponibilità = "
                While reader.Read
                    If primo = True Then
                        filtroRicerca += par.IfNull(reader("descrizione"), "").ToString.ToUpper
                        primo = False
                    Else
                        filtroRicerca += ", " & par.IfNull(reader("descrizione"), "").ToString.ToUpper
                    End If

                End While
                reader.Close()
            End If
            primo = True

            If DestUso <> "" Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO IN ('" & Replace(DestUso, ",", "','") & "')"

                par.cmd.CommandText = "Select descrizione from siscom_mi.DESTINAZIONI_USO_UI where id IN ('" & Replace(DestUso, ",", "','") & "')"
                reader = par.cmd.ExecuteReader
                filtroRicerca += "<br/>Dest. Uso = "
                While reader.Read
                    If primo = True Then
                        filtroRicerca += par.IfNull(reader("descrizione"), "").ToString.ToUpper
                        primo = False
                    Else
                        filtroRicerca += ", " & par.IfNull(reader("descrizione"), "").ToString.ToUpper
                    End If
                End While
                reader.Close()


            End If
            primo = True

            Select Case Soglia
                Case -1
                    'tutte
                Case 0
                    StringaSQL &= " AND NVL((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL'),10000)> " & par.VirgoleInPunti(valoreSoglia)
                Case 1
                    StringaSQL &= " AND NVL((SELECT MAX (SISCOM_MI.DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND SISCOM_MI.DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='AL'),10000)<= " & par.VirgoleInPunti(valoreSoglia)
            End Select

            If Quartiere <> "-1" And Quartiere <> "" Then
                StringaSQL = StringaSQL & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_QUARTIERE = " & Quartiere
                par.cmd.CommandText = "Select nome from siscom_mi.TAB_QUARTIERI where id = " & Quartiere
                reader = par.cmd.ExecuteReader
                If reader.Read Then
                    filtroRicerca += "<br/>Quartiere = " & par.IfNull(reader("nome"), "").ToString.ToUpper
                End If
                reader.Close()

            End If
            StringaSQL = StringaSQL & " ORDER BY SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE"
            RiempiDT()
            If ContrattiAperti = "" And ContrattiBozza = "" And ContrattiChiusi = "" Then
                TrovaContratti1()
            Else
                TrovaContratti2()
            End If
            Dim a As String = ""
            If Not String.IsNullOrEmpty(ContrattiChiusi) Then
                filtroRicerca += "<br/>Contratti Chiusi = " & par.FormattaData(ContrattiChiusi)
                If Not String.IsNullOrEmpty(StatoContrattiChiuso) Then
                    a = StatoContrattiChiuso
                    If a.Contains("1") Then
                        filtroRicerca += "Regolari"
                    End If
                    If a.Contains("2") Then
                        filtroRicerca += " Abusivi"
                    End If
                    If a.Contains("3") Then
                        filtroRicerca += " Senza Titolo"
                    End If
                End If
            End If
            If Not String.IsNullOrEmpty(ContrattiAperti) Then
                filtroRicerca += "<br/>Contratti Aperti = " & par.FormattaData(ContrattiChiusi)
                If Not String.IsNullOrEmpty(StatoContrattiAperti) Then
                    a = StatoContrattiAperti
                    If a.Contains("1") Then
                        filtroRicerca += "Regolari"
                    End If
                    If a.Contains("2") Then
                        filtroRicerca += " Abusivi"
                    End If
                    If a.Contains("3") Then
                        filtroRicerca += " Senza Titolo"
                    End If
                End If

            End If
            If Not String.IsNullOrEmpty(ContrattiBozza) Then
                filtroRicerca += "<br/>Contratti Bozza = " & par.FormattaData(ContrattiChiusi)
            End If
            par.cmd.CommandText = StringaSQL
            Titolo.Text += " in " & dtContratti.Rows.Count & " rapporti utenza.<br/>Operazione del: " & Format(Now, "dd/MM/yyyy alle HH:mm:ss") & " Operatore: " & Session.Item("OPERATORE")
            lblSottotitolo.Text = "Filtri di ricerca applicati: <br/>" & filtroRicerca

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Sub RiempiDT()
        Try
            par.cmd.CommandText = StringaSQL
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Titolo.Text += ": " & dt.Rows.Count
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Sub TrovaContratti1()
        Try
            CreaDTContratti()
            Dim row As Data.DataRow
            Dim rowcontratti As Data.DataRow
            For Each row In dt.Rows
                'par.cmd.CommandText = "SELECT SISCOM_MI.RAPPORTI_UTENZA.ID AS ID_CONTRATTO, siscom_mi.rapporti_utenza.cod_contratto, SISCOM_MI.getstatocontratto(siscom_mi.rapporti_utenza.ID) AS stato, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE.descrizione as tipo_contratto, " _
                '                    & "(CASE WHEN siscom_mi.anagrafica.cognome IS NULL AND siscom_mi.anagrafica.NOME IS NULL THEN siscom_mi.anagrafica.RAGIONE_SOCIALE ELSE siscom_mi.anagrafica.cognome || ' ' ||siscom_mi.anagrafica.NOME END) AS INTESTATARIO, " _
                '                    & "siscom_mi.rapporti_utenza.data_stipula, siscom_mi.rapporti_utenza.data_riconsegna " _
                '                    & "FROM siscom_mi.rapporti_utenza, siscom_mi.unita_contrattuale, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                '                    & "WHERE siscom_mi.unita_contrattuale.id_contratto = siscom_mi.rapporti_utenza.ID and SISCOM_MI.RAPPORTI_UTENZA.ID = SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                '                    & "AND SISCOM_MI.ANAGRAFICA.ID = SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SISCOM_MI.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                '                    & "and siscom_mi.rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC = siscom_mi.tipologia_contratto_locazione.cod AND SISCOM_MI.UNITA_CONTRATTUALE.ID_UNITA = " & row.Item("ID_UI") & " " _
                '                    & "order by siscom_mi.rapporti_utenza.cod_contratto desc"
                'par.cmd.ExecuteNonQuery()
                'Dim daquery As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                'Dim dtquery As New Data.DataTable
                'daquery.Fill(dtquery)
                'daquery.Dispose()

                rowcontratti = dtContratti.NewRow
                rowcontratti.Item("CODICE_UI") = row.Item("CODICE_UI")
                rowcontratti.Item("TIPOLOGIA") = row.Item("TIPOLOGIA")
                rowcontratti.Item("DESTINAZIONE_USO") = row.Item("DESTINAZIONE_USO")
                rowcontratti.Item("INDIRIZZO") = row.Item("INDIRIZZO")
                rowcontratti.Item("CIVICO") = row.Item("CIVICO")
                rowcontratti.Item("INTERNO") = row.Item("INTERNO")

                rowcontratti.Item("FOGLIO") = row.Item("FOGLIO")
                rowcontratti.Item("NUMERO") = row.Item("NUMERO")
                rowcontratti.Item("SUB") = row.Item("SUB")

                rowcontratti.Item("SCALA") = row.Item("SCALA")
                rowcontratti.Item("PIANO") = row.Item("PIANO")


                rowcontratti.Item("CAP") = row.Item("CAP")
                rowcontratti.Item("COMUNE") = row.Item("COMUNE")
                rowcontratti.Item("QUARTIERE") = row.Item("QUARTIERE")
                rowcontratti.Item("STRUTTURA") = row.Item("STRUTTURA")
                rowcontratti.Item("COD_EDIFICIO") = row.Item("COD_EDIFICIO")
                rowcontratti.Item("DENOMINAZIONE") = row.Item("DENOMINAZIONE")
                rowcontratti.Item("ASCENSORE") = row.Item("ASCENSORE")
                rowcontratti.Item("RISCAUTO") = row.Item("RISCAUTO")
                rowcontratti.Item("RISCCENT") = row.Item("RISCCENT")
                rowcontratti.Item("SUPNETTA") = row.Item("SUPNETTA")
                rowcontratti.Item("SUPUTILE") = row.Item("SUPUTILE")
                rowcontratti.Item("BALCONI") = row.Item("BALCONI")
                rowcontratti.Item("SUPCOMM") = row.Item("SUPCOMM")
                rowcontratti.Item("SUPESCLUSIVA") = row.Item("SUPESCLUSIVA")
                rowcontratti.Item("STATO_UI") = row.Item("STATO_UI")
                rowcontratti.Item("SOTTOSOGLIA") = row.Item("SOTTOSOGLIA")
                rowcontratti.Item("PROGR_INTERVENTI") = row.Item("PROGR_INTERVENTI")

                rowcontratti.Item("COD_CONTRATTO") = ""
                rowcontratti.Item("STATO_CONTRATTO") = ""
                rowcontratti.Item("TIPO_CONTRATTO") = ""
                rowcontratti.Item("NOMINATIVO") = ""
                rowcontratti.Item("DATA_STIPULA") = ""
                rowcontratti.Item("DATA_CHIUSURA") = ""
                rowcontratti.Item("DATO_CONTRATTO") = ""


                'Dim rowquery As Data.DataRow
                'Dim contacontratto As Integer = 0
                'For Each rowquery In dtquery.Rows
                '    If contacontratto = 0 Then
                '        rowcontratti.Item("COD_CONTRATTO") = rowcontratti.Item("COD_CONTRATTO") & rowquery.Item("COD_CONTRATTO")
                '        rowcontratti.Item("STATO_CONTRATTO") = rowcontratti.Item("STATO_CONTRATTO") & rowquery.Item("STATO")
                '        rowcontratti.Item("TIPO_CONTRATTO") = rowcontratti.Item("TIPO_CONTRATTO") & rowquery.Item("TIPO_CONTRATTO")
                '        rowcontratti.Item("NOMINATIVO") = rowcontratti.Item("NOMINATIVO") & rowquery.Item("INTESTATARIO")
                '        rowcontratti.Item("DATA_STIPULA") = rowcontratti.Item("DATA_STIPULA") & par.FormattaData(rowquery.Item("DATA_STIPULA").ToString)
                '        rowcontratti.Item("DATA_CHIUSURA") = rowcontratti.Item("DATA_CHIUSURA") & par.FormattaData(rowquery.Item("DATA_RICONSEGNA").ToString)
                '        rowcontratti.Item("DATO_CONTRATTO") = ""
                '    Else
                '        dtContratti.Rows.Add(rowcontratti)
                '        rowcontratti = dtContratti.NewRow
                '        rowcontratti.Item("CODICE_UI") = ""
                '        rowcontratti.Item("TIPOLOGIA") = ""
                '        rowcontratti.Item("DESTINAZIONE_USO") = ""
                '        rowcontratti.Item("INDIRIZZO") = ""
                '        rowcontratti.Item("CIVICO") = ""
                '        rowcontratti.Item("INTERNO") = ""
                '        rowcontratti.Item("SCALA") = ""
                '        rowcontratti.Item("PIANO") = ""
                '        rowcontratti.Item("CAP") = ""
                '        rowcontratti.Item("COMUNE") = ""
                '        rowcontratti.Item("QUARTIERE") = ""
                '        rowcontratti.Item("STRUTTURA") = ""
                '        rowcontratti.Item("COD_EDIFICIO") = ""
                '        rowcontratti.Item("DENOMINAZIONE") = ""
                '        rowcontratti.Item("ASCENSORE") = ""
                '        rowcontratti.Item("RISCAUTO") = ""
                '        rowcontratti.Item("RISCCENT") = ""
                '        rowcontratti.Item("SUPNETTA") = ""
                '        rowcontratti.Item("SUPUTILE") = ""
                '        rowcontratti.Item("BALCONI") = ""
                '        rowcontratti.Item("SUPCOMM") = ""
                '        rowcontratti.Item("SUPESCLUSIVA") = ""
                '        rowcontratti.Item("COD_CONTRATTO") = rowcontratti.Item("COD_CONTRATTO") & rowquery.Item("COD_CONTRATTO")
                '        rowcontratti.Item("STATO_CONTRATTO") = rowcontratti.Item("STATO_CONTRATTO") & rowquery.Item("STATO")
                '        rowcontratti.Item("TIPO_CONTRATTO") = rowcontratti.Item("TIPO_CONTRATTO") & rowquery.Item("TIPO_CONTRATTO")
                '        rowcontratti.Item("NOMINATIVO") = rowcontratti.Item("NOMINATIVO") & rowquery.Item("INTESTATARIO")
                '        rowcontratti.Item("DATA_STIPULA") = rowcontratti.Item("DATA_STIPULA") & par.FormattaData(rowquery.Item("DATA_STIPULA").ToString)
                '        rowcontratti.Item("DATA_CHIUSURA") = rowcontratti.Item("DATA_CHIUSURA") & par.FormattaData(rowquery.Item("DATA_RICONSEGNA").ToString)
                '        rowcontratti.Item("DATO_CONTRATTO") = ""
                '    End If
                'contacontratto = contacontratto + 1
                'Next
                dtContratti.Rows.Add(rowcontratti)
            Next
            par.cmd.Dispose()
            Session.Add("DATATABLEREPORTCONTRATTIUI", dtContratti)
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Function RestringiFiltro() As String
        RestringiFiltro = ""
        Dim passaggio As Integer = 0
        If ContrattiChiusi <> "" Then
            If ContrattiAperti = "" And ContrattiBozza = "" Then
                If StatoContrattiChiuso <> "" And StatoContrattiChiuso <> "1,2,3" Then
                    RestringiFiltro = RestringiFiltro & "AND (data_riconsegna <= '" & ContrattiChiusi & "' AND " & StatoContratto("SCC") & " ) "
                Else
                    RestringiFiltro = RestringiFiltro & "AND (data_riconsegna <= '" & ContrattiChiusi & "') "
                End If
            Else
                If StatoContrattiChiuso <> "" And StatoContrattiChiuso <> "1,2,3" Then
                    RestringiFiltro = RestringiFiltro & "AND ((data_riconsegna <= '" & ContrattiChiusi & "' AND " & StatoContratto("SCC") & " ) "
                Else
                    RestringiFiltro = RestringiFiltro & "AND ((data_riconsegna <= '" & ContrattiChiusi & "') "
                End If
            End If
            passaggio = 1
        End If
        If ContrattiAperti <> "" Then
            If passaggio = 0 Then
                If ContrattiBozza = "" Then
                    If StatoContrattiAperti <> "" And StatoContrattiAperti <> "1,2,3" Then
                        RestringiFiltro = RestringiFiltro & "AND (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                  & "and (rapporti_utenza.id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                  & "substr(data_ora,1,8)> '" & ContrattiAperti & "')) AND " & StatoContratto("SCA") & " ) "
                    Else
                        RestringiFiltro = RestringiFiltro & "AND (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                  & "and (rapporti_utenza.id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                  & "substr(data_ora,1,8)> '" & ContrattiAperti & "'))) "
                    End If
                Else
                    If StatoContrattiAperti <> "" And StatoContrattiAperti <> "1,2,3" Then
                        RestringiFiltro = RestringiFiltro & "AND ((data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                  & "and (rapporti_utenza.id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                  & "substr(data_ora,1,8)> '" & ContrattiAperti & "')) AND " & StatoContratto("SCA") & " ) "
                    Else
                        RestringiFiltro = RestringiFiltro & "AND ((data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                  & "and (rapporti_utenza.id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                  & "substr(data_ora,1,8)> '" & ContrattiAperti & "'))) "
                    End If
                End If
                passaggio = 1
            Else
                If ContrattiBozza = "" Then
                    If StatoContrattiAperti <> "" And StatoContrattiAperti <> "1,2,3" Then
                        RestringiFiltro = RestringiFiltro & "OR (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                  & "and (rapporti_utenza.id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                  & "substr(data_ora,1,8)> '" & ContrattiAperti & "'))) AND " & StatoContratto("SCA") & " ) "
                    Else
                        RestringiFiltro = RestringiFiltro & "OR (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                  & "and (rapporti_utenza.id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                  & "substr(data_ora,1,8)> '" & ContrattiAperti & "')))) "
                    End If

                Else
                    If StatoContrattiAperti <> "" And StatoContrattiAperti <> "1,2,3" Then
                        RestringiFiltro = RestringiFiltro & "OR (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                  & "and (rapporti_utenza.id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                  & "substr(data_ora,1,8)> '" & ContrattiAperti & "')) AND " & StatoContratto("SCA") & " ) "
                    Else
                        RestringiFiltro = RestringiFiltro & "OR (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                  & "and (rapporti_utenza.id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                  & "substr(data_ora,1,8)> '" & ContrattiAperti & "'))) "
                    End If
                End If
            End If

        End If
        If ContrattiBozza <> "" Then
            If passaggio = 0 Then
                RestringiFiltro = RestringiFiltro & "AND (data_decorrenza<= '" & ContrattiBozza & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiBozza & "') " _
                                                          & "and rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiBozza & "') and " _
                                                          & "rapporti_utenza.id  in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and substr(data_ora,1,8)> '" & ContrattiBozza & "')) "
                passaggio = 1
            Else
                RestringiFiltro = RestringiFiltro & "OR (data_decorrenza<= '" & ContrattiBozza & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiBozza & "') " _
                                                          & "and rapporti_utenza.id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiBozza & "') and " _
                                                          & "rapporti_utenza.id  in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and substr(data_ora,1,8)> '" & ContrattiBozza & "'))) "
            End If

        End If
        Return RestringiFiltro
    End Function
    Private Sub TrovaContratti2()
        Try
            CreaDTContratti()
            Dim row As Data.DataRow
            Dim rowquery As Data.DataRow
            Dim rowcontratti As Data.DataRow
            Dim ControllaFiltro As Boolean = False
            Dim AddQuery As String = ""
            For Each row In dt.Rows
                AddQuery = ""
                '09/01/2013 puccia ☺

                If Request.QueryString("ONFILT") = 0 Then
                    '10/01/2013 puccia ☺

                    AddQuery = RestringiFiltro()

                    ''solo chiusi
                    'If Not String.IsNullOrEmpty(ContrattiChiusi) Then
                    '    AddQuery = AddQuery & " rapporti_utenza.data_riconsegna <='" & ContrattiChiusi & "'  "
                    '    If Not String.IsNullOrEmpty(StatoContratto("SCC")) Then
                    '        AddQuery = AddQuery & " AND " & StatoContratto("SCC") & " "
                    '    End If
                    'End If
                    'solo aperti
                    'If Not String.IsNullOrEmpty(ContrattiAperti) Then
                    '    AddQuery = AddQuery & " OR (rapporti_utenza.data_stipula <='" & ContrattiAperti & "' and  (rapporti_utenza.data_riconsegna >='" & ContrattiAperti & "' or data_riconsegna is null))"
                    '    If Not String.IsNullOrEmpty(StatoContratto("SCA")) Then
                    '        AddQuery = AddQuery & " AND " & StatoContratto("SCA") & " "
                    '    End If
                    'End If
                    ' ''solo bozza
                    'If Not String.IsNullOrEmpty(ContrattiBozza) Then
                    '    AddQuery = AddQuery & " OR (data_decorrenza<= '" & ContrattiBozza & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiBozza & "')) "
                    'End If
                    'AddQuery = AddQuery & ")"
                End If

                par.cmd.CommandText = "SELECT SISCOM_MI.RAPPORTI_UTENZA.ID AS ID_CONTRATTO, siscom_mi.rapporti_utenza.cod_contratto, SISCOM_MI.getstatocontratto(siscom_mi.rapporti_utenza.ID) AS stato, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE.descrizione as tipo_contratto, " _
                                    & "(CASE WHEN siscom_mi.anagrafica.cognome IS NULL AND siscom_mi.anagrafica.NOME IS NULL THEN siscom_mi.anagrafica.RAGIONE_SOCIALE ELSE siscom_mi.anagrafica.cognome || ' ' ||siscom_mi.anagrafica.NOME END) AS INTESTATARIO, " _
                                    & "siscom_mi.rapporti_utenza.data_stipula, siscom_mi.rapporti_utenza.data_riconsegna " _
                                    & "FROM siscom_mi.rapporti_utenza, siscom_mi.unita_contrattuale, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE siscom_mi.unita_contrattuale.id_contratto = siscom_mi.rapporti_utenza.ID and SISCOM_MI.RAPPORTI_UTENZA.ID = SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                                    & "AND SISCOM_MI.ANAGRAFICA.ID = SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SISCOM_MI.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & "and siscom_mi.rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC = siscom_mi.tipologia_contratto_locazione.cod AND SISCOM_MI.UNITA_CONTRATTUALE.ID_UNITA = " & row.Item("ID_UI") & " " & AddQuery & " " _
                                    & "order by siscom_mi.rapporti_utenza.cod_contratto desc"
                Dim daquery As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtquery As New Data.DataTable
                daquery.Fill(dtquery)
                ControllaFiltro = False
                rowquery = Nothing
                rowcontratti = Nothing
                For Each rowquery In dtquery.Rows
                    If DatoContratto(rowquery.Item("ID_CONTRATTO").ToString) <> "" Then
                        ControllaFiltro = True
                    End If
                Next
                If ControllaFiltro = True Then
                    rowcontratti = dtContratti.NewRow
                    rowcontratti.Item("CODICE_UI") = row.Item("CODICE_UI")
                    rowcontratti.Item("TIPOLOGIA") = row.Item("TIPOLOGIA")
                    rowcontratti.Item("DESTINAZIONE_USO") = row.Item("DESTINAZIONE_USO")
                    rowcontratti.Item("INDIRIZZO") = row.Item("INDIRIZZO")
                    rowcontratti.Item("CIVICO") = row.Item("CIVICO")
                    rowcontratti.Item("INTERNO") = row.Item("INTERNO")
                    rowcontratti.Item("SCALA") = row.Item("SCALA")
                    rowcontratti.Item("PIANO") = row.Item("PIANO")
                    rowcontratti.Item("CAP") = row.Item("CAP")
                    rowcontratti.Item("COMUNE") = row.Item("COMUNE")
                    rowcontratti.Item("QUARTIERE") = row.Item("QUARTIERE")
                    rowcontratti.Item("STRUTTURA") = row.Item("STRUTTURA")
                    rowcontratti.Item("COD_EDIFICIO") = row.Item("COD_EDIFICIO")
                    rowcontratti.Item("DENOMINAZIONE") = row.Item("DENOMINAZIONE")
                    rowcontratti.Item("ASCENSORE") = row.Item("ASCENSORE")
                    rowcontratti.Item("RISCAUTO") = row.Item("RISCAUTO")
                    rowcontratti.Item("RISCCENT") = row.Item("RISCCENT")
                    rowcontratti.Item("SUPNETTA") = row.Item("SUPNETTA")
                    rowcontratti.Item("SUPUTILE") = row.Item("SUPUTILE")
                    rowcontratti.Item("BALCONI") = row.Item("BALCONI")
                    rowcontratti.Item("SUPCOMM") = row.Item("SUPCOMM")
                    rowcontratti.Item("SUPESCLUSIVA") = row.Item("SUPESCLUSIVA")
                    rowcontratti.Item("PROGR_INTERVENTI") = row.Item("PROGR_INTERVENTI")
                    Dim contacontratto As Integer = 0
                    For Each rowquery In dtquery.Rows
                        If contacontratto = 0 Then
                            rowcontratti.Item("COD_CONTRATTO") = rowcontratti.Item("COD_CONTRATTO") & rowquery.Item("COD_CONTRATTO")
                            rowcontratti.Item("STATO_CONTRATTO") = rowcontratti.Item("STATO_CONTRATTO") & rowquery.Item("STATO")
                            rowcontratti.Item("TIPO_CONTRATTO") = rowcontratti.Item("TIPO_CONTRATTO") & rowquery.Item("TIPO_CONTRATTO")
                            rowcontratti.Item("NOMINATIVO") = rowcontratti.Item("NOMINATIVO") & rowquery.Item("INTESTATARIO")
                            rowcontratti.Item("DATA_STIPULA") = rowcontratti.Item("DATA_STIPULA") & par.FormattaData(rowquery.Item("DATA_STIPULA").ToString)
                            rowcontratti.Item("DATA_CHIUSURA") = rowcontratti.Item("DATA_CHIUSURA") & par.FormattaData(rowquery.Item("DATA_RICONSEGNA").ToString)
                            If ContrattiAperti = "" And ContrattiBozza = "" And ContrattiChiusi = "" Then
                                rowcontratti.Item("DATO_CONTRATTO") = ""
                            Else
                                rowcontratti.Item("DATO_CONTRATTO") = DatoContratto(rowquery.Item("ID_CONTRATTO").ToString)
                            End If
                        Else
                            dtContratti.Rows.Add(rowcontratti)
                            rowcontratti = dtContratti.NewRow
                            rowcontratti.Item("CODICE_UI") = ""
                            rowcontratti.Item("TIPOLOGIA") = ""
                            rowcontratti.Item("DESTINAZIONE_USO") = ""
                            rowcontratti.Item("INDIRIZZO") = ""
                            rowcontratti.Item("CIVICO") = ""
                            rowcontratti.Item("INTERNO") = ""
                            rowcontratti.Item("SCALA") = ""
                            rowcontratti.Item("PIANO") = ""
                            rowcontratti.Item("CAP") = ""
                            rowcontratti.Item("COMUNE") = ""
                            rowcontratti.Item("QUARTIERE") = ""
                            rowcontratti.Item("STRUTTURA") = ""
                            rowcontratti.Item("COD_EDIFICIO") = ""
                            rowcontratti.Item("DENOMINAZIONE") = ""
                            rowcontratti.Item("ASCENSORE") = ""
                            rowcontratti.Item("RISCAUTO") = ""
                            rowcontratti.Item("RISCCENT") = ""
                            rowcontratti.Item("SUPNETTA") = ""
                            rowcontratti.Item("SUPUTILE") = ""
                            rowcontratti.Item("BALCONI") = ""
                            rowcontratti.Item("SUPCOMM") = ""
                            rowcontratti.Item("SUPESCLUSIVA") = ""
                            rowcontratti.Item("COD_CONTRATTO") = rowcontratti.Item("COD_CONTRATTO") & rowquery.Item("COD_CONTRATTO")
                            rowcontratti.Item("STATO_CONTRATTO") = rowcontratti.Item("STATO_CONTRATTO") & rowquery.Item("STATO")
                            rowcontratti.Item("TIPO_CONTRATTO") = rowcontratti.Item("TIPO_CONTRATTO") & rowquery.Item("TIPO_CONTRATTO")
                            rowcontratti.Item("NOMINATIVO") = rowcontratti.Item("NOMINATIVO") & rowquery.Item("INTESTATARIO")
                            rowcontratti.Item("DATA_STIPULA") = rowcontratti.Item("DATA_STIPULA") & par.FormattaData(rowquery.Item("DATA_STIPULA").ToString)
                            rowcontratti.Item("DATA_CHIUSURA") = rowcontratti.Item("DATA_CHIUSURA") & par.FormattaData(rowquery.Item("DATA_RICONSEGNA").ToString)
                            If ContrattiAperti = "" And ContrattiBozza = "" And ContrattiChiusi = "" Then
                                rowcontratti.Item("DATO_CONTRATTO") = ""
                            Else
                                rowcontratti.Item("DATO_CONTRATTO") = DatoContratto(rowquery.Item("ID_CONTRATTO").ToString)
                            End If
                        End If
                        contacontratto = contacontratto + 1
                    Next
                    dtContratti.Rows.Add(rowcontratti)
                End If
            Next
            Session.Add("DATATABLEREPORTCONTRATTIUI", dtContratti)
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Function DatoContratto(ByVal idcontratto As String) As String
        DatoContratto = ""
        Try
            Dim passaggio As Integer = 0
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & idcontratto & " "
            If ContrattiChiusi <> "" Then
                If ContrattiAperti = "" And ContrattiBozza = "" Then
                    If StatoContrattiChiuso <> "" And StatoContrattiChiuso <> "1,2,3" Then
                        par.cmd.CommandText = par.cmd.CommandText & "AND (data_riconsegna <= '" & ContrattiChiusi & "' AND " & StatoContratto("SCC") & " ) "
                    Else
                        par.cmd.CommandText = par.cmd.CommandText & "AND (data_riconsegna <= '" & ContrattiChiusi & "') "
                    End If
                Else
                    If StatoContrattiChiuso <> "" And StatoContrattiChiuso <> "1,2,3" Then
                        par.cmd.CommandText = par.cmd.CommandText & "AND ((data_riconsegna <= '" & ContrattiChiusi & "' AND " & StatoContratto("SCC") & " ) "
                    Else
                        par.cmd.CommandText = par.cmd.CommandText & "AND ((data_riconsegna <= '" & ContrattiChiusi & "') "
                    End If
                End If
                passaggio = 1
            End If
            If ContrattiAperti <> "" Then
                If passaggio = 0 Then
                    If ContrattiBozza = "" Then
                        If StatoContrattiAperti <> "" And StatoContrattiAperti <> "1,2,3" Then
                            par.cmd.CommandText = par.cmd.CommandText & "AND (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                      & "and (id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                      & "substr(data_ora,1,8)> '" & ContrattiAperti & "')) AND " & StatoContratto("SCA") & " ) "
                        Else
                            par.cmd.CommandText = par.cmd.CommandText & "AND (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                      & "and (id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                      & "substr(data_ora,1,8)> '" & ContrattiAperti & "'))) "
                        End If
                    Else
                        If StatoContrattiAperti <> "" And StatoContrattiAperti <> "1,2,3" Then
                            par.cmd.CommandText = par.cmd.CommandText & "AND ((data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                      & "and (id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                      & "substr(data_ora,1,8)> '" & ContrattiAperti & "')) AND " & StatoContratto("SCA") & " ) "
                        Else
                            par.cmd.CommandText = par.cmd.CommandText & "AND ((data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                      & "and (id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                      & "substr(data_ora,1,8)> '" & ContrattiAperti & "'))) "
                        End If
                    End If
                    passaggio = 1
                Else
                    If ContrattiBozza = "" Then
                        If StatoContrattiAperti <> "" And StatoContrattiAperti <> "1,2,3" Then
                            par.cmd.CommandText = par.cmd.CommandText & "OR (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                      & "and (id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                      & "substr(data_ora,1,8)> '" & ContrattiAperti & "'))) AND " & StatoContratto("SCA") & " ) "
                        Else
                            par.cmd.CommandText = par.cmd.CommandText & "OR (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                      & "and (id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                      & "substr(data_ora,1,8)> '" & ContrattiAperti & "')))) "
                        End If

                    Else
                        If StatoContrattiAperti <> "" And StatoContrattiAperti <> "1,2,3" Then
                            par.cmd.CommandText = par.cmd.CommandText & "OR (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                      & "and (id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                      & "substr(data_ora,1,8)> '" & ContrattiAperti & "')) AND " & StatoContratto("SCA") & " ) "
                        Else
                            par.cmd.CommandText = par.cmd.CommandText & "OR (data_decorrenza<= '" & ContrattiAperti & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiAperti & "') " _
                                                                      & "and (id in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiAperti & "') or id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and " _
                                                                      & "substr(data_ora,1,8)> '" & ContrattiAperti & "'))) "
                        End If
                    End If
                End If
            End If
            If ContrattiBozza <> "" Then
                If passaggio = 0 Then
                    par.cmd.CommandText = par.cmd.CommandText & "AND (data_decorrenza<= '" & ContrattiBozza & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiBozza & "') " _
                                                              & "and id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiBozza & "') and " _
                                                              & "id  in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and substr(data_ora,1,8)> '" & ContrattiBozza & "')) "
                    passaggio = 1
                Else
                    par.cmd.CommandText = par.cmd.CommandText & "OR (data_decorrenza<= '" & ContrattiBozza & "' and ( data_riconsegna  is null or  data_riconsegna >'" & ContrattiBozza & "') " _
                                                              & "and id not in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F04' and substr(data_ora,1,8)<= '" & ContrattiBozza & "') and " _
                                                              & "id  in (select id_Contratto from siscom_mi.eventi_contratti where cod_evento='F01' and substr(data_ora,1,8)> '" & ContrattiBozza & "'))) "
                End If
            End If
            Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myreader.Read Then
                DatoContratto = "CONTRATTO CON FILTRO"
            Else
                DatoContratto = ""
            End If
            myreader.Close()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
            Return ""
        End Try
        Return DatoContratto
    End Function
    Private Function StatoContratto(ByVal Stato As String) As String
        StatoContratto = ""
        Try
            If Stato = "SCC" Then
                Select Case StatoContrattiChiuso
                    Case "1"
                        StatoContratto = "COD_TIPOLOGIA_CONTR_LOC <> 'NONE' AND DATA_SCADENZA > " & ContrattiChiusi & " AND (DATA_RICONSEGNA IS NOT NULL OR DATA_RICONSEGNA < " & ContrattiChiusi & ")"
                    Case "2"
                        StatoContratto = "COD_TIPOLOGIA_CONTR_LOC = 'NONE'"
                    Case "3"
                        StatoContratto = "DATA_SCADENZA < " & ContrattiChiusi & " AND (DATA_RICONSEGNA IS NULL OR DATA_RICONSEGNA > " & ContrattiChiusi & ")"
                    Case "1,2"
                        StatoContratto = "((COD_TIPOLOGIA_CONTR_LOC <> 'NONE' AND DATA_SCADENZA > " & ContrattiChiusi & " AND (DATA_RICONSEGNA IS NOT NULL OR DATA_RICONSEGNA < " & ContrattiChiusi & ")) OR (COD_TIPOLOGIA_CONTR_LOC = 'NONE'))"
                    Case "1,3"
                        StatoContratto = "((COD_TIPOLOGIA_CONTR_LOC <> 'NONE' AND DATA_SCADENZA > " & ContrattiChiusi & " AND (DATA_RICONSEGNA IS NOT NULL OR DATA_RICONSEGNA < " & ContrattiChiusi & ")) OR (DATA_SCADENZA < " & ContrattiChiusi & " AND (DATA_RICONSEGNA IS NULL OR DATA_RICONSEGNA > " & ContrattiChiusi & ")))"
                    Case "2,3"
                        StatoContratto = "((COD_TIPOLOGIA_CONTR_LOC = 'NONE') OR (DATA_SCADENZA < " & ContrattiChiusi & " AND (DATA_RICONSEGNA IS NULL OR DATA_RICONSEGNA > " & ContrattiChiusi & ")))"
                    Case Else

                End Select
            Else
                Select Case StatoContrattiAperti
                    Case "1"
                        StatoContratto = "COD_TIPOLOGIA_CONTR_LOC <> 'NONE' AND DATA_SCADENZA > " & ContrattiAperti & " AND (DATA_RICONSEGNA IS NOT NULL OR DATA_RICONSEGNA < " & ContrattiAperti & ")"
                    Case "2"
                        StatoContratto = "COD_TIPOLOGIA_CONTR_LOC = 'NONE'"
                    Case "3"
                        StatoContratto = "DATA_SCADENZA < " & ContrattiAperti & " AND (DATA_RICONSEGNA IS NULL OR DATA_RICONSEGNA > " & ContrattiAperti & ")"
                    Case "1,2"
                        StatoContratto = "((COD_TIPOLOGIA_CONTR_LOC <> 'NONE' AND DATA_SCADENZA > " & ContrattiAperti & " AND (DATA_RICONSEGNA IS NOT NULL OR DATA_RICONSEGNA < " & ContrattiAperti & ")) OR (COD_TIPOLOGIA_CONTR_LOC = 'NONE'))"
                    Case "1,3"
                        StatoContratto = "((COD_TIPOLOGIA_CONTR_LOC <> 'NONE' AND DATA_SCADENZA > " & ContrattiAperti & " AND (DATA_RICONSEGNA IS NOT NULL OR DATA_RICONSEGNA < " & ContrattiAperti & ")) OR (DATA_SCADENZA < " & ContrattiAperti & " AND (DATA_RICONSEGNA IS NULL OR DATA_RICONSEGNA > " & ContrattiAperti & ")))"
                    Case "2,3"
                        StatoContratto = "((COD_TIPOLOGIA_CONTR_LOC = 'NONE') OR (DATA_SCADENZA < " & ContrattiAperti & " AND (DATA_RICONSEGNA IS NULL OR DATA_RICONSEGNA > " & ContrattiAperti & ")))"
                    Case Else

                End Select
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
            Return ""
        End Try
        Return StatoContratto
    End Function
    Private Sub CreaDTContratti()
        Try
            '######### SVUOTA E CREA COLONNE DATATABLE #########
            dtContratti.Clear()
            dtContratti.Columns.Clear()
            dtContratti.Rows.Clear()
            dtContratti.Columns.Add("CODICE_UI")
            dtContratti.Columns.Add("COD_EDIFICIO")
            dtContratti.Columns.Add("DENOMINAZIONE")
            dtContratti.Columns.Add("TIPOLOGIA")
            dtContratti.Columns.Add("DESTINAZIONE_USO")
            dtContratti.Columns.Add("INDIRIZZO")
            dtContratti.Columns.Add("CIVICO")
            dtContratti.Columns.Add("CAP")
            dtContratti.Columns.Add("COMUNE")
            dtContratti.Columns.Add("QUARTIERE")
            dtContratti.Columns.Add("SCALA")
            dtContratti.Columns.Add("PIANO")
            dtContratti.Columns.Add("INTERNO")

            dtContratti.Columns.Add("FOGLIO")
            dtContratti.Columns.Add("NUMERO")
            dtContratti.Columns.Add("SUB")

            dtContratti.Columns.Add("STRUTTURA")
            dtContratti.Columns.Add("ASCENSORE")
            dtContratti.Columns.Add("RISCAUTO")
            dtContratti.Columns.Add("RISCCENT")
            dtContratti.Columns.Add("SUPNETTA")
            dtContratti.Columns.Add("SUPUTILE")
            dtContratti.Columns.Add("BALCONI")
            dtContratti.Columns.Add("SUPCOMM")
            dtContratti.Columns.Add("SUPESCLUSIVA")
            dtContratti.Columns.Add("COD_CONTRATTO")
            dtContratti.Columns.Add("STATO_CONTRATTO")
            dtContratti.Columns.Add("TIPO_CONTRATTO")
            dtContratti.Columns.Add("NOMINATIVO")
            dtContratti.Columns.Add("DATA_STIPULA")
            dtContratti.Columns.Add("DATA_CHIUSURA")
            dtContratti.Columns.Add("DATO_CONTRATTO")
            dtContratti.Columns.Add("STATO_UI")
            dtContratti.Columns.Add("SOTTOSOGLIA")
            dtContratti.Columns.Add("PROGR_INTERVENTI")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dgvRptUnitaImm.DataSource = Session.Item("DATATABLEREPORTCONTRATTIUI")
            dgvRptUnitaImm.DataBind()
            If dgvRptUnitaImm.Items.Count = 0 Then
                Response.Write("<script>alert('I filtri di ricerca inseriti non hanno prodotto risultati!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Protected Sub dgvRptUnitaImm_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvRptUnitaImm.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvRptUnitaImm.CurrentPageIndex = e.NewPageIndex
            Response.Flush()
            BindGrid()
        End If
    End Sub
    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Response.Flush()
        Session.Remove("DATATABLEREPORTCONTRATTIUI")
        Response.Write("<script>self.close();</script>")
    End Sub
    Protected Sub btnport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnport.Click
        Try
            Dim dtexport As New Data.DataTable
            dtexport = Session.Item("DATATABLEREPORTCONTRATTIUI")
            Dim nomefile As String = DataTableALCSV(dtexport, "ExportRptUnitaImmobiliari", ";")
            If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                Response.Redirect("..\/FileTemp\/" & nomefile, False)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
            'If dgvRptUnitaImm.Items.Count > 0 Then
            '    Dim nomefile As String = EsportaExcelDaDT(Session.Item("DATATABLEREPORTCONTRATTIUI"), "ExportRptUnitaImmobiliari")
            '    If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
            '        Response.Redirect("..\/FileTemp\/" & nomefile, False)
            '    Else
            '        Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            '    End If
            'Else
            '    Response.Write("<script>alert('Nessun risultato da esportare!');</script>")
            'End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvRptUnitaImm_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvRptUnitaImm.ItemDataBound
        If e.Item.ItemType Then
            If e.Item.Cells(0).Text <> "&nbsp;" Then
                If backcolor = 0 Then
                    e.Item.BackColor = Drawing.Color.LightCyan
                    backcolor = 1
                Else
                    e.Item.BackColor = Drawing.Color.White
                    backcolor = 0
                End If
            Else
                If backcolor = 0 Then
                    e.Item.BackColor = Drawing.Color.White
                Else
                    e.Item.BackColor = Drawing.Color.LightCyan
                End If
            End If
            If e.Item.Cells(27).Text = "CONTRATTO CON FILTRO" Then
                e.Item.Cells(21).BackColor = Drawing.Color.LightGreen
                e.Item.Cells(22).BackColor = Drawing.Color.LightGreen
                e.Item.Cells(23).BackColor = Drawing.Color.LightGreen
                e.Item.Cells(24).BackColor = Drawing.Color.LightGreen
                e.Item.Cells(25).BackColor = Drawing.Color.LightGreen
                e.Item.Cells(26).BackColor = Drawing.Color.LightGreen
            Else
                'cancella riga perchè senza filtro
            End If
        End If
    End Sub
    Function DataTableALCSV(ByVal table As Data.DataTable, ByVal filename As String, ByVal sepcar As String, Optional ByVal creazip As Boolean = True) As String
        Dim sr As System.IO.StreamWriter = Nothing
        Dim sep As String = sepcar
        Dim intestazione As String = ""
        Dim flag_inizio As Integer = 0
        Dim indiceRighe As Long = 0
        Dim nome As String = filename & Format(Now, "yyyyMMddHHmmss")
        Try
            'CREO IL FILE CSV
            If table.Rows.Count <= 65536 Then
                Dim nomefile As String = nome & ".csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    Dim stringa As String = ""
                    For Each col As Data.DataColumn In table.Columns
                        If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                            If row(col.ColumnName).ToString <> "" Then
                                stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                stringa = par.RimuoviHTML(stringa)
                            Else
                                stringa = stringa & "" & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Else
                            stringa = stringa & row(col.ColumnName) & sep
                            stringa = par.RimuoviHTML(stringa)
                        End If
                    Next
                    sr.WriteLine(stringa)
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Dim FileNameCSV As String = nomefile
                Return FileNameCSV
            Else
                Dim nomefile1 As String = nome & "_1.csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile1))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    If indiceRighe <= 65535 Then
                        Dim stringa As String = ""
                        For Each col As Data.DataColumn In table.Columns
                            If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                                If row(col.ColumnName).ToString <> "" Then
                                    stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                Else
                                    stringa = stringa & "" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                End If
                            Else
                                stringa = stringa & row(col.ColumnName) & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Next
                        sr.WriteLine(stringa)
                    End If
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Dim nomefile2 As String = nome & "_2.csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile2))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    If indiceRighe > 65535 Then
                        Dim stringa As String = ""
                        For Each col As Data.DataColumn In table.Columns
                            If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                                If row(col.ColumnName).ToString <> "" Then
                                    stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                Else
                                    stringa = stringa & "" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                End If
                            Else
                                stringa = stringa & row(col.ColumnName) & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Next
                        sr.WriteLine(stringa)
                    End If
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                'CREAZIONE FILE ZIP
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String
                zipfic = Server.MapPath("..\FileTemp\" & nome & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & nomefile1)
                Dim strmFile As FileStream = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                Dim sFile As String = Path.GetFileName(strFile)
                Dim theEntry As ZipEntry = New ZipEntry(sFile)
                Dim fi As New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                File.Delete(strFile)
                strFile = Server.MapPath("..\FileTemp\" & nomefile2)
                strmFile = File.OpenRead(strFile)
                Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
                Dim sFile1 As String = Path.GetFileName(strFile)
                theEntry = New ZipEntry(sFile1)
                fi = New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer1)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
                File.Delete(strFile)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                Dim FileNameZip As String = nome & ".zip"
                Return FileNameZip
            End If
        Catch ex As Exception
            If Not sr Is Nothing Then
                sr.Close()
            End If
            Return ""
        End Try
    End Function
    'Function EsportaExcelDaDT(ByVal dt As Data.DataTable, ByVal nomeFile As String, Optional ByVal EliminazioneLink As Boolean = True) As String
    '    Try
    '        Dim NumeroColonneDT As Integer = dt.Columns.Count
    '        Dim FileExcel As New CM.ExcelFile
    '        Dim indiceRighe As Long = 0
    '        Dim IndiceColonne As Integer = 1
    '        Dim IndiceIntestazione As Integer = 1
    '        Dim FileName As String = nomeFile & Format(Now, "yyyyMMddHHmmss")
    '        indiceRighe = 0
    '        With FileExcel
    '            'CREO IL FILE 
    '            .CreateFile(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls"))
    '            .PrintGridLines = False
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
    '            .SetDefaultRowHeight(14)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
    '            For j = 0 To NumeroColonneDT - 1 Step 1
    '                If j < 27 Then
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, IndiceColonne, IndiceIntestazione, dt.Columns.Item(j).ColumnName, 0)
    '                End If
    '                IndiceIntestazione = IndiceIntestazione + 1
    '            Next
    '            indiceRighe = indiceRighe + 1
    '            For Each riga As Data.DataRow In dt.Rows
    '                indiceRighe = indiceRighe + 1
    '                Dim Cella As Integer = 0
    '                For IndiceColonne = 0 To NumeroColonneDT - 1
    '                    Select Case IndiceColonne
    '                        Case 0 'codice unita_immobiliare
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 1 'codice edificio
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 2 'denominazione
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 3 'tipologia
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 4 'indirizzo
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 5 'civico
    '                            If riga.Item(IndiceColonne).ToString <> "" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 1)
    '                            End If
    '                        Case 6 'cap
    '                            If riga.Item(IndiceColonne).ToString <> "" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 1)
    '                            End If
    '                        Case 7 'comune
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 8 'quartiere
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 9 'scala
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 10 'piano
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 11 'Interno
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 12 'struttura
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 13 'ascensore
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 14 'risc. automatico
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 15 'risc. centr.
    '                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                        Case 16 'sup.netta
    '                            If riga.Item(IndiceColonne).ToString <> "" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 4)
    '                            End If
    '                        Case 17 'sup. utile
    '                            If riga.Item(IndiceColonne).ToString <> "" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 4)
    '                            End If
    '                        Case 18 'sup. balconi
    '                            If riga.Item(IndiceColonne).ToString <> "" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 4)
    '                            End If
    '                        Case 19 'sup. commerciale
    '                            If riga.Item(IndiceColonne).ToString <> "" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 4)
    '                            End If
    '                        Case 20 'sup. esclusiva
    '                            If riga.Item(IndiceColonne).ToString <> "" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 4)
    '                            End If
    '                        Case 21 'cod. contratto
    '                            If riga.Item(27).ToString = "1" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            Else
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            End If
    '                        Case 22 'stato contratto
    '                            If riga.Item(27).ToString = "1" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            Else
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            End If
    '                        Case 23 'tipo contratto
    '                            If riga.Item(27).ToString = "1" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            Else
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            End If
    '                        Case 24 'nominativo
    '                            If riga.Item(27).ToString = "1" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            Else
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            End If
    '                        Case 25 'data stipula
    '                            If riga.Item(27).ToString = "1" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            Else
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            End If
    '                        Case 26 'data chiusura
    '                            If riga.Item(27).ToString = "1" Then
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            Else
    '                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, riga.Item(IndiceColonne), 0)
    '                            End If
    '                        Case Else

    '                    End Select
    '                    Cella = Cella + 1
    '                Next
    '            Next
    '            'CHIUSURA FILE
    '            .CloseFile()
    '        End With
    '        Dim FileNameXls As String = FileName & ".xls"
    '        Return FileNameXls
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function
End Class
