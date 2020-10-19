'*** STAMPA RISULTATO RICERCA SEGNALAZIONI

Partial Class ReportRisultatoSegnalazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreStato As String
    Public sOrdinamento As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim sStr1 As String = ""
        Dim sOrder As String = ""


        Dim FlagConnessione As Boolean = False

        Dim sStrID_TAB_FILIALI As String = ""
        Dim sStrID_ID_TIPOLOGIE As String = ""
        Dim sID_TIPO_ST As String = ""

        Dim sTipoRichiesta As String = "1"


        Dim sFiliale As String = "-1"
        If Session.Item("LIVELLO") <> "1" Then
            sFiliale = Session.Item("ID_STRUTTURA")
        End If



        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO SEGNALAZIONI"

                sValoreStato = Request.QueryString("ST")
                sOrdinamento = Request.QueryString("ORD")

                Select Case sOrdinamento
                    Case "DATA"
                        sOrder = " order by DATA_INSERIMENTO"
                    Case "TIPO"
                        sOrder = " order by SISCOM_MI.SEGNALAZIONI.ID_STATO"
                    Case "ID"
                        sOrder = " order by SISCOM_MI.SEGNALAZIONI.ID"

                    Case Else
                        sOrder = ""
                End Select

                ' APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    FlagConnessione = True
                End If


                If sFiliale <> "" Then

                    par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        sID_TIPO_ST = par.IfNull(myReader1("ID_TIPO_ST"), 0)
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
                                      & " where ID_TIPO_ST=" & sID_TIPO_ST

                    If sID_TIPO_ST = 2 Then
                        par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
                    End If
                    myReader1 = par.cmd.ExecuteReader()

                    While myReader1.Read
                        If sStrID_ID_TIPOLOGIE = "" Then
                            sStrID_ID_TIPOLOGIE = myReader1(0)
                        Else
                            sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
                        End If
                    End While
                    myReader1.Close()

                    If sID_TIPO_ST = 1 Then
                        par.cmd.CommandText = "select ID from SISCOM_MI.TAB_FILIALI where ID_TECNICA=" & sFiliale
                        myReader1 = par.cmd.ExecuteReader()

                        While myReader1.Read
                            If sStrID_TAB_FILIALI = "" Then
                                sStrID_TAB_FILIALI = myReader1(0)
                            Else
                                sStrID_TAB_FILIALI = sStrID_TAB_FILIALI & "," & myReader1(0)
                            End If
                        End While
                        myReader1.Close()

                    End If


                    'COMPLESSO
                    sStr1 = sStr1 & " select SEGNALAZIONI.ID, " _
                                & " ID_COMPLESSO as IDENTIFICATIVO," _
                                & " 'C' as TIPO_S," _
                                & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                                & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                                & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                         & " from SISCOM_MI.SEGNALAZIONI " _
                         & " where SEGNALAZIONI.ID_STATO=" & sValoreStato _
                         & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                         & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
                         & "   and SEGNALAZIONI.ID_EDIFICIO is NULL " _
                         & "   and SEGNALAZIONI.ID_UNITA Is  NULL "

                    If sID_TIPO_ST = 0 Then
                        sStr1 = sStr1 & "   and SEGNALAZIONI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                       & " where ID_FILIALE=" & sFiliale & ") "
                    ElseIf sID_TIPO_ST = 1 Then
                        sStr1 = sStr1 & "   and SEGNALAZIONI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                              & " where ID_FILIALE=" & sStrID_TAB_FILIALI & ") "
                    End If


                    'EDIFICIO
                    sStr1 = sStr1 & " union select SEGNALAZIONI.ID, " _
                                & " ID_EDIFICIO as IDENTIFICATIVO," _
                                & " 'E' as TIPO_S," _
                                & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                                & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                                & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                         & " from SISCOM_MI.SEGNALAZIONI " _
                         & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=" & sValoreStato _
                         & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                         & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
                         & "   and SEGNALAZIONI.ID_UNITA is NULL "

                    If sID_TIPO_ST = 0 Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                      & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                             & " where ID_FILIALE=" & sFiliale & ") ) "

                    ElseIf sID_TIPO_ST = 1 Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                      & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                             & " where ID_FILIALE=" & sStrID_TAB_FILIALI & ") ) "
                    End If

                    'UNITA
                    sStr1 = sStr1 & " union select SEGNALAZIONI.ID, " _
                                & " ID_UNITA as IDENTIFICATIVO," _
                                & " 'U' as TIPO_S," _
                                & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                                & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                                & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                         & " from SISCOM_MI.SEGNALAZIONI " _
                         & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=" & sValoreStato _
                         & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                         & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
                         & "   and SEGNALAZIONI.ID_EDIFICIO Is Not NULL " _
                         & "   and SEGNALAZIONI.ID_COMPLESSO Is Not NULL "

                    If sID_TIPO_ST = 0 Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_UNITA in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                            & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                   & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                         & " where ID_FILIALE=" & sFiliale & ") ))"

                    ElseIf sID_TIPO_ST = 1 Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_UNITA in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                            & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                   & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                         & " where ID_FILIALE=" & sStrID_TAB_FILIALI & ") ))"
                    End If



                Else

                    sStr1 = "select SEGNALAZIONI.ID," _
                                & " case when ID_UNITA      is not null then ID_UNITA " _
                                      & "when ID_EDIFICIO   is not null then ID_EDIFICIO " _
                                      & "when ID_COMPLESSO  is not null then ID_COMPLESSO " _
                                & " end as IDENTIFICATIVO," _
                                & " case when ID_UNITA      is not null then 'U' " _
                                     & " when ID_EDIFICIO   is not null then 'E' " _
                                     & " when ID_COMPLESSO  is not null then 'C' " _
                                & " end as TIPO_S,COGNOME_RS||' '||NOME as RICHIEDENTE," _
                                & "to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO," _
                                & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC " _
                            & " from SISCOM_MI.SEGNALAZIONI " _
                            & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=" & sValoreStato _
                            & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta


                End If

                sStr1 = sStr1 & sOrder


                '*** CARICO LA GRIGLIA
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStr1, par.OracleConn)
                Dim ds As New Data.DataSet()

                da.Fill(ds, "SISCOM_MI.SEGNALAZIONI")

                DataGrid1.DataSource = ds
                DataGrid1.DataBind()


                lblTotale.Text = "TOTALE SEGNALAZIONI TROVATE: " & ds.Tables(0).Rows.Count


                '************CHIUSURA CONNESSIONE**********
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    FlagConnessione = False
                End If

            Catch ex As Exception
                '************CHIUSURA CONNESSIONE**********
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            End Try
        End If

    End Sub

End Class
