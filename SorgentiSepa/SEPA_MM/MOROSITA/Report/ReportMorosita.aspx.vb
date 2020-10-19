' REPORT LISTA MOROSITA'

Partial Class ReportMorosita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreStrutturaAler As String
    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreIndirizzo As String
    Public sValoreCivico As String

    Public sValoreCodice As String
    Public sValoreCognome As String
    Public sValoreNome As String

    Public sValoreTI As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreData_Dal_P As String
    Public sValoreData_Al_P As String
    Public sValoreProtocollo As String

    Public sOrdinamento As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sStringaSql, sStringaSQL1 As String
        Dim sValore As String
        Dim sCompara As String


        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack Then

            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreIndirizzo = Request.QueryString("IN")
            sValoreCivico = Request.QueryString("CI")

            sValoreTI = UCase(Request.QueryString("TI"))

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreData_Dal_P = Request.QueryString("DAL_P")
            sValoreData_Al_P = Request.QueryString("AL_P")
            sValoreProtocollo = Request.QueryString("PRO")

            sValoreCodice = Request.QueryString("CD")
            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")

            sOrdinamento = Request.QueryString("ORD")

            Try

                par.OracleConn.Open()
                par.SettaCommand(par)


                sStringaSql = ""

                If sValoreCognome <> "" Then
                    sValore = Strings.UCase(sValoreCognome)
                    If InStr(sValore, "*") Then
                        sCompara = " LIKE "
                        Call par.ConvertiJolly(sValore)
                    Else
                        sCompara = " = "
                    End If
                    sStringaSql = "  SISCOM_MI.MOROSITA.ID in (" _
                                                    & " select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE " _
                                                    & "  where ID_ANAGRAFICA in (select ID from SISCOM_MI.ANAGRAFICA " _
                                                                            & "  where COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                    If sValoreNome <> "" Then
                        sStringaSql = sStringaSql & " and ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                    End If
                    sStringaSql = sStringaSql & "))"

                ElseIf sValoreNome <> "" Then
                    sValore = Strings.UCase(sValoreNome)
                    If InStr(sValore, "*") Then
                        sCompara = " LIKE "
                        Call par.ConvertiJolly(sValore)
                    Else
                        sCompara = " = "
                    End If

                    sStringaSql = "  SISCOM_MI.MOROSITA.ID in (" _
                                                    & " select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE " _
                                                    & "  where ID_ANAGRAFICA in (select ID from SISCOM_MI.ANAGRAFICA " _
                                                                            & "  where NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "')) "
                End If


                If sValoreCodice <> "" Then
                    sValore = Strings.UCase(sValoreCodice)
                    If InStr(sValore, "*") Then
                        sCompara = " LIKE "
                        Call par.ConvertiJolly(sValore)
                    Else
                        sCompara = " = "
                    End If

                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

                    sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                                    & " select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE " _
                                                    & "  where COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "') "


                End If


                If par.IfEmpty(sValoreIndirizzo, "-1") <> "-1" Then

                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

                    sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                                    & " select  ID_MOROSITA " _
                                                    & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                                    & " where   ID_CONTRATTO in (" _
                                                            & " select  ID_CONTRATTO " _
                                                            & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                            & " where   ID_UNITA in (" _
                                                                    & " select  ID " _
                                                                    & " from    SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                    & " where   ID_INDIRIZZO in (" _
                                                                         & " select ID from SISCOM_MI.INDIRIZZI " _
                                                                                 & " where DESCRIZIONE='" & par.PulisciStrSql(sValoreIndirizzo) & "'"




                    If par.IfEmpty(sValoreCivico, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and CIVICO='" & par.PulisciStrSql(sValoreCivico) & "'))))"
                    Else
                        sStringaSql = sStringaSql & " )))) "
                    End If

                ElseIf par.IfEmpty(sValoreEdificio, "-1") <> "-1" Then

                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

                    sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                                    & " select  ID_MOROSITA " _
                                                    & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                                    & " where   ID_CONTRATTO in (" _
                                                            & " select  ID_CONTRATTO " _
                                                            & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                            & " where   ID_UNITA in (" _
                                                                    & " select  ID " _
                                                                    & " from    SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                    & " where   ID_EDIFICIO=" & sValoreEdificio _
                                                    & "))) "


                ElseIf par.IfEmpty(sValoreComplesso, "-1") <> "-1" Then

                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

                    sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                                    & " select  ID_MOROSITA " _
                                                    & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                                    & " where   ID_CONTRATTO in (" _
                                                            & " select  ID_CONTRATTO " _
                                                            & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                            & " where   ID_UNITA in (" _
                                                                    & " select  ID " _
                                                                    & " from    SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                    & " where   ID_EDIFICIO in (" _
                                                                            & " select  ID " _
                                                                            & " from    SISCOM_MI.EDIFICI " _
                                                                            & " where   ID_COMPLESSO=" & sValoreComplesso _
                                                    & " )))) "
                ElseIf par.IfEmpty(sValoreStrutturaAler, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                                    & " select  ID_MOROSITA " _
                                                    & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                                    & " where   ID_CONTRATTO in (" _
                                                            & " select  ID_CONTRATTO " _
                                                            & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                            & " where   ID_UNITA in (" _
                                                                    & " select  ID " _
                                                                    & " from    SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                    & " where   ID_EDIFICIO in (" _
                                                                            & " select  ID " _
                                                                            & " from    SISCOM_MI.EDIFICI " _
                                                                            & " where   ID_COMPLESSO IN (" _
                                                                                    & " select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                    & " where ID_FILIALE=" & sValoreStrutturaAler & ")" _
                                                    & " )))) "


                End If


                If sValoreData_Dal <> "" Then
                    If sValoreData_Al <> "" Then

                        If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
                        sStringaSql = sStringaSql & " RIF_DA>=" & sValoreData_Dal & " and RIF_A<=" & sValoreData_Al

                    Else

                        If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
                        sStringaSql = sStringaSql & " RIF_DA>=" & sValoreData_Dal

                    End If
                ElseIf sValoreData_Al <> "" Then

                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
                    sStringaSql = sStringaSql & " RIF_A<=" & sValoreData_Al

                End If



                If sValoreTI <> "" Then

                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

                    sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                                    & " select  ID_MOROSITA " _
                                                    & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                                    & " where   ID_CONTRATTO in (" _
                                                            & " select  ID_CONTRATTO " _
                                                            & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                            & " where   TIPOLOGIA='" & par.PulisciStrSql(sValoreTI) & "' " _
                                                    & " )) "

                End If


                '*** PROTOCOLLO 
                If sValoreProtocollo <> "" Then
                    sValore = Strings.UCase(sValoreProtocollo)
                    If InStr(sValore, "*") Then
                        sCompara = " LIKE "
                        Call par.ConvertiJolly(sValore)
                    Else
                        sCompara = " = "
                    End If

                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
                    sStringaSql = sStringaSql & "  UPPER(MOROSITA.PROTOCOLLO_ALER) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
                End If
                '********************************

                '*** DATA_PROTOCOLLO 
                If sValoreData_Dal_P <> "" Then
                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

                    sStringaSql = sStringaSql & " MOROSITA.DATA_PROTOCOLLO>='" & sValoreData_Dal_P & "' "
                End If

                If sValoreData_Al_P <> "" Then
                    If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

                    sStringaSql = sStringaSql & " MOROSITA.DATA_PROTOCOLLO<='" & sValoreData_Al_P & "' "
                End If
                '********************************

                sStringaSQL1 = "select  MOROSITA.ID as ID_MOROSITA,MOROSITA.PROTOCOLLO_ALER," _
                                    & " to_char(to_date(substr(MOROSITA.DATA_PROTOCOLLO,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PROTOCOLLO," _
                                    & " MOROSITA.TIPO_INVIO," _
                                    & " to_char(to_date(substr(MOROSITA.RIF_DA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_DAL," _
                                    & " to_char(to_date(substr(MOROSITA.RIF_A,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_AL," _
                                    & "NOTE ,MOROSITA.DATA_PROTOCOLLO as ""DATA_PROTOCOLLO_ORD""" _
                          & " from  " _
                                & " SISCOM_MI.MOROSITA"


                If sStringaSql <> "" Then
                    sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql
                End If

                sStringaSQL1 = sStringaSQL1 & " ORDER BY " & sOrdinamento


                par.cmd.CommandText = sStringaSQL1

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim ds As New Data.DataSet()

                da.Fill(ds)


                DataGrid1.DataSource = ds
                DataGrid1.DataBind()

                da.Dispose()
                ds.Dispose()
                '*************************


                par.cmd.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        End If


    End Sub

End Class
