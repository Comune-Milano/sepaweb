
Partial Class ANAUT_Abb_Automatico_p2
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim dt0 As New System.Data.DataTable
    Dim dt1 As New System.Data.DataTable
    Dim dt2 As New System.Data.DataTable
    Dim dt3 As New System.Data.DataTable
    Dim dt4 As New System.Data.DataTable
    Dim ROW0 As System.Data.DataRow
    Dim ROW1 As System.Data.DataRow
    Dim ROW2 As System.Data.DataRow
    Dim ROW3 As System.Data.DataRow
    Dim ROW4 As System.Data.DataRow


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If IsPostBack = False Then
            Response.Flush()
            Riempi()

        End If
    End Sub

    Public Property associato() As Integer
        Get
            If Not (ViewState("par_associato") Is Nothing) Then
                Return CInt(ViewState("par_associato"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_associato") = value
        End Set

    End Property

    Public Property Gruppo() As Integer
        Get
            If Not (ViewState("par_Gruppo") Is Nothing) Then
                Return CInt(ViewState("par_Gruppo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Gruppo") = value
        End Set

    End Property


    Public Property lIndice() As Long
        Get
            If Not (ViewState("par_lIndice") Is Nothing) Then
                Return CLng(ViewState("par_lIndice"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIndice") = value
        End Set

    End Property

    Private Sub Riempi()
        Try
            Dim ElencoUnita As String = Session.Item("ElencoSimulazione")
            Dim Associazioni
            Dim i As Integer = 0
            Dim Colore As String = "bgcolor='#B9DCFF'"
            Dim PreferenzeEspresse0 As String = ""
            Dim PreferenzeRispettate0 As String = ""
            Dim PreferenzeEspresse1 As String = ""
            Dim PreferenzeRispettate1 As String = ""
            Dim PreferenzeEspresse2 As String = ""
            Dim PreferenzeRispettate2 As String = ""
            Dim PreferenzeEspresse3 As String = ""
            Dim PreferenzeRispettate3 As String = ""
            Dim PreferenzeEspresse4 As String = ""
            Dim PreferenzeRispettate4 As String = ""


            Dim Candidato0 As String = ""
            Dim Candidato1 As String = ""
            Dim Candidato2 As String = ""
            Dim Candidato3 As String = ""
            Dim Candidato4 As String = ""


            Dim strSQLdomanda As String = ""
            Dim tipoERP As String = ""

            dt0.Columns.Add("ID_DOMANDA")
            dt0.Columns.Add("ISBARC_R")
            dt0.Columns.Add("COMP_NUCLEO")
            dt0.Columns.Add("POSIZIONE")
            dt0.Columns.Add("PG")
            dt0.Columns.Add("ASSEGNATO_ERP")
            dt0.Columns.Add("NOMINATIVO")
            dt0.Columns.Add("TIPO_ALLOGGIO")

            dt1.Columns.Add("ID_DOMANDA")
            dt1.Columns.Add("ISBARC_R")
            dt1.Columns.Add("COMP_NUCLEO")
            dt1.Columns.Add("POSIZIONE")
            dt1.Columns.Add("PG")
            dt1.Columns.Add("ASSEGNATO_ERP")
            dt1.Columns.Add("NOMINATIVO")
            dt1.Columns.Add("TIPO_ALLOGGIO")

            dt2.Columns.Add("ID_DOMANDA")
            dt2.Columns.Add("ISBARC_R")
            dt2.Columns.Add("COMP_NUCLEO")
            dt2.Columns.Add("POSIZIONE")
            dt2.Columns.Add("PG")
            dt2.Columns.Add("ASSEGNATO_ERP")
            dt2.Columns.Add("NOMINATIVO")
            dt2.Columns.Add("TIPO_ALLOGGIO")

            dt3.Columns.Add("ID_DOMANDA")
            dt3.Columns.Add("ISBARC_R")
            dt3.Columns.Add("COMP_NUCLEO")
            dt3.Columns.Add("POSIZIONE")
            dt3.Columns.Add("PG")
            dt3.Columns.Add("ASSEGNATO_ERP")
            dt3.Columns.Add("NOMINATIVO")
            dt3.Columns.Add("TIPO_ALLOGGIO")

            dt4.Columns.Add("ID_DOMANDA")
            dt4.Columns.Add("ISBARC_R")
            dt4.Columns.Add("COMP_NUCLEO")
            dt4.Columns.Add("POSIZIONE")
            dt4.Columns.Add("PG")
            dt4.Columns.Add("ASSEGNATO_ERP")
            dt4.Columns.Add("NOMINATIVO")
            dt4.Columns.Add("TIPO_ALLOGGIO")


            PAR.OracleConn.Open()
            par.SettaCommand(par)

            'tipo = 0 erp normale
            For t As Integer = 0 To 4
                strSQLdomanda = ""

                strSQLdomanda = "SELECT   DOMANDE_BANDO.tipo_alloggio, BANDI_GRADUATORIA_DEF.tipo," _
                                    & "BANDI_GRADUATORIA_DEF.posizione, BANDI_GRADUATORIA_DEF.ID AS D11," _
                                    & " BANDI_GRADUATORIA_DEF.id_domanda, DICHIARAZIONI.n_comp_nucleo," _
                                    & " COMP_NUCLEO.cognome, COMP_NUCLEO.nome, DOMANDE_BANDO.pg," _
                                    & " DECODE (BANDI_GRADUATORIA_DEF.tipo, 1, 'Art.14') AS Art," _
                                    & " DOMANDE_BANDO.ID, DOMANDE_BANDO.fl_invito," _
                                    & " TRUNC (DOMANDE_BANDO.reddito_isee, 2) AS reddito_isee," _
                                    & " TRUNC (DOMANDE_BANDO.isbarc_r, 4) AS isbarc_r," _
                                    & " DOMANDE_BANDO.fl_controlla_requisiti," _
                                    & " T_TIPO_PRATICHE.descrizione AS D1" _
                                    & " FROM BANDI_GRADUATORIA_DEF," _
                                    & " DOMANDE_BANDO," _
                                    & " T_TIPO_PRATICHE," _
                                    & " COMP_NUCLEO," _
                                    & " DICHIARAZIONI " _
                                    & " WHERE BANDI_GRADUATORIA_DEF.tipo= " & t & " " _
                                    & " AND DOMANDE_BANDO.fl_controlla_requisiti = '2' " _
                                    & " AND DOMANDE_BANDO.fl_ass_esterna = '1' " _
                                    & " AND DOMANDE_BANDO.id_dichiarazione = DICHIARAZIONI.ID " _
                                    & " AND BANDI_GRADUATORIA_DEF.id_domanda = DOMANDE_BANDO.ID(+)  " _
                                    & " AND DOMANDE_BANDO.progr_componente = COMP_NUCLEO.progr " _
                                    & " AND COMP_NUCLEO.id_dichiarazione = DOMANDE_BANDO.id_dichiarazione " _
                                    & " AND DOMANDE_BANDO.tipo_pratica = T_TIPO_PRATICHE.cod(+) " _
                                    & " AND (DOMANDE_BANDO.id_stato = '9' OR DOMANDE_BANDO.id_stato = '8') " _
                                    & " AND (DOMANDE_BANDO.fl_proposta = '0' OR DOMANDE_BANDO.fl_proposta IS NULL " _
                                    & "          ) " _
                                    & " AND (   DOMANDE_BANDO.ID NOT IN ( " _
                                    & " SELECT DISTINCT id_pratica " _
                                    & " FROM REL_PRAT_ALL_CCAA_ERP " _
                                    & " WHERE esito <> 3 " _
                                    & " AND esito <> 4 " _
                                    & " AND ultimo = 1) " _
                                    & " OR DOMANDE_BANDO.ID IN (SELECT DISTINCT id_pratica " _
                                    & " FROM REL_PRAT_ALL_CCAA_ERP " _
                                    & " WHERE esito = 0 AND ultimo = 1) " _
                                    & " ) AND DOMANDE_BANDO.ID IN (SELECT ID_DOMANDA FROM DOMANDE_REDDITI) " _
                                    & " ORDER BY bandi_graduatoria_def.posizione ASC,domande_bando.DATA_PG ASC,domande_bando.isbarc_r DESC "

                'BANDO CAMBI
                If t = 3 Then
                    strSQLdomanda = "SELECT   DOMANDE_BANDO_CAMBI.tipo_alloggio, BANDI_GRADUATORIA_DEF_CAMBI.tipo," _
                    & "BANDI_GRADUATORIA_DEF_CAMBI.posizione, BANDI_GRADUATORIA_DEF_CAMBI.ID AS D11," _
                    & " BANDI_GRADUATORIA_DEF_CAMBI.id_domanda, DICHIARAZIONI_CAMBI.n_comp_nucleo," _
                    & " COMP_NUCLEO_CAMBI.cognome, COMP_NUCLEO_CAMBI.nome, DOMANDE_BANDO_CAMBI.pg," _
                    & " DECODE (BANDI_GRADUATORIA_DEF_CAMBI.tipo, 1, 'Art.14') AS Art," _
                    & " DOMANDE_BANDO_CAMBI.ID, DOMANDE_BANDO_CAMBI.fl_invito," _
                    & " TRUNC (DOMANDE_BANDO_CAMBI.reddito_isee, 2) AS reddito_isee," _
                    & " TRUNC (DOMANDE_BANDO_CAMBI.isbarc_r, 4) AS isbarc_r," _
                    & " DOMANDE_BANDO_CAMBI.fl_controlla_requisiti," _
                    & " T_TIPO_PRATICHE.descrizione AS D1" _
                    & " FROM BANDI_GRADUATORIA_DEF_CAMBI," _
                    & " DOMANDE_BANDO_CAMBI," _
                    & " T_TIPO_PRATICHE," _
                    & " COMP_NUCLEO_CAMBI," _
                    & " DICHIARAZIONI_CAMBI " _
                    & " WHERE " _
                    & " DOMANDE_BANDO_CAMBI.fl_controlla_requisiti = '2' " _
                    & " AND DOMANDE_BANDO_CAMBI.fl_ass_esterna = '1' " _
                    & " AND DOMANDE_BANDO_CAMBI.id_dichiarazione = DICHIARAZIONI_CAMBI.ID " _
                    & " AND BANDI_GRADUATORIA_DEF_CAMBI.id_domanda = DOMANDE_BANDO_CAMBI.ID(+)  " _
                    & " AND DOMANDE_BANDO_CAMBI.progr_componente = COMP_NUCLEO_CAMBI.progr " _
                    & " AND COMP_NUCLEO_CAMBI.id_dichiarazione = DOMANDE_BANDO_CAMBI.id_dichiarazione " _
                    & " AND DOMANDE_BANDO_CAMBI.tipo_pratica = T_TIPO_PRATICHE.cod(+) " _
                    & " AND (DOMANDE_BANDO_CAMBI.id_stato = '9' OR DOMANDE_BANDO_CAMBI.id_stato = '8') " _
                    & " AND (DOMANDE_BANDO_CAMBI.fl_proposta = '0' OR DOMANDE_BANDO_CAMBI.fl_proposta IS NULL " _
                    & "          ) " _
                    & " AND (   DOMANDE_BANDO_CAMBI.ID NOT IN ( " _
                    & " SELECT DISTINCT id_pratica " _
                    & " FROM REL_PRAT_ALL_CCAA_ERP " _
                    & " WHERE esito <> 3 " _
                    & " AND esito <> 4 " _
                    & " AND ultimo = 1) " _
                    & " OR DOMANDE_BANDO_CAMBI.ID IN (SELECT DISTINCT id_pratica " _
                    & " FROM REL_PRAT_ALL_CCAA_ERP " _
                    & " WHERE esito = 0 AND ultimo = 1) " _
                    & " ) AND DOMANDE_BANDO_CAMBI.ID IN (SELECT ID_DOMANDA FROM DOMANDE_REDDITI_CAMBI) " _
                    & " ORDER BY DOMANDE_BANDO_CAMBI.isbarc_r DESC,N_COMP_NUCLEO DESC "
                End If

                'CAMBI IN EMERGENZA
                If t = 4 Then
                    
                    'Dim strArt22 As String = ""
                    'Dim ElencoIdDomEMERG As String = ""
                    'strArt22 = "SELECT domande_bando_vsa.ID, SUM (punteggio) AS punteggio,data_presentazione " _
                    '& "FROM tab_punti_emergenze, domande_bando_vsa_punti_em, domande_bando_vsa " _
                    '& "WHERE domande_bando_vsa_punti_em.id_punteggio = tab_punti_emergenze.ID " _
                    '& "AND domande_bando_vsa_punti_em.id_domanda = domande_bando_vsa.ID " _
                    '& "AND domande_bando_vsa.id_stato = '9' " _
                    '& "AND domande_bando_vsa.id_motivo_domanda = 4 " _
                    '& "AND (domande_bando_vsa.fl_invito = '1') " _
                    '& "AND domande_bando_vsa.id_stato <> '10' " _
                    '& "AND (domande_bando_vsa.ID NOT IN ( " _
                    '& "SELECT DISTINCT id_pratica " _
                    '& "FROM rel_prat_all_ccaa_erp WHERE esito <> 3 AND esito <> 4 AND ultimo = 1)  " _
                    '& "OR domande_bando_vsa.ID IN (SELECT DISTINCT id_pratica FROM rel_prat_all_ccaa_erp WHERE esito = 0 AND ultimo = 1)) " _
                    '& "AND domande_bando_vsa.ID IN (SELECT id_domanda FROM domande_redditi_vsa) " _
                    '& "GROUP BY domande_bando_vsa.ID,data_presentazione " _
                    '& "ORDER BY punteggio DESC"

                    'Dim daE As Oracle.DataAccess.Client.OracleDataAdapter
                    'Dim dtE As New Data.DataTable
                    'daE = New Oracle.DataAccess.Client.OracleDataAdapter(strArt22, PAR.OracleConn)

                    'daE.Fill(dtE)
                    'daE.Dispose()

                    'Dim k As Integer = 0
                    'Dim idDomPreced As Long = 0
                    'Dim idDomSucc As Long = 0

                    'If dtE.Rows.Count > 1 Then
                    '    Do While k <= dtE.Rows.Count - 1
                    '        If k <> dtE.Rows.Count - 1 Then
                    '            idDomPreced = 0
                    '            idDomSucc = 0
                    '            If PAR.IfNull(dtE.Rows(k).Item("punteggio"), 0) = PAR.IfNull(dtE.Rows(k + 1).Item("punteggio"), 0) Then

                    '                If PAR.IfNull(dtE.Rows(k).Item("data_presentazione"), "") > PAR.IfNull(dtE.Rows(k + 1).Item("data_presentazione"), "") Then
                    '                    idDomPreced = dtE.Rows(k + 1).Item("ID")
                    '                    idDomSucc = dtE.Rows(k).Item("ID")
                    '                Else
                    '                    idDomPreced = dtE.Rows(k).Item("ID")
                    '                    idDomSucc = dtE.Rows(k + 1).Item("ID")
                    '                End If

                    '                ElencoIdDomEMERG = ElencoIdDomEMERG & idDomPreced & "," & idDomSucc & ","

                    '                k = k + 1
                    '            End If
                    '        End If
                    '        If idDomPreced = 0 Or idDomSucc = 0 Then
                    '            ElencoIdDomEMERG = ElencoIdDomEMERG & dtE.Rows(k).Item("ID") & ","
                    '        End If
                    '        k = k + 1
                    '    Loop
                    'Else
                    '    ElencoIdDomEMERG = ElencoIdDomEMERG & dtE.Rows(k).Item("ID") & ","
                    'End If

                    'If ElencoIdDomEMERG <> "" Then
                    '    ElencoIdDomEMERG = "(" & Mid(ElencoIdDomEMERG, 1, Len(ElencoIdDomEMERG) - 1) & ")"


                    '    strSQLdomanda = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOMANDA,DOMANDE_BANDO_VSA.ISBARC_R,DOMANDE_BANDO_VSA.TIPO_ALLOGGIO,DOMANDE_BANDO_VSA.PG,DICHIARAZIONI_VSA.N_COMP_NUCLEO,COMP_NUCLEO_VSA.NOME,COMP_NUCLEO_VSA.COGNOME " _
                    '        & "FROM DOMANDE_BANDO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA WHERE DOMANDE_BANDO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND COMP_NUCLEO_VSA.PROGR=0 AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND DOMANDE_BANDO_VSA.ID IN " & ElencoIdDomEMERG
                    'End If
                    strSQLdomanda = "SELECT domande_bando_vsa.ID as ID_DOMANDA, SUM (punteggio) AS punteggio,data_presentazione, domande_bando_vsa.isbarc_r," _
                    & "domande_bando_vsa.tipo_alloggio, domande_bando_vsa.pg," _
                    & "dichiarazioni_vsa.n_comp_nucleo, comp_nucleo_vsa.nome," _
                    & "comp_nucleo_vsa.cognome " _
                    & "FROM tab_punti_emergenze, domande_bando_vsa_punti_em, domande_bando_vsa,comp_nucleo_vsa,dichiarazioni_vsa " _
                    & "WHERE domande_bando_vsa_punti_em.id_punteggio = tab_punti_emergenze.ID " _
                    & "AND domande_bando_vsa_punti_em.id_domanda = domande_bando_vsa.ID " _
                    & "AND domande_bando_vsa.id_stato = '9' " _
                    & "AND domande_bando_vsa.id_motivo_domanda = 4 " _
                    & "AND (domande_bando_vsa.fl_invito = '1') " _
                    & "AND domande_bando_vsa.id_stato <> '10' " _
                    & "AND (domande_bando_vsa.ID NOT IN ( " _
                    & "SELECT DISTINCT id_pratica " _
                    & "FROM rel_prat_all_ccaa_erp WHERE esito <> 3 AND esito <> 4 AND ultimo = 1)  " _
                    & "OR domande_bando_vsa.ID IN (SELECT DISTINCT id_pratica FROM rel_prat_all_ccaa_erp WHERE esito = 0 AND ultimo = 1)) " _
                    & "AND domande_bando_vsa.ID IN (SELECT id_domanda FROM domande_redditi_vsa) " _
                    & " AND domande_bando_vsa.id_dichiarazione = dichiarazioni_vsa.ID " _
                    & "And comp_nucleo_vsa.progr = 0 " _
                    & "AND comp_nucleo_vsa.id_dichiarazione = dichiarazioni_vsa.ID " _
                    & "GROUP BY domande_bando_vsa.ID," _
                    & "data_presentazione," _
                    & "domande_bando_vsa.isbarc_r," _
                    & "domande_bando_vsa.tipo_alloggio," _
                    & "domande_bando_vsa.pg," _
                    & "dichiarazioni_vsa.n_comp_nucleo," _
                    & "comp_nucleo_vsa.nome," _
                    & "comp_nucleo_vsa.cognome " _
                    & "ORDER BY punteggio DESC, data_presentazione ASC"
                End If

                lIndice = 0
                PAR.cmd.CommandText = strSQLdomanda
                Dim myReaderBANDI As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myReaderBANDI.Read
                    Select Case t
                        Case 0 'ERP NORMALI
                            ROW0 = dt0.NewRow()
                            ROW0.Item("ID_DOMANDA") = myReaderBANDI("ID_DOMANDA")
                            ROW0.Item("ISBARC_R") = myReaderBANDI("ISBARC_R")
                            ROW0.Item("COMP_NUCLEO") = myReaderBANDI("N_COMP_NUCLEO")
                            ROW0.Item("PG") = myReaderBANDI("PG")
                            ROW0.Item("POSIZIONE") = myReaderBANDI("POSIZIONE")
                            ROW0.Item("ASSEGNATO_ERP") = "0"
                            ROW0.Item("NOMINATIVO") = myReaderBANDI("COGNOME") & " " & myReaderBANDI("NOME")
                            ROW0.Item("TIPO_ALLOGGIO") = myReaderBANDI("TIPO_ALLOGGIO")
                            dt0.Rows.Add(ROW0)
                        Case 1 'ERP ART.14
                            ROW1 = dt1.NewRow()
                            ROW1.Item("ID_DOMANDA") = myReaderBANDI("ID_DOMANDA")
                            ROW1.Item("ISBARC_R") = myReaderBANDI("ISBARC_R")
                            ROW1.Item("COMP_NUCLEO") = myReaderBANDI("N_COMP_NUCLEO")
                            ROW1.Item("PG") = myReaderBANDI("PG")
                            ROW1.Item("POSIZIONE") = myReaderBANDI("POSIZIONE")
                            ROW1.Item("ASSEGNATO_ERP") = "0"
                            ROW1.Item("NOMINATIVO") = myReaderBANDI("COGNOME") & " " & myReaderBANDI("NOME")
                            ROW1.Item("TIPO_ALLOGGIO") = myReaderBANDI("TIPO_ALLOGGIO")
                            dt1.Rows.Add(ROW1)
                        Case 2 'ERP ART.15
                            ROW2 = dt2.NewRow()
                            ROW2.Item("ID_DOMANDA") = myReaderBANDI("ID_DOMANDA")
                            ROW2.Item("ISBARC_R") = myReaderBANDI("ISBARC_R")
                            ROW2.Item("COMP_NUCLEO") = myReaderBANDI("N_COMP_NUCLEO")
                            ROW2.Item("PG") = myReaderBANDI("PG")
                            ROW2.Item("POSIZIONE") = myReaderBANDI("POSIZIONE")
                            ROW2.Item("ASSEGNATO_ERP") = "0"
                            ROW2.Item("NOMINATIVO") = myReaderBANDI("COGNOME") & " " & myReaderBANDI("NOME")
                            ROW2.Item("TIPO_ALLOGGIO") = myReaderBANDI("TIPO_ALLOGGIO")
                            dt2.Rows.Add(ROW2)
                        Case 3 'BANDO CAMBI
                            ROW3 = dt3.NewRow()
                            ROW3.Item("ID_DOMANDA") = myReaderBANDI("ID_DOMANDA")
                            ROW3.Item("ISBARC_R") = myReaderBANDI("ISBARC_R")
                            ROW3.Item("COMP_NUCLEO") = myReaderBANDI("N_COMP_NUCLEO")
                            ROW3.Item("PG") = myReaderBANDI("PG")
                            ROW3.Item("POSIZIONE") = myReaderBANDI("POSIZIONE")
                            ROW3.Item("ASSEGNATO_ERP") = "0"
                            ROW3.Item("NOMINATIVO") = myReaderBANDI("COGNOME") & " " & myReaderBANDI("NOME")
                            ROW3.Item("TIPO_ALLOGGIO") = myReaderBANDI("TIPO_ALLOGGIO")
                            dt3.Rows.Add(ROW3)
                        Case 4 'CAMBI EMERGENZA
                            ROW4 = dt4.NewRow()
                            ROW4.Item("ID_DOMANDA") = myReaderBANDI("ID_DOMANDA")
                            ROW4.Item("ISBARC_R") = myReaderBANDI("ISBARC_R")
                            ROW4.Item("COMP_NUCLEO") = myReaderBANDI("N_COMP_NUCLEO")
                            ROW4.Item("PG") = myReaderBANDI("PG")
                            ROW4.Item("POSIZIONE") = ""
                            ROW4.Item("ASSEGNATO_ERP") = "0"
                            ROW4.Item("NOMINATIVO") = myReaderBANDI("COGNOME") & " " & myReaderBANDI("NOME")
                            ROW4.Item("TIPO_ALLOGGIO") = myReaderBANDI("TIPO_ALLOGGIO")
                            dt4.Rows.Add(ROW4)
                    End Select

                    lIndice = lIndice + 1
                Loop
                myReaderBANDI.Close()
            Next


            lblTabella.Text = "<table style='width:100%;' cellpadding='3' cellspacing='0'>"
            lblTabella.Text = lblTabella.Text & "<tr bgcolor='Blue' style='font-family: arial, Helvetica, sans-serif; font-size: 10pt; color: #FFFFFF'><td width='40px'></td><td width='50px'>Abbina</td><td width='150px'>Unità</td><td width='100px'>Sup.Netta</td><td width='150px'>N.Comp.Nucleo</td><td width='150px'>Preferenze espresse</td><td width='150px'>Preferenze Rispettate</td><td>Candidato</td></tr>"

            'Dim associato As Integer = 0
            'Dim Gruppo As Integer = 0
            Dim Numero As Integer = 0
            Dim supAlloggio As String = "0,00"
            Dim NumeroComponenti0 As String = "0"
            Dim NumeroComponenti1 As String = "0"
            Dim NumeroComponenti2 As String = "0"
            Dim NumeroComponenti3 As String = "0"
            Dim NumeroComponenti4 As String = "0"

            Dim dtNominativi As New System.Data.DataTable
            Dim rowNominativi As System.Data.DataRow

            dtNominativi.Columns.Add(("ID_DOMANDA"))
            dtNominativi.Columns.Add("PG_DOMANDA")
            dtNominativi.Columns.Add("NOMINATIVO")
            dtNominativi.Columns.Add(("ID_ALLOGGIO"))
            dtNominativi.Columns.Add("COD_ALLOGGIO")
            dtNominativi.Columns.Add("TIPO_BANDO")

            PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID IN " & ElencoUnita  ' COD_UNITA_IMMOBILIARE IN (SELECT COD_ALLOGGIO FROM ALLOGGI WHERE ID IN " & ElencoUnita & ")"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                Gruppo = Gruppo + 1
                Candidato0 = AssociaERP(myReader("id"), Gruppo, Numero, PreferenzeEspresse0, PreferenzeRispettate0, NumeroComponenti0, supAlloggio)
                Candidato1 = AssociaART14(myReader("id"), Gruppo, Numero, PreferenzeEspresse1, PreferenzeRispettate1, NumeroComponenti1, supAlloggio)
                Candidato2 = AssociaART15(myReader("id"), Gruppo, Numero, PreferenzeEspresse2, PreferenzeRispettate2, NumeroComponenti2, supAlloggio)
                Candidato3 = AssociaCAMBI(myReader("id"), Gruppo, Numero, PreferenzeEspresse3, PreferenzeRispettate3, NumeroComponenti3, supAlloggio)
                Candidato4 = AssociaEMERG(myReader("id"), Gruppo, Numero, PreferenzeEspresse4, PreferenzeRispettate4, NumeroComponenti4, supAlloggio)

                If Candidato0 = "" And Candidato1 = "" And Candidato2 = "" And Candidato3 = "" And Candidato4 = "" Then
                    Gruppo = i
                End If


                If Candidato0 = "" Then
                    NumeroComponenti0 = ""
                    PreferenzeEspresse0 = ""
                    PreferenzeRispettate0 = ""
                Else

                    rowNominativi = dtNominativi.NewRow()
                    rowNominativi.Item("ID_DOMANDA") = ROW0.Item("ID_DOMANDA")
                    rowNominativi.Item("PG_DOMANDA") = ROW0.Item("PG")
                    rowNominativi.Item("NOMINATIVO") = ROW0.Item("NOMINATIVO")
                    rowNominativi.Item("ID_ALLOGGIO") = idAlloggio.Value
                    rowNominativi.Item("COD_ALLOGGIO") = myReader("cod_unita_immobiliare")
                    rowNominativi.Item("TIPO_BANDO") = "E.R.P"
                    dtNominativi.Rows.Add(rowNominativi)

                    NumeroComponenti0 &= "<br />"
                    PreferenzeEspresse0 &= "<br />"
                    PreferenzeRispettate0 &= "<br />"

                    Candidato0 &= "<br />"
                End If

                If Candidato1 = "" Then
                    NumeroComponenti1 = ""
                    PreferenzeEspresse1 = ""
                    PreferenzeRispettate1 = ""
                Else
                    rowNominativi = dtNominativi.NewRow()
                    rowNominativi.Item("ID_DOMANDA") = ROW1.Item("ID_DOMANDA")
                    rowNominativi.Item("PG_DOMANDA") = ROW1.Item("PG")
                    rowNominativi.Item("NOMINATIVO") = ROW1.Item("NOMINATIVO")
                    rowNominativi.Item("ID_ALLOGGIO") = idAlloggio.Value
                    rowNominativi.Item("COD_ALLOGGIO") = myReader("cod_unita_immobiliare")
                    rowNominativi.Item("TIPO_BANDO") = "ART.14"
                    dtNominativi.Rows.Add(rowNominativi)

                    NumeroComponenti1 &= "<br />"
                    PreferenzeEspresse1 &= "<br />"
                    PreferenzeRispettate1 &= "<br />"

                    Candidato1 &= "<br />"
                End If

                If Candidato2 = "" Then
                    NumeroComponenti2 = ""
                    PreferenzeEspresse2 = ""
                    PreferenzeRispettate2 = ""
                Else
                    rowNominativi = dtNominativi.NewRow()
                    rowNominativi.Item("ID_DOMANDA") = ROW2.Item("ID_DOMANDA")
                    rowNominativi.Item("PG_DOMANDA") = ROW2.Item("PG")
                    rowNominativi.Item("NOMINATIVO") = ROW2.Item("NOMINATIVO")
                    rowNominativi.Item("ID_ALLOGGIO") = idAlloggio.Value
                    rowNominativi.Item("COD_ALLOGGIO") = myReader("cod_unita_immobiliare")
                    rowNominativi.Item("TIPO_BANDO") = "ART.15"
                    dtNominativi.Rows.Add(rowNominativi)


                    NumeroComponenti2 &= "<br />"
                    PreferenzeEspresse2 &= "<br />"
                    PreferenzeRispettate2 &= "<br />"

                    Candidato2 &= "<br />"
                End If

                If Candidato3 = "" Then
                    NumeroComponenti3 = ""
                    PreferenzeEspresse3 = ""
                    PreferenzeRispettate3 = ""
                Else
                    rowNominativi = dtNominativi.NewRow()
                    rowNominativi.Item("ID_DOMANDA") = ROW3.Item("ID_DOMANDA")
                    rowNominativi.Item("PG_DOMANDA") = ROW3.Item("PG")
                    rowNominativi.Item("NOMINATIVO") = ROW3.Item("NOMINATIVO")
                    rowNominativi.Item("ID_ALLOGGIO") = idAlloggio.Value
                    rowNominativi.Item("COD_ALLOGGIO") = myReader("cod_unita_immobiliare")
                    rowNominativi.Item("TIPO_BANDO") = "BANDO CAMBI"
                    dtNominativi.Rows.Add(rowNominativi)


                    NumeroComponenti3 &= "<br />"
                    PreferenzeEspresse3 &= "<br />"
                    PreferenzeRispettate3 &= "<br />"

                    Candidato3 &= "<br />"
                End If

                If Candidato4 = "" Then
                    NumeroComponenti4 = ""
                    PreferenzeEspresse4 = ""
                    PreferenzeRispettate4 = ""
                Else
                    rowNominativi = dtNominativi.NewRow()
                    rowNominativi.Item("ID_DOMANDA") = ROW4.Item("ID_DOMANDA")
                    rowNominativi.Item("PG_DOMANDA") = ROW4.Item("PG")
                    rowNominativi.Item("NOMINATIVO") = ROW4.Item("NOMINATIVO")
                    rowNominativi.Item("ID_ALLOGGIO") = idAlloggio.Value
                    rowNominativi.Item("COD_ALLOGGIO") = myReader("cod_unita_immobiliare")
                    rowNominativi.Item("TIPO_BANDO") = "CAMBI EMERGENZA"
                    dtNominativi.Rows.Add(rowNominativi)


                    NumeroComponenti4 &= "<br />"
                    PreferenzeEspresse4 &= "<br />"
                    PreferenzeRispettate4 &= "<br />"

                    Candidato4 &= "<br />"
                End If

                If Candidato0 <> "" Or Candidato1 <> "" Or Candidato2 <> "" Or Candidato3 <> "" Or Candidato4 <> "" Then
                    associato = associato + 1
                    lblTabella.Text = lblTabella.Text & "<tr " & Colore & " style='font-family: arial, Helvetica, sans-serif; font-size: 10pt;'><td width='40px'>" & i + 1 & ")</td><td width='50px'><input id='Checkbox" & i & "' name='idUnita" & i + 1 & "' type='checkbox' checked='checked' value='" & idAlloggio.Value & "'/></td><td width='150px'><a href=" & Chr(34) & "javascript:window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & myReader("id") & "','Dettagli','height=580,top=0,left=0,width=780');void(0);" & Chr(34) & ">" & myReader("cod_unita_immobiliare") & "</a></td><td width='150px'>" & supAlloggio & "</td>" _
                        & "<td width='150px'>" & NumeroComponenti0 & NumeroComponenti1 & NumeroComponenti2 & NumeroComponenti3 & NumeroComponenti4 & "</td><td width='150px'>" & PreferenzeEspresse0 & PreferenzeEspresse1 & PreferenzeEspresse2 & PreferenzeEspresse3 & PreferenzeEspresse4 & "</td>" _
                        & "<td width='150px'>" & PreferenzeRispettate0 & PreferenzeRispettate1 & PreferenzeRispettate2 & PreferenzeRispettate3 & PreferenzeRispettate4 & "</td><td>" & Candidato0 & Candidato1 & Candidato2 & Candidato3 & Candidato4 & "</td></tr>"
                    i = i + 1
                    ' Colore = "bgcolor='#B9DCFF'"
                    If Colore = "bgcolor='#B9DCFF'" Then
                        Colore = "bgcolor='#FFFFFF'"
                    Else
                        Colore = "bgcolor='#B9DCFF'"
                    End If
                End If
            Loop
            lblTabella.Text = lblTabella.Text & "</table>"
            myReader.Close()
            Label2.Visible = True
            Label2.Text = "Sono stati abbinati " & associato & " Alloggi/Candidati"
            Session.Add("ElencoInquilini", dtNominativi)

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label2.Visible = True
            Label2.Text = ex.Message

        End Try
    End Sub

    

    Function AssociaERP(ByVal IndiceUnita As String, ByVal Gruppo As Integer, ByVal Numero As Integer, ByRef Preferenze As String, ByRef PreferenzeRispettate As String, ByRef NumComponenti As String, ByRef SupAlloggio As String) As String
        Dim i0 As Long = 0
        Dim i1 As Long = 0
        Dim i2 As Long = 0

        Dim M As Boolean
        Dim sZona As String = ""
        Dim sTipoAlloggio As String = "0"
        Dim sStringaSQL2 As String = ""
        Dim supOK As Boolean = False
        Dim dimen As String = ""
        Dim m1 As Boolean = False
        Dim strRadioButton As String = ""
        Dim visualizzaPreferenze As String = ""

        Preferenze = ""

        For Each Me.ROW0 In dt0.Rows
            Numero = 0
            sZona = ""
            Preferenze = "--"
            PreferenzeRispettate = "--"
            'VERIFICO SE ALLOGGIO ASSEGNATO O MENO
            If ROW0.Item("ASSEGNATO_ERP") = "0" Then
                'TIPO ALLOGGIO, CANONE SOCIALE O MODERATO
                sTipoAlloggio = ROW0.Item("TIPO_ALLOGGIO")
                supOK = False
                M = False
                sZona = ""
                dimen = ""
                m1 = False
                NumComponenti = ROW0.Item("COMP_NUCLEO")



                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & ROW0.Item("ID_DOMANDA")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE

                    If myReader.Read Then

                        If PAR.IfNull(myReader("PREF_ZONA1"), "0") <> "0" Then
                            ' sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA1"), " ") & "'"
                            sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA1"), "0") & "'"
                            M = True

                        End If
                        If PAR.IfNull(myReader("PREF_ZONA2"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA3"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA4"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA4"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA4"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA5"), "0") <> "0" Then
                            If M = False Then
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA5"), "0") & "'"
                                M = True
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA5"), "0") & "'"
                                M = True
                            End If
                        End If


                        If M = True Then sZona = sZona & ") "


                        If PAR.IfNull(myReader("PREF_BARRIERE"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            Else
                                sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                                M = True
                            End If
                        End If



                        If PAR.IfNull(myReader("PREF_SUP_MIN"), "0") <> "0" Then
                            dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_SUP_MAX"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ")"
                            Else
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ") "
                            End If
                        Else
                            If m1 = True Then
                                dimen = dimen & ")"
                            End If
                        End If


                        If dimen <> "" Then
                            If M = True Then
                                sZona = sZona & " AND " & dimen
                                M = True
                            Else
                                M = True
                                sZona = sZona & " " & dimen
                            End If
                        End If


                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        If PAR.IfNull(myReader("PREF_CONDOMINIO"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                            Else
                                sZona = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '08/10/2013 PIANO TRASFORMATO IN INTERVALLO

                        '----- PREF_PIANI_DA_CON
                        '----- PREF_PIANI_A_CON

                        '----- PREF_PIANI_DA_SENZA
                        '----- PREF_PIANI_DA_CON

                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") <> "0" Then

                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & "  (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                            'sZona = " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            'm1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        '08/10/2013 FINE PIANO TRASFORMATO IN INTERVALLO

                    End If
                End If
                myReader.Close()

                'PREFERENZE ESCLUSIONI
                m1 = False
                dimen = ""

                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCLUSIONI WHERE ID_DOMANDA=" & ROW0.Item("ID_DOMANDA")
                myReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE ESCLUSIONI

                    If myReader.Read Then

                        If PAR.IfNull(myReader("ESCL_ZONA1"), "0") <> "0" Then
                            If sZona <> "" Then
                                sZona = sZona & " AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            Else
                                sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA2"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA3"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA4"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA5"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            sZona = sZona & ") "
                            M = True
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If


                        



                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If


                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                    End If
                End If
                myReader.Close()

                '------


                If M = True Then
                    Preferenze = "SI"
                Else
                    Preferenze = "NO"
                End If

                If sTipoAlloggio = "1" Then
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='1') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='1') "
                        M = True
                    End If
                Else
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='0') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='0') "
                        M = True
                    End If
                End If


                sStringaSQL2 = "SELECT " _
                               & " " _
                               & "ALLOGGI.*,(select valore from siscom_mi.dimensioni where cod_tipologia='SUP_NETTA' AND id_unita_immobiliare=unita_immobiliari.id) as supAlloggio FROM T_TIPO_PROPRIETA,ALLOGGI," _
                               & " T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,ZONA_ALER,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TO_NUMBER(ALLOGGI.ZONA)=TO_NUMBER(ZONA_ALER.ZONA) AND UNITA_IMMOBILIARI.ID=" & IndiceUnita _
                               & " AND ALLOGGI.COD_ALLOGGIO =UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
                               & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                               & " edifici.id = unita_immobiliari.id_Edificio " _
                               & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND " & sZona & " "
                PAR.cmd.CommandText = sStringaSQL2
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read = True Then
                    SupAlloggio = PAR.IfNull(myReader1("supAlloggio"), "0,00")
                    supOK = False
                    Select Case NumComponenti
                        Case 1
                            If CDbl(SupAlloggio) > 28.800000000000001 And CDbl(SupAlloggio) <= 38 Then
                                supOK = True
                            End If
                        Case 2
                            If CDbl(SupAlloggio) > 33.600000000000001 And CDbl(SupAlloggio) <= 46 Then
                                supOK = True
                            End If
                        Case 3
                            If CDbl(SupAlloggio) > 43.350000000000001 And CDbl(SupAlloggio) <= 60.340000000000003 Then
                                supOK = True
                            End If
                        Case 4
                            If CDbl(SupAlloggio) > 60.350000000000001 And CDbl(SupAlloggio) <= 71.390000000000001 Then
                                supOK = True
                            End If
                        Case 5
                            If CDbl(SupAlloggio) > 71.400000000000006 And CDbl(SupAlloggio) <= 79.040000000000006 Then
                                supOK = True
                            End If
                        Case Is >= 5
                            If CDbl(SupAlloggio) > 79.049999999999997 And CDbl(SupAlloggio) <= 200 Then
                                supOK = True
                            End If
                    End Select
                    Dim numComp As Integer = 0
                    If supOK = True Then
                        dt0.Rows(i0).Item("ASSEGNATO_ERP") = "1"
                        strRadioButton = "<input id=Radio" & Numero & " style='height:14px;width:14px;vertical-align:middle;' name='" & Gruppo & "' type='radio' value='E.R.P" & ROW0.Item("ID_DOMANDA") & "'/>GRAD.ERP - POSIZIONE:" & ROW0.Item("POSIZIONE") & " - <i>" & ROW0.Item("NOMINATIVO") & "</i>" '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        'AssociaERP = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' checked='checked' value='" & ROW0.Item("NOMINATIVO") & "'/>GRAD.ERP - POSIZIONE:" & ROW0.Item("POSIZIONE") & " - " & ROW0.Item("NOMINATIVO") '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        idAlloggio.Value = myReader1("ID")
                        If Preferenze = "SI" Then
                            visualizzaPreferenze = "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW0.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=880');void(0);" & Chr(34) & ">SI</a>"
                            'Preferenze = "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW0.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
                            PreferenzeRispettate = "SI"
                        Else
                            Preferenze = "NO"
                            PreferenzeRispettate = "--"
                        End If

                        myReader1.Close()
                        myReader.Close()
                        Exit For
                    Else
                        'ALLOGGIO NON IDONEO PER SUP./N.COMPONENTI
                    End If
                Else
                    'HA ESPRESSO PREFERENZE MA NON CI SONO ALLOGGI
                    'RIMANE SENZA ALLOGGIO
                End If
                myReader1.Close()


            End If
            i0 = i0 + 1
        Next

        '' ''ERP ART.14
        ' ''For Each Me.ROW1 In dt1.Rows
        ' ''    Numero = 1
        ' ''    sZona = ""
        ' ''    Preferenze = "--"
        ' ''    PreferenzeRispettate = "--"
        ' ''    'VERIFICO SE ALLOGGIO ASSEGNATO O MENO
        ' ''    If ROW1.Item("ASSEGNATO_ERP") = "0" Then
        ' ''        'TIPO ALLOGGIO, CANONE SOCIALE O MODERATO
        ' ''        sTipoAlloggio = ROW1.Item("TIPO_ALLOGGIO")
        ' ''        supOK = False
        ' ''        M = False
        ' ''        sZona = ""
        ' ''        dimen = ""
        ' ''        m1 = False
        ' ''        NumComponenti = ROW1.Item("COMP_NUCLEO")



        ' ''        PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & ROW1.Item("ID_DOMANDA")
        ' ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

        ' ''        If myReader.HasRows = True Then
        ' ''            'HA ESPRESSO PREFERENZE

        ' ''            If myReader.Read Then

        ' ''                If PAR.IfNull(myReader("PREF_ZONA1"), "0") <> "0" Then
        ' ''                    ' sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA1"), " ") & "'"
        ' ''                    sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA1"), "0") & "'"
        ' ''                    M = True

        ' ''                End If
        ' ''                If PAR.IfNull(myReader("PREF_ZONA2"), "0") <> "0" Then
        ' ''                    If M = False Then
        ' ''                        'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
        ' ''                        sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
        ' ''                        M = True
        ' ''                    Else
        ' ''                        'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
        ' ''                        sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_ZONA3"), "0") <> "0" Then
        ' ''                    If M = False Then
        ' ''                        'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
        ' ''                        sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
        ' ''                        M = True
        ' ''                    Else
        ' ''                        'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
        ' ''                        sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                If M = True Then sZona = sZona & ") "


        ' ''                If PAR.IfNull(myReader("PREF_BARRIERE"), "0") = "1" Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
        ' ''                    Else
        ' ''                        sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If



        ' ''                If PAR.IfNull(myReader("PREF_SUP_MIN"), "0") <> "0" Then
        ' ''                    dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.IfNull(myReader("PREF_SUP_MIN"), "0") & " "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_SUP_MAX"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.IfNull(myReader("PREF_SUP_MIN"), "0") & " AND VALORE<=" & PAR.IfNull(myReader("PREF_SUP_MAX"), "0") & ")"
        ' ''                    Else
        ' ''                        dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE<=" & PAR.IfNull(myReader("PREF_SUP_MAX"), "0") & ") "
        ' ''                    End If
        ' ''                Else
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ")"
        ' ''                    End If
        ' ''                End If


        ' ''                If dimen <> "" Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND " & dimen
        ' ''                        M = True
        ' ''                    Else
        ' ''                        M = True
        ' ''                        sZona = sZona & " " & dimen
        ' ''                    End If
        ' ''                End If


        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("PREF_QUART1"), "0") <> "0" Then
        ' ''                    dimen = "  " & PAR.IfNull(myReader("PREF_QUART1"), "0")
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_QUART2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART2"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("PREF_QUART2"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_QUART3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART3"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("PREF_QUART3"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("PREF_LOCALITA1"), "0") <> "0" Then
        ' ''                    dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA1"), "0")
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_LOCALITA2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_LOCALITA3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND COMUNI_NAZIONI.ID IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " COMUNI_NAZIONI.ID IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------


        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------

        ' ''                If PAR.IfNull(myReader("PREF_CONDOMINIO"), "0") = "1" Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
        ' ''                    Else
        ' ''                        sZona = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If




        ' ''            End If
        ' ''        End If
        ' ''        myReader.Close()

        ' ''        'PREFERENZE ESCLUSIONI
        ' ''        m1 = False
        ' ''        dimen = ""

        ' ''        PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCLUSIONI WHERE ID_DOMANDA=" & ROW1.Item("ID_DOMANDA")
        ' ''        myReader = PAR.cmd.ExecuteReader()

        ' ''        If myReader.HasRows = True Then
        ' ''            'HA ESPRESSO PREFERENZE ESCLUSIONI

        ' ''            If myReader.Read Then

        ' ''                If PAR.IfNull(myReader("ESCL_ZONA1"), "0") <> "0" Then
        ' ''                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_ZONA2"), "0") <> "0" Then
        ' ''                    If m1 = False Then
        ' ''                        sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
        ' ''                        m1 = True
        ' ''                    Else

        ' ''                        sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_ZONA3"), "0") <> "0" Then
        ' ''                    If m1 = False Then

        ' ''                        sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
        ' ''                        m1 = True
        ' ''                    Else

        ' ''                        sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    sZona = sZona & ") "
        ' ''                    M = True
        ' ''                End If


        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------

        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("ESCL_QUART1"), "0") <> "0" Then
        ' ''                    dimen = "  " & PAR.IfNull(myReader("ESCL_QUART1"), "0")
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_QUART2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART2"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("ESCL_QUART2"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_QUART3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART3"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("ESCL_QUART3"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("ESCL_LOCALITA1"), "0") <> "0" Then
        ' ''                    dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA1"), "0")
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_LOCALITA2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_LOCALITA3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND COMUNI_NAZIONI.ID not IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " COMUNI_NAZIONI.ID not IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If


        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------

        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False
        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
        ' ''                    Else
        ' ''                        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------
        ' ''                'ESCLUDI PIANO CON ASCENSORE
        ' ''                dimen = ""
        ' ''                m1 = False
        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
        ' ''                    Else
        ' ''                        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------




        ' ''            End If
        ' ''        End If
        ' ''        myReader.Close()

        ' ''        '------


        ' ''        If M = True Then
        ' ''            Preferenze = "SI"
        ' ''        Else
        ' ''            Preferenze = "NO"
        ' ''        End If

        ' ''        If sTipoAlloggio = "1" Then
        ' ''            If M = True Then
        ' ''                sZona = sZona & " AND (ALLOGGI.fl_mod='1') "
        ' ''            Else
        ' ''                sZona = " (ALLOGGI.fl_mod='1') "
        ' ''                M = True
        ' ''            End If
        ' ''        Else
        ' ''            If M = True Then
        ' ''                sZona = sZona & " AND (ALLOGGI.fl_mod='0') "
        ' ''            Else
        ' ''                sZona = " (ALLOGGI.fl_mod='0') "
        ' ''                M = True
        ' ''            End If
        ' ''        End If


        ' ''        sStringaSQL2 = "SELECT " _
        ' ''                       & " " _
        ' ''                       & "ALLOGGI.*,(select valore from siscom_mi.dimensioni where cod_tipologia='SUP_NETTA' AND id_unita_immobiliare=unita_immobiliari.id) as supAlloggio FROM T_TIPO_PROPRIETA,ALLOGGI," _
        ' ''                       & " T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,ZONA_ALER,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TO_NUMBER(ALLOGGI.ZONA)=TO_NUMBER(ZONA_ALER.COD) AND UNITA_IMMOBILIARI.ID=" & IndiceUnita _
        ' ''                       & " AND ALLOGGI.COD_ALLOGGIO =UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
        ' ''                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
        ' ''                       & " edifici.id = unita_immobiliari.id_Edificio " _
        ' ''                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND " & sZona & " "
        ' ''        PAR.cmd.CommandText = sStringaSQL2
        ' ''        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        ' ''        If myReader1.Read = True Then
        ' ''            SupAlloggio = PAR.IfNull(myReader1("supAlloggio"), "0,00")
        ' ''            supOK = False
        ' ''            Select Case NumComponenti
        ' ''                Case 1
        ' ''                    If CDbl(SupAlloggio) > 28.800000000000001 And CDbl(SupAlloggio) <= 38 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case 2
        ' ''                    If CDbl(SupAlloggio) > 33.600000000000001 And CDbl(SupAlloggio) <= 46 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case 3
        ' ''                    If CDbl(SupAlloggio) > 43.350000000000001 And CDbl(SupAlloggio) <= 60.340000000000003 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case 4
        ' ''                    If CDbl(SupAlloggio) > 60.350000000000001 And CDbl(SupAlloggio) <= 71.390000000000001 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case 5
        ' ''                    If CDbl(SupAlloggio) > 71.400000000000006 And CDbl(SupAlloggio) <= 79.040000000000006 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case Is >= 5
        ' ''                    If CDbl(SupAlloggio) > 79.049999999999997 And CDbl(SupAlloggio) <= 200 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''            End Select
        ' ''            If supOK = True Then
        ' ''                dt1.Rows(i1).Item("ASSEGNATO_ERP") = "1"

        ' ''                strRadioButton = strRadioButton & "<br /><input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' checked='checked' value='" & ROW1.Item("ID_DOMANDA") & "'/>GRAD.ERP (ART.14) - POSIZIONE:" & ROW1.Item("POSIZIONE") & " - " & ROW1.Item("NOMINATIVO") '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"

        ' ''                If Preferenze = "SI" Then
        ' ''                    'Preferenze = "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW1.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
        ' ''                    visualizzaPreferenze = visualizzaPreferenze & "<br /><a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW0.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
        ' ''                    PreferenzeRispettate = "SI"
        ' ''                Else
        ' ''                    Preferenze = "NO"
        ' ''                    PreferenzeRispettate = "--"
        ' ''                End If

        ' ''                myReader1.Close()
        ' ''                myReader.Close()
        ' ''                Exit For
        ' ''            Else
        ' ''                'ALLOGGIO NON IDONEO PER SUP./N.COMPONENTI
        ' ''            End If
        ' ''        Else
        ' ''            'HA ESPRESSO PREFERENZE MA NON CI SONO ALLOGGI
        ' ''            Beep() 'RIMANE SENZA ALLOGGIO
        ' ''        End If
        ' ''        myReader1.Close()


        ' ''    End If
        ' ''    i1 = i1 + 1
        ' ''Next


        '' ''ERP ART.15
        ' ''For Each Me.ROW2 In dt2.Rows
        ' ''    Numero = 2
        ' ''    sZona = ""
        ' ''    Preferenze = "--"
        ' ''    PreferenzeRispettate = "--"
        ' ''    'VERIFICO SE ALLOGGIO ASSEGNATO O MENO
        ' ''    If ROW2.Item("ASSEGNATO_ERP") = "0" Then
        ' ''        'TIPO ALLOGGIO, CANONE SOCIALE O MODERATO
        ' ''        sTipoAlloggio = PAR.IfNull(ROW2.Item("TIPO_ALLOGGIO"), 0)
        ' ''        supOK = False
        ' ''        M = False
        ' ''        sZona = ""
        ' ''        dimen = ""
        ' ''        m1 = False
        ' ''        NumComponenti = ROW2.Item("COMP_NUCLEO")



        ' ''        PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & ROW2.Item("ID_DOMANDA")
        ' ''        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

        ' ''        If myReader.HasRows = True Then
        ' ''            'HA ESPRESSO PREFERENZE

        ' ''            If myReader.Read Then

        ' ''                If PAR.IfNull(myReader("PREF_ZONA1"), "0") <> "0" Then
        ' ''                    ' sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA1"), " ") & "'"
        ' ''                    sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA1"), "0") & "'"
        ' ''                    M = True

        ' ''                End If
        ' ''                If PAR.IfNull(myReader("PREF_ZONA2"), "0") <> "0" Then
        ' ''                    If M = False Then
        ' ''                        'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
        ' ''                        sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
        ' ''                        M = True
        ' ''                    Else
        ' ''                        'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
        ' ''                        sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_ZONA3"), "0") <> "0" Then
        ' ''                    If M = False Then
        ' ''                        'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
        ' ''                        sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
        ' ''                        M = True
        ' ''                    Else
        ' ''                        'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
        ' ''                        sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                If M = True Then sZona = sZona & ") "


        ' ''                If PAR.IfNull(myReader("PREF_BARRIERE"), "0") = "1" Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
        ' ''                    Else
        ' ''                        sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If



        ' ''                If PAR.IfNull(myReader("PREF_SUP_MIN"), "0") <> "0" Then
        ' ''                    dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.IfNull(myReader("PREF_SUP_MIN"), "0") & " "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_SUP_MAX"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.IfNull(myReader("PREF_SUP_MIN"), "0") & " AND VALORE<=" & PAR.IfNull(myReader("PREF_SUP_MAX"), "0") & ")"
        ' ''                    Else
        ' ''                        dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE<=" & PAR.IfNull(myReader("PREF_SUP_MAX"), "0") & ") "
        ' ''                    End If
        ' ''                Else
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ")"
        ' ''                    End If
        ' ''                End If


        ' ''                If dimen <> "" Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND " & dimen
        ' ''                        M = True
        ' ''                    Else
        ' ''                        M = True
        ' ''                        sZona = sZona & " " & dimen
        ' ''                    End If
        ' ''                End If


        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("PREF_QUART1"), "0") <> "0" Then
        ' ''                    dimen = "  " & PAR.IfNull(myReader("PREF_QUART1"), "0")
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_QUART2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART2"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("PREF_QUART2"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_QUART3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART3"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("PREF_QUART3"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("PREF_LOCALITA1"), "0") <> "0" Then
        ' ''                    dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA1"), "0")
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_LOCALITA2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_LOCALITA3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND COMUNI_NAZIONI.ID IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " COMUNI_NAZIONI.ID IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------


        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------

        ' ''                If PAR.IfNull(myReader("PREF_CONDOMINIO"), "0") = "1" Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
        ' ''                    Else
        ' ''                        sZona = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If




        ' ''            End If
        ' ''        End If
        ' ''        myReader.Close()

        ' ''        'PREFERENZE ESCLUSIONI
        ' ''        m1 = False
        ' ''        dimen = ""

        ' ''        PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCLUSIONI WHERE ID_DOMANDA=" & ROW2.Item("ID_DOMANDA")
        ' ''        myReader = PAR.cmd.ExecuteReader()

        ' ''        If myReader.HasRows = True Then
        ' ''            'HA ESPRESSO PREFERENZE ESCLUSIONI

        ' ''            If myReader.Read Then

        ' ''                If PAR.IfNull(myReader("ESCL_ZONA1"), "0") <> "0" Then
        ' ''                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_ZONA2"), "0") <> "0" Then
        ' ''                    If m1 = False Then
        ' ''                        sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
        ' ''                        m1 = True
        ' ''                    Else

        ' ''                        sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_ZONA3"), "0") <> "0" Then
        ' ''                    If m1 = False Then

        ' ''                        sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
        ' ''                        m1 = True
        ' ''                    Else

        ' ''                        sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    sZona = sZona & ") "
        ' ''                    M = True
        ' ''                End If


        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------

        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("ESCL_QUART1"), "0") <> "0" Then
        ' ''                    dimen = "  " & PAR.IfNull(myReader("ESCL_QUART1"), "0")
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_QUART2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART2"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("ESCL_QUART2"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_QUART3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART3"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("ESCL_QUART3"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("ESCL_LOCALITA1"), "0") <> "0" Then
        ' ''                    dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA1"), "0")
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_LOCALITA2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_LOCALITA3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
        ' ''                    Else
        ' ''                        dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND COMUNI_NAZIONI.ID not IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " COMUNI_NAZIONI.ID not IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If


        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
        ' ''                    Else
        ' ''                        sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------

        ' ''                '------
        ' ''                dimen = ""
        ' ''                m1 = False
        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
        ' ''                    Else
        ' ''                        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------
        ' ''                'ESCLUDI PIANO CON ASCENSORE
        ' ''                dimen = ""
        ' ''                m1 = False
        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0") <> "0" Then
        ' ''                    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0"))) & "' "
        ' ''                    m1 = True
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
        ' ''                    If m1 = True Then
        ' ''                        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
        ' ''                    Else
        ' ''                        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
        ' ''                        m1 = True
        ' ''                    End If
        ' ''                End If

        ' ''                If m1 = True Then
        ' ''                    If M = True Then
        ' ''                        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
        ' ''                    Else
        ' ''                        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
        ' ''                        M = True
        ' ''                    End If
        ' ''                End If

        ' ''                '-----------




        ' ''            End If
        ' ''        End If
        ' ''        myReader.Close()

        ' ''        '------


        ' ''        If M = True Then
        ' ''            Preferenze = "SI"
        ' ''        Else
        ' ''            Preferenze = "NO"
        ' ''        End If

        ' ''        If sTipoAlloggio = "1" Then
        ' ''            If M = True Then
        ' ''                sZona = sZona & " AND (ALLOGGI.fl_mod='1') "
        ' ''            Else
        ' ''                sZona = " (ALLOGGI.fl_mod='1') "
        ' ''                M = True
        ' ''            End If
        ' ''        Else
        ' ''            If M = True Then
        ' ''                sZona = sZona & " AND (ALLOGGI.fl_mod='0') "
        ' ''            Else
        ' ''                sZona = " (ALLOGGI.fl_mod='0') "
        ' ''                M = True
        ' ''            End If
        ' ''        End If


        ' ''        sStringaSQL2 = "SELECT " _
        ' ''                       & " " _
        ' ''                       & "ALLOGGI.*,(select valore from siscom_mi.dimensioni where cod_tipologia='SUP_NETTA' AND id_unita_immobiliare=unita_immobiliari.id) as supAlloggio FROM T_TIPO_PROPRIETA,ALLOGGI," _
        ' ''                       & " T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,ZONA_ALER,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TO_NUMBER(ALLOGGI.ZONA)=TO_NUMBER(ZONA_ALER.COD) AND UNITA_IMMOBILIARI.ID=" & IndiceUnita _
        ' ''                       & " AND ALLOGGI.COD_ALLOGGIO =UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
        ' ''                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
        ' ''                       & " edifici.id = unita_immobiliari.id_Edificio " _
        ' ''                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND " & sZona & " "
        ' ''        PAR.cmd.CommandText = sStringaSQL2
        ' ''        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        ' ''        If myReader1.Read = True Then
        ' ''            SupAlloggio = PAR.IfNull(myReader1("supAlloggio"), "0,00")
        ' ''            supOK = False
        ' ''            Select Case NumComponenti
        ' ''                Case 1
        ' ''                    If CDbl(SupAlloggio) > 28.800000000000001 And CDbl(SupAlloggio) <= 38 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case 2
        ' ''                    If CDbl(SupAlloggio) > 33.600000000000001 And CDbl(SupAlloggio) <= 46 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case 3
        ' ''                    If CDbl(SupAlloggio) > 43.350000000000001 And CDbl(SupAlloggio) <= 60.340000000000003 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case 4
        ' ''                    If CDbl(SupAlloggio) > 60.350000000000001 And CDbl(SupAlloggio) <= 71.390000000000001 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case 5
        ' ''                    If CDbl(SupAlloggio) > 71.400000000000006 And CDbl(SupAlloggio) <= 79.040000000000006 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''                Case Is >= 5
        ' ''                    If CDbl(SupAlloggio) > 79.049999999999997 And CDbl(SupAlloggio) <= 200 Then
        ' ''                        supOK = True
        ' ''                    End If
        ' ''            End Select
        ' ''            If supOK = True Then
        ' ''                dt2.Rows(i2).Item("ASSEGNATO_ERP") = "1"

        ' ''                strRadioButton = strRadioButton & "<br /><input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' checked='checked' value='" & ROW2.Item("ID_DOMANDA") & "'/>GRAD.ERP (ART.15) - POSIZIONE:" & ROW2.Item("POSIZIONE") & " - " & ROW2.Item("NOMINATIVO") '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
        ' ''                'AssociaERP = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' checked='checked' value='" & ROW2.Item("NOMINATIVO") & "'/>GRAD.ERP - POSIZIONE:" & ROW2.Item("POSIZIONE") & " - " & ROW2.Item("NOMINATIVO") '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"

        ' ''                If Preferenze = "SI" Then
        ' ''                    visualizzaPreferenze = visualizzaPreferenze & "<br /><a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW2.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
        ' ''                    'Preferenze = "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW2.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
        ' ''                    PreferenzeRispettate = "SI"
        ' ''                Else
        ' ''                    Preferenze = "NO"
        ' ''                    PreferenzeRispettate = "--"
        ' ''                End If

        ' ''                myReader1.Close()
        ' ''                myReader.Close()
        ' ''                Exit For
        ' ''            Else
        ' ''                'ALLOGGIO NON IDONEO PER SUP./N.COMPONENTI
        ' ''            End If
        ' ''        Else
        ' ''            'HA ESPRESSO PREFERENZE MA NON CI SONO ALLOGGI
        ' ''            Beep() 'RIMANE SENZA ALLOGGIO
        ' ''        End If
        ' ''        myReader1.Close()


        ' ''    End If
        ' ''    i2 = i2 + 1
        ' ''Next

        AssociaERP = strRadioButton

        If Preferenze = "SI" Then
            Preferenze = visualizzaPreferenze
            PreferenzeRispettate = "SI"
        Else
            Preferenze = "NO"
            PreferenzeRispettate = "--"
        End If

    End Function

    Function AssociaART14(ByVal IndiceUnita As String, ByVal Gruppo As Integer, ByVal Numero As Integer, ByRef Preferenze As String, ByRef PreferenzeRispettate As String, ByRef NumComponenti As String, ByRef SupAlloggio As String) As String
        Dim i0 As Long = 0
        Dim i1 As Long = 0
        Dim i2 As Long = 0

        Dim M As Boolean
        Dim sZona As String = ""
        Dim sTipoAlloggio As String = "0"
        Dim sStringaSQL2 As String = ""
        Dim supOK As Boolean = False
        Dim dimen As String = ""
        Dim m1 As Boolean = False
        Dim strRadioButton As String = ""
        Dim visualizzaPreferenze As String = ""

        Preferenze = ""

        'ERP ART.14
        For Each Me.ROW1 In dt1.Rows
            Numero = 1
            sZona = ""
            Preferenze = "--"
            PreferenzeRispettate = "--"
            'VERIFICO SE ALLOGGIO ASSEGNATO O MENO
            If ROW1.Item("ASSEGNATO_ERP") = "0" Then
                'TIPO ALLOGGIO, CANONE SOCIALE O MODERATO
                sTipoAlloggio = ROW1.Item("TIPO_ALLOGGIO")
                supOK = False
                M = False
                sZona = ""
                dimen = ""
                m1 = False
                NumComponenti = ROW1.Item("COMP_NUCLEO")



                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & ROW1.Item("ID_DOMANDA")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE

                    If myReader.Read Then

                        If PAR.IfNull(myReader("PREF_ZONA1"), "0") <> "0" Then
                            ' sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA1"), " ") & "'"
                            sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA1"), "0") & "'"
                            M = True

                        End If
                        If PAR.IfNull(myReader("PREF_ZONA2"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA3"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA4"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA4"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA4"), "0") & "'"
                                M = True
                            End If
                        End If


                        If PAR.IfNull(myReader("PREF_ZONA5"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA5"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA5"), "0") & "'"
                                M = True
                            End If
                        End If

                        If M = True Then sZona = sZona & ") "


                        If PAR.IfNull(myReader("PREF_BARRIERE"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            Else
                                sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                                M = True
                            End If
                        End If



                        If PAR.IfNull(myReader("PREF_SUP_MIN"), "0") <> "0" Then
                            dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_SUP_MAX"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " AND VALORE<=" & PAR.IfNull(myReader("PREF_SUP_MAX"), "0") & ")"
                            Else
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ") "
                            End If
                        Else
                            If m1 = True Then
                                dimen = dimen & ")"
                            End If
                        End If


                        If dimen <> "" Then
                            If M = True Then
                                sZona = sZona & " AND " & dimen
                                M = True
                            Else
                                M = True
                                sZona = sZona & " " & dimen
                            End If
                        End If


                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        If PAR.IfNull(myReader("PREF_CONDOMINIO"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                            Else
                                sZona = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '08/10/2013 PIANO TRASFORMATO IN INTERVALLO

                        '----- PREF_PIANI_DA_CON
                        '----- PREF_PIANI_A_CON

                        '----- PREF_PIANI_DA_SENZA
                        '----- PREF_PIANI_DA_CON

                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") <> "0" Then

                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & "  (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                            'sZona = " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            'm1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        '08/10/2013 FINE PIANO TRASFORMATO IN INTERVALLO

                    End If
                End If
                myReader.Close()

                'PREFERENZE ESCLUSIONI
                m1 = False
                dimen = ""

                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCLUSIONI WHERE ID_DOMANDA=" & ROW1.Item("ID_DOMANDA")
                myReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE ESCLUSIONI

                    If myReader.Read Then

                        If PAR.IfNull(myReader("ESCL_ZONA1"), "0") <> "0" Then
                            If sZona <> "" Then
                                sZona = sZona & " AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            Else
                                sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA2"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA3"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA4"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA5"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            sZona = sZona & ") "
                            M = True
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If



                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        '------
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                        ''-----------
                        ''ESCLUDI PIANO CON ASCENSORE
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                        '-----------




                    End If
                End If
                myReader.Close()

                '------


                If M = True Then
                    Preferenze = "SI"
                Else
                    Preferenze = "NO"
                End If

                If sTipoAlloggio = "1" Then
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='1') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='1') "
                        M = True
                    End If
                Else
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='0') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='0') "
                        M = True
                    End If
                End If


                sStringaSQL2 = "SELECT " _
                               & " " _
                               & "ALLOGGI.*,(select valore from siscom_mi.dimensioni where cod_tipologia='SUP_NETTA' AND id_unita_immobiliare=unita_immobiliari.id) as supAlloggio FROM T_TIPO_PROPRIETA,ALLOGGI," _
                               & " T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,ZONA_ALER,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TO_NUMBER(ALLOGGI.ZONA)=TO_NUMBER(ZONA_ALER.ZONA) AND UNITA_IMMOBILIARI.ID=" & IndiceUnita _
                               & " AND ALLOGGI.COD_ALLOGGIO =UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
                               & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                               & " edifici.id = unita_immobiliari.id_Edificio " _
                               & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND " & sZona & " "
                PAR.cmd.CommandText = sStringaSQL2
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read = True Then
                    SupAlloggio = PAR.IfNull(myReader1("supAlloggio"), "0,00")
                    supOK = False
                    Select Case NumComponenti
                        Case 1
                            If CDbl(SupAlloggio) > 28.8 And CDbl(SupAlloggio) <= 38 Then
                                supOK = True
                            End If
                        Case 2
                            If CDbl(SupAlloggio) > 33.6 And CDbl(SupAlloggio) <= 46 Then
                                supOK = True
                            End If
                        Case 3
                            If CDbl(SupAlloggio) > 43.35 And CDbl(SupAlloggio) <= 60.34 Then
                                supOK = True
                            End If
                        Case 4
                            If CDbl(SupAlloggio) > 60.35 And CDbl(SupAlloggio) <= 71.39 Then
                                supOK = True
                            End If
                        Case 5
                            If CDbl(SupAlloggio) > 71.4 And CDbl(SupAlloggio) <= 79.04 Then
                                supOK = True
                            End If
                        Case Is >= 5
                            If CDbl(SupAlloggio) > 79.05 And CDbl(SupAlloggio) <= 200 Then
                                supOK = True
                            End If
                    End Select
                    If supOK = True Then
                        dt1.Rows(i1).Item("ASSEGNATO_ERP") = "1"

                        strRadioButton = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' style='height:13px;width:13px;vertical-align:middle;' value='14ART" & ROW1.Item("ID_DOMANDA") & "'/>GRAD.ERP (ART.14) - POSIZIONE:" & ROW1.Item("POSIZIONE") & " - <i>" & ROW1.Item("NOMINATIVO") & "</i>" '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        idAlloggio.Value = myReader1("ID")
                        If Preferenze = "SI" Then
                            'Preferenze = "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW1.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
                            visualizzaPreferenze = visualizzaPreferenze & "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW0.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=880');void(0);" & Chr(34) & ">SI</a>"
                            PreferenzeRispettate = "SI"
                        Else
                            Preferenze = "NO"
                            PreferenzeRispettate = "--"
                        End If

                        myReader1.Close()
                        myReader.Close()
                        Exit For
                    Else
                        'ALLOGGIO NON IDONEO PER SUP./N.COMPONENTI
                    End If
                Else
                    'HA ESPRESSO PREFERENZE MA NON CI SONO ALLOGGI
                    'RIMANE SENZA ALLOGGIO
                End If
                myReader1.Close()


            End If
            i1 = i1 + 1
        Next

        'ERP ART.15
        AssociaART14 = strRadioButton

        If Preferenze = "SI" Then
            Preferenze = visualizzaPreferenze
            PreferenzeRispettate = "SI"
        Else
            Preferenze = "NO"
            PreferenzeRispettate = "--"
        End If

    End Function

    Function AssociaART15(ByVal IndiceUnita As String, ByVal Gruppo As Integer, ByVal Numero As Integer, ByRef Preferenze As String, ByRef PreferenzeRispettate As String, ByRef NumComponenti As String, ByRef SupAlloggio As String) As String
        Dim i0 As Long = 0
        Dim i1 As Long = 0
        Dim i2 As Long = 0

        Dim M As Boolean
        Dim sZona As String = ""
        Dim sTipoAlloggio As String = "0"
        Dim sStringaSQL2 As String = ""
        Dim supOK As Boolean = False
        Dim dimen As String = ""
        Dim m1 As Boolean = False
        Dim strRadioButton As String = ""
        Dim visualizzaPreferenze As String = ""

        Preferenze = ""

        'ERP ART.15
        For Each Me.ROW2 In dt2.Rows
            Numero = 2
            sZona = ""
            Preferenze = "--"
            PreferenzeRispettate = "--"
            'VERIFICO SE ALLOGGIO ASSEGNATO O MENO
            If ROW2.Item("ASSEGNATO_ERP") = "0" Then
                'TIPO ALLOGGIO, CANONE SOCIALE O MODERATO
                sTipoAlloggio = PAR.IfNull(ROW2.Item("TIPO_ALLOGGIO"), 0)
                supOK = False
                M = False
                sZona = ""
                dimen = ""
                m1 = False
                NumComponenti = ROW2.Item("COMP_NUCLEO")



                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE WHERE ID_DOMANDA=" & ROW2.Item("ID_DOMANDA")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE

                    If myReader.Read Then

                        If PAR.IfNull(myReader("PREF_ZONA1"), "0") <> "0" Then
                            ' sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA1"), " ") & "'"
                            sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA1"), "0") & "'"
                            M = True

                        End If
                        If PAR.IfNull(myReader("PREF_ZONA2"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA3"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA4"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA4"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA4"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA5"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA5"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA5"), "0") & "'"
                                M = True
                            End If
                        End If

                        If M = True Then sZona = sZona & ") "


                        If PAR.IfNull(myReader("PREF_BARRIERE"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            Else
                                sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                                M = True
                            End If
                        End If



                        If PAR.IfNull(myReader("PREF_SUP_MIN"), "0") <> "0" Then
                            dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_SUP_MAX"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ")"
                            Else
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ") "
                            End If
                        Else
                            If m1 = True Then
                                dimen = dimen & ")"
                            End If
                        End If


                        If dimen <> "" Then
                            If M = True Then
                                sZona = sZona & " AND " & dimen
                                M = True
                            Else
                                M = True
                                sZona = sZona & " " & dimen
                            End If
                        End If


                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART5"), "0")
                                m1 = True
                            End If
                        End If


                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        If PAR.IfNull(myReader("PREF_CONDOMINIO"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                            Else
                                sZona = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '08/10/2013 PIANO TRASFORMATO IN INTERVALLO

                        '----- PREF_PIANI_DA_CON
                        '----- PREF_PIANI_A_CON

                        '----- PREF_PIANI_DA_SENZA
                        '----- PREF_PIANI_DA_CON

                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") <> "0" Then

                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & "  (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                            'sZona = " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            'm1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        '08/10/2013 FINE PIANO TRASFORMATO IN INTERVALLO

                    End If
                End If
                myReader.Close()

                'PREFERENZE ESCLUSIONI
                m1 = False
                dimen = ""

                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCLUSIONI WHERE ID_DOMANDA=" & ROW2.Item("ID_DOMANDA")
                myReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE ESCLUSIONI

                    If myReader.Read Then

                        If PAR.IfNull(myReader("ESCL_ZONA1"), "0") <> "0" Then
                            If sZona <> "" Then
                                sZona = sZona & " AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            Else
                                sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA2"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA3"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA4"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA5"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            sZona = sZona & ") "
                            M = True
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If



                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        '------
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                        ''-----------
                        ''ESCLUDI PIANO CON ASCENSORE
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                        '-----------




                    End If
                End If
                myReader.Close()

                '------


                If M = True Then
                    Preferenze = "SI"
                Else
                    Preferenze = "NO"
                End If

                If sTipoAlloggio = "1" Then
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='1') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='1') "
                        M = True
                    End If
                Else
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='0') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='0') "
                        M = True
                    End If
                End If


                sStringaSQL2 = "SELECT " _
                               & " " _
                               & "ALLOGGI.*,(select valore from siscom_mi.dimensioni where cod_tipologia='SUP_NETTA' AND id_unita_immobiliare=unita_immobiliari.id) as supAlloggio FROM T_TIPO_PROPRIETA,ALLOGGI," _
                               & " T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,ZONA_ALER,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TO_NUMBER(ALLOGGI.ZONA)=TO_NUMBER(ZONA_ALER.ZONA) AND UNITA_IMMOBILIARI.ID=" & IndiceUnita _
                               & " AND ALLOGGI.COD_ALLOGGIO =UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
                               & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                               & " edifici.id = unita_immobiliari.id_Edificio " _
                               & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND " & sZona & " "
                PAR.cmd.CommandText = sStringaSQL2
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read = True Then
                    SupAlloggio = PAR.IfNull(myReader1("supAlloggio"), "0,00")
                    supOK = False
                    Select Case NumComponenti
                        Case 1
                            If CDbl(SupAlloggio) > 28.800000000000001 And CDbl(SupAlloggio) <= 38 Then
                                supOK = True
                            End If
                        Case 2
                            If CDbl(SupAlloggio) > 33.600000000000001 And CDbl(SupAlloggio) <= 46 Then
                                supOK = True
                            End If
                        Case 3
                            If CDbl(SupAlloggio) > 43.350000000000001 And CDbl(SupAlloggio) <= 60.340000000000003 Then
                                supOK = True
                            End If
                        Case 4
                            If CDbl(SupAlloggio) > 60.350000000000001 And CDbl(SupAlloggio) <= 71.390000000000001 Then
                                supOK = True
                            End If
                        Case 5
                            If CDbl(SupAlloggio) > 71.400000000000006 And CDbl(SupAlloggio) <= 79.040000000000006 Then
                                supOK = True
                            End If
                        Case Is >= 5
                            If CDbl(SupAlloggio) > 79.049999999999997 And CDbl(SupAlloggio) <= 200 Then
                                supOK = True
                            End If
                    End Select
                    If supOK = True Then
                        dt2.Rows(i2).Item("ASSEGNATO_ERP") = "1"

                        strRadioButton = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' style='height:14px;width:14px;vertical-align:middle;' value='15ART" & ROW2.Item("ID_DOMANDA") & "'/>GRAD.ERP (ART.15) - <i>" & ROW2.Item("NOMINATIVO") & "</i>" '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        'AssociaERP = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' checked='checked' value='" & ROW2.Item("NOMINATIVO") & "'/>GRAD.ERP - POSIZIONE:" & ROW2.Item("POSIZIONE") & " - " & ROW2.Item("NOMINATIVO") '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        idAlloggio.Value = myReader1("ID")
                        If Preferenze = "SI" Then
                            visualizzaPreferenze = visualizzaPreferenze & "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW2.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=880');void(0);" & Chr(34) & ">SI</a>"
                            'Preferenze = "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW2.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
                            PreferenzeRispettate = "SI"
                        Else
                            Preferenze = "NO"
                            PreferenzeRispettate = "--"
                        End If

                        myReader1.Close()
                        myReader.Close()
                        Exit For
                    Else
                        'ALLOGGIO NON IDONEO PER SUP./N.COMPONENTI
                    End If
                Else
                    'HA ESPRESSO PREFERENZE MA NON CI SONO ALLOGGI
                    'RIMANE SENZA ALLOGGIO
                End If
                myReader1.Close()


            End If
            i2 = i2 + 1
        Next

        AssociaART15 = strRadioButton

        If Preferenze = "SI" Then
            Preferenze = visualizzaPreferenze
            PreferenzeRispettate = "SI"
        Else
            Preferenze = "NO"
            PreferenzeRispettate = "--"
        End If



    End Function

    Function AssociaCAMBI(ByVal IndiceUnita As String, ByVal Gruppo As Integer, ByVal Numero As Integer, ByRef Preferenze As String, ByRef PreferenzeRispettate As String, ByRef NumComponenti As String, ByRef SupAlloggio As String) As String
        Dim i0 As Long = 0
        Dim i1 As Long = 0
        Dim i2 As Long = 0

        Dim M As Boolean
        Dim sZona As String = ""
        Dim sTipoAlloggio As String = "0"
        Dim sStringaSQL2 As String = ""
        Dim supOK As Boolean = False
        Dim dimen As String = ""
        Dim m1 As Boolean = False
        Dim strRadioButton As String = ""
        Dim visualizzaPreferenze As String = ""

        Preferenze = ""

        'ERP ART.15
        For Each Me.ROW3 In dt3.Rows
            Numero = 3
            sZona = ""
            Preferenze = "--"
            PreferenzeRispettate = "--"
            'VERIFICO SE ALLOGGIO ASSEGNATO O MENO
            If ROW3.Item("ASSEGNATO_ERP") = "0" Then
                'TIPO ALLOGGIO, CANONE SOCIALE O MODERATO
                sTipoAlloggio = PAR.IfNull(ROW3.Item("TIPO_ALLOGGIO"), 0)
                supOK = False
                M = False
                sZona = ""
                dimen = ""
                m1 = False
                NumComponenti = ROW3.Item("COMP_NUCLEO")



                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_CAMBI WHERE ID_DOMANDA=" & ROW3.Item("ID_DOMANDA")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE

                    If myReader.Read Then

                        If PAR.IfNull(myReader("PREF_ZONA1"), "0") <> "0" Then
                            ' sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA1"), " ") & "'"
                            sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA1"), "0") & "'"
                            M = True

                        End If
                        If PAR.IfNull(myReader("PREF_ZONA2"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA3"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA4"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA4"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA4"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA5"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA5"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA5"), "0") & "'"
                                M = True
                            End If
                        End If

                        If M = True Then sZona = sZona & ") "


                        If PAR.IfNull(myReader("PREF_BARRIERE"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            Else
                                sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                                M = True
                            End If
                        End If



                        If PAR.IfNull(myReader("PREF_SUP_MIN"), "0") <> "0" Then
                            dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_SUP_MAX"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ")"
                            Else
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ") "
                            End If
                        Else
                            If m1 = True Then
                                dimen = dimen & ")"
                            End If
                        End If


                        If dimen <> "" Then
                            If M = True Then
                                sZona = sZona & " AND " & dimen
                                M = True
                            Else
                                M = True
                                sZona = sZona & " " & dimen
                            End If
                        End If


                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        If PAR.IfNull(myReader("PREF_CONDOMINIO"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                            Else
                                sZona = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '08/10/2013 PIANO TRASFORMATO IN INTERVALLO

                        '----- PREF_PIANI_DA_CON
                        '----- PREF_PIANI_A_CON

                        '----- PREF_PIANI_DA_SENZA
                        '----- PREF_PIANI_DA_CON

                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") <> "0" Then

                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & "  (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                            'sZona = " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            'm1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        '08/10/2013 FINE PIANO TRASFORMATO IN INTERVALLO

                    End If
                End If
                myReader.Close()

                'PREFERENZE ESCLUSIONI
                m1 = False
                dimen = ""

                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCL_CAMBI WHERE ID_DOMANDA=" & ROW3.Item("ID_DOMANDA")
                myReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE ESCLUSIONI

                    If myReader.Read Then

                        If PAR.IfNull(myReader("ESCL_ZONA1"), "0") <> "0" Then
                            If sZona <> "" Then
                                sZona = sZona & " AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            Else
                                sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA2"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA3"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA4"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA4"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA5"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA5"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            sZona = sZona & ") "
                            M = True
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                            Else

                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If



                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        '------
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                        ''-----------
                        ''ESCLUDI PIANO CON ASCENSORE
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                        '-----------




                    End If
                End If
                myReader.Close()

                '------


                If M = True Then
                    Preferenze = "SI"
                Else
                    Preferenze = "NO"
                End If

                If sTipoAlloggio = "1" Then
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='1') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='1') "
                        M = True
                    End If
                Else
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='0') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='0') "
                        M = True
                    End If
                End If


                sStringaSQL2 = "SELECT " _
                               & " " _
                               & "ALLOGGI.*,(select valore from siscom_mi.dimensioni where cod_tipologia='SUP_NETTA' AND id_unita_immobiliare=unita_immobiliari.id) as supAlloggio FROM T_TIPO_PROPRIETA,ALLOGGI," _
                               & " T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,ZONA_ALER,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TO_NUMBER(ALLOGGI.ZONA)=TO_NUMBER(ZONA_ALER.ZONA) AND UNITA_IMMOBILIARI.ID=" & IndiceUnita _
                               & " AND ALLOGGI.COD_ALLOGGIO =UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
                               & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                               & " edifici.id = unita_immobiliari.id_Edificio " _
                               & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND " & sZona & " "
                PAR.cmd.CommandText = sStringaSQL2
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read = True Then
                    SupAlloggio = PAR.IfNull(myReader1("supAlloggio"), "0,00")
                    supOK = False
                    Select Case NumComponenti
                        Case 1
                            If CDbl(SupAlloggio) > 28.800000000000001 And CDbl(SupAlloggio) <= 38 Then
                                supOK = True
                            End If
                        Case 2
                            If CDbl(SupAlloggio) > 33.600000000000001 And CDbl(SupAlloggio) <= 46 Then
                                supOK = True
                            End If
                        Case 3
                            If CDbl(SupAlloggio) > 43.350000000000001 And CDbl(SupAlloggio) <= 60.340000000000003 Then
                                supOK = True
                            End If
                        Case 4
                            If CDbl(SupAlloggio) > 60.350000000000001 And CDbl(SupAlloggio) <= 71.390000000000001 Then
                                supOK = True
                            End If
                        Case 5
                            If CDbl(SupAlloggio) > 71.400000000000006 And CDbl(SupAlloggio) <= 79.040000000000006 Then
                                supOK = True
                            End If
                        Case Is >= 5
                            If CDbl(SupAlloggio) > 79.049999999999997 And CDbl(SupAlloggio) <= 200 Then
                                supOK = True
                            End If
                    End Select
                    If supOK = True Then
                        dt3.Rows(i2).Item("ASSEGNATO_ERP") = "1"

                        strRadioButton = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' style='height:14px;width:14px;vertical-align:middle;' value='CAMBI" & ROW3.Item("ID_DOMANDA") & "'/>GRAD.BANDO CAMBI - POSIZIONE:" & ROW3.Item("POSIZIONE") & " - <i>" & ROW3.Item("NOMINATIVO") & "</i>" '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        'AssociaERP = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' checked='checked' value='" & ROW3.Item("NOMINATIVO") & "'/>GRAD.ERP - POSIZIONE:" & ROW3.Item("POSIZIONE") & " - " & ROW3.Item("NOMINATIVO") '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        idAlloggio.Value = myReader1("ID")
                        If Preferenze = "SI" Then
                            visualizzaPreferenze = visualizzaPreferenze & "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=2&" & "ID=" & ROW3.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=880');void(0);" & Chr(34) & ">SI</a>"
                            'Preferenze = "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW3.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
                            PreferenzeRispettate = "SI"
                        Else
                            Preferenze = "NO"
                            PreferenzeRispettate = "--"
                        End If

                        myReader1.Close()
                        myReader.Close()
                        Exit For
                    Else
                        'ALLOGGIO NON IDONEO PER SUP./N.COMPONENTI
                    End If
                Else
                    'HA ESPRESSO PREFERENZE MA NON CI SONO ALLOGGI
                    'RIMANE SENZA ALLOGGIO
                End If
                myReader1.Close()


            End If
            i2 = i2 + 1
        Next

        AssociaCAMBI = strRadioButton

        If Preferenze = "SI" Then
            Preferenze = visualizzaPreferenze
            PreferenzeRispettate = "SI"
        Else
            Preferenze = "NO"
            PreferenzeRispettate = "--"
        End If



    End Function


    Function AssociaEMERG(ByVal IndiceUnita As String, ByVal Gruppo As Integer, ByVal Numero As Integer, ByRef Preferenze As String, ByRef PreferenzeRispettate As String, ByRef NumComponenti As String, ByRef SupAlloggio As String) As String
        Dim i0 As Long = 0
        Dim i1 As Long = 0
        Dim i2 As Long = 0

        Dim M As Boolean
        Dim sZona As String = ""
        Dim sTipoAlloggio As String = "0"
        Dim sStringaSQL2 As String = ""
        Dim supOK As Boolean = False
        Dim dimen As String = ""
        Dim m1 As Boolean = False
        Dim strRadioButton As String = ""
        Dim visualizzaPreferenze As String = ""

        Preferenze = ""

        'ERP ART.15
        For Each Me.ROW4 In dt4.Rows
            Numero = 4
            sZona = ""
            Preferenze = "--"
            PreferenzeRispettate = "--"
            'VERIFICO SE ALLOGGIO ASSEGNATO O MENO
            If ROW4.Item("ASSEGNATO_ERP") = "0" Then
                'TIPO ALLOGGIO, CANONE SOCIALE O MODERATO
                sTipoAlloggio = PAR.IfNull(ROW4.Item("TIPO_ALLOGGIO"), 0)
                supOK = False
                M = False
                sZona = ""
                dimen = ""
                m1 = False
                NumComponenti = ROW4.Item("COMP_NUCLEO")



                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_VSA WHERE ID_DOMANDA=" & ROW4.Item("ID_DOMANDA")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE

                    If myReader.Read Then

                        If PAR.IfNull(myReader("PREF_ZONA1"), "0") <> "0" Then
                            ' sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA1"), " ") & "'"
                            sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA1"), "0") & "'"
                            M = True

                        End If
                        If PAR.IfNull(myReader("PREF_ZONA2"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA2"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA2"), "0") & "'"
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_ZONA3"), "0") <> "0" Then
                            If M = False Then
                                'sZona = sZona & "(ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & "(ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            Else
                                'sZona = sZona & " OR ALLOGGI.ZONA='" & PAR.IfNull(myReader("PREF_ZONA3"), " ") & "'"
                                sZona = sZona & " OR ZONA_ALER.COD='" & PAR.IfNull(myReader("PREF_ZONA3"), "0") & "'"
                                M = True
                            End If
                        End If

                        If M = True Then sZona = sZona & ") "


                        If PAR.IfNull(myReader("PREF_BARRIERE"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            Else
                                sZona = " (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                                M = True
                            End If
                        End If



                        If PAR.IfNull(myReader("PREF_SUP_MIN"), "0") <> "0" Then
                            dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_SUP_MAX"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE>=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MIN"), "0")) & " AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ")"
                            Else
                                dimen = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND VALORE<=" & PAR.VirgoleInPunti(PAR.IfNull(myReader("PREF_SUP_MAX"), "0")) & ") "
                            End If
                        Else
                            If m1 = True Then
                                dimen = dimen & ")"
                            End If
                        End If


                        If dimen <> "" Then
                            If M = True Then
                                sZona = sZona & " AND " & dimen
                                M = True
                            Else
                                M = True
                                sZona = sZona & " " & dimen
                            End If
                        End If


                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If


                        If PAR.IfNull(myReader("PREF_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If


                        If PAR.IfNull(myReader("PREF_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If


                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("PREF_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        If PAR.IfNull(myReader("PREF_CONDOMINIO"), "0") = "1" Then
                            If M = True Then
                                sZona = sZona & " AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                            Else
                                sZona = " UNITA_IMMOBILIARI.ID IN (SELECT ID_UI FROM SISCOM_MI.COND_UI) "
                                M = True
                            End If
                        End If

                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("PREF_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        '08/10/2013 PIANO TRASFORMATO IN INTERVALLO

                        '----- PREF_PIANI_DA_CON
                        '----- PREF_PIANI_A_CON

                        '----- PREF_PIANI_DA_SENZA
                        '----- PREF_PIANI_DA_CON

                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") <> "0" Then

                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & "  (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                            'sZona = " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            'm1 = True
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_CON"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD >= " & PAR.IfNull(myReader("PREF_PIANI_DA_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        If PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") <> "0" Then
                            If M = True Then
                                sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                            Else
                                sZona = sZona & " (TIPO_LIVELLO_PIANO.COD < " & PAR.IfNull(myReader("PREF_PIANI_A_SENZA"), "0") & " AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                                M = True
                            End If
                        End If

                        '08/10/2013 FINE PIANO TRASFORMATO IN INTERVALLO

                    End If
                End If
                myReader.Close()

                'PREFERENZE ESCLUSIONI
                m1 = False
                dimen = ""

                PAR.cmd.CommandText = "SELECT * FROM DOMANDE_PREFERENZE_ESCL_VSA WHERE ID_DOMANDA=" & ROW4.Item("ID_DOMANDA")
                myReader = PAR.cmd.ExecuteReader()

                If myReader.HasRows = True Then
                    'HA ESPRESSO PREFERENZE ESCLUSIONI

                    If myReader.Read Then

                        If PAR.IfNull(myReader("ESCL_ZONA1"), "0") <> "0" Then
                            If sZona <> "" Then
                                sZona = sZona & " AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            Else
                                sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA1"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA2"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA2"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_ZONA3"), "0") <> "0" Then
                            If m1 = False Then
                                If sZona <> "" Then
                                    sZona = sZona & "AND (ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                Else
                                    sZona = sZona & "(ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                    m1 = True
                                End If
                            Else
                                sZona = sZona & " OR ZONA_ALER.COD<>'" & PAR.IfNull(myReader("ESCL_ZONA3"), "0") & "'"
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            sZona = sZona & ") "
                            M = True
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_QUART1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_QUART1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_QUART5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_QUART5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMPLESSI_IMMOBILIARI.ID_QUARTIERE NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_COMPLESSO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND COMPLESSI_IMMOBILIARI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        dimen = ""
                        m1 = False
                        If PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_EDIFICIO5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " AND EDIFICI.ID NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If



                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_LOCALITA1"), "0") <> "0" Then
                            dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA1"), "0")
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA2"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA3"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA4"), "0")
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_LOCALITA5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & "," & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                            Else
                                dimen = "  " & PAR.IfNull(myReader("ESCL_LOCALITA5"), "0")
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                            Else
                                sZona = sZona & " COMUNI_NAZIONI.ID not IN (" & dimen & ") "
                                M = True
                            End If
                        End If


                        '------
                        dimen = ""
                        m1 = False

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0") <> "0" Then
                            dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO1"), "0"))) & "' "
                            m1 = True
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO2"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO3"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO4"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0") <> "0" Then
                            If m1 = True Then
                                dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                            Else
                                dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_INDIRIZZO5"), "0"))) & "' "
                                m1 = True
                            End If
                        End If

                        If m1 = True Then
                            If M = True Then
                                sZona = sZona & " AND UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                            Else
                                sZona = sZona & " UPPER(INDIRIZZI.DESCRIZIONE) NOT IN (" & dimen & ") "
                                M = True
                            End If
                        End If

                        '-----------

                        '------
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA NOT IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                        ''-----------
                        ''ESCLUDI PIANO CON ASCENSORE
                        'dimen = ""
                        'm1 = False
                        'If PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0") <> "0" Then
                        '    dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_CA1"), "0"))) & "' "
                        '    m1 = True
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA2"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0") <> "0" Then
                        '    If m1 = True Then
                        '        dimen = dimen & ",'" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '    Else
                        '        dimen = "  '" & UCase(Trim(PAR.IfNull(myReader("ESCL_PIANO_SA3"), "0"))) & "' "
                        '        m1 = True
                        '    End If
                        'End If

                        'If m1 = True Then
                        '    If M = True Then
                        '        sZona = sZona & " AND (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '    Else
                        '        sZona = sZona & " (TIPO_LIVELLO_PIANO.COD NOT IN (" & dimen & ") AND UNITA_IMMOBILIARI.ID_SCALA IN (SELECT IMPIANTI_SCALE.ID_SCALA FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE WHERE IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)) "
                        '        M = True
                        '    End If
                        'End If

                        '-----------




                    End If
                End If
                myReader.Close()

                '------


                If M = True Then
                    Preferenze = "SI"
                Else
                    Preferenze = "NO"
                End If

                If sTipoAlloggio = "1" Then
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='1') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='1') "
                        M = True
                    End If
                Else
                    If M = True Then
                        sZona = sZona & " AND (ALLOGGI.fl_mod='0') "
                    Else
                        sZona = " (ALLOGGI.fl_mod='0') "
                        M = True
                    End If
                End If


                sStringaSQL2 = "SELECT " _
                               & " " _
                               & "ALLOGGI.*,(select valore from siscom_mi.dimensioni where cod_tipologia='SUP_NETTA' AND id_unita_immobiliare=unita_immobiliari.id) as supAlloggio FROM T_TIPO_PROPRIETA,ALLOGGI," _
                               & " T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,ZONA_ALER,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TO_NUMBER(ALLOGGI.ZONA)=TO_NUMBER(ZONA_ALER.ZONA) AND UNITA_IMMOBILIARI.ID=" & IndiceUnita _
                               & " AND ALLOGGI.COD_ALLOGGIO =UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND " _
                               & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                               & " edifici.id = unita_immobiliari.id_Edificio " _
                               & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND " & sZona & " "
                PAR.cmd.CommandText = sStringaSQL2
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read = True Then
                    SupAlloggio = PAR.IfNull(myReader1("supAlloggio"), "0,00")
                    supOK = False
                    Select Case NumComponenti
                        Case 1
                            If CDbl(SupAlloggio) > 28.800000000000001 And CDbl(SupAlloggio) <= 50.600000000000001 Then
                                supOK = True
                            End If
                        Case 2
                            If CDbl(SupAlloggio) > 33.600000000000001 And CDbl(SupAlloggio) <= 60.5 Then
                                supOK = True
                            End If
                        Case 3
                            If CDbl(SupAlloggio) > 43.350000000000001 And CDbl(SupAlloggio) <= 71.5 Then
                                supOK = True
                            End If
                        Case 4
                            If CDbl(SupAlloggio) > 60.350000000000001 And CDbl(SupAlloggio) <= 88 Then
                                supOK = True
                            End If
                        Case 5
                            If CDbl(SupAlloggio) > 71.400000000000006 And CDbl(SupAlloggio) <= 102.3 Then
                                supOK = True
                            End If
                        Case Is >= 5
                            If CDbl(SupAlloggio) > 79.049999999999997 And CDbl(SupAlloggio) <= 200 Then
                                supOK = True
                            End If
                    End Select
                    If supOK = True Then
                        dt4.Rows(i2).Item("ASSEGNATO_ERP") = "1"

                        strRadioButton = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' style='height:14px;width:14px;vertical-align:middle;' value='22ART" & ROW4.Item("ID_DOMANDA") & "'/>GRAD.CAMBI IN EMERGENZA - <i>" & ROW4.Item("NOMINATIVO") & "</i>" '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        'AssociaERP = "<input id=Radio" & Numero & " name='" & Gruppo & "' type='radio' checked='checked' value='" & ROW4.Item("NOMINATIVO") & "'/>GRAD.ERP - POSIZIONE:" & ROW4.Item("POSIZIONE") & " - " & ROW4.Item("NOMINATIVO") '& "<br /><input id=Radio" & Numero * 5 & " name='" & Gruppo & "' type='radio' value='prova'/>GRAD.ERP - POSIZIONE:PROVA - " & "PROVA NOME"
                        idAlloggio.Value = myReader1("ID")
                        If Preferenze = "SI" Then
                            visualizzaPreferenze = visualizzaPreferenze & "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=2&" & "ID=" & ROW4.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=880');void(0);" & Chr(34) & ">SI</a>"
                            'Preferenze = "<a href=" & Chr(34) & "javascript:window.open('Preferenze.aspx?T=1&" & "ID=" & ROW4.Item("ID_DOMANDA") & "&PG=" & "" & "&PROV=1','Preferenze','top=0,left=0,width=797,height=740');void(0);" & Chr(34) & ">SI</a>"
                            PreferenzeRispettate = "SI"
                        Else
                            Preferenze = "NO"
                            PreferenzeRispettate = "--"
                        End If

                        myReader1.Close()
                        myReader.Close()
                        Exit For
                    Else
                        'ALLOGGIO NON IDONEO PER SUP./N.COMPONENTI
                    End If
                Else
                    'HA ESPRESSO PREFERENZE MA NON CI SONO ALLOGGI
                    'RIMANE SENZA ALLOGGIO
                End If
                myReader1.Close()


            End If
            i2 = i2 + 1
        Next

        AssociaEMERG = strRadioButton

        If Preferenze = "SI" Then
            Preferenze = visualizzaPreferenze
            PreferenzeRispettate = "SI"
        Else
            Preferenze = "NO"
            PreferenzeRispettate = "--"
        End If



    End Function


    Protected Sub ImgProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Dim tipoAbbinamento As String = ""
        Dim idDomanda As Long = 0
        Dim idUnita As Long = 0
        Dim dtElenco As New System.Data.DataTable
        Dim rowElenco As System.Data.DataRow
        Dim dtElenco2 As New System.Data.DataTable
        Dim rowElenco2 As System.Data.DataRow
        Dim dtInquilini As New System.Data.DataTable
        Dim selezionato As Boolean = False
        Dim i As Integer
        'Dim j As Integer = 1
        Dim saltato As Boolean = False

        dtInquilini = Session.Item("ElencoInquilini")
        Try
            If confermaAbb.Value = "1" Then

                For h As Integer = 1 To associato
                    If Request.Item("idUnita" & h) <> "" Then
                        If Request.Item(h) = "" Then
                            Response.Write("<script>alert('Selezionare per ogni alloggio l\'inquilino che si desidera abbinare!')</script>")
                            Exit Try
                        End If
                    End If
                Next

                If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                    PAR.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                PAR.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans


                For i = 1 To associato
                    If Request.Item("idUnita" & i) <> "" Then

                        selezionato = True
                        idUnita = Request.Item("idUnita" & i)
                        idDomanda = Mid(Request.Item(i), 6, Request.Item(i).Length - 1)

                        Dim k As Integer = 0
                        Do While k < dtInquilini.Rows.Count
                            Dim rowScart As Data.DataRow = dtInquilini.Rows(k)
                            If rowScart.Item("ID_ALLOGGIO") = idUnita Then
                                If rowScart.Item("ID_DOMANDA") = idDomanda Then
                                    saltato = True
                                    rowScart.Delete()
                                End If
                            End If
                            k = k + 1
                        Loop
                        dtInquilini.AcceptChanges()


                        tipoAbbinamento = Left(Request.Item(i), 5).ToString
                        Select Case tipoAbbinamento
                            Case "E.R.P"
                                AbbinaERP(idDomanda, idUnita, dtElenco, rowElenco)
                            Case "14ART"
                                AbbinaERP(idDomanda, idUnita, dtElenco, rowElenco)
                            Case "15ART"
                                AbbinaERP(idDomanda, idUnita, dtElenco, rowElenco)
                            Case "CAMBI"
                                AbbinaCAMBI(idDomanda, idUnita, dtElenco, rowElenco)
                            Case "22ART"
                                AbbinaEMERG(idDomanda, idUnita, dtElenco, rowElenco)
                        End Select
                    End If
                Next


                PAR.myTrans.Commit()
                PAR.OracleConn.Close()
                PAR.OracleConn.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                If saltato = True Then
                    ChiamaPaginaElenco(dtInquilini, True)
                End If
                If selezionato = True Then
                    If dtElenco.Rows.Count > 0 Then
                        ChiamaPaginaElenco(dtElenco)
                    Else
                        Response.Write("<script>alert('Nessun abbinamento convalidato!')</script>")
                    End If
                End If
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Sub ExportExcel(ByVal dtElenco As System.Data.DataTable, ByRef nomeFileCompleto As String)
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            sNomeFile = "Abbinamenti_Scartati-" & Format(Now, "yyyyMMddHHmmss")

            nomeFileCompleto = sNomeFile

            i = 0

            With myExcelFile
                .CreateFile(Server.MapPath("..\ALLEGATI\ABBINAMENTI\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetColumnWidth(1, 1, 20)
                .SetColumnWidth(2, 2, 40)
                .SetColumnWidth(3, 3, 30)
                .SetColumnWidth(4, 4, 20)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "NUM. DOMANDA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "NOMINATIVO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COD. ALLOGGIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO BANDO", 0)

                K = 2
                For Each row In dtElenco.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.IfNull(dtElenco.Rows(i).Item("PG_DOMANDA"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.IfNull(dtElenco.Rows(i).Item("NOMINATIVO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.IfNull(dtElenco.Rows(i).Item("COD_ALLOGGIO"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.IfNull(dtElenco.Rows(i).Item("TIPO_BANDO"), "0"))
                    i = i + 1
                    K = K + 1
                Next
                .CloseFile()
            End With

           
            'System.IO.File.Move(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls", Server.MapPath("..\ALLEGATI\ABBINAMENTI\") & sNomeFile & ".xls")


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ExportExcelOK(ByVal dtElenco As System.Data.DataTable, ByRef nomeFileCompleto As String)
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            sNomeFile = "Abbinamenti_Automatici-" & Format(Now, "yyyyMMddHHmmss")

            nomeFileCompleto = sNomeFile
            
            i = 0

            With myExcelFile
                .CreateFile(Server.MapPath("..\ALLEGATI\ABBINAMENTI\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetColumnWidth(1, 1, 20)
                .SetColumnWidth(2, 2, 40)
                .SetColumnWidth(3, 3, 20)
                .SetColumnWidth(4, 4, 20)
                .SetColumnWidth(5, 5, 20)
                .SetColumnWidth(6, 6, 20)
                .SetColumnWidth(7, 7, 20)
                .SetColumnWidth(8, 8, 40)
                .SetColumnWidth(9, 9, 20)
                .SetColumnWidth(10, 10, 20)
                .SetColumnWidth(11, 11, 20)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "NUM. OFFERTA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "NOMINATIVO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "TIPO BANDO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "DATA SCADENZA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COD. ALLOGGIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "SUPERFICIE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DATA DISP.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "INTERNO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "SCALA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "PIANO", 0)


                K = 2
                For Each row In dtElenco.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.IfNull(dtElenco.Rows(i).Item("NUM_OFFERTA"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.IfNull(dtElenco.Rows(i).Item("NOMINATIVO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.IfNull(dtElenco.Rows(i).Item("TIPO_GRAD"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.IfNull(dtElenco.Rows(i).Item("DATA_SCADENZA_OFF"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, PAR.IfNull(dtElenco.Rows(i).Item("COD_UI"), "0"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, PAR.IfNull(dtElenco.Rows(i).Item("SUPERFICIE"), 0))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, PAR.IfNull(dtElenco.Rows(i).Item("DATA_DISP"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, PAR.IfNull(dtElenco.Rows(i).Item("INDIRIZZO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, PAR.IfNull(dtElenco.Rows(i).Item("NUM_ALL"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, PAR.IfNull(dtElenco.Rows(i).Item("SCALA"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, PAR.IfNull(dtElenco.Rows(i).Item("PIANO"), ""))

                    i = i + 1
                    K = K + 1
                Next
                .CloseFile()
            End With

            'Dim strFile As String
            'strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")

            'System.IO.File.Move(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls", Server.MapPath("..\ALLEGATI\ABBINAMENTI\") & sNomeFile & ".xls")


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ChiamaPaginaElenco(ByVal dtElenco As System.Data.DataTable, Optional ByVal scartati As Boolean = False)
        Try
            Dim scriptblock As String = ""

            '***** ESPORTA FILE EXCEL *****
            If scartati = True Then
                ''nomeFileScart.Value = PAR.EsportaExcelDaDT(dtElenco, "Abbinamenti_Scartati-", , False)
                ''If System.IO.File.Exists(Server.MapPath("..\FileTemp\") & nomeFileScart.Value) Then

                ''    'SPOSTO IL FILE NELLA CARTELLA DEGLI ALLEGATI
                ''    System.IO.File.Move(Server.MapPath("..\FileTemp\") & nomeFileScart.Value, Server.MapPath("..\ALLEGATI\ABBINAMENTI\") & nomeFileScart.Value)
                ''Else
                ''    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                ''End If
                ExportExcel(dtElenco, nomeFileScart.Value)
            Else
                Session.Add("dtAbbinamenti", dtElenco)
                ExportExcelOK(dtElenco, nomeFileOK.Value)
            End If

            If scartati = False Then
                Session.Remove("ElencoInquilini")
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Abbinamenti convalidati con successo! Sarà ora visualizzato l\'elenco delle convalide effettuate!');" _
                        & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If

                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "location.replace('ElencoAbbAutomatici.aspx?NF=" & nomeFileOK.Value & "&NF2=" & nomeFileScart.Value & "','Elenco');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                End If
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub AbbinaERP(ByVal lIdDomanda As Long, ByVal idUnita As Long, ByRef dtERP As System.Data.DataTable, ByVal rowERP As System.Data.DataRow)
        Dim scriptblock As String = ""
        Dim DATASCADENZA As String = ""
        Dim N_ABBINAMENTO As String
        Dim PG As String = ""
        Dim dataPG As String = ""
        Dim idBando As Integer = 0
        Dim tipoAlloggio As Integer = 0
        Dim nominativo As String = ""
        Dim tipoGraduatoria As String = ""
        Dim codAlloggio As String = ""
        Dim dataDisponib As String = ""
        Dim indirizzo As String = ""
        Dim superficie As Double = 0
        Dim numAlloggio As String = ""
        Dim scala As String = ""
        Dim piano As String = ""
        'Dim idUnita As Long = 0
        'Dim lidDomanda As Long = 0

        Try
            'PAR.OracleConn.Open()
            'par.SettaCommand(par)
            'PAR.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans

            
            If dtERP.Columns.Count = 0 Then
                dtERP.Columns.Add("NUM_OFFERTA")
                dtERP.Columns.Add("NOMINATIVO")
                dtERP.Columns.Add("TIPO_GRAD")
                dtERP.Columns.Add("DATA_SCADENZA_OFF")
                dtERP.Columns.Add("COD_UI")
                dtERP.Columns.Add("SUPERFICIE")
                dtERP.Columns.Add("DATA_DISP")
                dtERP.Columns.Add("INDIRIZZO")
                dtERP.Columns.Add("NUM_ALL")
                dtERP.Columns.Add("SCALA")
                dtERP.Columns.Add("PIANO")
            End If

            Dim invito As Boolean = False
            Dim abbinam As Boolean = False

            PAR.cmd.CommandText = "SELECT * from DOMANDE_BANDO,COMP_NUCLEO,BANDI_GRADUATORIA_DEF,DICHIARAZIONI WHERE DICHIARAZIONI.ID = DOMANDE_BANDO.ID_DICHIARAZIONE AND COMP_NUCLEO.ID_DICHIARAZIONE = DICHIARAZIONI.ID AND DOMANDE_BANDO.progr_componente = COMP_NUCLEO.progr AND BANDI_GRADUATORIA_DEF.id_domanda = DOMANDE_BANDO.ID(+) AND DOMANDE_BANDO.ID =" & lIdDomanda
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader0.Read Then
                PG = PAR.IfNull(myReader0("PG"), "")
                dataPG = PAR.IfNull(myReader0("DATA_PG"), "")
                idBando = PAR.IfNull(myReader0("ID_BANDO"), "")
                tipoAlloggio = PAR.IfNull(myReader0("TIPO_ALLOGGIO"), 0)
                nominativo = PAR.IfNull(myReader0("COGNOME"), "") & " " & PAR.IfNull(myReader0("NOME"), "")
                Select Case PAR.IfNull(myReader0("TIPO"), "")
                    Case 0
                        tipoGraduatoria = "E.R.P"
                    Case 1
                        tipoGraduatoria = "ART.14"
                    Case 2
                        tipoGraduatoria = "ART.15"
                End Select
            End If
            myReader0.Close()


            '1° FASE: INVITO
            PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_REDDITI WHERE ID_DOMANDA=" & lIdDomanda
            Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader123.HasRows = True Then

                PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderD.Read() Then
                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='9',FL_INVITO='1' WHERE ID=" & lIdDomanda
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                        & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                        & "','F08','','I')"
                    PAR.cmd.ExecuteNonQuery()
                End If
                myReaderD.Close()
                invito = True
            Else
                If tipoAlloggio = 1 Then
                    PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                    Dim myReaderD1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReaderD1.Read() Then
                        PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='9',FL_INVITO='1' WHERE ID=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                            & "','F08','','I')"
                        PAR.cmd.ExecuteNonQuery()

                    End If
                    myReaderD1.Close()
                    invito = True
                Else
                    'REDDITI CONVENZIONALI MANCANTI, INVITO NON EFFETTUATO
                End If
            End If
            myReader123.Close()


            '2° FASE: ABBINAMENTO
            If invito = True Then
                PAR.cmd.CommandText = "SELECT * FROM ALLOGGI WHERE ID=" & idUnita & " for update nowait"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader.Read() Then
                    If myReader("STATO") = "5" Then
                        codAlloggio = PAR.IfNull(myReader("COD_ALLOGGIO"), "")
                        dataDisponib = PAR.IfNull(myReader("DATA_DISPONIBILITA"), "")
                        superficie = PAR.IfNull(myReader("SUP"), 0)
                        scala = PAR.IfNull(myReader("SCALA"), "")
                        piano = PAR.IfNull(myReader("PIANO"), "")
                        numAlloggio = PAR.IfNull(myReader("NUM_ALLOGGIO"), "")

                        PAR.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & PAR.IfNull(myReader("TIPO_INDIRIZZO"), "")
                        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReaderT.Read Then
                            indirizzo = PAR.IfNull(myReaderT("DESCRIZIONE"), "") & " " & PAR.IfNull(myReader("INDIRIZZO"), "") & ", " & PAR.IfNull(myReader("NUM_CIVICO"), "")
                        End If
                        myReaderT.Close()

                        DATASCADENZA = PAR.AggiustaData(Date.Parse(Format(Now, "dd/MM/yyyy"), New System.Globalization.CultureInfo("it-IT", False)).AddDays(10).ToString("dd/MM/yyyy"))

                        PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & lIdDomanda & ",'" & DATASCADENZA & "','1')"
                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        N_ABBINAMENTO = ""
                        If myReader2.Read() Then
                            N_ABBINAMENTO = myReader2(0)
                        End If
                        myReader2.Close()

                        PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_PROPOSTA='1' WHERE ID=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()


                        PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & lIdDomanda & "," & idUnita & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & lIdDomanda & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & idUnita
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                            & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                            & "2,7," _
                                            & idUnita & "," _
                                            & lIdDomanda & ",'')"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReader1.Read() = False Then
                            PAR.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                            PAR.cmd.ExecuteNonQuery()
                        End If
                        myReader1.Close()

                        PAR.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F10','','I')"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                            & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & PG & "','" & dataPG & "',10," & idBando & ",10)"
                        PAR.cmd.ExecuteNonQuery()

                        abbinam = True

                    Else
                        'ALLOGGIO NON PIU' DISPONIBILE
                    End If
                End If
            End If

            If abbinam = True Then
                rowERP = dtERP.NewRow()
                rowERP.Item("NUM_OFFERTA") = N_ABBINAMENTO
                rowERP.Item("NOMINATIVO") = nominativo
                rowERP.Item("TIPO_GRAD") = tipoGraduatoria
                rowERP.Item("DATA_SCADENZA_OFF") = PAR.FormattaData(DATASCADENZA)
                rowERP.Item("COD_UI") = codAlloggio
                rowERP.Item("SUPERFICIE") = superficie
                rowERP.Item("DATA_DISP") = PAR.FormattaData(dataDisponib)
                rowERP.Item("INDIRIZZO") = indirizzo
                rowERP.Item("NUM_ALL") = numAlloggio
                rowERP.Item("SCALA") = scala
                rowERP.Item("PIANO") = piano
                dtERP.Rows.Add(rowERP)
            End If

            'PAR.myTrans.Commit()
            'PAR.OracleConn.Close()
            'PAR.Dispose()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            


        Catch ex As Exception
            If Not IsNothing(PAR.myTrans) Then
                PAR.myTrans.Rollback()
            End If
            PAR.OracleConn.Close()
            PAR.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub AbbinaCAMBI(ByVal lIdDomanda As Long, ByVal idUnita As Long, ByRef dtERP As System.Data.DataTable, ByVal rowERP As System.Data.DataRow)
        Dim scriptblock As String = ""
        Dim DATASCADENZA As String = ""
        Dim N_ABBINAMENTO As String
        Dim PG As String = ""
        Dim dataPG As String = ""
        Dim idBando As Integer = 0
        Dim tipoAlloggio As Integer = 0
        Dim nominativo As String = ""
        Dim tipoGraduatoria As String = ""
        Dim codAlloggio As String = ""
        Dim dataDisponib As String = ""
        Dim indirizzo As String = ""
        Dim superficie As Double = 0
        Dim numAlloggio As String = ""
        Dim scala As String = ""
        Dim piano As String = ""
        'Dim idUnita As Long = 0
        'Dim lidDomanda As Long = 0

        Try
            'PAR.OracleConn.Open()
            'par.SettaCommand(par)
            'PAR.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans


            If dtERP.Columns.Count = 0 Then
                dtERP.Columns.Add("NUM_OFFERTA")
                dtERP.Columns.Add("NOMINATIVO")
                dtERP.Columns.Add("TIPO_GRAD")
                dtERP.Columns.Add("DATA_SCADENZA_OFF")
                dtERP.Columns.Add("COD_UI")
                dtERP.Columns.Add("SUPERFICIE")
                dtERP.Columns.Add("DATA_DISP")
                dtERP.Columns.Add("INDIRIZZO")
                dtERP.Columns.Add("NUM_ALL")
                dtERP.Columns.Add("SCALA")
                dtERP.Columns.Add("PIANO")
            End If

            Dim invito As Boolean = False
            Dim abbinam As Boolean = False

            PAR.cmd.CommandText = "SELECT * from DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,BANDI_GRADUATORIA_DEF_CAMBI,DICHIARAZIONI_CAMBI WHERE DICHIARAZIONI_CAMBI.ID = DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE = DICHIARAZIONI_CAMBI.ID AND DOMANDE_BANDO_CAMBI.progr_componente = COMP_NUCLEO_CAMBI.progr AND BANDI_GRADUATORIA_DEF_CAMBI.id_domanda = DOMANDE_BANDO_CAMBI.ID(+) AND DOMANDE_BANDO_CAMBI.ID =" & lIdDomanda
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader0.Read Then
                PG = PAR.IfNull(myReader0("PG"), "")
                dataPG = PAR.IfNull(myReader0("DATA_PG"), "")
                idBando = PAR.IfNull(myReader0("ID_BANDO"), "")
                tipoAlloggio = PAR.IfNull(myReader0("TIPO_ALLOGGIO"), 0)
                nominativo = PAR.IfNull(myReader0("COGNOME"), "") & " " & PAR.IfNull(myReader0("NOME"), "")
            End If
            myReader0.Close()


            '1° FASE: INVITO
            PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_REDDITI_CAMBI WHERE ID_DOMANDA=" & lIdDomanda
            Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader123.HasRows = True Then

                PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_CAMBI WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderD.Read() Then
                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET id_stato='9',FL_INVITO='1' WHERE ID=" & lIdDomanda
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                        & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                        & "','F08','','I')"
                    PAR.cmd.ExecuteNonQuery()
                End If
                myReaderD.Close()
                invito = True
            Else
                If tipoAlloggio = 1 Then
                    PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_CAMBI WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                    Dim myReaderD1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReaderD1.Read() Then
                        PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET id_stato='9',FL_INVITO='1' WHERE ID=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                            & "','F08','','I')"
                        PAR.cmd.ExecuteNonQuery()

                    End If
                    myReaderD1.Close()
                    invito = True
                Else
                    'REDDITI CONVENZIONALI MANCANTI, INVITO NON EFFETTUATO
                End If
            End If
            myReader123.Close()


            '2° FASE: ABBINAMENTO
            If invito = True Then
                PAR.cmd.CommandText = "SELECT * FROM ALLOGGI WHERE ID=" & idUnita & " for update nowait"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader.Read() Then
                    If myReader("STATO") = "5" Then
                        codAlloggio = PAR.IfNull(myReader("COD_ALLOGGIO"), "")
                        dataDisponib = PAR.IfNull(myReader("DATA_DISPONIBILITA"), "")
                        superficie = PAR.IfNull(myReader("SUP"), 0)
                        scala = PAR.IfNull(myReader("SCALA"), "")
                        piano = PAR.IfNull(myReader("PIANO"), "")
                        numAlloggio = PAR.IfNull(myReader("NUM_ALLOGGIO"), "")

                        PAR.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & PAR.IfNull(myReader("TIPO_INDIRIZZO"), "")
                        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReaderT.Read Then
                            indirizzo = PAR.IfNull(myReaderT("DESCRIZIONE"), "") & " " & PAR.IfNull(myReader("INDIRIZZO"), "") & ", " & PAR.IfNull(myReader("NUM_CIVICO"), "")
                        End If
                        myReaderT.Close()

                        DATASCADENZA = PAR.AggiustaData(Date.Parse(Format(Now, "dd/MM/yyyy"), New System.Globalization.CultureInfo("it-IT", False)).AddDays(10).ToString("dd/MM/yyyy"))

                        PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & lIdDomanda & ",'" & DATASCADENZA & "','1')"
                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        N_ABBINAMENTO = ""
                        If myReader2.Read() Then
                            N_ABBINAMENTO = myReader2(0)
                        End If
                        myReader2.Close()

                        PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET FL_PROPOSTA='1' WHERE ID=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()


                        PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & lIdDomanda & "," & idUnita & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & lIdDomanda & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & idUnita
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                            & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                            & "2,7," _
                                            & idUnita & "," _
                                            & lIdDomanda & ",'')"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReader1.Read() = False Then
                            PAR.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                            PAR.cmd.ExecuteNonQuery()
                        End If
                        myReader1.Close()

                        PAR.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F10','','I')"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                            & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & PG & "','" & dataPG & "',10," & idBando & ",10)"
                        PAR.cmd.ExecuteNonQuery()

                        abbinam = True
                    Else
                        'ALLOGGIO NON PIU' DISPONIBILE
                    End If
                End If
            End If

            If abbinam = True Then
                rowERP = dtERP.NewRow()
                rowERP.Item("NUM_OFFERTA") = N_ABBINAMENTO
                rowERP.Item("NOMINATIVO") = nominativo
                rowERP.Item("TIPO_GRAD") = "BANDO CAMBI"
                rowERP.Item("DATA_SCADENZA_OFF") = PAR.FormattaData(DATASCADENZA)
                rowERP.Item("COD_UI") = codAlloggio
                rowERP.Item("SUPERFICIE") = superficie
                rowERP.Item("DATA_DISP") = PAR.FormattaData(dataDisponib)
                rowERP.Item("INDIRIZZO") = indirizzo
                rowERP.Item("NUM_ALL") = numAlloggio
                rowERP.Item("SCALA") = scala
                rowERP.Item("PIANO") = piano
                dtERP.Rows.Add(rowERP)
            End If

            

        Catch ex As Exception
            If Not IsNothing(PAR.myTrans) Then
                PAR.myTrans.Rollback()
            End If
            PAR.OracleConn.Close()
            PAR.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub AbbinaEMERG(ByVal lIdDomanda As Long, ByVal idUnita As Long, ByRef dtERP As System.Data.DataTable, ByVal rowERP As System.Data.DataRow)
        Dim scriptblock As String = ""
        Dim DATASCADENZA As String = ""
        Dim N_ABBINAMENTO As String
        Dim PG As String = ""
        Dim dataPG As String = ""
        Dim idBando As Integer = 0
        Dim tipoAlloggio As Integer = 0
        Dim nominativo As String = ""
        Dim tipoGraduatoria As String = ""
        Dim codAlloggio As String = ""
        Dim dataDisponib As String = ""
        Dim indirizzo As String = ""
        Dim superficie As Double = 0
        Dim numAlloggio As String = ""
        Dim scala As String = ""
        Dim piano As String = ""
        'Dim idUnita As Long = 0
        'Dim lidDomanda As Long = 0

        Try
            

            If dtERP.Columns.Count = 0 Then
                dtERP.Columns.Add("NUM_OFFERTA")
                dtERP.Columns.Add("NOMINATIVO")
                dtERP.Columns.Add("TIPO_GRAD")
                dtERP.Columns.Add("DATA_SCADENZA_OFF")
                dtERP.Columns.Add("COD_UI")
                dtERP.Columns.Add("SUPERFICIE")
                dtERP.Columns.Add("DATA_DISP")
                dtERP.Columns.Add("INDIRIZZO")
                dtERP.Columns.Add("NUM_ALL")
                dtERP.Columns.Add("SCALA")
                dtERP.Columns.Add("PIANO")
            End If

            Dim invito As Boolean = False
            Dim abbinam As Boolean = False

            PAR.cmd.CommandText = "SELECT * from DOMANDE_BANDO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND DOMANDE_BANDO_VSA.progr_componente = COMP_NUCLEO_VSA.progr AND DOMANDE_BANDO_VSA.ID =" & lIdDomanda
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader0.Read Then
                PG = PAR.IfNull(myReader0("PG"), "")
                dataPG = PAR.IfNull(myReader0("DATA_PG"), "")
                idBando = PAR.IfNull(myReader0("ID_BANDO"), "")
                tipoAlloggio = PAR.IfNull(myReader0("TIPO_ALLOGGIO"), 0)
                nominativo = PAR.IfNull(myReader0("COGNOME"), "") & " " & PAR.IfNull(myReader0("NOME"), "")
            End If
            myReader0.Close()


            '1° FASE: INVITO
            PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & lIdDomanda
            Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader123.HasRows = True Then

                PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderD.Read() Then
                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='9',FL_INVITO='1' WHERE ID=" & lIdDomanda
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                        & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                        & "','F08','','I')"
                    PAR.cmd.ExecuteNonQuery()
                End If
                myReaderD.Close()
                invito = True
            Else
                If tipoAlloggio = 1 Then
                    PAR.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                    Dim myReaderD1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReaderD1.Read() Then
                        PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='9',FL_INVITO='1' WHERE ID=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                            & "','F08','','I')"
                        PAR.cmd.ExecuteNonQuery()

                    End If
                    myReaderD1.Close()
                    invito = True
                Else
                    'REDDITI CONVENZIONALI MANCANTI, INVITO NON EFFETTUATO
                End If
            End If
            myReader123.Close()


            '2° FASE: ABBINAMENTO
            If invito = True Then
                PAR.cmd.CommandText = "SELECT * FROM ALLOGGI WHERE ID=" & idAlloggio.Value & " for update nowait"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader.Read() Then
                    If myReader("STATO") = "5" Then
                        codAlloggio = PAR.IfNull(myReader("COD_ALLOGGIO"), "")
                        dataDisponib = PAR.IfNull(myReader("DATA_DISPONIBILITA"), "")
                        superficie = PAR.IfNull(myReader("SUP"), 0)
                        scala = PAR.IfNull(myReader("SCALA"), "")
                        piano = PAR.IfNull(myReader("PIANO"), "")
                        numAlloggio = PAR.IfNull(myReader("NUM_ALLOGGIO"), "")

                        PAR.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & PAR.IfNull(myReader("TIPO_INDIRIZZO"), "")
                        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReaderT.Read Then
                            indirizzo = PAR.IfNull(myReaderT("DESCRIZIONE"), "") & " " & PAR.IfNull(myReader("INDIRIZZO"), "") & ", " & PAR.IfNull(myReader("NUM_CIVICO"), "")
                        End If
                        myReaderT.Close()


                        DATASCADENZA = PAR.AggiustaData(Date.Parse(Format(Now, "dd/MM/yyyy"), New System.Globalization.CultureInfo("it-IT", False)).AddDays(10).ToString("dd/MM/yyyy"))

                        PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & lIdDomanda & ",'" & DATASCADENZA & "','1')"
                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        N_ABBINAMENTO = ""
                        If myReader2.Read() Then
                            N_ABBINAMENTO = myReader2(0)
                        End If
                        myReader2.Close()

                        PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_vsa SET FL_PROPOSTA='1' WHERE ID=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()


                        PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & lIdDomanda
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & lIdDomanda & "," & idAlloggio.Value & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & lIdDomanda & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & idAlloggio.Value
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                            & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                            & "2,7," _
                                            & idAlloggio.Value & "," _
                                            & lIdDomanda & ",'')"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                        If myReader1.Read() = False Then
                            PAR.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                            PAR.cmd.ExecuteNonQuery()
                        End If
                        myReader1.Close()

                        PAR.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F10','','I')"
                        PAR.cmd.ExecuteNonQuery()

                        PAR.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                            & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & PG & "','" & dataPG & "',10," & idBando & ",10)"
                        PAR.cmd.ExecuteNonQuery()
                        abbinam = True
                    End If
                End If
            End If

            If abbinam = True Then
                rowERP = dtERP.NewRow()
                rowERP.Item("NUM_OFFERTA") = N_ABBINAMENTO
                rowERP.Item("NOMINATIVO") = nominativo
                rowERP.Item("TIPO_GRAD") = "CAMBI EMERGENZA"
                rowERP.Item("DATA_SCADENZA_OFF") = PAR.FormattaData(DATASCADENZA)
                rowERP.Item("COD_UI") = codAlloggio
                rowERP.Item("SUPERFICIE") = superficie
                rowERP.Item("DATA_DISP") = PAR.FormattaData(dataDisponib)
                rowERP.Item("INDIRIZZO") = indirizzo
                rowERP.Item("NUM_ALL") = numAlloggio
                rowERP.Item("SCALA") = scala
                rowERP.Item("PIANO") = piano
                dtERP.Rows.Add(rowERP)
            End If

        Catch ex As Exception
            If Not IsNothing(PAR.myTrans) Then
                PAR.myTrans.Rollback()
            End If
            PAR.OracleConn.Close()
            PAR.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
