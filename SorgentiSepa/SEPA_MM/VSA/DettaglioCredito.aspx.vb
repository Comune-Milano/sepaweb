
Partial Class VSA_DettaglioCredito
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim TotImporto As Double = 0
    Dim TotContabile As Double = 0
    Dim TotPagato As Double = 0
    Dim TotMorosita As Decimal = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaDettaglio()
        End If
    End Sub

    Private Sub CaricaDettaglio()
        Try
            '******APERTURA CONNESSIONE*****

            If Request.QueryString("ID") <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim testoTabella As String = ""
                Dim COLORE As String = "#E6E6E6"
                Dim EmessoContabile As Double = 0
                Dim ImportoBolletta As Double = 0
                Dim Contatore As Integer = 0

                Dim GiorniRitardo As String = 0
                Dim IDANA As String = ""

                par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",anagrafica.id as id_ana,COD_CONTRATTO FROM DOMANDE_BANDO_VSA,SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.COD_CONTRATTO=DOMANDE_BANDO_VSA.CONTRATTO_NUM AND DOMANDE_BANDO_VSA.ID = " & Request.QueryString("ID") & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.LblIntestazione.Text = myReader("INTESTATARIO") & " COD. CONTRATTO " & myReader("COD_CONTRATTO") '& " - " & UltimoPagam
                    IDANA = myReader("id_ana")
                End If

                myReader.Close()
                testoTabella = "<table cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf _
                                & "<tr>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>ANNO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO A CREDITO/DEBITO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>NOTE</strong></span></td>" _
                                & "</tr>"




                par.cmd.CommandText = "select * from VSA_CREDITI_RID_CANONE where id_domanda=" & Request.QueryString("ID") & " ORDER BY ANNO DESC"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read

                   

                        Contatore = Contatore + 1

                    testoTabella = testoTabella _
                    & "<tr bgcolor = '" & COLORE & "'>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("ANNO"), "") & "</a></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("IMPORTO"), "") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader1("NOTE"), "") & "</span></td>" _
                    & "</tr>"


                    TotImporto = TotImporto + par.IfNull(myReader1("importo"), 0)


                        If COLORE = "#FFFFFF" Then
                            COLORE = "#E6E6E6"
                        Else
                            COLORE = "#FFFFFF"
                        End If


                Loop

                myReader1.Close()

                testoTabella = testoTabella _
                    & "<tr bgcolor = '" & COLORE & "'>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>TOTALE</a></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & TotImporto & "</span></td>" _
                     & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                    & "</tr>"

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                Me.TBL_DETTAGLIO_SALDO.Text = testoTabella & "</table>"


            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
