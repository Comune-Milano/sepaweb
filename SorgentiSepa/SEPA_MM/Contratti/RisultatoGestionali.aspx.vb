Imports System.IO

Partial Class Contratti_RisultatoGestionali
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim lstBolliParz As New System.Collections.Generic.List(Of String)
    Dim listaCheck As New System.Collections.Generic.List(Of String)
    Public percent_avanz As Long = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Str = "<div id=""divPre"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"

        Me.ImgProcedi.Attributes.Add("onclick", "javascript:document.getElementById('elabora').value='1';")

        If Not IsPostBack Then
            Response.Write(Str)

            Response.Flush()
            RicavaPercentSost()

            Dim dtAppoggio As New Data.DataTable
            dtAppoggio.Columns.Add("id_contratto")
            dtAppoggio.Columns.Add("id_bolletta")
            dtAppoggio.Columns.Add("importo")
            dtAppoggio.Columns.Add("fl_parziale")
            Session.Add("dtAppoggio", dtAppoggio)

            tipoDoc = Request.QueryString("TDOC")
            dataEmiss1 = Request.QueryString("EMISS1")
            dataEmiss2 = Request.QueryString("EMISS2")
            dataRifer1 = Request.QueryString("RIFER1")
            dataRifer2 = Request.QueryString("RIFER2")
            tipoUI = Request.QueryString("TIPOUI")
            tipoContr = Request.QueryString("TIPOCONTR")
            tipoSpec = Request.QueryString("TSPEC")
            elaborati = Request.QueryString("ELAB")
            CodContr = Request.QueryString("CODCONTR")
            noteDocGestion = Request.QueryString("NOTE")
            credDebito = Request.QueryString("CRED")
            orderBy = Request.QueryString("ORD")

            Cerca()

            lblInfo.Text = "In caso di DEBITO:<ul><li>applicazione TOTALE del debito come nuova emissione</li><li> applicazione PARZIALE (in rate) come voce nello schema bollette, sino ad esaurimento del debito stesso </li></ul>" & _
                "In caso di CREDITO:<ul><li>applicazione TOTALE del credito attraverso compensazioni o restituzione dello stesso</li><li>applicazione PARZIALE del credito nello schema bollette (a scalare in funzione delle future emissioni)</li>"
            Dim stringa As String = ""
            stringa = "<table style=" & Chr(34) & "font-family: Arial; font-size:7pt; color:#808080; font-weight:bold;" & Chr(34) & "><tr><td>In caso di DEBITO:<br />&nbsp&nbsp - applicazione TOTALE: emissione nuova bolletta con eliminazione del debito dalla partita gestionale<br />&nbsp&nbsp - applicazione PARZIALE: scrittura voci nello schema bollette, sino ad esaurimento del debito stesso</td></tr>" _
                & "<tr><td>In caso di CREDITO:<br />&nbsp&nbsp - applicazione TOTALE: ripartizione totale del credito ed eliminazione dello stesso dalla partita gestionale<br />&nbsp&nbsp - applicazione PARZIALE: scrittura voci a credito nello schema bollette (a scalare in funzione delle future emissioni)</td></tr></table>"
            lblInfo.Text = stringa
        Else
            Dim Str2 As String = ""
            Dim StrElab As String = ""

            'Str2 = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            'Str2 = Str2 & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
            'Str2 = Str2 & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

            StrElab = "<div id=""dvvvPre"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../NuoveImm/load.gif"" alt=""Elaborazione in corso"" /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Elaborazione in corso...</span>" _
            & "<br /><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>" _
            & "</div><br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>" _
            & "</td></tr></table></div></div>"

            If elabora.Value = "0" Then
                Response.Write(Str)
                Response.Flush()
            Else
                Response.Write(StrElab)
                Response.Flush()
            End If
        End If
    End Sub

    Public Property dtdati() As Data.DataTable
        Get
            If Not (ViewState("dtdati") Is Nothing) Then
                Return ViewState("dtdati")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtdati") = value
        End Set
    End Property

    Public Property tipoDoc() As String
        Get
            If Not (ViewState("par_tipoDoc") Is Nothing) Then
                Return CStr(ViewState("par_tipoDoc"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipoDoc") = value
        End Set

    End Property

    Public Property dataEmiss1() As String
        Get
            If Not (ViewState("par_dataEmiss1") Is Nothing) Then
                Return CStr(ViewState("par_dataEmiss1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataEmiss1") = value
        End Set

    End Property

    Public Property dataEmiss2() As String
        Get
            If Not (ViewState("par_dataEmiss2") Is Nothing) Then
                Return CStr(ViewState("par_dataEmiss2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataEmiss2") = value
        End Set

    End Property

    Public Property dataRifer1() As String
        Get
            If Not (ViewState("par_dataRifer1") Is Nothing) Then
                Return CStr(ViewState("par_dataRifer1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataRifer1") = value
        End Set

    End Property

    Public Property dataRifer2() As String
        Get
            If Not (ViewState("par_dataRifer2") Is Nothing) Then
                Return CStr(ViewState("par_dataRifer2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataRifer2") = value
        End Set

    End Property

    Public Property tipoUI() As String
        Get
            If Not (ViewState("par_tipoUI") Is Nothing) Then
                Return CStr(ViewState("par_tipoUI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipoUI") = value
        End Set

    End Property

    Public Property tipoContr() As String
        Get
            If Not (ViewState("par_tipoContr") Is Nothing) Then
                Return CStr(ViewState("par_tipoContr"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipoContr") = value
        End Set

    End Property

    Public Property tipoSpec() As String
        Get
            If Not (ViewState("par_tipoSpec") Is Nothing) Then
                Return CStr(ViewState("par_tipoSpec"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipoSpec") = value
        End Set

    End Property

    Public Property elaborati() As String
        Get
            If Not (ViewState("par_elaborati") Is Nothing) Then
                Return CStr(ViewState("par_elaborati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_elaborati") = value
        End Set

    End Property

    Public Property CodContr() As String
        Get
            If Not (ViewState("par_CodContr") Is Nothing) Then
                Return CStr(ViewState("par_CodContr"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CodContr") = value
        End Set

    End Property

    Public Property noteDocGestion() As String
        Get
            If Not (ViewState("par_noteDocGestion") Is Nothing) Then
                Return CStr(ViewState("par_noteDocGestion"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_noteDocGestion") = value
        End Set

    End Property

    Public Property credDebito() As String
        Get
            If Not (ViewState("par_credDebito") Is Nothing) Then
                Return CStr(ViewState("par_credDebito"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_credDebito") = value
        End Set

    End Property

    Public Property orderBy() As String
        Get
            If Not (ViewState("par_orderBy") Is Nothing) Then
                Return CStr(ViewState("par_orderBy"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_orderBy") = value
        End Set

    End Property

    Public Property impBollo() As Decimal
        Get
            If Not (ViewState("par_impBollo") Is Nothing) Then
                Return CLng(ViewState("par_impBollo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_impBollo") = value
        End Set

    End Property

    Public Property idEventoPrincipale() As Long
        Get
            If Not (ViewState("par_idEventoPrincipale") Is Nothing) Then
                Return CLng(ViewState("par_idEventoPrincipale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEventoPrincipale") = value
        End Set

    End Property

    Private Sub Cerca()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim bTrovato As Boolean = True
            Dim sValore As String = ""
            Dim sCompara As String = ""
            Dim sStringaSql As String = ""
            Dim elencoID As String = ""
            Dim strORDERBY As String = ""

            If par.IfEmpty(tipoDoc, "Null") <> "Null" And tipoDoc <> "-1" Then
                bTrovato = True
                sValore = tipoDoc

                sStringaSql = sStringaSql & " AND BOL_BOLLETTE_GEST.ID_TIPO=" & sValore
            End If

            If par.IfEmpty(dataEmiss1, "Null") <> "Null" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = par.AggiustaData(dataEmiss1)
                bTrovato = True
                sStringaSql = sStringaSql & " BOL_BOLLETTE_GEST.DATA_EMISSIONE>='" & sValore & "' "
            End If

            If par.IfEmpty(dataEmiss2, "Null") <> "Null" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = par.AggiustaData(dataEmiss2)
                bTrovato = True
                sStringaSql = sStringaSql & " BOL_BOLLETTE_GEST.DATA_EMISSIONE<='" & sValore & "' "
            End If

            If par.IfEmpty(dataRifer1, "Null") <> "Null" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = par.AggiustaData(dataRifer1)
                bTrovato = True
                sStringaSql = sStringaSql & " BOL_BOLLETTE_GEST.RIFERIMENTO_DA>='" & sValore & "' "
            End If

            If par.IfEmpty(dataRifer2, "Null") <> "Null" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = par.AggiustaData(dataRifer2)
                bTrovato = True
                sStringaSql = sStringaSql & " BOL_BOLLETTE_GEST.RIFERIMENTO_A<='" & sValore & "' "
            End If

            If par.IfEmpty(tipoUI, "Null") <> "Null" And tipoUI <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = tipoUI
                bTrovato = True

                sStringaSql = sStringaSql & " tipologia_unita_immobiliari.cod ='" & sValore & "' "
            End If

            If par.IfEmpty(tipoContr, "Null") <> "Null" And tipoContr <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = tipoContr
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC ='" & par.PulisciStrSql(sValore) & "' "
            End If

            If tipoContr = "ERP" Then
                If tipoSpec <> "0" And tipoSpec <> "2" Then 'Canone convenz., Art.22, Forze dell'ordine
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                    sValore = tipoSpec
                    bTrovato = True
                    sStringaSql = sStringaSql & " RAPPORTI_UTENZA.PROVENIENZA_ASS = " & tipoSpec & " "
                End If

                If tipoSpec = "2" Then 'Erp Moderato
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                    sValore = tipoSpec
                    bTrovato = True
                    sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = " & tipoSpec & " "
                End If
            End If

            If tipoContr = "L43198" Then
                If tipoSpec <> "-1" Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                    sValore = tipoSpec
                    bTrovato = True
                    sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DEST_USO = '" & tipoSpec & "' "
                End If
            End If

            If dtDati.Rows.Count = 0 Then
                If elaborati = "1" Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                    sValore = elaborati
                    bTrovato = True

                    sStringaSql = sStringaSql & " BOL_BOLLETTE_GEST.TIPO_APPLICAZIONE='N' "
                End If
            End If

            If dtdati.Rows.Count = 0 Then
                If credDebito = "1" Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                    sValore = credDebito
                    bTrovato = True

                    sStringaSql = sStringaSql & " IMPORTO_TOTALE<0 "
                End If
                If credDebito = "0" Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                    sValore = credDebito
                    bTrovato = True

                    sStringaSql = sStringaSql & " IMPORTO_TOTALE>0 "
                End If
            End If

            If CodContr <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = CodContr
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True

                'sValore = CodContr
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If noteDocGestion <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = noteDocGestion
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True

                'sValore = CodContr
                bTrovato = True
                sStringaSql = sStringaSql & " BOL_BOLLETTE_GEST.NOTE " & sCompara & " '" & par.PulisciStrSql(sValore).ToUpper & "' "
            End If


            If orderBy <> "" Then
                Select Case orderBy
                    Case "INTEST"
                        strORDERBY = "ORDER BY intestatario asc,data_emissione desc,riferimento_da asc "
                    Case "INDIR"
                        strORDERBY = "ORDER BY indirizzo asc,data_emissione desc,riferimento_da asc "
                    Case "CODUI"
                        strORDERBY = "ORDER BY cod_unita_immobiliare asc,data_emissione desc,riferimento_da asc "
                End Select
            Else
                strORDERBY = "ORDER BY intestatario asc,data_emissione desc,riferimento_da asc "
            End If

            If dtDati.Rows.Count > 0 Then
                For Each rowDati As Data.DataRow In dtDati.Rows
                    elencoID = elencoID & rowDati.Item("ID_BOLLETTA") & ","
                Next

                If elencoID <> "" Then
                    elencoID = "(" & Mid(elencoID, 1, Len(elencoID) - 1) & ")"
                End If
                sStringaSql = sStringaSql & " AND (BOL_BOLLETTE_GEST.TIPO_APPLICAZIONE='N' or BOL_BOLLETTE_GEST.id in " & elencoID & ")"

                dtDati.Rows.Clear()
            End If


            Dim sStringaSql2 As String = ""
            sStringaSql2 = "SELECT bol_bollette_GEST.id as id_boll,to_char(abs(BOL_BOLLETTE_GEST.IMPORTO_TOTALE),'9G999G990D99') as IMP_EMESSO,'' as DETTAGLI,BOL_BOLLETTE_GEST.IMPORTO_TOTALE as importoTOT,rapporti_utenza.ID as id,cod_contratto, tipologia_contratto_locazione.descrizione AS tipo_cont, " _
                        & "tipologia_unita_immobiliari.descrizione AS tipo_ui,data_emissione,riferimento_da, " _
                        & "TO_CHAR (TO_DATE (replace(data_emissione,'NULL',''), 'yyyymmdd'),'dd/mm/yyyy') AS data_emiss," _
                        & "TO_CHAR (TO_DATE (replace(riferimento_da,'NULL',''), 'yyyymmdd'),'dd/mm/yyyy') AS data_riferim1," _
                        & "TO_CHAR (TO_DATE (replace(riferimento_a,'NULL',''), 'yyyymmdd'),'dd/mm/yyyy') AS data_riferim2, " _
                        & "(CASE " _
                        & "WHEN rapporti_utenza.provenienza_ass = 1 " _
                        & "AND unita_immobiliari.id_destinazione_uso <> 2 " _
                        & "THEN 'ERP Sociale' " _
                        & "WHEN unita_immobiliari.id_destinazione_uso = 2 " _
                        & "THEN 'ERP Moderato' " _
                        & "WHEN rapporti_utenza.provenienza_ass = 12 " _
                        & "THEN 'CANONE CONVENZ.' " _
                        & "WHEN rapporti_utenza.provenienza_ass = 10 " _
                        & "THEN 'FORZE DELL''ORDINE' " _
                        & "WHEN rapporti_utenza.dest_uso = 'C' " _
                        & "THEN 'Cooperative' " _
                        & "WHEN rapporti_utenza.dest_uso = 'P' " _
                        & "THEN '431 P.O.R.' " _
                        & "WHEN rapporti_utenza.dest_uso = 'D' " _
                        & "THEN '431/98 ART.15 R.R.1/2004' " _
                        & "WHEN rapporti_utenza.dest_uso = 'S' " _
                        & "THEN '431/98 Speciali' " _
                        & "WHEN rapporti_utenza.dest_uso = '0' " _
                        & "THEN 'Standard' " _
                        & "END " _
                        & ") AS TIPO_spec,indirizzi.descrizione ||', '|| indirizzi.civico as indirizzo, bol_bollette_GEST.note," _
                        & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome) ) END) AS INTESTATARIO," _
                        & "tipo_bollette_gest.DESCRIZIONE AS TIPO_DOC,soggetti_contrattuali.id_anagrafica," _
                        & "(CASE WHEN BOL_BOLLETTE_GEST.IMPORTO_TOTALE <0 THEN '1' ELSE '0' END) AS CREDITO,BOL_BOLLETTE_GEST.TIPO_APPLICAZIONE,unita_immobiliari.cod_unita_immobiliare " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,siscom_mi.tipologia_contratto_locazione,siscom_mi.tipologia_unita_immobiliari,siscom_mi.tipo_bollette_gest,SISCOM_MI.unita_immobiliari,siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica,siscom_mi.indirizzi " _
                        & "WHERE BOL_BOLLETTE_GEST.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND FL_VISUALIZZABILE=1 AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO and UNITA_CONTRATTUALE.id_unita=UNITA_IMMOBILIARI.ID and UNITA_CONTRATTUALE.id_unita_principale is null " _
                        & "AND tipologia_contratto_locazione.cod = rapporti_utenza.cod_tipologia_contr_loc and bol_bollette_gest.id_tipo=tipo_bollette_gest.id and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.id_contratto=rapporti_utenza.id " _
                        & "AND unita_immobiliari.id_indirizzo = indirizzi.ID (+) and COD_TIPOLOGIA_OCCUPANTE='INTE' AND unita_immobiliari.cod_tipologia = tipologia_unita_immobiliari.cod " _
                        & sStringaSql & " " & strORDERBY

            '& "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN (CASE WHEN LENGTH (RAGIONE_SOCIALE)>25 THEN SUBSTR(RAGIONE_SOCIALE,1,25)||'...' ELSE RAGIONE_SOCIALE  END) ELSE (CASE WHEN LENGTH (RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome)))>25 THEN SUBSTR(RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome)),1,25)||'...' ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome) ) END) END) AS INTESTATARIO," _

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql2, par.OracleConn)
            Dim dt As New Data.DataTable()
            Dim dt2 As New Data.DataTable

            dt2.Columns.Add("id")
            dt2.Columns.Add("cod_contratto")
            dt2.Columns.Add("INTESTATARIO")
            dt2.Columns.Add("TIPO_CONT")
            dt2.Columns.Add("TIPO_SPEC")
            dt2.Columns.Add("TIPO_UI")
            dt2.Columns.Add("INDIRIZZO")
            dt2.Columns.Add("TIPO_DOC")
            dt2.Columns.Add("IMP_EMESSO")
            dt2.Columns.Add("DATA_EMISS")
            dt2.Columns.Add("DATA_RIFERIM1")
            dt2.Columns.Add("DATA_RIFERIM2")
            dt2.Columns.Add("DETTAGLI")
            dt2.Columns.Add("CREDITO")
            dt2.Columns.Add("RADIO")
            dt2.Columns.Add("ID_BOLL")
            dt2.Columns.Add("importoTOT")
            dt2.Columns.Add("TIPO_APPL")
            dt2.Columns.Add("NOTE")

            da.Fill(dt)
            da.Dispose()

            'DataGrid1.DataSource = dt
            'DataGrid1.DataBind()
            Dim k As Integer = 0
            Dim k2 As Integer = 0
            Dim miocolore As String = "#DCE3F5"
            Dim valoreUguale As Boolean = False


            Dim rowDT As System.Data.DataRow
            Dim idAngrafica As Long = 0

            Dim numradio As Integer = 1
            Dim tipoDocumento As String = ""
            Dim numRighe As Long = dt.Rows.Count
            Dim contatore As Long = 0
            If dt.Rows.Count > 0 Then

                For Each row As Data.DataRow In dt.Rows

                    rowDT = dt2.NewRow()

                    idAngrafica = par.IfNull(row.Item("ID_ANAGRAFICA"), 0)

                    rowDT.Item("id") = par.IfNull(row.Item("id"), 0)
                    'rowDT.Item("cod_contratto") = par.IfNull(row.Item("cod_contratto"), "")
                    rowDT.Item("cod_contratto") = "<a href=""javascript:window.open('Contratto.aspx?LT=1&ID=" & par.IfNull(row.Item("id"), 0) & "&COD=" & par.IfNull(row.Item("cod_contratto"), "") & "','CONTRATTO','height=780,top=0,left=0,width=1160');void(0);"">" & par.IfNull(row.Item("cod_contratto"), "") & "</a>"
                    rowDT.Item("INTESTATARIO") = par.IfNull(row.Item("intestatario"), "")
                    rowDT.Item("TIPO_CONT") = par.IfNull(row.Item("TIPO_CONT"), "")
                    rowDT.Item("TIPO_SPEC") = par.IfNull(row.Item("TIPO_SPEC"), "")
                    rowDT.Item("TIPO_UI") = par.IfNull(row.Item("TIPO_UI"), "")
                    rowDT.Item("INDIRIZZO") = par.IfNull(row.Item("INDIRIZZO"), "")
                    rowDT.Item("TIPO_DOC") = par.IfNull(row.Item("TIPO_DOC"), "")
                    tipoDocGest.Value = par.IfNull(row.Item("TIPO_DOC"), "")
                    rowDT.Item("IMP_EMESSO") = par.IfNull(row.Item("IMP_EMESSO"), 0)
                    rowDT.Item("DATA_EMISS") = par.IfNull(row.Item("DATA_EMISS"), "")
                    rowDT.Item("DATA_RIFERIM1") = par.IfNull(row.Item("DATA_RIFERIM1"), "")
                    rowDT.Item("DATA_RIFERIM2") = par.IfNull(row.Item("DATA_RIFERIM2"), "")
                    'rowDT.Item("DETTAGLI") = par.IfNull(row.Item("DETTAGLI"), "")
                    rowDT.Item("CREDITO") = par.IfNull(row.Item("CREDITO"), "")
                    rowDT.Item("importoTOT") = par.IfNull(row.Item("importoTOT"), 0)
                    rowDT.Item("ID_BOLL") = par.IfNull(row.Item("ID_BOLL"), 0)

                    rowDT.Item("TIPO_APPL") = par.IfNull(row.Item("TIPO_APPLICAZIONE"), "N")
                    rowDT.Item("NOTE") = par.IfNull(row.Item("NOTE"), "")

                    Select Case rowDT.Item("TIPO_APPL")
                        Case "P"
                            'rowDT.Item("cod_contratto") = par.IfNull(row.Item("cod_contratto"), "")
                            If par.IfNull(row.Item("CREDITO"), "") = "1" Then
                                rowDT.Item("dettagli") = "<a href=""javascript:window.open('DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("ID_BOLL"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                            Else
                                rowDT.Item("dettagli") = "<a href=""javascript:window.open('DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("ID_BOLL"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                            End If
                        Case "T"
                            'rowDT.Item("cod_contratto") = par.IfNull(row.Item("cod_contratto"), "")
                            If par.IfNull(row.Item("CREDITO"), "") = "1" Then
                                rowDT.Item("dettagli") = "<a href=""javascript:window.open('DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("ID_BOLL"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                            Else
                                rowDT.Item("dettagli") = "<a href=""javascript:window.open('DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("ID_BOLL"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                            End If
                        Case "N"
                            rowDT.Item("dettagli") = ""
                    End Select


                    'If rowDT.Item("CREDITO") = "1" Then
                    '    If numradio = 1 Then
                    '        tipoDocumento = "C"
                    '        rowDT.Item("RADIO") = "<input id='Radio" & numradio & "' name='rdb" & numradio & "' type='radio' value='T' onclick='ValorizzaACascataRB(this.checked,this.value);'/>TOTALE<input id='Radio" & numradio + 1 & "' name='rdb" & numradio & "' type='radio' value='P' onclick='ValorizzaACascataRB(this.checked,this.value);'/>PARZIALE"
                    '    Else
                    '        tipoDocumento = "C"
                    '        rowDT.Item("RADIO") = "<input id='Radio" & numradio & "' name='rdb" & numradio & "' type='radio' value='T'/>TOTALE<input id='Radio" & numradio + 1 & "' name='rdb" & numradio & "' type='radio' value='P'/>PARZIALE"
                    '    End If
                    'Else
                    '    If numradio = 1 Then
                    '        tipoDocumento = "D"
                    '        rowDT.Item("RADIO") = "<input id='Radio" & numradio & "' name='rdb" & numradio & "' type='radio' value='T' onclick='ValorizzaACascataRB(this.checked,this.value);'/>TOTALE<input id='Radio" & numradio + 1 & "' name='rdb" & numradio & "' type='radio' value='P' onclick='ValorizzaACascataRB(this.checked,this.value);ScegliPercentuale();'/>PARZIALE"
                    '    Else
                    '        If tipoDocumento = "C" Then
                    '            rowDT.Item("RADIO") = "<input id='Radio" & numradio & "' name='rdb" & numradio & "' type='radio' value='T' />TOTALE<input id='Radio" & numradio + 1 & "' name='rdb" & numradio & "' type='radio' value='P' onclick='ScegliPercentuale();'/>PARZIALE"
                    '        Else
                    '            tipoDocumento = "D"
                    '            rowDT.Item("RADIO") = "<input id='Radio" & numradio & "' name='rdb" & numradio & "' type='radio' value='T'/>TOTALE<input id='Radio" & numradio + 1 & "' name='rdb" & numradio & "' type='radio' value='P'/>PARZIALE"
                    '        End If
                    '    End If

                    'End If

                    dt2.Rows.Add(rowDT)
                    numradio = numradio + 2

                Next
                DataGrid1.DataSource = dt2
                DataGrid1.DataBind()

                numRigheDT.Value = Format(dt2.Rows.Count, "##,##")


                Do While k2 < dt2.Rows.Count - 1
                    If par.IfNull(dt2.Rows(k2).Item("CREDITO"), 0) = "1" Then
                        For Each di As DataGridItem In DataGrid1.Items
                            If par.IfNull(di.Cells(13).Text, 0) = par.IfNull(dt2.Rows(k2).Item("CREDITO"), 0) Then
                                di.Cells(9).ForeColor = Drawing.ColorTranslator.FromHtml("#008400")
                            End If
                        Next
                    Else
                        For Each di As DataGridItem In DataGrid1.Items
                            If par.IfNull(di.Cells(13).Text, 0) = par.IfNull(dt2.Rows(k2).Item("CREDITO"), 0) Then
                                di.Cells(9).ForeColor = Drawing.ColorTranslator.FromHtml("red")
                            End If
                        Next
                    End If
                    k2 = k2 + 1
                Loop

                Do While k < dt2.Rows.Count - 1
                    If par.IfNull(dt2.Rows(k).Item("id"), 0) = par.IfNull(dt2.Rows(k + 1).Item("id"), 0) Then
                        valoreUguale = True
                        If k > 0 Then
                            If miocolore = "#DCE3F5" Then
                                miocolore = "white"
                            Else
                                miocolore = "#DCE3F5"
                            End If
                        End If
                        For Each di As DataGridItem In DataGrid1.Items
                            If di.Cells(0).Text = par.IfNull(dt2.Rows(k).Item("id"), 0) Then
                                di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)

                                If k < dt.Rows.Count - 1 Then
                                    If di.Cells(0).Text = par.IfNull(dt2.Rows(k + 1).Item("id"), 0) Then
                                        di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
                                        k = k + 1
                                    End If
                                End If
                            End If
                        Next
                    Else
                        If k > 0 Then
                            If miocolore = "#DCE3F5" Then
                                miocolore = "white"
                            Else
                                miocolore = "#DCE3F5"
                            End If
                            For Each di As DataGridItem In DataGrid1.Items
                                If di.Cells(0).Text = par.IfNull(dt2.Rows(k).Item("id"), 0) Then
                                    di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
                                End If
                            Next
                        End If
                    End If
                    k = k + 1
                Loop

                If valoreUguale = False And dt.Rows.Count <> 1 Then
                    DataGrid1.AlternatingItemStyle.BackColor = Drawing.ColorTranslator.FromHtml("White")
                End If

                lblNumRisult.Text = " - Trovati " & Format(dt2.Rows.Count, "##,##") & " documenti gestionali da elaborare"
            Else
                lblNumRisult.Text = " - Trovati 0 documenti gestionali da elaborare"
            End If

            If k = dt2.Rows.Count - 1 And dt2.Rows.Count <> 1 Then
                If miocolore = "#DCE3F5" Then
                    miocolore = "white"
                Else
                    miocolore = "#DCE3F5"
                End If
                For Each di As DataGridItem In DataGrid1.Items
                    If di.Cells(0).Text = par.IfNull(dt2.Rows(k).Item("id"), 0) Then
                        di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
                    End If
                Next
            End If

            For Each di In Me.DataGrid1.Items 'INIZIO MODIFICA INDICI CELLA
                If di.Cells(19).Text.Contains("P") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        di.Cells(0).FindControl("ChSelezionato").Enabled = False
                        di.Cells(15).FindControl("ChParziale").Enabled = False
                        'di.Cells(j).Font.Strikeout = True
                        di.Cells(j).ToolTip = "Documento già elaborato con scrittura delle voci in schema bollette! L'importo scalerà in base alle future emissioni."
                    Next
                End If
                If di.Cells(19).Text.Contains("T") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        di.Cells(0).FindControl("ChSelezionato").Enabled = False
                        di.Cells(15).FindControl("ChParziale").Enabled = False
                        di.Cells(j).Font.Strikeout = True
                    Next
                End If
            Next

            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CreaDTelaborazioni()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim dtElaboraz As New Data.DataTable

            dtElaboraz.Columns.Add("id_bolletta")
            dtElaboraz.Columns.Add("id_contratto")
            dtElaboraz.Columns.Add("intestatario")
            dtElaboraz.Columns.Add("cod_contratto")
            dtElaboraz.Columns.Add("tipo_bolletta")
            dtElaboraz.Columns.Add("importo")
            dtElaboraz.Columns.Add("fl_parziale")
            dtElaboraz.Columns.Add("data_elaborazione")
            dtElaboraz.Columns.Add("operatore")

            Dim RIGA As Data.DataRow
            For Each rowDati As Data.DataRow In dtdati.Rows
                RIGA = dtElaboraz.NewRow()
                RIGA.Item("id_bolletta") = rowDati.Item("ID_BOLLETTA")
                RIGA.Item("id_contratto") = rowDati.Item("ID_CONTRATTO")
                RIGA.Item("importo") = rowDati.Item("IMPORTO")
                RIGA.Item("fl_parziale") = rowDati.Item("FL_PARZIALE")

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & rowDati.Item("ID_CONTRATTO")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    RIGA.Item("cod_contratto") = par.IfNull(myReader("COD_CONTRATTO"), "")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.id AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & rowDati.Item("ID_CONTRATTO") & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    RIGA.Item("INTESTATARIO") = par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT TIPO_BOLLETTE_GEST.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & rowDati.Item("ID_BOLLETTA") & " AND BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    RIGA.Item("TIPO_BOLLETTA") = par.IfNull(myReader("DESCRIZIONE"), "")
                End If
                myReader.Close()

                RIGA.Item("DATA_ELABORAZIONE") = Format(Now, "dd/MM/yyyy")

                RIGA.Item("OPERATORE") = Session.Item("OPERATORE")

                dtElaboraz.Rows.Add(RIGA)
            Next

            DataGrid1FINE.DataSource = dtElaboraz

            DataGrid1FINE.DataBind()

            Dim nomeFile As String = par.EsportaExcelDaDTWithDatagrid(dtElaboraz, DataGrid1FINE, "Elaboraz_Massiva_Gestionali_", , , , False)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                'Response.Redirect("../FileTemp/" & nomeFile, False)
                File.Move(Server.MapPath("..\FileTemp\") & nomeFile, Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\") & nomeFile)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
            End If

            'par.OracleConn.Close()
            'par.OracleConn.Dispose()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub RicavaPercentSost()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_GEST_CREDITO WHERE ID=1"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                percenSost = par.IfNull(myReader("PERC_SOSTEN"), 1)
            End If
            myReader.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Public Property percenSost() As Integer
        Get
            If Not (ViewState("par_percenSost") Is Nothing) Then
                Return CInt(ViewState("par_percenSost"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_percenSost") = value
        End Set

    End Property

    Private Function ControllaCheckRadio() As Boolean
        ControllaCheckRadio = True
        Try
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            Dim chkParz As System.Web.UI.WebControls.CheckBox
            Dim h As Integer = 1
            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                chkParz = oDataGridItem.FindControl("ChParziale")
                If chkParz.Checked Then
                    If Not chkExport.Checked Then
                        ControllaCheckRadio = False
                    End If
                End If
                h = h + 2
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function

    Private Function ControllaCheckbox() As Boolean
        ControllaCheckbox = False
        Try
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then
                    ControllaCheckbox = True
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub ImgProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            If errore.Value = 0 And conferma.Value = "1" Then
                Dim Elenco As String = ""

                Dim radioCheckata As String = ""

                Dim msgerrore As String = ""

                Dim h As Integer = 1
                Dim numElaboraz As Integer = 0

                If ControllaCheckbox() = False Then
                    Response.Write("<script>alert('Selezionare il contratto!')</script>")
                    Exit Try
                End If
                If ControllaCheckRadio() = False Then
                    Response.Write("<script>alert('Selezionare il contratto!')</script>")
                    Exit Try
                End If


                Dim RIGA As Data.DataRow
                dtDati = Session.Item("dtAppoggio")

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans

                Dim chkSelezione As System.Web.UI.WebControls.CheckBox
                Dim chkParziale As System.Web.UI.WebControls.CheckBox

                Dim esistente As Boolean = False
                Dim numRigheElab As Long = 0
                Dim contatore As Long = 0
                If dtDati.Rows.Count > 0 Then
                    For Each oDataGridItem In Me.DataGrid1.Items
                        chkSelezione = oDataGridItem.FindControl("ChSelezionato")
                        chkParziale = oDataGridItem.FindControl("ChParziale")

                        If chkSelezione.Checked Then
                            For Each rowDati As Data.DataRow In dtDati.Rows
                                If oDataGridItem.Cells(16).Text = rowDati.Item("ID_BOLLETTA") Then
                                    esistente = True
                                    Exit For
                                End If
                            Next
                            If esistente = False Then
                                RIGA = dtDati.NewRow()
                                RIGA.Item("id_bolletta") = oDataGridItem.Cells(16).Text
                                RIGA.Item("id_contratto") = oDataGridItem.Cells(0).Text
                                RIGA.Item("importo") = oDataGridItem.Cells(17).Text
                                If oDataGridItem.Cells(15).FindControl("ChParziale").Checked Then
                                    RIGA.Item("fl_parziale") = "SI"
                                Else
                                    RIGA.Item("fl_parziale") = "NO"
                                End If
                                dtDati.Rows.Add(RIGA)
                            End If
                        End If
                    Next


                    For Each rowDati As Data.DataRow In dtDati.Rows
                        numRigheElab = dtDati.Rows.Count

                        idContratto.Value = rowDati.Item("ID_CONTRATTO")
                        importoBolletta.Value = rowDati.Item("IMPORTO")
                        idBolletta.Value = rowDati.Item("ID_BOLLETTA")
                        If rowDati.Item("FL_PARZIALE") = "SI" Then
                            If importoBolletta.Value < 0 Then
                                ElaborazioneCredito(idContratto.Value, idBolletta.Value, "P")
                            Else
                                ElaborazioneParzDebito(idContratto.Value, idBolletta.Value, percentuale.Value)
                            End If
                        Else
                            If importoBolletta.Value < 0 Then
                                ElaborazioneCredito(idContratto.Value, idBolletta.Value, "T")
                            Else
                                ElaborazioneTotDebito(idContratto.Value, idBolletta.Value)
                            End If
                        End If
                        contatore = contatore + 1
                        percent_avanz = (contatore * 100) / numRigheElab
                        Response.Write("<script>tempo=" & Format(percent_avanz, "0") & ";</script>")
                        Response.Flush()
                    Next
                Else
                    For Each oDataGridItem In Me.DataGrid1.Items
                        chkSelezione = oDataGridItem.FindControl("ChSelezionato")
                        chkParziale = oDataGridItem.FindControl("ChParziale")

                        If chkSelezione.Checked Then
                            RIGA = dtDati.NewRow()
                            RIGA.Item("id_bolletta") = oDataGridItem.Cells(16).Text
                            RIGA.Item("id_contratto") = oDataGridItem.Cells(0).Text
                            RIGA.Item("importo") = oDataGridItem.Cells(17).Text
                            If oDataGridItem.Cells(15).FindControl("ChParziale").Checked Then
                                RIGA.Item("fl_parziale") = "SI"
                            Else
                                RIGA.Item("fl_parziale") = "NO"
                            End If
                            dtDati.Rows.Add(RIGA)
                        End If
                    Next
                    If dtDati.Rows.Count > 0 Then
                        For Each rowDati As Data.DataRow In dtDati.Rows
                            numRigheElab = dtDati.Rows.Count

                            idContratto.Value = rowDati.Item("ID_CONTRATTO")
                            importoBolletta.Value = rowDati.Item("IMPORTO")
                            idBolletta.Value = rowDati.Item("ID_BOLLETTA")
                            If rowDati.Item("FL_PARZIALE") = "SI" Then
                                If importoBolletta.Value < 0 Then
                                    ElaborazioneCredito(idContratto.Value, idBolletta.Value, "P")
                                Else
                                    ElaborazioneParzDebito(idContratto.Value, idBolletta.Value, percentuale.Value)
                                End If
                            Else
                                If importoBolletta.Value < 0 Then
                                    ElaborazioneCredito(idContratto.Value, idBolletta.Value, "T")
                                Else
                                    ElaborazioneTotDebito(idContratto.Value, idBolletta.Value)
                                End If
                            End If
                            contatore = contatore + 1
                            percent_avanz = (contatore * 100) / numRigheElab
                            Response.Write("<script>tempo=" & Format(percent_avanz, "0") & ";</script>")
                            Response.Flush()
                        Next
                    Else
                        Response.Write("<script>alert('Errore nella memorizzazione dei dati!')</script>")
                    End If
                End If

                'For Each oDataGridItem In Me.DataGrid1.Items
                '    chkExport = oDataGridItem.FindControl("ChSelezionato")

                '    If chkExport.Checked Then
                '        Elenco = Elenco & oDataGridItem.Cells(0).Text & ","
                '        idContratto.Value = oDataGridItem.Cells(0).Text
                '        importoBolletta.Value = oDataGridItem.Cells(16).Text
                '        idBolletta.Value = oDataGridItem.Cells(15).Text
                '        If Request.Item("rdb" & h) <> "" Then
                '            If Request.Item("rdb" & h) = "T" Then
                '                If importoBolletta.Value < 0 Then
                '                    'Response.Redirect("SpostamGestionaleTot.aspx?IDC=" & idContratto & "&IDB=" & idBolletta & "&TIPO=CRED", False)

                '                    ElaborazioneCredito(idContratto.Value, idBolletta.Value, "T")
                '                Else
                '                    'Response.Redirect("SpostamGestionaleTot.aspx?IDC=" & idContratto & "&IDB=" & idBolletta & "&TIPO=DEB", False)

                '                    ElaborazioneTotDebito(idContratto.Value, idBolletta.Value)
                '                End If
                '                numElaboraz = numElaboraz + 1
                '            ElseIf Request.Item("rdb" & h) = "P" Then
                '                If importoBolletta.Value < 0 Then
                '                    'Response.Redirect("SpostamGestionaleParziale.aspx?IDC=" & idContratto & "&IDB=" & idBolletta & "&TIPO=CRED", False)

                '                    ElaborazioneCredito(idContratto.Value, idBolletta.Value, "P")
                '                Else
                '                    'Response.Redirect("SpostamGestionaleParziale.aspx?IDC=" & idContratto & "&IDB=" & idBolletta & "&TIPO=DEB", False)

                '                    ElaborazioneParzDebito(idContratto.Value, idBolletta.Value, percentuale.Value)
                '                End If
                '                numElaboraz = numElaboraz + 1
                '            End If
                '        Else
                '            Response.Write("<script>alert('" & numElaboraz & "')</script>")
                '        End If
                '    End If
                '    h = h + 2
                'Next

                CreaDTelaborazioni()

                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione effettuata con successo!')</script>")

                Cerca()
                elaborati = "0"
                elabora.Value = "0"

                If Elenco <> "" Then
                    Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
                End If
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Sub CreaListaCheckPrec(ByRef listaCheck As System.Collections.Generic.List(Of String))
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            If Not IsNothing(Session.Item("listaCheck")) Then
                If Session.Item("listaCheck").Contains(oDataGridItem.Cells(16).Text) Then
                    chkExport.Checked = True
                Else
                    chkExport.Checked = False
                End If
            Else
                CreaListaCheck(listaCheck)
            End If
        Next
    End Sub

    Private Sub CreaListaCheck(ByRef listaCheck As System.Collections.Generic.List(Of String))
        Try
            Dim chkExport As System.Web.UI.WebControls.CheckBox

            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then
                    listaCheck.Add(oDataGridItem.Cells(16).Text)
                End If
            Next

            If listaCheck.Count > 0 Then
                Session.Add("listaCheck", listaCheck)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ElaborazioneTotDebito(ByVal idContr As Long, ByVal idBollGest As Long)
        Try
            '***** APPLICA INTERAMENTE COME NUOVA EMISSIONE *****
            Dim IDUNITA As Long = 0
            Dim anno As Integer = 0
            Dim INIZIO As String = ""
            Dim FINE As String = ""
            Dim idTIPO As Integer = 0
            Dim noteGEST As String = ""
            Dim importoGest As Integer = 0
            Dim idAnagrafica As Long = 0

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                IDUNITA = par.IfNull(myReader0("ID_UNITA"), 0)
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & idContr
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), 0)
            End If
            myReader0.Close()

            'NUOVA TABELLA 1
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & idBollGest
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                INIZIO = par.IfNull(myReader0("RIFERIMENTO_DA"), "")
                FINE = par.IfNull(myReader0("RIFERIMENTO_A"), "")
                idTIPO = par.IfNull(myReader0("ID_TIPO"), 0)
                noteGEST = par.IfNull(myReader0("NOTE"), "")
                importoGest = par.IfNull(myReader0("IMPORTO_TOTALE"), 0)
            End If
            myReader0.Close()

            par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & idContr & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderS.Read Then
                par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                                        & "(ID, N_RATA,DATA_EMISSIONE, DATA_SCADENZA," _
                                        & "NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                        & "ID_UNITA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                        & "FL_STAMPATO, ID_COMPLESSO," _
                                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, RIF_FILE,ID_TIPO) " _
                                        & "Values " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL,99,'" & Format(Now, "yyyyMMdd") _
                                        & "', '29991231','" & par.PulisciStrSql(noteGEST) & "'," _
                                        & "" & idContr _
                                        & " ," & par.RicavaEsercizioCorrente & ", " _
                                        & IDUNITA _
                                        & ",'', " & "NULL" _
                                        & ", '" & par.PulisciStrSql(par.IfNull(myReaderS("PRESSO_COR"), "")) & "', " _
                                        & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                                        & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                                        & "', '', '" & INIZIO _
                                        & "', '" & FINE & "', " _
                                        & "'0', " & myReaderS("ID_COMPLESSO") & ", " _
                                        & Year(Now) & ", '', " & myReaderS("ID_EDIFICIO") & ",'MOD',1)"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReaderA.Read Then
                    Dim ID_BOLLETTA As Long = myReaderA(0)

                    'NUOVA TABELLA 2
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & idBollGest
                    Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dtVoci As New Data.DataTable
                    daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    daVoci.Fill(dtVoci)
                    daVoci.Dispose()
                    For Each row As Data.DataRow In dtVoci.Rows
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                            & "," & row.Item("ID_VOCE") & "" _
                                            & "," & par.VirgoleInPunti(row.Item("IMPORTO")) & ",'')"
                        par.cmd.ExecuteNonQuery()
                    Next
                End If
                myReaderA.Close()
            End If
            myReaderS.Close()

            'AGGIORNO CON TIPO APPLICAZIONE = T (: spostamento totale)
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T',DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()


            'SCRIVO EVENTO ELABORAZIONE MASSIVA DOCUMENTI
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F208','IMPORTO ELABORATO: EURO " & par.VirgoleInPunti(importoGest) & "')"
            par.cmd.ExecuteNonQuery()


        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ElaborazioneCredito(ByVal idContr As Long, ByVal idBollGest As Long, ByVal TipoElabor As String)
        Try
            'BISOGNO PRIMA PROCEDERE AL CONTROLLO DELLE MOROSITA

            Dim importoCreditoIniziale As Decimal = 0
            Dim importoCredito As Decimal = 0
            Dim importoCreditoRateizz As Decimal = 0
            Dim importoMorosita As Decimal = 0
            Dim diffCreditoMoros As Decimal = 0

            'Dim idIncasso As Long = 0
            Dim impNuovoPAGATO As Decimal = 0
            Dim idTipoBoll As Long = 0
            Dim tipoPagParz As Long = 0
            Dim statoContratto As String = ""

            Dim TotBollettePagabile As Decimal = 0
            Dim ripartizione As Boolean = False
            Dim nMesi As Integer = 0
            'par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO,0)-NVL(IMP_PAGATO,0)) AS TOT " _
            '                    & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
            '                    & "WHERE FL_ANNULLATA = '0' " _
            '                    & "AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND IMPORTO_TOTALE > 0 AND " _
            '                    & "ID_BOLLETTA = BOL_BOLLETTE.ID and data_scadenza<='" & Format(Now, "yyyyMMdd") & "' " _
            '                    & "AND ID_VOCE = T_VOCI_BOLLETTA.ID AND ID_RATEIZZAZIONE IS NULL " _
            '                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & idContr & " " _
            '                    & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL "
            'Dim lettoreT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreT.Read Then
            '    TotBollettePagabile = Format(par.IfNull(lettoreT("TOT"), 0), "##,##0.00")
            'End If
            'lettoreT.Close()
            TotBollettePagabile = par.CalcolaSaldoAttuale(idContr)

            par.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(ID) as statoContr FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                statoContratto = par.IfNull(myReader0("statoContr"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=43"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                nMesi = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE BOL_BOLLETTE_GEST.ID=BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND BOL_BOLLETTE_GEST.ID=" & idBollGest
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & idBollGest
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()
            If dtVoci.Rows.Count > 0 Then
                For Each row0 As Data.DataRow In dtVoci.Rows
                    importoCreditoIniziale = Math.Abs(par.IfNull(row0.Item("IMPORTO_TOTALE"), 0))
                    importoCredito = Math.Abs(par.IfNull(row0.Item("IMPORTO_TOTALE"), 0))
                    importoCreditoRateizz = importoCreditoIniziale
                    idTipoBoll = par.IfNull(row0.Item("ID_TIPO"), 0)

                    If TotBollettePagabile <> 0 Then

                        If TotBollettePagabile < importoCreditoIniziale Then
                            'importoCreditoIniziale = TotBollettePagabile
                            'importoCredito = TotBollettePagabile
                        End If
                    End If

                    If idTipoBoll = 4 Then
                        tipoPagParz = 12
                    Else
                        tipoPagParz = 11
                    End If

                    Dim noteEvento As String = ""

                    Select Case idTipoBoll
                        Case 50, 51, 52
                            idTipoPagamento = 5
                            noteEvento = "C/C 59"
                        Case 53
                            idTipoPagamento = 6
                            noteEvento = "C/C 60"
                        Case Else
                            idTipoPagamento = 4
                    End Select
                    Dim condizioneFacility As String = ""

                    condizioneFacility = " AND BOL_BOLLETTE.ID_TIPO NOT IN (26)"


                    '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
                    par.cmd.CommandText = "select bol_bollette.id,bol_bollette.*,bol_bollette_voci.*, (case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) as ordine,BOL_BOLLETTE_VOCI.ID AS ID_VOCE1,priorita,(CASE WHEN NVL(ID_TIPO,0)=4 OR NVL(ID_TIPO,0)=5 THEN '1' ELSE '0' END) AS RICLASS from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci,siscom_mi.T_VOCI_BOLLETTA where bol_bollette.id=bol_bollette_voci.id_bolletta and id_contratto=" & idContr & " and ADD_MONTHS(TO_DATE (bol_bollette.data_scadenza, 'yyyyMMdd'), " & nMesi & ") <= TO_DATE ('" & Format(Now, "yyyyMMdd") & "', 'yyyyMMdd') and abs(importo)>abs(nvl(imp_pagato,0)) and abs(importo)>0 and fl_annullata=0 and id_tipo<> 22 and " _
                        & "NVL (id_rateizzazione, 0) = 0 AND NVL (id_bolletta_ric, 0) = 0  AND BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID(+) AND nvl(IMPORTO_RUOLO,0) = 0 " _
                        & condizioneFacility _
                        & " order by " _
                        & "riclass ASC,data_scadenza asc,data_emissione asc " _
                        & " ,BOL_BOLLETTE_VOCI.id_bolletta ASC " _
                        & " ,ordine ASC"
                    Dim daMoros1 As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dtMoros1 As New Data.DataTable
                    daMoros1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    daMoros1.Fill(dtMoros1)
                    daMoros1.Dispose()
                    If dtMoros1.Rows.Count > 0 Then
                        ripartizione = True
                        '(0)*** AGGIORNO LA VOCE degli INCASSI EXTRAMAV ***
                        par.cmd.CommandText = "INSERT INTO siscom_mi.incassi_extramav (ID, id_tipo_pag, motivo_pagamento, id_contratto,data_pagamento, riferimento_da, riferimento_a, fl_annullata,importo, id_operatore,id_bolletta_gest,data_ora)" _
                            & "VALUES (siscom_mi.seq_incassi_extramav.NEXTVAL," & tipoPagParz & ",'RIPARTIZ. CREDITO GESTIONALE " & noteEvento & "'," & idContr & ",'" & Format(Now, "yyyyMMdd") & "','','',0," & par.VirgoleInPunti(importoCredito) & "," & Session.Item("ID_OPERATORE") & "," & idBollGest & ",'" & Format(Now, "yyyyMMddHHmmss") & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.currval from dual"
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            idIncasso = par.IfNull(lettore(0), "")
                        End If
                        lettore.Close()

                        'idEventoPrincipale = WriteEvent(True, "null", "F205", importoCredito, Format(Now, "dd/MM/yyyy"), "null", idIncasso, idContr, "COPERTURA VOCI BOLLETTA DA CREDITO GESTIONALE " & noteEvento & "")

                        For Each row1 As Data.DataRow In dtMoros1.Rows

                            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "' AND ID=" & row1.Item("ID_BOLLETTA")
                            'lettore = par.cmd.ExecuteReader
                            'If Not lettore.Read Then
                            If importoCredito <> 0 Then
                                importoCreditoRateizz = importoCredito
                                'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                '            & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                '            & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                                'par.cmd.ExecuteNonQuery()


                                'RIF mail amisano 25/08/2017 a cosa serve questo blocco di codice??? non sono ancora state pagate le rate...
                                If row1.Item("ID_TIPO") = "5" Then
                                    If importoCreditoRateizz <> 0 Then
                                        'RIF mail amisano 25/08/2017 la funzione successiva, quando si paga una rata, non ripartisce nulla sulle riclassificate.
                                        ' Se in alcuni casi ha ripartito è perchè una rata era parzialmente pagata quindi ha pagato nuovamente delle riclassificate
                                        ' senza verificare l'importo per cui la rata è stata pagata dall'incasso in corso!

                                        'RIF mail amisano 25/08/2017 La chiamata alla funzione PagaBolRateizzazione deve avvenire solo per le quote capitali o gli anticipi dei piani di rientro!
                                        'PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCreditoIniziale)
                                        If row1.Item("ID_VOCE") = "676" Or row1.Item("ID_VOCE") = "677" Then
                                            PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCreditoRateizz)
                                        End If

                                    End If
                                End If
                            End If
                            '        End If
                            'lettore.Close()

                            If row1.Item("ID_TIPO") <> "4" Then
                                importoMorosita = par.IfNull(row1.Item("IMPORTO"), 0) - par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                If importoCredito <> 0 Then

                                    diffCreditoMoros = importoMorosita - Math.Abs(importoCredito)

                                    If diffCreditoMoros >= 0 Then
                                        If isPagabileBollo(row1.Item("ID_VOCE1").ToString, importoCredito, impBollo) = False Then
                                            'salta il pagamento e va alla voce successiva
                                            Continue For
                                        End If

                                        '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO in BOL_BOLLETTE_VOCI_PAGAM ***
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                        & "(" & row1.Item("ID_VOCE1") & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(Math.Round(importoCredito, 2)) & "," & idTipoPagamento & "," & idIncasso & ")"
                                        par.cmd.ExecuteNonQuery()

                                        'WriteEvent(False, row1.Item("ID_VOCE1"), "F205", importoCredito, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContr, "")

                                        importoCredito = importoCredito + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                        'ATTRAVERSO IL CREDITO, INIZIO AD INCASSARE LE BOLLETTE (scaduta e sollecitata) PARTENDO DALLE PIù VECCHIE

                                        '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(importoCredito) & " where id=" & row1.Item("ID_VOCE1")
                                        par.cmd.ExecuteNonQuery()

                                        importoCredito = 0
                                    Else
                                        impNuovoPAGATO = importoMorosita + par.IfNull(row1.Item("IMP_PAGATO"), 0)

                                        If isPagabileBollo(row1.Item("ID_VOCE1").ToString, impNuovoPAGATO, impBollo) = False Then
                                            'salta il pagamento e va alla voce successiva
                                            Continue For
                                        End If

                                        '(1)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(impNuovoPAGATO) & " where id=" & row1.Item("ID_VOCE1")
                                        par.cmd.ExecuteNonQuery()

                                        '(2)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        If importoMorosita <> 0 Then
                                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                            & "(" & row1.Item("ID_VOCE1") & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(Math.Round(importoMorosita, 2)) & "," & idTipoPagamento & "," & idIncasso & ")"
                                            par.cmd.ExecuteNonQuery()
                                        End If

                                        'SEGNALAZIONE 476/2016 NON POSSO PAGARE I BOLLI PARZIALMENTE

                                        importoCredito = Math.Abs(importoCredito) - importoMorosita
                                    End If

                                Else
                                    Exit For
                                End If
                            Else
                                'NEL CASO DI BOLLETTE RICLASSIFICATE (MOROSITA'/RATEIZZAZIONE)
                                If importoCredito <> 0 Then
                                    If row1.Item("ID_TIPO") = "4" Then
                                        PagaVociBolRiclass(row1.Item("ID_BOLLETTA"), importoCredito)
                                    Else
                                        'PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCredito)
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                                & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                                & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()
                        Next
                    End If
                Next
            End If


            If idBollGest <> 0 Then
                ValorizzColonneImporti(idBollGest)
            End If

            Dim errScriveInSchema As Boolean = False


            Dim importoCreditoRest As Decimal = 0
            'importoCredito = TotBollettePagabile
            'If ripartizione = True Then
            If ripartizione = False Then
                importoCredito = importoCreditoRateizz
            End If

            'If BolloPagParz.Value = "1" And importoCreditoIniziale < impBollo Then
            '    'par.myTrans.Rollback()
            '    'par.myTrans = par.OracleConn.BeginTransaction()
            '    'HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
            '    Response.Write("<script>alert('Ripartizione interrotta per pagamento parziale delle voce bollo!');self.close();</script>")
            '    Exit Sub
            'End If


            'If ripartizione = True Then
            If importoCredito > 0 Then
                'If Math.Abs(importoCredito) > 0 Then

                'importoCreditoIniziale = importoCreditoRateizz
                importoCreditoRest = importoCredito
                If ripartizione = False Then
                    importoCreditoRateizz = 0
                End If
                If statoContratto = "CHIUSO" Or par.IfEmpty(Request.QueryString("GEST"), "1") = "1" Then
                    CreditoResidInGest(idBollGest, importoCreditoRest, idContr)
                Else
                    RestituzCreditoInBoll(idBollGest, importoCreditoRest, idContr, errScriveInSchema)
                    If errScriveInSchema = True Then
                        'par.myTrans.Rollback()
                        'par.myTrans = par.OracleConn.BeginTransaction()
                        'HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
                        'Response.Write("<script>self.close();if (typeof opener.opener.opener != 'undefined'){opener.close();}</script>")
                        Exit Sub
                    End If
                End If

                If conferma0.Value = "1" Then

                    'SCRIVO EVENTO ELABORAZIONE MASSIVA DOCUMENTI
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F208','IMPORTO CREDITO INIZIALE EURO " & par.VirgoleInPunti(importoCreditoIniziale * -1) & " / COMPENSATO EURO " & par.VirgoleInPunti((importoCreditoIniziale - importoCreditoRest)) & " / ECCEDENZA EURO " & par.VirgoleInPunti((importoCreditoRest) * (-1)) & "')"
                    par.cmd.ExecuteNonQuery()

                    If statoContratto = "CHIUSO" Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                        par.cmd.ExecuteNonQuery()
                    Else

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                        par.cmd.ExecuteNonQuery()

                    End If
                    'Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")
                Else
                    If importoCreditoIniziale = importoCreditoRest Then
                        'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                        'par.cmd.ExecuteNonQuery()
                    Else
                        'IL RESTO DEL CREDITO E' STATO UTILIZZATO ATTRAVERSO LA BOLLETTE PAGATE
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F02','COPERTURA BOLLETTE SCADUTE/SOLLECITATE PER UN TOTALE DI " & ((importoCreditoIniziale - importoCreditoRest)) & "')"
                        par.cmd.ExecuteNonQuery()

                    End If
                    'Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")

                End If
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F208','IMPORTO CREDITO INIZIALE EURO " & par.VirgoleInPunti(importoCreditoIniziale * -1) & " / COMPENSATO EURO " & par.VirgoleInPunti((importoCreditoIniziale - importoCreditoRest) * -1) & " / ECCEDENZA EURO " & par.VirgoleInPunti((importoCreditoRest) * (-1)) & "')"
                par.cmd.ExecuteNonQuery()

                'AGGIORNO IL DOCUMENTO COME CONTABILE E TIPO APPLICAZIONE = 1 (: spostamento parziale)
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                par.cmd.ExecuteNonQuery()

                'Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")
            End If


        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Exit Sub
        End Try

    End Sub

    Private Function isPagabileBollo(ByVal idVoce As Integer, ByVal impPagamento As Decimal, ByRef resBollo As Decimal) As Boolean
        isPagabileBollo = True
        par.cmd.CommandText = "select (nvl(importo,0)-nvl(imp_pagato,0)) as resBollo from siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette_voci where t_voci_bolletta.id = bol_bollette_voci.id_voce and t_voci_bolletta.gruppo = 7 and bol_bollette_voci.id =" & idVoce
        resBollo = par.IfNull(par.cmd.ExecuteScalar, 0)
        If resBollo <> 0 Then
            If impPagamento < resBollo Then
                lstBolliParz.Add(idVoce)
                isPagabileBollo = False
                'Me.BolloPagParz.Value = 1
            Else
                lstBolliParz.Remove(idVoce)
            End If
        Else
            lstBolliParz.Remove(idVoce)
        End If
    End Function

    Public Function WriteEvent(ByVal TipoPadre As Boolean, ByVal ID_VOCE As String, ByVal CodEvento As String, ByVal Importo As Decimal, ByVal DataPagamento As String, ByVal idEvPadre As String, ByVal vIdIncassoExtramav As Long, ByVal idContratto As Long, Optional ByVal Motivazione As String = "", Optional ByVal idMain As String = "") As String
        Dim idPadre As String = "null"
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            If TipoPadre = True Then

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZIALI.NEXTVAL FROM DUAL"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idPadre = lettore(0)
                End If
                lettore.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI (ID,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_CONTRATTO,ID_MAIN,ID_INCASSO_EXTRAMAV,IMPORTO) " _
                & "VALUES ( " & idPadre & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & CodEvento & "','" & Motivazione & "'," & idContratto & "," & par.IfEmpty(idMain, "NULL") & "," & vIdIncassoExtramav & "," & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ")"

                'End If
                par.cmd.ExecuteNonQuery()
            Else
                'evento figlio
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT (ID,ID_EVENTO_PRINCIPALE,ID_VOCE_BOLLETTA,IMPORTO) " _
                                    & "VALUES ( SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZ_DETT.NEXTVAL," & idEvPadre & "," & ID_VOCE & ", " _
                                    & " " & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ")"
                par.cmd.ExecuteNonQuery()
            End If

            'If ConnOpenNow = True Then
            '    '*********************CHIUSURA CONNESSIONE**********************
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            'End If

            Return idPadre

        Catch ex As Exception

            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Rollback()
                End If

                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteEvent" & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try

    End Function

    Private Function AggiustaDebitoReale(ByVal dt As Data.DataTable) As Data.DataTable 'ORIGINALE
        AggiustaDebitoReale = dt.Clone
        'Dim returnValue() As Data.DataRow
        Dim negativiDisponibili As Decimal = 0
        Dim impnegativo As Decimal = 0
        Dim valida As Boolean = True


        For Each riga As Data.DataRow In dt.Rows
            If par.IfNull(riga.Item("IMPORTO"), 0) < 0 And (par.IfNull(riga.Item("IMP_PAGATO"), 0) <> par.IfNull(riga.Item("IMPORTO"), 0)) Then
                impnegativo = Math.Abs(par.IfNull(riga.Item("IMPORTO"), 0))
                For Each r As Data.DataRow In dt.Rows
                    If par.IfNull(riga.Item("GRUPPO"), 4) = par.IfNull(r.Item("GRUPPO"), 4) And par.IfNull(riga.Item("ID"), 0) <> par.IfNull(r.Item("ID"), 0) Then
                        If impnegativo <= (par.IfNull(r.Item("IMPORTO"), 0) - par.IfNull(r.Item("IMP_PAGATO"), 0)) Then
                            r.Item("IMPORTO") = par.IfNull(r.Item("IMPORTO"), 0) - impnegativo
                            'If par.IfNull(r.Item("IMP_PAGATO"), 0) <> 0 Then
                            '    r.Item("IMP_PAGATO") = par.IfNull(r.Item("IMP_PAGATO"), 0) - impnegativo
                            'End If

                            impnegativo = 0
                        Else
                            If par.IfNull(r.Item("IMP_PAGATO"), 0) <> 0 Then
                                impnegativo = impnegativo - (par.IfNull(r.Item("IMPORTO"), 0) - par.IfNull(r.Item("IMP_PAGATO"), 0))

                            Else
                                impnegativo = impnegativo - par.IfNull(r.Item("IMPORTO"), 0)

                            End If
                            r.Item("IMPORTO") = 0
                        End If
                    End If
                Next
                negativiDisponibili = negativiDisponibili + impnegativo

            End If
        Next

        While negativiDisponibili > 0
            For Each r As Data.DataRow In dt.Rows
                If par.IfNull(r.Item("IMPORTO"), 0) > 0 Then

                    If negativiDisponibili <= (par.IfNull(r.Item("IMPORTO"), 0) - par.IfNull(r.Item("IMP_PAGATO"), 0)) Then
                        r.Item("IMPORTO") = par.IfNull(r.Item("IMPORTO"), 0) - negativiDisponibili
                        negativiDisponibili = 0
                    Else
                        If par.IfNull(r.Item("IMP_PAGATO"), 0) <> 0 Then
                            negativiDisponibili = negativiDisponibili - (par.IfNull(r.Item("IMPORTO"), 0) - par.IfNull(r.Item("IMP_PAGATO"), 0))
                        Else
                            negativiDisponibili = negativiDisponibili - par.IfNull(r.Item("IMPORTO"), 0)
                        End If
                        r.Item("IMPORTO") = 0
                    End If
                End If
            Next
        End While


        For Each r0 As Data.DataRow In dt.Rows
            If par.IfNull(r0("IMPORTO"), 0) > 0 Then
                AggiustaDebitoReale.Rows.Add(r0.ItemArray)
            End If
        Next

        Return AggiustaDebitoReale
    End Function

    Private Sub PagataCompletamente(ByVal idBolletta As String)
        Try
            Dim ConnOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim PagataIntera As Boolean = False

            par.cmd.CommandText = "select sum(importo) as importo_totale , sum(nvl(imp_pagato,0)) as totale_pagato from siscom_mi.bol_bollette_voci where id_bolletta = " & idBolletta
            lettore = par.cmd.ExecuteReader

            If lettore.Read Then
                If par.IfNull(lettore("importo_totale"), 0) = par.IfNull(lettore("totale_pagato"), 0) Then
                    PagataIntera = True
                End If
            End If
            lettore.Close()

            If PagataIntera = True Then
                '***********************memorizzo l'importo pagato inserito dall'utente che ha generato un pagamento totale*********************************
                par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato_bak = imp_pagato where id_bolletta = " & idBolletta
                par.cmd.ExecuteNonQuery()

                '*********************aggiorno l'importo pagato con l'importo della voce (anche per le negative) *******************************************
                par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = importo, importo_riclassificato_pagato = importo_riclassificato where id_bolletta = " & idBolletta
                par.cmd.ExecuteNonQuery()

            End If

            If ConnOpenNow = True Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagataCompletamente" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Private Sub ValorizzColonneImporti(ByVal idBollGest As Decimal)

        Dim incassato As Decimal = 0
        Dim credito As Decimal = 0
        Dim importoEccedenza As Decimal = 0


        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & idBollGest
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            credito = par.IfNull(lettore("IMPORTO_TOTALE"), 0)
        End If
        lettore.Close()

        If idIncasso <> 0 Then
            par.cmd.CommandText = "SELECT id_bolletta,round(SUM(importo_pagato),2) AS incassato " _
                        & "FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                        & "WHERE FL_NO_REPORT=0 AND id_incasso_extramav = " & idIncasso & " " _
                        & "GROUP BY id_bolletta "
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()
            If dtVoci.Rows.Count > 0 Then
                For Each rowB As Data.DataRow In dtVoci.Rows
                    incassato = incassato + par.IfNull(rowB.Item("incassato"), 0)
                Next
            End If
        End If
        importoEccedenza = Math.Abs(credito) - Math.Abs(incassato)
        par.cmd.CommandText = "update siscom_mi.incassi_extramav set importo_incassato = " & par.insDbValue(incassato, False) & ", " _
                & "importo_eccedenza=" & par.insDbValue((importoEccedenza) * -1, False) & " where id = " & idIncasso
        par.cmd.ExecuteNonQuery()

    End Sub

    Private Sub PagaBolRateizzazione(ByVal idBolletta As Integer, ByRef ImpForRateiz As Decimal)
        Try
            Dim quotaCapitale As Decimal = 0

            'RIF MAIL AMISANO 25/08/2017 MANCAVA NVL(IMP_PAGATO,0) E COMUNQUE NON SI CAPISCE PERCHE' FACCIA QUESTO
            par.cmd.CommandText = "SELECT (IMPORTO-nvl(IMP_PAGATO,0)) AS IMP FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_VOCE in (676,677) and ID_BOLLETTA=" & idBolletta
            Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderI.Read Then
                quotaCapitale = par.IfNull(myReaderI("IMP"), 0)
                'RIF MAIL AMISANO 25/08/2017 SE PAGO PARZIALMENTE UNA RATA, 
                'NON POSSO USARE RIPARTIRE L'INTERO IMPORTO DELL'INCASSO SULLE RICLA, MA SOLO QUELLO INERENTE ALLA QUOTA CAPITALI. MANCAVA L'IF...
                If ImpForRateiz > quotaCapitale Then
                    ImpForRateiz = quotaCapitale
                End If
            End If
            myReaderI.Close()


            par.cmd.CommandText = "SELECT ID,IMPORTO_RIC_B FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_RATEIZZAZIONE = " _
                                & "(SELECT ID_RATEIZZAZIONE FROM SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE ID_BOLLETTA = " & idBolletta & ")" _
                                & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBoll As New Data.DataTable()
            da.Fill(dtBoll)
            'ImpForRateiz = Importo
            Dim idBoll As String = "0"
            For Each r As Data.DataRow In dtBoll.Rows
                If ImpForRateiz > 0 Then
                    'If par.IfNull(r.Item("IMPORTO_RIC_B"), 0) > 0 Then


                    'End If
                    PagaVociBolRiclass(r.Item("ID"), ImpForRateiz)

                    '25/07/2012 se pagata completamente la riclassificata vengono allineati gli importi di pagamento

                    PagataCompletamente(par.IfNull(r.Item("ID"), 0))

                End If

            Next

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagaVociBolRateizzate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub PagaVociBolRiclass(ByVal idBolRateizzata As Integer, ByRef ImpForRateiz As Decimal)
        Try
            Dim OldIdBolletta As String = 0
            Dim Pagato As Decimal = 0
            Dim QVersato As Decimal = 0
            Dim PagatoReale As Decimal = 0

            '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.TIPO_VOCE,(case when (importo - nvl(imp_pagato,0))<0 then 1000000 + abs((importo - nvl(imp_pagato,0))) else (importo - nvl(imp_pagato,0)) end) as ordine,BOL_BOLLETTE.ID_TIPO,GRUPPO " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                                & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND " _
                                & "ID_BOLLETTA = BOL_BOLLETTE.ID " _
                                & "AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                                & "AND BOL_BOLLETTE.ID = " & idBolRateizzata & " " _
                                & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL " _
                                & " ORDER BY ordine DESC"
            Dim row As Data.DataRow
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)

            dt = AggiustaDebitoReale(dt)

            For Each row In dt.Rows
                QVersato = 0
                'ImpForRateiz della voce di bolletta positivo eseguo algoritmo ripartizione 
                If par.IfNull(row.Item("IMPORTO"), 0) > 0 Then

                    Pagato = par.IfNull(row.Item("IMP_PAGATO"), 0)

                    If Pagato = 0 Then
                        If isPagabileBollo(row.Item("ID").ToString, ImpForRateiz, impBollo) = False Then
                            'salta il pagamento e va alla voce successiva
                            Continue For
                        End If
                        If ImpForRateiz > 0 And ImpForRateiz >= par.IfNull(row.Item("IMPORTO"), 0) Then
                            'se importo pagamento > importo della voce da pagare, la pago tutta
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) _
                                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()

                            lstBolliParz.Remove(row.Item("ID"))

                            'WriteEvent(False, row.Item("ID"), "F205", par.IfNull(row.Item("IMPORTO"), 0), Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto)

                            WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), CDec(par.IfNull(row.Item("IMPORTO"), 0)), idIncasso, idTipoPagamento)

                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassMoros(row.Item("ID_BOLLETTA"), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                            If par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                                If par.IfNull(row.Item("ID_VOCE"), 1) <> 95 And par.IfNull(row.Item("ID_VOCE"), 1) <> 407 And par.IfNull(row.Item("ID_VOCE"), 1) <> 678 Then
                                    PagaBolRateizzazione(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                End If
                            End If

                            ImpForRateiz = ImpForRateiz - par.IfNull(row.Item("IMPORTO"), 0)

                        ElseIf ImpForRateiz > 0 And ImpForRateiz < (par.IfNull(row.Item("IMPORTO"), 0)) Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()
                            'se importo pagamento < importo della voce da pagare, la pago per l'importo pagamento che mi resta
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpForRateiz) _
                                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()

                            lstBolliParz.Remove(row.Item("ID"))

                            'WriteEvent(False, row.Item("ID"), "F205", ImpForRateiz, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto)

                            WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), ImpForRateiz, idIncasso, idTipoPagamento)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassMoros(par.IfNull(row.Item("ID_BOLLETTA"), 0), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                            If par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                                If par.IfNull(row.Item("ID_VOCE"), 1) <> 95 And par.IfNull(row.Item("ID_VOCE"), 1) <> 407 And par.IfNull(row.Item("ID_VOCE"), 1) <> 678 Then
                                    PagaBolRateizzazione(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                End If
                            End If
                            ImpForRateiz = ImpForRateiz - ImpForRateiz
                        ElseIf ImpForRateiz = 0 Then
                            Exit For
                        End If
                    ElseIf par.IfNull(row.Item("IMPORTO"), 0) > par.IfNull(row.Item("IMP_PAGATO"), 0) Then

                        If ImpForRateiz > 0 And ImpForRateiz > Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2) Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()

                            QVersato = Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(QVersato) & " + NVL(IMP_PAGATO,0) " _
                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()

                            lstBolliParz.Remove(row.Item("ID"))

                            'WriteEvent(False, row.Item("ID"), "F205", QVersato, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto)

                            WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), QVersato, idIncasso, idTipoPagamento)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassMoros(row.Item("ID_BOLLETTA"), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                            If par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                                If par.IfNull(row.Item("ID_VOCE"), 1) <> 95 And par.IfNull(row.Item("ID_VOCE"), 1) <> 407 And par.IfNull(row.Item("ID_VOCE"), 1) <> 678 Then
                                    PagaBolRateizzazione(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                End If
                            End If
                            ImpForRateiz = ImpForRateiz - QVersato

                        ElseIf ImpForRateiz > 0 And ImpForRateiz <= (CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))) Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpForRateiz) & "+ NVL(IMP_PAGATO,0) " _
                                                & " WHERE ID = " & row.Item("ID")
                            par.cmd.ExecuteNonQuery()

                            lstBolliParz.Remove(row.Item("ID"))

                            'WriteEvent(False, row.Item("ID"), "F205", ImpForRateiz, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto)

                            WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), ImpForRateiz, idIncasso, idTipoPagamento)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassMoros(row.Item("ID_BOLLETTA"), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                            If par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                                If par.IfNull(row.Item("ID_VOCE"), 1) <> 95 And par.IfNull(row.Item("ID_VOCE"), 1) <> 407 And par.IfNull(row.Item("ID_VOCE"), 1) <> 678 Then
                                    PagaBolRateizzazione(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                End If
                            End If
                            ImpForRateiz = 0
                        End If

                    End If

                End If

            Next
            da.Dispose()
            dt.Dispose()
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagaVociBolRateizzate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Private Sub PagaRiclassMoros(ByVal idBollettaRic As String, ByVal ImpPaga As Decimal)
        Try

            Dim OldIdBolletta As String = 0
            Dim Pagato As Decimal = 0
            Dim TotNegativi As Decimal = 0
            Dim TotPositivi As Decimal = 0
            Dim PercIncidenza As Decimal = 0

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID,  (case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) as ordine " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                        & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) " _
                        & "AND ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                        & "AND IMPORTO > 0 " _
                        & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC = " & idBollettaRic & " " _
                        & "AND T_VOCI_BOLLETTA.COMPETENZA <> 3 " _
                        & "GROUP BY BOL_BOLLETTE.ID,  (case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) " _
                        & "ORDER BY ordine ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBollette As New Data.DataTable
            da.Fill(dtBollette)
            Dim idBolletta As String = "0"

            For Each r As Data.DataRow In dtBollette.Rows

                ''*************26/08/2011 Inserimento della data di pagamento per successivi UNDO************
                'If idBolletta <> par.IfNull(r.Item("ID"), 0) Then
                '    idBolletta = par.IfNull(r.Item("ID"), 0)
                '    WriteDataPagBol(idBolletta, idEventoPrincipale)
                'End If

                '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.TIPO_VOCE,(case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) as ordine,BOL_BOLLETTE.ID_TIPO,T_VOCI_BOLLETTA.GRUPPO " _
                            & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                            & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) " _
                            & "AND ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                            & "AND BOL_BOLLETTE.ID = " & r.Item("ID") & " " _
                            & "AND T_VOCI_BOLLETTA.COMPETENZA <> 3 " _
                            & "ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC, ordine asc "
                '& "AND IMPORTO > 0 " _

                Dim row As Data.DataRow
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtMorosita As New Data.DataTable()
                da.Fill(dtMorosita)

                '25/07/2012 viene allineato il debito alle voci negative presenti nella bolletta (B.M.)
                dtMorosita = AggiustaDebitoReale(dtMorosita)


                For Each row In dtMorosita.Rows

                    'Importo della voce di bolletta positivo eseguo algoritmo ripartizione 
                    If row.Item("IMPORTO") > 0 Then
                        'PagatoReale = par.IfNull(row.Item("IMP_PAGATO"), 0)
                        'PercIncidenza = Math.Round((row.Item("IMPORTO") * 100) / TotPositivi, 6)
                        'If TotNegativi > 0 Then
                        '    row.Item("IMP_PAGATO") = par.IfNull(row.Item("IMP_PAGATO"), 0) + Math.Round((TotNegativi * PercIncidenza) / 100, 6)
                        'End If
                        Pagato = par.IfNull(row.Item("IMP_PAGATO"), 0)
                        If Pagato = 0 Then
                            If ImpPaga > 0 And ImpPaga >= par.IfNull(row.Item("IMPORTO"), 0) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                                par.cmd.ExecuteNonQuery()


                                'se importo pagamento > importo della voce da pagare, la pago tutta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                lstBolliParz.Remove(row.Item("ID"))

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMPORTO_RICLASSIFICATO_PAGATO = ((NVL(IMP_PAGATO,0) + NVL(IMPORTO_RICLASSIFICATO,0))-NVL(IMPORTO,0)) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), CDec(par.IfNull(row.Item("IMPORTO"), 0)), idIncasso, idTipoPagamento)


                                '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************
                                ImpPaga = ImpPaga - par.IfNull(row.Item("IMPORTO"), 0)

                            ElseIf ImpPaga > 0 And ImpPaga < (par.IfNull(row.Item("IMPORTO"), 0)) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                                par.cmd.ExecuteNonQuery()

                                'se importo pagamento < importo della voce da pagare, la pago per l'importo pagamento che mi resta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                lstBolliParz.Remove(row.Item("ID"))

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMPORTO_RICLASSIFICATO_PAGATO = " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()


                                WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), ImpPaga, idIncasso, idTipoPagamento)



                                ImpPaga = ImpPaga - ImpPaga

                            ElseIf ImpPaga = 0 Then
                                Exit For
                            End If
                        ElseIf par.IfNull(row.Item("IMPORTO"), 0) > par.IfNull(row.Item("IMP_PAGATO"), 0) Then

                            If ImpPaga > 0 And ImpPaga > Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)) & " + NVL(IMP_PAGATO,0) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                lstBolliParz.Remove(row.Item("ID"))

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO_PAGATO = ((NVL(IMP_PAGATO,0) + NVL(IMPORTO_RICLASSIFICATO,0))-NVL(IMPORTO,0)) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()


                                WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0)), idIncasso, idTipoPagamento)

                                ImpPaga = ImpPaga - Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)

                            ElseIf ImpPaga > 0 And ImpPaga <= (CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpPaga) & "+ NVL(IMP_PAGATO,0) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                lstBolliParz.Remove(row.Item("ID"))

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO_PAGATO = NVL(IMPORTO_RICLASSIFICATO_PAGATO,0) + " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), ImpPaga, idIncasso, idTipoPagamento)
                                ImpPaga = 0
                            End If
                        End If
                    End If
                Next
                da.Dispose()
                dtMorosita.Dispose()

                '25/07/2012 se pagata completamente la riclassificata vengono allineati gli importi di pagamento (B.M.)
                PagataCompletamente(par.IfNull(r.Item("ID"), 0))

            Next
            dtBollette.Dispose()
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagaRiclassificate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub WriteVociPagamenti(ByVal idVoceBolletta As Integer, ByVal dataPagamento As String, ByVal importo As Decimal, ByVal idIncassoExt As Integer, ByVal tipo As Integer)
        Try
            If importo > 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                    & "(" & idVoceBolletta & ",'" & par.AggiustaData(dataPagamento) & "'," & par.VirgoleInPunti(Math.Round(importo, 2)) & "," & tipo & "," & idIncassoExt & ")"
                par.cmd.ExecuteNonQuery()
            End If

        Catch ex As Exception

            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If

            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteEvent" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Private Sub WriteVociPagRicla(ByVal idVoceBolletta As Integer, ByVal dataPagamento As String, ByVal importo As Decimal, ByVal idIncassoExt As Integer, ByVal tipo As Integer)

        Try
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                & "(" & idVoceBolletta & ",'" & par.AggiustaData(dataPagamento) & "'," & par.VirgoleInPunti(Math.Round(importo, 2)) & "," & tipo & "," & idIncassoExt & ")"
            par.cmd.ExecuteNonQuery()

        Catch ex As Exception

            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If

            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteEvent" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Public Property dtdatiInsert() As Data.DataTable
        Get
            If Not (ViewState("dtdatiInsert") Is Nothing) Then
                Return ViewState("dtdatiInsert")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtdatiInsert") = value
        End Set
    End Property

    Private Sub CreditoResidInGest(ByVal idBollGest As Long, ByVal importoCreditoRest As Decimal, ByVal idContr As Long)

        Dim ID_VOCE As Integer = 0
        Dim MiaData As Date = DateAdd(DateInterval.Second, 1, Now)
        Dim idIncassoExtraMav As String = "NULL"
        If idIncasso > 0 Then
            idIncassoExtraMav = idIncasso
        End If
        Try
            If importoCreditoRest <> 0 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & idBollGest & ""
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & idBollGest
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        ID_VOCE = myReader2("ID_VOCE")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA, RIFERIMENTO_DA, RIFERIMENTO_A, IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO, TIPO_APPLICAZIONE, DATA_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE, ID_EVENTO_PAGAMENTO) VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL, " & par.IfNull(myReader("ID_CONTRATTO"), "NULL") & ", 29, " & par.IfNull(myReader("ID_UNITA"), "NULL") & ", " & par.IfNull(myReader("ID_ANAGRAFICA"), "NULL") & ",'" & par.IfNull(myReader("RIFERIMENTO_DA"), "NULL") & "', '" & par.IfNull(myReader("RIFERIMENTO_A"), "NULL") & "', " & par.VirgoleInPunti(importoCreditoRest * -1) & ", '" & par.IfNull(myReader("DATA_EMISSIONE"), "NULL") & "', NULL,NULL, " & par.IfNull(myReader("ID_TIPO"), "NULL") & ", 'N', NULL, '','Importo residuo in seguito a ripartizione credito " & idBollGest & "', " & idIncassoExtraMav & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO, IMP_PAGATO, FL_ACCERTATO) VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL, " & myReader1(0) & ", " & ID_VOCE & ", " & par.VirgoleInPunti(importoCreditoRest * -1) & ", NULL, NULL)"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(MiaData, "yyyyMMddHHmmss") & "'," _
                                    & "'F204','ECCEDENZA DI EURO " & importoCreditoRest & " A SEGUITO DI RIPARTIZIONE CREDITO GESTIONALE')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()
                End If
                myReader.Close()
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub RestituzCreditoInBoll(ByVal idBollGest As Long, ByVal importoCreditoRest As Decimal, ByVal idContr As Long, ByRef errore As Boolean)
        Try

            Dim importoIniziale As Decimal = 0
            Dim num_rate As Decimal = 0
            Dim dataDecorr As String = ""
            Dim IDUNITA As Long = 0

            Dim importoCredito As Decimal = 0

            Dim importoSpesa300 As Decimal = 0
            Dim importoSpesa301 As Decimal = 0
            Dim importoSpesa302 As Decimal = 0
            Dim importoSpesa303 As Decimal = 0
            Dim importoSpesaTOT As Decimal = 0

            Dim BOLLO_BOLLETTA As Decimal = 0
            Dim SPESEPOSTALI As Decimal = 0
            Dim SPMAV As Decimal = 0
            Dim vociAutomatiche As Decimal = 0


            Dim importoNewBolletta As Decimal = 0

            Dim rataProssima As Integer = 0

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                importoIniziale = par.IfNull(myReader0("IMP_CANONE_INIZIALE"), 0)
                num_rate = par.IfNull(myReader0("NRO_RATE"), 0)
                dataDecorr = par.IfNull(myReader0("DATA_DECORRENZA"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContr & ""
            Dim myReaderU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderU.Read Then
                IDUNITA = par.IfNull(myReaderU("ID_UNITA"), 0)
            End If
            myReaderU.Close()

            Dim prossimaBolletta As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.rapporti_utenza_prossima_bol WHERE ID_CONTRATTO=" & idContr
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                prossimaBolletta = par.IfNull(myReader0("PROSSIMA_BOLLETTA"), "")
                rataProssima = CInt(Mid(prossimaBolletta, 5, 2))
            End If
            myReader0.Close()

            Dim annoSchema As Integer = Year(Now)
            Dim esercFinanz As Integer = 0

            If Mid(prossimaBolletta, 1, 4) <> Year(Now) Then
                esercFinanz = par.RicavaEsercizio(Mid(prossimaBolletta, 1, 4))

                par.cmd.CommandText = "select * from siscom_mi.pf_main where ID_ESERCIZIO_FINANZIARIO =" & esercFinanz
                myReader0 = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    If par.IfNull(myReader0("APPLICAZIONE_BOL"), 0) = 1 Then
                        annoSchema = Mid(prossimaBolletta, 1, 4)
                    Else
                        annoSchema = Year(Now)
                    End If
                End If
                myReader0.Close()
            End If

            par.cmd.CommandText = "SELECT * from SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContr & " AND ANNO=" & annoSchema & ""
            Dim daSpese As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtSpese As New Data.DataTable
            daSpese = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daSpese.Fill(dtSpese)
            daSpese.Dispose()
            Dim impVoceNegativo As Decimal = 0
            For Each row As Data.DataRow In dtSpese.Rows
                Select Case row.Item("ID_VOCE")
                    Case "300"
                        importoSpesa300 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "301"
                        importoSpesa301 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "302"
                        importoSpesa302 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "303"
                        importoSpesa303 = row.Item("IMPORTO_SINGOLA_RATA")
                End Select
                If row.Item("IMPORTO_SINGOLA_RATA") < 0 Then
                    If rataProssima <= (row.Item("DA_RATA") + row.Item("PER_RATE")) Then
                        impVoceNegativo += row.Item("IMPORTO_SINGOLA_RATA")
                    End If
                End If
            Next

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                BOLLO_BOLLETTA = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=17"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPESEPOSTALI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPMAV = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            vociAutomatiche = BOLLO_BOLLETTA + SPESEPOSTALI + SPMAV


            Dim ESERCIZIOF As Long = 0

            Dim dataProssimoPeriodo As Integer = 0

            Dim rate As Integer = 0
            Dim importoDifferenza As Decimal = 0
            Dim importoCredRateizz As Decimal = 0

            ESERCIZIOF = par.RicavaEsercizioCorrente

            importoSpesaTOT = importoSpesa300 + importoSpesa301 + importoSpesa302 + importoSpesa303 + vociAutomatiche + impVoceNegativo

            'IMPORTO ANNUALE: voci canone + eventuali altre spese + spese fisse
            importoNewBolletta = Format((importoIniziale / num_rate) + importoSpesaTOT, "##,##0.00")

            Dim annoBolletta As Integer = Mid(prossimaBolletta, 1, 4)

            Dim mesePrecUltimaBolletta As String = ""

            If Mid(prossimaBolletta, 5, 1) = "0" Then
                If Mid(prossimaBolletta, 5, 4) = "01" Then
                    mesePrecUltimaBolletta = "12"
                Else
                    mesePrecUltimaBolletta = "0" & Mid(prossimaBolletta, 6, 4) - 1
                End If
            Else
                mesePrecUltimaBolletta = Mid(prossimaBolletta, 5, 4) - 1
            End If

            'If mesePrecUltimaBolletta = "12" Then
            '    annoBolletta = annoBolletta - 1
            'End If


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & idBollGest
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()

            For Each row As Data.DataRow In dtVoci.Rows
                importoCredito = importoCreditoRest / dtVoci.Rows.Count

                importoDifferenza = importoNewBolletta - importoCredito
                If importoDifferenza < 0 Then
                    Dim i As Integer = 0
                    While importoCredito > importoNewBolletta
                        rate = rate + (importoCredito / importoNewBolletta)
                        rate = Format(rate, "0")
                        importoCredito = importoCredito / rate
                        i = i + 1
                    End While

                    importoCredito = importoCreditoRest / rate

                    par.scritturaSchemaBollette(idContr, IDUNITA, row.Item("ID_VOCE"), importoCreditoRest * -1, rate, annoBolletta & Format(CInt(rataProssima), "00"), , row.Item("ID"), annoBolletta & Format(rataProssima, "00"), 2)

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F184','RIPARTIZIONE PARZIALE DI IMPORTO A CREDITO GESTIONALE')"
                    par.cmd.ExecuteNonQuery()
                Else
                    rate = 1
                    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & row.Item("ID_VOCE") & ", " & (par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))) & " , " & annoBolletta & "," & par.IfNull(row.Item("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            'If conferma0.Value = "1" Then
            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            '                & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            '                & "'F184','RIPARTIZIONE PARZIALE DI IMPORTO A CREDITO GESTIONALE')"
            '    par.cmd.ExecuteNonQuery()
            'End If

            If dtdatiInsert.Rows.Count > 0 Then
                ConfEsegui()
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ConfEsegui()
        If dtdatiInsert.Rows.Count > 0 Then
            If riga.Value < dtdatiInsert.Rows.Count Then
                For I As Integer = riga.Value To dtdatiInsert.Rows.Count
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "ConfermaRate(" & dtdatiInsert.Rows(I).Item("rate") & ");", True)
                    Exit For
                Next
                'Else
                '    Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")

                '    '*******************RICHIAMO LA CONNESSIONE*********************
                '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                '    par.SettaCommand(par)
                '    '*******************RICHIAMO LA TRANSAZIONE*********************
                '    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '    ‘‘par.cmd.Transaction = par.myTrans
                '    If Not IsNothing(par.myTrans) Then
                '        par.myTrans.Commit()
                '    End If
                '    par.OracleConn.Close()
                '    par.OracleConn.Dispose()
            End If
        End If
    End Sub

    Public Property Str() As String
        Get
            If Not (ViewState("par_str") Is Nothing) Then
                Return CStr(ViewState("par_str"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_str") = value
        End Set
    End Property

    Public Property idTipoPagamento() As Long
        Get
            If Not (ViewState("par_idTipoPagamento") Is Nothing) Then
                Return CLng(ViewState("par_idTipoPagamento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idTipoPagamento") = value
        End Set

    End Property


    Public Property idIncasso() As Long
        Get
            If Not (ViewState("par_idIncasso") Is Nothing) Then
                Return CLng(ViewState("par_idIncasso"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idIncasso") = value
        End Set

    End Property

    Private Sub ElaborazioneParzDebito(ByVal idContr As Long, ByVal idBollGest As Long, ByVal percentualeSost As Integer)
        Try
            Dim importoIniziale As Decimal = 0
            Dim importoNewBolletta As Decimal = 0
            Dim num_rate As Integer = 0
            Dim importoBolletta As Decimal = 0
            Dim IDUNITA As Long = 0
            Dim dataDecorr As String = ""

            '*** 
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                importoIniziale = par.IfNull(myReader0("IMP_CANONE_INIZIALE"), 0)
                num_rate = par.IfNull(myReader0("NRO_RATE"), 0)
                dataDecorr = par.IfNull(myReader0("DATA_DECORRENZA"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContr & ""
            Dim myReaderU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderU.Read Then
                IDUNITA = par.IfNull(myReaderU("ID_UNITA"), 0)
            End If
            myReaderU.Close()

            par.cmd.CommandText = "SELECT to_char(BOL_BOLLETTE_GEST.IMPORTO_TOTALE,'9G999G990D99') as IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & idBollGest
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                importoBolletta = CDec(par.IfNull(myReader1("IMP_EMESSO"), 0))
            End If
            myReader1.Close()

            Dim importoSpesa300 As Decimal = 0
            Dim importoSpesa301 As Decimal = 0
            Dim importoSpesa302 As Decimal = 0
            Dim importoSpesa303 As Decimal = 0
            Dim importoSpesaTOT As Decimal = 0

            Dim BOLLO_BOLLETTA As Decimal = 0
            Dim SPESEPOSTALI As Decimal = 0
            Dim SPMAV As Decimal = 0
            Dim vociAutomatiche As Decimal = 0

            percentualeSost = percentuale.Value
            Dim importoDaConfrontare As Decimal = 0
            Dim rate As Integer = 0
            Dim impRateizzato As Decimal = 0

            par.cmd.CommandText = "SELECT * from SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContr & " AND ANNO=" & Year(Now) & ""
            Dim daSpese As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtSpese As New Data.DataTable
            daSpese = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daSpese.Fill(dtSpese)
            daSpese.Dispose()
            For Each row As Data.DataRow In dtSpese.Rows
                Select Case row.Item("ID_VOCE")
                    Case "300"
                        importoSpesa300 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "301"
                        importoSpesa301 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "302"
                        importoSpesa302 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "303"
                        importoSpesa303 = row.Item("IMPORTO_SINGOLA_RATA")
                End Select
            Next

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            Dim myReaderPar As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderPar.Read Then
                BOLLO_BOLLETTA = CDbl(par.PuntiInVirgole(myReaderPar("VALORE")))
            End If
            myReaderPar.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=17"
            myReaderPar = par.cmd.ExecuteReader()
            If myReaderPar.Read Then
                SPESEPOSTALI = CDbl(par.PuntiInVirgole(myReaderPar("VALORE")))
            End If
            myReaderPar.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderPar = par.cmd.ExecuteReader()
            If myReaderPar.Read Then
                SPMAV = CDbl(par.PuntiInVirgole(myReaderPar("VALORE")))
            End If
            myReaderPar.Close()

            vociAutomatiche = BOLLO_BOLLETTA + SPESEPOSTALI + SPMAV

            Dim ESERCIZIOF As Long = 0
            Dim rataProssima As Integer = 0
            Dim dataProssimoPeriodo As Integer = 0
            'Dim annoDecorrenza As Integer = 0

            ESERCIZIOF = par.RicavaEsercizioCorrente

            importoSpesaTOT = importoSpesa300 + importoSpesa301 + importoSpesa302 + importoSpesa303 + vociAutomatiche
            importoNewBolletta = Format((importoIniziale / num_rate) + importoSpesaTOT, "##,##0.00")

            '***** CALCOLO PER RATA SOSTENIBILE *****
            importoDaConfrontare = (importoNewBolletta * percentualeSost) / 100

            'rataProssima = par.ProssimaRata(num_rate, dataDecorr, dataProssimoPeriodo)

            Dim prossimaBolletta As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.rapporti_utenza_prossima_bol WHERE ID_CONTRATTO=" & idContr
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                prossimaBolletta = par.IfNull(myReader0("PROSSIMA_BOLLETTA"), "")
                rataProssima = CInt(Mid(prossimaBolletta, 5, 2))
            End If
            myReader0.Close()

            'annoDecorrenza = Year(CDate(par.FormattaData(dataDecorr)))
            Dim annoBolletta As Integer = Mid(prossimaBolletta, 1, 4)

            Dim idVoce As Integer = 0
            Dim importoVoce As Decimal = 0
            Dim idBolSchema As Long = 0
            par.cmd.CommandText = "SELECT bol_bollette_voci_gest.* FROM SISCOM_MI.bol_bollette_voci_gest,siscom_mi.bol_bollette_gest WHERE bol_bollette_gest.ID=BOL_BOLLETTE_VOCI_gest.ID_BOLLETTA_GEST AND bol_bollette_gest.ID=" & idBollGest
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()
            Dim importoTettoMax As Decimal = 0
            For Each row As Data.DataRow In dtVoci.Rows
                importoVoce = row.Item("IMPORTO")
                idVoce = row.Item("ID_VOCE")
                importoTettoMax = importoDaConfrontare / dtVoci.Rows.Count
                If importoVoce > importoTettoMax Then
                    rate = Format(importoVoce / importoTettoMax, "##,##0.00")
                    rate = Format(rate, "0")
                    impRateizzato = Format(importoVoce / rate, "##,##0.00")

                    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & idVoce & ", " & (par.VirgoleInPunti(importoVoce)) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(impRateizzato)) & " , " & annoBolletta & "," & row.Item("ID") & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_SCHEMA.CURRVAL FROM DUAL"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        idBolSchema = myReaderA(0)
                    End If
                    myReaderA.Close()
                Else
                    rate = 1

                    'bol_schema
                    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & idVoce & ", " & (par.VirgoleInPunti(importoVoce)) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(importoVoce)) & " , " & annoBolletta & "," & row.Item("ID") & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            'AGGIORNO IL DOCUMENTO COME CONTABILE E TIPO APPLICAZIONE = 1 (: spostamento parziale)
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P',DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F184','APPLICAZIONE PARZIALE DI IMPORTO A DEBITO GESTIONALE')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F208','IMPORTO ELABORATO: EURO " & par.VirgoleInPunti(importoBolletta) & "')"
            par.cmd.ExecuteNonQuery()

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ElaborazioneParzCredito(ByVal idContr As Long)
        Try



        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGrid1_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        If DirectCast(DataGrid1.Items(0).Cells(0).FindControl("CheckAll"), CheckBox).Checked = True Then
            For Each di As DataGridItem In DataGrid1.Items
                DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = True
            Next
        Else
            For Each di As DataGridItem In DataGrid1.Items
                DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = False
            Next
        End If
    End Sub


    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#FFD784';}")
            'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#DCE3F5';}")

            'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('idBolletta').value=" & e.Item.Cells(0).Text & ";document.getElementById('importoBolletta').value=" & e.Item.Cells(16).Text & ";")

            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='lightsteelblue';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('idBolletta').value=" & e.Item.Cells(0).Text & ";document.getElementById('importoBolletta').value=" & e.Item.Cells(17).Text & ";")


        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#FFD784';}")
            'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#F7F7F7';}")

            'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('idBolletta').value=" & e.Item.Cells(0).Text & ";document.getElementById('importoBolletta').value=" & e.Item.Cells(16).Text & ";")

            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='lightsteelblue';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('idBolletta').value=" & e.Item.Cells(0).Text & ";document.getElementById('importoBolletta').value=" & e.Item.Cells(17).Text & ";")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        Try

            If e.NewPageIndex >= 0 Then
                Dim chkPagCorrente As Boolean = False
                DataGrid1.CurrentPageIndex = e.NewPageIndex

                Dim dtAppoggio2 As New Data.DataTable
                Dim RIGA As Data.DataRow
                controlloSelezione.Value = 0
                controlloParziale.Value = 0

                dtAppoggio2 = Session.Item("dtAppoggio")

                Dim chkSelezione As System.Web.UI.WebControls.CheckBox
                Dim chkParziale As System.Web.UI.WebControls.CheckBox

                Dim dtVuota As Boolean = True
                Dim listaIdBoll0 As New System.Collections.Generic.List(Of String)

                If dtAppoggio2.Rows.Count > 0 Then
                    dtVuota = False
                    For Each rowAppoggio As Data.DataRow In dtAppoggio2.Rows
                        If rowAppoggio.Item("ID_BOLLETTA") <> "-1" Then
                            listaIdBoll0.Add(rowAppoggio.Item("ID_BOLLETTA") & rowAppoggio.Item("fl_parziale"))
                        End If
                    Next
                Else
                    dtVuota = True
                End If

                For Each oDataGridItem In Me.DataGrid1.Items
                    chkSelezione = oDataGridItem.FindControl("ChSelezionato")
                    chkParziale = oDataGridItem.FindControl("ChParziale")

                    If chkSelezione.Checked Then
                        If dtAppoggio2.Rows.Count > 0 And dtVuota = False Then
                            dtVuota = False
                            'For K As Integer = 0 To DataGrid1.Items.Count - 1
                            If listaIdBoll0.Contains(oDataGridItem.Cells(16).Text & "SI") Then

                            ElseIf listaIdBoll0.Contains(oDataGridItem.Cells(16).Text & "NO") Then

                            Else
                                RIGA = dtAppoggio2.NewRow()
                                If oDataGridItem.Cells(0).FindControl("ChSelezionato").Checked Then
                                    RIGA.Item("id_bolletta") = oDataGridItem.Cells(16).Text
                                    RIGA.Item("id_contratto") = oDataGridItem.Cells(0).Text
                                    RIGA.Item("importo") = oDataGridItem.Cells(17).Text
                                End If

                                If oDataGridItem.Cells(15).FindControl("ChParziale").Checked Then
                                    RIGA.Item("fl_parziale") = "SI"
                                Else
                                    RIGA.Item("fl_parziale") = "NO"
                                End If
                                dtAppoggio2.Rows.Add(RIGA)
                            End If
                            'Next
                        Else
                            dtVuota = True
                            RIGA = dtAppoggio2.NewRow()
                            RIGA.Item("id_bolletta") = oDataGridItem.Cells(16).Text
                            RIGA.Item("id_contratto") = oDataGridItem.Cells(0).Text
                            RIGA.Item("importo") = oDataGridItem.Cells(17).Text
                            If chkParziale.Checked Then
                                RIGA.Item("fl_parziale") = "SI"
                            Else
                                RIGA.Item("fl_parziale") = "NO"
                            End If
                            dtAppoggio2.Rows.Add(RIGA)
                        End If
                    Else
                        'RIGA = dtAppoggio2.NewRow()
                        'RIGA.Item("id_bolletta") = "-1"
                        'RIGA.Item("id_contratto") = ""
                        'RIGA.Item("importo") = ""
                        'dtAppoggio2.Rows.Add(RIGA)
                    End If
                Next

                Session.Item("dtAppoggio") = dtAppoggio2

                'Dim j As Integer = 0
                'Dim confermaCheck As Integer = 0
                'Do While j < dtAppoggio2.Rows.Count
                '    Dim rowScart As Data.DataRow = dtAppoggio2.Rows(j)
                '    If rowScart.Item("ID_BOLLETTA") = "-1" Then
                '        rowScart.Delete()
                '    End If
                '    j = j + 1
                'Loop
                'dtAppoggio2.AcceptChanges()

                'RIEMPIMENTO LISTA
                Dim listaIdBoll As New System.Collections.Generic.List(Of String)
                For Each rowAppoggio As Data.DataRow In dtAppoggio2.Rows
                    If rowAppoggio.Item("ID_BOLLETTA") <> "-1" Then
                        listaIdBoll.Add(rowAppoggio.Item("ID_BOLLETTA") & rowAppoggio.Item("fl_parziale"))
                    End If
                Next

                Cerca()

                Dim indiceLista As Integer = 0
                Dim tipoElaboraz As String = ""
                If dtAppoggio2.Rows.Count > 0 Then
                    'For Each rowAppoggio As Data.DataRow In dtAppoggio2.Rows
                    For K As Integer = 0 To DataGrid1.Items.Count - 1
                        'chkSelezione = DataGrid1.Items(K).Cells(0).FindControl("ChSelezionato")
                        'chkParziale = DataGrid1.Items(K).Cells(14).FindControl("ChParziale")
                        If listaIdBoll.Contains(DataGrid1.Items(K).Cells(16).Text & "SI") Then
                            DirectCast(DataGrid1.Items(K).Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = True
                            DirectCast(DataGrid1.Items(K).Cells(15).FindControl("ChParziale"), CheckBox).Checked = True
                        ElseIf listaIdBoll.Contains(DataGrid1.Items(K).Cells(16).Text & "NO") Then
                            DirectCast(DataGrid1.Items(K).Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = True
                            DirectCast(DataGrid1.Items(K).Cells(15).FindControl("ChParziale"), CheckBox).Checked = False
                        Else
                            DirectCast(DataGrid1.Items(K).Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = False
                            DirectCast(DataGrid1.Items(K).Cells(15).FindControl("ChParziale"), CheckBox).Checked = False
                        End If
                    Next
                    'Next
                Else
                    For Each oDataGridItem In Me.DataGrid1.Items
                        chkParziale = oDataGridItem.FindControl("ChParziale")
                        chkParziale.Checked = False
                    Next
                End If
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("dtAppoggio")
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub chkSelezionato_click(sender As Object, e As System.EventArgs)
        Try
            If controlloSelezione.Value = 0 Then
                For Each di As DataGridItem In DataGrid1.Items
                    If DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Enabled = True Then
                        DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = True
                    End If
                Next
                controlloSelezione.Value = 1
            Else
                For Each di As DataGridItem In DataGrid1.Items
                    If DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Enabled = True Then
                        DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = False
                    End If
                Next
                controlloSelezione.Value = 0
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub chkParziale_click(sender As Object, e As System.EventArgs)
        Try
            If controlloParziale.Value = 0 Then
                For Each di As DataGridItem In DataGrid1.Items
                    If DirectCast(di.Cells(0).FindControl("ChParziale"), CheckBox).Enabled = True Then
                        DirectCast(di.Cells(0).FindControl("ChParziale"), CheckBox).Checked = True
                    End If
                Next
                controlloParziale.Value = 1
            Else
                For Each di As DataGridItem In DataGrid1.Items
                    If DirectCast(di.Cells(0).FindControl("ChParziale"), CheckBox).Enabled = True Then
                        DirectCast(di.Cells(0).FindControl("ChParziale"), CheckBox).Checked = False
                    End If
                Next
                controlloParziale.Value = 0
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    
    Protected Sub btnConfermaRate_Click(sender As Object, e As System.EventArgs) Handles btnConfermaRate.Click
        ConfEsegui()
    End Sub

    Protected Sub Esegui_Click(sender As Object, e As System.EventArgs) Handles Esegui.Click
        'eseguo la insert, incremeto riga.value, richiamo conferma
        If dtdatiInsert.Rows.Count > 0 Then
            If riga.Value <= dtdatiInsert.Rows.Count Then
                par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                    & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) " _
                                    & "Values (siscom_mi.seq_bol_schema.nextval, " & dtdatiInsert.Rows(riga.Value).Item("id_contratto") & ", " _
                                    & dtdatiInsert.Rows(riga.Value).Item("id_unita") & ", " & dtdatiInsert.Rows(riga.Value).Item("esercizio_f") & ", " _
                                    & dtdatiInsert.Rows(riga.Value).Item("id_voce") & ", " & dtdatiInsert.Rows(riga.Value).Item("importo_cred") & " , " _
                                    & dtdatiInsert.Rows(riga.Value).Item("rataProssima") & ", " & dtdatiInsert.Rows(riga.Value).Item("rate") & ", " _
                                    & dtdatiInsert.Rows(riga.Value).Item("importo_cred_sing") & " , " & dtdatiInsert.Rows(riga.Value).Item("anno") & "," _
                                    & dtdatiInsert.Rows(riga.Value).Item("id_voce_boll_gest") & ")"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & dtdatiInsert.Rows(riga.Value).Item("id_contratto") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F184','RIPARTIZIONE PARZIALE DI IMPORTO A CREDITO GESTIONALE')"
                par.cmd.ExecuteNonQuery()

                riga.Value = riga.Value + 1

                ConfEsegui()
            End If
        End If
    End Sub
End Class
