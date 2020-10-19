
Partial Class Contabilita_NucleoRiepilogo
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim Query As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim testoTabella As String = ""
            Dim COLORE As String = "#E6E6E6"

            If Request.QueryString("IDAU") <> "" Then

                Query = "SELECT ID,COGNOME, NOME, COD_FISCALE, SESSO, to_char(to_date(DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA ,T_TIPO_PARENTELA.DESCRIZIONE AS GR_PARENT, PERC_INVAL FROM SEPA.UTENZA_COMP_NUCLEO, SEPA.T_TIPO_PARENTELA WHERE T_TIPO_PARENTELA.COD=UTENZA_COMP_NUCLEO.GRADO_PARENTELA AND ID_DICHIARAZIONE = " & Request.QueryString("IDAU")

                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                & "<tr>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>COGNOME</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>NOME</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.FISCALE</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>SESSO</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA NASCITA</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>GRADO PARENTELA</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>PERC. INVALIDITA'</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "</tr>"

                par.cmd.CommandText = Query
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read

                    testoTabella = testoTabella _
                    & "<tr bgcolor = '" & COLORE & "'>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("COGNOME"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("NOME"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("COD_FISCALE"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("SESSO"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("DATA_NASCITA"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("GR_PARENT"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("PERC_INVAL"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "</tr>"



                    If COLORE = "#FFFFFF" Then
                        COLORE = "#E6E6E6"
                    Else
                        COLORE = "#FFFFFF"
                    End If
                Loop
                myReader.Close()
                Me.TBL_NUCLEO.Text = testoTabella & "</table>"
            ElseIf Request.QueryString("ID_CONT") <> "" Then

                'CONTINUA A PRENDERE I DATI IN JOIN CON SOGGETTI_CONTRATTUALI PERCHE' MI SERVE IL CODICE PARENTELA, E COMUNQUE GIA' I DATI DELLA PAGINA PRECEDENTE SONO PRESI IN JOIN CON INTESTATARI_RAPPORTI
                par.cmd.CommandText = "SELECT ID, COGNOME,NOME, SESSO,COD_FISCALE, TO_CHAR(TO_DATE(DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA, TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTELA FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPOLOGIA_PARENTELA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA= ANAGRAFICA.ID  AND ID_CONTRATTO = " & Request.QueryString("ID_CONT") & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then

                    If par.IfNull(myReader("COGNOME"), "") <> "" AndAlso par.IfNull(myReader("NOME"), "") <> "" Then
                        Me.LblIntestazione.Text = "Informazioni Anagrafiche sui Componenti del Nucleo"

                        Query = "SELECT ID, COGNOME,NOME, SESSO,COD_FISCALE, TO_CHAR(TO_DATE(DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA, TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTELA FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPOLOGIA_PARENTELA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA= ANAGRAFICA.ID  AND ID_CONTRATTO = " & Request.QueryString("ID_CONT") & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD"

                        testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>COGNOME</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>NOME</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>COD.FISCALE</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>SESSO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA NASCITA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>PARENTELA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "</td>" _
                        & "<td style='height: 19px'>" _
                        & "</td>" _
                        & "</tr>"

                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        par.cmd.CommandText = Query

                        Do While myReader2.Read

                            testoTabella = testoTabella _
                            & "<tr bgcolor = '" & COLORE & "'>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("COGNOME"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("NOME"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("COD_FISCALE"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("SESSO"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("DATA_NASCITA"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("PARENTELA"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"

                            If COLORE = "#FFFFFF" Then
                                COLORE = "#E6E6E6"
                            Else
                                COLORE = "#FFFFFF"
                            End If
                        Loop
                        Me.TBL_NUCLEO.Text = testoTabella & "</table>"
                        myReader2.Close()
                        myReader.Close()
                    Else
                        Me.LblIntestazione.Text = "Riepilogo Anagrafico della Società"
                        Query = "SELECT ID, RAGIONE_SOCIALE , PARTITA_IVA , to_char(to_date(DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA FROM SISCOM_MI.ANAGRAFICA WHERE ID IN  (SELECT ID_ANAGRAFICA FROM SISCOM_MI.INTESTATARI_RAPPORTO WHERE ID_CONTRATTO = " & Request.QueryString("ID_CONT") & ")"

                        testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>RAGIONE SOCIALE</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>P.IVA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA_NASCITA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "</td>" _
                        & "<td style='height: 19px'>" _
                        & "</td>" _
                        & "</tr>"

                        par.cmd.CommandText = Query
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader2.Read

                            testoTabella = testoTabella _
                            & "<tr bgcolor = '" & COLORE & "'>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("RAGIONE_SOCIALE"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("PARTITA_IVA"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader2("DATA_NASCITA"), "") & "</span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"

                            If COLORE = "#FFFFFF" Then
                                COLORE = "#E6E6E6"
                            Else
                                COLORE = "#FFFFFF"
                            End If
                        Loop
                        Me.TBL_NUCLEO.Text = testoTabella & "</table>"
                        myReader2.Close()
                        myReader.Close()
                    End If
                    myReader.Close()

                End If

            Else
                Response.Write("<SCRIPT>alert('Non è possibile trovare informazioni nell\'Anagrafe Utenza!');</SCRIPT>")
                Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
                Exit Sub
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.CommandText = ""
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End If
        Query = ""

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    End Sub

End Class
